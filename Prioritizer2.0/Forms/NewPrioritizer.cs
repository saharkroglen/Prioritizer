using System;
using System.ComponentModel;
using Telerik.WinControls.UI;
using System.Collections.Generic;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Linq;
using Microsoft.VisualBasic;
using System.Text;
using Telerik.WinControls.Data;
using System.Data.Objects;
using System.Media;
using Telerik.WinControls.UI.Export;
using Prioritizer2._0.Forms;
using System.Runtime.Serialization.Formatters.Binary;
//using Netformx.Online.Services.PrioritizerService.Contracts.Data;
//using Netformx.Online.Foundation.SelfTrackingEntities;
using PrioritizerService.Model;
using System.ServiceModel;


namespace Prioritizer2._0
{
    public enum logDelimiterMode
    {
        Added,Modified,Deleted
    }

    public enum formMode
    {
        add,
        update,
        delete
    }

    public partial class NewPrioritizer : Form
    {
        #region form properties

        private static Bitmap _attachmentIcon = null;
        private RadControl _selectedControl;
        public static List<projects> ProjectList = null;
        public static BindingList<Users> usersList = null;
        public static Dictionary<int, string> statusDict;
        //public static Dictionary<int, string> usersDict;
        public static Dictionary<Guid, Users> usersDict;
        public static Dictionary<string, Guid> usersDomainDict;
        public static Dictionary<Guid, string> projectsDict;
        public BindingList<TaskStatus> taskStatusList = null;
        //public static prioritizerDBEntities repository;
        //public static MyServiceClient repository;
        

        public static PrioritizerServiceClient _proxyClient;
        public static PrioritizerServiceClient ProxyClient
        {
            get
            {
                initProxyClient();
                return _proxyClient; 
            }
        }
        
        //public static attachDBEntities attachmentRepository;
        public static object prioritizerDBPath = null;
        public static List<Users> currentMeetingAttendeesList = null;
        public object SelectedMeeting { get { return cboMeetings.SelectedValue; } }
        public Meetings selectedMeeting;

        private MeetingSummaryControl meetingSummaryControl;
        GridViewComboBoxColumn cboProject;
        static DataSet usersDs = null;
        int lastMinimumFormWidth;
        int lastMaximumFormWidth;
        static bool exitForUpgrade = false;
        static DateTime exitForUpgradeTimestamp;
        readonly string LINE_DELIMITER = "-------";
        readonly string DB_CONNECTION_STRING = "metadata=res://*/DAL.csdl|res://*/DAL.ssdl|res://*/DAL.msl;provider=System.Data.SqlServerCe.4.0;provider connection string=\"Data Source={0};Password=1q2w3e4R;Persist Security Info=True\"";
        readonly string ATTACHMENT_DB_CONNECTION_STRING = "metadata=res://*/attachmentsDAL.csdl|res://*/attachmentsDAL.ssdl|res://*/attachmentsDAL.msl;provider=System.Data.SqlServerCe.4.0;provider connection string=\"Data Source={0};Password=1q2w3e4R;Persist Security Info=True\"";
        public readonly string FAILED_TO_OPEN_DB_ERROR_MESSAGE = "failed on open";
        readonly string NO_CONNECTIVITY_ERROR_MSG = "Cannot connect to DB.\nPlease check your connectivity to network or availability of DB file";
        string UserDomainName = "";
        public static Guid loggedInUserID = Guid.Parse("00000000-0000-0000-0000-000000000000");
        Users loggedInUser= null;
        string friendlyUserName = "";
        BindingList<Users> users = null;
        Guid lastSelectedRowIndex = Guid.Parse("00000000-0000-0000-0000-000000000000");
        IList<Tasks> allTaskList = null;
        GridRowElement hoveredRow;
        Point downPoint;
        GridViewRowInfo rowToDrag;
        string defaultSortField = "priority";
        RadSortOrder defaultSortFieldOrder = RadSortOrder.Ascending;
        List<Users> myAllowedUsers = null;
        public static  readonly string DATE_FORMAT_STRING = "{0:d/M/yyyy}";
        //public bool isMeetingMode { get {return radioTasksForMeeting.IsChecked ;} }
        public bool isMeetingTasksMode { get { return radPageView1.SelectedPage.Text.ToLower() == "meeting tasks"; } }
        public bool isUserTasksMode { get { return radPageView1.SelectedPage.Text.ToLower() == "user tasks"; } }
        static readonly string TEMP_DIR = "c:\\temp\\";
        #endregion

        #region CTOR
        
            public NewPrioritizer()
        {
            /*if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                locateMdbFile(true, mdbPathRegistryKey, prioritizerDBDescription);
                

            }*/

            initRepository();
            InitializeComponent();
            
            //initializeDAL();
            initializeNewPrioritizer();
            
        }

        #endregion

        #region initialization methods

        /*private void initializeDAL()
        {
            //main DB
            prioritizerDBPath = Registry.GetValue(prioritizerRegistryKeyRoot, mdbPathRegistryKey, null);
            if (prioritizerDBPath != null)
            {
                initRepository();
            }
            else
            {
                locateMdbFile(true, mdbPathRegistryKey, prioritizerDBDescription);
                prioritizerDBPath = Registry.GetValue(prioritizerRegistryKeyRoot, mdbPathRegistryKey, null);
                initRepository();
            }

            DBUpgrade();

            //attachments DB
            if (prioritizerDBPath != null)
            {
                FindDBDirectory();
                //if (Registry.GetValue(prioritizerRegistryKeyRoot, attachmentDBPathRegistryKey, null) != null)
                //{
                    initAttachmentRepository();
                //}
                //else
                //{
                //    locateMdbFile(true, attachmentDBPathRegistryKey, "Attachments Database");
                //    initAttachmentRepository();
                //}
            }
        }*/

        /*private static void FindDBDirectory()
        {
            string[] pathParts = prioritizerDBPath.ToString().Split('\\');
            DBDirectory = prioritizerDBPath.ToString().Replace(pathParts[pathParts.Count() - 1], "");
        }*/

        /*private void DBUpgrade()
        {
            try
            {
                statusBar.Text = "Upgrading DB...";
                Application.DoEvents();
                List<ConfigTable> configCollection = null;
                try
                {
                    configCollection = checkDBVer();
                }
                catch (Exception ex)
                {
                    repository.ExecuteStoreCommand("CREATE TABLE ConfigTable(	ID int NOT NULL IDENTITY (1, 1),ConfigName nvarchar(50) NULL,ConfigValue nvarchar(50) NULL)");
                    configCollection = checkDBVer();
                }

                ConfigTable dbVer = configCollection.First();
                dbVer.StartTracking();

                int version = Convert.ToInt16(dbVer.ConfigValue);

                if (version < 2)
                {
                    //upgrade to version 2
                    dbVer.ConfigValue = "2";
                }

                if (version < 4)
                {
                    //repository.ExecuteStoreCommand("ALTER TABLE Tasks DROP COLUMN hasAttachment");
                    repository.ExecuteStoreCommand("ALTER TABLE Tasks ADD hasAttachment bit NULL");
                    dbVer.ConfigValue = "4";
                }

                if (version < 5)
                {
                    //repository.ExecuteStoreCommand("ALTER TABLE Tasks DROP COLUMN hasAttachment");
                    repository.ExecuteStoreCommand("CREATE INDEX IDX_USER_ID on Tasks (userID)");
                    dbVer.ConfigValue = "5";
                }

                if (version < 6)
                {
                    repository.ExecuteStoreCommand("CREATE TABLE Meetings(	ID int NOT NULL IDENTITY (1, 1),MeetingName nvarchar(100) NULL,MeetingOwner int NULL,MeetingDate datetime)");
                    repository.ExecuteStoreCommand("CREATE TABLE MeetingAttendies(	ID int NOT NULL IDENTITY (1, 1),MeetingID int NULL,AttendeeID int NULL)");
                    repository.ExecuteStoreCommand("CREATE TABLE MeetingTasks(	ID int NOT NULL IDENTITY (1, 1),MeetingID int NULL,TaskID int NULL)");
                    repository.ExecuteStoreCommand("CREATE TABLE MeetingCategory(	ID int NOT NULL IDENTITY (1, 1),CategoryName nvarchar(100) NULL,CategoryOwner int NULL)");
                    repository.ExecuteStoreCommand("CREATE TABLE MeetingCategoryMap(	ID int NOT NULL IDENTITY (1, 1),MeetingID int NULL,MeetingCategoryID int NULL)");

                    repository.ExecuteStoreCommand("CREATE INDEX IDX_Meetings_meetingOwner on Meetings (MeetingOwner)");
                    repository.ExecuteStoreCommand("CREATE INDEX IDX_MeetingTasks_meetingID on MeetingTasks (MeetingID)");
                    repository.ExecuteStoreCommand("CREATE INDEX IDX_MeetingAttendies_meetingID on MeetingAttendies (MeetingID)");
                    repository.ExecuteStoreCommand("CREATE INDEX IDX_MeetingCategoryMap_meetingID on MeetingCategoryMap (MeetingID)");
                    repository.ExecuteStoreCommand("CREATE INDEX IDX_MeetingCategoryMap_MeetingCategoryID on MeetingCategoryMap (MeetingCategoryID)");
                    
                    dbVer.ConfigValue = "6";
                }

                if (version < 7)
                {
                    //repository.ExecuteStoreCommand("ALTER TABLE Tasks DROP COLUMN hasAttachment");
                    repository.ExecuteStoreCommand("ALTER TABLE Meetings ADD MeetingSummaryRTF image NULL");
                    dbVer.ConfigValue = "7";
                }
                
                
                if (version < 8)
                {
                    //repository.ExecuteStoreCommand("ALTER TABLE Tasks DROP COLUMN hasAttachment");
                    repository.ExecuteStoreCommand("CREATE UNIQUE INDEX IDX_MEETING_TASKS on MeetingTasks (MeetingID,TaskID)");
                    dbVer.ConfigValue = "8";
                }

                if (version < 9)
                {
                    //repository.ExecuteStoreCommand("ALTER TABLE Tasks DROP COLUMN hasAttachment");
                    repository.ExecuteStoreCommand("ALTER TABLE Meetings ADD updateDate datetime NULL");
                    dbVer.ConfigValue = "9";
                }



                if (dbVer.ChangeTracker.State != ObjectState.Unchanged)
                {
                    repository.ConfigTable.ApplyChanges(dbVer);
                    repository.SaveChanges();
                    statusBar.Text = "DB upgrade succeeded to version " + dbVer.ConfigValue;
                    Application.DoEvents();
                }
                else
                {
                    statusBar.Text = "No DB upgrade Needed. (DB ver: " + dbVer.ConfigValue + ")";
                    Application.DoEvents();
                }
                
            }
            catch (Exception ex)
            {
                statusBar.Text = "DB upgrade Failed";
                Application.DoEvents();

                MessageBox.Show(ex.Message, "DB Upgrade Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
            
        }*/

        /*private static List<ConfigTable> checkDBVer()
        {
            string DBVerConfig = "DBVer";
            List<ConfigTable> configCollection = repository.ConfigTable.Where(t => t.ConfigName == DBVerConfig).ToList();
            if (configCollection.Count() == 0)
            {
                configCollection = addDBVerRow(DBVerConfig, configCollection);
            }
            return configCollection;
        }*/

        /*private static List<ConfigTable> addDBVerRow(string DBVerConfig, List<ConfigTable> configCollection)
        {
            ConfigTable dbConfig = new ConfigTable();
            dbConfig.ConfigName = DBVerConfig;
            dbConfig.ConfigValue = "1";
            repository.ConfigTable.ApplyChanges(dbConfig);
            repository.SaveChanges();
            configCollection = repository.ConfigTable.Where(t => t.ConfigName == DBVerConfig).ToList();
            return configCollection;
        }*/

