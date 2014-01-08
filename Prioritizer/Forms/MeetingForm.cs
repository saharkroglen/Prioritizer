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
using Prioritizer.Shared.Model;
using Prioritizer.Proxy;

namespace Prioritizer.Forms
{
    public partial class MeetingForm : DevExpress.XtraEditors.XtraForm
    {
        private List<MeetingCategory> _meetingCategories;
        private frmMain _parentForm;
        public MeetingForm(frmMain parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
                        
            this.DialogResult = DialogResult.None;

            initStuff();
            loadLookups();
            bindCombos();

        }

        private void initStuff()
        {
            dtmMeetingDate.EditValue = DateTime.Now;
        }

        private void loadLookups()
        {
            _meetingCategories = _parentForm.getMeetingCategoryList(false, true);


        }
        private void bindCombos()
        {
            //cboMeetingCategory.Properties.ValueMember = "ID";
            //cboMeetingCategory.Properties.DisplayMember = "userName";
            //cboMeetingCategory.Properties.Columns.Add(new LookUpColumnInfo("userName", 80));
            //cboMeetingCategory.Properties.DataSource = frmMain.usersList;
            bindMeetingCategoryCombo(false);
        }
        private void bindMeetingCategoryCombo(bool freshBinding)
        {
            if (cboMeetingCategory.Properties.DataSource == null || freshBinding)
            {
                cboMeetingCategory.Properties.DataSource = null;
                cboMeetingCategory.Properties.ValueMember = "ID";
                cboMeetingCategory.Properties.DisplayMember = "CategoryName";
                cboMeetingCategory.Properties.DataSource = _meetingCategories;
                //cboMeetingCategory.EditValue = Guid.Empty;

                // Add two columns to the dropdown.
                if (cboMeetingCategory.Properties.Columns.Count == 0)
                {
                    cboMeetingCategory.Properties.Columns.Add(new LookUpColumnInfo("ID", 0));
                    cboMeetingCategory.Properties.Columns.Add(new LookUpColumnInfo("CategoryName", 150));
                }

            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void showAssignedToControl(bool show)
        {
            lblAssignedTo.Visible = show;
            cboMeetingCategory.Visible = show;
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (Validate())
            {
                Guid? meetingCategory = null;
                if (cboMeetingCategory.EditValue != null /*&& Guid.Parse(cboMeetingCategory.EditValue) != Guid.Empty*/)
                    meetingCategory = Guid.Parse(cboMeetingCategory.EditValue.ToString());

                CreateMeeting(txtMeetingName.Text, Convert.ToDateTime(dtmMeetingDate.EditValue), meetingCategory, null, null);
                this.Close();

                this.DialogResult = DialogResult.OK;
            }
        }


        public bool Validate()
        {
            dxErrorProvider1.ClearErrors();
            bool isValid = true;
            string ErrorCaption = string.Empty;
            if (txtMeetingName.Text.Trim().Length == 0)
            {
                ErrorCaption = "Meeting Name can't be empty";
                dxErrorProvider1.SetError(txtMeetingName, ErrorCaption);
                isValid = false;
            }

            return isValid;
        }

        public void CreateMeeting(string meetingName, DateTime meetingDate, Guid? meetingCategory, byte[] meetingSummaryRTF, List<Users> attendeeslList)
        {
            Meetings m = new Meetings();
            m.StartTracking();
            m.MeetingName = meetingName;
            m.MeetingOwner = frmMain.loggedInUserID;
            m.MeetingDate = meetingDate;
            m.MeetingSummaryRTF = meetingSummaryRTF;
            m.TenantID = frmMain._tenantID;
            m.updateDate = DateTime.Now;


            //add attendee list copied from given list
            if (attendeeslList != null)
            {
                foreach (var user in attendeeslList)
                {
                    MeetingAttendies attendee = new MeetingAttendies();
                    attendee.StartTracking();
                    attendee.AttendeeID = user.ID;
                    attendee.MeetingID = m.ID;
                    m.MeetingAttendies.Add(attendee);
                }
            }

            //add meeting category according to given category
            if (meetingCategory != null && meetingCategory != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                MeetingCategoryMap mcm = new MeetingCategoryMap();
                mcm.StartTracking();
                mcm.MeetingCategoryID = meetingCategory;
                mcm.MeetingID = m.ID;
                mcm.TenantID = frmMain._tenantID;
                m.MeetingCategoryMap.Add(mcm);
            }

            ConnectionManager.Proxy.applyChangesMeetings(m, frmMain._tenantID);
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = _parentForm.AddMeetingCategory();
            loadLookups();
            bindMeetingCategoryCombo(true);
            foreach (var c in _meetingCategories)
            {
                if (c.CategoryName == categoryName)
                {
                    cboMeetingCategory.EditValue = c.ID;
                    break;
                }
            }

        }

    }
}