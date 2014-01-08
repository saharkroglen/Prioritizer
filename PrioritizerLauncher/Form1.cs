using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Configuration;
using PrioritizerService;
using System.IO.Compression;
using PrioritizerService;


namespace PrioritizerLauncher
{
    public partial class Form1 : Form
    {
        private string PRIORITIZER_INSTALL_DIR = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86) + @"\Prioritizer";
        private string PACKAGE_FOLDER_NAME = @"\prioritizerPackage";
        private string PRIORITIZER_APPLICATION_EXECUTABLE_NAME = @"\prioritizer.exe";
        private bool _silent;
        public Form1(bool silent)
        {
            _silent = silent;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private static void CopyDirectory(string sourcePath, string destPath)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            
            foreach (string file in Directory.GetFiles(sourcePath))
            {
                try
                {
                    string dest = Path.Combine(destPath, Path.GetFileName(file));
                    File.Copy(file, dest,true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            foreach (string folder in Directory.GetDirectories(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(folder));
                CopyDirectory(folder, dest);
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            
 
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            PrioritizerServiceClient proxy = new PrioritizerServiceClient();
            //ClientPackage package = proxy.getLatestClient();

            //using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(@"C:\2.zip"))
            //{
            //    outfile.Write(package.zip);
            //    outfile.Close();
            //}


            lblInfo.Text = "Checking for new version...";
            Thread.Sleep(300);
            string packageDirectory = Directory.GetCurrentDirectory() + PACKAGE_FOLDER_NAME;
            if (Directory.Exists(packageDirectory))
            {                
                Application.DoEvents();

                if (!Directory.Exists(PRIORITIZER_INSTALL_DIR))
                {                    
                    Directory.CreateDirectory(PRIORITIZER_INSTALL_DIR);
                    lblInfo.Text = "Copy prioritizer package to local disk...";
                    CopyDirectory(packageDirectory, PRIORITIZER_INSTALL_DIR);
                    //Thread.Sleep(2000);
                }
                else
                {                    
                    
                    var mostUpdatedClientVersion = proxy.getClientVersion();
                    var config = ConfigurationManager.OpenExeConfiguration(PRIORITIZER_INSTALL_DIR + PRIORITIZER_APPLICATION_EXECUTABLE_NAME);
                    var actualClientVersionOnLocalMachine = config.AppSettings.Settings["currentVer"];
                    if (mostUpdatedClientVersion != actualClientVersionOnLocalMachine.Value)
                    {
                        var userChoice = MessageBox.Show("New Prioritizer version exists. You Need to upgrade your Prioritizer version.\nClick 'OK' to terminate existing instances and get latest version.\nClick 'Cancel' to exit", "Upgrade", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (userChoice == System.Windows.Forms.DialogResult.OK)
                        {
                            var prioritizerInstances = System.Diagnostics.Process.GetProcessesByName("prioritizer");
                            foreach (var instance in prioritizerInstances)
                                instance.Kill();
                        }
                        else
                        {
                            Application.Exit();
                            return;
                        }
                        lblInfo.Text = "Copy prioritizer package to local disk...";
                        CopyDirectory(packageDirectory, PRIORITIZER_INSTALL_DIR);
                        //Thread.Sleep(2000);
                    }
                }
            }
            else
            {
                lblInfo.Text = "Can't find package folder to copy from\nPlease contact administrator";
                Application.DoEvents();
                return;
            }


            lblInfo.Text = "Launching Prioritizer from local disk...";
            Application.DoEvents();
            Thread.Sleep(500);
            System.Diagnostics.Process.Start(PRIORITIZER_INSTALL_DIR + PRIORITIZER_APPLICATION_EXECUTABLE_NAME);

            lblInfo.Text = "Exiting...";
            Application.DoEvents();
            Thread.Sleep(500);
            Application.Exit();
        }
    }
}
