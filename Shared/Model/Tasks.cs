//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace Prioritizer.Shared.Model
{
    [DataContract(IsReference = true)]
    [Serializable]
    [KnownType(typeof(MeetingTasks))]
    [KnownType(typeof(Alerts))]
    [KnownType(typeof(Tenant))]
    [KnownType(typeof(Tasks))]
    [KnownType(typeof(Users))]
    public partial class Tasks: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid ID
        {
            get { return _iD; }
            set
            {
                if (_iD != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'ID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _iD = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        private System.Guid _iD;
    
        [DataMember]
        public Nullable<int> priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    ChangeTracker.RecordPropertyChange("priority");
                    _priority = value;
                    OnPropertyChanged("priority");
                }
            }
        }
        private Nullable<int> _priority;
    
        [DataMember]
        public string taskName
        {
            get { return _taskName; }
            set
            {
                if (_taskName != value)
                {
                    ChangeTracker.RecordPropertyChange("taskName");
                    _taskName = value;
                    OnPropertyChanged("taskName");
                }
            }
        }
        private string _taskName;
    
        [DataMember]
        public Nullable<int> estimatedWorkHours
        {
            get { return _estimatedWorkHours; }
            set
            {
                if (_estimatedWorkHours != value)
                {
                    ChangeTracker.RecordPropertyChange("estimatedWorkHours");
                    _estimatedWorkHours = value;
                    OnPropertyChanged("estimatedWorkHours");
                }
            }
        }
        private Nullable<int> _estimatedWorkHours;
    
        [DataMember]
        public Nullable<int> completionPercentage
        {
            get { return _completionPercentage; }
            set
            {
                if (_completionPercentage != value)
                {
                    ChangeTracker.RecordPropertyChange("completionPercentage");
                    _completionPercentage = value;
                    OnPropertyChanged("completionPercentage");
                }
            }
        }
        private Nullable<int> _completionPercentage;
    
        [DataMember]
        public string remarks
        {
            get { return _remarks; }
            set
            {
                if (_remarks != value)
                {
                    ChangeTracker.RecordPropertyChange("remarks");
                    _remarks = value;
                    OnPropertyChanged("remarks");
                }
            }
        }
        private string _remarks;
    
        [DataMember]
        public Nullable<System.Guid> projectID
        {
            get { return _projectID; }
            set
            {
                if (_projectID != value)
                {
                    ChangeTracker.RecordOriginalValue("projectID", _projectID);
                    _projectID = value;
                    OnPropertyChanged("projectID");
                }
            }
        }
        private Nullable<System.Guid> _projectID;
    
        [DataMember]
        public Nullable<System.Guid> userID
        {
            get { return _userID; }
            set
            {
                if (_userID != value)
                {
                    ChangeTracker.RecordOriginalValue("userID", _userID);
      if (!IsDeserializing)
      {
                          if (Users != null && Users.ID != value)
                        {
                            Users = null;
                        }
                    }
                    _userID = value;
                    OnPropertyChanged("userID");
                }
            }
        }
        private Nullable<System.Guid> _userID;
    
        [DataMember]
        public Nullable<System.Guid> requesterID
        {
            get { return _requesterID; }
            set
            {
                if (_requesterID != value)
                {
                    ChangeTracker.RecordPropertyChange("requesterID");
                    _requesterID = value;
                    OnPropertyChanged("requesterID");
                }
            }
        }
        private Nullable<System.Guid> _requesterID;
    
        [DataMember]
        public Nullable<bool> updateRequester
        {
            get { return _updateRequester; }
            set
            {
                if (_updateRequester != value)
                {
                    ChangeTracker.RecordPropertyChange("updateRequester");
                    _updateRequester = value;
                    OnPropertyChanged("updateRequester");
                }
            }
        }
        private Nullable<bool> _updateRequester;
    
        [DataMember]
        public Nullable<int> taskStatusID
        {
            get { return _taskStatusID; }
            set
            {
                if (_taskStatusID != value)
                {
                    ChangeTracker.RecordPropertyChange("taskStatusID");
                    _taskStatusID = value;
                    OnPropertyChanged("taskStatusID");
                }
            }
        }
        private Nullable<int> _taskStatusID;
    
        [DataMember]
        public string defectNumber
        {
            get { return _defectNumber; }
            set
            {
                if (_defectNumber != value)
                {
                    ChangeTracker.RecordPropertyChange("defectNumber");
                    _defectNumber = value;
                    OnPropertyChanged("defectNumber");
                }
            }
        }
        private string _defectNumber;
    
        [DataMember]
        public Nullable<System.DateTime> dateEntered
        {
            get { return _dateEntered; }
            set
            {
                if (_dateEntered != value)
                {
                    ChangeTracker.RecordPropertyChange("dateEntered");
                    _dateEntered = value;
                    OnPropertyChanged("dateEntered");
                }
            }
        }
        private Nullable<System.DateTime> _dateEntered;
    
        [DataMember]
        public string UpdatesLog
        {
            get { return _updatesLog; }
            set
            {
                if (_updatesLog != value)
                {
                    ChangeTracker.RecordPropertyChange("UpdatesLog");
                    _updatesLog = value;
                    OnPropertyChanged("UpdatesLog");
                }
            }
        }
        private string _updatesLog;
    
        [DataMember]
        public Nullable<System.DateTime> dateClosed
        {
            get { return _dateClosed; }
            set
            {
                if (_dateClosed != value)
                {
                    ChangeTracker.RecordPropertyChange("dateClosed");
                    _dateClosed = value;
                    OnPropertyChanged("dateClosed");
                }
            }
        }
        private Nullable<System.DateTime> _dateClosed;
    
        [DataMember]
        public Nullable<int> actualWorkHours
        {
            get { return _actualWorkHours; }
            set
            {
                if (_actualWorkHours != value)
                {
                    ChangeTracker.RecordPropertyChange("actualWorkHours");
                    _actualWorkHours = value;
                    OnPropertyChanged("actualWorkHours");
                }
            }
        }
        private Nullable<int> _actualWorkHours;
    
        [DataMember]
        public Nullable<System.DateTime> dueDate
        {
            get { return _dueDate; }
            set
            {
                if (_dueDate != value)
                {
                    ChangeTracker.RecordPropertyChange("dueDate");
                    _dueDate = value;
                    OnPropertyChanged("dueDate");
                }
            }
        }
        private Nullable<System.DateTime> _dueDate;
    
        [DataMember]
        public System.DateTime dateUpdated
        {
            get { return _dateUpdated; }
            set
            {
                if (_dateUpdated != value)
                {
                    ChangeTracker.RecordOriginalValue("dateUpdated", _dateUpdated);
                    _dateUpdated = value;
                    OnPropertyChanged("dateUpdated");
                }
            }
        }
        private System.DateTime _dateUpdated;
    
        [DataMember]
        public Nullable<bool> hasAttachment
        {
            get { return _hasAttachment; }
            set
            {
                if (_hasAttachment != value)
                {
                    ChangeTracker.RecordPropertyChange("hasAttachment");
                    _hasAttachment = value;
                    OnPropertyChanged("hasAttachment");
                }
            }
        }
        private Nullable<bool> _hasAttachment;
    
        [DataMember]
        public Nullable<bool> hasAlert
        {
            get { return _hasAlert; }
            set
            {
                if (_hasAlert != value)
                {
                    ChangeTracker.RecordPropertyChange("hasAlert");
                    _hasAlert = value;
                    OnPropertyChanged("hasAlert");
                }
            }
        }
        private Nullable<bool> _hasAlert;
    
        [DataMember]
        public int taskType
        {
            get { return _taskType; }
            set
            {
                if (_taskType != value)
                {
                    ChangeTracker.RecordPropertyChange("taskType");
                    _taskType = value;
                    OnPropertyChanged("taskType");
                }
            }
        }
        private int _taskType;
    
        [DataMember]
        public System.Guid TenantID
        {
            get { return _tenantID; }
            set
            {
                if (_tenantID != value)
                {
                    ChangeTracker.RecordOriginalValue("TenantID", _tenantID);
      if (!IsDeserializing)
      {
                          if (Tenant != null && Tenant.ID != value)
                        {
                            Tenant = null;
                        }
                    }
                    _tenantID = value;
                    OnPropertyChanged("TenantID");
                }
            }
        }
        private System.Guid _tenantID;
    
        [DataMember]
        public Nullable<System.Guid> CopiedFromTaskID
        {
            get { return _copiedFromTaskID; }
            set
            {
                if (_copiedFromTaskID != value)
                {
                    ChangeTracker.RecordOriginalValue("CopiedFromTaskID", _copiedFromTaskID);
      if (!IsDeserializing)
      {
                          if (TaskIWasCopiedFrom != null && TaskIWasCopiedFrom.ID != value)
                        {
                            TaskIWasCopiedFrom = null;
                        }
                    }
                    _copiedFromTaskID = value;
                    OnPropertyChanged("CopiedFromTaskID");
                }
            }
        }
        private Nullable<System.Guid> _copiedFromTaskID;
    
        [DataMember]
        public bool privateTask
        {
            get { return _privateTask; }
            set
            {
                if (_privateTask != value)
                {
                    ChangeTracker.RecordPropertyChange("privateTask");
                    _privateTask = value;
                    OnPropertyChanged("privateTask");
                }
            }
        }
        private bool _privateTask;
    
        [DataMember]
        public int Importance
        {
            get { return _importance; }
            set
            {
                if (_importance != value)
                {
                    ChangeTracker.RecordPropertyChange("Importance");
                    _importance = value;
                    OnPropertyChanged("Importance");
                }
            }
        }
        private int _importance;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<MeetingTasks> MeetingTasks
        {
            get
            {
                if (_meetingTasks == null)
                {
                    _meetingTasks = new TrackableCollection<MeetingTasks>();
                    _meetingTasks.CollectionChanged += FixupMeetingTasks;
                }
                return _meetingTasks;
            }
            set
            {
                if (!ReferenceEquals(_meetingTasks, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_meetingTasks != null)
                    {
                        _meetingTasks.CollectionChanged -= FixupMeetingTasks;
                    }
                    _meetingTasks = value;
                    if (_meetingTasks != null)
                    {
                        _meetingTasks.CollectionChanged += FixupMeetingTasks;
                    }
                    OnNavigationPropertyChanged("MeetingTasks");
                }
            }
        }
        private TrackableCollection<MeetingTasks> _meetingTasks;
    
        [DataMember]
        public TrackableCollection<Alerts> Alerts
        {
            get
            {
                if (_alerts == null)
                {
                    _alerts = new TrackableCollection<Alerts>();
                    _alerts.CollectionChanged += FixupAlerts;
                }
                return _alerts;
            }
            set
            {
                if (!ReferenceEquals(_alerts, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_alerts != null)
                    {
                        _alerts.CollectionChanged -= FixupAlerts;
                    }
                    _alerts = value;
                    if (_alerts != null)
                    {
                        _alerts.CollectionChanged += FixupAlerts;
                    }
                    OnNavigationPropertyChanged("Alerts");
                }
            }
        }
        private TrackableCollection<Alerts> _alerts;
    
        [DataMember]
        public Tenant Tenant
        {
            get { return _tenant; }
            set
            {
                if (!ReferenceEquals(_tenant, value))
                {
                    var previousValue = _tenant;
                    _tenant = value;
                    FixupTenant(previousValue);
                    OnNavigationPropertyChanged("Tenant");
                }
            }
        }
        private Tenant _tenant;
    
        [DataMember]
        public TrackableCollection<Tasks> TasksCopiedFromCurrentTask
        {
            get
            {
                if (_tasksCopiedFromCurrentTask == null)
                {
                    _tasksCopiedFromCurrentTask = new TrackableCollection<Tasks>();
                    _tasksCopiedFromCurrentTask.CollectionChanged += FixupTasksCopiedFromCurrentTask;
                }
                return _tasksCopiedFromCurrentTask;
            }
            set
            {
                if (!ReferenceEquals(_tasksCopiedFromCurrentTask, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tasksCopiedFromCurrentTask != null)
                    {
                        _tasksCopiedFromCurrentTask.CollectionChanged -= FixupTasksCopiedFromCurrentTask;
                    }
                    _tasksCopiedFromCurrentTask = value;
                    if (_tasksCopiedFromCurrentTask != null)
                    {
                        _tasksCopiedFromCurrentTask.CollectionChanged += FixupTasksCopiedFromCurrentTask;
                    }
                    OnNavigationPropertyChanged("TasksCopiedFromCurrentTask");
                }
            }
        }
        private TrackableCollection<Tasks> _tasksCopiedFromCurrentTask;
    
        [DataMember]
        public Tasks TaskIWasCopiedFrom
        {
            get { return _taskIWasCopiedFrom; }
            set
            {
                if (!ReferenceEquals(_taskIWasCopiedFrom, value))
                {
                    var previousValue = _taskIWasCopiedFrom;
                    _taskIWasCopiedFrom = value;
                    FixupTaskIWasCopiedFrom(previousValue);
                    OnNavigationPropertyChanged("TaskIWasCopiedFrom");
                }
            }
        }
        private Tasks _taskIWasCopiedFrom;
    
        [DataMember]
        public Users Users
        {
            get { return _users; }
            set
            {
                if (!ReferenceEquals(_users, value))
                {
                    var previousValue = _users;
                    _users = value;
                    FixupUsers(previousValue);
                    OnNavigationPropertyChanged("Users");
                }
            }
        }
        private Users _users;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            MeetingTasks.Clear();
            Alerts.Clear();
            Tenant = null;
            TasksCopiedFromCurrentTask.Clear();
            TaskIWasCopiedFrom = null;
            Users = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTenant(Tenant previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Tasks.Contains(this))
            {
                previousValue.Tasks.Remove(this);
            }
    
            if (Tenant != null)
            {
                if (!Tenant.Tasks.Contains(this))
                {
                    Tenant.Tasks.Add(this);
                }
    
                TenantID = Tenant.ID;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Tenant")
                    && (ChangeTracker.OriginalValues["Tenant"] == Tenant))
                {
                    ChangeTracker.OriginalValues.Remove("Tenant");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Tenant", previousValue);
                }
                if (Tenant != null && !Tenant.ChangeTracker.ChangeTrackingEnabled)
                {
                    Tenant.StartTracking();
                }
            }
        }
    
        private void FixupTaskIWasCopiedFrom(Tasks previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TasksCopiedFromCurrentTask.Contains(this))
            {
                previousValue.TasksCopiedFromCurrentTask.Remove(this);
            }
    
            if (TaskIWasCopiedFrom != null)
            {
                if (!TaskIWasCopiedFrom.TasksCopiedFromCurrentTask.Contains(this))
                {
                    TaskIWasCopiedFrom.TasksCopiedFromCurrentTask.Add(this);
                }
    
                CopiedFromTaskID = TaskIWasCopiedFrom.ID;
            }
            else if (!skipKeys)
            {
                CopiedFromTaskID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TaskIWasCopiedFrom")
                    && (ChangeTracker.OriginalValues["TaskIWasCopiedFrom"] == TaskIWasCopiedFrom))
                {
                    ChangeTracker.OriginalValues.Remove("TaskIWasCopiedFrom");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TaskIWasCopiedFrom", previousValue);
                }
                if (TaskIWasCopiedFrom != null && !TaskIWasCopiedFrom.ChangeTracker.ChangeTrackingEnabled)
                {
                    TaskIWasCopiedFrom.StartTracking();
                }
            }
        }
    
        private void FixupUsers(Users previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Tasks.Contains(this))
            {
                previousValue.Tasks.Remove(this);
            }
    
            if (Users != null)
            {
                if (!Users.Tasks.Contains(this))
                {
                    Users.Tasks.Add(this);
                }
    
                userID = Users.ID;
            }
            else if (!skipKeys)
            {
                userID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Users")
                    && (ChangeTracker.OriginalValues["Users"] == Users))
                {
                    ChangeTracker.OriginalValues.Remove("Users");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Users", previousValue);
                }
                if (Users != null && !Users.ChangeTracker.ChangeTrackingEnabled)
                {
                    Users.StartTracking();
                }
            }
        }
    
        private void FixupMeetingTasks(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (MeetingTasks item in e.NewItems)
                {
                    item.Tasks = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("MeetingTasks", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MeetingTasks item in e.OldItems)
                {
                    if (ReferenceEquals(item.Tasks, this))
                    {
                        item.Tasks = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("MeetingTasks", item);
                    }
                }
            }
        }
    
        private void FixupAlerts(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Alerts item in e.NewItems)
                {
                    item.Tasks = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Alerts", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Alerts item in e.OldItems)
                {
                    if (ReferenceEquals(item.Tasks, this))
                    {
                        item.Tasks = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Alerts", item);
                    }
                }
            }
        }
    
        private void FixupTasksCopiedFromCurrentTask(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Tasks item in e.NewItems)
                {
                    item.TaskIWasCopiedFrom = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TasksCopiedFromCurrentTask", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Tasks item in e.OldItems)
                {
                    if (ReferenceEquals(item.TaskIWasCopiedFrom, this))
                    {
                        item.TaskIWasCopiedFrom = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TasksCopiedFromCurrentTask", item);
                    }
                }
            }
        }

        #endregion
    }
}