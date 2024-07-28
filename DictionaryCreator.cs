#nullable enable
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ude;

namespace RhymingTool
{
  public class Word(string surface, string partOfSpeech, string reading, string pronunciation, List<string> vowels)
  {
    public string Surface { get; set; } = surface;
    public string PartOfSpeech { get; set; } = partOfSpeech;
    public string Reading { get; set; } = reading;
    public string Pronunciation { get; set; } = pronunciation;
    public List<string> Vowels { get; set; } = vowels;
  }

  public class DictionaryCreator
  {
    // データベースファイルのパス
    private readonly string _dbPath;
    // SQLite接続に必要な文字列
    private readonly string _connectionString;

    // 母音取得クラスのインスタンス
    private readonly VowelExtractor vow;

    public DictionaryCreator(string dbPath)
    {
      _dbPath = dbPath;
      _connectionString = $"Data Source={_dbPath};Version=3;";
      vow = new VowelExtractor();
    }

    public async Task CreateDatabaseAsync()
    {
      await Task.Run(() =>
      {
        //progress?.Report(0);
        SQLiteConnection.CreateFile(_dbPath);
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        // 頻繁に検索されるであろう「単語」「読み」「発音」にインデックスを張る
        command.CommandText =
          """
          CREATE TABLE Words (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Surface TEXT NOT NULL,
            PartOfSpeech TEXT NOT NULL,
            Reading TEXT NOT NULL,
            Pronunciation TEXT NOT NULL,
            Vowels TEXT NOT NULL
          );
          CREATE INDEX idx_surface ON Words(Surface);
          CREATE INDEX idx_pronunciation ON Words(Pronunciation);
          CREATE INDEX idx_part_of_speech ON Words(PartOfSpeech);
          """;
        command.ExecuteNonQuery();

        //progress?.Report(100);
      });

    }

    /// <summary>
    /// CSV構造を調査する
    ///（CSVファイルによっては内容のカラム位置が異なるため）
    /// </summary>
    /// <param name="headerLine">チェックする一行目</param>
    /// <returns></returns>
    private (int surfaceIndex, int posIndex, int readingIndex, int pronunciationIndex) AnalyzeCsvStructure(string headerLine)
    {
      var fields = headerLine.Split(',');
      var surfaceIndex = 0;
      var posIndex = -1;
      var readingIndex = -1;
      var pronunciationIndex = -1;

      for (var i = 0; i < fields.Length; i++)
      {
        var field = fields[i].Trim();
        // 先頭のフィールドは無条件で「単語」
        if (i == 0 && field == "単語")
        {
          surfaceIndex = i;
        }
        // フィールドの末尾に「詞」が付けば「品詞」
        else if (field.EndsWith("詞") && posIndex == -1)
        {
          posIndex = i;
        }
        else if (ContainsKatakana(field) && readingIndex == -1)
        {
          // カタカナがあれば、連続して「読み方」「発音」
          readingIndex = i;
          if (i + 1 < fields.Length && ContainsKatakana(fields[i + 1]))
          {
            pronunciationIndex = i + 1;
          }
        }
      }

      Debug.WriteLine($"CSV Structure: Surface={surfaceIndex}, POS={posIndex}, Reading={readingIndex}, Pronunciation={pronunciationIndex}");
      return (surfaceIndex, posIndex, readingIndex, pronunciationIndex);
    }

    private bool ContainsKatakana(string text)
    {
      //入力されたテキスト（読み方）「すべて」がカタカナかチェックする
      return text.All(c => c is >= '\u30A0' and <= '\u30FF' or >= '\u31F0' and <= '\u31FF');
    }

