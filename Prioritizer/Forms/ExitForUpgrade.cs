using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shared;
using PrioritizerService;
using Prioritizer.Utils;
using System.Threading.Tasks;
using System.Threading;
using Prioritizer.Shared;
using Prioritizer.Proxy;
using Prioritizer.Class;

namespace Prioritizer.Forms
{
    public partial class ExitForUpgrade : Form
    {
        private int _countdownInSec;
        //private bool _showCountdown;
        public ExitForUpgrade(int seconds,bool showCountdown)
        {            
            //_showCountdown = showCountdown;
            _countdownInSec = seconds;
            InitializeComponent();

            //if (_showCountdown)
            //{
                btnExitAndLaunchSetup.Text = "Exit and Launch Setup";
            //}
            //else
            //{
            //    btnExitAndLaunchSetup.Text = "Launch Setup";
            //    //lblCountDown.Text = "";
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private bool _downloadingSetupInProgress = false;
        public delegate void FinishedDownloadHandler();
        public event FinishedDownloadHandler OnFinishedDownloading;

        private AutoResetEvent _downloadCompleteEvent = new AutoResetEvent(false);
        private void downloadLatestSetupFile()
        {
            //if (!_downloadingSetupInProgress)
            //{
                //_downloadingSetupInProgress = true;
                Logger.Instance.Info("downloading new installation from server");
                ClientPackage installationPack = ConnectionManager.Proxy.getLatestClient();
                Logger.Instance.Info("Download Complete");
                frmMain.ClientSetupDownloadLocation = string.Format(@"{0}\{1}", ClientUtils.TEMP_DIR, installationPack.binName);
                Logger.Instance.Info(string.Format("Saving msi to {0}", frmMain.ClientSetupDownloadLocation));
                Shared.Utils.SaveBinaryToFile(frmMain.ClientSetupDownloadLocation, installationPack.bin);
                Logger.Instance.Info("Saving msi completed successfully");
                //_downloadingSetupInProgress = false;
                _downloadCompleteEvent.Set();
                if (OnFinishedDownloading != null)
                    OnFinishedDownloading();
            //}
        }

        

        //private void timerCountDown_Tick(object sender, EventArgs e)
        //{
        //    if (_showCountdown)
        //    {
        //        lblCountDown.Text = string.Format("Priori will Shutdown in {0} seconds", _countdownInSec--);
        //        if (_countdownInSec == 0)
        //        {
        //            ((frmMain)this.Owner).exitForUpgrade();
        //        }
        //    }            
        //}

        private void btnExitAndLaunchSetup_Click(object sender, EventArgs e)
        {
            ClientUtils.StartClientSetup(frmMain.ClientSetupDownloadLocation);
            ClientUtils.exitForUpgrade();
        }

        private void ExitForUpgrade_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!_showCountdown)
            //{
                Application.Exit();
            //}
        }

        Splash waitingNotification;
        private void btnDownloadSetup_Click(object sender, EventArgs e)
        {
            //ClientUtils.OpenSplashScreen(typeof(global::Prioritizer.Forms.ProcessingWaitForm), this);      
            waitingNotification = new Splash(this);

            Task.Factory.StartNew(() => downloadLatestSetupFile());
            btnDownloadSetup.Text = "Downloading...";
            btnDownloadSetup.Enabled = false;
        }

        private void ExitForUpgrade_Load(object sender, EventArgs e)
        {
            this.OnFinishedDownloading += new FinishedDownloadHandler(ExitForUpgrade_OnFinishedDownloading);
        }

        void ExitForUpgrade_OnFinishedDownloading()
        {
            BeginInvoke(new MethodInvoker(delegate { btnDownloadSetup.Text = "Download Completed"; }));
            BeginInvoke(new MethodInvoker(delegate { btnExitAndLaunchSetup.Enabled = true; }));
            //ClientUtils.SplashScreenManager.CloseWaitForm();
            waitingNotification.Dispose();
            //BeginInvoke(new MethodInvoker(delegate { btnDownloadSetup.Enabled = false; }));

        }
    }
}
