using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using Prioritizer.Forms;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using Shared;
using Prioritizer.Utils;
using Prioritizer.Proxy;
using Prioritizer.Shared;

namespace Prioritizer
{
    static class Program
    {
        static frmMain _mainForm = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                
                log4net.Config.DOMConfigurator.Configure(); //Load from App.Config file
                Logger.Instance.Info("App Start");
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                UserLookAndFeel.Default.StyleChanged += new EventHandler(Default_StyleChanged);
                Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.UserSkins.BonusSkins.Register();

                frmMain.LoadUserInfo();
                SetSkin();

                _mainForm = new frmMain();
                Application.Run(_mainForm);
            }
            catch (Exception ex)
            {
                handleDisconnectionExceptionOnStartup(ex);
            }
        }

        public static void handleDisconnectionExceptionOnStartup(Exception ex)
        {
            if (!ConnectionManager.IsAlive)
            {
                MessageBox.Show("Server is not responding\nPlease check your connectivity to the network and try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Instance.Warn("Main form initializaiton failure - Server is not responding ");
                //Application.Exit();
                Environment.Exit(0);
                
            }
            Logger.Instance.Error("Main form initialization failed", ex);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            handleUnhandledException(e.Exception);
        }

        private static void SetSkin()
        {
            if (frmMain.UserInfo != null && frmMain.UserInfo.SkinName != null)
            {
                UserLookAndFeel.Default.SetSkinStyle(frmMain.UserInfo.SkinName);
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("iMaginary");//DevExpress Style");
            }
        }

        static void Default_StyleChanged(object sender, EventArgs e)
        {
            if (frmMain.UserInfo == null)
            {
                frmMain.UserInfo = new UserInfo();
            }

            frmMain.UserInfo.SkinName = UserLookAndFeel.Default.SkinName;

            
            string jsonFormat = JsonConvert.SerializeObject(frmMain.UserInfo, Formatting.Indented);
            Prioritizer.Shared.Utils.SaveFileContent(frmMain.USER_INFO_DIRECTORY, frmMain.USER_INFO_FILE_NAME, jsonFormat);
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            handleUnhandledException(e.ExceptionObject as Exception);
        }

        private static void handleUnhandledException(Exception e)
        {
            string exceptionMessage = e.Message;
            if (e is PrioritizerDisconnectException || ConnectionManager.IsConnectionRelatedException(e))
            {
                _mainForm.setStatusBarText(string.Format("Action Aborted - Server is not available"));
            }
            else
            {
                MessageBox.Show("General Error\n" + exceptionMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Logger.Instance.Fatal(exceptionMessage, e);
        }
    }
}