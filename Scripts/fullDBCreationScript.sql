drop table Tenant
GO

CREATE TABLE [dbo].[Tenant](
	[ID] [uniqueidentifier] NOT NULL,
	[TenantName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TenantNameUnique] UNIQUE NONCLUSTERED 
(
	[TenantName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Tenant] ADD  CONSTRAINT [DF_Tenant_ID]  DEFAULT (newid()) FOR [ID]
GO

insert into  Tenant values ('61559893-32CA-445C-A686-A45C1D84BE2D','Default')
go

----------------------------------------------------------

drop table TaskStatus
go
CREATE TABLE [dbo].[TaskStatus](
	[ID] [int] NOT NULL,
	[StatusName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TaskStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

insert into [TaskStatus] values (1,'Pending')
insert into [TaskStatus] values (2,'In Progress')
insert into [TaskStatus] values (3,'On Hold')
insert into [TaskStatus] values (4,'Finished')
insert into [TaskStatus] values (5,'Cancelled')
go
----------------------------------------------------------

drop table ConfigTable
go
CREATE TABLE [dbo].[ConfigTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConfigName] [nvarchar](50) NULL,
	[ConfigValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_ConfigTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
insert into  dbo.configtable(configName) values ('DBVer')
go

--------------------------------------------------



drop table Alerts
GO
CREATE TABLE [dbo].[Alerts](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[taskID] [uniqueidentifier] NOT NULL,
	[nextAlert] [datetime] NULL,
	[active] [bit] NULL,
	[sendEmail] [bit] NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Alerts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Alerts] ADD  CONSTRAINT [DF_Alerts_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[Alerts] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

---------------------------------------------------------------------
drop table attachments
go
CREATE TABLE [dbo].[attachments](
	[ID]  [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[bin] [image] NULL,
	[taskID] [uniqueidentifier] NULL,
	[fileName] [nvarchar](100) NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__attachments__0000000000000128] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[attachments] ADD  CONSTRAINT [DF_attachments_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[attachments] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO
---------------------------------------------------------------------
drop table [ManagerTeamMemberRelations]
go
CREATE TABLE [dbo].[ManagerTeamMemberRelations](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ManagerID] [uniqueidentifier] NOT NULL,
	[TeamMemberID] [uniqueidentifier] NOT NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ManagerTeamMemberRelations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ManagerTeamMemberRelations] ADD  CONSTRAINT [DF_ManagerTeamMemberRelations_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[ManagerTeamMemberRelations] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

-----------------------------------------------------------------------
drop table[MeetingAttendies]
go
CREATE TABLE [dbo].[MeetingAttendies](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[MeetingID] [uniqueidentifier] NULL,
	[AttendeeID] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MeetingAttendies] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MeetingAttendies] ADD  CONSTRAINT [DF_MeetingAttendies_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[MeetingAttendies] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

-----------------------------------------------------------------------
drop table [MeetingCategory]
go
CREATE TABLE [dbo].[MeetingCategory](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
	[CategoryOwner] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MeetingCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MeetingCategory] ADD  CONSTRAINT [DF_MeetingCategory_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[MeetingCategory] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO
-----------------------------------------------------------------------
drop table [MeetingCategoryMap]
go

CREATE TABLE [dbo].[MeetingCategoryMap](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[MeetingID] [uniqueidentifier] NULL,
	[MeetingCategoryID] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MeetingCategoryMap] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MeetingCategoryMap] ADD  CONSTRAINT [DF_MeetingCategoryMap_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[MeetingCategoryMap] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

-----------------------------------------------------------------------
drop table [Meetings]
go

CREATE TABLE [dbo].[Meetings](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[MeetingName] [nvarchar](100) NULL,
	[MeetingOwner] [uniqueidentifier] NULL,
	[MeetingDate] [datetime] NULL,
	[MeetingSummaryRTF] [image] NULL,
	[updateDate] [datetime] NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Meetings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Meetings] ADD  CONSTRAINT [DF_Meetings_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Meetings] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

-----------------------------------------------------------------------
drop table [MeetingTasks]
go

CREATE TABLE [dbo].[MeetingTasks](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[MeetingID] [uniqueidentifier] NULL,
	[TaskID] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MeetingTasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MeetingTasks] ADD  CONSTRAINT [DF_MeetingTasks_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[MeetingTasks] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

-----------------------------------------------------------------------
drop table [Notifications]
go

CREATE TABLE [dbo].[Notifications](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[userID] [uniqueidentifier] NULL,
	[projectID] [uniqueidentifier] NULL,
	[NotificationRecipientID] [uniqueidentifier] NOT NULL,
	[NotificationTypeID] [uniqueidentifier] NOT NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Notifications] ADD  CONSTRAINT [DF_Notifications_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[Notifications] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

-----------------------------------------------------------------------
drop table [NotificationType]
go

CREATE TABLE [dbo].[NotificationType](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[notificationTypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_NotificationType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[NotificationType] ADD  CONSTRAINT [DF_NotificationType_ID]  DEFAULT (newid()) FOR [ID]
GO
-----------------------------------------------------------------------
drop table [projects]
go

CREATE TABLE [dbo].[projects](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[projectName] [nvarchar](50) NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_projects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[projects] ADD  CONSTRAINT [DF_projects_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[projects] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

-----------------------------------------------------------------------
drop table [Tasks]
go

CREATE TABLE [dbo].[Tasks](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[priority] [int] NULL,
	[taskName] [nvarchar](256) NULL,
	[estimatedWorkHours] [int] NULL,
	[completionPercentage] [int] NULL,
	[remarks] [ntext] NULL,
	[projectID] [uniqueidentifier] NULL,
	[userID] [uniqueidentifier] NULL,
	[requesterID] [uniqueidentifier] NULL,
	[updateRequester] [bit] NULL,
	[taskStatusID] [int] NULL,
	[defectNumber] [nvarchar](50) NULL,
	[dateEntered] [datetime] NULL,
	[UpdatesLog] [ntext] NULL,
	[dateClosed] [datetime] NULL,
	[actualWorkHours] [int] NULL,
	[dueDate] [datetime] NULL,
	[dateUpdated] [datetime] NOT NULL,
	[hasAttachment] [bit] NULL,
	[hasAlert] [bit] NULL,
	[taskType] [int] NOT NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
	[CopiedFromTaskID] [uniqueidentifier] NULL,
	[privateTask] [bit] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Tasks_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ((0)) FOR [taskType]
GO

ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

-----------------------------------------------------------------------
drop table [Users]
go

CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[domainUserName] [nvarchar](100) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[password] [nvarchar](100) NULL,
	[Activated] [bit] NOT NULL,
	[TemporaryPassword] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('61559893-32ca-445c-a686-a45c1d84be2d') FOR [TenantID]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsAdmin]
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Activated]
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [TemporaryPassword]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_username_unique] ON [dbo].[Users] 
(
	[userName] ASC,
	[TenantID] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

------------------------------------------------------------------------
--add default admin user with password = '1234' ------------------------
insert into users (username,domainusername,email,tenantid,isadmin,[password])
  values ('admin','admin','dont-send-email@to-me.com','61559893-32CA-445C-A686-A45C1D84BE2D',1,'81-DC-9B-DB-52-D0-4D-C2-00-36-DB-D8-31-3E-D0-55')
GO
------------------------------------------------------------------------

/* create new tenant with admin user script with temporary password=1234
declare @tenantName varchar(50)
set @tenantName = 'kantoo'

declare @AdminName varchar(50)
set @AdminName = 'keren k'

declare @AdminEmail varchar(50)
set @AdminEmail = 'kkroglen@gmail.com'

INSERT INTO [prioritizerDB].[dbo].[Tenant]([TenantName]) VALUES(@tenantName)

--add default admin user with password = '1234' ------------------------
declare @tenantID uniqueidentifier
select @tenantID=ID from Tenant where TenantName= @tenantName
insert into users (username,domainusername,email,tenantid,isadmin,[password])
  values (@AdminName,'admin',@AdminEmail,@tenantID ,1,'81-DC-9B-DB-52-D0-4D-C2-00-36-DB-D8-31-3E-D0-55')
GO
*/