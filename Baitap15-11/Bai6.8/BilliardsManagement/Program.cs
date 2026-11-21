using System;
using System.Windows.Forms;
using BilliardsManagement.GUI;

namespace BilliardsManagement
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
