﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Prioritizer.Shared.Model
{
    public partial class prioritizerDBEntities : ObjectContext
    {
        public const string ConnectionString = "name=prioritizerDBEntities";
        public const string ContainerName = "prioritizerDBEntities";
    
        #region Constructors
    
        public prioritizerDBEntities()
            : base(ConnectionString, ContainerName)
        {
            Initialize();
        }
    
        public prioritizerDBEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            Initialize();
        }
    
        public prioritizerDBEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            Initialize();
        }
    
        private void Initialize()
        {
            // Creating proxies requires the use of the ProxyDataContractResolver and
            // may allow lazy loading which can expand the loaded graph during serialization.
            ContextOptions.ProxyCreationEnabled = false;
            ObjectMaterialized += new ObjectMaterializedEventHandler(HandleObjectMaterialized);
        }
    
        private void HandleObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var entity = e.Entity as IObjectWithChangeTracker;
            if (entity != null)
            {
                bool changeTrackingEnabled = entity.ChangeTracker.ChangeTrackingEnabled;
                try
                {
                    entity.MarkAsUnchanged();
                }
                finally
                {
                    entity.ChangeTracker.ChangeTrackingEnabled = changeTrackingEnabled;
                }
                this.StoreReferenceKeyValues(entity);
            }
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<NotificationType> NotificationType
        {
            get { return _notificationType  ?? (_notificationType = CreateObjectSet<NotificationType>("NotificationType")); }
        }
        private ObjectSet<NotificationType> _notificationType;
    
        public ObjectSet<TaskStatus> TaskStatus
        {
            get { return _taskStatus  ?? (_taskStatus = CreateObjectSet<TaskStatus>("TaskStatus")); }
        }
        private ObjectSet<TaskStatus> _taskStatus;
    
        public ObjectSet<Users> Users
        {
            get { return _users  ?? (_users = CreateObjectSet<Users>("Users")); }
        }
        private ObjectSet<Users> _users;
    
        public ObjectSet<projects> projects
        {
            get { return _projects  ?? (_projects = CreateObjectSet<projects>("projects")); }
        }
        private ObjectSet<projects> _projects;
    
        public ObjectSet<Tasks> Tasks
        {
            get { return _tasks  ?? (_tasks = CreateObjectSet<Tasks>("Tasks")); }
        }
        private ObjectSet<Tasks> _tasks;
    
        public ObjectSet<ConfigTable> ConfigTable
        {
            get { return _configTable  ?? (_configTable = CreateObjectSet<ConfigTable>("ConfigTable")); }
        }
        private ObjectSet<ConfigTable> _configTable;
    
        public ObjectSet<Meetings> Meetings
        {
            get { return _meetings  ?? (_meetings = CreateObjectSet<Meetings>("Meetings")); }
        }
        private ObjectSet<Meetings> _meetings;
    
        public ObjectSet<MeetingTasks> MeetingTasks
        {
            get { return _meetingTasks  ?? (_meetingTasks = CreateObjectSet<MeetingTasks>("MeetingTasks")); }
        }
        private ObjectSet<MeetingTasks> _meetingTasks;
    
        public ObjectSet<MeetingAttendies> MeetingAttendies
        {
            get { return _meetingAttendies  ?? (_meetingAttendies = CreateObjectSet<MeetingAttendies>("MeetingAttendies")); }
        }
        private ObjectSet<MeetingAttendies> _meetingAttendies;
    
        public ObjectSet<MeetingCategory> MeetingCategory
        {
            get { return _meetingCategory  ?? (_meetingCategory = CreateObjectSet<MeetingCategory>("MeetingCategory")); }
        }
        private ObjectSet<MeetingCategory> _meetingCategory;
    
        public ObjectSet<MeetingCategoryMap> MeetingCategoryMap
        {
            get { return _meetingCategoryMap  ?? (_meetingCategoryMap = CreateObjectSet<MeetingCategoryMap>("MeetingCategoryMap")); }
        }
        private ObjectSet<MeetingCategoryMap> _meetingCategoryMap;
    
        public ObjectSet<Notifications> Notifications
        {
            get { return _notifications  ?? (_notifications = CreateObjectSet<Notifications>("Notifications")); }
        }
        private ObjectSet<Notifications> _notifications;
    
        public ObjectSet<ManagerTeamMemberRelations> ManagerTeamMemberRelations
        {
            get { return _managerTeamMemberRelations  ?? (_managerTeamMemberRelations = CreateObjectSet<ManagerTeamMemberRelations>("ManagerTeamMemberRelations")); }
        }
        private ObjectSet<ManagerTeamMemberRelations> _managerTeamMemberRelations;
    
        public ObjectSet<attachments> attachments
        {
            get { return _attachments  ?? (_attachments = CreateObjectSet<attachments>("attachments")); }
        }
        private ObjectSet<attachments> _attachments;
    
        public ObjectSet<Alerts> Alerts
        {
            get { return _alerts  ?? (_alerts = CreateObjectSet<Alerts>("Alerts")); }
        }
        private ObjectSet<Alerts> _alerts;
    
        public ObjectSet<Tenant> Tenant
        {
            get { return _tenant  ?? (_tenant = CreateObjectSet<Tenant>("Tenant")); }
        }
        private ObjectSet<Tenant> _tenant;

        #endregion
    }
}
