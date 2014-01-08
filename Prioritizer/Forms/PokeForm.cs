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
    public partial class PokeForm : DevExpress.XtraEditors.XtraForm
    {
        private frmMain _parentForm;
        private Prioritizer.Shared.Model.Alerts _alert;
        private Dictionary<enPokeMood, List<String>> moodTexts = new Dictionary<enPokeMood, List<string>>();
        //private List<KeyValuePair<string, enmPokeMood>> pokeModeText = new List<KeyValuePair<string, enmPokeMood>>();
        private Tasks _task;

        public PokeForm(frmMain parent, Tasks selectedTask)
        {
            _task = selectedTask;
            _parentForm = parent;
            InitializeComponent();
            initCombobox();
        }

        private void initCombobox()
        {
            initMoodTexts();
            //lblTo.Text = string.Format("Poke: {0}",frmMain.usersDict[_task.userID.Value].userName);

            cboTo.Properties.ValueMember = "ID";
            cboTo.Properties.DisplayMember = "userName";
            cboTo.Properties.Columns.Add(new LookUpColumnInfo("userName", 80));
            cboTo.Properties.DataSource = frmMain.usersList;
            cboTo.EditValue =  _task.userID;
            
        }

        private void initMoodTexts()
        {
            List<string> frientlyTexts = new List<string>();
            frientlyTexts.Add("Just wondering...");
            frientlyTexts.Add("Any news?");
            frientlyTexts.Add("Nock Nock :-)");
            moodTexts.Add(enPokeMood.friendly, frientlyTexts);

            List<string> FrustratedTexts = new List<string>();
            FrustratedTexts.Add("Hmm...");
            FrustratedTexts.Add("Any chance getting your attention here?");
            moodTexts.Add(enPokeMood.frustrated, FrustratedTexts);

            List<string> surprisedTexts = new List<string>();
            surprisedTexts.Add("I was sure it's a matter of minutes");
            surprisedTexts.Add("Not done yet ??");
            moodTexts.Add(enPokeMood.surprised, surprisedTexts);

            List<string> madTexts = new List<string>();
            madTexts.Add("I'm loosing it !");
            madTexts.Add("Wrrrr...");
            moodTexts.Add(enPokeMood.mad, madTexts);

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

        private static int MAX_ALERT_TASK_NAME_LENGTH = 40;
        private void btnPoke_Click(object sender, EventArgs e)
        {
            string comment = txtRemarks.Text.Length>0 ? txtRemarks.Text: cboPokeText.EditValue.ToString();
            string alertText = string.Format("<b>Task: {0}... </b><br>{1}", _task.taskName.Substring(0, Math.Min(_task.taskName.Length,MAX_ALERT_TASK_NAME_LENGTH)), comment);
            ConnectionManager.Proxy.Poke(new Poke() { TaskID= _task.ID, From = frmMain.loggedInUserID, To = _task.userID.Value, SendEmail = chkEmail.Checked, Comment = alertText, PokeMood = _selectedMood, SentOn = DateTime.UtcNow, Type = enPokeType.Invoker });
            this.Close();
        }

        

        private void PokeForm_Load(object sender, EventArgs e)
        {
            btnFriendly.Checked = true;
        }

        private enPokeMood _selectedMood;
        private void selectMood(enPokeMood mood)
        {
            _selectedMood = mood;
            switch (mood)
            {
                case enPokeMood.friendly:
                    btnFrustrated.Checked = btnSurprised.Checked = btnMad.Checked = false;
                    cboPokeText.Properties.DataSource = moodTexts[enPokeMood.friendly];
                    cboPokeText.EditValue = moodTexts[enPokeMood.friendly][0];
                break;
                case enPokeMood.frustrated:
                    btnFriendly.Checked = btnSurprised.Checked = btnMad.Checked = false;
                    cboPokeText.Properties.DataSource = moodTexts[enPokeMood.frustrated];
                    cboPokeText.EditValue = moodTexts[enPokeMood.frustrated][0];
                break;
                case enPokeMood.surprised:
                    btnFriendly.Checked = btnFrustrated.Checked = btnMad.Checked = false;
                    cboPokeText.Properties.DataSource = moodTexts[enPokeMood.surprised];
                    cboPokeText.EditValue = moodTexts[enPokeMood.surprised][0];
                break;
                case enPokeMood.mad:
                    btnFriendly.Checked = btnFrustrated.Checked = btnSurprised.Checked = false;
                    cboPokeText.Properties.DataSource = moodTexts[enPokeMood.mad];
                    cboPokeText.EditValue = moodTexts[enPokeMood.mad][0];
                break;

            }
        }


        private void btnFriendly_CheckedChanged(object sender, EventArgs e)
        {
            if (btnFriendly.Checked)
            {
                selectMood(enPokeMood.friendly);
            }
        }

        private void btnFrustrated_CheckedChanged(object sender, EventArgs e)
        {
            if (btnFrustrated.Checked)
            {
                selectMood(enPokeMood.frustrated);
            }
        }

        private void btnSurprised_CheckedChanged(object sender, EventArgs e)
        {
            if (btnSurprised.Checked)
            {
                selectMood(enPokeMood.surprised);
            }
        }

        private void btnMad_CheckedChanged(object sender, EventArgs e)
        {
            if (btnMad.Checked)
            {
                selectMood(enPokeMood.mad);
            }
        }

        private void txtRemarks_EditValueChanged(object sender, EventArgs e)
        {
            cboPokeText.Enabled = !(txtRemarks.Text.Length > 0);
        }
    }
    
    
}