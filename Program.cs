using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace RhymingTool
{
  internal static class Program
  {
    private const string mutexName = "RhymingTool";

    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    private static void Main()
    {

      // Mutexを初期化
      var mutex = new Mutex(true, mutexName, out var isCreatedNew);

      // 既に同じ名前のMutexが存在する場合はアプリケーションを終了
      if (isCreatedNew == false)
      {
        return;
      }

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
    }
  }

}
