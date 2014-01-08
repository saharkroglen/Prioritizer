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

namespace Prioritizer.Forms
{
    public partial class AlertForm : DevExpress.XtraEditors.XtraForm
    {
        private frmMain _parentForm;
        private Prioritizer.Shared.Model.Alerts _alert;
        private List<KeyValuePair<string, int>> snoozeTimeFrames = new List<KeyValuePair<string, int>>();
        private Guid _taskID;

        public AlertForm(frmMain parent, bool showSnoozeOptions, Guid taskID)
        {
            _taskID = taskID;
            _parentForm = parent;
            InitializeComponent();
            initCombobox();
            btnOpenItem.Visible =  btnSnooze.Visible = btnDismiss.Visible = cboSnooze.Visible = lblSnooze.Visible = showSnoozeOptions;
        }

        private void initCombobox()
        {
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("5 min", 5));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("10 min", 10));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("30 min", 30));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("1 hour", 60));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("2 hour", 120));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("5 hours", 300));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("0.5 day", 720));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("1 day", 1440));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("2 days", 2880));
            snoozeTimeFrames.Add(new KeyValuePair<string, int>("3 days", 4320));
            cboSnooze.Properties.DisplayMember = "Key";
            cboSnooze.Properties.ValueMember = "Value";
            cboSnooze.Properties.DataSource = snoozeTimeFrames;            
            cboSnooze.Properties.ShowHeader = false;

            cboSnooze.Properties.Columns.Add(new LookUpColumnInfo("Value", 0));
            cboSnooze.Properties.Columns.Add(new LookUpColumnInfo("Key", 50));

            cboSnooze.EditValue = 5;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Alerts alert = new Alerts();
            save();
        }

        private void save()
        {
            _alert.active = chkActivate.Checked;
            _alert.sendEmail = chkEmail.Checked;
            _alert.nextAlert = Convert.ToDateTime(dtmAlert.EditValue).ToUniversalTime();
            _alert.taskID = _taskID;// _parentForm.SelectedTask.ID;
            ConnectionManager.Proxy.applyChangesAlerts(_alert, frmMain.loggedInUserID);
            this.Close();
        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            Alerts[] alertList = ConnectionManager.Proxy.getAlertForTask(_taskID);
            if (alertList.Length == 0)
            {
                _alert = new Alerts();
                chkActivate.Checked = false;
                chkEmail.Checked = false;
            }
            else
            {
                _alert = alertList[0];
                chkActivate.Checked = _alert.active.Value;
                chkEmail.Checked = _alert.sendEmail.Value;
                dtmAlert.EditValue = _alert.nextAlert.Value.ToLocalTime();
            }
        }

        private void chkActivate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivate.Checked == true)
            {
                dtmAlert.EditValue = DateTime.Now;
            }
        }

        private void btnSnooze_Click(object sender, EventArgs e)
        {
            dtmAlert.EditValue = DateTime.Now.AddMinutes(Convert.ToInt16(cboSnooze.EditValue));
            save();
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            chkActivate.Checked = false;
            save();
        }

        private void btnOpenItem_Click(object sender, EventArgs e)
        {
            _parentForm.openTaskByID(_taskID);
            this.Close();
        }
    }
}