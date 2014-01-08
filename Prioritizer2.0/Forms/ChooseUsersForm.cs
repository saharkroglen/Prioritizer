using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrioritizerService.Model;
//using Netformx.Online.Services.PrioritizerService.Contracts.Data;

namespace Prioritizer2._0.Forms
{
    public partial class ChooseUsersForm : Form
    {
        public List<int> selectedUsers;
        public ChooseUsersForm()
        {
            InitializeComponent();
            bindControls();
        }

        private void bindControls()
        {            
            
            List<Users> userList = NewPrioritizer.ProxyClient.getUsers(null).ToList();
            listUsers.DisplayMember = "userName";
            listUsers.ValueMember = "ID";
            listUsers.DataSource = userList;
        }
                
        private void btnDone_Click(object sender, EventArgs e)
        {
            selectedUsers = listUsers.SelectedItems.Select(a=> Convert.ToInt32(a.DisplayValue)).ToList();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
