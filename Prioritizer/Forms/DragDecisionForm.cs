using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Prioritizer.Forms
{
    public partial class DragDecisionForm : DevExpress.XtraEditors.XtraForm
    {
        public int Result = -1;

        public DragDecisionForm(string AttachToTaskText)
        {
            InitializeComponent();
            btnAttach.ToolTip = AttachToTaskText;
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            Result = 0;
            this.Close();
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            Result = 1;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}