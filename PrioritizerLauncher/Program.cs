using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PrioritizerLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool silentLaunch = false;
            
            if (args.Count() > 0)
            {
                if (args[0].ToLower() == "silent")
                    silentLaunch = true;
            }

            Application.Run(new Form1(silentLaunch));
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string exceptionMessage = (e.ExceptionObject as Exception).Message;
            MessageBox.Show("General Error\n" + exceptionMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
