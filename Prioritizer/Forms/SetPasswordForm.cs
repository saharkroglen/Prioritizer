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
using Prioritizer.Proxy;

namespace Prioritizer.Forms
{
    public partial class SetPasswordForm : DevExpress.XtraEditors.XtraForm
    {
        private frmMain _parentForm;
        public SetPasswordForm(frmMain parentForm)
        {
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
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (frmMain.loggedInUser.password == null || (Prioritizer.Shared.Utils.EncodePassword(txtOldPassword.Text) == frmMain.loggedInUser.password))
            {
                if (txtNewPass.Text.Trim().Length > 0)
                {
                    if (txtNewPass.Text == txtOldPassword.Text)
                    {
                        MessageBox.Show(string.Format("New password must be different than current one{0}Please try again", Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (txtNewPass.Text != txtConfirmPass.Text)
                    {
                        MessageBox.Show(string.Format("New password and Confirmation password must be identical{0}Please try again",Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        frmMain.loggedInUser.password = Prioritizer.Shared.Utils.EncodePassword(txtNewPass.Text);
                        frmMain.loggedInUser.TemporaryPassword = false;
                        frmMain.loggedInUser.Activated = true;
                        ConnectionManager.Proxy.applyChangesUsers(frmMain.loggedInUser, frmMain.loggedInUserID);
                    }
                }
                else
                    MessageBox.Show("Password is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(string.Format("Current Password is incorrect.{0}Please try again.",Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
            this.DialogResult = DialogResult.OK;

        }
     
    }
}