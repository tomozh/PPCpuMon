using System;
using System.Threading;
using System.Windows.Forms;

namespace PPCpuMon
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 多重起動防止処理
            string mutexName = Application.CompanyName + '_' + Application.ProductName;

            var mutex = new Mutex(true, mutexName, out bool createdNew);

            if (createdNew)
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                finally
                {
                    mutex.ReleaseMutex();
                    mutex.Close();
                }
            }
        }
    }
}
