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
    public partial class UsersForm : Form
    {
        private static List<Users> usersList;
        //private prioritizerDBEntities repository = NewPrioritizer.repository;
        List<Users> deletedRowsList = new List<Users>();
        public UsersForm()
        {
            InitializeComponent();
            
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
           
            usersList = NewPrioritizer.ProxyClient.getUsers(null).ToList();
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
            usersGrid.Columns.Remove(usersGrid.MasterTemplate.Columns["userid_teammemberid"]);
            usersGrid.Columns.Remove(usersGrid.MasterTemplate.Columns["userid_managerid"]);
            usersGrid.Columns.Remove(usersGrid.MasterTemplate.Columns["changeTracker"]);


            usersGrid.MasterTemplate.Columns["username"].Width = 70;
            usersGrid.MasterTemplate.Columns["username"].HeaderText = "User Name";
            usersGrid.MasterTemplate.Columns["domainusername"].Width = 70;
            usersGrid.MasterTemplate.Columns["domainusername"].HeaderText = "Domain Name";
            usersGrid.MasterTemplate.Columns["email"].Width = 120;
            usersGrid.MasterTemplate.Columns["email"].HeaderText = "Email";
            usersGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            //add new lookup column instead of RequesterID column which was removed
            /*GridViewComboBoxColumn ManagerCombo = new GridViewComboBoxColumn();
            ManagerCombo.FieldName = "ManagerID";
            ManagerCombo.ValueMember = "ID";
            ManagerCombo.DisplayMember = "Username";
            ManagerCombo.DataSource = NewPrioritizer.usersList;
            ManagerCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            ManagerCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            usersGrid.Columns.Add(ManagerCombo);
            usersGrid.MasterTemplate.Columns["ManagerID"].HeaderText = "Manager";


            //add new lookup column instead of RequesterID column which was removed
            GridViewComboBoxColumn UsersCombo = new GridViewComboBoxColumn();
            UsersCombo.FieldName = "TeamMemberID";
            UsersCombo.ValueMember = "ID";
            UsersCombo.DisplayMember = "Username";
            UsersCombo.DataSource = NewPrioritizer.usersList;
            UsersCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            UsersCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            usersGrid.Columns.Add(UsersCombo);
            usersGrid.MasterTemplate.Columns["TeamMemberID"].HeaderText = "Team Member";
            
            usersGrid.MasterTemplate.Columns["ManagerID"].Width = 100;
            usersGrid.MasterTemplate.Columns["TeamMemberID"].Width = 100;*/

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
                        NewPrioritizer.ProxyClient.deleteUsers(x);
                    }
                    deletedRowsList.Clear();
                }

                //modified or added rows
                foreach (Users user in usersList)
                {


                    if (user.ChangeTracker.State != ObjectState.Unchanged)
                    {
                        NewPrioritizer.ProxyClient.applyChangesUsers(user);
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
            deletedRowsList.Add((Users)(((Telerik.WinControls.UI.BaseGridNavigator)(sender)).MasterTemplate.CurrentRow).DataBoundItem);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
