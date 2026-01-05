using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FakeUser
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WindowsFormsSettings.SetDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}