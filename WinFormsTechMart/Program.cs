using System;
using System.Windows.Forms;

namespace WinFormsTechMart
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize(); // .NET 6+ WinForms helper
            Application.Run(new MainForm());
        }
    }
}