        private void initRepository()
        {
            try
            {
                initProxyClient();
                
                ////object dbPathRegistry = Registry.GetValue(prioritizerRegistryKeyRoot, mdbPathRegistryKey, null);
                //if (prioritizerDBPath == null || prioritizerDBPath == "")
                //{
                //    locateMdbFile(true, mdbPathRegistryKey, prioritizerDBDescription);
                //    prioritizerDBPath = Registry.GetValue(prioritizerRegistryKeyRoot, mdbPathRegistryKey, null);
                //}

                //string connectionString = string.Format(DB_CONNECTION_STRING, prioritizerDBPath.ToString());
                ////repository = new prioritizerDBEntities(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void initProxyClient()
        {
            try
            {                
                if (_proxyClient == null || _proxyClient.State == CommunicationState.Closed || _proxyClient.State == CommunicationState.Faulted)
                {

                    _proxyClient = new PrioritizerServiceClient();
                }
                _proxyClient.ping(Guid.Parse("00000000-0000-0000-0000-000000000000"));
            }
            catch (Exception ex) //if disconnected
            {
                if (ex.Message.Contains("target machine actively refused it") || ex.Message.Contains("The socket connection was aborted"))
                    _proxyClient = new PrioritizerServiceClient();
                else
                    throw ex;
            }
        }

        private static string attachmentDBName = "attachDB.sdf";
        
        //private void initAttachmentRepository()
        //{
        //    //object attachmentDBPathRegistry = Registry.GetValue(prioritizerRegistryKeyRoot, attachmentDBPathRegistryKey, null);
        //    //string connectionString = string.Format(ATTACHMENT_DB_CONNECTION_STRING, attachmentDBPathRegistry.ToString());
        //    string attachmentDBPath = DBDirectory + attachmentDBName;
        //    string connectionString = string.Format(ATTACHMENT_DB_CONNECTION_STRING, attachmentDBPath);
        //    if (File.Exists(attachmentDBPath))
        //        attachmentRepository = new attachDBEntities(connectionString);
        //    else
        //    {
        //        attachmentRepository = null;
        //        radStatusStrip1.Text = "Can't find attachment DB in: " + attachmentDBPath;
        //    }
        //}

        private void initializeNewPrioritizer()
        {

            loadImages();

            this._selectedControl = this.tasksGrid;
            UserDomainName = System.Environment.UserName.ToLower(); //User.Identity.Name;

            setFormCaption();
            loadLookups();
            loadDictionaries();

            if (usersDomainDict.Count > 0 && usersDomainDict.ContainsKey(UserDomainName))
                loggedInUserID = usersDomainDict[UserDomainName];
                       
                loggedInUser = ProxyClient.getUserByID(loggedInUserID);

                if (!usersDomainDict.ContainsKey(UserDomainName))
                {
                    friendlyUserName = UserDomainName;
                }
                else
                    friendlyUserName = usersDict[usersDomainDict[UserDomainName]].userName;

            setAllowedUsers();
            initPageViewControl();
            addRichTextEditor();
            toggleRightLeftSplitterVisibility();

        }

        
        private void addRichTextEditor()
        {
            meetingSummaryControl = new MeetingSummaryControl(this);
            rightLeftSplitter.Panel2.Controls.Add(meetingSummaryControl);
            if (rightLeftSplitter.Panel2.Controls.Count > 0)
            {
                rightLeftSplitter.Panel2.Controls[0].Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
                rightLeftSplitter.Panel2.Controls[0].Dock = DockStyle.Fill;                
            }
        }
                
        private void setFormCaption()
        {
            this.Text = this.Text + " (Domain User: " + UserDomainName + ")";
            object dbPath = Registry.GetValue(prioritizerRegistryKeyRoot, mdbPathRegistryKey, null);
            if (dbPath != null)
                this.Text += " | (DB location: " + dbPath.ToString() + ")";
        }

        private void loadLookups()
        {   
            usersList = new BindingList<Users>(ProxyClient.getUsers(null).OrderBy(p => p.userName).ToList());
            taskStatusList = new BindingList<TaskStatus>(ProxyClient.getTaskStatusList().ToList());
            loadProjectList();
        }

        private void loadDictionaries()
        {
            if (statusDict == null)
            {
                loadTaskStatusDictionary();
            }

            loadUserssDictionary();            
            loadProjectsDictionary();
            
        }

        private void loadTaskStatusDictionary()
        {
            statusDict = new Dictionary<int, string>();
            foreach (var status in taskStatusList)
            {
                statusDict.Add(status.ID, status.StatusName);
            }
        }

        private void loadUserssDictionary()
        {
            usersDict = new Dictionary<Guid, Users>();
            usersDomainDict = new Dictionary<string, Guid>();
            foreach (var user in usersList)
            {
                if (user.domainUserName == null) continue; //ignore domain user names which are null. add enforcement in DAL as well.
                usersDict.Add(user.ID, user);
                usersDomainDict.Add(user.domainUserName.ToLower(), user.ID);
            }
        }

        private void loadProjectsDictionary()
        {
            projectsDict = new Dictionary<Guid, string>();
            foreach (var project in ProjectList)
            {
                projectsDict.Add(project.ID, project.projectName);
            }
        }

        private void loadProjectList()
        {
            ProjectList = ProxyClient.getProjectList(null).OrderBy(p => p.projectName).ToList();

            if (projectsDict != null)
                projectsDict.Clear();
            projectsDict = null;
            loadProjectsDictionary();
        }

        private void BindControls()
        {
            bindMeetingCategoryCombo(false);  
            bindMeetingsCombo(null,cboMeetings,false);
            bindUsersCombo(false);
                      

            foreach (Users u in usersList)
            {
                cboUserToCopyTo.ComboBoxElement.Items.Add(new RadComboBoxItem(u.userName, u.ID));
                cboUserToForwardTo.ComboBoxElement.Items.Add(new RadComboBoxItem(u.userName, u.ID));
            }

            if (cboMeetings.SelectedItem != null)
            {
                btnAttendeesForm.Enabled = ((Meetings)cboMeetings.SelectedItem.DataBoundItem).MeetingOwner == loggedInUserID; ;               
            }
            else
            {
                btnAttendeesForm.Enabled = false;                
            }
        }

        private void BindAttendeesList()
        {
            Guid meetingID = Guid.Parse(cboMeetings.SelectedValue.ToString());
            currentMeetingAttendeesList = ProxyClient.getMeetingAttendeesUserList(meetingID).ToList(); 
            listAttendees.DisplayMember = "userName";
            listAttendees.ValueMember = "ID";
            listAttendees.DataSource = currentMeetingAttendeesList;
        }

        private void bindUsersCombo(bool reloadFromDB)
        {
            if (reloadFromDB || cboUsers1.DataSource == null)
            {
                cboUsers1.ValueMember = "ID";
                cboUsers1.DisplayMember = "userName";
                FilterDescriptor filter = new FilterDescriptor();
                filter.PropertyName = this.cboUsers1.DisplayMember;
                filter.Operator = FilterOperator.Contains;
                cboUsers1.EditorControl.MasterTemplate.FilterDescriptors.Add(filter);

                cboUsers1.DataSource = myAllowedUsers;

                cboUsers1.MultiColumnComboBoxElement.Columns.ToList().ForEach(a => a.IsVisible = false);// ["sql"].IsVisible = false;
                cboUsers1.MultiColumnComboBoxElement.Columns["userName"].IsVisible = true;
                cboUsers1.MultiColumnComboBoxElement.Columns["userName"].Width = 150;
            }
        }

        public void bindMeetingsCombo(Guid? meetingCategory, RadDropDownList comboBox,bool reloadFromDB)
        {
            if (reloadFromDB || comboBox.DataSource == null)
            {
                comboBox.DataSource = null;
                cboMeetings.Text = null;
                cboMeetings.ValueMember = "ID";
                cboMeetings.DisplayMember = "MeetingName";

                if (meetingCategory == null)
                    meetingCategory = Guid.Parse(cboMeetingCategory.SelectedValue.ToString());

                List<Meetings> meetingsList = null;

                meetingsList = ProxyClient.getMeetingList(meetingCategory.Value, loggedInUserID, chkFinished.Checked,null).ToList();

                comboBox.DataSource = meetingsList;
            }
        }



        #endregion
        
        #region private methods


        private void ChangeUser()
        {
            lastSelectedRowIndex = Guid.Parse("00000000-0000-0000-0000-000000000000");
            if (cboUsers1.SelectedValue != null)
                refreshTaskGrid(true);
            else
                return;

            if (cboUsers1.SelectedValue.ToString() != "0")
            {
                Registry.SetValue(prioritizerRegistryKeyRoot, currentUserRegistryKey, cboUsers1.SelectedValue);
            }
        }
        public static bool checkNewVersion()
        {
            string versionFilePath = ConfigurationSettings.AppSettings["versionFilePath"] + @"\ver.txt";
            FileStream fileStream = null;
            StreamReader streamReader = null;
            try
            {

                if (File.Exists(versionFilePath))
                {
                    fileStream = new FileStream(versionFilePath, FileMode.Open,FileAccess.Read);
                    streamReader = new StreamReader(fileStream);
                    string availableVersion = "";
                    while (true)
                    {
                        string line = streamReader.ReadLine();
                        string[] lineSplit = line.Split('=');
                        if (lineSplit[0].ToUpper() == "VER")
                        {
                            availableVersion = lineSplit[1];
                            streamReader.Close();
                            fileStream.Close();
                            break;

                        }
                        if (string.IsNullOrEmpty(line))
                            break;
                    }

                    string currentVer = ConfigurationSettings.AppSettings["currentVer"];
                    if (currentVer != availableVersion)
                    {
                        return true;
                    }

                }
                return false;
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
                if (fileStream != null)
                    fileStream.Close();
            }
        }


        /// <summary>
        /// fill the list of allowed users. these users are the ones current logged in user can view
        /// </summary>
        private void setAllowedUsers()
        {
            myAllowedUsers = new List<Users>();           
            try
            {
                handleNewUser();                                
                var a = ProxyClient.findAllTeamMembersAllowedForUser(loggedInUserID);

                //gather the users into one list of type <Users>
                myAllowedUsers = (from t in a select t.TeamMemberID_UserID).ToList();
                myAllowedUsers.Insert(0, loggedInUser); //add the logged in user as first user in the list
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //log.Info("setAllowedUsers end");
            }
        }

        private void handleNewUser()
        {
            List<Users> u = ProxyClient.getUserByDomainName(UserDomainName,null).ToList();
            users = new BindingList<Users>(u);
            if (users.Count == 0)
            {
                String requestedUserName = Interaction.InputBox(string.Format("New User :'{0}' was recognized.\nA new account is created for you\nContact prioritizer admin to set your team visibility\nPlease enter your name.", UserDomainName),
                    "New User Detected", UserDomainName, -1, -1);
                if (requestedUserName.Length == 0)
                    requestedUserName = UserDomainName;
                try
                {
                    Users newUser = new Users();
                    newUser.domainUserName = UserDomainName;
                    newUser.userName = requestedUserName;

                    if (ProxyClient.getUserByDomainName(UserDomainName,null).Count() > 0)
                        throw new Exception("User already exists");

                    NewPrioritizer.ProxyClient.applyChangesUsers(newUser);
                    //repository.SaveChanges();

                    if (loggedInUser == null)
                        loggedInUser = newUser;
                    users = new BindingList<Users>();


                    initRepository();
                    initializeNewPrioritizer();
                    //MessageBox.Show("New User registration is successfull\nPlease restart Prioritizer application");
                    //System.Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Failed to register user with the following error: '{0}'", ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
                }
            }
        }

        private Image LoadImage(string name)
        {
            Stream stream = Assembly.GetAssembly(this.GetType()).GetManifestResourceStream(name);
            if (stream == null)
            {
                throw new NullReferenceException("Cannot find " + name);
            }
            return new Bitmap(stream);
        }

        private void setFormMaximumWidth()
        {
            lastMinimumFormWidth = this.Width;
            if (lastMaximumFormWidth == 0)
                this.Width = 1000;
            else
                this.Width = lastMaximumFormWidth;
        }

        private void setFormMinimumWidth()
        {
            try
            {
                int constantWidth = 120;

                int width = tasksGrid.Columns["priority"].Width + tasksGrid.Columns["taskStatusID"].Width + tasksGrid.Columns["taskName"].Width + tasksGrid.Columns["projectID"].Width + constantWidth;
                this.Width = width;//width<280 ? 280 : width;
            }
            catch (Exception ex)
            {
                this.Width = 500;
            }

        }

        public void openTaskForm(TaskForm newTaskForm)
        {
            try
            {
                meetingSummaryControl.SaveMeetingRTF();
                newTaskForm.StartPosition = FormStartPosition.CenterParent;
                DialogResult res = newTaskForm.ShowDialog();

                if (res.Equals(DialogResult.OK))
                {                    
                    //refresh grid, new task was entered
                    refreshTaskGrid(true);
                }
                
            }
            catch (Exception ex) {  }
        }

        GridViewRowInfo GetRowAtPoint(RadGridView grid, Point location)
        {
            RadElement element = grid.ElementTree.GetElementAtPoint(location);
            if (element is GridCellElement)
            {
                return ((GridCellElement)element).RowInfo;
            }
            if (element is GridDataRowElement)
            {
                return ((GridRowElement)element).RowInfo;
            }
            return null;
        }

        /*private void forwardTo(int taskID, object targetUser)
        {
            IList<Tasks> currentTaskList = (List<Tasks>)tasksGrid.DataSource;
            Tasks selectedTask = (from t in currentTaskList where t.ID == taskID select t).FirstOrDefault();
            selectedTask.StartTracking();

            //log action
            string loggedInUsername = usersDict[loggedInUserID].userName;
            string log = string.Format("{0}{1}{2} - Forward to '{3}' by '{7}'{4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, usersDict[(int)targetUser].userName, Environment.NewLine, LINE_DELIMITER, Environment.NewLine, loggedInUsername);
            selectedTask.UpdatesLog = log + selectedTask.UpdatesLog;

            selectedTask.userID = Convert.ToInt32(targetUser.ToString());
            logChanges(selectedTask);
            repository.Tasks.ApplyChanges(selectedTask);
            selectedTask.AcceptChanges();
            repository.SaveChanges();
        }*/

        /*private void copyTo(int taskID, object targetUser)
        {
            IList<Tasks> currentTaskList = (List<Tasks>)tasksGrid.DataSource;
            Tasks selectedTask = (from t in currentTaskList where t.ID == taskID select t).FirstOrDefault();
           
            Tasks newTask = selectedTask.Clone();

            newTask.StopTracking();           
            newTask.dateEntered = DateTime.Now;
            newTask.userID = Convert.ToInt32(targetUser.ToString());
            newTask.ChangeTracker.State = ObjectState.Added;
            
            //log action
            string loggedInUserName = usersDict[loggedInUserID].userName;
            string log = string.Format("{0}{1}{2} - Copied to '{3}' by '{7}' {4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, usersDict[(int)newTask.userID].userName, Environment.NewLine, LINE_DELIMITER, Environment.NewLine, loggedInUserName);
            newTask.UpdatesLog = log + newTask.UpdatesLog;

            repository.Tasks.AddObject(newTask);
            logChanges(newTask);
            repository.SaveChanges();
        }*/

        private bool authorizeAccess()
        {
            String password = Interaction.InputBox("Please enter password:", "Password Required", "XXXXXX", -1, -1);
            if (password == "1q2w3e4R")
                return true;
            else
            {
                MessageBox.Show("Wrong password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;                
            }
        }

        #endregion

        #region CRUD
        private void save(bool refreshGrid)
        {
            try
            {
                if (isMeetingTasksMode)
                {
                    meetingSummaryControl.SaveMeetingRTF();
                }

                Guid savedRowIndex = lastSelectedRowIndex;
                tasksGrid.EndEdit();
                tasksGrid.Focus();

                saveTasksChanges();

                //reload from DB
                if (refreshGrid)
                    refreshTaskGrid(true);


                // move a row differet than last selected row to refresh binding
                foreach (GridViewRowInfo row in tasksGrid.Rows)
                {
                    if (Guid.Parse(row.Cells["ID"].Value.ToString()) != lastSelectedRowIndex)
                    {
                        tasksGrid.CurrentRow = row;
                        break;
                    }
                }
                lastSelectedRowIndex = savedRowIndex;
                reSelectLastSelectedRow();

            }
            catch (EntityException ex)
            {
                if (ex.Message.ToLower().Contains(FAILED_TO_OPEN_DB_ERROR_MESSAGE))
                    MessageBox.Show(NO_CONNECTIVITY_ERROR_MSG, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }


        private bool isTaskListDirty()
        {
            if (tasksGrid.DataSource == null)
                return false;

            List<Tasks> taskList = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource); ;
            foreach (Tasks t in taskList)
            {
                if (t.ChangeTracker.State != PrioritizerService.Model.ObjectState.Unchanged)
                    return true;
            }
            return false;
        }
        private void saveTasksChanges()
        {
            //try
            //{
            if (isTaskListDirty())
            {
                if (tasksGrid.DataSource != null)
                {
                    List<Tasks> taskList = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource); // (List<Tasks>)tasksGrid.DataSource;
                    List<Tasks> changedTasksList = new List<Tasks>();
                    foreach (Tasks t in taskList)
                    {
                        saveOneTaskChanges(t, changedTasksList);
                    }

                    List<Tasks> failedTasks = ProxyClient.applyChangesTasksList(changedTasksList.ToArray<Tasks>(), loggedInUserID).ToList<Tasks>();
                    string tasksNames = string.Empty;
                    if (failedTasks.Count() > 0)
                    {
                        foreach (Tasks t in failedTasks)
                        {
                            tasksNames += t.taskName.Substring(0, Math.Min(30, t.taskName.Length)) + "...\n";
                        }
                        MessageBox.Show("Following tasks couldn't be saved. Might be due to concurrent updates by other user:\n" + tasksNames, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void saveOneTaskChanges(Tasks t,List<Tasks> changedTasksList)
        {
            try
            {
                if (t.ChangeTracker.State != PrioritizerService.Model.ObjectState.Unchanged)
                {
                    if (t.userID == null && !this.isMeetingTasksMode)
                        t.userID = Guid.Parse(cboUsers1.SelectedValue.ToString());

                   

                    //repository.applyChangesTasks(t, loggedInUserID);
                    changedTasksList.Add(t);
                    //t.MeetingTasks.ToList().ForEach(a => a.StopTracking());
                    //t.MeetingTasks.ToList().ForEach(a => a.Meetings.StopTracking());
                    //t.MeetingTasks.ToList().ForEach(a => a.Meetings.MeetingTasks.StopTracking());
                    //t.StopTracking();
                    //repository.applyChangesTasks(t,loggedInUserID);
                    //logChanges(t);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Entities may have been modified or deleted since entities were loaded"))
                {
                    
                    MessageBox.Show(string.Format("Task '{0}' \ncouldn't be saved due to concurrent updates by other user.", t.taskName.Substring(0, 30) + "..."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    throw ex;
            }
        }

        /*public void logChanges(Tasks t)
        {
            string log = string.Empty;
            string Delimiterline = string.Empty;
            
            if (t.ChangeTracker.State == ObjectState.Modified) 
            {                
                if (t.ChangeTracker.ChangedProperties.Count > 0)
                {
                    DateTime? dateClosed = t.dateClosed;
                    Delimiterline = getLogDelimiterLine(logDelimiterMode.Modified); //string.Format("{0}{1}{2} - Changed by '{3}':{4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, friendlyUserName, Environment.NewLine, LINE_DELIMITER, Environment.NewLine);
                    foreach (string changedProperty in t.ChangeTracker.ChangedProperties)
                    {
                        PropertyInfo piInstance =
                            typeof(Tasks).GetProperty(changedProperty);
                        var val = piInstance.GetValue(t, null);
                        string currentValue=string.Empty;
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
                                    log += string.Format("Status changed to '{0}'{1}", statusDict[Int32.Parse(currentValue)], Environment.NewLine);
                                }
                                
                                break;
                            case "requesterid":
                                if (currentValue.Length == 0)
                                    log += string.Format("Requested by Cleared{0}", Environment.NewLine);
                                else
                                    log += string.Format("Requester changed to '{0}'{1}", usersDict[Int32.Parse(currentValue)].userName, Environment.NewLine);
                                break;
                            case "projectid":
                                if (currentValue.Length == 0)
                                    log += string.Format("Project Cleared{0}", Environment.NewLine);
                                else
                                {
                                    if (projectsDict.ContainsKey(Int32.Parse(currentValue)))
                                        log += string.Format("Project changed to '{0}'{1}", projectsDict[Int32.Parse(currentValue)], Environment.NewLine);
                                }
                                break;
                            case "remarks":
                                log += string.Format("Remarks Change{0}", Environment.NewLine);
                                break;
                            case "priority":
                                //skip logging changes in priority
                                break;
                            default:
                                if (changedProperty.ToLower() != "updateslog" && changedProperty.ToLower() != "userid")
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
                log = getLogDelimiterLine(logDelimiterMode.Added); //string.Format("{0}{1}{2} - Added by '{3}':{4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, friendlyUserName, Environment.NewLine, LINE_DELIMITER, Environment.NewLine);
            }
            if (log.Length > 0)
            {
                t.UpdatesLog = Delimiterline + log + t.UpdatesLog;
            }
            repository.Tasks.ApplyChanges(t);
            t.AcceptChanges();
            t.StopTracking();
        }
        */


        public string getLogDelimiterLine(logDelimiterMode mode)
        {
            string modeStr = mode.ToString();
            return string.Format("{0}{1}{2} - {7} by '{3}':{4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, friendlyUserName, Environment.NewLine, LINE_DELIMITER, Environment.NewLine,modeStr);

        }


        #endregion

        #region grid


        private void reSelectLastSelectedRow()
        {
            bool rowFound = false;
            //re-select last selected row.
            foreach (GridViewRowInfo row in tasksGrid.Rows)
            {
                if (Guid.Parse(row.Cells["ID"].Value.ToString()) == lastSelectedRowIndex)
                {
                    tasksGrid.CurrentRow = row;

                    /*row.IsSelected = true;
                    row.IsCurrent = true;
                    row.EnsureVisible();*/
                    rowFound = true;
                    break;
                }
            }
            if (!rowFound && tasksGrid.Rows.Count > 1)
            {
                tasksGrid.CurrentRow = tasksGrid.Rows[0];
            }
        }
        
        private void sortMainGrid(string fieldName, RadSortOrder sortOrder)
        {
            tasksGrid.MasterTemplate.SortExpressions.Clear();

            GridSortField sort = new GridSortField(fieldName, sortOrder);
            tasksGrid.MasterTemplate.SortExpressions.Add(sort);
        }

        public void SetTopPriority(Guid taskID)
        {
            ProxyClient.moveAllTasksPriorityForUser(1, Guid.Parse(cboUsers1.SelectedValue.ToString()));

            Tasks t = ProxyClient.getTaskByID(taskID);
            t.StartTracking();
            t.priority = 1;
            ProxyClient.applyChangesTasks(t,loggedInUserID);
            t.AcceptChanges();
            //repository.SaveChanges();

            //}

            refreshTaskGrid(true);
        }

        //public void SetLastPriority(int taskID)
        //{
        //    //using (prioritizerDBEntities repository = new prioritizerDBEntities())
        //    //{
        //        int selectedUser = Convert.ToInt32(cboUsers1.SelectedValue);
        //        int? maxPriority = repository.Tasks.Where(task=> task.userID == selectedUser).Max(task => task.priority);

        //        Tasks t = repository.getTaskByID(taskID);
        //        t.StartTracking();
        //        t.priority = maxPriority + 1;
        //        repository.applyChangesTasks(t);
        //        t.AcceptChanges();
        //        repository.SaveChanges();
        //    //}
        //    refreshTaskGrid(true);
        //}

        public void selectRowByTaskID(Guid taskID)
        {           
            foreach (GridViewRowInfo row in tasksGrid.Rows)
            {
                if (Guid.Parse(row.Cells["ID"].Value.ToString()) == taskID)
                {
                    tasksGrid.CurrentRow = row;
                    break;
                }
            }
        }

        public Tasks getTaskByTaskID(Guid taskID)
        {
            var tasks = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource).Where(t => t.ID == taskID);
            if (tasks.Count()>0)
                return tasks.First();
            else
                return null;
        }

        private void bindToCurrentRow()
        {
            GridViewRowInfo currentSelectedRow = tasksGrid.CurrentRow;
            
            if (currentSelectedRow != null && currentSelectedRow != null)
            {
                remarks.DataBindings.Clear();
                remarks.DataBindings.Add("Text", currentSelectedRow.DataBoundItem, "remarks");

                updateLog.DataBindings.Clear();
                updateLog.DataBindings.Add("Text", currentSelectedRow.DataBoundItem, "UpdatesLog");

            }
        }

        private void BindToDataSet()
        {
            refreshTaskGrid(false);
        }
        
        private void refreshTaskGrid(bool reloadFromDB)
        {
            try
            {               
                loadData(reloadFromDB);
                
                if (tasksGrid.DataSource != null)
                    tasksGrid.DataSource = null;
                tasksGrid.Columns.Clear();
                
                //IList<Tasks> filteredList = filterTaskList();
                allTaskList.ToList().ForEach(i => i.StartTracking()); //start the self tracking for each element
                
                tasksGrid.Rows.CollectionChanged += new Telerik.WinControls.Data.NotifyCollectionChangedEventHandler(Rows_CollectionChanged);

                SetGridProperties();

                bindGrid(allTaskList);

                SetColumnsProperties();
                //SetColumnsWidth();
                setConditionalFormatting();

                sortMainGrid(defaultSortField, defaultSortFieldOrder);

                SetColumnsWidth();

                //RefreshFormWidth();

                //if (reloadFromDB)
                //    lastSelectedRowIndex = -1;
                reSelectLastSelectedRow();
            }
            catch (EntityException ex)
            {
                if (ex.Message.ToLower().Contains(FAILED_TO_OPEN_DB_ERROR_MESSAGE))
                    MessageBox.Show(NO_CONNECTIVITY_ERROR_MSG, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
               
        private void bindGrid(IList<Tasks> taskList)
        {
            tasksGrid.DataSource = taskList;
        }
        
        private void loadData(bool reloadFromDB)
        {
            try
            {
                if (allTaskList == null || reloadFromDB)
                {
                    Guid selectedUser = Guid.Parse(cboUsers1.SelectedValue.ToString());
                    Guid selectedMeetingID = Guid.Parse(cboMeetings.SelectedValue.ToString());
                    /*if (allTaskList != null)
                    {
                        repository.Refresh(RefreshMode.StoreWins, allTaskList);
                        repository.Refresh(RefreshMode.StoreWins, ProjectList);
                        repository.Refresh(RefreshMode.StoreWins, repository.Meetings);
                    }*/
                    if (isUserTasksMode)
                    {
                        loadTasksForUser(selectedUser);
                    }
                    else if (isMeetingTasksMode)
                    {
                        loadTasksForMeeting(selectedMeetingID);
                    }

                    loadProjectList();
                }
            }
            catch (EntityException ex)
            {
                if (ex.Message.ToLower().Contains(FAILED_TO_OPEN_DB_ERROR_MESSAGE))
                    MessageBox.Show(NO_CONNECTIVITY_ERROR_MSG, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }

        public void setSelectedMeeting(Guid meetingID, bool includeTasks)
        {
            selectedMeeting = ProxyClient.getMeetingByID(meetingID, includeTasks);
        }



        private void loadTasksForMeeting(Guid selectedMeetingID)
        {
            setSelectedMeeting(selectedMeetingID,true);
            if (selectedMeeting != null)
                allTaskList = ProxyClient.getTasksForMeeting(selectedMeetingID).ToList();// selectedMeeting.MeetingTasks.Select(a => a.Tasks).ToList();
            else if (allTaskList != null)
                allTaskList.Clear();
            
            if (selectedMeeting != null && selectedMeeting.MeetingSummaryRTF != null)
                meetingSummaryControl.loadRTFDocument(new MemoryStream(selectedMeeting.MeetingSummaryRTF));
            else
                meetingSummaryControl.createNewDocument();

            addTasksFromRTFToMeetingTasks();

        }

        /// <summary>
        /// add to the allTaskList all tasks mentioned in the RTF document and not included in that
        /// meeting already.
        /// this is to support a situation where an RTF document holds references to AI's that where finished/cancelled
        /// already in previous/different meetings but we still would like to see them in the tasks list and get 
        /// updates on their status within the meeting view.
        /// </summary>
        private void addTasksFromRTFToMeetingTasks()
        {
            List<Guid> tasksInRTFDocument = meetingSummaryControl.getTasksInDocument();
            var taskIDs = tasksInRTFDocument.ToArray<Guid>();
            IList<Tasks> additionalTasksBoundToRTFDocument = ProxyClient.getTasksByIDs(taskIDs).ToList<Tasks>();
            foreach (var task in additionalTasksBoundToRTFDocument)
            {
                var tasks = allTaskList.Where(t=>t.ID == task.ID);
                if (tasks.Count() == 0)
                    allTaskList.Add(task);
            }
        }

        private void loadTasksForUser(Guid selectedUser)
        {
            allTaskList = ProxyClient.getTasksForUser(chkFinished.Checked, chkCancelled.Checked, selectedUser).ToList();            
        }

      
        void setConditionalFormatting()
        {
            //in progress task in orange
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("orange", ConditionTypes.Equal, "2", "", true);
            c1.RowBackColor = getColorForStatus("2");
            //c1.RowForeColor = Color.Black;
            tasksGrid.Columns["taskStatusID"].ConditionalFormattingObjectList.Add(c1);

            //finished task in green
            ConditionalFormattingObject c2 = new ConditionalFormattingObject("green", ConditionTypes.Equal, "4", "", true);
            c2.RowBackColor = getColorForStatus("4");
            //c2.RowForeColor = Color.Black;
            tasksGrid.Columns["taskStatusID"].ConditionalFormattingObjectList.Add(c2);

            //cancelled task in red
            ConditionalFormattingObject c3 = new ConditionalFormattingObject("red", ConditionTypes.Equal, "5", "", true);
            c3.RowBackColor = getColorForStatus("5");
            //c3.RowForeColor = Color.Black;
            tasksGrid.Columns["taskStatusID"].ConditionalFormattingObjectList.Add(c3);
        }
        
        public static string getStatusName(int statusID)
        {
            switch (statusID)
            {
                case 1:
                    return "Pending";
                    break;
                case 2:
                    return "In Progress";
                    break;
                case 3:
                    return "On Hold";
                    break;
                case 4:
                    return "Finished";
                    break;
                case 5:
                    return "Cancelled";
                    break;
                default:
                    return "N/A";
            }
        }

        public Color getColorForStatus(string statusID)
        {
            switch (statusID)
            {
                case "2":
                    return Color.Orange;
                    break;
                case "4":
                    return Color.GreenYellow;
                    break;
                case "5":
                    return Color.Red;
                    break;
                default:
                    return Color.White;
            }
        }

        private void SetGridProperties()
        {
            tasksGrid.MasterTemplate.AllowDeleteRow = false;
            tasksGrid.MasterTemplate.AllowAddNewRow = true;
            tasksGrid.MasterTemplate.AutoGenerateColumns = true;
            tasksGrid.MasterTemplate.EnableGrouping = true;
            tasksGrid.MasterTemplate.EnableFiltering = true;
            tasksGrid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            tasksGrid.MasterTemplate.AllowRowResize = true;
            tasksGrid.MasterTemplate.AllowColumnResize = true;
            tasksGrid.GridElement.TableHeaderHeight = 50;
            tasksGrid.EnableHotTracking = true;

        }

        private void initPageViewControl()
        {
            radPageView1.ViewElement.ShowItemCloseButton = false;
        }

        private void SetColumnsProperties()
        {

            foreach (GridViewColumn col in tasksGrid.MasterTemplate.Columns)
            {
                //col.WrapText = true;
                col.AllowResize = true;
            }


            hideUnnecessaryColumns();
            
            setColumnsWidthAndHeader();
            
            removeAutoGeneratedColumns();

            addEmbeddedColumnControls();

            reorderColumns();




        }

        private void reorderColumns()
        {
            //reorder columns
            //tasksGrid.Columns.Move(tasksGrid.MasterTemplate.Columns["attachmentColumn"].Index, 1);
            if (isUserTasksMode)
                tasksGrid.Columns.Move(tasksGrid.MasterTemplate.Columns["priority"].Index, 2);
            else if (isMeetingTasksMode)
                tasksGrid.Columns.Move(tasksGrid.MasterTemplate.Columns["userID"].Index, 2);

            tasksGrid.Columns.Move(tasksGrid.MasterTemplate.Columns["taskName"].Index, 3);
            tasksGrid.Columns.Move(tasksGrid.MasterTemplate.Columns["taskStatusID"].Index, 4);
            tasksGrid.Columns.Move(tasksGrid.MasterTemplate.Columns["ProjectID"].Index, 5);
            tasksGrid.Columns.Move(tasksGrid.MasterTemplate.Columns["requesterID"].Index, 6);
            tasksGrid.Columns.Move(tasksGrid.MasterTemplate.Columns["meetingName"].Index, 7);           

        }

        private readonly string attachColName = "attachmentColumn";
        
        private void addEmbeddedColumnControls()
        {
            if (isMeetingTasksMode)
            {
                //add new lookup column instead of userID column which was hidden only in the case
                //of "tasks for meetings" mode
                GridViewComboBoxColumn userDropDownList = new GridViewComboBoxColumn();
                userDropDownList.FieldName = "userID";
                userDropDownList.ValueMember = "ID";
                userDropDownList.DisplayMember = "Username";
                userDropDownList.DataSource = usersList;
                userDropDownList.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
                userDropDownList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tasksGrid.Columns.Add(userDropDownList);
                tasksGrid.MasterTemplate.Columns["userID"].HeaderText = "Assigned To";
            }

            //attachment column
            GridViewImageColumn attachmentColumn = new GridViewImageColumn();
            attachmentColumn.Name = attachColName;
            attachmentColumn.HeaderImage = _attachmentIcon;
            attachmentColumn.ImageLayout = ImageLayout.None;
            attachmentColumn.Width = 20;
            tasksGrid.MasterTemplate.Columns.Insert(0, attachmentColumn);

            //add new lookup column instead of projectID column which was removed
            cboProject = new GridViewComboBoxColumn();//GridViewComboBoxColumn
            cboProject.FieldName = "ProjectID";
            cboProject.ValueMember = "ID";
            cboProject.DisplayMember = "projectName";
            cboProject.AllowGroup = true;
            cboProject.DataSource = ProjectList;
            cboProject.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            cboProject.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboProject.WrapText = true;
            tasksGrid.Columns.Add(cboProject);
            tasksGrid.MasterTemplate.Columns["ProjectID"].HeaderText = "Project";
                     
            GridViewDateTimeColumn dueDate = new GridViewDateTimeColumn();
            dueDate.Name = "dueDate";
            dueDate.HeaderText = "Due Date";
            dueDate.FieldName = "dueDate";
            dueDate.FormatString = DATE_FORMAT_STRING;

            tasksGrid.MasterTemplate.Columns.Add(dueDate);


            GridViewDateTimeColumn dateEntered = new GridViewDateTimeColumn();
            dateEntered.Name = "dateEntered";
            dateEntered.HeaderText = "Created";
            dateEntered.FieldName = "dateEntered";
            dateEntered.FormatString = DATE_FORMAT_STRING;


            tasksGrid.MasterTemplate.Columns.Add(dateEntered);

            GridViewDateTimeColumn dateClosed = new GridViewDateTimeColumn();
            dateClosed.Name = "dateClosed";
            dateClosed.HeaderText = "Closed";
            dateClosed.FieldName = "dateClosed";
            dateClosed.FormatString = DATE_FORMAT_STRING;

            tasksGrid.MasterTemplate.Columns.Add(dateClosed);

            //add new lookup column instead of RequesterID column which was removed
            GridViewComboBoxColumn requesterDropDownlist = new GridViewComboBoxColumn();
            requesterDropDownlist.FieldName = "requesterID";
            requesterDropDownlist.ValueMember = "ID";
            requesterDropDownlist.DisplayMember = "Username";
            requesterDropDownlist.DataSource = usersList;
            requesterDropDownlist.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            requesterDropDownlist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tasksGrid.Columns.Add(requesterDropDownlist);
            tasksGrid.MasterTemplate.Columns["requesterID"].HeaderText = "Requested By";

            //add new lookup column instead of taskStatusID column which was removed
            GridViewComboBoxColumn statusDropDownlist = new GridViewComboBoxColumn();
            statusDropDownlist.FieldName = "taskStatusID";
            statusDropDownlist.DataSource = taskStatusList;
            statusDropDownlist.ValueMember = "ID";
            statusDropDownlist.DisplayMember = "statusName";
            statusDropDownlist.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            statusDropDownlist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tasksGrid.Columns.Add(statusDropDownlist);
            tasksGrid.MasterTemplate.Columns["taskStatusID"].HeaderText = "Status";
        }

        private void setColumnsWidthAndHeader()
        {
            tasksGrid.MasterTemplate.Columns["taskName"].HeaderText = "Name";
            tasksGrid.MasterTemplate.Columns["taskName"].StretchVertically = true;
            tasksGrid.MasterTemplate.Columns["taskName"].WrapText = true;
            //tasksGrid.MasterTemplate.Columns["taskName"].;

            tasksGrid.MasterTemplate.Columns["estimatedWorkHours"].HeaderText = "Estimated Hours";
            tasksGrid.MasterTemplate.Columns["estimatedWorkHours"].TextAlignment = ContentAlignment.MiddleCenter;

            tasksGrid.MasterTemplate.Columns["completionPercentage"].HeaderText = "% complete";
            tasksGrid.MasterTemplate.Columns["completionPercentage"].TextAlignment = ContentAlignment.MiddleCenter;

            tasksGrid.MasterTemplate.Columns["updateRequester"].HeaderText = "Update Requester";
            tasksGrid.MasterTemplate.Columns["updateRequester"].TextAlignment = ContentAlignment.MiddleCenter;

            tasksGrid.MasterTemplate.Columns["requesterID"].HeaderText = "Requested By";
            tasksGrid.MasterTemplate.Columns["requesterID"].TextAlignment = ContentAlignment.MiddleCenter;
            tasksGrid.MasterTemplate.Columns["defectnumber"].HeaderText = "Defect #";

            tasksGrid.MasterTemplate.Columns["actualWorkHours"].HeaderText = "Actual Work (hours)";
            tasksGrid.MasterTemplate.Columns["actualWorkHours"].TextAlignment = ContentAlignment.MiddleCenter;

            tasksGrid.MasterTemplate.Columns["meetingName"].HeaderText = "Meeting Name";
            tasksGrid.MasterTemplate.Columns["meetingName"].StretchVertically = true;

        }

        private void removeAutoGeneratedColumns()
        {
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["dirty"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["Requester"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["Project"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["ProjectID"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["requesterID"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["taskStatusID"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["changeTracker"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["dateUpdated"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["updateRequester"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["dueDate"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["dateEntered"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["dateClosed"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["MeetingTasks"]);
            tasksGrid.Columns.Remove(tasksGrid.MasterTemplate.Columns["userID"]);
            
        }

        private void SetColumnsWidth()
        {
            try
            {
                //tasksGrid.MasterTemplate.Columns["priority"].Width = 30;
                //tasksGrid.MasterTemplate.Columns["taskName"].BestFit();
                //tasksGrid.MasterTemplate.Columns["estimatedWorkHours"].Width = 70;
                //tasksGrid.MasterTemplate.Columns["completionPercentage"].BestFit();
                ////tasksGrid.MasterTemplate.Columns["updateRequester"].BestFit();
                //tasksGrid.MasterTemplate.Columns["requesterID"].BestFit();
                //tasksGrid.MasterTemplate.Columns["defectnumber"].BestFit();
                //tasksGrid.MasterTemplate.Columns["actualWorkHours"].Width = 70;
                //tasksGrid.MasterTemplate.Columns["ProjectID"].BestFit();
                //tasksGrid.MasterTemplate.Columns["dueDate"].Width = 70;
                //tasksGrid.MasterTemplate.Columns["dateEntered"].Width = 70;
                //tasksGrid.MasterTemplate.Columns["dateClosed"].Width = 70;
                //tasksGrid.MasterTemplate.Columns["taskStatusID"].Width = 70;
                //tasksGrid.MasterTemplate.Columns["meetingName"].Width = 70;
                //if (isMeetingTasksMode)
                //    tasksGrid.MasterTemplate.Columns["userID"].Width = 70;

                tasksGrid.MasterGridViewTemplate.BestFitColumns();// = true;
            }
            catch (Exception ex)
            {
                //do something...
            }

        }

        private void reorderRowsByPriority(Guid sourceRowid, int sourcePriority, int targetPriority)
        {
            if (sourcePriority == targetPriority)
            {
                MessageBox.Show("Failed to reorder - Both source and target tasks have  same priority\nPlease change one of their priorities manually then save and retry","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            List<Tasks> taskList = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource); ;
            if (targetPriority != -1)
            {
                if (sourcePriority < targetPriority)
                {
                    foreach (var row in taskList)
                    {
                        if (row.priority > sourcePriority && row.priority <= targetPriority)
                            row.priority -= 1;
                        if (row.ID == sourceRowid)
                            row.priority = targetPriority;
                    }
                }
                else
                {
                    foreach (var row in taskList)
                    {
                        if (row.priority < sourcePriority && row.priority >= targetPriority)
                            row.priority += 1;
                        if (row.ID == sourceRowid)
                            row.priority = targetPriority;
                    }
                }


                BindToDataSet();
            }
        }

        private void hideUnnecessaryColumns()
        {
            tasksGrid.MasterTemplate.Columns["ID"].IsVisible = false;
            
            if (isUserTasksMode)
                tasksGrid.MasterTemplate.Columns["priority"].IsVisible = true;
            else
                tasksGrid.MasterTemplate.Columns["priority"].IsVisible = false;

            //tasksGrid.MasterTemplate.Columns["userID"].IsVisible = false;
            tasksGrid.MasterTemplate.Columns["remarks"].IsVisible = false;
            tasksGrid.MasterTemplate.Columns["updatesLog"].IsVisible = false;
            tasksGrid.MasterTemplate.Columns["hasAttachment"].IsVisible = false;
        }


        #endregion
        
        #region Events

        private void mnuClosedLastWeek_Click(object sender, EventArgs e)
        {
            CompositeFilterDescriptor compositeFilter = new CompositeFilterDescriptor();
            compositeFilter.FilterDescriptors.Add(new FilterDescriptor("dateClosed", FilterOperator.IsGreaterThan, DateTime.Now.AddDays(-8)));
            compositeFilter.FilterDescriptors.Add(new FilterDescriptor("dateClosed", FilterOperator.IsLessThanOrEqualTo, DateTime.Now));
            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
            this.tasksGrid.FilterDescriptors.Add(compositeFilter);
        }

        private void chkFinished_CheckedChanged(object sender, EventArgs e)
        {
            refreshPrioritizer();
            bindMeetingsCombo(null,cboMeetings,true);
            
        }

        private void chkCancelled_CheckedChanged(object sender, EventArgs e)
        {
            refreshPrioritizer();            
        }
        private void radGridView1_GroupSumaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {

            if (e.SummaryItem.FieldName == "ProjectID")
            {
                string projectGroupName = "";
                Guid projectID = Guid.Parse(e.Group[0].Cells["ProjectID"].Value.ToString());
                if (projectID == null)
                    projectGroupName = "Not Assigned";
                else
                {
                    var projectName = (from p in ProjectList where p.ID == projectID select p.projectName).FirstOrDefault<string>();
                    projectGroupName = projectName;
                }
                e.FormatString = projectGroupName;
            }
        }

        private void radGridView1_DragDrop(object sender, DragEventArgs e)
        {
            dragDrop(e, true);
        }
        private void radGridView1_DragEnter(object sender, DragEventArgs e)
        {
            dragEnter(e, true);
        }

        private void NewPrioritizer_DragEnter(object sender, DragEventArgs e)
        {
            dragEnter(e, false);
        }

        private void NewPrioritizer_DragDrop(object sender, DragEventArgs e)
        {
            dragDrop(e, false);
        }

        private void mnuNewTask_Click(object sender, EventArgs e)
        {
            TaskForm newTaskForm = new TaskForm(this, new Tasks(), formMode.add, enTaskType.ActionItem);
            openTaskForm(newTaskForm);
        }

        private void cboUserToCopyTo_ComboBoxElement_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboUserToCopyTo.ComboBoxElement.SelectedIndex != -1)
            {
                Guid taskID = Guid.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
                var targetUser = ((Telerik.WinControls.UI.RadComboBoxElement)(sender)).SelectedValue;

                ProxyClient.copyTo(taskID, Guid.Parse(targetUser.ToString()), loggedInUserID);
                refreshTaskGrid(true);
                cboUserToCopyTo.ComboBoxElement.SelectedIndex = -1;
                this.tasksGrid.Focus();
            }
        }

        private void cboUserToForwardTo_ComboBoxElement_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboUserToForwardTo.ComboBoxElement.SelectedIndex != -1)
            {
                Guid taskID = Guid.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
                Guid targetUser = Guid.Parse(((Telerik.WinControls.UI.RadComboBoxElement)(sender)).SelectedValue.ToString());
                ProxyClient.forwardTo(taskID, targetUser, loggedInUserID);
                refreshTaskGrid(true);
                cboUserToForwardTo.ComboBoxElement.SelectedIndex = -1;
                this.tasksGrid.Focus();
            }
        }

        private void mnuCopyToMyself_Click(object sender, EventArgs e)
        {
            if (tasksGrid.CurrentRow.Cells["ID"].Value != null)
            {
                Guid taskID = Guid.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
                //copyTo(taskID, loggedInUserID);
                ProxyClient.copyTo(taskID, loggedInUserID, loggedInUserID);
                refreshTaskGrid(true);
            }
            else
                MessageBox.Show("Please select a row to copy", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tasksGrid_UserAddedRow(object sender, GridViewRowEventArgs e)
        {
            Tasks addedTask = (Tasks)e.Row.DataBoundItem;

            if (isMeetingTasksMode)
            {
                Guid meetingID = Guid.Parse(cboMeetings.SelectedValue.ToString());
                assignTaskToMeeting(addedTask,meetingID);
            }
            addedTask.taskStatusID = 1;//pending
            addedTask.requesterID = loggedInUserID;
            addedTask.userID = Guid.Parse(cboUsers1.SelectedValue.ToString());
            addedTask.dateUpdated = DateTime.Now;

            //saveOneTaskChanges(repository, addedTask);
            //repository.applyChangesTasks(addedTask,loggedInUserID);
            addedTask = ProxyClient.addTask(addedTask, loggedInUserID);
            //repository.SaveChanges();
            lastSelectedRowIndex = addedTask.ID;
            SetTopPriority(addedTask.ID);
            
        }

        public MeetingTasks assignTaskToMeeting(Tasks addedTask, Guid? meetingID)
        {
            if (meetingID.HasValue)
            {
                if (addedTask.MeetingTasks.Where(a => a.MeetingID == meetingID.Value).Count() == 0)
                {
                    MeetingTasks ms = new MeetingTasks();
                    //int meetingID = Convert.ToInt16(cboMeetings.SelectedValue);
                    ms.TaskID = addedTask.ID;
                    ms.MeetingID = meetingID.Value;
                    addedTask.MeetingTasks.Add(ms);
                    return ms;
                }
                else
                {
                    throw new UpdateException("Duplicate meeting - task is already assigned to this meeting");
                }
            }
            return null;
        }

        private void radMenuItem5_Click(object sender, EventArgs e)
        {

        }

        public static string prioritizerDBDescription = "Prioritizer Database";
        private void mnuChangeDB_Click(object sender, EventArgs e)
        {
            /*locateMdbFile(false,mdbPathRegistryKey,prioritizerDBDescription);
            initRepository();
            MessageBox.Show("Attach to new DB succeeded. Please restart prioritizer", "Attach To DB", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Environment.Exit(0);*/
        }

        private void radMenuItem9_Click(object sender, EventArgs e)
        {

        }

        private void mnuAuthorization_Click(object sender, EventArgs e)
        {

            //String password = Interaction.InputBox("Please enter password:","Password Required", "XXXXXX", -1, -1);
            if (authorizeAccess())
            {
                AuthorizationForm authForm = new AuthorizationForm();
                authForm.StartPosition = FormStartPosition.CenterParent;
                authForm.ShowDialog();
            }
        }

        private void mnuUsers_Click(object sender, EventArgs e)
        {
            if (authorizeAccess())
            {
                UsersForm userForm = new UsersForm();
                userForm.StartPosition = FormStartPosition.CenterParent;
                userForm.ShowDialog();
            }
        }

        private void radGridView1_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            //if (e.Action == NotifyCollectionChangedAction.Add)
            //{

            //}
            //if (e.Action== Telerik.wincontrols.data.notifycollectionchangedaction.add)
            //{
            //    int numberofrows = ((MasterTemplate)sender).rowcount;
            //    bindinglist<tasks> rowslist = (bindinglist<tasks>)((MasterTemplate)sender).datasource;
            //    var maxpriority = 1;
            //    if (numberofrows > 0)
            //    {
            //        tasks newlyaddedtask = rowslist[numberofrows - 1];
            //        maxpriority = (from m in rowslist
            //                       select m.priority).max();
            //    }
            //    tasks t = new tasks(); //auditable.makeauditable<tasks>();
            //    //newlyaddedtask.copyto(t);
            //    t.userid = convert.toint16(cbousers1.selectedvalue);
            //    t.taskstatusid = 1;//pending

            //    t.priority = maxpriority + 1;
            //    t.dateclosed = null;
            //    t.dateentered = datetime.now;
            //    t.duedate = null;
            //    t.projectid = null;
            //    //auditable.clearauditlog(t);
            //    //auditable.audit(t, new auditobject { status = "new task" });

            //    //rowslist[numberofrows == 0 ? 1 : numberofrows - 1] = t;//convert to dynamicproxy version of the tasks object (which is an nhibernate object)
            //    rowslist.add(t);

            //}
        }

       

        private void refreshPrioritizer()
        {
            Guid savedRowIndex = lastSelectedRowIndex;
            refreshTaskGrid(true);
            /*if (isMeetingTasksMode)
                loadTasksForMeeting(Convert.ToInt16(cboMeetings.SelectedValue));*/
            lastSelectedRowIndex = savedRowIndex;
            reSelectLastSelectedRow();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save(true);
        }

        private void addProject_Click(object sender, EventArgs e)
        {
            string newProjName = Interaction.InputBox("Enter Project Name", "Add New Project", "", -1, -1);
            if (newProjName.Trim().Length == 0)
            {
                MessageBox.Show("Project name can't be an empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            projects p = new projects() { projectName = newProjName };
            //using (prioritizerDBEntities repository = new prioritizerDBEntities())
            //{
            ProxyClient.applyChangesProjects(p,null);
            //repository.SaveChanges();
            loadProjectList();
            //}

            cboProject.DataSource = ProjectList;

        }

        void Rows_CollectionChanged(object sender, Telerik.WinControls.Data.NotifyCollectionChangedEventArgs e)
        {
            int x = 0;
        }
                      
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                statusBar.Text = "Saving...";
                Application.DoEvents();
                //Thread.Sleep(500);
                save(true);
                statusBar.Text = "Save Done";
                Application.DoEvents();
                //Thread.Sleep(500);
                statusBar.Text = "";
                return true;
            }

            if (keyData == (Keys.Control | Keys.N))
            {
                tasksGrid.Focus();
                tasksGrid.MasterGridViewInfo.TableAddNewRow.IsCurrent=true;
                if (isMeetingTasksMode)
                    tasksGrid.MasterGridViewInfo.TableAddNewRow.Cells["userID"].BeginEdit();
                else if (isUserTasksMode)
                    tasksGrid.MasterGridViewInfo.TableAddNewRow.Cells["taskName"].BeginEdit();
                //tasksGrid.BeginEdit();
                return true;
            }

            if (keyData == (Keys.F5))
            {
                statusBar.Text = "Refreshing...";
                Application.DoEvents();                
                refresh();                
                statusBar.Text = "";
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
 
        private void OnFormLoad(object sender, EventArgs e)
        {            
            radPageView1.SelectedPage = tabUserTasks;
            BindControls();
            
            
            object currUser;
            if ((currUser = Registry.GetValue(prioritizerRegistryKeyRoot, currentUserRegistryKey, null)) != null)
            {
                Guid userID = Guid.Parse(currUser.ToString());

                 var u = (from user in myAllowedUsers
                          where user.ID == userID
                          select user).FirstOrDefault<Users>();

                if (u == null)
                    cboUsers1.SelectedValue = myAllowedUsers[0].ID;
                else
                    cboUsers1.SelectedValue = u.ID;
                    
            }

           
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    save();
        //}

        private void tasksDS_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                DataTable dt = ((DataSet)tasksGrid.DataSource).Tables[0];
                save(true);
            }
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            ToggleFormWidth();
        }

        private void ToggleFormWidth()
        {
            /*if (isFormCollapsed())
            {
                tasksGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                btnMore.Text = "<<";
                setFormMaximumWidth();
            }
            else if (isFormExpanded())
            {
                tasksGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;                
                btnMore.Text = ">>";
                setFormMinimumWidth();
            }*/
        }
      

        //private bool isFormExpanded()
        //{
        //    return btnMore.Text == "<<";
        //}

        //private bool isFormCollapsed()
        //{
        //    return btnMore.Text == ">>";
        //}

        private void RefreshFormWidth()
        {            
            //tasksGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;

            /*if (btnMore.Text == "<<" || this.WindowState == FormWindowState.Maximized)
            {
                setFormMaximumWidth();
                tasksGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            }
            else if (btnMore.Text == ">>")
            {
                setFormMinimumWidth();
                tasksGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            }*/
        }

        private void radGridView1_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (tasksGrid.CurrentRow != null && tasksGrid.CurrentRow.DataBoundItem != null)
            {                
                lastSelectedRowIndex = Guid.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
                bindToCurrentRow();

                if (isMeetingTasksMode)
                {
                    meetingSummaryControl.saveCaretPosition();                    
                    meetingSummaryControl.focusOnHyperlink(lastSelectedRowIndex);
                }
            }

        }

        private void txtRemarks_Leave(object sender, EventArgs e)
        {
            txtRemarks.Update();
        }

        public Tasks SelectedTask
        {
            get {
                if (tasksGrid.CurrentRow.Index == -1 && tasksGrid.CurrentRow == null)
                    return null;
                //IList<Tasks> taskList = ((List<Tasks>)tasksGrid.DataSource).ToList();
                //return taskList[tasksGrid.CurrentRow.Index]; 
                return (Tasks)tasksGrid.CurrentRow.DataBoundItem;
            }
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (tasksGrid.CurrentRow.Cells["ID"].Value == null)
                return;
            if (tasksGrid.Rows[0].IsCurrent)
                return; //no higher than this

            int selectedRowID = Int32.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
            DataRow prevRow = null;
            int selectedRowIndex = tasksGrid.CurrentRow.Index;
            IList<Tasks> taskList = (new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource)).OrderBy(t => t.priority).ToList();

            //swap priorities
            int? nextItemPriority = taskList[selectedRowIndex - 1].priority;
            int? selectedItemPriority = taskList[selectedRowIndex].priority;
            if (nextItemPriority == selectedItemPriority)
            {
                //see if more priority duplicates exists up the road
                for (int idx = selectedRowIndex - 2, shift = -2; idx + 1 >0; idx--, shift--)
                {
                    if (taskList[selectedRowIndex].priority == taskList[idx].priority)
                        taskList[idx].priority += shift;
                }
                taskList[selectedRowIndex].priority -= 1;
            }
            else
            {
                int? tempPriority = taskList[selectedRowIndex - 1].priority;
                taskList[selectedRowIndex - 1].priority = taskList[selectedRowIndex].priority;
                taskList[selectedRowIndex].priority = tempPriority;
            }

            tasksGrid.DataSource = taskList.OrderBy(t => t.priority).ToList();
            tasksGrid.Rows[selectedRowIndex - 1].IsCurrent = true; //re-select the last selected row
            
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (tasksGrid.CurrentRow.Cells["ID"].Value == null)
                return;
            int selectedRowID = Int32.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());            
            int selectedRowIndex = tasksGrid.CurrentRow.Index;
            IList<Tasks> taskList = (new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource)).OrderBy( t=> t.priority).ToList();
            
            //swap priorities
            if (selectedRowIndex + 1 > taskList.Count - 1)
                return;

            int? nextItemPriority = taskList[selectedRowIndex + 1].priority;
            int? selectedItemPriority = taskList[selectedRowIndex].priority;
            if (nextItemPriority == selectedItemPriority)
            {   
                //see if more priority duplicates exists down the road
                for (int idx = selectedRowIndex+2, shift=2;idx + 1 < taskList.Count - 1;idx++,shift++)
                {
                    if (taskList[selectedRowIndex].priority == taskList[idx].priority)
                        taskList[idx].priority += shift;
                }
                taskList[selectedRowIndex].priority+= 1;
            }
            else
            {
                int? tempPriority = taskList[selectedRowIndex + 1].priority;
                taskList[selectedRowIndex + 1].priority = taskList[selectedRowIndex].priority;
                taskList[selectedRowIndex].priority = tempPriority;
            }

            tasksGrid.DataSource = taskList;
           
            tasksGrid.Rows[selectedRowIndex].IsCurrent = true; //re-select the last selected row
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            

        }

        private void btnTopPriority_Click(object sender, EventArgs e)
        {
            //if (tasksGrid.Rows[0].IsCurrent)
            //    return; //no higher than this

            //int selectedRowID = Int32.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
            //SetTopPriority(selectedRowID);
            //tasksGrid.Rows[0].IsCurrent = true; //select the top most row
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (exitForUpgrade)
            {
                if (exitForUpgradeTimestamp.AddMinutes(1) < DateTime.Now)
                {
                    save(false);
                    System.Environment.Exit(0);
                }
                else
                    return;
            }
            
            try
            {               
                bool newVersionExist = checkNewVersion();
                if (newVersionExist)
                {
                    exitForUpgrade = true;
                    exitForUpgradeTimestamp = DateTime.Now;
                    //MessageBox.Show("New version is available. Prioritizer will shutdown in 1 minute", "New Version", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SystemSounds.Beep.Play();
                    ExitForUpgrade upgradeform = new ExitForUpgrade();
                    upgradeform.Show();
                    FlashWindow.Flash(upgradeform);
                }
            }
            catch (Exception ex){}
           
            
        }

        private void NewPrioritizer_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void NewPrioritizer_MouseEnter(object sender, EventArgs e)
        {
            hoveredRow = null;
        }

        private void radGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = e.Location;
            rowToDrag = GetRowAtPoint(this.tasksGrid, e.Location);
        }

        private void radGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left &&
                rowToDrag != null &&
                IsRealDrag(e.Location, downPoint))
            {
                if (rowToDrag.Cells[0].Value != null && rowToDrag.Cells[0].Value.ToString() != string.Empty)
                {
                    int id = (int)rowToDrag.Cells[0].Value;
                    this.tasksGrid.DoDragDrop(id, DragDropEffects.Move);
                }
            }
        }

        private void radGridView1_SortChanged(object sender, GridViewCollectionChangedEventArgs e)
        {

        }

        private void radGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //open selected task only if a row was double clicked (don't open when double clicking other areas of the grid, like scrollers and column sorting headers etc)
            GridRowHeaderCellElement rowSelector = this.tasksGrid.ElementTree.GetElementAtPoint(e.Location) as GridRowHeaderCellElement;
            GridDataCellElement cell = this.tasksGrid.ElementTree.GetElementAtPoint(e.Location) as GridDataCellElement;
            if (cell != null || rowSelector != null)
            {
                openSelectedTask();
            }
        }

        private void openSelectedTask()
        {            
            Tasks taskToUpdate = (Tasks)tasksGrid.CurrentRow.DataBoundItem;
            if (taskToUpdate == null)
                return;
            TaskForm taskForm = new TaskForm(this, taskToUpdate, formMode.update, enTaskType.ActionItem);
            openTaskForm(taskForm);
        }

        private void cboUsers1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeUser();
            tasksGrid.Focus();
        }

        private void radGridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void NewPrioritizer_Resize(object sender, EventArgs e)
        {
            /*if (this.WindowState == FormWindowState.Maximized)
                tasksGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            else if (this.WindowState == FormWindowState.Normal)
            {
                tasksGrid.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
                //SetColumnsWidth();
                //RefreshFormWidth();
            }*/
            SetColumnsWidth();
        }

        private void mnuProjects_Click(object sender, EventArgs e)
        {
            if (authorizeAccess())
            {
                projectsForm projForm = new projectsForm();
                projForm.StartPosition = FormStartPosition.CenterParent;
                projForm.ShowDialog();
            }
        }

        private void mnuResetFilters_Click(object sender, EventArgs e)
        {
            refreshPrioritizer();
        }
        #endregion  

        #region static methods
        public static string DBPath;
        private static readonly string prioritizerRegistryKeyRoot = @"HKEY_CURRENT_USER\Software\prioritizer";
        private static readonly string connStringRegistryKey = "connString";
        public static readonly string mdbPathRegistryKey = "mdbPath";
        private static readonly string attachmentDBPathRegistryKey = "attachmentDBPath";
        private static string DBDirectory = "";
        private static readonly string currentUserRegistryKey = "currentUser";
        /*public static void locateMdbFile(bool showMessage, string registryKey, string DBName)
        {
            if (showMessage)
            {
                DialogResult res = MessageBox.Show("Can't find " + DBName + " file\nPlease point to " + DBName + " file location.", "DB file not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (res == DialogResult.Cancel)
                    System.Environment.Exit(0);
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"c:\";
            openFileDialog1.Filter = "Sql Server CE(*.sdf)|*.sdf";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string newDBLocation = openFileDialog1.FileName;
                Registry.SetValue(prioritizerRegistryKeyRoot, registryKey, newDBLocation);
                //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + newDBLocation;
                DBPath = newDBLocation;
                //Registry.SetValue(prioritizerRegistryKeyRoot, connStringRegistryKey, connString);
            }
        }*/

        private static void loadImages()
        {
            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Prioritizer2._0.Images.IconAttachment.gif");
            _attachmentIcon = new Bitmap(s);
            s.Close();
        }
        public object getMeetingsDatasource()
        {
            return cboMeetings.DataSource;
        }

        
     
        #endregion

        #region drag drop
        private void dragDrop(DragEventArgs e, bool draggedIntoTaskGrid)
        {
            try
            {

                if (DragToChangeRowOrder(e)) //change rows order
                {
                    if (!tasksGrid.Columns["priority"].IsSorted)
                    {
                        DialogResult res = MessageBox.Show("Can't change row order when grid sort order is changed from the default.\n click 'Ok' to change grid sort order to default", "Warning", MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                        if (res == DialogResult.OK)
                        {
                            sortMainGrid(defaultSortField, defaultSortFieldOrder);
                        }
                        return;
                    }

                    Guid sourceRowid;
                    int sourcePriority;
                    int targetPriority;
                    evaluateDragdropAction(e, out sourceRowid, out sourcePriority, out targetPriority);
                    reorderRowsByPriority(sourceRowid, sourcePriority, targetPriority);

                }
                else //drag files/emails from outside into prioritizer
                {


                    string[] filenames = null;
                    MemoryStream[] filestreams = null;
                    if (emailDroppedIntoApplicationArea(e)) //handle email dropped from outlook
                    {
                        //wrap standard IDataObject in OutlookDataObject
                        OutlookDataObject dataObject = new OutlookDataObject(e.Data);
                        //get the names and data streams of the files dropped
                        filenames = (string[])dataObject.GetData("FileGroupDescriptorW");
                        if (!SingleFileDropped(filenames))
                            return;
                        filestreams = (MemoryStream[])dataObject.GetData("FileContents");
                    }
                    else //handle file dropped from windows explorer
                    {
                        filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
                        if (!SingleFileDropped(filenames))
                            return;
                        filestreams = new MemoryStream[1];
                        filestreams[0] = new MemoryStream(File.ReadAllBytes(filenames[0]));
                    }

                    if (hoveredRow == null) //dragged into form 
                    {
                        CreateTaskFromDraggedFile(e, filestreams[0]);
                    }
                    else //dragged onto a row in the main grid
                    {
                        DialogResult result = MessageBox.Show(string.Format("Add dragged file as attachment to task:\n '{0}'?\n Click 'Yes' To continue or 'No' to create new task from this file content", hoveredRow.RowInfo.Cells["taskName"].Value), "Drag Action", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            saveDraggedFileToDisk(filenames, filestreams, Guid.Parse(hoveredRow.RowInfo.Cells["id"].Value.ToString()));
                            refreshTaskGrid(false);
                        }
                        else if (result == DialogResult.No)
                        {
                            CreateTaskFromDraggedFile(e, filestreams[0]);
                        }
                        else
                            return;
                    }
                }
            }
            catch (Exception ex)
            { 
                //suppress exceptions coming from dragging outlook objects into prioritizer. 
                //many problems with this on different machines with different outlook versions.
                //in worst case it won't work o
            }
        }

        private void dragEnter(DragEventArgs e,bool draggedIntoTaskGrid)
        {   
            if (DragToChangeRowOrder(e)) 
            {
                e.Effect = DragDropEffects.Move;
            }  
            else
                e.Effect = DragDropEffects.All;
        }

        private static bool DragToChangeRowOrder(DragEventArgs e)
        {
            return e.Data.GetDataPresent(typeof(int));
        }

        private static bool emailDroppedIntoApplicationArea(DragEventArgs e)
        {
            return e.Data.GetDataPresent("FileGroupDescriptor");
        }

        private static bool fileDroppedIntoGrid(DragEventArgs e, bool draggedIntoTaskGrid)
        {
            return e.Data.GetDataPresent(DataFormats.FileDrop, false) == true &&
                            draggedIntoTaskGrid;
        }


        private void evaluateDragdropAction(DragEventArgs e, out Guid sourceRowid, out int sourcePriority, out int targetPriority)
        {
            sourceRowid = (Guid)e.Data.GetData(typeof(Guid));
            sourcePriority = Convert.ToInt16(tasksGrid.CurrentRow.Cells["priority"].Value);
            int targetRowid = -1;
            targetPriority = -1;
            //Console.WriteLine("source row:" + sourceRowid);
            if (hoveredRow != null && hoveredRow.RowInfo != null)
            {
                targetRowid = Convert.ToInt16(hoveredRow.RowInfo.Cells["id"].Value);
                targetPriority = Convert.ToInt16(hoveredRow.RowInfo.Cells["priority"].Value);
                //Console.WriteLine("target row:" + targetRowid);
            }
        }

        private static bool SingleFileDropped(string[] filenames)
        {
            if (filenames != null)
            {
                if (filenames.Length > 1)
                {
                    MessageBox.Show("Only one file can be dropped each time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            return false;
        }

        private void CreateTaskFromDraggedFile(DragEventArgs e, MemoryStream filestream)
        {
            if (e.Data.GetDataPresent("Object Descriptor"))
            {
                MemoryStream myMem = new MemoryStream();
                Byte[] myByte;
                string strCheck = "";
                myMem = (MemoryStream)e.Data.GetData("Object Descriptor");
                myByte = myMem.ToArray();
                myMem.Close();
                for (int i = 0; i < myByte.Length; i++)
                {
                    if (myByte[i] != 0)
                    {
                        strCheck += Convert.ToChar(myByte[i]);
                    }
                }

                if (strCheck.ToLower().IndexOf("outlook") > -1)
                {
                    Microsoft.Office.Interop.Outlook.MailItem myMailItem = null;
                    Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application(); // get current selected items
                    Microsoft.Office.Interop.Outlook.Selection sel = app.ActiveExplorer().Selection;
                    if (!(sel[1] is Microsoft.Office.Interop.Outlook.MailItem))
                    {
                        MessageBox.Show("Dragged item is not of type ''Mail Item''. \n Please Try dragging a different outlook item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    myMailItem = (Microsoft.Office.Interop.Outlook.MailItem)sel[1];
                    TaskForm newTaskForm = new TaskForm(this, new Tasks() { taskName = myMailItem.Subject.ToString(), remarks = myMailItem.Body.ToString(), updateRequester = false, projectID = Guid.Parse("00000000-0000-0000-0000-000000000000"), requesterID = Guid.Parse("00000000-0000-0000-0000-000000000000") }, formMode.add, enTaskType.ActionItem);
                    openTaskForm(newTaskForm);
                }
            }
            else
            {
                using (StreamReader r = new StreamReader(filestream, Encoding.UTF8))
                {
                    TaskForm newTaskForm = new TaskForm(this, new Tasks() { taskName = "", remarks = r.ReadToEnd(), updateRequester = false, projectID = Guid.Parse("00000000-0000-0000-0000-000000000000"), requesterID = Guid.Parse("00000000-0000-0000-0000-000000000000") }, formMode.add, enTaskType.ActionItem);
                    openTaskForm(newTaskForm);

                    r.Close();
                }
            }
        }

        private void radGridView1_DragOver(object sender, DragEventArgs e)
        {
            //var mousePt = MousePosition;
            //outlineForm.Location = new Point(mousePt.X + 5, mousePt.Y + 5);

            //A nice user friendly feature, that Jack of Telerik helped to achieve, is when hovering over the gridview were the items are getting dropped
            //highlight the rows so you can see better on which row you are dropping the dragged rows.
            var scrpt = new Point(e.X, e.Y);//rgvDrag.PointToScreen(e.Location);
            var pt = PointToClient(scrpt);
            pt = tasksGrid.PointToClient(scrpt);
            var element = tasksGrid.ElementTree.GetElementAtPoint(pt);
            var cell = element as GridDataCellElement;
            if (cell != null)
            {
                var row = cell.RowElement;
                if (row != null && hoveredRow != row)
                {
                    if (hoveredRow != null)
                    {
                        hoveredRow.IsMouseOver = false;
                    }
                    hoveredRow = row;
                    hoveredRow.IsMouseOver = true;
                }
            }
        }

        private void radGridView1_DragLeave(object sender, EventArgs e)
        {
            hoveredRow = null;
        }

        bool IsRealDrag(Point mousePosition, Point initialMousePosition)
        {
            return (Math.Abs(mousePosition.X - initialMousePosition.X) >= SystemInformation.DragSize.Width) ||
                (Math.Abs(mousePosition.Y - initialMousePosition.Y) >= SystemInformation.DragSize.Height);
        }

        //private static bool AttachmentRepositoryExists()
        //{
        //    if (attachmentRepository == null)
        //    {
        //        MessageBox.Show("Can't find attachment DB in: " + DBDirectory + attachmentDBName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    else
        //        return true;
        //}
        private static void saveDraggedFileToDisk(string[] filenames, MemoryStream[] filestreams, Guid taskID)
        {
            //if (AttachmentRepositoryExists())
            //{                
                for (int fileIndex = 0; fileIndex < filenames.Length; fileIndex++)
                {
                    //use the fileindex to get the name and data stream
                    string[] filePathParts = filenames[fileIndex].Split('\\');
                    string filename = filePathParts[filePathParts.Length - 1];
                    MemoryStream filestream = filestreams[fileIndex];
                    StreamReader reader = new StreamReader(filestream);

                    ////save the file stream using its name to the application path
                    //string attachmentFilePath = "";
                    //string taskAttachmentDirectory = taskID.ToString();
                    //string rootPath = ConfigurationSettings.AppSettings["attachmentsPath"];
                    //if (rootPath.Trim().Length > 0)
                    //{
                    //    attachmentFilePath += rootPath;
                    //    taskAttachmentDirectory = attachmentFilePath + "\\" + taskAttachmentDirectory;
                    //}

                    //Directory.CreateDirectory(taskAttachmentDirectory);
                    //attachmentFilePath = taskAttachmentDirectory + "\\" + filename;
                    //FileStream outputStream = File.Create(attachmentFilePath);
                    //filestream.WriteTo(outputStream);
                    //outputStream.Close();

                    //save attachment file into DB
                    attachments attachedFile = new attachments();
                    attachedFile.bin = filestream.ToArray();
                    attachedFile.taskID = taskID;
                    attachedFile.fileName = filename;
                    //attachmentRepository.attachments.AddObject(attachedFile);
                    ProxyClient.addAttachment(attachedFile,loggedInUserID);
                    //attachmentRepository.SaveChanges();

                    //Tasks task = repository.getTaskByID(taskID);
                    //task.hasAttachment = true;
                    //repository.applyChangesTasks(task,loggedInUserID);
                    //repository.SaveChanges();
                }
            //}
        }

        #endregion

        private void tasksGrid_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement dataCell = e.CellElement as GridDataCellElement;

            if (dataCell.ColumnInfo.Name == attachColName)
            {
                GridViewDataRowInfo dataRow = dataCell.RowInfo as GridViewDataRowInfo;
                if (dataRow != null)
                {
                    
                    dataCell.ImageAlignment = ContentAlignment.MiddleLeft;

                    bool hasAttachment = Convert.ToBoolean(dataRow.Cells["hasAttachment"].Value);

                    if(hasAttachment)
                        dataCell.Image = _attachmentIcon;
                   

                    dataCell.TextImageRelation = TextImageRelation.ImageBeforeText;
                }
            }

            //add tooltip for specific fields/columns in the grid
            if ((((GridViewCellEventArgsBase)(e)).Column).FieldName == "MeetingName" ||
                (((GridViewCellEventArgsBase)(e)).Column).FieldName == "taskName" ||
                (((GridViewCellEventArgsBase)(e)).Column).FieldName == "dateEntered")
            {
                e.CellElement.ToolTipText = dataCell.Value != null ? dataCell.Value.ToString() : string.Empty;
            }
            
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tasksGrid_CellClick(object sender, GridViewCellEventArgs e)
        {
           
            if ((((Telerik.WinControls.UI.GridVirtualizedCellElement)(sender)).Data).Name == attachColName)
            {
                //if (AttachmentRepositoryExists())
                //{
                Guid taskID = Guid.Parse(e.Row.Cells["ID"].Value.ToString());
                    List<attachments> attachCollection = ProxyClient.getAttachmentsForTask(taskID).ToList() ;// attachmentRepository.attachments.Where(t => t.taskID == taskID).ToList();
                    if (attachCollection.Count == 1)
                    {
                        try
                        {
                            openAttachment(attachCollection[0].bin, attachCollection[0].fileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Can't open attachment\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (attachCollection.Count > 1)
                    {
                        attachmentsForm attachForm = new attachmentsForm(attachCollection);
                        attachForm.StartPosition = FormStartPosition.CenterParent;
                        attachForm.ShowDialog();
                    }
                //}
            }
            
        }

        public static void openAttachment(byte[] attachStream,string fileName)
        {
            string tempDir =  "c:\\temp\\";
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            string filePath = tempDir  + fileName;
            System.IO.FileStream file = System.IO.File.Create(filePath);

            file.Write(attachStream, 0, attachStream.Length);
            file.Close();
            System.Diagnostics.Process.Start(filePath);
        }

        private void mnuQueryExecutor_Click(object sender, EventArgs e)
        {
            if (authorizeAccess())
            {
                QueryExecutor queryForm = new QueryExecutor();
                queryForm.StartPosition = FormStartPosition.CenterParent;
                queryForm.ShowDialog();
            }
        }

        private void mnuDeleteAttachments_Click(object sender, EventArgs e)
        {
            //if (attachmentRepository == null)
            //{
            //    MessageBox.Show("Can't find attachment DB in: " + DBDirectory + attachmentDBName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            Guid taskID = Guid.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
            Tasks t = ProxyClient.getTaskByID(taskID);
            t.hasAttachment = false;
            ProxyClient.applyChangesTasks(t,loggedInUserID);
            //repository.SaveChanges();


            List<attachments> attachments = ProxyClient.getAttachmentsForTask(taskID).ToList(); // attachmentRepository.attachments.Where(s => s.taskID == taskID).ToList();
            foreach (attachments a in attachments)
            {
                ProxyClient.deleteAttachment(a);
            }

          
            refreshTaskGrid(true);
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refresh();         
        }

        private void refresh()
        {
            tasksGrid.Focus();
            if (isTaskListDirty())
            {
                DialogResult result = MessageBox.Show("Changes have been made, would you like to save them first?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    save(true);
                }
            }
            refreshPrioritizer();
        }

        private void cboMeetings_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            save(false);
            BindAttendeesList();
            refreshTaskGrid(true);
            setMeetingDateLabel();
            setMeetingOwnerLabel();
            meetingSummaryControl.syncActionItems();
        }

        private void setMeetingDateLabel()
        {
            if (cboMeetings.SelectedItem != null && ((Meetings)cboMeetings.SelectedItem.DataBoundItem).MeetingDate.HasValue)
                lblMeetingDate.Text = ((Meetings)cboMeetings.SelectedItem.DataBoundItem).MeetingDate.Value.ToShortDateString();
            else
                lblMeetingDate.Text = "";
        }

        private void setMeetingOwnerLabel()
        {
            if (cboMeetings.SelectedItem != null)
                lblMeetingOwner.Text = usersDict[((Meetings)cboMeetings.SelectedItem.DataBoundItem).MeetingOwner].userName;
            else
                lblMeetingOwner.Text = "N/A";
        }
        
        private void btnAttendeesForm_Click(object sender, EventArgs e)
        {
            MeetingAttendeesForm meetingAttendeesFrm = new MeetingAttendeesForm(Guid.Parse(cboMeetings.SelectedValue.ToString()));
            meetingAttendeesFrm.StartPosition = FormStartPosition.CenterParent;
            meetingAttendeesFrm.ShowDialog();
            BindAttendeesList();
        }

        private void tasksGrid_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radRadioButton1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private static void createDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
        private void radButton1_Click(object sender, EventArgs e)
        {
            exportToHTML();

        }

        private void exportToHTML()
        {
            try
            {
                createDir(TEMP_DIR);
                string filePath = TEMP_DIR + "prioritizerGridExport.html";

                string tableCaption = "";
                string subjectStr = "";
                string distributionList = "";
                string footer = "";
                if (isMeetingTasksMode)
                {
                    string meetingOwner = usersDict[((Meetings)cboMeetings.SelectedItem.DataBoundItem).MeetingOwner].userName;
                    string attendeesList = concatAttendeeList();
                    distributionList = concatDistributionList(distributionList);
                    tableCaption += string.Format("<span style='text-align:left;font-size:10px;font-family:calibri'>(Generated by Prioritizer)<br><b>Action items for meeting:</b> '{0}' <br><b>Meeting Owner:</b> {1} <br><b>Attendees:</b> {2}</span>", cboMeetings.SelectedText, meetingOwner, attendeesList);
                    subjectStr = string.Format("Action items for meeting: '{0}' ", cboMeetings.SelectedText);
                    footer = "<br>" + meetingSummaryControl.getDocumentAsHtml();
                }
                else
                {
                    tableCaption += string.Format("Tasks summary for: '{0}'", cboUsers1.Text);
                    subjectStr = tableCaption;
                }

                ExportToHTML exporter = new ExportToHTML(this.tasksGrid);
                exporter.HTMLTableCaptionFormatting += new Telerik.WinControls.UI.Export.HTML.HTMLTableCaptionFormattingEventHandler(exporter_HTMLTableCaptionFormatting);
                exporter.TableBorderThickness = 1;
                exporter.FileExtension = "html";
                exporter.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                exporter.ExportVisualSettings = true;
                exporter.TableCaption = tableCaption;
                exporter.SummariesExportOption = SummariesOption.DoNotExport;
                exporter.RunExport(filePath);

                System.IO.FileStream fileStream = System.IO.File.Open(filePath, FileMode.Open);
                StreamReader reader = new StreamReader(fileStream);
                string reportText = reader.ReadToEnd();
                fileStream.Close();
                reader.Close();
                Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                oMailItem.Subject = subjectStr;
                oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                oMailItem.HTMLBody = reportText + footer;

                oMailItem.To = distributionList;
                oMailItem.Display(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed exporting report to HTML" + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string concatDistributionList(string distributionList)
        {
            foreach (var attendee in currentMeetingAttendeesList)
            {
                //string email = usersDict.ContainsKey(attendee.AttendeeID) ? usersDict[attendee.AttendeeID].email : "";
                if (attendee.email != null && attendee.email.Length > 0)
                    distributionList += attendee.email + ";";

            }
            if (distributionList.EndsWith(","))
                distributionList = distributionList.Substring(0, distributionList.Length - 1);
            return distributionList;
        }

        private string concatAttendeeList()
        {
            string attendeesList = "";
            foreach (var attendee in listAttendees.Items)
            {
                attendeesList += attendee.DisplayValue + ",";
            }
            if (attendeesList.EndsWith(","))
                attendeesList = attendeesList.Substring(0, attendeesList.Length - 1);
            return attendeesList;
        }

        void exporter_HTMLTableCaptionFormatting(object sender, Telerik.WinControls.UI.Export.HTML.HTMLTableCaptionFormattingEventArgs e)
        {            
            e.TableCaptionElement.Styles.Add("text-align", "left");
            e.TableCaptionElement.Styles.Add("font-size", "10px");
            e.TableCaptionElement.Styles.Add("font-family", "calibri");
        }

        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            save(true);           

            hideChkCancelledInMeetingMode();

            toggleRightLeftSplitterVisibility();
        }

        private void toggleRightLeftSplitterVisibility()
        {
            if (radPageView1.SelectedPage.Text.Equals("user tasks", StringComparison.CurrentCultureIgnoreCase))
            {
                btnUp.Visible = true;
                btnDown.Visible = true;
                rightLeftSplitter.Panel2.Hide();
                rightLeftSplitter.Panel2Collapsed = true;
            }
            else
            {
                btnUp.Visible = false;
                btnDown.Visible = false;

                rightLeftSplitter.Panel2Collapsed = false;
                rightLeftSplitter.Panel2.Show();
                rightLeftSplitter.SplitterDistance = Convert.ToInt16(this.Width / 2);
            }
        }

        private void hideChkCancelledInMeetingMode()
        {
            if (isMeetingTasksMode)
                chkCancelled.Visible = false;
            else
                chkCancelled.Visible = true;
        }

        private void mnuNewMeeting_Click(object sender, EventArgs e)
        {
            addNewMeeting();
        }
        private void addNewMeeting()
        {
            MeetingForm newMeetingForm = new MeetingForm(this);
            newMeetingForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult res = newMeetingForm.ShowDialog();
            bindMeetingsCombo(null,cboMeetings,false);
        }

        private void mnuEditMeetings_Click(object sender, EventArgs e)
        {
            MeetingListForm mlf = new MeetingListForm();
            mlf.StartPosition = FormStartPosition.CenterParent;
            mlf.ShowDialog();
        }

        public static List<MeetingCategory> getMeetingCategoryList(bool addCategoryOnTop,bool filterCategoriesOnlyForLoggedInUser)
        {
            List<MeetingCategory> categoriesCopy = null;

            categoriesCopy = ProxyClient.getMeetingCategoryList(filterCategoriesOnlyForLoggedInUser, NewPrioritizer.loggedInUserID).ToList();
            
            if (addCategoryOnTop)
                categoriesCopy.Insert(0, new MeetingCategory() { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), CategoryName = "(No Category)" });
            return categoriesCopy;
        }

        private void btnRefreshMeetings_Click(object sender, EventArgs e)
        {
            refreshMeetings();
        }

        private void refreshMeetings()
        {
            bindMeetingsCombo(Guid.Parse("00000000-0000-0000-0000-000000000001"), cboMeetings, true);
            bindMeetingCategoryCombo(true);
        }

        private void btnAddNewMeeting_Click(object sender, EventArgs e)
        {
            addNewMeeting();
        }

        private void btnEditMeetingCategories_Click(object sender, EventArgs e)
        {
            openMeetingCategoriesForm();
        }

        private void openMeetingCategoriesForm()
        {
            meetingCategoryForm mcf = new meetingCategoryForm();
            mcf.StartPosition = FormStartPosition.CenterParent;
            mcf.ShowDialog();
            bindMeetingCategoryCombo(false);
        }

        private void btnRefreshMeetingCategory_Click(object sender, EventArgs e)
        {
            
        }

        private void bindMeetingCategoryCombo(bool reloadFromDB)
        {
            if (reloadFromDB || cboMeetingCategory.DataSource == null)
            {
                cboMeetingCategory.ValueMember = "ID";
                cboMeetingCategory.DisplayMember = "CategoryName";
                List<MeetingCategory> mcl = getMeetingCategoryList(false, false);// repository.MeetingCategory.Where(m => m.CategoryOwner == loggedInUserID).OrderBy(p => p.CategoryName).ToList();
                mcl.Insert(0, new MeetingCategory() { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), CategoryName = "(All Meetings)" });
                mcl.Insert(1, new MeetingCategory() { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), CategoryName = "(Meetings With No Category)" });
                cboMeetingCategory.DataSource = mcl;
            }            
        }

        private void cboMeetingCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            bindMeetingsCombo(null, cboMeetings,true);
        }

        private void mnuMeetingCategories_Click(object sender, EventArgs e)
        {
            openMeetingCategoriesForm();
        }

        private void tasksGrid_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            //if (isMeetingTasksMode)
            //{
                RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
                e.ContextMenu.Items.Add(separator);
                
                RadMenuItem moveToAnotherMeeting = new RadMenuItem();
                moveToAnotherMeeting.Text = "Assign task to meeting";
                moveToAnotherMeeting.Click += new EventHandler(moveToAnotherMeeting_Click);
                e.ContextMenu.Items.Add(moveToAnotherMeeting);
                
                
                /*RadMenuItem assignToMultipleUsers = new RadMenuItem();
                assignToMultipleUsers.Text = "Assign Task to multiple users";
                assignToMultipleUsers.Click += new EventHandler(assignToMultipleUsers_Click);
                e.ContextMenu.Items.Add(assignToMultipleUsers);
                */

               
                
                
            //}
        }

        void assignToMultipleUsers_Click(object sender, EventArgs e)
        {

            ChooseUsersForm chooseUsers = new ChooseUsersForm();
            chooseUsers.StartPosition = FormStartPosition.CenterParent;
            chooseUsers.ShowDialog();

            List<int> selectedUsers = chooseUsers.selectedUsers;

            if (selectedUsers.Count>0)
            {
                Guid selectedTaskID = Guid.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
                Tasks task = NewPrioritizer.ProxyClient.getTaskByID(selectedTaskID);
                tasksGrid.CurrentRow.Cells["userID"].Value = selectedUsers[0];

                for (int c = 1; c < selectedUsers.Count; c++)
                {

                }
                
                //MeetingTasks meetingTask = repository.MeetingTasks.Where(a => a.TaskID == selectedTaskID).FirstOrDefault();
                //meetingTask.StartTracking();
                //meetingTask.MeetingID = selectedMeeting.Value;
                //repository.MeetingTasks.ApplyChanges(meetingTask);
                //repository.SaveChanges();
                //refreshTaskGrid(true);
            }
        }

        void moveToAnotherMeeting_Click(object sender, EventArgs e)
        {
            try
            {
                Guid? selectedMeeting = SelectMeeting();

                if (selectedMeeting.HasValue)
                {
                    Guid selectedTaskID = Guid.Parse(tasksGrid.CurrentRow.Cells["ID"].Value.ToString());
                    MeetingTasks meetingTask = null;
                    /*if (isMeetingTasksMode)
                    {

                        meetingTask = ProxyClient.getMeetingTaskByID(selectedTaskID);
                        meetingTask.StartTracking();
                        meetingTask.MeetingID = selectedMeeting.Value;
                    }
                    else
                    {*/
                        Tasks t = ProxyClient.getTaskByID(selectedTaskID);
                        meetingTask = assignTaskToMeeting(t, selectedMeeting);
                    //}


                    ProxyClient.applyChangesMeetingTasks(meetingTask);
                    //repository.SaveChanges();
                    refreshTaskGrid(true);
                }
            }
            catch (UpdateException ex)
            {
                if (ex.Message.ToLower().Contains("duplicate") || ex.InnerException.Message.ToLower().Contains("duplicate"))
                {
                    MessageBox.Show("This task is already assigned to target meeting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private Guid? SelectMeeting()
        {
            ChooseMeetingForm chooseMeeting = new ChooseMeetingForm(this);
            chooseMeeting.StartPosition = FormStartPosition.CenterParent;
            chooseMeeting.ShowDialog();

            Guid? selectedMeeting = chooseMeeting.selectedMeetingID;
            return selectedMeeting;
        }

        private void tasksGrid_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "taskName")
            {
                if (e.Column is GridViewTextBoxColumn)
                {
                    ((RadTextBoxEditorElement)((RadTextBoxEditor)this.tasksGrid.ActiveEditor).EditorElement).MaxLength = 254;
                }
            }  
        }

        private void btnMoveToNewMeeting_Click(object sender, EventArgs e)
        {
            Guid? selectedMeetingID = null;

            List<Tasks> originalMeetingtaskList = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource);
            List<Users> originalMeetingAttendeesList = new List<Users>(currentMeetingAttendeesList);
            DialogResult res =  MessageBox.Show("Move to existing meeting?", "Target Meeting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                //select from existing meetings
                selectedMeetingID = SelectMeeting();
                if (selectedMeetingID == null)
                    return;
            }
            else
            {
                //create new meeting similar to current displayed meeting. then choose it as selected.
                string currentMeetingName = cboMeetings.SelectedItem.ToString();
                String NewMeetingName = Interaction.InputBox(
                    "Enter new meeting name", "Move To New Meeting",currentMeetingName + "_copy" , -1, -1);

                if (NewMeetingName.Length == 0)
                    return;

                MeetingCategoryMap mcm = ProxyClient.getMeetingCategoryMapByID(((Meetings)cboMeetings.SelectedItem.DataBoundItem).ID);
                Guid? meetingCategory = null;
                if (mcm != null)
                    meetingCategory = mcm.MeetingCategoryID;

                //create new meeting and copy summary document from current
                Guid currentMeetingID = Guid.Parse(cboMeetings.SelectedValue.ToString());
                Meetings currentMeeting = ProxyClient.getMeetingByID(currentMeetingID,false);
                MeetingForm.CreateMeeting(NewMeetingName, DateTime.Now, meetingCategory, currentMeeting.MeetingSummaryRTF, originalMeetingAttendeesList);

                
                refreshMeetings();
                selectedMeetingID = ProxyClient.getMeetingByName(NewMeetingName).ID;
                
            }

            moveAllActiveTasksToSelectedMeeting(selectedMeetingID.Value, originalMeetingtaskList);            
            //repository.SaveChanges();
            refreshTaskGrid(true);
        }

        private void moveAllActiveTasksToSelectedMeeting(Guid selectedMeetingID, List<Tasks> taskList)
        {
            //List<Tasks> taskList = (List<Tasks>)tasksGrid.DataSource;
            List<MeetingTasks> changedMeetingTasks = new List<MeetingTasks>();
            foreach (var row in taskList)
            {
                MeetingTasks meetingTask = null;
                if (row.taskStatusID != 4 && row.taskStatusID != 5)
                {
                    meetingTask = ProxyClient.getMeetingTaskByID(row.ID);
                    meetingTask.StartTracking();
                    meetingTask.MeetingID = selectedMeetingID;
                    changedMeetingTasks.Add(meetingTask);
                }
            }
            ProxyClient.applyChangesMeetingTasksList(changedMeetingTasks.ToArray<MeetingTasks>());
        }

      

        private void radSplitContainer1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer3_Panel2_Resize(object sender, EventArgs e)
        {
            
        }

        private void tasksGrid_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo.IsCurrent)
            {               
                e.RowElement.ForeColor = Color.Blue;
                e.RowElement.Font = new Font(e.RowElement.Font, FontStyle.Bold); 
            }
            else
            {
                e.RowElement.ForeColor = Color.Black;
                e.RowElement.Font = new Font(e.RowElement.Font, FontStyle.Regular); 
                
            }
        }

        private void tasksGrid_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            //if (tasksGridRowClicked())
                
        }

       
        //public bool tasksGridRowClicked()
        //{
        //    //// make sure we do have a valid current row
        //    //if(tasksGrid.CurrentRow==null)
        //    //{
        //    //    return false;
        //    //}

        //    //// make sure we have a data row
        //    //if (tasksGrid.CurrentRow.RowElementType != typeof(GridDataRowElement))
        //    //{
        //    //    return false;
        //    //}
        //    //return true;
            
 
        //   if (tasksGrid.CurrentRow != null)
        //      if (tasksGrid.CurrentRow is GridViewDataRowInfo)
        //        return true;
        //   return false;
        //}
      
    }

    public static class ExtensionMethods
    {
        public static string getUserName(this Tasks task)
        {
            if (task.userID.HasValue && task.userID.Value != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                return NewPrioritizer.usersDict[task.userID.Value].userName;
            else
                return "Decision";
        }

        public static string getTaskSummary(this Tasks task)
        {
            //return "";
            string status = "";
            if (task.taskStatusID.HasValue)
                status = NewPrioritizer.getStatusName(task.taskStatusID.Value);

            string AssignedTo = task.getUserName();
            string taskName = task.taskName;
            string remarks = task.remarks;
            return string.Format("Status: {0}{3}Assigned To: {1}{3}Task: {2}{3}", status, AssignedTo, taskName, " | ");
        }

        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}


