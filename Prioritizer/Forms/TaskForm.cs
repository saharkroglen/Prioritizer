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
using Prioritizer.Proxy;
using Prioritizer.Shared;
namespace Prioritizer.Forms
{
    public partial class TaskForm : DevExpress.XtraEditors.XtraForm
    {
        public Tasks _task;
        private Guid _userId;
        private frmMain _parentForm;
        private formMode _mode;
        private enTaskType _taskType;
        public TaskForm(frmMain parentForm, Tasks task , formMode mode, enTaskType taskType)
        {      
            _mode = mode; _task = task; _parentForm = parentForm;
            _taskType = taskType;
            InitializeComponent();
            _userId = _parentForm.GetSelectedUserID();
            
            this.DialogResult = DialogResult.None;
                                    
            cmbRequester1.Properties.ValueMember = "ID";
            cmbRequester1.Properties.DisplayMember = "userName";
            cmbRequester1.Properties.Columns.Add(new LookUpColumnInfo("userName", 80));
            cmbRequester1.Properties.DataSource = new BindingList<Users>(frmMain.usersList);

            cmbAssignedTo.Properties.ValueMember = "ID";
            cmbAssignedTo.Properties.DisplayMember = "userName";
            cmbAssignedTo.Properties.Columns.Add(new LookUpColumnInfo("userName", 80));
            cmbAssignedTo.Properties.DataSource = frmMain.usersList;
            cmbAssignedTo.EditValue = task.userID;


            cmbImportance.Properties.ValueMember = "ID";
            cmbImportance.Properties.DisplayMember = "Name";
            cmbImportance.Properties.Columns.Add(new LookUpColumnInfo("Name", 80));
            cmbImportance.Properties.DataSource = frmMain.importanceList;
            cmbImportance.EditValue = enTaskImportance.Low;

            cmbProject1.Properties.ValueMember = "ID";
            cmbProject1.Properties.DisplayMember = "projectName";
            cmbProject1.Properties.Columns.Add(new LookUpColumnInfo("projectName", 80));
            cmbProject1.Properties.DataSource = frmMain.ProjectList;

            if (_task.taskName != null)
                txtName.Text = _task.taskName;
            if (_task.actualWorkHours != null)
                txtActualWork1.Text = _task.actualWorkHours.Value.ToString();
            if (_task.completionPercentage != null)
                txtCompletedPercent1.Text = _task.completionPercentage.Value.ToString();
            if (_task.defectNumber != null)
                txtDefectNum1.Text = _task.defectNumber;
            if (_task.estimatedWorkHours != null)
                txtEstimatedHours1.Text = _task.estimatedWorkHours.Value.ToString();
            if (_task.dueDate != null)
                dtmDueDate1.EditValue = _task.dueDate.Value;


            cmbProject1.EditValue = null;
            cmbImportance.EditValue = _task.Importance;

            if (_task.projectID != null)
                cmbProject1.EditValue = _task.projectID.Value;
            if (_task.requesterID != null && _task.requesterID != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                cmbRequester1.EditValue = _task.requesterID.Value;
            else
                cmbRequester1.EditValue = frmMain.loggedInUserID;

            if (_task.remarks != null)
                txtRemarks.Text = _task.remarks;

            _task.StartTracking();

            if (_parentForm.isMeetingTasksMode)
            {
                if (taskType == enTaskType.ActionItem)
                    showAssignedToControl(true);
                else
                {
                    showAssignedToControl(false);
                    _task.taskType = (int)taskType;
                }
            }
            else
            {
                showAssignedToControl(false);
            }
        
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void showAssignedToControl(bool show)
        {
            lblAssignedTo.Visible = show;
            cmbAssignedTo.Visible = show;
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (_parentForm.isMeetingTasksMode)
            {
                if (_taskType == enTaskType.Decision)
                    _task.userID = null;
                else
                {
                    if (cmbAssignedTo.EditValue != null)
                        _task.userID = Guid.Parse(cmbAssignedTo.EditValue.ToString());
                    else
                        _task.userID = _userId;
                }

                Guid meetingID = Guid.Parse(_parentForm.selectedMeeting.ID.ToString());
                if (_mode == formMode.add)
                {
                    _parentForm.assignTaskToMeeting(_task, meetingID);
                }
            }
            else
            {
                _task.userID = _userId;
            }


            _task.Importance = Convert.ToInt16(cmbImportance.EditValue);
            _task.taskStatusID = _task.taskStatusID ?? 1;//pending

            _task.dateClosed = null;
            _task.dateEntered = _task.dateEntered ?? DateTime.Now;

            //update task with form values
            _task.remarks = txtRemarks.Text;
            _task.taskName = txtName.Text;

            _task.defectNumber = txtDefectNum1.Text;

            if (dtmDueDate1.EditValue != null)
                _task.dueDate = Convert.ToDateTime(dtmDueDate1.EditValue) ;

            if (txtActualWork1.Text.Length > 0)
                _task.actualWorkHours = Convert.ToInt16(txtActualWork1.Text);
            if (txtCompletedPercent1.Text.Length > 0)
                _task.completionPercentage = Convert.ToInt16(txtCompletedPercent1.Text);
            if (txtEstimatedHours1.Text.Length > 0)
                _task.estimatedWorkHours = Convert.ToInt16(txtEstimatedHours1.Text);
            if (cmbProject1.EditValue != null)
                _task.projectID = Guid.Parse(cmbProject1.EditValue.ToString());
            if (cmbRequester1.EditValue != null)
                _task.requesterID = Guid.Parse(cmbRequester1.EditValue.ToString());


            _task.projectID = cmbProject1.EditValue == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : Guid.Parse(cmbProject1.EditValue.ToString());
            _task.requesterID = Guid.Parse(cmbRequester1.EditValue.ToString());

            if (_mode == formMode.add)
            {
                _task.UpdatesLog = _parentForm.getLogDelimiterLine(logDelimiterMode.Added);
                _task.TenantID = frmMain._tenantID;
                _task = ConnectionManager.Proxy.addTask(_task, frmMain.loggedInUserID);
                _parentForm.SetTopPriority(_task.ID);
            }
            else if (_mode == formMode.update)
            {
                _task = ConnectionManager.Proxy.applyChangesTasks(_task, frmMain.loggedInUserID);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

    }
}