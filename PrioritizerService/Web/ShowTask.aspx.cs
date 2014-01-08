using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Prioritizer.Shared.Model;
using Ext.Net;
using Ext.Net.Utilities;
using System.Globalization;

namespace PrioritizerService
{
    public partial class ShowTask : System.Web.UI.Page
    {

        private Online.PrioritizerService _prioritizerService = new Online.PrioritizerService();
        public Tasks _requestedTask = null;
        public string kk;
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid taskID;
            if (Guid.TryParse(Request.QueryString["id"], out taskID))
            {
                _requestedTask =  _prioritizerService.getTaskByID(taskID);
                if (_requestedTask != null)
                {
//                    ControlUtils.FindControl<
                    TaskName.Value = _requestedTask.taskName;
                    EstimatedHours.Value = _requestedTask.estimatedWorkHours;
                    RequestedBy.Value = _prioritizerService.UsersDict[_requestedTask.requesterID.Value].userName;
                    CompletionPercentage.Value = _requestedTask.completionPercentage;
                    if (_requestedTask.projectID.HasValue && _prioritizerService.ProjectsDict.ContainsKey(_requestedTask.projectID.Value))
                        Project.Value = _prioritizerService.ProjectsDict[_requestedTask.projectID.Value];
                    Defect.Value = _requestedTask.defectNumber;
                    ActualWork.Value = _requestedTask.actualWorkHours;
                    if (_requestedTask.dueDate.HasValue)
                        DueDate.Value = _requestedTask.dueDate.Value.ToShortDateString();
                    Body.Value = _requestedTask.remarks;
                }
                else
                {
                    taskNotFoundNotification();
                }
            }
            else
            {
                taskNotFoundNotification();
            }
        }

        private static void taskNotFoundNotification()
        {
            X.Msg.Alert("Task not found", "Requested Task not Found", new JFunction { Fn = "showNotification" }).Show();
        }


    }
}