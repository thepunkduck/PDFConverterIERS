using ApplicationNiceties;
using System;
using System.Windows.Forms;

namespace Docotron
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationSetup.SetStartMenuItemInPublisherFolder();
            ApplicationSetup.CreateApplicationResourcesPath();
            ApplicationSetup.CreateApplicationRegistry();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form1();
            TimeLicence.URL = "www.quantiseal.com";
            TimeLicence.Protect(form);
            Application.Run(form);
        }
    }
}
