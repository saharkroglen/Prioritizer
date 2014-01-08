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
using Prioritizer.Shared.Model;
using Prioritizer.Proxy;
using Prioritizer.Shared;

namespace Prioritizer.Forms
{
    public partial class PokeMessageForm : DevExpress.XtraEditors.XtraForm
    {
        private frmMain _parentForm;
        private Prioritizer.Shared.Model.Alerts _alert;
        //private Dictionary<enPokeMood, List<String>> moodTexts = new Dictionary<enPokeMood, List<string>>();
        private List<String> predefinedReplies = new List<string>();
        //private List<KeyValuePair<string, enmPokeMood>> pokeModeText = new List<KeyValuePair<string, enmPokeMood>>();
        Poke _originalPoke = null;

        public PokeMessageForm(frmMain parent, Poke originalPoke)
        {
            _parentForm = parent;
            _originalPoke = originalPoke;
            InitializeComponent();
            
        }

        private void initCombobox()
        {
            

            cboTo.Properties.ValueMember = "ID";
            cboTo.Properties.DisplayMember = "userName";
            cboTo.Properties.Columns.Add(new LookUpColumnInfo("userName", 80));
            cboTo.Properties.DataSource = frmMain.usersList;
            cboTo.EditValue = null;

            if (_originalPoke != null)
                cboTo.EditValue = string.Format("Poke: {0}", frmMain.usersDict[_originalPoke.From].ID);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static int MAX_ALERT_TASK_NAME_LENGTH = 20;
        private void btnPoke_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                string comment = txtRemarks.Text;
                ConnectionManager.Proxy.Poke(new Poke() { From = frmMain.loggedInUserID, To = (Guid)cboTo.EditValue, SendEmail = chkEmail.Checked, Comment = comment, SentOn = DateTime.UtcNow, Type = enPokeType.PlainMessage });
                this.Close();
            }
        }



        private void PokeMessageForm_Load(object sender, EventArgs e)
        {
            initCombobox();

            cboTo.Select();
        }

        public bool Validate()
        {
            dxErrorProvider1.ClearErrors();
            bool isValid = true;
            string ErrorCaption = string.Empty;
            if (cboTo.EditValue == null)
            {
                ErrorCaption = "Recipient can't be empty";
                dxErrorProvider1.SetError(cboTo, ErrorCaption);
                isValid = false;
            }
            
            return isValid;
        }

        private void txtRemarks_EditValueChanged(object sender, EventArgs e)
        {
        }
    }
    
    
}