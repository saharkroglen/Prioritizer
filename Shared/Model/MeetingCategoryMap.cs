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
    [KnownType(typeof(MeetingCategory))]
    [KnownType(typeof(Tenant))]
    public partial class MeetingCategoryMap: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public Nullable<System.Guid> MeetingID
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
        private Nullable<System.Guid> _meetingID;
    
        [DataMember]
        public Nullable<System.Guid> MeetingCategoryID
        {
            get { return _meetingCategoryID; }
            set
            {
                if (_meetingCategoryID != value)
                {
                    ChangeTracker.RecordOriginalValue("MeetingCategoryID", _meetingCategoryID);
      if (!IsDeserializing)
      {
                          if (MeetingCategory != null && MeetingCategory.ID != value)
                        {
                            MeetingCategory = null;
                        }
                    }
                    _meetingCategoryID = value;
                    OnPropertyChanged("MeetingCategoryID");
                }
            }
        }
        private Nullable<System.Guid> _meetingCategoryID;
    
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
        public MeetingCategory MeetingCategory
        {
            get { return _meetingCategory; }
            set
            {
                if (!ReferenceEquals(_meetingCategory, value))
                {
                    var previousValue = _meetingCategory;
                    _meetingCategory = value;
                    FixupMeetingCategory(previousValue);
                    OnNavigationPropertyChanged("MeetingCategory");
                }
            }
        }
        private MeetingCategory _meetingCategory;
    
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
            MeetingCategory = null;
            Tenant = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupMeetings(Meetings previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.MeetingCategoryMap.Contains(this))
            {
                previousValue.MeetingCategoryMap.Remove(this);
            }
    
            if (Meetings != null)
            {
                if (!Meetings.MeetingCategoryMap.Contains(this))
                {
                    Meetings.MeetingCategoryMap.Add(this);
                }
    
                MeetingID = Meetings.ID;
            }
            else if (!skipKeys)
            {
                MeetingID = null;
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
    
        private void FixupMeetingCategory(MeetingCategory previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.MeetingCategoryMap.Contains(this))
            {
                previousValue.MeetingCategoryMap.Remove(this);
            }
    
            if (MeetingCategory != null)
            {
                if (!MeetingCategory.MeetingCategoryMap.Contains(this))
                {
                    MeetingCategory.MeetingCategoryMap.Add(this);
                }
    
                MeetingCategoryID = MeetingCategory.ID;
            }
            else if (!skipKeys)
            {
                MeetingCategoryID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("MeetingCategory")
                    && (ChangeTracker.OriginalValues["MeetingCategory"] == MeetingCategory))
                {
                    ChangeTracker.OriginalValues.Remove("MeetingCategory");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("MeetingCategory", previousValue);
                }
                if (MeetingCategory != null && !MeetingCategory.ChangeTracker.ChangeTrackingEnabled)
                {
                    MeetingCategory.StartTracking();
                }
            }
        }
    
        private void FixupTenant(Tenant previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.MeetingCategoryMap.Contains(this))
            {
                previousValue.MeetingCategoryMap.Remove(this);
            }
    
            if (Tenant != null)
            {
                if (!Tenant.MeetingCategoryMap.Contains(this))
                {
                    Tenant.MeetingCategoryMap.Add(this);
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