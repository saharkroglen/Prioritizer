using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PrioritizerService.Model;
using System.Data.Objects;
using System.IO;
using PrioritizerService;
using Shared;
using Prioritizer.Shared.Model;
using Prioritizer.Shared;
using PrioritizerService.Class;

namespace Online
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPrioritizerService
    {

        //[OperationContract]
        //string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        //[OperationContract]
        //IEnumerable<MyEntity> GetMyEntitiesBySearchText(string searchText);
        [OperationContract]
        void sendInvitationMail(Users u, Guid userID);
        [OperationContract]
        bool DomainControllerAuthenticate();
        [OperationContract]
        Users Authenticate(string username, string password, string company);

        [OperationContract]
        ClientPackage getLatestClient();
        [OperationContract]
        ClientMessage ping(Guid userID, DateTime lastUpdate);
        [OperationContract]
        void ClientMessageReceiveConfirmation(Guid userID);
        [OperationContract]
        void Poke(Poke p);
        [OperationContract]
        string HelloWorld(int input);
        [OperationContract]
        List<Alerts> getAlertsForUser(Guid userID);
        [OperationContract]
        List<Alerts> getAlertForTask(Guid taskID);
        [OperationContract]
        void applyChangesAlerts(Alerts a, Guid userID);
        [OperationContract]
        void deleteAlert(Alerts a);

        [OperationContract]
        List<Users> getUsersList(Guid? tenantID);

        [OperationContract]
        void deleteMeetingCategory(MeetingCategory mc);
        [OperationContract]
        void applyChangesMeetingCategory(MeetingCategory mc, Guid? tenantID);
        [OperationContract]
        void deleteMeetings(Meetings m);
        [OperationContract]
        void applyChangesMeetings(Meetings m, Guid? tenantID);
        [OperationContract]
        void deleteTasks(Tasks t);
        [OperationContract]
        Tasks addTask(Tasks t, Guid userID);
        [OperationContract]
        void applyChangesMeetingTasksList(List<MeetingTasks> changedMeetingTasksList);
        [OperationContract]
        List<TaskSaveFailure> applyChangesTasksList(List<Tasks> changedTasksList, Guid userID);
        [OperationContract]
        Tasks applyChangesTasks(Tasks t, Guid userID);
        [OperationContract]
        void deleteMeetingTasks(MeetingTasks mt);
        [OperationContract]
        void applyChangesMeetingTasks(MeetingTasks mt);
        [OperationContract]
        void deleteMeetingCategoryMap(MeetingCategoryMap mcm);
        [OperationContract]
        void applyChangesMeetingCategoryMap(MeetingCategoryMap mcm);
        [OperationContract]
        void deleteProjects(projects p);
        [OperationContract]
        void applyChangesProjects(projects p, Guid? tenantID);
        [OperationContract]
        void deleteUsers(Users u);
        [OperationContract]
        Users applyChangesUsers(Users u, Guid userID);
        [OperationContract]
        void deleteManagerTeamMemberRelations(ManagerTeamMemberRelations mtr);
        [OperationContract]
        void applyChangesManagerTeamMemberRelations(ManagerTeamMemberRelations mtr);
        [OperationContract]
        void deleteMeetingAttendies(MeetingAttendies ma);
        [OperationContract]
        void applyChangesMeetingAttendies(MeetingAttendies ma);        
        [OperationContract]        
        IEnumerable<Users> getUsers(Guid? tenantID);
        [OperationContract]
        void moveAllTasksPriorityForUser(int numOfSteps, Guid userID);
        [OperationContract]
        Tasks getTaskByID(Guid taskID);
        [OperationContract]
        IEnumerable<Tasks> getTasksByIDs(List<Guid> taskIDs);
        [OperationContract]
        Users getUserByID(Guid userID);
        [OperationContract]
        IEnumerable<Users> getUserByDomainName(string domainName, Guid? tenantID);
        [OperationContract]
        MeetingTasks getMeetingTaskByID(Guid taskID);
        [OperationContract]
        IEnumerable<MeetingTasks> getMeetingTasksByID(Guid taskID);
        [OperationContract]
        MeetingCategoryMap getMeetingCategoryMapByID(Guid meetingID);
        [OperationContract]
        Meetings getMeetingByID(Guid meetingID, bool includeTasks);
        [OperationContract]
        List<Tasks> getTasksForMeeting(Guid meetingID);
        [OperationContract]
        Meetings getMeetingByName(string meetingName);
        [OperationContract]
        IEnumerable<ManagerTeamMemberRelations> findAllTeamMembersAllowedForUser(Guid userID);
        [OperationContract]
        IEnumerable<MeetingCategory> getMeetingCategoryList(bool filterCategoriesOnlyForLoggedInUser, Guid userID, Guid tenantID);
        [OperationContract]
        IEnumerable<Meetings> getMeetingList(Guid meetingCategory, Guid userID, bool includeFinished, Guid? tenantID);
        [OperationContract]
        IEnumerable<Tasks> getTasksForUser(bool includeFinishedTasks, bool includeCancelledTasks, Guid userID, Guid requestingUserID);
        [OperationContract]
        IEnumerable<Tasks> getTasksForTeamMembers(bool includeFinishedTasks, bool includeCancelledTasks, Guid userID, Guid requestingUserID);
        [OperationContract]
        IEnumerable<TaskStatus> getTaskStatusList();
        [OperationContract]
        IEnumerable<projects> getProjectList(Guid? tenantID);
        [OperationContract]
        IEnumerable<ManagerTeamMemberRelations> getManagerTeamMemberRelationsList(Guid? tenantID);
        [OperationContract]
        IEnumerable<Meetings> getMeetingsForOwner(Guid ownerUserID);
        [OperationContract]
        bool relationExists(Guid managerID, Guid teamMemberID);
        [OperationContract]
        void forwardTo(Guid taskID, Guid targetUserID, Guid loggedInUserID);
        [OperationContract]
        void copyTo(Guid taskID, Guid targetUserID, Guid loggedInUserID);
        [OperationContract]
        IEnumerable<MeetingAttendies> getMeetingAttendees(Guid meetingID);
        [OperationContract]
        IEnumerable<Users> getMeetingAttendeesUserList(Guid meetingID);
        [OperationContract]
        void deleteAttachment(attachments a, Guid userID);
        [OperationContract]
        void applyChangesAttachments(attachments a);
        [OperationContract]
        void addAttachment(attachments a, Guid userID);
        [OperationContract]
        List<attachments> getAttachmentsForTask(Guid taskID);
        [OperationContract]
        string getShowTaskURL();
        [OperationContract]
        string getServerVersion();
        [OperationContract]
        string getClientVersion();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
