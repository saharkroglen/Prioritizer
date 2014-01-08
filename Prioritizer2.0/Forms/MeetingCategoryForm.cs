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
    public partial class meetingCategoryForm : Form
    {
        private List<MeetingCategory> meetingCategoriesList;
        //private prioritizerDBEntities repository = NewPrioritizer.repository;

        List<MeetingCategory> deletedRowsList = new List<MeetingCategory>();
        public meetingCategoryForm()
        {
            InitializeComponent();
            
        }
        

        private void meetingCategoryForm_Load(object sender, EventArgs e)
        {
            meetingCategoriesList = NewPrioritizer.getMeetingCategoryList(false,true); //repository.MeetingCategory.Where(mc => mc.CategoryOwner == NewPrioritizer.loggedInUserID).ToList();
            meetingCategoriesList.ToList().ForEach(i => i.StartTracking()); //start the self tracking for each element
            meetingCategoryGrid.DataSource = meetingCategoriesList;            
                        
            SetGridProperties();
            SetColumnsProperties();
           
        }


        private void SetGridProperties()
        {            
            meetingCategoryGrid.MasterTemplate.AllowAddNewRow = true;
            meetingCategoryGrid.MasterTemplate.AutoGenerateColumns = true;
            meetingCategoryGrid.MasterTemplate.EnableGrouping = true;
            meetingCategoryGrid.MasterTemplate.EnableFiltering = true;
            meetingCategoryGrid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            meetingCategoryGrid.MasterTemplate.AllowRowResize = true;
            meetingCategoryGrid.MasterTemplate.AllowColumnResize = true;
            meetingCategoryGrid.GridElement.TableHeaderHeight = 50;
        }

        private void SetColumnsProperties()
        {

            foreach (GridViewColumn col in meetingCategoryGrid.MasterTemplate.Columns)
            {
                col.WrapText = true;
                col.AllowResize = true;
                col.AllowResize = true;
            }

            meetingCategoryGrid.EnableHotTracking = true;

            //hide columns
            meetingCategoryGrid.MasterTemplate.Columns["ID"].IsVisible = false;            
            
            ////remove auto generated columns
            meetingCategoryGrid.Columns.Remove(meetingCategoryGrid.MasterTemplate.Columns["categoryOwner"]);
            meetingCategoryGrid.Columns.Remove(meetingCategoryGrid.MasterTemplate.Columns["users"]);
            meetingCategoryGrid.Columns.Remove(meetingCategoryGrid.MasterTemplate.Columns["meetingcategorymap"]);
            meetingCategoryGrid.Columns.Remove(meetingCategoryGrid.MasterTemplate.Columns["ChangeTracker"]);


            meetingCategoryGrid.MasterTemplate.Columns["CategoryName"].Width = 70;
            meetingCategoryGrid.MasterTemplate.Columns["CategoryName"].HeaderText = "Category Name";            
            
            meetingCategoryGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            //add new lookup column instead of RequesterID column which was removed
            /*GridViewComboBoxColumn ManagerCombo = new GridViewComboBoxColumn();
            ManagerCombo.FieldName = "ManagerID";
            ManagerCombo.ValueMember = "ID";
            ManagerCombo.DisplayMember = "Username";
            ManagerCombo.DataSource = NewPrioritizer.projectsList;
            ManagerCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            ManagerCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            projectsGrid.Columns.Add(ManagerCombo);
            projectsGrid.MasterTemplate.Columns["ManagerID"].HeaderText = "Manager";


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

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //deleted rows
                if (deletedRowsList.Count() > 0)
                {
                    foreach (var x in deletedRowsList)
                    {
                        //repository.MeetingCategory.DeleteObject(x);
                        NewPrioritizer.ProxyClient.deleteMeetingCategory(x);
                    }
                    deletedRowsList.Clear();
                }

                //modified or added rows
                foreach (MeetingCategory category in meetingCategoriesList)
                {
                    if (category.ChangeTracker.State != ObjectState.Unchanged)
                    {
                        NewPrioritizer.ProxyClient.applyChangesMeetingCategory(category,null);                        
                    }
                }
                //NewPrioritizer.repository.SaveChanges();
                //
                this.Close();
            }
            catch (Exception ex) { }
        }

        private void authorizationGrid_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {
            
        }

        private void authorizationGrid_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            deletedRowsList.Add((MeetingCategory)(((Telerik.WinControls.UI.BaseGridNavigator)(sender)).MasterTemplate.CurrentRow).DataBoundItem);
        }

        private void meetingCategoryGrid_UserAddedRow(object sender, GridViewRowEventArgs e)
        {
            MeetingCategory category = (MeetingCategory)e.Row.DataBoundItem;

            category.CategoryOwner = NewPrioritizer.loggedInUserID;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
