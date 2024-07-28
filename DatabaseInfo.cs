using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhymingTool
{
  /// <summary>
  /// 現在登録中の辞書の詳細情報を取得するクラス
  /// </summary>
  public class DatabaseInfo(string dbPath)
  {
    private readonly string _connectionString = $"Data Source={dbPath};Version=3;";

    public int TotalWords { get; private set; }
    public long DatabaseSize { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public Dictionary<string, int> PartOfSpeechCounts { get; private set; } = new();
    public string LongestWord { get; private set; }
    public string ShortestWord { get; private set; }
    public double AverageWordLength { get; private set; }
    public string MostCommonVowelPattern { get; private set; }
    public int UniqueVowelPatterns { get; private set; }
    public int TotalUniqueWords { get; private set; }
    public Dictionary<int, int> WordLengthDistribution { get; private set; } = new();

    public async Task LoadInfoAsync()
    {
      using var connection = new SQLiteConnection(_connectionString);
      await connection.OpenAsync();

      await LoadTotalWordsAsync(connection);
      LoadDatabaseSize();
      LoadLastUpdated();
      await LoadPartOfSpeechCountsAsync(connection);
      await LoadWordLengthInfoAsync(connection);
      await LoadVowelInfoAsync(connection);
      await LoadUniqueWordsCountAsync(connection);
      await LoadWordLengthDistributionAsync(connection);
    }

    private async Task LoadTotalWordsAsync(SQLiteConnection connection)
    {
      using var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Words", connection);
      TotalWords = Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    private void LoadDatabaseSize()
    {
      var dbFile = new FileInfo(_connectionString.Replace("Data Source=", "").Split(';')[0]);
      DatabaseSize = dbFile.Length;
    }

    private void LoadLastUpdated()
    {
      var dbFile = new FileInfo(_connectionString.Replace("Data Source=", "").Split(';')[0]);
      LastUpdated = dbFile.LastWriteTime;
    }

    private async Task LoadPartOfSpeechCountsAsync(SQLiteConnection connection)
    {
      using var cmd = new SQLiteCommand("SELECT PartOfSpeech, COUNT(*) FROM Words GROUP BY PartOfSpeech", connection);
      using var reader = await cmd.ExecuteReaderAsync();
      while (await reader.ReadAsync())
      {
        PartOfSpeechCounts[reader.GetString(0)] = reader.GetInt32(1);
      }
    }

    private async Task LoadWordLengthInfoAsync(SQLiteConnection connection)
    {
      using (var cmd = new SQLiteCommand("SELECT Surface FROM Words ORDER BY LENGTH(Surface) DESC LIMIT 1", connection))
      {
        LongestWord = (string)await cmd.ExecuteScalarAsync();
      }

      using (var cmd = new SQLiteCommand("SELECT Surface FROM Words ORDER BY LENGTH(Surface) ASC LIMIT 1", connection))
      {
        ShortestWord = (string)await cmd.ExecuteScalarAsync();
      }

      using (var cmd = new SQLiteCommand("SELECT AVG(LENGTH(Surface)) FROM Words", connection))
      {
        AverageWordLength = Convert.ToDouble(await cmd.ExecuteScalarAsync());
      }
    }

    private async Task LoadVowelInfoAsync(SQLiteConnection connection)
    {
      using (var cmd = new SQLiteCommand("SELECT Vowels, COUNT(*) as Count FROM Words GROUP BY Vowels ORDER BY Count DESC LIMIT 1", connection))
      using (var reader = await cmd.ExecuteReaderAsync())
      {
        if (await reader.ReadAsync())
        {
          MostCommonVowelPattern = reader.GetString(0);
        }
      }

      using (var cmd = new SQLiteCommand("SELECT COUNT(DISTINCT Vowels) FROM Words", connection))
      {
        UniqueVowelPatterns = Convert.ToInt32(await cmd.ExecuteScalarAsync());
      }
    }

    private async Task LoadUniqueWordsCountAsync(SQLiteConnection connection)
    {
      using var cmd = new SQLiteCommand("SELECT COUNT(DISTINCT Surface) FROM Words", connection);
      TotalUniqueWords = Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    private async Task LoadWordLengthDistributionAsync(SQLiteConnection connection)
    {
      using var cmd = new SQLiteCommand("SELECT LENGTH(Surface) as Length, COUNT(*) as Count FROM Words GROUP BY Length ORDER BY Length", connection);
      var reader = await cmd.ExecuteReaderAsync();
      while (await reader.ReadAsync())
      {
        WordLengthDistribution[reader.GetInt32(0)] = reader.GetInt32(1);
      }
    }

    public Dictionary<string, string> GetSummary()
    {
      var dic = new Dictionary<string, string>
      {
        { "総単語数: ", TotalWords.ToString() },
        { "ユニーク単語数: ", TotalUniqueWords.ToString() },
        { "データベースサイズ: ", (DatabaseSize / 1024 / 1024).ToString() + " MB" },
        { "最終更新日時: ", LastUpdated.ToString("yyyy/MM/dd HH:mm") },
        { "最も一般的な母音パターン: ", MostCommonVowelPattern },
        { "ユニークな母音パターン数: ", UniqueVowelPatterns.ToString() },
      };

      foreach (var pos in PartOfSpeechCounts.OrderByDescending(x => x.Value))
      {
        dic.Add($"{pos.Key} 単語数: ", pos.Value.ToString());
      }

      return dic;

    }
  }
}
