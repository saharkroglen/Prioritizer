using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prioritizer2._0;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
//using Netformx.Online.Services.PrioritizerService.Contracts.Data;
//using Netformx.Online.Foundation.SelfTrackingEntities;
using PrioritizerService.Model;


namespace Prioritizer2._0
{
    public partial class TaskForm : Form
    {
        public Tasks _task;
        private Guid _userId;
        private NewPrioritizer _parentForm;
        private formMode _mode;
        private enTaskType _taskType;

        /// <summary>
        /// ctor used to update existing task
        /// </summary>
        /// <param name="task"></param>
        public TaskForm(NewPrioritizer parentForm, Tasks task , formMode mode, enTaskType taskType)
        {      
            _mode = mode; _task = task; _parentForm = parentForm;
            _taskType = taskType;
            InitializeComponent();            
            radioFirstPriority1.IsChecked = true;
            _userId = Guid.Parse(_parentForm.cboUsers1.SelectedValue.ToString());
            
            this.DialogResult = DialogResult.None;
                                    
            cmbRequester1.ValueMember = "ID";
            cmbRequester1.DisplayMember = "Username";
            FilterDescriptor filterRequester = new FilterDescriptor();
            filterRequester.PropertyName = this.cmbRequester1.DisplayMember;
            filterRequester.Operator = FilterOperator.Contains;
            cmbRequester1.EditorControl.MasterTemplate.FilterDescriptors.Add(filterRequester);              
            cmbRequester1.DataSource = new BindingList<Users>(NewPrioritizer.usersList);
            cmbRequester1.MultiColumnComboBoxElement.Columns.ToList().ForEach(a => a.IsVisible = false);// ["sql"].IsVisible = false;
            cmbRequester1.MultiColumnComboBoxElement.Columns["userName"].IsVisible = true;
            cmbRequester1.MultiColumnComboBoxElement.Columns["userName"].Width = 150;

            cmbAssignedTo.ValueMember = "ID";
            cmbAssignedTo.DisplayMember = "Username";
            FilterDescriptor filterUsers = new FilterDescriptor();
            filterUsers.PropertyName = this.cmbAssignedTo.DisplayMember;
            filterUsers.Operator = FilterOperator.Contains;
            cmbAssignedTo.EditorControl.MasterTemplate.FilterDescriptors.Add(filterUsers);  
            cmbAssignedTo.DataSource = NewPrioritizer.usersList;
            cmbAssignedTo.MultiColumnComboBoxElement.Columns.ToList().ForEach(a => a.IsVisible = false);// ["sql"].IsVisible = false;
            cmbAssignedTo.MultiColumnComboBoxElement.Columns["userName"].IsVisible = true;
            cmbAssignedTo.MultiColumnComboBoxElement.Columns["userName"].Width = 150;
            cmbAssignedTo.SelectedValue = task.userID;
            
            cmbProject1.ValueMember = "ID";
            cmbProject1.DisplayMember = "projectName";
            cmbProject1.DataSource = NewPrioritizer.ProjectList;

            if (_task.taskName != null)
                name.Text = _task.taskName;
            if (_task.actualWorkHours != null)
                txtActualWork1.Text = _task.actualWorkHours.Value.ToString();
            if (_task.completionPercentage != null)
                txtCompletedPercent1.Text = _task.completionPercentage.Value.ToString();
            if (_task.defectNumber != null)
                txtDefectNum1.Text = _task.defectNumber;
            if (_task.estimatedWorkHours != null)
                txtEstimatedHours1.Text = _task.estimatedWorkHours.Value.ToString();
            if (_task.dueDate != null)
                dtmDueDate1.Value = _task.dueDate.Value;           

            cmbProject1.SelectedValue = null;
            if (_task.projectID != null)
                cmbProject1.SelectedValue =  _task.projectID.Value ;
            //cmbRequester1.SelectedValue = null;
            cmbAssignedTo.SelectedValue = null;
            if (_task.requesterID != null && _task.requesterID != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                cmbRequester1.SelectedValue = _task.requesterID.Value;
            else
                cmbRequester1.SelectedValue = NewPrioritizer.loggedInUserID;

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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_parentForm.isMeetingTasksMode) 
            {                
                if (_taskType == enTaskType.Decision)
                    _task.userID = null;
                else
                {
                    if (cmbAssignedTo.SelectedValue != null)
                        _task.userID = Guid.Parse(cmbAssignedTo.SelectedValue.ToString());
                    else
                        _task.userID = _userId;
                }

                Guid meetingID = Guid.Parse(_parentForm.SelectedMeeting.ToString());
                if (_mode == formMode.add)
                {
                    _parentForm.assignTaskToMeeting(_task, meetingID);
                }
            }
            else
            {
                _task.userID = _userId;                
            }


            _task.taskStatusID = _task.taskStatusID ?? 1;//pending

            _task.dateClosed = null;
            _task.dateEntered = _task.dateEntered ?? DateTime.Now;

            //update task with form values
            _task.remarks = txtRemarks.Text;
            _task.taskName = name.Text ;

            _task.defectNumber = txtDefectNum1.Text;

            if(dtmDueDate1.Value.ToString() != "12/31/1760 12:00:00 AM")
                _task.dueDate = dtmDueDate1.Value;
            
            if(txtActualWork1.Text.Length>0) 
                _task.actualWorkHours = Convert.ToInt16(txtActualWork1.Text);
            if (txtCompletedPercent1.Text.Length>0)
                _task.completionPercentage = Convert.ToInt16(txtCompletedPercent1.Text);            
            if (txtEstimatedHours1.Text.Length>0)
                _task.estimatedWorkHours = Convert.ToInt16(txtEstimatedHours1.Text);             
            if (cmbProject1.SelectedValue != null)
                _task.projectID = Guid.Parse(cmbProject1.SelectedValue.ToString());
            if (cmbRequester1.SelectedValue != null)
                _task.requesterID = Guid.Parse(cmbRequester1.SelectedValue.ToString());
            
          
            //chkUpdateRequester1.Checked = _task.updateRequester ?? false;
            _task.projectID = Guid.Parse(cmbProject1.SelectedValue.ToString());
            _task.requesterID = Guid.Parse(cmbRequester1.SelectedValue.ToString());
                
            if (_mode == formMode.add)
            {
                _task.UpdatesLog = _parentForm.getLogDelimiterLine(logDelimiterMode.Added); 
            
                _task = NewPrioritizer.ProxyClient.addTask(_task, NewPrioritizer.loggedInUserID);
                if (radioFirstPriority1.IsChecked)
                    _parentForm.SetTopPriority(_task.ID);
            }
            else if (_mode == formMode.update)
            {
                //_parentForm.logChanges(_task);
                _task = NewPrioritizer.ProxyClient.applyChangesTasks(_task, NewPrioritizer.loggedInUserID);                
                //_task.ChangeTracker.AcceptChanges();
                //NewPrioritizer.repository.SaveChanges();                                
            }
       
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //_task.projectID = 2;
        }

        private void dtmDueDate1_Opened(object sender, EventArgs e)
        {
            dtmDueDate1.Value = DateTime.Now;
        }

        private void cmbAssignedTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
                
    }
}