    /// <summary>
    /// 各CSVファイルのエンコーディングを取得する
    /// </summary>
    /// <param name="filePath">エンコーディングを判定するCSVファイルパス</param>
    /// <returns></returns>
    private Encoding DetectEncoding(string filePath)
    {
      using var fileStream = File.OpenRead(filePath);
      // NuGetからインストールしたエンコーディング判別ライブラリを使う
      var detector = new CharsetDetector();
      detector.Feed(fileStream);
      detector.DataEnd();

      if (detector.Charset == null) return Encoding.UTF8;

      var encodingName = detector.Charset.ToUpperInvariant();
      Debug.WriteLine($"Detected charset: {encodingName}");

      // エンコーディングの名前をフォームに表示するために呼ぶ
      OnEncodingDetected(detector.Charset);

      // エンコーディング名のマッピング
      try
      {
        switch (encodingName)
        {
          case "EUC-JP":
            return Encoding.GetEncoding("euc-jp");
          case "SHIFT_JIS":
            return Encoding.GetEncoding("shift_jis");
          case "ISO-2022-JP":
            return Encoding.GetEncoding("iso-2022-jp");
          default:  // デフォルトはUTF-8
            return Encoding.GetEncoding(encodingName);
        }
      }
      catch (ArgumentException)
      {
        Debug.WriteLine($"Unsupported encoding: {encodingName}. Falling back to UTF-8.");
        return Encoding.UTF8;
      }
    }

    /// <summary>
    /// フォームへ取得したエンコーディング名を引き渡すための関数
    /// </summary>
    /// <param name="encodingName"></param>
    public class EncodingDetectedEventArgs(string encodingName) : EventArgs
    {
      public string EncodingName { get; } = encodingName;
    }

    public event EventHandler<EncodingDetectedEventArgs>? EncodingDetected;

    protected virtual void OnEncodingDetected(string encodingName)
    {
      EncodingDetected?.Invoke(this, new EncodingDetectedEventArgs(encodingName));
    }

