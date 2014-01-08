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
    public partial class AuthorizationForm : Form
    {
        private static List<ManagerTeamMemberRelations> relations;
        //private prioritizerDBEntities repository = NewPrioritizer.repository;
        List<ManagerTeamMemberRelations> deletedRowsList = new List<ManagerTeamMemberRelations>();
        public AuthorizationForm()
        {
            InitializeComponent();
            
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {

            relations = NewPrioritizer.ProxyClient.getManagerTeamMemberRelationsList(null).ToList();
            
            relations.ToList().ForEach(i => i.StartTracking()); //start the self tracking for each element
            authorizationGrid.DataSource = relations;
            
            SetGridProperties();
            SetColumnsProperties();
           
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SetGridProperties()
        {
            authorizationGrid.MasterTemplate.AllowAddNewRow = true;
            authorizationGrid.MasterTemplate.AutoGenerateColumns = true;
            authorizationGrid.MasterTemplate.EnableGrouping = true;
            authorizationGrid.MasterTemplate.EnableFiltering = true;            
            authorizationGrid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            authorizationGrid.MasterTemplate.AllowRowResize = true;
            authorizationGrid.MasterTemplate.AllowColumnResize = true;
            authorizationGrid.GridElement.TableHeaderHeight = 50;
            authorizationGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        }

        private void SetColumnsProperties()
        {

            foreach (GridViewColumn col in authorizationGrid.MasterTemplate.Columns)
            {
                col.WrapText = true;
                col.AllowResize = true;
                col.AllowResize = true;
            }

            authorizationGrid.EnableHotTracking = true;

            //hide columns
            authorizationGrid.MasterTemplate.Columns["ID"].IsVisible = false;            
            
            ////remove auto generated columns
            authorizationGrid.Columns.Remove(authorizationGrid.MasterTemplate.Columns["ManagerID"]);
            authorizationGrid.Columns.Remove(authorizationGrid.MasterTemplate.Columns["TeamMemberID"]);
            authorizationGrid.Columns.Remove(authorizationGrid.MasterTemplate.Columns["ManagerID_UserID"]);
            authorizationGrid.Columns.Remove(authorizationGrid.MasterTemplate.Columns["TeamMemberID_UserID"]);
            authorizationGrid.Columns.Remove(authorizationGrid.MasterTemplate.Columns["changeTracker"]);
          
           

            //add new lookup column instead of RequesterID column which was removed
            GridViewComboBoxColumn ManagerCombo = new GridViewComboBoxColumn();
            ManagerCombo.FieldName = "ManagerID";
            ManagerCombo.ValueMember = "ID";
            ManagerCombo.DisplayMember = "Username";
            ManagerCombo.DataSource = NewPrioritizer.usersList;
            ManagerCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            ManagerCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            authorizationGrid.Columns.Add(ManagerCombo);
            authorizationGrid.MasterTemplate.Columns["ManagerID"].HeaderText = "Manager";


            //add new lookup column instead of RequesterID column which was removed
            GridViewComboBoxColumn UsersCombo = new GridViewComboBoxColumn();
            UsersCombo.FieldName = "TeamMemberID";
            UsersCombo.ValueMember = "ID";
            UsersCombo.DisplayMember = "Username";
            UsersCombo.DataSource = NewPrioritizer.usersList;
            UsersCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            UsersCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            authorizationGrid.Columns.Add(UsersCombo);
            authorizationGrid.MasterTemplate.Columns["TeamMemberID"].HeaderText = "Team Member";
            
            authorizationGrid.MasterTemplate.Columns["ManagerID"].Width = 100;
            authorizationGrid.MasterTemplate.Columns["TeamMemberID"].Width = 100;

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
                        NewPrioritizer.ProxyClient.deleteManagerTeamMemberRelations(x);
                    }
                    deletedRowsList.Clear();
                }

                //modified or added rows
                foreach (ManagerTeamMemberRelations relation in relations)
                {


                    if (relation.ChangeTracker.State != ObjectState.Unchanged)
                    {
                        if (relation.ManagerID == relation.TeamMemberID)
                        {
                            MessageBox.Show("Can't have a relation where manager ID is equal to his team member ID\nThis change won't be saved");

                            continue;
                        }
                        else if (relation.ChangeTracker.State == ObjectState.Added)
                        {
                            if (NewPrioritizer.ProxyClient.relationExists(relation.ManagerID, relation.TeamMemberID))
                            {
                                MessageBox.Show("Can't save a duplicate relation\nThis change won't be saved");
                                continue;
                            }
                        }
                        NewPrioritizer.ProxyClient.applyChangesManagerTeamMemberRelations(relation);
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
            deletedRowsList.Add((ManagerTeamMemberRelations)(((Telerik.WinControls.UI.BaseGridNavigator)(sender)).MasterTemplate.CurrentRow).DataBoundItem);
        }
    }
}
