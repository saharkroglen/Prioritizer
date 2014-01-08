using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Netformx.Online.Services.PrioritizerService.Contracts.Data;
//using Netformx.Online.Foundation.SelfTrackingEntities;
using PrioritizerService.Model;

namespace Prioritizer2._0.Forms
{
    public partial class MeetingForm : Form
    {
        private List<MeetingCategory> _meetingCategories;
        private NewPrioritizer _parentForm;
        public MeetingForm(NewPrioritizer parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;

            initStuff();
            loadLookups();
            bindCombos();
        }

        private void initStuff()
        {
            dtmMeetingDate.Value = DateTime.Now;
        }

        private void loadLookups()
        {
            _meetingCategories = NewPrioritizer.getMeetingCategoryList(true,true);
            
        
        }
        private void bindCombos()
        {
            cboMeetingCategory.ValueMember = "ID";
            cboMeetingCategory.DisplayMember = "CategoryName";
            cboMeetingCategory.DataSource = _meetingCategories;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Guid? meetingCategory = null;
            if (cboMeetingCategory.SelectedValue != null)
                meetingCategory = Guid.Parse(cboMeetingCategory.SelectedValue.ToString());

            CreateMeeting(txtMeetingName.Text, dtmMeetingDate.Value, meetingCategory,null,null);
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static void CreateMeeting(string meetingName, DateTime meetingDate, Guid? meetingCategory, byte[] meetingSummaryRTF, List<Users> attendeeslList)
        {
            Meetings m = new Meetings();
            m.StartTracking();
            m.MeetingName = meetingName;
            m.MeetingOwner = NewPrioritizer.loggedInUserID;
            m.MeetingDate = meetingDate;
            m.MeetingSummaryRTF = meetingSummaryRTF;
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
            if (meetingCategory != null && meetingCategory != Guid.Parse("00000000-0000-0000-0000-000000000001"))
            {
                MeetingCategoryMap mcm = new MeetingCategoryMap();
                mcm.StartTracking();
                mcm.MeetingCategoryID = meetingCategory;
                mcm.MeetingID = m.ID;
                m.MeetingCategoryMap.Add(mcm);
            }

            NewPrioritizer.ProxyClient.applyChangesMeetings(m,null);
            //NewPrioritizer.repository.SaveChanges();
        }

        private void btnEditMeetingCategories_Click(object sender, EventArgs e)
        {
            meetingCategoryForm mcf = new meetingCategoryForm();
            mcf.ShowDialog();            
            loadLookups();
            bindCombos();
        }
    }
}
