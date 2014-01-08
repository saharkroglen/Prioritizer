﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5456
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrioritizerService.Model
{
    using System.Runtime.Serialization;
    using System;



    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IPrioritizerService")]
    public interface IPrioritizerService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getLatestClient", ReplyAction = "http://tempuri.org/IPrioritizerService/getLatestClientResponse")]
        PrioritizerService.ClientPackage getLatestClient();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/ping", ReplyAction = "http://tempuri.org/IPrioritizerService/pingResponse")]
        bool ping(System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/HelloWorld", ReplyAction = "http://tempuri.org/IPrioritizerService/HelloWorldResponse")]
        string HelloWorld(int input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getAlertsForUser", ReplyAction = "http://tempuri.org/IPrioritizerService/getAlertsForUserResponse")]
        PrioritizerService.Model.Alerts[] getAlertsForUser(System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getAlertForTask", ReplyAction = "http://tempuri.org/IPrioritizerService/getAlertForTaskResponse")]
        PrioritizerService.Model.Alerts[] getAlertForTask(System.Guid taskID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesAlerts", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesAlertsResponse")]
        void applyChangesAlerts(PrioritizerService.Model.Alerts a, System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteAlert", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteAlertResponse")]
        void deleteAlert(PrioritizerService.Model.Alerts a);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getUsersList", ReplyAction = "http://tempuri.org/IPrioritizerService/getUsersListResponse")]
        PrioritizerService.Model.Users[] getUsersList(System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteMeetingCategory", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteMeetingCategoryResponse")]
        void deleteMeetingCategory(PrioritizerService.Model.MeetingCategory mc);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesMeetingCategory", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesMeetingCategoryResponse")]
        void applyChangesMeetingCategory(PrioritizerService.Model.MeetingCategory mc, System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteMeetings", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteMeetingsResponse")]
        void deleteMeetings(PrioritizerService.Model.Meetings m);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesMeetings", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesMeetingsResponse")]
        void applyChangesMeetings(PrioritizerService.Model.Meetings m, System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteTasks", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteTasksResponse")]
        void deleteTasks(PrioritizerService.Model.Tasks t);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/addTask", ReplyAction = "http://tempuri.org/IPrioritizerService/addTaskResponse")]
        PrioritizerService.Model.Tasks addTask(PrioritizerService.Model.Tasks t, System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesMeetingTasksList", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesMeetingTasksListResponse")]
        void applyChangesMeetingTasksList(PrioritizerService.Model.MeetingTasks[] changedMeetingTasksList);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesTasksList", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesTasksListResponse")]
        PrioritizerService.Model.Tasks[] applyChangesTasksList(PrioritizerService.Model.Tasks[] changedTasksList, System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesTasks", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesTasksResponse")]
        PrioritizerService.Model.Tasks applyChangesTasks(PrioritizerService.Model.Tasks t, System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteMeetingTasks", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteMeetingTasksResponse")]
        void deleteMeetingTasks(PrioritizerService.Model.MeetingTasks mt);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesMeetingTasks", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesMeetingTasksResponse")]
        void applyChangesMeetingTasks(PrioritizerService.Model.MeetingTasks mt);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteMeetingCategoryMap", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteMeetingCategoryMapResponse")]
        void deleteMeetingCategoryMap(PrioritizerService.Model.MeetingCategoryMap mcm);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesMeetingCategoryMap", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesMeetingCategoryMapResponse")]
        void applyChangesMeetingCategoryMap(PrioritizerService.Model.MeetingCategoryMap mcm);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteProjects", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteProjectsResponse")]
        void deleteProjects(PrioritizerService.Model.projects p);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesProjects", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesProjectsResponse")]
        void applyChangesProjects(PrioritizerService.Model.projects p, System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteUsers", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteUsersResponse")]
        void deleteUsers(PrioritizerService.Model.Users u);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesUsers", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesUsersResponse")]
        void applyChangesUsers(PrioritizerService.Model.Users u);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteManagerTeamMemberRelations", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteManagerTeamMemberRelationsResponse")]
        void deleteManagerTeamMemberRelations(PrioritizerService.Model.ManagerTeamMemberRelations mtr);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesManagerTeamMemberRelations", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesManagerTeamMemberRelationsResp" +
            "onse")]
        void applyChangesManagerTeamMemberRelations(PrioritizerService.Model.ManagerTeamMemberRelations mtr);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteMeetingAttendies", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteMeetingAttendiesResponse")]
        void deleteMeetingAttendies(PrioritizerService.Model.MeetingAttendies ma);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesMeetingAttendies", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesMeetingAttendiesResponse")]
        void applyChangesMeetingAttendies(PrioritizerService.Model.MeetingAttendies ma);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getUsers", ReplyAction = "http://tempuri.org/IPrioritizerService/getUsersResponse")]
        PrioritizerService.Model.Users[] getUsers(System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/moveAllTasksPriorityForUser", ReplyAction = "http://tempuri.org/IPrioritizerService/moveAllTasksPriorityForUserResponse")]
        void moveAllTasksPriorityForUser(int numOfSteps, System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getTaskByID", ReplyAction = "http://tempuri.org/IPrioritizerService/getTaskByIDResponse")]
        PrioritizerService.Model.Tasks getTaskByID(System.Guid taskID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getTasksByIDs", ReplyAction = "http://tempuri.org/IPrioritizerService/getTasksByIDsResponse")]
        PrioritizerService.Model.Tasks[] getTasksByIDs(System.Guid[] taskIDs);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getUserByID", ReplyAction = "http://tempuri.org/IPrioritizerService/getUserByIDResponse")]
        PrioritizerService.Model.Users getUserByID(System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getUserByDomainName", ReplyAction = "http://tempuri.org/IPrioritizerService/getUserByDomainNameResponse")]
        PrioritizerService.Model.Users[] getUserByDomainName(string domainName, System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingTaskByID", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingTaskByIDResponse")]
        PrioritizerService.Model.MeetingTasks getMeetingTaskByID(System.Guid taskID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingCategoryMapByID", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingCategoryMapByIDResponse")]
        PrioritizerService.Model.MeetingCategoryMap getMeetingCategoryMapByID(System.Guid meetingID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingByID", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingByIDResponse")]
        PrioritizerService.Model.Meetings getMeetingByID(System.Guid meetingID, bool includeTasks);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getTasksForMeeting", ReplyAction = "http://tempuri.org/IPrioritizerService/getTasksForMeetingResponse")]
        PrioritizerService.Model.Tasks[] getTasksForMeeting(System.Guid meetingID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingByName", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingByNameResponse")]
        PrioritizerService.Model.Meetings getMeetingByName(string meetingName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/findAllTeamMembersAllowedForUser", ReplyAction = "http://tempuri.org/IPrioritizerService/findAllTeamMembersAllowedForUserResponse")]
        PrioritizerService.Model.ManagerTeamMemberRelations[] findAllTeamMembersAllowedForUser(System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingCategoryList", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingCategoryListResponse")]
        PrioritizerService.Model.MeetingCategory[] getMeetingCategoryList(bool filterCategoriesOnlyForLoggedInUser, System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingList", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingListResponse")]
        PrioritizerService.Model.Meetings[] getMeetingList(System.Guid meetingCategory, System.Guid userID, bool includeFinished, System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getTasksForUser", ReplyAction = "http://tempuri.org/IPrioritizerService/getTasksForUserResponse")]
        PrioritizerService.Model.Tasks[] getTasksForUser(bool includeFinishedTasks, bool includeCancelledTasks, System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getTaskStatusList", ReplyAction = "http://tempuri.org/IPrioritizerService/getTaskStatusListResponse")]
        PrioritizerService.Model.TaskStatus[] getTaskStatusList();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getProjectList", ReplyAction = "http://tempuri.org/IPrioritizerService/getProjectListResponse")]
        PrioritizerService.Model.projects[] getProjectList(System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getManagerTeamMemberRelationsList", ReplyAction = "http://tempuri.org/IPrioritizerService/getManagerTeamMemberRelationsListResponse")]
        PrioritizerService.Model.ManagerTeamMemberRelations[] getManagerTeamMemberRelationsList(System.Nullable<System.Guid> tenantID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingsForOwner", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingsForOwnerResponse")]
        PrioritizerService.Model.Meetings[] getMeetingsForOwner(System.Guid ownerUserID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/relationExists", ReplyAction = "http://tempuri.org/IPrioritizerService/relationExistsResponse")]
        bool relationExists(System.Guid managerID, System.Guid teamMemberID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/forwardTo", ReplyAction = "http://tempuri.org/IPrioritizerService/forwardToResponse")]
        void forwardTo(System.Guid taskID, System.Guid targetUserID, System.Guid loggedInUserID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/copyTo", ReplyAction = "http://tempuri.org/IPrioritizerService/copyToResponse")]
        void copyTo(System.Guid taskID, System.Guid targetUserID, System.Guid loggedInUserID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingAttendees", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingAttendeesResponse")]
        PrioritizerService.Model.MeetingAttendies[] getMeetingAttendees(System.Guid meetingID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getMeetingAttendeesUserList", ReplyAction = "http://tempuri.org/IPrioritizerService/getMeetingAttendeesUserListResponse")]
        PrioritizerService.Model.Users[] getMeetingAttendeesUserList(System.Guid meetingID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/deleteAttachment", ReplyAction = "http://tempuri.org/IPrioritizerService/deleteAttachmentResponse")]
        void deleteAttachment(PrioritizerService.Model.attachments a);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/applyChangesAttachments", ReplyAction = "http://tempuri.org/IPrioritizerService/applyChangesAttachmentsResponse")]
        void applyChangesAttachments(PrioritizerService.Model.attachments a);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/addAttachment", ReplyAction = "http://tempuri.org/IPrioritizerService/addAttachmentResponse")]
        void addAttachment(PrioritizerService.Model.attachments a, System.Guid userID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getAttachmentsForTask", ReplyAction = "http://tempuri.org/IPrioritizerService/getAttachmentsForTaskResponse")]
        PrioritizerService.Model.attachments[] getAttachmentsForTask(System.Guid taskID);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getShowTaskURL", ReplyAction = "http://tempuri.org/IPrioritizerService/getShowTaskURLResponse")]
        string getShowTaskURL();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getServerVersion", ReplyAction = "http://tempuri.org/IPrioritizerService/getServerVersionResponse")]
        string getServerVersion();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrioritizerService/getClientVersion", ReplyAction = "http://tempuri.org/IPrioritizerService/getClientVersionResponse")]
        string getClientVersion();
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IPrioritizerServiceChannel : IPrioritizerService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class PrioritizerServiceClient : System.ServiceModel.ClientBase<IPrioritizerService>, IPrioritizerService
    {

        public PrioritizerServiceClient()
        {
        }

        public PrioritizerServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public PrioritizerServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public PrioritizerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public PrioritizerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public PrioritizerService.ClientPackage getLatestClient()
        {
            return base.Channel.getLatestClient();
        }

        public bool ping(System.Guid userID)
        {
            return base.Channel.ping(userID);
        }

        public string HelloWorld(int input)
        {
            return base.Channel.HelloWorld(input);
        }

        public PrioritizerService.Model.Alerts[] getAlertsForUser(System.Guid userID)
        {
            return base.Channel.getAlertsForUser(userID);
        }

        public PrioritizerService.Model.Alerts[] getAlertForTask(System.Guid taskID)
        {
            return base.Channel.getAlertForTask(taskID);
        }

        public void applyChangesAlerts(PrioritizerService.Model.Alerts a, System.Guid userID)
        {
            base.Channel.applyChangesAlerts(a, userID);
        }

        public void deleteAlert(PrioritizerService.Model.Alerts a)
        {
            base.Channel.deleteAlert(a);
        }

        public PrioritizerService.Model.Users[] getUsersList(System.Nullable<System.Guid> tenantID)
        {
            return base.Channel.getUsersList(tenantID);
        }

        public void deleteMeetingCategory(PrioritizerService.Model.MeetingCategory mc)
        {
            base.Channel.deleteMeetingCategory(mc);
        }

        public void applyChangesMeetingCategory(PrioritizerService.Model.MeetingCategory mc, System.Nullable<System.Guid> tenantID)
        {
            base.Channel.applyChangesMeetingCategory(mc, tenantID);
        }

        public void deleteMeetings(PrioritizerService.Model.Meetings m)
        {
            base.Channel.deleteMeetings(m);
        }

        public void applyChangesMeetings(PrioritizerService.Model.Meetings m, System.Nullable<System.Guid> tenantID)
        {
            base.Channel.applyChangesMeetings(m, tenantID);
        }

        public void deleteTasks(PrioritizerService.Model.Tasks t)
        {
            base.Channel.deleteTasks(t);
        }

        public PrioritizerService.Model.Tasks addTask(PrioritizerService.Model.Tasks t, System.Guid userID)
        {
            return base.Channel.addTask(t, userID);
        }

        public void applyChangesMeetingTasksList(PrioritizerService.Model.MeetingTasks[] changedMeetingTasksList)
        {
            base.Channel.applyChangesMeetingTasksList(changedMeetingTasksList);
        }

        public PrioritizerService.Model.Tasks[] applyChangesTasksList(PrioritizerService.Model.Tasks[] changedTasksList, System.Guid userID)
        {
            return base.Channel.applyChangesTasksList(changedTasksList, userID);
        }

        public PrioritizerService.Model.Tasks applyChangesTasks(PrioritizerService.Model.Tasks t, System.Guid userID)
        {
            return base.Channel.applyChangesTasks(t, userID);
        }

        public void deleteMeetingTasks(PrioritizerService.Model.MeetingTasks mt)
        {
            base.Channel.deleteMeetingTasks(mt);
        }

        public void applyChangesMeetingTasks(PrioritizerService.Model.MeetingTasks mt)
        {
            base.Channel.applyChangesMeetingTasks(mt);
        }

        public void deleteMeetingCategoryMap(PrioritizerService.Model.MeetingCategoryMap mcm)
        {
            base.Channel.deleteMeetingCategoryMap(mcm);
        }

        public void applyChangesMeetingCategoryMap(PrioritizerService.Model.MeetingCategoryMap mcm)
        {
            base.Channel.applyChangesMeetingCategoryMap(mcm);
        }

        public void deleteProjects(PrioritizerService.Model.projects p)
        {
            base.Channel.deleteProjects(p);
        }

        public void applyChangesProjects(PrioritizerService.Model.projects p, System.Nullable<System.Guid> tenantID)
        {
            base.Channel.applyChangesProjects(p, tenantID);
        }

        public void deleteUsers(PrioritizerService.Model.Users u)
        {
            base.Channel.deleteUsers(u);
        }

        public void applyChangesUsers(PrioritizerService.Model.Users u)
        {
            base.Channel.applyChangesUsers(u);
        }

        public void deleteManagerTeamMemberRelations( PrioritizerService.Model.ManagerTeamMemberRelations mtr)
        {
            base.Channel.deleteManagerTeamMemberRelations(mtr);
        }

        public void applyChangesManagerTeamMemberRelations(PrioritizerService.Model.ManagerTeamMemberRelations mtr)
        {
            base.Channel.applyChangesManagerTeamMemberRelations(mtr);
        }

        public void deleteMeetingAttendies(PrioritizerService.Model.MeetingAttendies ma)
        {
            base.Channel.deleteMeetingAttendies(ma);
        }

        public void applyChangesMeetingAttendies(PrioritizerService.Model.MeetingAttendies ma)
        {
            base.Channel.applyChangesMeetingAttendies(ma);
        }

        public PrioritizerService.Model.Users[] getUsers(System.Nullable<System.Guid> tenantID)
        {
            return base.Channel.getUsers(tenantID);
        }

        public void moveAllTasksPriorityForUser(int numOfSteps, System.Guid userID)
        {
            base.Channel.moveAllTasksPriorityForUser(numOfSteps, userID);
        }

        public PrioritizerService.Model.Tasks getTaskByID(System.Guid taskID)
        {
            return base.Channel.getTaskByID(taskID);
        }

        public PrioritizerService.Model.Tasks[] getTasksByIDs(System.Guid[] taskIDs)
        {
            return base.Channel.getTasksByIDs(taskIDs);
        }

        public PrioritizerService.Model.Users getUserByID(System.Guid userID)
        {
            return base.Channel.getUserByID(userID);
        }

        public PrioritizerService.Model.Users[] getUserByDomainName(string domainName, System.Nullable<System.Guid> tenantID)
        {
            return base.Channel.getUserByDomainName(domainName, tenantID);
        }

        public PrioritizerService.Model.MeetingTasks getMeetingTaskByID(System.Guid taskID)
        {
            return base.Channel.getMeetingTaskByID(taskID);
        }

        public PrioritizerService.Model.MeetingCategoryMap getMeetingCategoryMapByID(System.Guid meetingID)
        {
            return base.Channel.getMeetingCategoryMapByID(meetingID);
        }

        public PrioritizerService.Model.Meetings getMeetingByID(System.Guid meetingID, bool includeTasks)
        {
            return base.Channel.getMeetingByID(meetingID, includeTasks);
        }

        public PrioritizerService.Model.Tasks[] getTasksForMeeting(System.Guid meetingID)
        {
            return base.Channel.getTasksForMeeting(meetingID);
        }

        public PrioritizerService.Model.Meetings getMeetingByName(string meetingName)
        {
            return base.Channel.getMeetingByName(meetingName);
        }

        public PrioritizerService.Model.ManagerTeamMemberRelations[] findAllTeamMembersAllowedForUser(System.Guid userID)
        {
            return base.Channel.findAllTeamMembersAllowedForUser(userID);
        }

        public PrioritizerService.Model.MeetingCategory[] getMeetingCategoryList(bool filterCategoriesOnlyForLoggedInUser, System.Guid userID)
        {
            return base.Channel.getMeetingCategoryList(filterCategoriesOnlyForLoggedInUser, userID);
        }

        public PrioritizerService.Model.Meetings[] getMeetingList(System.Guid meetingCategory, System.Guid userID, bool includeFinished, System.Nullable<System.Guid> tenantID)
        {
            return base.Channel.getMeetingList(meetingCategory, userID, includeFinished, tenantID);
        }

        public PrioritizerService.Model.Tasks[] getTasksForUser(bool includeFinishedTasks, bool includeCancelledTasks, System.Guid userID)
        {
            return base.Channel.getTasksForUser(includeFinishedTasks, includeCancelledTasks, userID);
        }

        public PrioritizerService.Model.TaskStatus[] getTaskStatusList()
        {
            return base.Channel.getTaskStatusList();
        }

        public PrioritizerService.Model.projects[] getProjectList(System.Nullable<System.Guid> tenantID)
        {
            return base.Channel.getProjectList(tenantID);
        }

        public PrioritizerService.Model.ManagerTeamMemberRelations[] getManagerTeamMemberRelationsList(System.Nullable<System.Guid> tenantID)
        {
            return base.Channel.getManagerTeamMemberRelationsList(tenantID);
        }

        public PrioritizerService.Model.Meetings[] getMeetingsForOwner(System.Guid ownerUserID)
        {
            return base.Channel.getMeetingsForOwner(ownerUserID);
        }

        public bool relationExists(System.Guid managerID, System.Guid teamMemberID)
        {
            return base.Channel.relationExists(managerID, teamMemberID);
        }

        public void forwardTo(System.Guid taskID, System.Guid targetUserID, System.Guid loggedInUserID)
        {
            base.Channel.forwardTo(taskID, targetUserID, loggedInUserID);
        }

        public void copyTo(System.Guid taskID, System.Guid targetUserID, System.Guid loggedInUserID)
        {
            base.Channel.copyTo(taskID, targetUserID, loggedInUserID);
        }

        public PrioritizerService.Model.MeetingAttendies[] getMeetingAttendees(System.Guid meetingID)
        {
            return base.Channel.getMeetingAttendees(meetingID);
        }

        public PrioritizerService.Model.Users[] getMeetingAttendeesUserList(System.Guid meetingID)
        {
            return base.Channel.getMeetingAttendeesUserList(meetingID);
        }

        public void deleteAttachment(PrioritizerService.Model.attachments a)
        {
            base.Channel.deleteAttachment(a);
        }

        public void applyChangesAttachments(PrioritizerService.Model.attachments a)
        {
            base.Channel.applyChangesAttachments(a);
        }

        public void addAttachment(PrioritizerService.Model.attachments a, System.Guid userID)
        {
            base.Channel.addAttachment(a, userID);
        }

        public PrioritizerService.Model.attachments[] getAttachmentsForTask(System.Guid taskID)
        {
            return base.Channel.getAttachmentsForTask(taskID);
        }

        public string getShowTaskURL()
        {
            return base.Channel.getShowTaskURL();
        }

        public string getServerVersion()
        {
            return base.Channel.getServerVersion();
        }

        public string getClientVersion()
        {
            return base.Channel.getClientVersion();
        }
    }

}