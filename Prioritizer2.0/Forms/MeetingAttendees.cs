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

namespace Prioritizer2._0
{
    public partial class MeetingAttendeesForm : Form
    {
        private static List<MeetingAttendies> usersList;
        //private prioritizerDBEntities repository = NewPrioritizer.repository;
        List<MeetingAttendies> deletedRowsList = new List<MeetingAttendies>();
        private Guid _meetingID;
        public MeetingAttendeesForm(Guid MeetingID)
        {
            _meetingID = MeetingID;
            InitializeComponent();
            
        }

        private void MeetingAttendees_Load(object sender, EventArgs e)
        {

            usersList = NewPrioritizer.ProxyClient.getMeetingAttendees(_meetingID).ToList();
            usersList.ToList().ForEach(i => i.StartTracking()); //start the self tracking for each element
            usersGrid.DataSource = usersList;
            
            SetGridProperties();
            SetColumnsProperties();
           
        }


        private void SetGridProperties()
        {            
            usersGrid.MasterTemplate.AllowAddNewRow = true;
            usersGrid.MasterTemplate.AutoGenerateColumns = true;
            usersGrid.MasterTemplate.EnableGrouping = true;
            usersGrid.MasterTemplate.EnableFiltering = true;
            usersGrid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            usersGrid.MasterTemplate.AllowRowResize = true;
            usersGrid.MasterTemplate.AllowColumnResize = true;
            usersGrid.GridElement.TableHeaderHeight = 50;
        }

        private void SetColumnsProperties()
        {

            foreach (GridViewColumn col in usersGrid.MasterTemplate.Columns)
            {
                col.WrapText = true;
                col.AllowResize = true;
                col.AllowResize = true;
            }

            usersGrid.EnableHotTracking = true;

            //hide columns
            usersGrid.MasterTemplate.Columns["ID"].IsVisible = false;            
            
            ////remove auto generated columns
            usersGrid.Columns.Remove(usersGrid.MasterTemplate.Columns["meetings"]);
            usersGrid.Columns.Remove(usersGrid.MasterTemplate.Columns["users"]);
            usersGrid.Columns.Remove(usersGrid.MasterTemplate.Columns["AttendeeID"]);
            usersGrid.Columns.Remove(usersGrid.MasterTemplate.Columns["changetracker"]);
            usersGrid.Columns.Remove(usersGrid.MasterTemplate.Columns["meetingID"]);
            


            //usersGrid.MasterTemplate.Columns["AttendeeID"].Width = 70;
            //usersGrid.MasterTemplate.Columns["AttendeeID"].HeaderText = "User Name";            
            

            //add new lookup column instead of attendeeID column which was removed
            GridViewComboBoxColumn ManagerCombo = new GridViewComboBoxColumn();
            ManagerCombo.FieldName = "attendeeID";
            ManagerCombo.ValueMember = "ID";
            ManagerCombo.DisplayMember = "Username";
            ManagerCombo.DataSource = NewPrioritizer.usersList;
            ManagerCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            ManagerCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            usersGrid.Columns.Add(ManagerCombo);
            usersGrid.MasterTemplate.Columns["attendeeID"].HeaderText = "Attendee";

            usersGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

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
                        NewPrioritizer.ProxyClient.deleteMeetingAttendies(x);
                    }
                    deletedRowsList.Clear();
                }

                //modified or added rows
                foreach (MeetingAttendies user in usersList)
                {
                    if (user.ChangeTracker.State != ObjectState.Unchanged)
                    {
                        user.MeetingID = _meetingID;
                        NewPrioritizer.ProxyClient.applyChangesMeetingAttendies(user);
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
            deletedRowsList.Add((MeetingAttendies)(((Telerik.WinControls.UI.BaseGridNavigator)(sender)).MasterTemplate.CurrentRow).DataBoundItem);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
