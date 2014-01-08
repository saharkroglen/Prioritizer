using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PrioritizerService.Model;
using DevExpress.XtraEditors.Controls;
using Prioritizer.Utils;
using Newtonsoft.Json;
using System.IO;
using Prioritizer.Shared;
using Prioritizer.Shared.Model;
using Prioritizer.Proxy;

namespace Prioritizer.Forms
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        
        private frmMain _parentForm;
        public Users AuthenticatedUser;
        public LoginForm(frmMain parentForm)
        {
            bool newVersionExist = ClientUtils.CheckNewVersion();
            if (newVersionExist)
            {
                ClientUtils.Upgrade(this);
                Environment.Exit(0);
                return;
            }

            InitializeComponent();
            _parentForm = parentForm;
                        
            this.DialogResult = DialogResult.None;

            loadLookups();
            bindCombos();
        }

        private void loadLookups()
        {
        }
        private void bindCombos()
        {
            
        }
       
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) Environment.Exit(0);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Users user = null;
            if (Validate())
            {
                user = ConnectionManager.Proxy.Authenticate(txtUserName.Text, Prioritizer.Shared.Utils.EncodePassword(txtPassword.Text), txtNetworkName.Text);

                frmMain.UserInfo.Network = txtNetworkName.Text;
                frmMain.UserInfo.Username = txtUserName.Text;
                string jsonFormat = JsonConvert.SerializeObject(frmMain.UserInfo, Formatting.Indented);
                Prioritizer.Shared.Utils.SaveFileContent(frmMain.USER_INFO_DIRECTORY, frmMain.USER_INFO_FILE_NAME, jsonFormat);
            }

            if (user != null)
            {
                AuthenticatedUser = user;
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                 MessageBox.Show("Login failed, please try again.", "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        public bool Validate()
        {
            dxErrorProvider1.ClearErrors();
            bool isValid = true;
            string ErrorCaption = string.Empty;
            if (txtNetworkName.Text.Trim().Length == 0 )
            {
                ErrorCaption = "Please Enter Company";
                dxErrorProvider1.SetError(txtNetworkName, ErrorCaption);
                isValid = false;
            }
            if (txtPassword.Text.Trim().Length == 0 )
            {
                ErrorCaption = "Please Enter Password";
                dxErrorProvider1.SetError(txtPassword, ErrorCaption);
                isValid = false;
            }
            if (txtUserName.Text.Trim().Length == 0)
            {
                ErrorCaption = "Please Enter Username";
                dxErrorProvider1.SetError(txtUserName, ErrorCaption);
                isValid = false;
            }


            return isValid;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (frmMain.UserInfo != null && frmMain.UserInfo.Username != null && frmMain.UserInfo.Network != null)
            {              
                txtPassword.TabIndex = 1;
                txtNetworkName.TabIndex = 2;
                btnLogin.TabIndex = 3;
                txtUserName.TabIndex = 4;
                txtUserName.Text = frmMain.UserInfo.Username;
                txtNetworkName.Text = frmMain.UserInfo.Network;
            }
            else
            {
                txtUserName.TabIndex = 1;
                txtPassword.TabIndex = 2;
                txtNetworkName.TabIndex = 3;
                btnLogin.TabIndex = 4;                
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult != System.Windows.Forms.DialogResult.OK)
                Environment.Exit(0);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //DurableServiceClient durableClient = new DurableServiceClient();
            //durableClient.Open();
            //durableClient.DoWork();
            //durableClient.EndPersistence();

        }
    }
}