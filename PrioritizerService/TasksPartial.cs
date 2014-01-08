using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrioritizerService.Model
{
    public partial class Tasks 
    {
        public string MeetingName
        {
            get
            {
                string meetingNames = "";
                if (this.MeetingTasks != null && this.MeetingTasks.Count() > 0 && this.MeetingTasks[0].Meetings != null)
                {
                    this.MeetingTasks.ToList().ForEach(a => meetingNames += a.Meetings.MeetingName + " ,");
                    if (meetingNames.EndsWith(","))
                        meetingNames = meetingNames.Substring(0, meetingNames.Length - 1);
                    return meetingNames;
                }
                //return this.MeetingTasks[0].Meetings.MeetingName.ToString();


                return string.Empty;

            }
        }
    }

}