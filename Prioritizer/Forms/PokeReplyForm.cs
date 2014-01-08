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
    public partial class PokeReplyForm : DevExpress.XtraEditors.XtraForm
    {
        private frmMain _parentForm;
        private Prioritizer.Shared.Model.Alerts _alert;
        //private Dictionary<enPokeMood, List<String>> moodTexts = new Dictionary<enPokeMood, List<string>>();
        private List<String> predefinedReplies = new List<string>();
        //private List<KeyValuePair<string, enmPokeMood>> pokeModeText = new List<KeyValuePair<string, enmPokeMood>>();
        Poke _originalPoke;

        public PokeReplyForm(frmMain parent, Poke originalPoke)
        {
            _parentForm = parent;
            _originalPoke = originalPoke;
            InitializeComponent();
            
        }

        private void initCombobox()
        {
            initPredefinedReplyList();
            lblTo.Text = string.Format("Reply To: {0}",frmMain.usersDict[_originalPoke.From].userName);
            
        }

        private void initPredefinedReplyList()
        {
            predefinedReplies.Add("On It !");
            predefinedReplies.Add("Ready by end of the day");
            predefinedReplies.Add("Let's talk");

            cboPokeText.Properties.DataSource = predefinedReplies;
            cboPokeText.EditValue = predefinedReplies[0];
            cboPokeText.Properties.ShowHeader = false;
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
            string taskName = string.Empty;
            Guid taskId = Guid.Empty;
            if (_originalPoke.TaskID != Guid.Empty)
            {
                Tasks task = ConnectionManager.Proxy.getTaskByID(_originalPoke.TaskID);
                taskName = task.taskName;
                taskId = task.ID;
            }
            string comment = txtRemarks.Text.Length>0 ? txtRemarks.Text: cboPokeText.EditValue.ToString();
            string alertText = string.Empty;
            if (taskId != Guid.Empty)
            {
                alertText = string.Format("<b>Task: {0}... </b><br>", taskName.Substring(0, Math.Min(taskName.Length, MAX_ALERT_TASK_NAME_LENGTH)));
            }
            alertText += string.Format("{0}",comment);

            ConnectionManager.Proxy.Poke(new Poke() { TaskID = taskId, From = frmMain.loggedInUserID, To = _originalPoke.From, SendEmail = chkEmail.Checked, Comment = alertText, SentOn = DateTime.UtcNow, Type = enPokeType.Reply });
            this.Close();
        }



        private void PokeReplyForm_Load(object sender, EventArgs e)
        {
            initCombobox();
        }

  

        private void txtRemarks_EditValueChanged(object sender, EventArgs e)
        {
            cboPokeText.Enabled = !(txtRemarks.Text.Length > 0);
        }
    }
    
    
}