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

namespace Prioritizer2._0
{
    public partial class attachmentsForm : Form
    {
        private static List<ManagerTeamMemberRelations> relations;
        //private prioritizerDBEntities repository = NewPrioritizer.repository;
        List<ManagerTeamMemberRelations> deletedRowsList = new List<ManagerTeamMemberRelations>();
        List<attachments> _attachCollection;
        public attachmentsForm(List<attachments> attachCollection)
        {
            _attachCollection = attachCollection;
            InitializeComponent();
            
        }

        private void attachmentsForm_Load(object sender, EventArgs e)
        {
           
            //relations = repository.ManagerTeamMemberRelations.ToList();
            //relations.ToList().ForEach(i => i.StartTracking()); //start the self tracking for each element
            attachmentsGrid.DataSource = _attachCollection;
            
            SetGridProperties();
            SetColumnsProperties();
           
        }


        private void SetGridProperties()
        {
            attachmentsGrid.MasterTemplate.AllowAddNewRow = true;
            attachmentsGrid.MasterTemplate.AutoGenerateColumns = true;
            attachmentsGrid.MasterTemplate.EnableGrouping = true;
            attachmentsGrid.MasterTemplate.EnableFiltering = true;            
            attachmentsGrid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            attachmentsGrid.MasterTemplate.AllowRowResize = true;
            attachmentsGrid.MasterTemplate.AllowColumnResize = true;
            attachmentsGrid.GridElement.TableHeaderHeight = 50;
            attachmentsGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        }

        private void SetColumnsProperties()
        {

            foreach (GridViewColumn col in attachmentsGrid.MasterTemplate.Columns)
            {
                col.WrapText = true;
                col.AllowResize = true;
                col.AllowResize = true;
            }

            attachmentsGrid.EnableHotTracking = true;
            attachmentsGrid.MasterTemplate.Columns["fileName"].HeaderText = "File Name";

            //hide columns
            attachmentsGrid.MasterTemplate.Columns["ID"].IsVisible = false;
            attachmentsGrid.MasterTemplate.Columns["bin"].IsVisible = false;
            attachmentsGrid.MasterTemplate.Columns["taskID"].IsVisible = false;
            
            ////remove auto generated columns            
            attachmentsGrid.Columns.Remove(attachmentsGrid.MasterTemplate.Columns["changeTracker"]);
            //attachmentsGrid.Columns.Remove(attachmentsGrid.MasterTemplate.Columns["bin"]);
            //attachmentsGrid.Columns.Remove(attachmentsGrid.MasterTemplate.Columns["taskID"]);
          
           


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
                    if (relation.ChangeTracker.State != PrioritizerService.Model.ObjectState.Unchanged)
                    {
                        if (relation.ManagerID == relation.TeamMemberID)
                        {
                            MessageBox.Show("Can't have a relation where manager ID is equal to his team member ID\nThis change won't be saved");

                            continue;
                        }
                        else if (relation.ChangeTracker.State == PrioritizerService.Model.ObjectState.Added)
                        {
                            if (NewPrioritizer.ProxyClient.relationExists(relation.ManagerID,relation.TeamMemberID))
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

        private void attachmentsGrid_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {
            
        }

        private void attachmentsGrid_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            deletedRowsList.Add((ManagerTeamMemberRelations)(((Telerik.WinControls.UI.BaseGridNavigator)(sender)).MasterTemplate.CurrentRow).DataBoundItem);
        }

        private void attachmentsGrid_CellClick(object sender, GridViewCellEventArgs e)
        {
            Guid attachID = Guid.Parse(e.Row.Cells["ID"].Value.ToString());
            attachments attachedFile = _attachCollection.Where(t => t.ID == attachID).First();
            NewPrioritizer.openAttachment(attachedFile.bin, attachedFile.fileName);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
