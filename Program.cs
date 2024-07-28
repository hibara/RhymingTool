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
    /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
    /// </summary>
    [STAThread]
    private static void Main()
    {

      // Mutex��������
      var mutex = new Mutex(true, mutexName, out var isCreatedNew);

      // ���ɓ������O��Mutex�����݂���ꍇ�̓A�v���P�[�V�������I��
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
