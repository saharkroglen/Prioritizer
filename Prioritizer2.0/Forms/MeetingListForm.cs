using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using PrioritizerService.Model;
//using Netformx.Online.Services.PrioritizerService.Contracts.Data;
//using Netformx.Online.Foundation.SelfTrackingEntities;

namespace Prioritizer2._0.Forms
{
    public partial class MeetingListForm : Form
    {
        private List<Meetings> meetingList;
        //private prioritizerDBEntities repository = NewPrioritizer.repository;
        List<Meetings> deletedRowsList = new List<Meetings>();
        public MeetingListForm()
        {
            InitializeComponent();
            
        }

        private void MeetingListForm_Load(object sender, EventArgs e)
        {

            meetingList = NewPrioritizer.ProxyClient.getMeetingsForOwner(NewPrioritizer.loggedInUserID).ToList();
            //meetingList = repository.Meetings.Where(mc => mc.MeetingOwner == NewPrioritizer.loggedInUserID).ToList();
            meetingList.ToList().ForEach(i => i.StartTracking()); //start the self tracking for each element
            MeetingListGrid.DataSource = meetingList;
            
            SetGridProperties();
            SetColumnsProperties();
           
        }


        private void SetGridProperties()
        {            
            MeetingListGrid.MasterTemplate.AllowAddNewRow = true;
            MeetingListGrid.MasterTemplate.AutoGenerateColumns = true;
            MeetingListGrid.MasterTemplate.EnableGrouping = true;
            MeetingListGrid.MasterTemplate.EnableFiltering = true;
            MeetingListGrid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            MeetingListGrid.MasterTemplate.AllowRowResize = true;
            MeetingListGrid.MasterTemplate.AllowColumnResize = true;
            MeetingListGrid.GridElement.TableHeaderHeight = 50;
        }

        private void SetColumnsProperties()
        {

            foreach (GridViewColumn col in MeetingListGrid.MasterTemplate.Columns)
            {
                col.WrapText = true;
                col.AllowResize = true;
                col.AllowResize = true;
            }

            MeetingListGrid.EnableHotTracking = true;

            //hide columns
            MeetingListGrid.MasterTemplate.Columns["ID"].IsVisible = false;            
            
            ////remove auto generated columns
            MeetingListGrid.Columns.Remove(MeetingListGrid.MasterTemplate.Columns["meetingOwner"]);
            MeetingListGrid.Columns.Remove(MeetingListGrid.MasterTemplate.Columns["meetingTasks"]);
            MeetingListGrid.Columns.Remove(MeetingListGrid.MasterTemplate.Columns["meetingAttendies"]);
            MeetingListGrid.Columns.Remove(MeetingListGrid.MasterTemplate.Columns["users"]);
            MeetingListGrid.Columns.Remove(MeetingListGrid.MasterTemplate.Columns["meetingCategoryMap"]);
            MeetingListGrid.Columns.Remove(MeetingListGrid.MasterTemplate.Columns["ChangeTracker"]);
            MeetingListGrid.Columns.Remove(MeetingListGrid.MasterTemplate.Columns["meetingCategoryID"]);
            

            MeetingListGrid.MasterTemplate.Columns["MeetingName"].Width = 70;
            MeetingListGrid.MasterTemplate.Columns["MeetingName"].HeaderText = "Meeting Name";

            MeetingListGrid.MasterTemplate.Columns["MeetingDate"].Width = 70;
            MeetingListGrid.MasterTemplate.Columns["MeetingDate"].HeaderText = "Meeting Date";            
            
            MeetingListGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            //add new lookup column instead of RequesterID column which was removed
            GridViewComboBoxColumn meetingCategoryCombo = new GridViewComboBoxColumn();
            meetingCategoryCombo.FieldName = "meetingCategoryID";
            meetingCategoryCombo.ValueMember = "ID";
            meetingCategoryCombo.DisplayMember = "CategoryName";
            meetingCategoryCombo.DataSource = NewPrioritizer.getMeetingCategoryList(true,true);
            meetingCategoryCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            meetingCategoryCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            meetingCategoryCombo.RadPropertyChanged += new Telerik.WinControls.RadPropertyChangedEventHandler(meetingCategoryCombo_RadPropertyChanged);
            MeetingListGrid.Columns.Add(meetingCategoryCombo);
            MeetingListGrid.MasterTemplate.Columns["MeetingCategoryID"].HeaderText = "Category";

            /*
            //add new lookup column instead of RequesterID column which was removed
            GridViewComboBoxColumn projectsCombo = new GridViewComboBoxColumn();
            projectsCombo.FieldName = "TeamMemberID";
            projectsCombo.ValueMember = "ID";
            projectsCombo.DisplayMember = "Username";
            projectsCombo.DataSource = NewPrioritizer.projectsList;
            projectsCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            projectsCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            projectsGrid.Columns.Add(projectsCombo);
            projectsGrid.MasterTemplate.Columns["TeamMemberID"].HeaderText = "Team Member";
            
            projectsGrid.MasterTemplate.Columns["ManagerID"].Width = 100;
            projectsGrid.MasterTemplate.Columns["TeamMemberID"].Width = 100;*/

        }

        void meetingCategoryCombo_RadPropertyChanged(object sender, Telerik.WinControls.RadPropertyChangedEventArgs e)
        {            
            //Meetings meeting = (Meetings)MeetingListGrid.CurrentRow.DataBoundItem;
                        
            //if (e.NewValue != e.OldValue)
            //    meeting.MeetingCategoryID = Convert.ToInt16(e.NewValue);
            
        }

      

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //deleted rows
                if (deletedRowsList.Count() > 0)
                {
                    foreach (var x in deletedRowsList)
                    {
                        if (x.MeetingCategoryMap.Count > 0)
                            NewPrioritizer.ProxyClient.deleteMeetingCategoryMap(x.MeetingCategoryMap[0]); //first delete child objects
                        if (x.MeetingTasks.Count > 0)
                            NewPrioritizer.ProxyClient.deleteMeetingTasks(x.MeetingTasks[0]); //first delete child objects
                        NewPrioritizer.ProxyClient.deleteMeetings(x);
                    }
                    deletedRowsList.Clear();
                }

                //modified or added rows
                foreach (Meetings meeting in meetingList)
                {
                    if (meeting.ChangeTracker.State != ObjectState.Unchanged)
                    {
                        NewPrioritizer.ProxyClient.applyChangesMeetings(meeting,null);
                    }
                    
                }
                //NewPrioritizer.repository.SaveChanges();
                this.Close();
            }
            catch (Exception ex) { }
        }

        private void authorizationGrid_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {
            
        }

        private void authorizationGrid_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            deletedRowsList.Add((Meetings)(((Telerik.WinControls.UI.BaseGridNavigator)(sender)).MasterTemplate.CurrentRow).DataBoundItem);
        }

        private void MeetingListGrid_UserAddedRow(object sender, GridViewRowEventArgs e)
        {
            Meetings meeting = (Meetings)e.Row.DataBoundItem;

            meeting.MeetingOwner = NewPrioritizer.loggedInUserID;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
