using System;
using System.Windows.Forms;

namespace test2._2_chieu_23_10
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmEmployeeManager());
        }
    }
}
