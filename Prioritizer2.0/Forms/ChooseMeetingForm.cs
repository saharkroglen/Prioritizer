using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prioritizer2._0.Forms
{
    public partial class ChooseMeetingForm : Form
    {
        private NewPrioritizer _parentForm;
        public Guid? selectedMeetingID;
        public ChooseMeetingForm(NewPrioritizer parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            bindControls();
        }

        private void bindControls()
        {
            cboMeetings.DisplayMember = "MeetingName";
            cboMeetings.ValueMember = "ID";
            cboMeetings.DataSource = _parentForm.getMeetingsDatasource();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            selectedMeetingID = Guid.Parse(cboMeetings.SelectedValue.ToString());
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
