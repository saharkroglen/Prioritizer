using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prioritizer.Utils;
using System.Windows.Forms;
using System.ComponentModel;

namespace Prioritizer.Class
{
    public class Splash:IDisposable
    {
        private BackgroundWorker bgWorker;
        private DevExpress.XtraSplashScreen.SplashScreenManager SplashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager();

        public Splash(Form frm)
        {
            OpenSplashScreen(typeof(global::Prioritizer.Forms.ProcessingWaitForm), frm);
        }

        private void OpenSplashScreen(Type screenType, Form frm)
        {
            bgWorker = new BackgroundWorker();
            if (bgWorker.IsBusy != true)
            {
                SplashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(frm, screenType, true, true);
                if (!SplashScreenManager.IsSplashFormVisible)
                    SplashScreenManager.ShowWaitForm();
            }
        }
       

        public void Dispose()
        {
            SplashScreenManager.CloseWaitForm();
        }
        
    }
}
