using System;
using MeCab;
using System.Text;

namespace RhymingTool
{
  /// <summary>
  /// 日本語形態素解析クラス（MeCab）
  /// </summary>
  public class MorphologicalAnalysis
  {
    private readonly MeCabTagger tagger;

    public MorphologicalAnalysis()
    {
      try
      {
        var param = new MeCabParam { DicDir = Form1.MeCabDicPath };
        tagger = MeCabTagger.Create(param);
      }
      catch (Exception ex)
      {
        throw new Exception("MeCabの初期化に失敗しました。", ex);
      }
    }

    public string Analyze(string text)
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        return "テキストが入力されていません。";
      }

      try
      {
        var nodes = tagger.ParseToNodes(text);
        var result = new StringBuilder();

        foreach (var node in nodes)
        {
          switch (node.Feature)
          {
            case "BOS/EOS":
            case null:
              continue;
          }

          var features = node.Feature.Split(',');
          result.AppendLine($"表層形: {node.Surface}");
          result.AppendLine($"品詞: {GetFeature(features, 0)}");
          result.AppendLine($"品詞細分類1: {GetFeature(features, 1)}");
          result.AppendLine($"品詞細分類2: {GetFeature(features, 2)}");
          result.AppendLine($"品詞細分類3: {GetFeature(features, 3)}");
          result.AppendLine($"活用型: {GetFeature(features, 4)}");
          result.AppendLine($"活用形: {GetFeature(features, 5)}");
          result.AppendLine($"原形: {GetFeature(features, 6)}");
          result.AppendLine($"読み: {GetFeature(features, 7)}");
          result.AppendLine($"発音: {GetFeature(features, 8)}");
          result.AppendLine();
        }

        return result.ToString();
      }
      catch (Exception ex)
      {
        return $"形態素解析中にエラーが発生しました: {ex.Message}";
      }
    }

    private static string GetFeature(string[] features, int index)
    {
      return features.Length > index ? features[index] : "N/A";
    }

  }
}
