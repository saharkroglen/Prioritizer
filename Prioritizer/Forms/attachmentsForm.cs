﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PrioritizerService.Model;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Linq;
using DevExpress.XtraGrid.Columns;
using Prioritizer.Shared.Model;
using Prioritizer.Proxy;
namespace Prioritizer.Forms
{
    public partial class attachmentsForm : DevExpress.XtraEditors.XtraForm
    {
        private static List<ManagerTeamMemberRelations> relations;
        private frmMain _parentForm;
        List<attachments> deletedRowsList = new List<attachments>();
        List<attachments> _attachCollection;
        public attachmentsForm(List<attachments> attachCollection, frmMain parentForm)
        {
            _parentForm = parentForm;
            _attachCollection = attachCollection;
            InitializeComponent();

        }



        private void SetGridProperties()
        {
            //gridView1.OptionsBehavior.Editable= false;
        }

        private void SetColumnsProperties()
        {

            hideUnnecessaryColumns();
            removeAutoGeneratedColumns();
            bindComboToGrid();
        }

        private void bindComboToGrid()
        {
            //add "open attachment" button
            RepositoryItemButtonEdit repositoryItemButtons = new RepositoryItemButtonEdit();
            repositoryItemButtons.Buttons[0].Kind = ButtonPredefines.Glyph;
            repositoryItemButtons.Buttons[0].Image = Properties.Resources.preview_Attachment16;
            repositoryItemButtons.TextEditStyle = TextEditStyles.HideTextEditor;
            GridColumn unbColumn = gridView1.Columns.AddField("Preview");
            unbColumn.Width = 18;
            unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            unbColumn.VisibleIndex = gridView1.Columns.Count;
            unbColumn.ColumnEdit = repositoryItemButtons;            
            gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            repositoryItemButtons.Click += new EventHandler(btnOpenAttachment_Click);


            ////delete attachment button
            //RepositoryItemButtonEdit repositoryDeleteAttachButton = new RepositoryItemButtonEdit();
            //GridColumn deleteColumn = gridView1.Columns.AddField("Delete");
            //deleteColumn.Width = 18;
            //deleteColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            //deleteColumn.VisibleIndex = gridView1.Columns.Count;
            //deleteColumn.ColumnEdit = repositoryDeleteAttachButton;
            //gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            //repositoryDeleteAttachButton.Click += new EventHandler(repositoryDeleteAttachButton_Click);
           
        }

        void repositoryDeleteAttachButton_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Delete Selected Attachments ?", "Delete Attachment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;

            attachments a = gridView1.GetRow(gridView1.GetSelectedRows().FirstOrDefault()) as attachments;
            ConnectionManager.Proxy.deleteAttachment(a, frmMain.loggedInUserID);
        }

        void btnOpenAttachment_Click(object sender, EventArgs e)
        {
            openSelectedAttachment();
        }

        private void removeAutoGeneratedColumns()
        {
            gridView1.Columns.Remove(gridView1.Columns["changetracker"]);
        }

        private void hideUnnecessaryColumns()
        {
            for (int x = 0; x < gridView1.Columns.Count; x++)
                gridView1.Columns[x].Visible = false;

            gridView1.Columns["fileName"].Visible = true;
        }




        void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void usersGrid_Load(object sender, EventArgs e)
        {

        }

        private void MeetingAttendeesForm_Load(object sender, EventArgs e)
        {
            attachmentsGrid.DataSource = _attachCollection;
            

            SetGridProperties();
            SetColumnsProperties();
        }

        private void usersGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                var selectedRowHandle = gridView1.GetSelectedRows()[0];
                deletedRowsList.Add(gridView1.GetRow(selectedRowHandle) as attachments);
                gridView1.DeleteRow(selectedRowHandle);
            }
        }

        private void attachmentsGrid_Click(object sender, EventArgs e)
        {
            
        }

        private void openSelectedAttachment()
        {
            attachments attch = gridView1.GetRow(gridView1.GetSelectedRows().FirstOrDefault()) as attachments;
            Guid attachID = attch.ID;
            attachments attachedFile = _attachCollection.Where(t => t.ID == attachID).First();
            frmMain.openAttachment(attachedFile.bin, attachedFile.fileName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //deleted rows
            if (deletedRowsList.Count > 0)
            {
                foreach (var x in deletedRowsList)
                {
                    if (x != null)
                        ConnectionManager.Proxy.deleteAttachment(x, frmMain.loggedInUserID);
                }
                deletedRowsList.Clear();
            }

            //modified or added rows
            foreach (attachments attach in _attachCollection)
            {
                if (attach.ChangeTracker.State != ObjectState.Unchanged)
                {
                    attach.TenantID = frmMain._tenantID;
                    ConnectionManager.Proxy.applyChangesAttachments(attach);
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}