using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Web;
//using Netformx.Online.Services.PrioritizerService.Contracts.Data;
//using Netformx.Online.Foundation.SelfTrackingEntities;
using PrioritizerService.Model;

//namespace Netformx.Online.Services.PrioritizerService.Contracts.Data
namespace Prioritizer2._0
{
    //public partial class Tasks
    //{
    //    public string MeetingName
    //    {
    //        get
    //        {
    //            string meetingNames = "";
    //            if (this.MeetingTasks != null && this.MeetingTasks.Count() > 0 && this.MeetingTasks[0].Meetings != null)
    //            {
    //                this.MeetingTasks.ToList().ForEach(a => meetingNames += a.Meetings.MeetingName + " ,");
    //                if (meetingNames.EndsWith(","))
    //                    meetingNames = meetingNames.Substring(0, meetingNames.Length - 1);
    //                return meetingNames;
    //            }
    //            //return this.MeetingTasks[0].Meetings.MeetingName.ToString();


    //            return string.Empty;

    //        }
    //    }
    //}

    //public partial class Meetings
    //{

    //    public int MeetingCategoryID
    //    {
    //        get
    //        {
    //            if (this.MeetingCategoryMap != null && this.MeetingCategoryMap.Count() > 0 /*&& this.MeetingCategoryMap[0].MeetingCategory != null*/)
    //                return this.MeetingCategoryMap[0].MeetingCategoryID.Value;

    //            return -1;

    //        }
    //        set
    //        {
    //            this.StartTracking();
    //            if (MeetingCategoryMap.Count > 0)
    //            {
    //                MeetingCategoryMap[0].StartTracking();

    //                if (value == -1)
    //                {
    //                    //MeetingCategoryMap[0].MarkAsDeleted();
    //                    NewPrioritizer.repository.MeetingCategoryMap.DeleteObject(MeetingCategoryMap[0]);
    //                    //MeetingCategoryMap.Remove(MeetingCategoryMap[0]);
    //                }
    //                else
    //                    MeetingCategoryMap[0].MeetingCategoryID = value;
    //            }
    //            else
    //            {
    //                MeetingCategoryMap mcm = new MeetingCategoryMap();
    //                mcm.StartTracking();
    //                mcm.MeetingCategoryID = value;
    //                mcm.MeetingID = this.ID;
    //                this.MeetingCategoryMap.Add(mcm);
    //            }
    //            //this.ChangeTracker.State = ObjectState.Modified;

    //        }
    //    }

    //}
    
    
    static class Util
    {
        public static Tasks Clone(this Tasks Entity)
        {
            //var Type = Entity.GetType();
            //var Clone = Activator.CreateInstance(Type);
            Tasks Clone = new Tasks();

            /*foreach (var Property in Type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.SetProperty))
            {
                if (Property.PropertyType.IsGenericType && Property.PropertyType.GetGenericTypeDefinition() == typeof(EntityReference<>)) break;
                if (Property.PropertyType.IsGenericType && Property.PropertyType.GetGenericTypeDefinition() == typeof(EntityCollection<>)) break;
                if (Property.PropertyType.IsSubclassOf(typeof(EntityObject))) break;

                if (Property.CanWrite && Property.Name != "ID")
                {
                    Property.SetValue(Clone, Property.GetValue(Entity, null), null);
                }
            }*/
            Clone.priority = Entity.priority;
            Clone.projectID = Entity.projectID;
            Clone.remarks = Entity.remarks;
            Clone.requesterID = Entity.requesterID;
            Clone.taskName = Entity.taskName;
            Clone.dateEntered = DateTime.Now;
            Clone.taskStatusID = 1;
            Clone.userID = Entity.userID;
            Clone.updateRequester = Entity.updateRequester;
            Clone.dueDate = Entity.dueDate;


            var sourceTaskMeeting = NewPrioritizer.ProxyClient.getMeetingTaskByID(Entity.ID);
            if (sourceTaskMeeting != null)
            {
                MeetingTasks meetingTask = new MeetingTasks();
                meetingTask.StartTracking();
                meetingTask.MeetingID = sourceTaskMeeting.MeetingID;
                //repository.MeetingTasks.AddObject(meetingTask);
                meetingTask.TaskID = Clone.ID;
                Clone.MeetingTasks.Add(meetingTask);
                //repository.MeetingTasks.AddObject(meetingTask);
            }


            return (Tasks)Clone;
        }

        private const string debugSeperator =
    "-------------------------------------------------------------------------------";

        public static IQueryable<T> TraceQuery<T>(IQueryable<T> query)
        {
            if (query != null)
            {
                ObjectQuery<T> objectQuery = query as ObjectQuery<T>;
                if (objectQuery != null /*&& Boolean.Parse(ConfigurationManager.AppSettings["Debugging"])*/)
                {
                    StringBuilder queryString = new StringBuilder();
                    queryString.Append(Environment.NewLine)
                        .AppendLine(debugSeperator)
                        .AppendLine("QUERY GENERATED...")
                        .AppendLine(debugSeperator)
                        .AppendLine(objectQuery.ToTraceString())
                        .AppendLine(debugSeperator)
                        .AppendLine(debugSeperator)
                        .AppendLine("PARAMETERS...")
                        .AppendLine(debugSeperator);
                    foreach (ObjectParameter parameter in objectQuery.Parameters)
                    {
                        queryString.Append(String.Format("{0}({1}) \t- {2}", parameter.Name, parameter.ParameterType, parameter.Value)).Append(Environment.NewLine);
                    }
                    queryString.AppendLine(debugSeperator).Append(Environment.NewLine);
                    Console.WriteLine(queryString);
                    Trace.WriteLine(queryString);
                }
            }
            return query;
        }
    }


    public static class SimplerAES
    {
        private static byte[] key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private static byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156 };
        private static ICryptoTransform encryptor, decryptor;
        private static UTF8Encoding encoder;

        static SimplerAES()
        {
            RijndaelManaged rm = new RijndaelManaged();
            encryptor = rm.CreateEncryptor(key, vector);
            decryptor = rm.CreateDecryptor(key, vector);
            encoder = new UTF8Encoding();
        }

        public static string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)));
        }

        public static string Decrypt(string encrypted)
        {
            return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        public static string EncryptToUrl(string unencrypted)
        {
            return HttpUtility.UrlEncode(Encrypt(unencrypted));
        }

        public static string DecryptFromUrl(string encrypted)
        {
            return Decrypt(HttpUtility.UrlDecode(encrypted));
        }

        public static byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, encryptor);
        }

        public static byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, decryptor);
        }

        private static byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            MemoryStream stream = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
    }
}
