using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Prioritizer.Utils;

namespace Prioritizer.Forms
{
    public partial class ChooseMeetingForm : DevExpress.XtraEditors.XtraForm
    {
        private frmMain _parentForm;
        public Guid? selectedMeetingID;
        public ChooseMeetingForm(frmMain parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            bindControls();
        }

        private void bindControls()
        {
            cboMeetings.Properties.DisplayMember = "MeetingName";
            cboMeetings.Properties.ValueMember = "ID";
            cboMeetings.Properties.Columns.Add(new LookUpColumnInfo("MeetingName", 150));
            cboMeetings.Properties.DataSource = _parentForm.getMeetingsDatasource();
        }

        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            selectedMeetingID = Guid.Parse(cboMeetings.EditValue.ToString());
            this.Close();
        }
    }
}