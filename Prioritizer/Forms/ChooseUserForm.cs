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
namespace Prioritizer.Forms
{
    public partial class ChooseUserForm : DevExpress.XtraEditors.XtraForm
    {
        public Users SelectedUser = null;
        private frmMain _parentForm;
        
        public ChooseUserForm(frmMain parentForm)
        {      
            _parentForm = parentForm;
            InitializeComponent();
            
            this.DialogResult = DialogResult.None;
                                    
            //cmbUsers.Properties.ValueMember = "ID";
            cmbUsers.Properties.DisplayMember = "userName";
            cmbUsers.Properties.Columns.Add(new LookUpColumnInfo("userName", 80));
            cmbUsers.Properties.DataSource = new BindingList<Users>(frmMain.usersList);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

      
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbUsers.EditValue != null)
                SelectedUser = cmbUsers.EditValue as Users;

            this.DialogResult = DialogResult.OK;
        }

     

    }
}