    /// <summary>
    /// 投げ込まれたCSV形式の辞書ファイルリストから読み込みアプリケーションが利用する辞書へ追加する
    /// </summary>
    /// <param name="csvPath">抽出するCSVファイルのパス</param>
    /// <param name="progress">プログレスバーのインスタンス</param>
    /// <param name="currentLines">現在作業中のライン数（進捗表示で使う）</param>
    /// <param name="totalLines">処理する合計行数（進捗表示で使う）</param>
    /// <returns></returns>
    public async Task<int> ImportFromCsvAsync(string csvPath, IProgress<int>? progress, int currentLines, int totalLines)
    {
      const int batchSize = 1000;
      var words = new List<Word>();

      try
      {
        // ファイルのエンコーディングを検出
        var detectedEncoding = DetectEncoding(csvPath);
        Debug.WriteLine($"Detected encoding: {detectedEncoding.EncodingName}");

        using var reader = new StreamReader(csvPath, detectedEncoding);
        // ヘッダー行を読み込んで解析
        var headerLine = await reader.ReadLineAsync() ?? throw new InvalidOperationException("CSV file is empty");

        // CSVの書式を分析し、一行目どのカラムにそれぞれ抽出する内容があるか調査
        var (surfaceIndex, posIndex, readingIndex, pronunciationIndex) = AnalyzeCsvStructure(headerLine);

        while (await reader.ReadLineAsync() is { } line)
        {
          var fields = line.Split(',');
          if (fields.Length > Math.Max(surfaceIndex, Math.Max(posIndex, Math.Max(readingIndex, pronunciationIndex))))
          {
            var surface = fields[surfaceIndex];                           // 単語
            var partOfSpeech = posIndex >= 0 ? fields[posIndex] : "";     // 品詞
            var reading = readingIndex >= 0 ? fields[readingIndex] : "";  // 読み方
            var pronunciation = pronunciationIndex >= 0 ? fields[pronunciationIndex] : reading; // 発音

            Debug.WriteLine($"Read: Surface={surface}, PartOfSpeech={partOfSpeech}, Reading={reading}, Pronunciation={pronunciation}");

            var vowels = vow.ExtractVowels(reading, pronunciation); // 母音
            words.AddRange(vowels.Select(vowel => new Word(surface, partOfSpeech, reading, pronunciation, [vowel])));

            currentLines++;

            if (words.Count < batchSize) continue;

            // 進捗表示の更新
            progress?.Report(currentLines);

            // 母音を含むすべての単語をデータベースにInsertする
            await InsertWordsAsync(words);

            Debug.WriteLine($"Processed {currentLines} lines so far.");

            words.Clear();
          }
          else
          {
            Debug.WriteLine($"Skipping invalid line: {line}");
          }
        }

        // 残りのワードを処理
        if (words.Count > 0)
        {
          await InsertWordsAsync(words);
          progress?.Report(currentLines);
          Debug.WriteLine($"Processed final batch. Total lines: {currentLines}");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error during import: {ex.Message}");
      }

      return currentLines;
    }

    /// <summary>
    /// 一通り揃えた母音を含む単語配列をデータベースにインサートする
    /// </summary>
    /// <param name="words">単語配列（単語、品詞、読み方、発音、母音）</param>
    /// <returns></returns>
    private async Task InsertWordsAsync(List<Word> words)
    {
      if (words.Count == 0) return;

      using var connection = new SQLiteConnection(_connectionString);
      await connection.OpenAsync();

      // トランザクション開始
      using var transaction = connection.BeginTransaction();
      try
      {
        // 重複登録を避けるため、同じ内容がないかチェックするSQL
        using var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = """
          SELECT COUNT(*) 
            FROM Words 
            WHERE Surface = @Surface 
              AND PartOfSpeech = @PartOfSpeech 
              AND Reading = @Reading 
              AND Pronunciation = @Pronunciation
        """;

        // 単語配列をインサートするSQL
        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = """
          INSERT INTO Words (Surface, PartOfSpeech, Reading, Pronunciation, Vowels)
          VALUES (@Surface, @PartOfSpeech, @Reading, @Pronunciation, @Vowels)
        """;

        foreach (var word in words)
        {
          // 既存のエントリをチェック
          checkCommand.Parameters.Clear();
          checkCommand.Parameters.AddWithValue("@Surface", word.Surface);
          checkCommand.Parameters.AddWithValue("@PartOfSpeech", word.PartOfSpeech);
          checkCommand.Parameters.AddWithValue("@Reading", word.Reading);
          checkCommand.Parameters.AddWithValue("@Pronunciation", word.Pronunciation);

          var count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

          if (count == 0)
          {
            // 重複がない場合のみ挿入
            insertCommand.Parameters.Clear();
            insertCommand.Parameters.AddWithValue("@Surface", word.Surface);
            insertCommand.Parameters.AddWithValue("@PartOfSpeech", word.PartOfSpeech);
            insertCommand.Parameters.AddWithValue("@Reading", word.Reading);
            insertCommand.Parameters.AddWithValue("@Pronunciation", word.Pronunciation);
            insertCommand.Parameters.AddWithValue("@Vowels", string.Join(",", word.Vowels));
            await insertCommand.ExecuteNonQueryAsync();
          }
          else
          {
            // 重複がある場合はログ出力
            Debug.WriteLine($"Duplicate entry found for: {word.Surface}");
          }
        }

        // コミット
        transaction.Commit();
        Debug.WriteLine("Inserted words successfully.");
      }
      catch (Exception ex)
      {
        // ロールバックして
        transaction.Commit();
        Debug.WriteLine($"Error inserting words: {ex.Message}");
        throw; // 上位の呼び出し元に例外を再スローする
      }
    }


    public class VowelExtractor
    {
      // 静的コンストラクタを使用して母音マップ（Dictionary<char, char>）を一度だけ初期化
      private readonly Dictionary<char, char> KatakanaToVowelMap = new()
      {
        { 'ア', 'ア' }, { 'イ', 'イ' }, { 'ウ', 'ウ' }, { 'エ', 'エ' }, { 'オ', 'オ' },
        { 'カ', 'ア' }, { 'キ', 'イ' }, { 'ク', 'ウ' }, { 'ケ', 'エ' }, { 'コ', 'オ' },
        { 'サ', 'ア' }, { 'シ', 'イ' }, { 'ス', 'ウ' }, { 'セ', 'エ' }, { 'ソ', 'オ' },
        { 'タ', 'ア' }, { 'チ', 'イ' }, { 'ツ', 'ウ' }, { 'テ', 'エ' }, { 'ト', 'オ' },
        { 'ナ', 'ア' }, { 'ニ', 'イ' }, { 'ヌ', 'ウ' }, { 'ネ', 'エ' }, { 'ノ', 'オ' },
        { 'ハ', 'ア' }, { 'ヒ', 'イ' }, { 'フ', 'ウ' }, { 'ヘ', 'エ' }, { 'ホ', 'オ' },
        { 'マ', 'ア' }, { 'ミ', 'イ' }, { 'ム', 'ウ' }, { 'メ', 'エ' }, { 'モ', 'オ' },
        { 'ヤ', 'ア' }, { 'ユ', 'ウ' }, { 'ヨ', 'オ' },
        { 'ラ', 'ア' }, { 'リ', 'イ' }, { 'ル', 'ウ' }, { 'レ', 'エ' }, { 'ロ', 'オ' },
        { 'ワ', 'ア' }, { 'ヰ', 'イ' }, { 'ヱ', 'エ' }, { 'ヲ', 'オ' },
        { 'ッ', 'ッ' }, // 促音（例外）
        { 'ン', 'ン' }, // 撥音（例外）
        { 'ガ', 'ア' }, { 'ギ', 'イ' }, { 'グ', 'ウ' }, { 'ゲ', 'エ' }, { 'ゴ', 'オ' },
        { 'ザ', 'ア' }, { 'ジ', 'イ' }, { 'ズ', 'ウ' }, { 'ゼ', 'エ' }, { 'ゾ', 'オ' },
        { 'ダ', 'ア' }, { 'ヂ', 'イ' }, { 'ヅ', 'ウ' }, { 'デ', 'エ' }, { 'ド', 'オ' },
        { 'バ', 'ア' }, { 'ビ', 'イ' }, { 'ブ', 'ウ' }, { 'ベ', 'エ' }, { 'ボ', 'オ' },
        { 'パ', 'ア' }, { 'ピ', 'イ' }, { 'プ', 'ウ' }, { 'ペ', 'エ' }, { 'ポ', 'オ' },
        { 'ャ', 'ア' }, { 'ュ', 'ウ' }, { 'ョ', 'オ' },
        { 'ァ', 'ア' }, { 'ィ', 'イ' }, { 'ゥ', 'ウ' }, { 'ェ', 'エ' }, { 'ォ', 'オ' },
        { 'ヴ', 'ウ' },
        { 'ー', 'ー' }, // 音引き（例外）
      };

      /// <summary>
      /// 母音を抽出する処理
      /// </summary>
      /// <param name="reading">読み（カタカナ）</param>
      /// <param name="pronunciation">発音（カタカナ）</param>
      /// <returns></returns>
      public List<string> ExtractVowels(string reading, string pronunciation)
      {
        var vowels = new List<string>();
        if (reading == pronunciation)
        {
          vowels.Add(ExtractVowelsFromString(pronunciation));
        }
        else
        { // 読みと発音が異なる場合は二つ登録する
          vowels.Add(ExtractVowelsFromString(reading));
          vowels.Add(ExtractVowelsFromString(pronunciation));
        }

        return vowels;
      }

      /// <summary>
      /// 前述のカタカナ・テーブルから母音を取り出す
      /// </summary>
      /// <param name="input">入力文字</param>
      /// <returns></returns>
      private string ExtractVowelsFromString(string input)
      {
        var vowels = new StringBuilder();
        foreach (var c in input)
        {
          vowels.Append(KatakanaToVowelMap.TryGetValue(c, out var value) ? value : c);
        }
        return vowels.ToString();
      }
    }


  }

}

