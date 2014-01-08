using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
//using Netformx.Online.Services.PrioritizerService.Contracts.Data;
//using Netformx.Online.Foundation.SelfTrackingEntities;
using PrioritizerService.Model;

namespace Prioritizer2._0
{
    public partial class projectsForm : Form
    {
        private static List<projects> projectsList;
       // private prioritizerDBEntities repository = NewPrioritizer.repository;
        List<projects> deletedRowsList = new List<projects>();
        public projectsForm()
        {
            InitializeComponent();
            
        }

        private void projectsForm_Load(object sender, EventArgs e)
        {

            projectsList = NewPrioritizer.ProxyClient.getProjectList(null).ToList();
            projectsList.ToList().ForEach(i => i.StartTracking()); //start the self tracking for each element
            projectsGrid.DataSource = projectsList;
            
            SetGridProperties();
            SetColumnsProperties();
           
        }


        private void SetGridProperties()
        {            
            projectsGrid.MasterTemplate.AllowAddNewRow = true;
            projectsGrid.MasterTemplate.AutoGenerateColumns = true;
            projectsGrid.MasterTemplate.EnableGrouping = true;
            projectsGrid.MasterTemplate.EnableFiltering = true;
            projectsGrid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            projectsGrid.MasterTemplate.AllowRowResize = true;
            projectsGrid.MasterTemplate.AllowColumnResize = true;
            projectsGrid.GridElement.TableHeaderHeight = 50;
        }

        private void SetColumnsProperties()
        {

            foreach (GridViewColumn col in projectsGrid.MasterTemplate.Columns)
            {
                col.WrapText = true;
                col.AllowResize = true;
                col.AllowResize = true;
            }

            projectsGrid.EnableHotTracking = true;

            //hide columns
            projectsGrid.MasterTemplate.Columns["ID"].IsVisible = false;            
            
            ////remove auto generated columns
            projectsGrid.Columns.Remove(projectsGrid.MasterTemplate.Columns["userid_teammemberid"]);
            projectsGrid.Columns.Remove(projectsGrid.MasterTemplate.Columns["userid_managerid"]);
            projectsGrid.Columns.Remove(projectsGrid.MasterTemplate.Columns["changeTracker"]);


            projectsGrid.MasterTemplate.Columns["projectName"].Width = 70;
            projectsGrid.MasterTemplate.Columns["projectName"].HeaderText = "Project Name";            
            
            projectsGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

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
                        NewPrioritizer.ProxyClient.deleteProjects(x);
                    }
                    deletedRowsList.Clear();
                }

                //modified or added rows
                foreach (projects user in projectsList)
                {


                    if (user.ChangeTracker.State != ObjectState.Unchanged)
                    {
                        NewPrioritizer.ProxyClient.applyChangesProjects(user,null);
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
            deletedRowsList.Add((projects)(((Telerik.WinControls.UI.BaseGridNavigator)(sender)).MasterTemplate.CurrentRow).DataBoundItem);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
