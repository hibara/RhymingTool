using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhymingTool
{
  public static class AssemblyLoader
  {
    public static bool IsMeCabLoaded()
    {
      return AppDomain.CurrentDomain.GetAssemblies().Any(a => a.GetName().Name == "MeCab.DotNet");
    }

    public static void LoadMeCab()
    {
      if (!IsMeCabLoaded())
      {
        try
        {
          System.Reflection.Assembly.Load("MeCab.DotNet");
        }
        catch (Exception ex)
        {
          Console.WriteLine($@"MeCab.DotNetの読み込みに失敗しました: {ex.Message}");
        }
      }
    }

    public static async Task WaitForMeCabLoadAsync(int timeoutSeconds = 30)
    {
      var startTime = DateTime.Now;
      while (!AssemblyLoader.IsMeCabLoaded())
      {
        if ((DateTime.Now - startTime).TotalSeconds > timeoutSeconds)
        {
          throw new TimeoutException("MeCab.DotNetの読み込みがタイムアウトしました。");
        }
        await Task.Delay(100); // 100ミリ秒待機
      }
    }


  }


}
