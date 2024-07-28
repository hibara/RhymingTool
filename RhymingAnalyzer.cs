using MeCab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using static RhymingTool.DictionaryCreator;

namespace RhymingTool
{
  /// <summary>
  /// ライミング検索クラス
  /// </summary>
  /// <param name="dbPath">ライミング・ツールが使う辞書ファイルパス</param>
  public class RhymingAnalyzer(string dbPath)
  {
    // 進捗表示用デリゲート
    public delegate void ProgressReportDelegate(int percentage, string status);

    private readonly VowelExtractor _vowelExtractor = new();
    public double MatchThreshold { get; set; } = 0.75;   // 75%のマッチ率
    public int MaxPronunciationWords { get; set; } = 0; // 発音でピックアップする単語の最大数
    public int MaxVowelWords { get; set; } = 20;         // 母音でピックアップする単語の最大数

    private ProgressReportDelegate _progressReport;
    public ProgressReportDelegate ProgressReport
    {
      get => _progressReport;
      set => _progressReport = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// 入力した文字列（文章）を形態素解析して出力するまでの一連の処理群
    /// </summary>
    /// <param name="inputText">入力テキスト</param>
    /// <returns></returns>
    public async Task<string> AnalyzeAndRhymeAsync(string inputText)
    {
      _progressReport?.Invoke(0, "形態素解析を開始しています...");
      var morphemes = await AnalyzeTextAsync(inputText);

      _progressReport?.Invoke(10, "データベースから類似語を検索しています...");
      var rhymes = await FindRhymesAsync(morphemes);

      _progressReport?.Invoke(95, "結果を整形しています...");
      var result = FormatOutput(morphemes, rhymes);

      _progressReport?.Invoke(100, "完了しました。");
      return result;
    }

    /// <summary>
    /// 非同期にテキスト解析を行う
    /// </summary>
    /// <param name="inputText"></param>
    /// <returns></returns>
    private Task<List<Morpheme>> AnalyzeTextAsync(string inputText)
    {
      var morphemes = new List<Morpheme>();

      var param = new MeCabParam { DicDir = Form1.MeCabDicPath };
      using (var mecab = MeCabTagger.Create(param))
      {
        // 最初の要素（全文）をスキップ
        foreach (var node in mecab.ParseToNodes(inputText).Skip(1))
        {
          if (node.Feature == null) continue;
          var features = node.Feature.Split(',');
          var surface = node.Surface;     // 単語
          var partOfSpeech = features[0]; // 品詞
          var reading = features.Length > 7 ? features[7] : ""; // 読み方
          var pronunciation = features.Length > 9 ? features[9] : reading;  // 発音

          // 読み方、または発音から「母音」を取得する
          var vowels = _vowelExtractor.ExtractVowels(reading, pronunciation);

          morphemes.Add(new Morpheme(surface, partOfSpeech, reading, pronunciation, vowels));
        }
      }

      return Task.FromResult(morphemes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="morphemes"></param>
    /// <returns></returns>
    private async Task<Dictionary<Morpheme, List<string>>> FindRhymesAsync(List<Morpheme> morphemes)
    {
      var rhymes = new Dictionary<Morpheme, List<string>>();

      var builder = new SQLiteConnectionStringBuilder
      {
        DataSource = dbPath,
        ReadOnly = true,                          // 読み取り専用モード
        JournalMode = SQLiteJournalModeEnum.Off,  // ジャーナルを無効化
        CacheSize = -50000,                       // キャッシュサイズを大きく（約50MB）
        PageSize = 4096,                          // ページサイズを最大に
        DefaultTimeout = 30,                      // タイムアウトを30秒に設定
        Pooling = true                            // 接続プーリングを有効化
      };
      //var con = new SQLiteConnection(builder.ToString());

      using var connection = new SQLiteConnection(builder.ToString());
      await connection.OpenAsync();

      for (var i = 0; i < morphemes.Count; i++)
      {
        var morpheme = morphemes[i];
        var matchingWords = new List<string>();

        // 母音での検索
        foreach (var vowel in morpheme.Vowels)
        {
          matchingWords.AddRange(await FindMatchingWordsAsync(connection, morpheme.PartOfSpeech, vowel, "Vowels", MaxVowelWords));
          if (matchingWords.Count >= MaxPronunciationWords + MaxVowelWords) break;
        }

        // 発音での検索
        matchingWords.AddRange(await FindMatchingWordsAsync(connection, morpheme.PartOfSpeech, morpheme.Pronunciation, "Pronunciation", MaxPronunciationWords));

        rhymes[morpheme] = matchingWords.Distinct().ToList();

        // 進捗状況を報告
        var percentage = (int)((i + 1) / (double)morphemes.Count * 85) + 10;
        _progressReport?.Invoke(percentage, $"単語 {i + 1}/{morphemes.Count} を処理中...");
      }

      return rhymes;
    }

    /// <summary>
    /// 指定された条件に一致する単語をデータベースから非同期で検索
    /// </summary>
    /// <param name="connection">SQLiteConnectionオブジェクト</param>
    /// <param name="partOfSpeech">品詞</param>
    /// <param name="searchTerm">検索語</param>
    /// <param name="searchColumn">検索対象の列名</param>
    /// <param name="maxWords">最大単語数</param>
    /// <returns>検索結果の単語リスト</returns>
    private async Task<List<string>> FindMatchingWordsAsync(
      SQLiteConnection connection, string partOfSpeech, string searchTerm, string searchColumn, int maxWords)
    {
      // 検索結果を格納するリスト
      var matchingWords = new List<string>();

      // コマンドオブジェクトを作成
      var command = connection.CreateCommand();
      command.CommandText = $"""
         SELECT Surface, {searchColumn}
         FROM Words
         WHERE PartOfSpeech = @PartOfSpeech
         ORDER BY 
             CASE 
                 WHEN {searchColumn} = @SearchTerm THEN 0 
                 ELSE 1 
             END,
             LENGTH({searchColumn}) DESC
      """;
      command.Parameters.AddWithValue("@PartOfSpeech", partOfSpeech); // 品詞
      command.Parameters.AddWithValue("@SearchTerm", searchTerm);     // 検索語

      // コマンドを非同期で実行し、結果を取得する
      using var reader = await command.ExecuteReaderAsync();
      while (await reader.ReadAsync())
      {
        var surface = reader.GetString(0);
        var dbTerm = reader.GetString(1);

        // 検索語とデータベースの単語が一致するかチェックする
        if (!IsMatch(searchTerm, dbTerm)) continue;

        // 一致する単語をリストに追加する
        matchingWords.Add(surface);

        // 最大単語数に達した場合はループを終了する
        if (matchingWords.Count >= maxWords) break;
      }

      // 検索結果のリストを返す
      return matchingWords;
    }

    /// <summary>
    /// 指定された入力とデータベースの単語が一致するかを判定します。
    /// </summary>
    /// <param name="input">入力</param>
    /// <param name="dbWord">データベースの単語</param>
    /// <returns>一致する場合はtrue、それ以外はfalse</returns>
    private bool IsMatch(string input, string dbWord)
    {
      if (input == dbWord) return true; // 完全一致の場合

      var minLength = Math.Min(input.Length, dbWord.Length);
      var threshold = (int)Math.Floor(minLength * MatchThreshold);
      for (var i = 1; i <= minLength; i++)
      {
        var inputSuffix = input.Substring(input.Length - i);
        var dbWordSuffix = dbWord.Substring(dbWord.Length - i);
        if (inputSuffix == dbWordSuffix && i >= threshold)
        {
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// 指定された形態素解析結果と類似語の辞書を整形して出力する
    /// </summary>
    /// <param name="morphemes">形態素解析結果のリスト</param>
    /// <param name="rhymes">類似語の辞書</param>
    /// <returns>整形された出力文字列</returns>
    private static string FormatOutput(List<Morpheme> morphemes, Dictionary<Morpheme, List<string>> rhymes)
    {
      var output = new StringBuilder();

      foreach (var morpheme in morphemes)
      {
        output.AppendLine($"{morpheme.Surface}\t{string.Join(", ", rhymes[morpheme])}");
      }

      return output.ToString();
    }
  }

  /// <summary>
  /// 分析する形態素解析した要素
  /// </summary>
  /// <param name="surface">単語</param>
  /// <param name="partOfSpeech">品詞</param>
  /// <param name="reading">読み方</param>
  /// <param name="pronunciation">発音</param>
  /// <param name="vowels">母音</param>
  public class Morpheme(string surface, string partOfSpeech, string reading, string pronunciation, List<string> vowels)
  {
    public string Surface { get; } = surface;
    public string PartOfSpeech { get; } = partOfSpeech;
    public string Reading { get; } = reading;
    public string Pronunciation { get; } = pronunciation;
    public List<string> Vowels { get; } = vowels;
  }
}
