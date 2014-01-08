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
    [KnownType(typeof(Meetings))]
    [KnownType(typeof(Tasks))]
    [KnownType(typeof(Tenant))]
    public partial class MeetingTasks: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public System.Guid MeetingID
        {
            get { return _meetingID; }
            set
            {
                if (_meetingID != value)
                {
                    ChangeTracker.RecordOriginalValue("MeetingID", _meetingID);
      if (!IsDeserializing)
      {
                          if (Meetings != null && Meetings.ID != value)
                        {
                            Meetings = null;
                        }
                    }
                    _meetingID = value;
                    OnPropertyChanged("MeetingID");
                }
            }
        }
        private System.Guid _meetingID;
    
        [DataMember]
        public System.Guid TaskID
        {
            get { return _taskID; }
            set
            {
                if (_taskID != value)
                {
                    ChangeTracker.RecordOriginalValue("TaskID", _taskID);
      if (!IsDeserializing)
      {
                          if (Tasks != null && Tasks.ID != value)
                        {
                            Tasks = null;
                        }
                    }
                    _taskID = value;
                    OnPropertyChanged("TaskID");
                }
            }
        }
        private System.Guid _taskID;
    
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

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Meetings Meetings
        {
            get { return _meetings; }
            set
            {
                if (!ReferenceEquals(_meetings, value))
                {
                    var previousValue = _meetings;
                    _meetings = value;
                    FixupMeetings(previousValue);
                    OnNavigationPropertyChanged("Meetings");
                }
            }
        }
        private Meetings _meetings;
    
        [DataMember]
        public Tasks Tasks
        {
            get { return _tasks; }
            set
            {
                if (!ReferenceEquals(_tasks, value))
                {
                    var previousValue = _tasks;
                    _tasks = value;
                    FixupTasks(previousValue);
                    OnNavigationPropertyChanged("Tasks");
                }
            }
        }
        private Tasks _tasks;
    
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
            Meetings = null;
            Tasks = null;
            Tenant = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupMeetings(Meetings previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.MeetingTasks.Contains(this))
            {
                previousValue.MeetingTasks.Remove(this);
            }
    
            if (Meetings != null)
            {
                if (!Meetings.MeetingTasks.Contains(this))
                {
                    Meetings.MeetingTasks.Add(this);
                }
    
                MeetingID = Meetings.ID;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Meetings")
                    && (ChangeTracker.OriginalValues["Meetings"] == Meetings))
                {
                    ChangeTracker.OriginalValues.Remove("Meetings");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Meetings", previousValue);
                }
                if (Meetings != null && !Meetings.ChangeTracker.ChangeTrackingEnabled)
                {
                    Meetings.StartTracking();
                }
            }
        }
    
        private void FixupTasks(Tasks previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.MeetingTasks.Contains(this))
            {
                previousValue.MeetingTasks.Remove(this);
            }
    
            if (Tasks != null)
            {
                if (!Tasks.MeetingTasks.Contains(this))
                {
                    Tasks.MeetingTasks.Add(this);
                }
    
                TaskID = Tasks.ID;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Tasks")
                    && (ChangeTracker.OriginalValues["Tasks"] == Tasks))
                {
                    ChangeTracker.OriginalValues.Remove("Tasks");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Tasks", previousValue);
                }
                if (Tasks != null && !Tasks.ChangeTracker.ChangeTrackingEnabled)
                {
                    Tasks.StartTracking();
                }
            }
        }
    
        private void FixupTenant(Tenant previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.MeetingTasks.Contains(this))
            {
                previousValue.MeetingTasks.Remove(this);
            }
    
            if (Tenant != null)
            {
                if (!Tenant.MeetingTasks.Contains(this))
                {
                    Tenant.MeetingTasks.Add(this);
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

        #endregion
    }
}