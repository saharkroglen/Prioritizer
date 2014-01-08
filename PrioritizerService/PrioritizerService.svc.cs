using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PrioritizerService.Model;
using System.Reflection;
using System.Data.Objects;
using System.Configuration;
using PrioritizerService;
using System.IO;
using System.Web.Security;
using System.Web.Hosting;
using Prioritizer.Shared.Model;
using Prioritizer.Shared;
using Shared;
using PrioritizerService.Class;
namespace Online
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,InstanceContextMode = InstanceContextMode.PerSession)]
    public class PrioritizerService : IPrioritizerService
    {
        public static readonly Guid MEETINGS_FILTER_NO_CATEGORY = Guid.Parse("00000000-0000-0000-0000-000000000002");
        public static readonly Guid MEETINGS_FILTER_ALL_MEETINGS = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static readonly string CLIENT_INSTALLATION_FOLDER = "ClientInstallation";
        public static readonly string CLIENT_INSTALLATION_FILE_NAME = "PrioriSetup.msi";
        
        #region CTOR
        public PrioritizerService()
        {            
            loadUserssDictionary(false);            
        }
        #endregion
        
        #region WCF contracts

        public ClientPackage getLatestClient()
        {
            MemoryStream stream = new MemoryStream(); 
            ClientPackage package = new ClientPackage();
            package.bin = File.ReadAllBytes(string.Format(@"{0}\{1}\{2}", HostingEnvironment.ApplicationPhysicalPath, CLIENT_INSTALLATION_FOLDER, CLIENT_INSTALLATION_FILE_NAME));
            package.binName = CLIENT_INSTALLATION_FILE_NAME;
            return package;
        }

      

        public string  HelloWorld(int input)
        {
            //using (PrioritizerUnitOfWork uow = UnitOfWork)
            //{
            //    var x = uow.Context.Users.Where(t => t.ID == input).FirstOrDefault();
            //    return x.userName;
            //}
            return "Hello";
        }

        public List<Alerts> getAlertsForUser(Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                var alertList = uow.Context.Alerts.Include("Tasks").Where(a => a.Tasks.userID == userID && a.active.Value && a.Tasks.taskStatusID <= 3 ).ToList();
                return alertList;
            }
        }

        public List<Alerts> getAlertForTask(Guid taskID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.Alerts.Where(s => s.taskID == taskID).ToList();
            }
        }

        public void applyChangesAlerts(Alerts a, Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                if (a.ChangeTracker.State == ObjectState.Added)
                {
                    uow.Context.Alerts.AddObject(a);
                    Tasks task = uow.Context.Tasks.Where(t => t.ID == a.taskID).FirstOrDefault();
                    task.StartTracking();
                    task.hasAlert = true;
                    applyChangesTasks(task, userID, uow);
                    uow.Commit();
                }
                else if (a.ChangeTracker.ChangedProperties.Count > 0 || a.ChangeTracker.State != ObjectState.Modified)
                {
                    uow.Context.Alerts.ApplyChanges(a);
                    uow.Commit();
                }
            }
        }
        
        public void deleteAlert(Alerts a)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.Alerts.DeleteObject(a);
                uow.Commit();
            }
        }

        public List<Users> getUsersList(Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                tenantID = handleTenantID(tenantID);
                var userList = uow.Context.Users.Include("Tenant").Where(u => u.TenantID == tenantID.Value).OrderBy(p => p.userName).ToList();
                return userList;
            }
        }

        public void deleteMeetingCategory(MeetingCategory mc)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.AttachTo("MeetingCategory", mc);
                uow.Context.MeetingCategory.DeleteObject(mc);
                uow.Commit();
            }
        }

        public void applyChangesMeetingCategory(MeetingCategory mc, Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                tenantID = handleTenantID(tenantID);
                if (mc.ChangeTracker.State == ObjectState.Added)
                    mc.TenantID = tenantID.Value;

                uow.Context.MeetingCategory.ApplyChanges(mc);
                uow.Commit();
            }
        }

        public void deleteMeetings(Meetings m)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.AttachTo("Meetings", m);
                uow.Context.Meetings.DeleteObject(m);
                uow.Commit();
            }
        }

        public void applyChangesMeetings(Meetings m, Guid? tenantID)
        {
            try
            {
                using (PrioritizerUnitOfWork uow = UnitOfWork)
                {
                    tenantID = handleTenantID(tenantID);
                    if (m.TenantID == null)
                        m.TenantID = tenantID.Value;
                    if (m.ChangeTracker.State == ObjectState.Added)
                    {
                        uow.Context.Meetings.AddObject(m);
                        uow.Commit();
                    }
                    else
                        if (m.ChangeTracker.ChangedProperties.Count > 0 || m.ChangeTracker.State != ObjectState.Modified)
                        {
                            uow.Context.Meetings.ApplyChanges(m);
                            uow.Commit();
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void deleteTasks(Tasks t)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.Tasks.DeleteObject(t);
                uow.Commit();
            }
        }

        //private void applyChangesAndSave<T>(ObjectSet<T> os,ref T entity, int userID, PrioritizerUnitOfWork uow) where T : IObjectWithChangeTracker
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("objectSet");
        //    }

        //    if (uow == null)
        //        uow = new PrioritizerUnitOfWork(os.Context as prioritizerDBEntities);

            
        //    os.Context.ApplyChanges<T>(os.EntityContainer.Name + "." + os.Name, entity);

        //    uow.Context.SaveChanges();
        //}

        public Tasks addTask(Tasks t, Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                NormalizeTaskName(t);
                uow.Context.Tasks.AddObject(t);
                t.dateUpdated = DateTime.Now;
                applyChangesTasks(t, userID, uow);
                return t;
            }
        }

        public List<TaskSaveFailure> applyChangesTasksList(List<Tasks> changedTasksList, Guid userID)
        { 
            //using (PrioritizerUnitOfWork uow = UnitOfWork)
            //{
            List<TaskSaveFailure> tasksWhichFailedToSave = new List<TaskSaveFailure>();
                 
                foreach (Tasks t in changedTasksList)
                {
                    try
                    {
                        applyChangesTasks(t, userID, null);
                        //applyChangesTasks(t, userID, uow);
                    }
                    catch (Exception ex)
                    {
                        tasksWhichFailedToSave.Add(new TaskSaveFailure() { Task = t, ExceptionMessage = ex.Message, ExceptionType =  ex.GetType().ToString() });
                    }
                }
                return tasksWhichFailedToSave;

            //}
        }

        public void applyChangesMeetingTasksList(List<MeetingTasks> changedMeetingTasksList)
        {

            foreach (MeetingTasks mt in changedMeetingTasksList)
            {
                using (PrioritizerUnitOfWork uow = UnitOfWork)
                {
                    validatePrivateTaskInMeeting(mt);

                    uow.Context.MeetingTasks.ApplyChanges(mt);
                    uow.Commit();
                }
            }
        }

        private static void validatePrivateTaskInMeeting(MeetingTasks mt)
        {
            if (mt.Tasks != null && mt.Tasks.privateTask)
                throw new Exception(string.Format("Private task can't be linked to meeting"));
                //throw new PrioritizerExceptionPrivateTaskInMeeting(mt.Tasks, null);
        }
        private static void validatePrivateTaskInMeeting(Tasks t)
        {
            if (t.privateTask && t.MeetingTasks.FirstOrDefault()!= null)
                throw new Exception(string.Format("Private task can't be linked to meeting"));
                //throw new PrioritizerExceptionPrivateTaskInMeeting(t, null);
        }

        public Tasks applyChangesTasks(Tasks t, Guid userID)
        {
            return applyChangesTasks(t, userID, null);

        }


        public ClientMessage ping(Guid userID, DateTime lastUpdate)
        {
            if (!ServerUtils.connectedUsers.ContainsKey(userID))
            {
                ServerUtils.connectedUsers.Add(userID, DateTime.UtcNow);
            }
            else
                ServerUtils.connectedUsers[userID] = DateTime.UtcNow;


            return ServerMessagesManager.Instance.GetMessages(userID,lastUpdate);            
        }

        public void ClientMessageReceiveConfirmation(Guid userID)
        {
            ServerMessagesManager.Instance.ClearClientMessage(userID);
        }

        public void Poke(Poke p)
        {
            ServerMessagesManager.Instance.Poke(p);
        }

        public void deleteMeetingTasks(MeetingTasks mt)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.MeetingTasks.DeleteObject(mt);
                uow.Commit();
            }
        }

        public void applyChangesMeetingTasks(MeetingTasks mt)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                validatePrivateTaskInMeeting(mt);
                uow.Context.MeetingTasks.ApplyChanges(mt);
                uow.Commit();
            }
        }

        public void deleteMeetingCategoryMap(MeetingCategoryMap mcm)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.MeetingCategoryMap.DeleteObject(mcm);
                uow.Commit();
            }
        }

        public void applyChangesMeetingCategoryMap(MeetingCategoryMap mcm)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.MeetingCategoryMap.ApplyChanges(mcm);
                uow.Commit();
            }
        }
        
        public void deleteProjects(projects p)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.AttachTo("projects", p);
                uow.Context.projects.DeleteObject(p);
                uow.Commit();
            }
        }

        public void applyChangesProjects(projects p, Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                tenantID = handleTenantID(tenantID);
                bool projectAdded = false;
                if (p.ChangeTracker.State == ObjectState.Added)
                {
                    projectAdded = true;
                }
                p.TenantID = tenantID.Value;
                uow.Context.projects.ApplyChanges(p);
                uow.Commit();

                if (projectAdded)
                    loadProjectList();
            }
        }
        
        public void deleteUsers(Users u)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.AttachTo("Users", u);
                uow.Context.Users.DeleteObject(u);
                uow.Commit();
            }
        }

        public Users applyChangesUsers(Users u,Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                bool userAdded = false;
                if (u.ChangeTracker.State == ObjectState.Added)
                {
                    userAdded = true;

                    sendInvitationMailInternal(u, userID);                         

                }
                if (u.domainUserName == null)
                {
                    u.domainUserName = u.userName;
                }
                uow.Context.Users.ApplyChanges(u);
                uow.Commit();

                if (userAdded)
                    loadUserssDictionary(true);

                return u;
            }
        }

        public void sendInvitationMail(Users u, Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                sendInvitationMailInternal(u, userID);
                uow.Context.Users.ApplyChanges(u);
                uow.Commit();
            }
        }

        public static string CLIENT_DOWNLOAD_URL = "http://54.235.151.96/prioritizerservice/index.aspx";
        private void sendInvitationMailInternal(Users u, Guid userID)
        {
            Email newUserInvitation = new Email();
            newUserInvitation.To = u.email;
            newUserInvitation.From = UsersDict[userID].email;
            newUserInvitation.subject = string.Format("You have been invited to use 'Priori - Task and Meeting Management' by {0}", UsersDict[userID].userName);
            StringBuilder sb = new StringBuilder();
            string temporaryPassword = Membership.GeneratePassword(6, 2);
            u.password = Prioritizer.Shared.Utils.EncodePassword(temporaryPassword);
            u.TemporaryPassword = true;

            sb.Append(string.Format("Hi {0}{1}", u.email, "<br>"));
            sb.Append(string.Format("Please join my task and meeting management platform so we can share, collaborate and manage our mutual interests.{0}{0}", "<br>"));
            sb.Append(string.Format("<a href='{0}'>Download Priori client application for windows.</a>{1}", CLIENT_DOWNLOAD_URL, "<br>"));
            sb.Append(string.Format("Your user name is: {0}{1}", u.userName, "<br>"));
            sb.Append(string.Format("Your initial password is: {0}{1}", temporaryPassword, "<br>"));
            sb.Append(string.Format("Belongs to Network: {0}{1}", UsersDict[userID].Tenant.TenantName, "<br>"));

            newUserInvitation.Body = sb.ToString();
            EmailManager.Enqueue(newUserInvitation);
        }

        public void deleteManagerTeamMemberRelations(ManagerTeamMemberRelations mtr)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.AttachTo("ManagerTeamMemberRelations", mtr);
                uow.Context.DeleteObject(mtr);
                uow.Commit();

            }
        }

        public void applyChangesManagerTeamMemberRelations(ManagerTeamMemberRelations mtr)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.ManagerTeamMemberRelations.ApplyChanges(mtr);
                uow.Commit();
            }
        }
        
        public void deleteMeetingAttendies(MeetingAttendies ma)
        {
            try
            {
                using (PrioritizerUnitOfWork uow = UnitOfWork)
                {
                    //var attendee = uow.Context.MeetingAttendies.Where(a => a.ID == ma.ID).FirstOrDefault();
                    uow.Context.AttachTo("MeetingAttendies", ma);
                    uow.Context.MeetingAttendies.DeleteObject(ma);
                    uow.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void applyChangesMeetingAttendies(MeetingAttendies ma)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.MeetingAttendies.ApplyChanges(ma);
                uow.Commit();
            }
        }

        public void deleteAttachment(attachments a, Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                Guid taskId = a.taskID.Value;
                uow.Context.AttachTo("attachments", a);
                uow.Context.DeleteObject(a);

                uow.Commit();
                if (uow.Context.attachments.Where(attach => attach.taskID == taskId).Count() == 0)
                {
                    Tasks task = uow.Context.Tasks.Where(t => t.ID == taskId).FirstOrDefault();
                    task.hasAttachment = false;
                    applyChangesTasks(task, userID);
                }
                uow.Commit();
            }
        }

        public void applyChangesAttachments(attachments a)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                uow.Context.attachments.ApplyChanges(a);
                uow.Commit();
            }
        }

        public void addAttachment(attachments a, Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                try
                {
                    uow.Context.attachments.AddObject(a);

                    Tasks task = getTaskByID(a.taskID.Value);
                    task.StartTracking();
                    task.hasAttachment = true;
                    applyChangesTasks(task, userID,null);
                    uow.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<attachments> getAttachmentsForTask(Guid taskID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.attachments.Where(s => s.taskID == taskID).ToList();
            }
        }
        
        public IEnumerable<Users> getUsers(Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                try
                {
                    tenantID = handleTenantID(tenantID);
                    //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
                    return uow.Context.Users.Include("Tenant").Where(u => u.TenantID == tenantID.Value).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void moveAllTasksPriorityForUser(int numOfSteps, Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                moveAllTasksPriorityForUserInternal(numOfSteps, userID, uow,true);
            }
        }

        public void moveAllTasksPriorityForUserInternal(int numOfSteps, Guid userID, PrioritizerUnitOfWork uow,bool commitOnFinish)
        {
            if (uow == null)            
                uow = UnitOfWork;

            uow.Context.ExecuteStoreCommand("update Tasks set priority=priority+{0} where userid={1}", numOfSteps, userID);
            if (commitOnFinish)
                uow.Commit();
        }
       
        public Tasks getTaskByID(Guid taskID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.Tasks.Where(task => task.ID == taskID).FirstOrDefault();
            }
        }

        public Users getUserByID(Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.Users.Where(u => u.ID == userID).FirstOrDefault();
            }
        }

        public void unlockUser(Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                Users user =  uow.Context.Users.Where(u => u.ID == userID).FirstOrDefault();
                user.Activated = true;
                applyChangesUsers(user, Guid.Empty);
                uow.Commit();
            }
        }

        public IEnumerable<Users> getUserByDomainName(string domainName, Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                tenantID = handleTenantID(tenantID);
                return uow.Context.Users.Include("Tenant").Where(u => u.domainUserName == domainName &&  u.TenantID == tenantID.Value).ToList();
            }
        }
        public IEnumerable<Users> getUserByName(string name, Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                tenantID = handleTenantID(tenantID);
                return uow.Context.Users.Include("Tenant").Where(u => u.userName == name && u.TenantID == tenantID.Value).ToList();
            }
        }
        public IEnumerable<Users> getUserByEmail(string email, Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                tenantID = handleTenantID(tenantID);
                return uow.Context.Users.Include("Tenant").Where(u => u.email == email && u.TenantID == tenantID.Value).ToList();
            }
        }

        public IEnumerable<Users> findUserByName(string nameToMatch)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.Users.Include("Tenant").Where(u => u.userName.Contains(nameToMatch)).ToList();
            }
        }

        

        public MeetingTasks getMeetingTaskByID(Guid taskID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return getMeetingTasksByID(taskID).FirstOrDefault();
            }
        }

        public IEnumerable<MeetingTasks> getMeetingTasksByID(Guid taskID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                try
                {
                    return uow.Context.MeetingTasks.Where(a => a.TaskID == taskID).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Instance.Fatal("Failed retrieving meeting tasks", ex);
                    return null;
                }
            }
        }

        public MeetingCategoryMap getMeetingCategoryMapByID(Guid meetingID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.MeetingCategoryMap.Where(a => a.MeetingID == meetingID).FirstOrDefault();
            }
        }

        public Meetings getMeetingByID(Guid meetingID, bool includeTasks)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                if (includeTasks)
                    return uow.Context.Meetings.Include("MeetingTasks").Include("MeetingTasks.Tasks").Where(t => t.ID == meetingID).FirstOrDefault();
                else
                    return uow.Context.Meetings.Where(t => t.ID == meetingID).FirstOrDefault();
            }
        }

        public List<Tasks> getTasksForMeeting(Guid meetingID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
               /*var meeting = uow.Context.Meetings.Include("MeetingTasks").Include("MeetingTasks.Tasks").Where(t => t.ID == meetingID).FirstOrDefault();
               return meeting.MeetingTasks.Select(a => a.Tasks).ToList();*/
                return uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => t.MeetingTasks.Any(c => c.MeetingID == meetingID)).ToList();
            }
        }
        
        public Meetings getMeetingByName(string meetingName)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.Meetings.Where(a => a.MeetingName == meetingName).First();
            }
        }

        public IEnumerable<ManagerTeamMemberRelations> findAllTeamMembersAllowedForUser(Guid userID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                var a = (from i in uow.Context.ManagerTeamMemberRelations.Include("TeamMemberID_UserID")
                         where i.ManagerID == userID
                         orderby i.TeamMemberID_UserID.userName
                         select i).ToList();
                return a;
            }
        }

        public IEnumerable<MeetingCategory> getMeetingCategoryList(bool filterCategoriesOnlyForLoggedInUser, Guid userID, Guid tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                if (filterCategoriesOnlyForLoggedInUser)
                    return uow.Context.MeetingCategory.Where(mc => (mc.CategoryOwner == userID) && (mc.TenantID == tenantID)).OrderBy(p => p.CategoryName).ToList();
                else
                    return uow.Context.MeetingCategory.Where(
                        mc => (mc.TenantID == tenantID &&
                                (
                                    (mc.CategoryOwner == userID ) || 
                                    mc.MeetingCategoryMap.Any(a => a.Meetings.MeetingAttendies.Any(b => b.AttendeeID == userID)) ||
                                    mc.MeetingCategoryMap.Any(a => a.Meetings.MeetingTasks.Any(b => b.Tasks.userID == userID)) 
                                )
                              )
                    ).OrderBy(p => p.CategoryName).ToList();
            }
        }

        public Users Authenticate(string username, string password, string company)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                //string md5Password = Prioritizer.Shared.Utils.EncodePassword(password);
                var user = uow.Context.Users.Include("Tenant").Where(u => u.userName == username && u.password == password && u.Tenant.TenantName == company).FirstOrDefault();
                return user;
            }
        }
        
        public IEnumerable<Meetings> getMeetingList(Guid meetingCategory, Guid userID, bool includeFinished, Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                tenantID = handleTenantID(tenantID);
                if (meetingCategory == MEETINGS_FILTER_ALL_MEETINGS) //show all meetings which answer the following criteria: meetings owned by logged in user OR meetings that logged in user attended to OR meetings that logged in user have tasks assigned to him which were originated by that meeting
                {
                    if (includeFinished)// show all meetings even those which all their action items are finished/cancelled
                        return uow.Context.Meetings.Where(m => (m.TenantID == tenantID.Value) && (m.MeetingOwner == userID || m.MeetingAttendies.Any(b => b.AttendeeID == userID) /*|| m.MeetingTasks.Any(c => c.Tasks.userID == userID)*/)).OrderByDescending(p => p.MeetingDate).ToList();
                    else //show only meetings which one of their task is still pending/hold/progress
                        return uow.Context.Meetings.Where(m => (m.TenantID == tenantID.Value) && ((m.MeetingTasks.Any(a => a.Tasks.taskStatusID <= 3) || !m.MeetingTasks.Any()) && (m.MeetingOwner == userID || m.MeetingAttendies.Any(b => b.AttendeeID == userID) /*|| m.MeetingTasks.Any(c => c.Tasks.userID == userID)*/))).OrderByDescending(p => p.MeetingDate).ToList();

                }
                if (meetingCategory == MEETINGS_FILTER_NO_CATEGORY) // show all meetings which answer same criteria of "meetingCategory == -1" + only those meetings that are not assigned to any meeting category
                {
                    if (includeFinished)// show all meetings even those which all their action items are finished/cancelled
                        return uow.Context.Meetings.Where(m => (m.TenantID == tenantID.Value) && (!m.MeetingCategoryMap.Any() && ((m.MeetingOwner == userID) || m.MeetingAttendies.Any(b => b.AttendeeID == userID) /*|| m.MeetingTasks.Any(c => c.Tasks.userID == userID)*/))).OrderByDescending(p => p.MeetingDate).ToList();//meetings which belong to no meeting category
                    else //show only meetings which one of their task is still pending/hold/progress
                        return uow.Context.Meetings.Where(m => (m.TenantID == tenantID.Value) && ((m.MeetingTasks.Any(a => a.Tasks.taskStatusID <= 3) || !m.MeetingTasks.Any()) && !m.MeetingCategoryMap.Any() && ((m.MeetingOwner == userID) || m.MeetingAttendies.Any(b => b.AttendeeID == userID) /*|| m.MeetingTasks.Any(c => c.Tasks.userID == userID)*/))).OrderByDescending(p => p.MeetingDate).ToList();//meetings which belong to no meeting category
                }
                else if (meetingCategory != MEETINGS_FILTER_ALL_MEETINGS && meetingCategory != MEETINGS_FILTER_NO_CATEGORY)  // show all meetings which answer same criteria of "meetingCategory == -1" but + only those meetings belong to specific meeting category
                {
                    if (includeFinished) // show all meetings even those which all their action items are finished/cancelled
                        return uow.Context.Meetings.Where(m => (m.TenantID == tenantID.Value) && (m.MeetingCategoryMap.Any(a => a.MeetingCategoryID == meetingCategory) && ((m.MeetingOwner == userID) || m.MeetingAttendies.Any(b => b.AttendeeID == userID) /*|| m.MeetingTasks.Any(c => c.Tasks.userID == userID)*/))).OrderByDescending(p => p.MeetingDate).ToList();//meetings filtered by meeting category
                    else //show only meetings which one of their task is still pending/hold/progress
                        return uow.Context.Meetings.Where(m => (m.TenantID == tenantID.Value) && ((m.MeetingTasks.Any(a => a.Tasks.taskStatusID <= 3) || !m.MeetingTasks.Any()) && m.MeetingCategoryMap.Any(a => a.MeetingCategoryID == meetingCategory) && ((m.MeetingOwner == userID) || m.MeetingAttendies.Any(b => b.AttendeeID == userID) /*|| m.MeetingTasks.Any(c => c.Tasks.userID == userID)*/))).OrderByDescending(p => p.MeetingDate).ToList();//meetings filtered by meeting category
                }

                return null;
            }
        }

        public IEnumerable<Tasks> getTasksByIDs(List<Guid> taskIDs)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => taskIDs.Contains(t.ID)).ToList();
            }
        }

        public IEnumerable<Tasks> getTasksForUser(bool includeFinishedTasks, bool includeCancelledTasks, Guid userID, Guid requestingUserID)
        {
            try
            {
                IEnumerable<Tasks> result = null;
                using (PrioritizerUnitOfWork uow = UnitOfWork)
                {
                    if (includeFinishedTasks && includeCancelledTasks)//include all
                    {
                        result =  uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => t.userID == userID).ToList();
                    }
                    else if (includeFinishedTasks && !includeCancelledTasks)//include only finished
                    {
                        result = uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => t.userID == userID && (t.taskStatusID <= 4 || t.taskStatusID == null)).ToList();
                    }
                    else if (!includeFinishedTasks && includeCancelledTasks)//include only cancelled
                    {
                        result = uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => t.userID == userID && (t.taskStatusID <= 3 || t.taskStatusID == 5 || t.taskStatusID == null)).ToList();
                    }
                    else if (!includeFinishedTasks && !includeCancelledTasks) //only pending/onhold/inprogress
                    {
                        result = uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => t.userID == userID && (t.taskStatusID <= 3 || t.taskStatusID == null)).ToList();
                    }

                    if (userID != requestingUserID)
                    {
                        result = result.Where(t => t.privateTask == false);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


         public IEnumerable<Tasks> getTasksForTeamMembers(bool includeFinishedTasks, bool includeCancelledTasks, Guid userID, Guid requestingUserID)
        {
            try
            {
                IEnumerable<Tasks> result = null;
                using (PrioritizerUnitOfWork uow = UnitOfWork)
                {
                    if (includeFinishedTasks && includeCancelledTasks)//include all
                    {
                        result = uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => (t.Users.UserID_TeamMemberID.Any(u => u.ManagerID == userID) || t.userID == requestingUserID)).ToList();
                    }
                    else if (includeFinishedTasks && !includeCancelledTasks)//include only finished
                    {
                        result = uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => (t.Users.UserID_TeamMemberID.Any(u=>u.ManagerID == userID) || t.userID == requestingUserID) && (t.taskStatusID <= 4 || t.taskStatusID == null)).ToList();
                    }
                    else if (!includeFinishedTasks && includeCancelledTasks)//include only cancelled
                    {
                        result = uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => (t.Users.UserID_TeamMemberID.Any(u => u.ManagerID == userID) || t.userID == requestingUserID) && (t.taskStatusID <= 3 || t.taskStatusID == 5 || t.taskStatusID == null)).ToList();
                    }
                    else if (!includeFinishedTasks && !includeCancelledTasks) //only pending/onhold/inprogress
                    {
                        result = uow.Context.Tasks.Include("MeetingTasks").Include("MeetingTasks.Meetings").Where(t => (t.Users.UserID_TeamMemberID.Any(u => u.ManagerID == userID) || t.userID == requestingUserID) && (t.taskStatusID <= 3 || t.taskStatusID == null)).ToList();
                    }

                    if (userID != requestingUserID)
                    {
                        result = result.Where(t => t.privateTask == false);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TaskStatus> getTaskStatusList()
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.TaskStatus.ToList();
            }
        }

        public IEnumerable<projects> getProjectList(Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                try
                {
                    tenantID = handleTenantID(tenantID);
                    return uow.Context.projects.Include("Tenant").Where(p => p.TenantID == tenantID.Value).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<ManagerTeamMemberRelations> getManagerTeamMemberRelationsList(Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                tenantID = handleTenantID(tenantID);
                return uow.Context.ManagerTeamMemberRelations.Include("Tenant").Where(u => u.TenantID == tenantID.Value).ToList();
            }
        }

        public IEnumerable<Meetings> getMeetingsForOwner(Guid ownerUserID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.Meetings.Include("MeetingCategoryMap").Where(mc => mc.MeetingOwner == ownerUserID).ToList();
            }
        }

        public bool relationExists(Guid managerID, Guid teamMemberID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.ManagerTeamMemberRelations.Where(t => t.ManagerID == managerID && t.TeamMemberID == teamMemberID).Count() > 0;
            }
        }

        public void forwardTo(Guid taskID, Guid targetUserID, Guid loggedInUserID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                
                    Tasks t = uow.Context.Tasks.Where(a => a.ID == taskID).FirstOrDefault();
                    t.StartTracking();

                    //log action
                    string loggedInUsername = UsersDict[loggedInUserID].userName;
                    string log = string.Format("{0}{1}{2} - Forward to '{3}' by '{7}'{4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, UsersDict[(Guid)targetUserID].userName, Environment.NewLine, "", Environment.NewLine, loggedInUsername);
                    t.UpdatesLog = log + t.UpdatesLog;

                    t.userID = targetUserID;
                    //logChanges(t, loggedInUserID);
                    applyChangesTasks(t, loggedInUserID, uow);
                    //t.AcceptChanges();
                    uow.Commit();
                
            }
        }

        public void copyTo(Guid taskID, Guid targetUserID, Guid loggedInUserID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                Tasks t = uow.Context.Tasks.Where(a => a.ID == taskID).FirstOrDefault();
                Tasks newTask = CloneTask(t, getMeetingTaskByID(taskID));

                newTask.StopTracking();
                newTask.dateUpdated  = DateTime.Now;
                newTask.dateEntered = DateTime.Now;
                
                newTask.userID = targetUserID;
                newTask.ChangeTracker.State = ObjectState.Added;
                newTask.CopiedFromTaskID = t.ID;

                //log action
                string loggedInUserName = UsersDict[loggedInUserID].userName;
                string log = string.Format("{0}{1}{2} - Copied to '{3}' by '{7}' {4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, UsersDict[(Guid)newTask.userID].userName, Environment.NewLine, "", Environment.NewLine, loggedInUserName);
                newTask.UpdatesLog = log + newTask.UpdatesLog;

                uow.Context.Tasks.AddObject(newTask);
                //logChanges(newTask, loggedInUserID);
                uow.Commit();
            }
        }

        public IEnumerable<MeetingAttendies> getMeetingAttendees(Guid meetingID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.MeetingAttendies.Where(t => t.MeetingID == meetingID).ToList();
            }
        }

        public IEnumerable<Users> getMeetingAttendeesUserList(Guid meetingID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                return uow.Context.Users.Where(u => u.MeetingAttendies.Any(a => a.MeetingID == meetingID)).ToList();
            }
        }

        #endregion
        
        #region general public WCF contracts

        public string getShowTaskURL()
        {            
            return  ConfigurationSettings.AppSettings["PrioritizerServerURL"] + ConfigurationSettings.AppSettings["PrioritizerServerShowTaskUrl"];
        }

        public string getServerVersion()
        {
            return ConfigurationSettings.AppSettings["currentServerVer"];
        }

        public string getClientVersion()
        {
            return ConfigurationSettings.AppSettings["currentClientVer"];
        }

        public bool DomainControllerAuthenticate()
        {
            return Boolean.Parse(ConfigurationSettings.AppSettings["DomainControllerAuthenticate"]);
        }

        #endregion

        #region dictionaries

        public static Dictionary<string, Guid> _domainUsersDict;
        public static Dictionary<Guid, string> _projectsDict;
        public static List<projects> _projectsList;
        public static Dictionary<int, string> _statusDict;
        private void loadTaskStatusDictionary()
        {
            _statusDict = new Dictionary<int, string>();
            foreach (var status in getTaskStatusList())
            {
                _statusDict.Add(status.ID, status.StatusName);
            }
        }
        private void loadProjectsDictionary()
        {
            _projectsDict = new Dictionary<Guid, string>();
            foreach (var project in _projectsList)
            {
                _projectsDict.Add(project.ID, project.projectName);
            }
        }

        private void loadUserssDictionary(bool forceReload)
        {
            if (ServerUtils._usersDict == null || forceReload)
            {
                ServerUtils._usersDict = new Dictionary<Guid, Users>();
                _domainUsersDict = new Dictionary<string, Guid>();
                foreach (var user in getUsers())
                {
                    if (user.domainUserName == null) continue; //ignore domain user names which are null. add enforcement in DAL as well.
                    ServerUtils._usersDict.Add(user.ID, user);
                    _domainUsersDict.Add(user.DomainUserKey(), user.ID);
                }
            }
        }

        private void loadProjectList()
        {
            _projectsList = getProjectList().OrderBy(p => p.projectName).ToList();

            if (_projectsDict != null)
                _projectsDict.Clear();
            _projectsDict = null;
            loadProjectsDictionary();
        }

        public Dictionary<Guid, Users> UsersDict
        {
            get
            {
                if (ServerUtils._usersDict == null)
                    loadUserssDictionary(false);

                return ServerUtils._usersDict;
            }
        }

        public Dictionary<int, string> StatusDict
        {
            get
            {
                if (_statusDict == null)
                    loadTaskStatusDictionary();

                return _statusDict;
            }
        }

        public Dictionary<Guid, string> ProjectsDict
        {
            get
            {
                if (_projectsDict == null)
                    loadProjectList();

                return _projectsDict;
            }
        }

        #endregion
        
        #region private methods

        readonly string LINE_DELIMITER = "-------------------------";
        public enum logDelimiterMode
        {
            Added, Modified, Deleted
        }

        internal PrioritizerUnitOfWork UnitOfWork
        {
            get
            {
                //if (_context == null)
                //_context = new prioritizerDBEntities();
                //return _context;
                return new PrioritizerUnitOfWork(new prioritizerDBEntities());
            }
        }

        private static Tasks CloneTask(Tasks sourceTask, MeetingTasks sourceTaskMeeting)
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
            Clone.priority = sourceTask.priority;
            Clone.projectID = sourceTask.projectID;
            Clone.remarks = sourceTask.remarks;
            Clone.requesterID = sourceTask.requesterID;
            Clone.taskName = sourceTask.taskName;
            Clone.dateEntered = DateTime.Now;
            Clone.taskStatusID = 1;
            Clone.userID = sourceTask.userID;
            Clone.updateRequester = sourceTask.updateRequester;
            Clone.dueDate = sourceTask.dueDate;


            //var sourceTaskMeeting = NewPrioritizer.repository.getMeetingTaskByID(taskID);
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

        private string getLogDelimiterLine(logDelimiterMode mode, Guid userID, string taskName)
        {
            string modeStr = mode.ToString();
            string friendlyUserName = UsersDict[userID].userName;
            string addedModeAddition = "";
            if (mode == logDelimiterMode.Added)
            {
                addedModeAddition = string.Format("{0}Task: '{1}'", Environment.NewLine, taskName);
            }
            string log =  string.Format("{0}{1}{2} - {4} by '{3}'{5}:{1}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, friendlyUserName, modeStr,addedModeAddition);
            
            return log;

        }

        private void logChanges(Tasks t, Guid userID)
        {
            string log = string.Empty;
            string Delimiterline = string.Empty;

            if (t.ChangeTracker.State == ObjectState.Modified)
            {
                if (t.ChangeTracker.ChangedProperties.Count > 0)
                {
                    DateTime? dateClosed = t.dateClosed;
                    Delimiterline = getLogDelimiterLine(logDelimiterMode.Modified, userID, t.taskName);
                    foreach (string changedProperty in t.ChangeTracker.ChangedProperties)
                    {
                        PropertyInfo piInstance =
                            typeof(Tasks).GetProperty(changedProperty);
                        var val = piInstance.GetValue(t, null);
                        string currentValue = string.Empty;
                        if (val != null)
                            currentValue = val.ToString();

                        switch (changedProperty.ToLower())
                        {
                            case "taskstatusid":
                                if (currentValue.Length == 0)
                                    log += string.Format("Status Cleared{0}", Environment.NewLine);
                                else
                                {
                                    if (Int32.Parse(currentValue) == 4) //finished
                                    {
                                        dateClosed = DateTime.Now;
                                    }
                                    log += string.Format("Status changed to '{0}'{1}", StatusDict[Int32.Parse(currentValue)], Environment.NewLine);
                                }

                                break;
                            case "requesterid":
                                if (currentValue.Length == 0)
                                    log += string.Format("Requested by Cleared{0}", Environment.NewLine);
                                else
                                    log += string.Format("Requester changed to '{0}'{1}", UsersDict[Guid.Parse(currentValue)].userName, Environment.NewLine);
                                break;
                            case "projectid":
                                if (currentValue.Length == 0)
                                    log += string.Format("Project Cleared{0}", Environment.NewLine);
                                else
                                {
                                    if (ProjectsDict.ContainsKey(Guid.Parse(currentValue)))
                                        log += string.Format("Project changed to '{0}'{1}", ProjectsDict[Guid.Parse(currentValue)], Environment.NewLine);
                                }
                                break;
                            case "remarks":
                                log += string.Format("Remarks Change{0}", Environment.NewLine);
                                break;
                            case "priority":
                                //skip logging changes in priority
                                break;
                            case "userid":
                                log += string.Format("Task was assigned to: '{0}' by '{2}'{1}", UsersDict[Guid.Parse(currentValue)].userName, Environment.NewLine, UsersDict[userID].userName);
                                break;
                            default:
                                if (changedProperty.ToLower() != "updateslog")
                                    log += string.Format("'{0}' changed to : '{1}'{2}", changedProperty, currentValue, Environment.NewLine);
                                break;
                        }
                    }
                    t.dateClosed = dateClosed;
                }
            }
            else if (t.ChangeTracker.State == ObjectState.Added)
            {
                t.dateEntered = DateTime.Now;
                log = getLogDelimiterLine(logDelimiterMode.Added, userID, t.taskName); //string.Format("{0}{1}{2} - Added by '{3}':{4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, friendlyUserName, Environment.NewLine, LINE_DELIMITER, Environment.NewLine);
            }
            if (log.Length > 0)
            {
                t.UpdatesLog = Delimiterline + log + t.UpdatesLog;
            }          
        }

        internal Tasks applyChangesTasks(Tasks t, Guid userID, PrioritizerUnitOfWork uow)
        {
            try
            {
                if (t.ChangeTracker.State == ObjectState.Added)
                {
                    NormalizeTaskName(t);
                }


                validatePrivateTaskInMeeting(t);
                if (uow == null)
                    uow = UnitOfWork;

                isolateTaskFromOtherTasksWithinSameMeeting(t);

                logChanges(t, userID);
                updateTaskMembersBeforeSaving(t, userID, uow);
                uow.Context.Tasks.ApplyChanges(t);

                
                uow.Commit();
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void NormalizeTaskName(Tasks t)
        {            
            t.taskName = t.taskName.Replace(Convert.ToChar(183).ToString(), "");
        }

        /// <summary>
        /// we like to isolate saving of this task, 
        /// so to avoid circular commit of other tasks in same meeting (due to the full graph of the task->MeetingTasks->meeting relations)
        /// delete the meetingTasks member before saving this single task
        /// </summary>
        /// <param name="t"></param>
        private static void isolateTaskFromOtherTasksWithinSameMeeting(Tasks t)
        {            
            t.StopTracking();
            if(t.MeetingTasks.Count>0)
                t.MeetingTasks[0].Meetings= null;
            t.StartTracking();
        }

        private void updateTaskMembersBeforeSaving(Tasks t, Guid userID, PrioritizerUnitOfWork uow)
        {
            if (t.ChangeTracker.State == ObjectState.Added)
            {
                t.dateUpdated = DateTime.Now;
                Guid assigneeID = t.userID.HasValue ? t.userID.Value:Guid.Empty;
                moveAllTasksPriorityForUserInternal(1, assigneeID, uow, false);
                t.priority = 1;
                return;
            }

            if (t.ChangeTracker.ChangedProperties.Count <= 1)
            {
                if (t.ChangeTracker.ChangedProperties.Count > 0 && t.ChangeTracker.ChangedProperties.ElementAt(0).ToLower() != "priority") //changes in priority is not considered an event for updateDate change
                    t.dateUpdated = DateTime.Now;
            }
            else
                t.dateUpdated = DateTime.Now;
        }

        /// <summary>
        /// get all users in the system. used ONLY for caching purposes. shouldn't be exposed to clients.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Users> getUsers()
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                try
                {
                    return uow.Context.Users.Include("Tenant").ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static Guid? DefaultTenantID = null;
        private Guid handleTenantID(Guid? tenantID)
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                if (tenantID == null)
                {
                    if (DefaultTenantID == null)
                    {
                        Tenant tenant = uow.Context.Tenant.Where(t => t.TenantName == "DefaultTenant").FirstOrDefault();
                        if (tenant != null)
                            DefaultTenantID = tenant.ID;
                        else
                            throw new Exception("Can't locate default tenant");
                    }
                    return DefaultTenantID.Value;
                }
                else
                    return tenantID.Value;
            }
        }

        /// <summary>
        /// get all projects in the system. used ONLY for caching purposes. shouldn't be exposed to clients.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<projects> getProjectList()
        {
            using (PrioritizerUnitOfWork uow = UnitOfWork)
            {
                try
                {
                    return uow.Context.projects.Include("Tenant").ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        
        #endregion 

        

    }
}
