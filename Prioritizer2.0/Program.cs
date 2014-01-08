using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Prioritizer2._0
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NewPrioritizer mainForm = null;
            try
            {
               
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
               // Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentCulture);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                bool newVersionExist = NewPrioritizer.checkNewVersion();
                if (newVersionExist)
                    return;
                    
                mainForm = new NewPrioritizer();                
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                /*if (ex.Message.ToLower().Contains(mainForm.FAILED_TO_OPEN_DB_ERROR_MESSAGE))
                {
                    NewPrioritizer.locateMdbFile(true, NewPrioritizer.mdbPathRegistryKey, NewPrioritizer.prioritizerDBDescription);
                    MessageBox.Show("Attach to new DB succeeded. Please restart prioritizer");
                    System.Environment.Exit(0);
                }*/
                MessageBox.Show(ex.Data + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException );
            }
        }
    }
}
