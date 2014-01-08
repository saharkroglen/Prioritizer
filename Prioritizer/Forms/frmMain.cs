using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using PrioritizerService.Model;
using System.ServiceModel;
using System.Linq;
using Microsoft.VisualBasic;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraBars;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Reflection;
using DevExpress.Data;
using Prioritizer.Forms;
using System.Configuration;
using DevExpress.Utils;
using DevExpress.Data.Filtering;
using System.Media;
using Prioritizer.Utils;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using PrioritizerService;
using System.Diagnostics;
using Shared;
using Prioritizer.Shared.Model;
using System.ServiceModel.Configuration;
using Prioritizer.Shared;
using Prioritizer.Proxy;
using Prioritizer.Class;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands;
using Prioritizer.Properties;



namespace Prioritizer.Forms
{
    public partial class frmMain : XtraForm
    {
        private List<Alerts> _userAlertList = null;
        public Dictionary<Guid, Alerts> UserAlerts = new Dictionary<Guid,Alerts>();
        public Dictionary<DateTime, List<Alerts>> UserAlertsByTime = new Dictionary<DateTime, List<Alerts>>();
        private ClientMessage _clientMessage;
        public static Guid _tenantID ;
        private string PRIORITIZER_INSTALL_DIR = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86) + @"\Prioritizer";
        private string PACKAGE_FOLDER_NAME = @"\prioritizerPackage";
        private string PRIORITIZER_APPLICATION_EXECUTABLE_NAME = @"\prioritizer.exe";
        public bool ConnectionStatus = false;
        private readonly string attachColName = "attachmentColumn";
        private readonly string alertColName = "alertColumn";
        private static Image _attachmentIcon = null;
        private static Image _alertIcon = null;
        private static Image _alertRedIcon = null;
        private int _lastPanelTasksWidth;
        public static string ShowTaskURL = string.Empty;
        private BindingList<Person> gridDataList = new BindingList<Person>();
        private IList<Tasks> allTaskList = null;
        private List<Users> myAllowedUsers = null;
        private string UserDomainName = "";
        private BindingList<Users> users = null;
        private Guid lastSelectedRowIndex = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public static string ClientSetupDownloadLocation = string.Empty;
        public static string APP_VERSION = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static readonly string USER_INFO_DIRECTORY = string.Format(@"{0}\Prioritizer", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        public static readonly string USER_INFO_FILE_NAME = "user.info";
        public static readonly string USER_LAYOUT_FILE_NAME = "user.layout";
        public static readonly string USER_INFO_FILE_PATH = string.Format(@"{0}\{1}", USER_INFO_DIRECTORY, USER_INFO_FILE_NAME);
        public static List<projects> ProjectList = null;
        public static BindingList<Users> usersList = null;
        public BindingList<Shared.Model.TaskStatus> taskStatusList = null;
        public static BindingList<ImportanceItem> importanceList = new BindingList<ImportanceItem>(Shared.Utils.GetImportanceList());
        public static Dictionary<Guid, string> projectsDict;
        public static Dictionary<Guid, Users> usersDict;
        public static Dictionary<string, Guid> usersDomainDict;
        public static UserInfo UserInfo { get; set; }
        static bool isUpgradeRequired = false;
        public static Guid loggedInUserID = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public static Users loggedInUser = null;
        string friendlyUserName = "";
        public static Dictionary<int, string> statusDict;
        public bool isMeetingTasksMode { get { return ribbonControl.SelectedPage.Text.ToLower() == "meeting tasks"; } }
        public bool isUserTasksMode { get { return ribbonControl.SelectedPage.Text.ToLower() == "user tasks"; } }
        //public object SelectedMeeting { get { return cboMeetings.EditValue; } }
        public Meetings selectedMeeting;
        private MeetingSummaryControl meetingSummaryControl;
        readonly string LINE_DELIMITER = "------------------";
        public static List<Users> currentMeetingAttendeesList = null;
        Tasks hoveredTask = null;

        public frmMain()
        {
            try
            {
                InitializeComponent();

                try
                {
                    //initProxyClient();
                    ConnectionManager.init();
                    ConnectionManager.OnConnectionStateChange += new ConnectionManager.ConnectionStateChange(ConnectionManager_OnConnectionStateChange);
                    ConnectionManager.OnServerMessage += new ConnectionManager.ServerMessageArrival(ConnectionManager_OnServerMessage);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Can't establish connection to server\nPlease try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new PrioritizerDisconnectException(null);
                }

                if (!authenticate())
                {
                    Environment.Exit(0);
                    return;
                }

                

                InitSkinGallery();
                //InitGrid();

                InitControls();

                initializePrioritizer();
                //refreshTaskGrid(true);

            }
            catch (Exception ex)
            {
                Program.handleDisconnectionExceptionOnStartup(ex);
            }

        }

        void ConnectionManager_OnServerMessage(ClientMessage message)
        {
            _clientMessage = message;
           
        }

        void ConnectionManager_OnConnectionStateChange(bool Connected)
        {
            setConnectionStatus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            BindControls();
            toggleMeetingSummaryControl();
            
            
            //setGridSort();
            gridView1.MoveFirst();

            this.ShowPopupMenu += new EventHandler(Pad_ShowPopupMenu);
            this.ShowMiniToolbar += new EventHandler(pad_ShowMiniToolbar);

            alertPoke.FormClosing += new DevExpress.XtraBars.Alerter.AlertFormClosingEventHandler(alertPoke_FormClosing);
            alertPoke.ButtonClick += new DevExpress.XtraBars.Alerter.AlertButtonClickEventHandler(alertPoke_ButtonClick);


            AdminPage.Visible = loggedInUser.IsAdmin;
            //CheckUpgradeOnStartup();
            checkExitForUpgrade();
        }

        void alertPoke_ButtonClick(object sender, DevExpress.XtraBars.Alerter.AlertButtonClickEventArgs e)
        {
            Poke selectedPoke = e.AlertForm.AlertInfo.Tag as Poke;
            if (e.ButtonName == "btnDismiss")
            {
                dismissPokeAlert(e, selectedPoke);
            }
            if (e.ButtonName == "btnReply")
            {
                dismissPokeAlert(e, selectedPoke);
                PokeReplyForm replyForm = new PokeReplyForm(this, selectedPoke);
                replyForm.StartPosition = FormStartPosition.CenterParent;
                replyForm.ShowDialog();
            }
        }

        private void dismissPokeAlert(DevExpress.XtraBars.Alerter.AlertButtonClickEventArgs e, Poke selectedPoke)
        {
            for (int i = 0; i < _clientMessage.PokeList.Count(); i++)
            {
                Poke p = _clientMessage.PokeList[i];
                if (p.Comment == selectedPoke.Comment)
                {
                    _clientMessage.PokeList.RemoveAt(i);
                }
            }
            e.AlertForm.Close();

        }

        //private void CheckUpgradeOnStartup()
        //{
        //    bool newVersionExist = checkNewVersion();
        //    if (newVersionExist)
        //    {
        //        timerConnectionStatus.Enabled = false;
        //        this.Enabled = false;
        //        isUpgradeRequired = true;
        //        SystemSounds.Beep.Play();
        //        //downloadLatestSetupFile();
        //        save(false);
        //        ExitForUpgrade upgradeform = new ExitForUpgrade(60, false);
        //        upgradeform.StartPosition = FormStartPosition.CenterParent;
        //        upgradeform.Show(this);
        //    }
        //}
        void alertPoke_FormClosing(object sender, DevExpress.XtraBars.Alerter.AlertFormClosingEventArgs e)
        {
            //prevent closing alert forms due to timeup (work around since there is no way to stay pinned in other way)
            if (e.CloseReason == DevExpress.XtraBars.Alerter.AlertFormCloseReason.TimeUp) e.Cancel = true;
        }

        private bool _domainControllerAuthenticate;
        private bool authenticate()
        {
            bool authentic = false;

            _domainControllerAuthenticate = ConnectionManager.Proxy.DomainControllerAuthenticate();
                if (_domainControllerAuthenticate)
                {
                    _tenantID = Guid.Parse(ConfigurationManager.AppSettings["defaultTenantID"]); //load default tenant GUID
                    loadLookups();
                    loadDictionaries();
                    UserDomainName = System.Environment.UserName.ToLower();
                    if (usersDomainDict.Count > 0 && usersDomainDict.ContainsKey(UserDomainName))
                    {
                        loggedInUserID = usersDomainDict[UserDomainName];
                        loggedInUser = ConnectionManager.Proxy.getUserByID(loggedInUserID);
                        ConnectionManager.SetConnectionIdentity(loggedInUserID);
                    }
                    if (!usersDomainDict.ContainsKey(UserDomainName))
                    {
                        friendlyUserName = UserDomainName;
                    }
                    else
                        friendlyUserName = usersDict[usersDomainDict[UserDomainName]].userName;

                    authentic = true;
                }
                else
                {
                    LoginForm login = new LoginForm(this);
                    login.StartPosition = FormStartPosition.CenterParent;
                    login.ShowDialog();

                    if (login.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        loggedInUser = login.AuthenticatedUser;
                        friendlyUserName = login.AuthenticatedUser.userName;
                        loggedInUserID = loggedInUser.ID;
                        ConnectionManager.SetConnectionIdentity(loggedInUserID);
                        _tenantID = login.AuthenticatedUser.TenantID;
                        authentic = true;

                        if (loggedInUser.TemporaryPassword)
                        {
                            if (!SetPassword())
                            {
                                MessageBox.Show(string.Format("Must change temporary password, Priori will shutdown. {0}Please try again later",Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Environment.Exit(0);
                            }
                        }


                        loadLookups();
                        loadDictionaries();
                    }
                    else
                    {
                        authentic = false;
                    }
                }
            
            return authentic;
        }

        private void InitControls()
        {
            chkFinished.Checked = false;
            chkCancelled.Checked = false;
            cboUsersSelector.Width = 200;
            ribbonControl.SelectedPage = ribbonUserTasks;
        }


        public Guid GetSelectedUserID()
        {
            return Guid.Parse(cboUsersSelector.EditValue.ToString());
        }

        public static void openAttachment(byte[] attachStream, string fileName)
        {
            string tempDir = "c:\\temp\\";
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            string filePath = tempDir + fileName;
            System.IO.FileStream file = System.IO.File.Create(filePath);

            file.Write(attachStream, 0, attachStream.Length);
            file.Close();
            System.Diagnostics.Process.Start(filePath);
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
                    return Color.LightGray;
                    break;
                default:
                    return Color.Transparent;
            }
        }


        private bool gridInitialized = false;
        private void initGrid()
        {
            if (!gridInitialized)
            {
                addAlertColumn();
                addAttachmentColumn();                
                string gridLayoutFilePath = string.Format(@"{0}\{1}", USER_INFO_DIRECTORY, USER_LAYOUT_FILE_NAME);
                if (File.Exists(gridLayoutFilePath))
                {
                    gridView1.RestoreLayoutFromXml(gridLayoutFilePath);
                }
                else
                {
                    setColumnsWidthAndHeader();
                    gridView1.BestFitColumns();

                    reorderColumns();
                }
                
                removeAutoGeneratedColumns();
                addBoundColumnControls();
                
                setGridProperties();
                
                gridInitialized = true;
            }
            reorderEssentialColumns();
            hideUnnecessaryColumns();
            setGridSort();
        }

        private void setGridProperties()
        {
            gridView1.OptionsDetail.EnableMasterViewMode = false;            
            gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
            gridView1.FocusedRowChanged += new FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsPrint.AutoWidth = false;
            gridView1.InitNewRow += new InitNewRowEventHandler(gridView1_InitNewRow);
            gridView1.CustomUnboundColumnData += new CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);

        }

        void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            bindToCurrentRow();
        }

        
        private void setGridSort()
        {
            if (chkViewAllMembers.Checked)
            {
                gridView1.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { new GridColumnSortInfo(gridView1.Columns["userID"], ColumnSortOrder.Ascending) });
            }
            else
            {
                 gridView1.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { new GridColumnSortInfo(gridView1.Columns["priority"], ColumnSortOrder.Ascending) });
            }
        }

        void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //throw new NotImplementedException();
        }

        

        void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            Tasks currentTask = null;
            if (e.Column.FieldName == attachColName && e.IsGetData)
            {
                currentTask = ((Tasks)(e.Row));
                if (currentTask.hasAttachment != null && currentTask.hasAttachment.Value)
                    e.Value = _attachmentIcon;

            }
            if (e.Column.FieldName == alertColName && e.IsGetData)
            {
                currentTask = ((Shared.Model.Tasks)(e.Row));
                if (UserAlerts.ContainsKey(currentTask.ID) || (currentTask.hasAlert != null && currentTask.hasAlert.Value) || currentTask.dueDate != null)
                {
                    DateTime alertDateTime;

                    bool alertIsActive = false;
                    bool hasAlert = false;
                    if (UserAlerts.ContainsKey(currentTask.ID) && UserAlerts[currentTask.ID].nextAlert.HasValue)
                    {
                        hasAlert = true;
                        if (UserAlerts[currentTask.ID].active.HasValue)
                            alertIsActive = UserAlerts[currentTask.ID].active.Value;
                    }
                                       

                    if (hasAlert && alertIsActive)
                    {
                        Alerts alert = UserAlerts[currentTask.ID];// ProxyClient.getAlertForTask(currentTask.ID).First();
                        alertDateTime = alert.nextAlert.Value;
                    }
                    else if (currentTask.dueDate.HasValue && 
                        (currentTask.hasAlert == null || (currentTask.hasAlert.Value && !alertIsActive)) && 
                        !(currentTask.taskStatusID == 4 || currentTask.taskStatusID == 5))
                    {
                        alertDateTime = currentTask.dueDate.Value;
                    }
                    else 
                    {
                        return;
                    }

                   
                    if (alertDateTime <= DateTime.UtcNow)
                        e.Value = _alertRedIcon;
                    else
                        e.Value = _alertIcon;
                }
            }
        }

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {            
            if (e.Column.FieldName.Equals("taskStatusID") && e.CellValue != null)
                e.Appearance.BackColor = getColorForStatus(e.CellValue.ToString());
        }

        private void loadImages()
        {
            _attachmentIcon = this.imageList2.Images[4];//Assembly.GetExecutingAssembly().GetManifestResourceStream("Prioritizer.Resources.Images.IconAttachment.gif");
            _alertIcon =  this.imageList2.Images[8];
            _alertRedIcon = this.imageList2.Images[10];
        }

        RepositoryItemLookUpEdit _cboGridProject;
        private void addBoundColumnControls()
        {
            

            RepositoryItemLookUpEdit cboGridUsers = new RepositoryItemLookUpEdit();
            gridView1.Columns["userID"].ColumnEdit = cboGridUsers;
            cboGridUsers.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cboGridUsers.Name = "userID";
            cboGridUsers.ValueMember = "ID";
            cboGridUsers.DisplayMember = "userName";
            cboGridUsers.Columns.Add(new LookUpColumnInfo("userName", 80));
            tasksGrid.RepositoryItems.Add(cboGridUsers);
            cboGridUsers.DataSource = usersList;
            cboGridUsers.NullText = "Decision";
            cboGridUsers.ForceInitialize();
            gridView1.Columns["userID"].Caption = "Assigned To";


            ////attachment column
            //GridViewImageColumn attachmentColumn = new GridViewImageColumn();
            //attachmentColumn.Name = attachColName;
            //attachmentColumn.HeaderImage = _attachmentIcon;
            //attachmentColumn.ImageLayout = ImageLayout.None;
            //attachmentColumn.Width = 20;
            //tasksGrid.MasterTemplate.Columns.Insert(0, attachmentColumn);

            ////add new lookup column instead of projectID column which was removed            
            _cboGridProject = new RepositoryItemLookUpEdit();
            gridView1.Columns["projectID"].ColumnEdit = _cboGridProject;
            _cboGridProject.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            _cboGridProject.Name = "ProjectID";
            _cboGridProject.ValueMember = "ID";
            _cboGridProject.DisplayMember = "projectName";
            _cboGridProject.Columns.Add(new LookUpColumnInfo("projectName", 80));
            _cboGridProject.NullText = "";
            tasksGrid.RepositoryItems.Add(_cboGridProject);
            _cboGridProject.DataSource = ProjectList;
            _cboGridProject.ForceInitialize();
            gridView1.Columns["projectID"].Caption = "Project";


            RepositoryItemDateEdit dueDate = new RepositoryItemDateEdit();
            dueDate.NullText = "";
            tasksGrid.RepositoryItems.Add(dueDate);
            gridView1.Columns["dueDate"].ColumnEdit = dueDate;
            gridView1.Columns["dueDate"].Caption = "Due Date";
          

            RepositoryItemDateEdit dateEntered = new RepositoryItemDateEdit();
            dateEntered.NullText = "";
            tasksGrid.RepositoryItems.Add(dateEntered);
            gridView1.Columns["dateEntered"].ColumnEdit = dateEntered;
            gridView1.Columns["dateEntered"].Caption = "Date Created";

            RepositoryItemDateEdit dateClosed = new RepositoryItemDateEdit();
            dateClosed.NullText = "";
            tasksGrid.RepositoryItems.Add(dateClosed);
            gridView1.Columns["dateClosed"].ColumnEdit = dateEntered;
            gridView1.Columns["dateClosed"].Caption = "Date Closed";
            

            ////add new lookup column instead of RequesterID column which was removed
            RepositoryItemLookUpEdit cboGridRequester = new RepositoryItemLookUpEdit();
            gridView1.Columns["requesterID"].ColumnEdit = cboGridRequester;
            cboGridRequester.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cboGridRequester.Name = "requesterID";
            cboGridRequester.ValueMember = "ID";
            cboGridRequester.DisplayMember = "userName";
            cboGridRequester.Columns.Add(new LookUpColumnInfo("userName", 80));
            tasksGrid.RepositoryItems.Add(cboGridRequester);
            cboGridRequester.ShowHeader = false;
            cboGridRequester.DataSource = usersList;
            cboGridRequester.NullText = "";
            cboGridRequester.ForceInitialize();
            gridView1.Columns["requesterID"].Caption = "Requested By";


            ////add new lookup column instead of taskStatusID column which was removed
            RepositoryItemLookUpEdit cboGridStatus = new RepositoryItemLookUpEdit();
            gridView1.Columns["taskStatusID"].ColumnEdit = cboGridStatus;
            cboGridStatus.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cboGridStatus.Name = "taskStatusID";
            cboGridStatus.ValueMember = "ID";
            cboGridStatus.DisplayMember = "StatusName";
            cboGridStatus.NullText = "";
            cboGridStatus.Columns.Add(new LookUpColumnInfo("StatusName", 80));
            cboGridStatus.ShowHeader = false;
            tasksGrid.RepositoryItems.Add(cboGridStatus);
            cboGridStatus.DataSource = taskStatusList;
            cboGridStatus.ForceInitialize();
            gridView1.Columns["taskStatusID"].Caption = "Status";



            ////add new lookup column instead of importance column which was removed
            RepositoryItemLookUpEdit cboImportance = new RepositoryItemLookUpEdit();
            cboImportance.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cboImportance.Name = "Importance";
            cboImportance.ValueMember = "ID";
            cboImportance.DisplayMember = "Name";
            cboImportance.NullText = "";
            cboImportance.Columns.Add(new LookUpColumnInfo("Name", 80));
            tasksGrid.RepositoryItems.Add(cboImportance);
            cboImportance.DataSource = importanceList;
            cboImportance.ForceInitialize();
            cboImportance.ShowHeader = false;
            gridView1.Columns["Importance"].Caption = "Importance";
            gridView1.Columns["Importance"].ColumnEdit = cboImportance;


        }

        private void setColumnsWidthAndHeader()
        {
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.Columns["priority"].Caption = "Priority";
            gridView1.Columns["priority"].Width = gridView1.Columns["priority"].GetBestWidth();

            gridView1.Columns["defectNumber"].Caption = "Defect #";
            gridView1.Columns["priority"].Width = gridView1.Columns["priority"].GetBestWidth();

            gridView1.Columns["Importance"].Width = gridView1.Columns["Importance"].GetBestWidth();
            

            gridView1.Columns["taskName"].Caption = "Name";
            gridView1.Columns["taskName"].Width = 200;

            gridView1.Columns["estimatedWorkHours"].Caption= "Estimated Hours";
            gridView1.Columns["estimatedWorkHours"].Width = gridView1.Columns["estimatedWorkHours"].GetBestWidth();

            gridView1.Columns["completionPercentage"].Caption = "% complete";
            gridView1.Columns["completionPercentage"].Width = gridView1.Columns["completionPercentage"].GetBestWidth();

            //gridView1.Columns["updateRequester"].Caption = "Update Requester";
            //gridView1.Columns["updateRequester"].Width = gridView1.Columns["updateRequester"].GetBestWidth();

            gridView1.Columns["requesterID"].Caption = "Requested By";
            gridView1.Columns["requesterID"].Width = gridView1.Columns["requesterID"].GetBestWidth();

            gridView1.Columns["actualWorkHours"].Caption = "Actual Work (hours)";
            gridView1.Columns["actualWorkHours"].Width = gridView1.Columns["actualWorkHours"].GetBestWidth();

            gridView1.Columns["MeetingName"].Caption = "Meeting Name";
            gridView1.Columns["MeetingName"].Width = gridView1.Columns["MeetingName"].GetBestWidth();

            gridView1.Columns["privateTask"].Caption = "Private Task";
            gridView1.Columns["privateTask"].Width = gridView1.Columns["privateTask"].GetBestWidth();

        }

        private void removeAutoGeneratedColumns()
        {
            gridView1.Columns.Remove(gridView1.Columns["dirty"]);
            gridView1.Columns.Remove(gridView1.Columns["Requester"]);
            gridView1.Columns.Remove(gridView1.Columns["Project"]);
            gridView1.Columns.Remove(gridView1.Columns["Change Tracker"]);
            gridView1.Columns.Remove(gridView1.Columns["ChangeTracker"]);
            gridView1.Columns.Remove(gridView1.Columns["dateUpdated"]);
            gridView1.Columns.Remove(gridView1.Columns["updateRequester"]);
            gridView1.Columns.Remove(gridView1.Columns["MeetingTasks"]);

        }

        private void hideUnnecessaryColumns()
        {
            gridView1.Columns["ID"].Visible = false;

            if (isUserTasksMode)
            {
                hidePrivateTaskColumnWhenManagerWatchEmployeeTray();

                if (chkViewAllMembers.Checked)
                {
                    gridView1.Columns["userID"].Visible = true;
                    gridView1.Columns["priority"].Visible = false;
                }
                else
                {
                    gridView1.Columns["userID"].Visible = false;
                    gridView1.Columns["priority"].Visible = true;
                }

            }
            else //meeting mode
            {
                gridView1.Columns["priority"].Visible = false;
                gridView1.Columns["userID"].Visible = true;
                gridView1.Columns["privateTask"].Visible = false;
            }

            


            gridView1.Columns["remarks"].Visible = false;
            gridView1.Columns["UpdatesLog"].Visible = false;
            gridView1.Columns["hasAttachment"].Visible = false;
            gridView1.Columns["hasAlert"].Visible = false;
            gridView1.Columns["taskType"].Visible = false;

            gridView1.Columns["TenantID"].Visible = false;
            gridView1.Columns["CopiedFromTaskID"].Visible = false;
            gridView1.Columns["Tenant"].Visible = false;
            gridView1.Columns["TaskIWasCopiedFrom"].Visible = false;
            gridView1.Columns["Users"].Visible = false;

        }

        private void hidePrivateTaskColumnWhenManagerWatchEmployeeTray()
        {
            if (GetSelectedUserID() != loggedInUserID || chkViewAllMembers.Checked)
            {
                gridView1.Columns["privateTask"].Visible = false;
            }
            else
            {
                gridView1.Columns["privateTask"].Visible = true;
            }
        }


        private void reorderColumns()
        {

            reorderEssentialColumns();

            gridView1.Columns["Importance"].VisibleIndex = 4;            
            gridView1.Columns["taskName"].VisibleIndex = 5;
            gridView1.Columns["taskStatusID"].VisibleIndex =6;
            gridView1.Columns["projectID"].VisibleIndex = 7;
            gridView1.Columns["requesterID"].VisibleIndex = 8;
            gridView1.Columns["MeetingName"].VisibleIndex = 9;
            gridView1.Columns["estimatedWorkHours"].VisibleIndex = 10;
            gridView1.Columns["completionPercentage"].VisibleIndex = 11;
            gridView1.Columns["defectNumber"].VisibleIndex = 12;
            gridView1.Columns["actualWorkHours"].VisibleIndex = 13;
            gridView1.Columns["dueDate"].VisibleIndex = 14;
            gridView1.Columns["dateEntered"].VisibleIndex = 15;
            gridView1.Columns["dateClosed"].VisibleIndex = 16;           
            
        }

        private void reorderEssentialColumns()
        {
            gridView1.Columns[attachColName].VisibleIndex = 0;
            gridView1.Columns[alertColName].VisibleIndex = 1;
            if (isUserTasksMode)
            {
                if (GetSelectedUserID() == loggedInUserID && !chkViewAllMembers.Checked)
                {
                    gridView1.Columns["privateTask"].VisibleIndex = 2;
                }
                if (chkViewAllMembers.Checked)
                {
                    gridView1.Columns["userID"].VisibleIndex = 3;
                }
                else
                {
                    gridView1.Columns["priority"].VisibleIndex = 3;
                }
            }
            else if (isMeetingTasksMode)
            {
                gridView1.Columns["userID"].VisibleIndex = 3;
            }
        }

        public void setStatusBarText(string text)
        {
            siStatus.Caption = text;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && gridView1.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
            {
                save(true);
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                setStatusBarText("Saving...");
                Application.DoEvents();
                //Thread.Sleep(500);
                save(true);
                setStatusBarText("Save Done");
                Application.DoEvents();
                //Thread.Sleep(500);
                setStatusBarText("");
                return true;
            }

            if (keyData == (Keys.Control | Keys.N))
            {
                gridView1.Focus();
                gridView1.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
                if (isMeetingTasksMode)
                    gridView1.FocusedColumn = gridView1.Columns["userID"];
                else if (isUserTasksMode)
                    gridView1.FocusedColumn = gridView1.Columns["taskName"];
                

                return true;
            }

            if (keyData == (Keys.F5))
            {
                setStatusBarText("Refreshing...");
                Application.DoEvents();
                refresh();
                setStatusBarText ("");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void BindUsersCombo(bool reloadFromDB)
        {
            if (reloadFromDB || repositoryUsers.DataSource == null)
            {
                repositoryCboUsers.DataSource = null;
                repositoryCboUsers.ValueMember = "ID";
                repositoryCboUsers.DisplayMember = "userName";               
                repositoryCboUsers.DataSource = myAllowedUsers;

                repositoryUsers.DataSource = null;
                repositoryUsers.ValueMember = "ID";
                repositoryUsers.DisplayMember = "userName";
                repositoryUsers.DataSource = myAllowedUsers;
                

                if (repositoryUsers.Columns.Count == 0)
                {
                    // Add two columns to the dropdown.
                    //repositoryCboUsers.Columns.Add(new LookUpColumnInfo("ID", 0));
                    //repositoryCboUsers.Columns.Add(new LookUpColumnInfo("userName", 150));
                    repositoryUsers.Columns.Add(new LookUpColumnInfo("userName", 150));
                }
                
                //set first user as selected index
                var users = repositoryUsers.DataSource as List<Users>;
                if (users.Count > 0)
                {
                    cboUsersSelector.EditValue = users[0].ID;
                }
                //var users = repositoryCboUsers.DataSource as List<Users>;              
                //if (users.Count > 0)
                //{
                //    cboUsers.EditValue = users[0].ID;
                //}

            }
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
        private void refreshPrioritizer()
        {
            Guid savedRowIndex = lastSelectedRowIndex;
            refreshTaskGrid(true);
            lastSelectedRowIndex = savedRowIndex;
            reSelectLastSelectedRow();

        }
        internal void save(bool refreshGrid)
        {
            
                tasksGrid.Focus();
                if (isMeetingTasksMode)
                {
                    meetingSummaryControl.SaveMeetingRTF();
                }

                Guid savedRowIndex = lastSelectedRowIndex;
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();

               
                saveTasksChanges();

                //reload from DB
                if (refreshGrid)
                    refreshTaskGrid(true);


                lastSelectedRowIndex = savedRowIndex;
                reSelectLastSelectedRow();

                saveGridLayout();    
        }


        private void reSelectLastSelectedRow()
        {

            var rowHandle = gridView1.LocateByValue("ID", lastSelectedRowIndex);
            if (rowHandle == -2147483648)
                gridView1.MoveFirst();
            else
                gridView1.FocusedRowHandle = rowHandle;

            bindToCurrentRow();
            //bool rowFound = false;            
            //rowFound = selectRowByTaskID(lastSelectedRowIndex);
            ////otherwise select first row
            //if (!rowFound && gridView1.RowCount> 1)
            //{
            //    gridView1.MoveFirst();
            //}
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

                    List<TaskSaveFailure> failedTasks = ConnectionManager.Proxy.applyChangesTasksList(changedTasksList.ToArray<Tasks>(), loggedInUserID).ToList<TaskSaveFailure>();
                    string failureMessages = string.Empty;
                    if (failedTasks.Count() > 0)
                    {
                        foreach (TaskSaveFailure t in failedTasks)
                        {
                            string message;
                            if (t.ExceptionType.ToLower().Contains("optimisticconcurrencyexception"))
                            {
                                message = "Concurrent updates with another user";
                            }
                            else
                            {
                                message = t.ExceptionMessage;
                            }
                            
                            failureMessages += string.Format("{0}... - {1}\n",t.Task.taskName.Substring(0, Math.Min(30, t.Task.taskName.Length)),message);
                        }
                        MessageBox.Show("Following tasks couldn't be saved:\n" + failureMessages, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
        }

        internal bool isTaskListDirty()
        {
            if (tasksGrid.DataSource == null)
                return false;

            List<Tasks> taskList = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource); ;
            foreach (Tasks t in taskList)
            {
                if (t.ChangeTracker.State != ObjectState.Unchanged)
                    return true;
            }
            return false;
        }

        private void saveOneTaskChanges(Tasks t, List<Tasks> changedTasksList)
        {
            try
            {
                if (t.ChangeTracker.State != ObjectState.Unchanged)
                {
                    setUserIDForNewTask(t);

                    if (t.ChangeTracker.State == ObjectState.Added)
                    {
                        if (isMeetingTasksMode)
                        {
                            Guid meetingID = Guid.Parse(cboMeetings.EditValue.ToString());
                            assignTaskToMeeting(t, meetingID);
                        }
                        t.taskStatusID = 1;//pending
                        t.requesterID = loggedInUserID;
                        
                        t.dateUpdated = DateTime.Now;
                        t.TenantID = _tenantID;
                       
                    }
                    changedTasksList.Add(t);                   
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

        private void setUserIDForNewTask(Tasks t)
        {
            if (t.userID == null && t.taskType == (int)enTaskType.ActionItem)
            {
                if (isUserTasksMode)
                    t.userID = GetSelectedUserID();
                else if (isMeetingTasksMode)
                    t.userID = loggedInUserID;
            }
        }


        /// <summary>
        /// fill the list of allowed users. these users are the ones current logged in user can view
        /// </summary>
        public  void ReloadAllowedUsers()
        {
            myAllowedUsers = new List<Users>();
            try
            {
                handleNewUser();
                var a = ConnectionManager.Proxy.findAllTeamMembersAllowedForUser(loggedInUserID);

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
            if (_domainControllerAuthenticate)
            {
                List<Users> u = ConnectionManager.Proxy.getUserByDomainName(UserDomainName, _tenantID).ToList();
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
                        newUser.TenantID = _tenantID;

                        if (ConnectionManager.Proxy.getUserByDomainName(UserDomainName, _tenantID).Count() > 0)
                            throw new Exception("User already exists");

                        loggedInUser = ConnectionManager.Proxy.applyChangesUsers(newUser, frmMain.loggedInUserID);

                        //if (loggedInUser == null)
                        //    loggedInUser = newUser;
                        
                        users = new BindingList<Users>();

                        loadLookups();
                        loadDictionaries();
                        authenticate();
                        initializePrioritizer();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Failed to register user with the following error: '{0}'", ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    }
                }
            }
        }

        private void initializePrioritizer()
        {
            loadImages();
            setFormCaption();
            ReloadAllowedUsers();
            addRichTextEditor();
            loadAlertsForLoggedInUser();
            InitShowTaskURL();
        }

        private static void InitShowTaskURL()
        {
            if (ShowTaskURL == string.Empty)
                ShowTaskURL = string.Format(ConnectionManager.Proxy.getShowTaskURL(), frmMain.HostAddress, frmMain.HostPort);
        }


        private static string ALERTS_TIME_FORMAT = "yyyy-MM-dd H:mm";
        private void loadAlertsForLoggedInUser()
        {
            _userAlertList = ConnectionManager.Proxy.getAlertsForUser(loggedInUser.ID).ToList();
            UserAlerts.Clear();
            foreach (var alert in _userAlertList)
            {
                if (!UserAlerts.ContainsKey(alert.taskID))
                    UserAlerts.Add(alert.taskID, alert);
            }

            UserAlertsByTime.Clear();
            foreach (var alert in _userAlertList)
            {

                string alertTimeStr = alert.nextAlert.Value.ToString(ALERTS_TIME_FORMAT); //truncate seconds
                DateTime alertTime = Convert.ToDateTime(alertTimeStr);
                if (!UserAlertsByTime.ContainsKey(alertTime))
                {
                    UserAlertsByTime.Add(alertTime, new List<Alerts>());
                    UserAlertsByTime[alertTime].Add(alert);
                }
                else
                {
                    if (UserAlertsByTime[alertTime] == null)
                        UserAlertsByTime[alertTime] = new List<Alerts>();
                    UserAlertsByTime[alertTime].Add(alert);
                }                
            }
            //sort by date
            UserAlertsByTime = (from entry in UserAlertsByTime orderby entry.Key ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            

        }

        private void addRichTextEditor()
        {
            meetingSummaryControl = new MeetingSummaryControl(this);
            //meetingSummaryControl.Location = new Point(0,40);
            panelMeetingSummary_Container.Controls.Add(this.meetingSummaryControl);
            panelMeetingSummary_Container.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
            panelMeetingSummary_Container.Controls[0].Dock = DockStyle.Fill;
            
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
        public void loadLookups()
        {
            usersList = new BindingList<Users>(ConnectionManager.Proxy.getUsers(_tenantID).OrderBy(p => p.userName).ToList());
            taskStatusList = new BindingList<Shared.Model.TaskStatus>(ConnectionManager.Proxy.getTaskStatusList().ToList());
            loadProjectList();
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


        private void loadProjectList()
        {
            ProjectList = ConnectionManager.Proxy.getProjectList(_tenantID).OrderBy(p => p.projectName).ToList();

            if (projectsDict != null)
                projectsDict.Clear();
            projectsDict = null;
            loadProjectsDictionary();
            if (_cboGridProject != null)
                _cboGridProject.DataSource = ProjectList;
        }

        private void loadProjectsDictionary()
        {
            projectsDict = new Dictionary<Guid, string>();
            foreach (var project in ProjectList)
            {
                projectsDict.Add(project.ID, project.projectName);
            }
        }

        public static string HostAddress;
        public static string HostPort;
        private void setFormCaption()
        {
            var clientVersion = APP_VERSION;
            ClientSection clientSection = (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");  
            ChannelEndpointElement endpoint = clientSection.Endpoints[0];            
            HostAddress = endpoint.Address.Host;
            HostPort = endpoint.Address.Port.ToString();
            this.Text = String.Format("{0} (User: {1} | Network: {2}) - (Client Version : {3} | Host : {4})", "Prioritizer", loggedInUser.userName, loggedInUser.Tenant.TenantName, clientVersion, HostAddress);
        }
       
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }
        
        private void refreshTaskGrid(bool reloadFromDB)
        {
           
                loadData(reloadFromDB);

                if (tasksGrid.DataSource != null)
                    tasksGrid.DataSource = null;

                allTaskList.ToList().ForEach(i => i.StartTracking()); //start the self tracking for each element

                bindGrid(allTaskList);

                initGrid();
                
                reSelectLastSelectedRow();
        }

        private void bindGrid(IList<Tasks> taskList)
        {
            tasksGrid.DataSource = taskList;            
        }
       
        private void loadData(bool reloadFromDB)
        {
            //try
            //{
                if (allTaskList == null || reloadFromDB)
                {
                    Guid selectedUser = GetSelectedUserID();
                    
                    if (isUserTasksMode)
                    {
                        loadTasksForUser(selectedUser);
                    }
                    else if (isMeetingTasksMode)
                    {
                        loadTasksForMeeting();
                    }

                    loadAlertsForLoggedInUser();
                }
            //}
            //catch (Exception ex)
            //{
            //    if (isPrioritizerDisconnected(ex))
            //    { setConnectionStatus(); }
            //    else
            //        throw ex;
            //}
        }

        private void loadTasksForMeeting()
        {
            Guid? selectedMeetingID = (cboMeetings.EditValue == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : Guid.Parse(cboMeetings.EditValue.ToString()));

            selectedMeeting = null;
            if (selectedMeetingID.HasValue && selectedMeetingID.Value != Guid.Empty)
            {
                setSelectedMeeting(selectedMeetingID.Value, true);
                if (selectedMeeting != null)
                    allTaskList = new BindingList<Tasks>(ConnectionManager.Proxy.getTasksForMeeting(selectedMeetingID.Value).ToList());
                //else if (allTaskList != null)
                //    allTaskList.Clear();
            }
            else
            {
                if (allTaskList != null)
                    allTaskList.Clear();
            }

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
            IList<Tasks> additionalTasksBoundToRTFDocument = ConnectionManager.Proxy.getTasksByIDs(taskIDs).ToList<Tasks>();
            foreach (var task in additionalTasksBoundToRTFDocument)
            {
                var tasks = allTaskList.Where(t => t.ID == task.ID);
                if (tasks.Count() == 0)
                    allTaskList.Add(task);
            }
        }
        public void bindMeetingsCombo(Guid? meetingCategory, bool reloadFromDB)
        {
            if (reloadFromDB || repositoryCboMeetings1.DataSource == null)
            {
                if (repositoryCboMeetings1.DataSource == null)
                {
                    repositoryCboMeetings1.DataSource = null;
                    repositoryCboMeetings1.ValueMember = "ID";
                    repositoryCboMeetings1.DisplayMember = "MeetingName";

                    // Add two columns to the dropdown on first bind
                    //repositoryCboMeetings1.Columns.Add(new LookUpColumnInfo("ID", 0));
                    repositoryCboMeetings1.Columns.Add(new LookUpColumnInfo("MeetingName", 150));
                }
                
                if (meetingCategory == null)
                    meetingCategory = Guid.Parse(cboMeetingCategory.EditValue.ToString());

                List<Meetings> meetingsList = null;

                meetingsList = ConnectionManager.Proxy.getMeetingList(meetingCategory.Value, loggedInUserID, chkFinishedMeetings.Checked, _tenantID).ToList();

                repositoryCboMeetings1.DataSource = meetingsList;

                toggleTasksGridEnabled(meetingsList);

            }
            
        }

        private void toggleTasksGridEnabled(List<Meetings> meetingsList)
        {
            if (isMeetingTasksMode && meetingsList != null)
            {
                
                if (meetingsList.Count() > 0)
                {
                    cboMeetings.EditValue = meetingsList[0].ID;
                    loadTasksForMeeting();
                    meetingSummaryControl.Enabled = tasksGrid.Enabled = true;
                    //meetingSummaryControl.SetAccessibility();
                }
                else if (meetingsList.Count == 0)
                {
                    loadTasksForMeeting();
                    meetingSummaryControl.Enabled = tasksGrid.Enabled = false;
                }
            }
            else
                meetingSummaryControl.Enabled = tasksGrid.Enabled = true;
        }

        public static readonly Guid MEETINGS_FILTER_ALL_MEETINGS = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static readonly Guid MEETINGS_FILTER_NO_CATEGORY = Guid.Parse("00000000-0000-0000-0000-000000000002");
        private void bindMeetingCategoryCombo(bool reloadFromDB)
        {
            if (reloadFromDB || repositoryCboMeetingCategory.DataSource == null)
            {
                repositoryCboMeetingCategory.ValueMember = "ID";
                repositoryCboMeetingCategory.DisplayMember = "CategoryName";
                List<MeetingCategory> mcl = getMeetingCategoryList(false, false);// repository.MeetingCategory.Where(m => m.CategoryOwner == loggedInUserID).OrderBy(p => p.CategoryName).ToList();
                mcl.Insert(0, new MeetingCategory() { ID = MEETINGS_FILTER_ALL_MEETINGS, CategoryName = "(All Meetings)" });
                mcl.Insert(1, new MeetingCategory() { ID = MEETINGS_FILTER_NO_CATEGORY, CategoryName = "(Meetings With No Category)" });
                repositoryCboMeetingCategory.DataSource = mcl;
                cboMeetingCategory.EditValue = MEETINGS_FILTER_ALL_MEETINGS;

                // Add two columns to the dropdown.
                if (repositoryCboMeetingCategory.Columns.Count == 0)
                {
                    //repositoryCboMeetingCategory.Columns.Add(new LookUpColumnInfo("ID", 0));
                    repositoryCboMeetingCategory.Columns.Add(new LookUpColumnInfo("CategoryName", 150));
                }
                
            }
        }
        public List<MeetingCategory> getMeetingCategoryList(bool addCategoryOnTop, bool filterCategoriesOnlyForLoggedInUser)
        {
            List<MeetingCategory> categoriesCopy = null;

            categoriesCopy = ConnectionManager.Proxy.getMeetingCategoryList(filterCategoriesOnlyForLoggedInUser, frmMain.loggedInUserID, _tenantID).ToList();

            if (addCategoryOnTop)
                categoriesCopy.Insert(0, new MeetingCategory() { ID = MEETINGS_FILTER_ALL_MEETINGS, CategoryName = "(No Category)" });
            return categoriesCopy;
        }
        private void BindControls()
        {
            bindMeetingCategoryCombo(false);
            bindMeetingsCombo(null, false);
            BindUsersCombo(false);


            //foreach (Users u in usersList)
            //{
            //    cboUserToCopyTo.ComboBoxElement.Items.Add(new RadComboBoxItem(u.userName, u.ID));
            //    cboUserToForwardTo.ComboBoxElement.Items.Add(new RadComboBoxItem(u.userName, u.ID));
            //}

            //if (cboMeetings.SelectedItem != null)
            //{
            //    btnAttendeesForm.Enabled = ((Meetings)cboMeetings.SelectedItem.DataBoundItem).MeetingOwner == loggedInUserID; ;
            //}
            //else
            //{
            //    btnAttendeesForm.Enabled = false;
            //}
        }

        private void loadTasksForUser(Guid selectedUser)
        {
            if (chkViewAllMembers.Checked)
                allTaskList = new BindingList<Tasks>(ConnectionManager.Proxy.getTasksForTeamMembers(chkFinished.Checked, chkCancelled.Checked, selectedUser, loggedInUserID).ToList());
            else
                allTaskList = new BindingList<Tasks>(ConnectionManager.Proxy.getTasksForUser(chkFinished.Checked, chkCancelled.Checked, selectedUser, loggedInUserID).ToList());
        }


        public static bool isConnected = true;
        //public static PrioritizerServiceClient _proxyClient;
        //public PrioritizerServiceClient ProxyClient
        //{
        //    get
        //    {
        //        initProxyClient();
        //        return _proxyClient;
        //    }
        //}


        //private void initProxyClient()
        //{
        //    try
        //    {
        //        if (!isConnected)
        //            return;

        //        if (_proxyClient == null || _proxyClient.State == CommunicationState.Closed || _proxyClient.State == CommunicationState.Faulted)
        //        {
        //            _proxyClient = new PrioritizerServiceClient();
        //            //_proxyClient.ClientCredentials.Windows.ClientCredential.UserName = Environment.UserDomainName;
        //            //_proxyClient.ClientCredentials.Windows.ClientCredential.Password = "dd";
        //        }
        //        Task pingTask = Task.Factory.StartNew(() => pingAsync(loggedInUserID));
        //        bool succeed = pingTask.Wait(PING_TIMEOUT_INTERVAL);
        //        if (!succeed)
        //        {
        //            throw new TimeoutException(PING_TIMEOUT_INTERVAL.ToString());
        //        }
        //        //_proxyClient.ping(loggedInUserID);
        //        isConnected = true;
        //        setConnectionStatus();

        //    }
        //    catch (TimeoutException ex)
        //    {
        //        Logger.Instance.Warn(string.Format("Ping timeout: {0} ms", ex.Message));
        //        isConnected = false;
        //        setConnectionStatus();

        //        startConnectionWatchdog();
        //        return;
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Instance.Warn(string.Format("Ping failure: {0}", ex.Message));
        //        isConnected = false;
        //        setConnectionStatus();
        //        if (isPrioritizerDisconnected(ex))  
        //        {
        //            startConnectionWatchdog();
        //            return;
        //        }
        //        else
        //            throw ex;
        //    }
        //}

       

        
        // This delegate enables asynchronous calls for setting the connection status control state
        //delegate void setConnectionstatusCallback();
        private void setConnectionStatus()
        {
            if (this.IsHandleCreated) //only if form was created already
            {
                if (ConnectionManager.IsAlive)
                {
                    BeginInvoke(new MethodInvoker(delegate { btnConnectionStatus.ImageIndex = 0; }));
                    BeginInvoke(new MethodInvoker(delegate { btnConnectionStatus.ForeColor = Color.Green; }));
                    BeginInvoke(new MethodInvoker(delegate { btnConnectionStatus.Text = "Connected"; }));
                    BeginInvoke(new MethodInvoker(delegate { setStatusBarText(""); }));
                }
                else
                {
                    BeginInvoke(new MethodInvoker(delegate { btnConnectionStatus.ImageIndex = 1; }));
                    BeginInvoke(new MethodInvoker(delegate { btnConnectionStatus.ForeColor = Color.Red; }));
                    BeginInvoke(new MethodInvoker(delegate { btnConnectionStatus.Text = "Disconnected"; }));
                }

                BeginInvoke(new MethodInvoker(delegate { btnSave.Enabled = ConnectionManager.IsAlive;}));
                BeginInvoke(new MethodInvoker(delegate { btnNew.Enabled = ConnectionManager.IsAlive;}));
                BeginInvoke(new MethodInvoker(delegate { btnRefresh.Enabled = ConnectionManager.IsAlive;}));
            }
            
            //if (this.InvokeRequired)
            //{
            //    setConnectionstatusCallback callback = new setConnectionstatusCallback(setConnectionStatus);
            //    this.Invoke(callback);
            //}
            //else
            //{
            //    if (isConnected)
            //    {
            //        btnConnectionStatus.ImageIndex = 0;
            //        btnConnectionStatus.ForeColor = Color.Green;
            //        btnConnectionStatus.Text = "Connected";
            //    }
            //    else
            //    {
            //        btnConnectionStatus.ImageIndex = 1;
            //        btnConnectionStatus.ForeColor = Color.Red;
            //        btnConnectionStatus.Text = "Disconnected";
            //    }
            //}
            

        }

        void pad_ShowMiniToolbar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((RichTextBox)sender).SelectedText))
                return;
            ShowSelectionMiniToolbar();
        }
        protected virtual void ShowSelectionMiniToolbar()
        {
            Point pt = Control.MousePosition;
            pt.Offset(0, -11);
            ribbonMiniToolbar1.Alignment = ContentAlignment.TopRight;
            ribbonMiniToolbar1.PopupMenu = null;
            ribbonMiniToolbar1.Show(pt);
        }
        
        void Pad_ShowPopupMenu(object sender, EventArgs e)
        {
            pmMain.RibbonToolbar = ribbonMiniToolbar1;
            pmMain.ShowPopup(Control.MousePosition);
        }
       
        private void addAttachmentColumn()
        {
            GridColumn attachCol = new GridColumn();
            attachCol.Name = attachColName;
            attachCol.FieldName = attachColName;
            attachCol.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            attachCol.Caption = " ";
            gridView1.Columns.Insert(1, attachCol);
            attachCol.Visible = true;
            gridView1.Columns[attachColName].VisibleIndex = 0;
            RepositoryItemPictureEdit pictureEdit = new RepositoryItemPictureEdit();
            gridView1.Columns[attachColName].ColumnEdit = pictureEdit;
            pictureEdit.NullText = " ";
            tasksGrid.RepositoryItems.Add(pictureEdit);
            gridView1.Columns[attachColName].ImageIndex =4;
            gridView1.Columns[attachColName].AppearanceHeader.Options.UseImage = true;
            gridView1.Columns[attachColName].AppearanceHeader.Options.UseFont = false;
            gridView1.Columns[attachColName].Width = 26;
            

        }
        private void addAlertColumn()
        {
            if (gridView1.Columns[alertColName] == null)
            {
                GridColumn alertCol = new GridColumn();
                alertCol.Name = alertColName;
                alertCol.FieldName = alertColName;
                alertCol.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                alertCol.Caption = " ";
                gridView1.Columns.Insert(1, alertCol);
                alertCol.Visible = true;
                gridView1.Columns[alertColName].VisibleIndex = 1;
                RepositoryItemPictureEdit pictureEdit = new RepositoryItemPictureEdit();
                gridView1.Columns[alertColName].ColumnEdit = pictureEdit;
                pictureEdit.NullText = " ";
                tasksGrid.RepositoryItems.Add(pictureEdit);
                gridView1.Columns[alertColName].ImageIndex = 8;
                gridView1.Columns[alertColName].AppearanceHeader.Options.UseImage = true;
                gridView1.Columns[alertColName].AppearanceHeader.Options.UseFont = false;
                gridView1.Columns[alertColName].Width = 26;
            }

        }

        private void dockManager1_EndDocking(object sender, DevExpress.XtraBars.Docking.EndDockingEventArgs e)
        {
            PanelRightPane.Width = this.Width;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //panelMeetingSummary.Width = this.Width;
            SetUserTasksPanelWidth();
        }

       

        private void ChangeUser()
        {
            //lastSelectedRowIndex = -1;
            if (cboUsersSelector.EditValue != null)
            {
                using (new Splash(this))
                {
                    refreshTaskGrid(true);
                    tasksGrid.Focus();
                }
            }
            else
                return;            
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new Splash(this))
            {
                save(true);
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //if (isNewRowAdded(e))
            //{

            //    List<Tasks> taskList = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource);
            //    foreach (var task in taskList)
            //    {
            //        task.priority++;
            //    }
            //}
        }

        private bool isNewRowAdded(DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            int visibleIndex = gridView1.GetVisibleIndex(e.RowHandle);
            int dataControllerRowIndex  = gridView1.DataController.GetControllerRowHandle(visibleIndex);
            return (gridView1.DataController.GetListSourceRowIndex(dataControllerRowIndex) == GridControl.InvalidRowHandle);
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //if (gridView1.selected != null && tasksGrid.CurrentRow.DataBoundItem != null)
            //{

            object rowID = gridView1.GetRowCellValue(e.RowHandle, "ID");
            if (rowID != null)
            {
                lastSelectedRowIndex = Guid.Parse(gridView1.GetRowCellValue(e.RowHandle, "ID").ToString());
                bindToCurrentRow();

                if (isMeetingTasksMode)
                {
                    meetingSummaryControl.saveCaretPosition();
                    meetingSummaryControl.focusOnHyperlink(lastSelectedRowIndex);
                }
            }
            //}
        }
        public void SetTopPriority(Guid taskID)
        {
            ConnectionManager.Proxy.moveAllTasksPriorityForUser(1, GetSelectedUserID());

            Tasks t = ConnectionManager.Proxy.getTaskByID(taskID);
            t.StartTracking();
            t.priority = 1;
            ConnectionManager.Proxy.applyChangesTasks(t, loggedInUserID);
            t.AcceptChanges();
            //repository.SaveChanges();

            //}

            refreshTaskGrid(true);
        }
        public string getLogDelimiterLine(logDelimiterMode mode)
        {
            string modeStr = mode.ToString();
            return string.Format("{0}{1}{2} - {7} by '{3}':{4}{5}{6}", LINE_DELIMITER, Environment.NewLine, DateTime.Now, friendlyUserName, Environment.NewLine, LINE_DELIMITER, Environment.NewLine, modeStr);

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
            catch (Exception ex) { }
        }
        public bool selectRowByTaskID(Guid taskID)
        {
            var rowHandle = gridView1.LocateByValue("ID", taskID);
            if (rowHandle == -2147483648)
                return false;
            else
            {
                gridView1.FocusedRowHandle = rowHandle;
                bindToCurrentRow();
                return true;
            }

          
        }

        public Tasks SelectedTask
        {
            get
            {
                if (gridView1.GetSelectedRows().Count()==0)
                    return null;
                return gridView1.GetRow(gridView1.GetSelectedRows().FirstOrDefault()) as Tasks;
            }
        }
        public void setSelectedMeeting(Guid meetingID, bool includeTasks)
        {
            selectedMeeting = ConnectionManager.Proxy.getMeetingByID(meetingID, includeTasks);
        }


        public Tasks getTaskByTaskID(Guid taskID)
        {
            var tasks = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource).Where(t => t.ID == taskID);
            if (tasks.Count() > 0)
                return tasks.First();
            else
                return null;
        }
        private void bindToCurrentRow()
        {
            //GridViewRowInfo currentSelectedRow = tasksGrid.CurrentRow;

            if (SelectedTask != null)
            {
                remarks.DataBindings.Clear();
                remarks.DataBindings.Add("Text", SelectedTask, "remarks");

                updateLog.DataBindings.Clear();
                updateLog.DataBindings.Add("Text", SelectedTask, "UpdatesLog");

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void iNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TaskForm newTaskForm = new TaskForm(this, new Tasks(), formMode.add, enTaskType.ActionItem);
            openTaskForm(newTaskForm);
        }

        private void tasksGrid_DoubleClick(object sender, EventArgs e)
        {            

            Point pt = gridView1.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = gridView1.CalcHitInfo(pt);

            if (info.InRow)
            {
                openSelectedTask();
            }
        }
        public void openSelectedTask()
        {
            Tasks taskToUpdate = SelectedTask;

            if (taskToUpdate == null)
                return;
            TaskForm taskForm = new TaskForm(this, taskToUpdate, formMode.update, enTaskType.ActionItem);
            openTaskForm(taskForm);
        }

        public void openTaskByID(Guid taskID)
        {
            Tasks taskToUpdate = ConnectionManager.Proxy.getTaskByID(taskID);

            if (taskToUpdate == null)
                return;
            TaskForm taskForm = new TaskForm(this, taskToUpdate, formMode.update, enTaskType.ActionItem);
            openTaskForm(taskForm);
        }

        
     
        private void chkFinished_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            refreshPrioritizer();
        }

        private void chkCancelled_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            refreshPrioritizer();
        }

        private void cboMeetingCategory_EditValueChanged(object sender, EventArgs e)
        {
            bindMeetingsCombo(null, true);
        }

        private void cboMeetings_EditValueChanged(object sender, EventArgs e)
        {
            using (new Splash(this))
            {
                save(false);
                refreshTaskGrid(true);
                meetingSummaryControl.syncActionItems();
                setMeetingInfo();
            }
        }

        
        private void setMeetingInfo()
        {
            if (selectedMeeting != null)
                PanelRightPane.Text = string.Format("{0} | Owner: {1} | Date: {2} | Attendees: {3}", selectedMeeting.MeetingName, getMeetingOwner(), getMeetingDate(), getMeetingAttendees());
            else
                PanelRightPane.Text = "Meeting Summary";
        }
        private string getMeetingAttendees()
        {

            Guid meetingID = Guid.Parse(cboMeetings.EditValue.ToString());
            currentMeetingAttendeesList = ConnectionManager.Proxy.getMeetingAttendeesUserList(meetingID).ToList();
            string attendeesList="";
            foreach (var attendee in currentMeetingAttendeesList)
                attendeesList += attendee.userName + ", ";
            if (attendeesList.Length > 0)
                attendeesList = attendeesList.Substring(0, attendeesList.Length - 2);
             return attendeesList;            
        }
        private string getMeetingDate()
        {
            string meetingDate = "";
            if (cboMeetings.EditValue != null && selectedMeeting != null)
                meetingDate = selectedMeeting.MeetingDate.ToString();
            else
                meetingDate = "";

            return  meetingDate;
        }

        private string getMeetingOwner()
        {
            string meetingOwner = "";
            if (cboMeetings.EditValue != null && selectedMeeting != null)
                meetingOwner = usersDict[selectedMeeting.MeetingOwner].userName;
            else
                meetingOwner= "N/A";
            return meetingOwner;
        }

        
        private void addNewMeeting()
        {
            MeetingForm newMeetingForm = new MeetingForm(this);
            newMeetingForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult res = newMeetingForm.ShowDialog();
            bindMeetingsCombo(null, false);
        }

        private void ribbonControl_SelectedPageChanged(object sender, EventArgs e)
        {
            if (isMeetingTasksMode || isUserTasksMode)
            {
                using (new Splash(this))
                {
                    toggleMeetingSummaryControl();
                    refreshTaskGrid(true);
                }
            }
            
        }
        private void ribbonControl_SelectedPageChanging(object sender, DevExpress.XtraBars.Ribbon.RibbonPageChangingEventArgs e)
        {
            if (ConnectionManager.IsAlive)
            {
                save(false);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toggleMeetingSummaryControl()
        {
            SetUserTasksPanelWidth();            
            if (isUserTasksMode)
            {
                PanelRightPane.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                PanelTaskList_Container.Controls[0].Dock = DockStyle.Fill;
                toggleTasksGridEnabled(null);

            }
            if (isMeetingTasksMode)
            {
                PanelRightPane.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                panelMeetingSummary_Container.Controls[0].Dock = DockStyle.Fill;
                PanelRightPane.Width = this.Width - PanelTaskList.Width;
                meetingSummaryControl.Refresh();
                setMeetingInfo();
                toggleTasksGridEnabled(repositoryCboMeetings1.DataSource as List<Meetings>);
            }
               
        }

        private void SetUserTasksPanelWidth()
        {
            if (isUserTasksMode)
            {
                _lastPanelTasksWidth = PanelTaskList.Width;
                PanelLeftPane.Width = this.Width - 10;
            }
            else if (isMeetingTasksMode)
            {
                PanelLeftPane.Width = _lastPanelTasksWidth;
                PanelRightPane.Width = this.Width - PanelLeftPane.Width;
                if (PanelRightPane.Width < 500)
                {
                    int thirdScreenWidth = this.Width/3;
                    PanelLeftPane.Width = thirdScreenWidth;
                    PanelRightPane.Width = 2 * thirdScreenWidth;
                }

            }
        }

        private void dockPanel1_Click(object sender, EventArgs e)
        {

        }

        private void tasksGrid_Click(object sender, EventArgs e)
        {
            Point pt = gridView1.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = gridView1.CalcHitInfo(pt);

            if (info.InColumn)
            {
                gridView1.FocusedRowHandle = info.RowHandle;
            }

            handleAttachColumnClickEvent(info);
            handleAlertColumnClickEvent(info);
        }

        private void handleAttachColumnClickEvent(GridHitInfo info)
        {
            if (info.Column != null && info.Column.FieldName == attachColName && SelectedTask != null)
            {

                Guid taskID = SelectedTask.ID;
                List<attachments> attachCollection = ConnectionManager.Proxy.getAttachmentsForTask(taskID).ToList();
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
                    ShowAttachmentsForm(attachCollection);
                }

            }
        }

        private void ShowAttachmentsForm(List<attachments> attachCollection)
        {
            attachmentsForm attachForm = new attachmentsForm(attachCollection, this);
            attachForm.StartPosition = FormStartPosition.CenterParent;
            attachForm.ShowDialog();
        }

        private void handleAlertColumnClickEvent(GridHitInfo info)
        {
            if (info.Column != null && info.Column.FieldName == alertColName && 
                isUserTasksMode && 
                GetSelectedUserID() == loggedInUserID &&
                !chkViewAllMembers.Checked)
            {
                openAlertForm(false,null);
            }
        }

        private void openAlertForm(bool showSnoozeOptions, Guid? taskID)
        {
            if (SelectedTask == null)
                return;
            if (taskID == null) 
                taskID = SelectedTask.ID;
            AlertForm alertForm = new AlertForm(this, showSnoozeOptions, taskID.Value);
            alertForm.StartPosition = FormStartPosition.CenterParent;
            alertForm.ShowDialog();
            loadAlertsForLoggedInUser();
        }

        private void btnAttendees_ItemClick(object sender, ItemClickEventArgs e)
        {
            MeetingAttendeesForm meetingAttendeesFrm = new MeetingAttendeesForm(Guid.Parse(cboMeetings.EditValue.ToString()), this);
            meetingAttendeesFrm.StartPosition = FormStartPosition.CenterParent;
            meetingAttendeesFrm.ShowDialog();
            //BindAttendeesList();
            setMeetingInfo();
        }

        private void btnSend_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new Splash(this))
            {
                exportToHTML();
            }
        }

      
        private void exportToHTML()
        {
            try
            {
                Prioritizer.Shared.Utils.createDir(ClientUtils.TEMP_DIR);
                string filePath = ClientUtils.TEMP_DIR + "prioritizerGridExport.html";

                string tableCaption = "";
                string subjectStr = "";
                string distributionList = "";
                string footer = "";
                if (isMeetingTasksMode)
                {

                    string meetingOwner = usersDict[selectedMeeting.MeetingOwner].userName;
                    string attendeesList = getMeetingAttendees();
                    distributionList = concatDistributionList(distributionList);
                    tableCaption += string.Format("<span style='text-align:left;font-size:10px;font-family:calibri'>(Generated by Prioritizer)<br><b>Action items for meeting:</b> '{0}' <br><b>Meeting Owner:</b> {1} <br><b>Attendees:</b> {2}</span>", selectedMeeting.MeetingName, meetingOwner, attendeesList);
                    subjectStr = string.Format("Action items for meeting: '{0}' ", selectedMeeting.MeetingName);
                    footer = "<br>" + meetingSummaryControl.getDocumentAsHtml();
                }
                else
                {
                    tableCaption += string.Format("Tasks summary for: '{0}'", cboUsersSelector);
                    subjectStr = tableCaption;
                }
                               
                MemoryStream ms = new MemoryStream();
                string decisionsGrid = "";
                string actionsGrid = ""; 
                try
                {
                    gridView1.BestFitColumns();

                    //decisions grid export
                    gridView1.ActiveFilterCriteria = GroupOperator.And(new NullOperator("userID"));                    
                    gridView1.ExportToHtml(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    StreamReader sr = new StreamReader(ms);
                    decisionsGrid = sr.ReadToEnd();

                    //action items grid export
                    ms.Close();
                    ms = new MemoryStream();
                    gridView1.ActiveFilterCriteria = null;
                    gridView1.ActiveFilterCriteria = GroupOperator.And(new NotOperator(new NullOperator("userID")));
                    gridView1.ExportToHtml(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    sr = new StreamReader(ms);
                    actionsGrid = sr.ReadToEnd();

                    gridView1.ActiveFilterCriteria = null;
                }
                finally
                {
                    ms.Close();
                }
                
                Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                oMailItem.Subject = subjectStr;
                oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                oMailItem.HTMLBody = tableCaption + "<br/>Decisions:<br/>" + decisionsGrid +  "<br/>Action Items:<br/>" + actionsGrid +  footer;

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


        #region drag drop

        private void dragDrop(DragEventArgs e, bool draggedIntoTaskGrid)
        {
            try
            {

                if (DragToChangeRowOrder(e)) //change rows order
                {
                    int sourceRowid;
                    int sourcePriority;
                    int targetPriority;
                    evaluateDragdropAction(e, out sourceRowid, out sourcePriority, out targetPriority);
                    //reorderRowsByPriority(sourceRowid, sourcePriority, targetPriority);

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

                    if (hoveredTask == null) //dragged into form 
                    {
                        CreateTaskFromDraggedFile(e, filestreams[0]);
                    }
                    else //dragged onto a row in the main grid
                    {
                        DragDecisionForm dragDecisionForm = new DragDecisionForm(string.Format("Attach to Task:\n '{0}'", hoveredTask.taskName));
                        dragDecisionForm.StartPosition = FormStartPosition.CenterParent;
                        dragDecisionForm.ShowDialog();

                        //DialogResult result = MessageBox.Show(string.Format("Add dragged file as attachment to task:\n '{0}'?\n Click 'Yes' To continue or 'No' to create new task from this file content", hoveredTask.taskName), "Drag Action", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (dragDecisionForm.Result == 0)
                        {
                            saveDraggedFileToDisk(filenames, filestreams, hoveredTask.ID);
                            refreshTaskGrid(false);
                        }
                        else if (dragDecisionForm.Result == 1)
                        {
                            Tasks createdTask = CreateTaskFromDraggedFile(e, filestreams[0]);
                            if (createdTask != null)
                                saveDraggedFileToDisk(filenames, filestreams, createdTask.ID);
                        }
                        else
                            return;
                    }
                    foreach (var stream in filestreams)
                        stream.Close();
                }
                refreshTaskGrid(true);
            }
            catch (Exception ex)
            {
                //suppress exceptions coming from dragging outlook objects into prioritizer. 
                //many problems with this on different machines with different outlook versions.
                //in worst case it won't work o
            }
        }

        private void tasksGrid_DragEnter(object sender, DragEventArgs e)
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


        private void evaluateDragdropAction(DragEventArgs e, out int sourceRowid, out int sourcePriority, out int targetPriority)
        {
            sourceRowid = (int)e.Data.GetData(typeof(int));
            sourcePriority = Convert.ToInt16(SelectedTask.priority);
            int targetRowid = -1;
            targetPriority = -1;
            //Console.WriteLine("source row:" + sourceRowid);
            if (hoveredTask != null)
            {
                targetRowid = Convert.ToInt16(hoveredTask.ID);
                targetPriority = Convert.ToInt16(hoveredTask.priority);
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

        private Tasks CreateTaskFromDraggedFile(DragEventArgs e, MemoryStream filestream)
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
                        return null;
                    }
                    myMailItem = (Microsoft.Office.Interop.Outlook.MailItem)sel[1];
                    TaskForm newTaskForm = new TaskForm(this, new Tasks() { taskName = myMailItem.Subject.ToString(), remarks = myMailItem.Body.ToString(), updateRequester = false, projectID = Guid.Parse("00000000-0000-0000-0000-000000000000"), requesterID = Guid.Parse("00000000-0000-0000-0000-000000000000") }, formMode.add, enTaskType.ActionItem);
                    openTaskForm(newTaskForm);
                    return newTaskForm._task;
                }
            }
            else
            {
                //using (StreamReader r = new StreamReader(filestream, Encoding.UTF8))
                //{
                StreamReader r = new StreamReader(filestream, Encoding.UTF8);
                TaskForm newTaskForm = new TaskForm(this, new Tasks() { taskName = "", remarks = r.ReadToEnd(), updateRequester = false, projectID = Guid.Parse("00000000-0000-0000-0000-000000000000"), requesterID = Guid.Parse("00000000-0000-0000-0000-000000000000") }, formMode.add, enTaskType.ActionItem);
                openTaskForm(newTaskForm);
                //r.Close();
                return newTaskForm._task;
                //}
            }
            return null;
        }

        private void tasksGrid_DragOver(object sender, DragEventArgs e)
        {
            //hoveredTask = e.Data as Tasks;


            GridControl grid = sender as GridControl;
            Point pt = grid.PointToClient(new Point(e.X, e.Y));
            BaseView view = grid.GetViewAt(pt);
            if(view == null) return;
            grid.FocusedView = view;
            if(view is GridView) {
                GridView gv = view as GridView;
                gv.FocusedRowHandle = gv.CalcHitInfo(pt).RowHandle;
                hoveredTask = gridView1.GetRow(gv.FocusedRowHandle) as Tasks;
            }

            //var scrpt = new Point(e.X, e.Y);//rgvDrag.PointToScreen(e.Location);
            //var pt = PointToClient(scrpt);
            //pt = tasksGrid.PointToClient(scrpt);
            //var element = tasksGrid.ElementTree.GetElementAtPoint(pt);
            //var cell = element as GridDataCellElement;
            //if (cell != null)
            //{
            //    var row = cell.RowElement;
            //    if (row != null && hoveredTask != row)
            //    {
            //        if (hoveredTask != null)
            //        {
            //            hoveredTask.IsMouseOver = false;
            //        }
            //        hoveredTask = row;
            //        hoveredTask.IsMouseOver = true;
            //    }
            //}
        }

        private void tasksGrid_DragLeave(object sender, EventArgs e)
        {
            hoveredTask = null;
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
        private void saveDraggedFileToDisk(string[] filenames, MemoryStream[] filestreams, Guid taskID)
        {
            for (int fileIndex = 0; fileIndex < filenames.Length; fileIndex++)
            {
                //use the fileindex to get the name and data stream
                string[] filePathParts = filenames[fileIndex].Split('\\');
                string filename = filePathParts[filePathParts.Length - 1];
                MemoryStream filestream = filestreams[fileIndex];
                StreamReader reader = new StreamReader(filestream);

                //save attachment file into DB
                attachments attachedFile = new attachments();
                attachedFile.bin = filestream.ToArray();
                attachedFile.taskID = taskID;
                attachedFile.fileName = filename;
                ConnectionManager.Proxy.addAttachment(attachedFile, loggedInUserID);
             
            }
        }

        private void tasksGrid_DragDrop(object sender, DragEventArgs e)
        {
            dragDrop(e, true);
        }
        #endregion

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveAndExit();
        }

        private void SaveAndExit()
        {
            if (!ConnectionManager.IsAlive)
            {
                if (MessageBox.Show("Prioritizer seems to be disconnected from server.\nWould you like to exit without saving?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    Application.Exit();
                else
                    return;
            }
            else
            {
                save(false);
                Application.Exit();
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveAndExit();
        }

        private void btnFinishedMeetings_CheckedChanged(object sender, ItemClickEventArgs e)
        {            
            bindMeetingsCombo(null, true);
            refreshPrioritizer();
        }

        private void chkFinished_CheckedChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void btnRefreshTaskList_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new Splash(this))
            {
                refreshTaskGrid(true);
            }
        }

        private void btnAddMeeting_ItemClick(object sender, ItemClickEventArgs e)
        {
            addNewMeeting();
            refreshMeetings();
            //meetingSummaryControl.createNewDocument();
        }

        private void btnRefreshMeetings_ItemClick(object sender, ItemClickEventArgs e)
        {
            refreshMeetings();
        }

        private void refreshMeetings()
        {
            bindMeetingsCombo(MEETINGS_FILTER_ALL_MEETINGS, true);
            bindMeetingCategoryCombo(true);
        }

        private void btnPriorityUp1_ItemClick(object sender, ItemClickEventArgs e)
        {               
            int selectedRowIndex = gridView1.FocusedRowHandle;
            Guid selectedTaskID = SelectedTask.ID;

            IList<Tasks> taskList = (new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource)).OrderBy(t => t.priority).ToList();

            //swap priorities
            if (selectedRowIndex <= 0)
                return;

            int? nextItemPriority = taskList[selectedRowIndex - 1].priority;
            int? selectedItemPriority = taskList[selectedRowIndex].priority;
            if (nextItemPriority == selectedItemPriority)
            {
                //see if more priority duplicates exists up the road
                for (int idx = selectedRowIndex - 2, shift = -2; idx + 1 > 0; idx--, shift--)
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

            selectRowByTaskID(selectedTaskID);           
            
        }

        private void btnPriorityDown_ItemClick(object sender, ItemClickEventArgs e)
        {
            int selectedRowIndex = gridView1.FocusedRowHandle;
            Guid selectedTaskID = SelectedTask.ID;
            IList<Tasks> taskList = (new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource)).OrderBy(t => t.priority).ToList();

            //swap priorities
            if (selectedRowIndex + 1 > taskList.Count - 1)
                return;

            int? nextItemPriority = taskList[selectedRowIndex + 1].priority;
            int? selectedItemPriority = taskList[selectedRowIndex].priority;
            if (nextItemPriority == selectedItemPriority)
            {
                //see if more priority duplicates exists down the road
                for (int idx = selectedRowIndex + 2, shift = 2; idx + 1 < taskList.Count - 1; idx++, shift++)
                {
                    if (taskList[selectedRowIndex].priority == taskList[idx].priority)
                        taskList[idx].priority += shift;
                }
                taskList[selectedRowIndex].priority += 1;
            }
            else
            {
                int? tempPriority = taskList[selectedRowIndex + 1].priority;
                taskList[selectedRowIndex + 1].priority = taskList[selectedRowIndex].priority;
                taskList[selectedRowIndex].priority = tempPriority;
            }

            tasksGrid.DataSource = taskList;

            selectRowByTaskID(selectedTaskID);
        }

        private void btnForwardMeeting_ItemClick(object sender, ItemClickEventArgs e)
        {

            List<Tasks> originalMeetingtaskList = new List<Tasks>((IEnumerable<Tasks>)tasksGrid.DataSource);
            List<Users> originalMeetingAttendeesList = new List<Users>(currentMeetingAttendeesList);
            //DialogResult res = MessageBox.Show("Move to existing meeting?", "Target Meeting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (res == System.Windows.Forms.DialogResult.Yes)
            //{
            //    //select from existing meetings
            //    selectedMeetingID = selectedMeeting.ID;
            //    if (selectedMeetingID == null)
            //        return;
            //}
            //else
            //{
                //create new meeting similar to current displayed meeting. then choose it as selected.
                string currentMeetingName = selectedMeeting.MeetingName;
                String NewMeetingName = Interaction.InputBox(
                    "Enter new meeting name", "Move To New Meeting", currentMeetingName + "_copy", -1, -1);

                if (NewMeetingName.Length == 0)
                    return;

                MeetingCategoryMap mcm = ConnectionManager.Proxy.getMeetingCategoryMapByID(selectedMeeting.ID);
                Guid? meetingCategory = null;
                if (mcm != null)
                    meetingCategory = mcm.MeetingCategoryID;

                //create new meeting and copy summary document from current
                Guid currentMeetingID = selectedMeeting.ID;
                Meetings currentMeeting = ConnectionManager.Proxy.getMeetingByID(currentMeetingID, false);
                new MeetingForm(this).CreateMeeting(NewMeetingName, DateTime.Now, meetingCategory, currentMeeting.MeetingSummaryRTF, originalMeetingAttendeesList);


                refreshMeetings();
                Guid newMeetingID = ConnectionManager.Proxy.getMeetingByName(NewMeetingName).ID;

            //}

            moveAllActiveTasksToSelectedMeeting(currentMeetingID,newMeetingID, originalMeetingtaskList);
            refreshTaskGrid(true);
        }


        private void moveAllActiveTasksToSelectedMeeting(Guid prevMeetingID,Guid selectedMeetingID, List<Tasks> taskList)
        {
            //List<Tasks> taskList = (List<Tasks>)tasksGrid.DataSource;
            List<MeetingTasks> changedMeetingTasks = new List<MeetingTasks>();
            foreach (var task in taskList)
            {
                //MeetingTasks meetingTask = null;
                List<MeetingTasks> meetingTaskList = null;
                if (task.taskStatusID != 4 && task.taskStatusID != 5)
                {
                    meetingTaskList = ConnectionManager.Proxy.getMeetingTasksByID(task.ID).ToList<MeetingTasks>();
                    foreach (var meetingTask in meetingTaskList)
                    {
                        if (meetingTask.MeetingID == prevMeetingID)
                        {
                            meetingTask.StartTracking();
                            meetingTask.MeetingID = selectedMeetingID;
                            meetingTask.TenantID = _tenantID;
                            changedMeetingTasks.Add(meetingTask);
                        }
                    }
                }
            }
            ConnectionManager.Proxy.applyChangesMeetingTasksList(changedMeetingTasks.ToArray<MeetingTasks>());
        }

        private void timerConnectionStatus_Tick(object sender, EventArgs e)
        {
            if (!ConnectionManager.IsAlive)
                return;
            //initProxyClient();
            //setConnectionStatus();
            checkAlerts();
            checkExitForUpgrade();
        }

        private void checkAlerts()
        {
            checkReminders();

            checkClientMessage();
        }

        private void checkClientMessage()
        {
            if (_clientMessage != null && _clientMessage.PokeList.Count > 0)
            {


                bool newPokesCameIn = false;
                foreach (var poke in _clientMessage.PokeList)
                {
                    if (poke.DeliveredByMailAfterTimeout)
                        continue;

                    string alertTextTitle = string.Empty;


                    switch (poke.Type)
                    {
                        case enPokeType.Invoker:
                            alertTextTitle = string.Format("<b>{0} From '{1}'</b>", Shared.Utils.getMoodName(poke.PokeMood), usersDict[poke.From].userName);
                            break;
                        case enPokeType.Reply:
                            alertTextTitle = string.Format("<b>Reply From '{0}'</b>", usersDict[poke.From].userName);
                            break;
                        case enPokeType.PlainMessage:
                            alertTextTitle = string.Format("<b>Message From '{0}'</b>", usersDict[poke.From].userName);
                            break;
                    }
                   
                    bool formAlreadyDisplayedOrPinned = checkIfAlertAlreadyVisible(alertPoke, poke.Comment);
                    if (!formAlreadyDisplayedOrPinned)
                    {
                        Image img = null;
                        if (poke.Type == enPokeType.Invoker)
                            img = getMoodImage(poke.PokeMood);
                        else if (poke.Type == enPokeType.PlainMessage)
                            img = Prioritizer.Properties.Resources.message30;
                        
                        alertPoke.Show(this, alertTextTitle, poke.Comment, null, img, poke);
                        newPokesCameIn = true;
                    }
                }
                if (newPokesCameIn)
                {
                    SoundPlayer myPlayer = new SoundPlayer(Resources.pokeAlertSound);
                    myPlayer.Play();
                }
                ConnectionManager.Proxy.ClientMessageReceiveConfirmation(loggedInUserID);
            }
        }

       
        private Image getMoodImage(enPokeMood mood)
        {
            switch (mood)
            {
                case enPokeMood.friendly:
                    return Prioritizer.Properties.Resources.Friendly30;
                    break;
                case enPokeMood.surprised:
                    return Prioritizer.Properties.Resources.Surprised30;
                    break;
                case enPokeMood.frustrated:
                    return Prioritizer.Properties.Resources.Frustrated30;
                    break;
                case enPokeMood.mad:
                    return Prioritizer.Properties.Resources.angry30;
                    break;
            }
            return null;
        }

        private void checkReminders()
        {
            var nowStr = DateTime.Now.ToString(ALERTS_TIME_FORMAT); //truncate seconds
            DateTime now = Convert.ToDateTime(nowStr);
            foreach (var alertList in UserAlertsByTime)
            {
                foreach (var alert in alertList.Value as List<Alerts>)
                {
                    if (alert.active.Value && alert.nextAlert.Value.ToLocalTime() < now)
                    {
                        bool formAlreadyDisplayedOrPinned = checkIfAlertAlreadyVisible(alertReminder, alert.Tasks.taskName);
                        if (!formAlreadyDisplayedOrPinned)
                            alertReminder.Show(this, "Task Alert", alert.Tasks.taskName, alert.Tasks.taskName, null, alert.taskID);
                    }
                }
            }
        }

        private bool checkIfAlertAlreadyVisible(DevExpress.XtraBars.Alerter.AlertControl alertControl, string alertName)
        {
            bool formAlreadyDisplayedOrPinned = false;
            foreach (var alertForm in alertControl.AlertFormList)
            {
                if (alertForm.Info.Text == alertName)
                {
                    formAlreadyDisplayedOrPinned = true;
                    continue;
                }

            }
            return formAlreadyDisplayedOrPinned;
        }

        private void btnLinkToMeeting2_ItemClick(object sender, ItemClickEventArgs e)
        {
            LinkTaskToMeeting();
        }

        private void LinkTaskToMeeting()
        {
            try
            {
                Guid? selectedMeeting = SelectMeeting();

                if (selectedMeeting.HasValue)
                {
                    MeetingTasks meetingTask = null;
                    Tasks t = ConnectionManager.Proxy.getTaskByID(SelectedTask.ID);
                    meetingTask = assignTaskToMeeting(t, selectedMeeting);
                    meetingTask.TenantID = _tenantID;
                    ConnectionManager.Proxy.applyChangesMeetingTasks(meetingTask);
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

        public object getMeetingsDatasource()
        {
            return repositoryCboMeetings1.DataSource;
        }

        private Guid? SelectMeeting()
        {
            ChooseMeetingForm chooseMeeting = new ChooseMeetingForm(this);
            chooseMeeting.StartPosition = FormStartPosition.CenterParent;
            chooseMeeting.ShowDialog();

            Guid? selectedMeeting = chooseMeeting.selectedMeetingID;
            return selectedMeeting;
        }
        private void btnLinkToMeeting_ItemClick(object sender, ItemClickEventArgs e)
        {
            LinkTaskToMeeting();
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            refreshTaskGrid(true);
        }

        
        public event EventHandler ShowPopupMenu;
        public event EventHandler ShowMiniToolbar;
        void RaiseShowPopupMenu()
        {
            if (ShowPopupMenu != null)
                ShowPopupMenu(remarks, EventArgs.Empty);
        }
        void RaiseShowMiniToolbar()
        {
            if (ShowMiniToolbar != null)
                ShowMiniToolbar(remarks, EventArgs.Empty);
        }

        private void remarks_MouseUp(object sender, MouseEventArgs e)
        {
            if (remarks.ClientRectangle.Contains(e.X, e.Y))
            {
                if ((e.Button & MouseButtons.Right) != 0)
                    RaiseShowPopupMenu();
                else if (e.Button == MouseButtons.Left)
                    RaiseShowMiniToolbar();
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            GridHitInfo hitInfo = gridView1.CalcHitInfo(e.ControlMousePosition);
            if (hitInfo.HitTest != GridHitTest.RowCell)
                return;

            GridColumn col = hitInfo.Column;

            if (col.Name == alertColName)
            {
                var row = gridView1.GetRow(hitInfo.RowHandle);
                var hoveredTask = row as Tasks;
                if (hoveredTask != null)
                {
                    bool alertIsActive = false;
                    if (UserAlerts.ContainsKey(hoveredTask.ID))
                    {
                        if (UserAlerts[hoveredTask.ID].active.HasValue)
                            alertIsActive = UserAlerts[hoveredTask.ID].active.Value;

                        if (UserAlerts[hoveredTask.ID].nextAlert.HasValue && alertIsActive)
                        {
                            e.Info = new ToolTipControlInfo("?!?!", string.Format("Alert Set to: '{0}'", UserAlerts[hoveredTask.ID].nextAlert.Value.ToLocalTime()));
                            return;
                        }
                    }                  
                    
                }
                if (hoveredTask != null && hoveredTask.dueDate != null)
                    e.Info = new ToolTipControlInfo("?!?!", string.Format("Due Date Set to: '{0}'", hoveredTask.dueDate));
            }
        }

      
        private void resetDockLayout()
        {
            PanelRightPane.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            PanelTaskList.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            txtAudit.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            PanelRemarks.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
        }

        private void alertControl1_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {
            openAlert(e);
        }

        private Guid taskID;
        private void openAlert(DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {
            taskID = Guid.Parse(e.AlertForm.AlertInfo.Tag.ToString());
            selectRowByTaskID(taskID);
            openAlertForm(true,taskID);
        }

        private void iSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            save(true);
        }

        private void oneMinuteTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void checkExitForUpgrade()
        {
            //if (isUpgradeRequired)
            //{
            //    if (exitForUpgradeTimestamp.AddMinutes(1) < DateTime.Now)
            //    {
            //        exitForUpgrade();
            //        //System.Environment.Exit(0);                    
            //    }
            //    else
            //        return;
            //}

            try
            {
                bool newVersionExist = ClientUtils.CheckNewVersion();
                if (newVersionExist && !isUpgradeRequired)
                {
                    save(false);
                    this.Enabled = false;
                    isUpgradeRequired = true;
                    ClientUtils.Upgrade(this);               
                }
            }
            catch (Exception ex) {
                ConnectionManager.IsConnectionRelatedException(ex);
                Logger.Instance.Error(ex.Message, ex);
            }
        }

       

        
  


       

        private void alertControl1_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertButtonClickEventArgs e)
        {
            openAlert(e);
        }

        private void btnManagerEmployeeHierarchy_ItemClick(object sender, ItemClickEventArgs e)
        {
            AurhorizationForm authorizatonForm = new AurhorizationForm(_tenantID, this);
            authorizatonForm.StartPosition = FormStartPosition.CenterParent;
            authorizatonForm.ShowDialog();

            loadLookups();
            ReloadAllowedUsers();
            BindUsersCombo(true);
        }

        private void btnManageUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            UsersForm usersForm = new UsersForm(_tenantID, this,null);
            usersForm.StartPosition = FormStartPosition.CenterParent;
            usersForm.ShowDialog();

            loadLookups();
        }

        

        private Users chooseUser()
        {
            ChooseUserForm chooseUser = new ChooseUserForm(this);
            chooseUser.StartPosition = FormStartPosition.CenterParent;
            chooseUser.ShowDialog();
            return chooseUser.SelectedUser;
        }
        private void copyTo(Users user)
        {
            ConnectionManager.Proxy.copyTo(SelectedTask.ID, user.ID, loggedInUserID);
            refreshTaskGrid(true);            
        }

        private void forwardTo(Users user)
        {
            ConnectionManager.Proxy.forwardTo(SelectedTask.ID, user.ID, loggedInUserID);
            refreshTaskGrid(true);
        }

       
        private void btnCopyTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            Users user = null;
            user = chooseUser();
            if (user != null)
            {
                copyTo(user);
                MessageBox.Show(string.Format("Task was successfully copied to '{0}'", user.userName), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnCloneTask_ItemClick(object sender, ItemClickEventArgs e)
        {
            copyTo(loggedInUser);
            MessageBox.Show(string.Format("Task was successfully Cloned"), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMoveTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            Users user = null;
            user = chooseUser();
            if (user != null)
            {
                forwardTo(user);
                MessageBox.Show(string.Format("Task was successfully moved to '{0}'", user.userName), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnDeleteAttachments_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SelectedTask == null)
                return;

            if (MessageBox.Show("Delete all Attachments for this task?", "Delete Attachments", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;

            Guid taskID = SelectedTask.ID;
            Tasks t = ConnectionManager.Proxy.getTaskByID(taskID);
            t.hasAttachment = false;
            ConnectionManager.Proxy.applyChangesTasks(t, loggedInUserID);

            List<attachments> attachments = ConnectionManager.Proxy.getAttachmentsForTask(taskID).ToList(); // attachmentRepository.attachments.Where(s => s.taskID == taskID).ToList();
            foreach (attachments a in attachments)
            {
                ConnectionManager.Proxy.deleteAttachment(a, frmMain.loggedInUserID);
            }

            refreshTaskGrid(true);
        }

        
        private void AddProject()
        {
            string newProjName = Interaction.InputBox("Enter Project Name", "Add New Project", "", -1, -1);
            if (newProjName.Trim().Length == 0)
            {
                MessageBox.Show("Project name can't be an empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            projects p = new projects() { projectName = newProjName };
            ConnectionManager.Proxy.applyChangesProjects(p, _tenantID);
            loadProjectList();
        }

        private void mnuEditProjects_ItemPress(object sender, ItemClickEventArgs e)
        {
            EditProjects();
        }

        private void EditProjects()
        {
            List<projects> projectCollection = ConnectionManager.Proxy.getProjectList(_tenantID).ToList();
            ProjectsForm frm = new ProjectsForm(projectCollection, this);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            loadProjectList();
        }

        private void mnuAddProject_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddProject();

        }

        private void mnuEditProjectList_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditProjects();
        }

        private void btnMeetingsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            MeetingsForm MeetinglistForm = new MeetingsForm(this);
            MeetinglistForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult res = MeetinglistForm.ShowDialog();
            refreshMeetings();
            loadData(true);
        }

        private void btnAddMeetingCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddMeetingCategory();
        }

        public string AddMeetingCategory()
        {
            String categoryName = Interaction.InputBox(string.Format("Enter Category Name"), "New Category", "", -1, -1);
            if (categoryName.Length > 0)
            {
                MeetingCategory mc = new MeetingCategory();
                mc.CategoryName = categoryName;
                mc.CategoryOwner = loggedInUserID;
                ConnectionManager.Proxy.applyChangesMeetingCategory(mc, _tenantID);
            }

            bindMeetingCategoryCombo(true);
            return categoryName;
        }

        private void btnEditCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            MeetingCategoriesForm MeetinglistForm = new MeetingCategoriesForm(this);
            MeetinglistForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult res = MeetinglistForm.ShowDialog();

            bindMeetingCategoryCombo(true);
        }

        private void btnUserManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            UsersForm userForm = new UsersForm(_tenantID, this, loggedInUser);
            userForm.StartPosition = FormStartPosition.CenterParent;
            userForm.ShowDialog();

        }

        private void btnChangePassword_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPassword();
        }

        private bool SetPassword()
        {
            SetPasswordForm passwordForm = new SetPasswordForm(this);
            passwordForm.StartPosition = FormStartPosition.CenterParent;
            passwordForm.ShowDialog();
            return (passwordForm.DialogResult == System.Windows.Forms.DialogResult.OK);
        }

      
        public static void LoadUserInfo()
        {
            if (File.Exists(frmMain.USER_INFO_FILE_PATH))
            {
                string fileContent = Prioritizer.Shared.Utils.LoadFile(frmMain.USER_INFO_FILE_PATH);

                frmMain.UserInfo =  JsonConvert.DeserializeObject<UserInfo>(fileContent);
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Guid taskID = SelectedTask.ID;
            List<attachments> attachCollection = ConnectionManager.Proxy.getAttachmentsForTask(taskID).ToList();
            ShowAttachmentsForm(attachCollection);
            refreshTaskGrid(true);
        }

        private void btnConnectionStatus_Click(object sender, EventArgs e)
        {
            ConnectionManager.CheckConnection();
        }

        private void cboUsers_EditValueChanged(object sender, EventArgs e)
        {
            //ChangeUser();            
        }

        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            ChangeUser();
        }

        private void ribbonControl_Click(object sender, EventArgs e)
        {

        }

        private void btnSendTask_ItemClick(object sender, ItemClickEventArgs e)
        {
            string taskURL = string.Format("{0}{1}",ShowTaskURL, SelectedTask.ID);
            Process.Start(taskURL);

        }

        private void btnPoke_ItemClick(object sender, ItemClickEventArgs e)
        {
            pokeAction();
        }

        private void pokeAction()
        {
            if (SelectedTask.userID != null)
            {
                PokeForm pokeFrm = new PokeForm(this, SelectedTask);
                pokeFrm.StartPosition = FormStartPosition.CenterParent;
                pokeFrm.ShowDialog();
            }
        }


        private void messageAction()
        {
            PokeMessageForm message = new PokeMessageForm(this,null);
            message.StartPosition = FormStartPosition.CenterParent;
            message.ShowDialog();
        }

        private void lnkAddRemark_Click(object sender, EventArgs e)
        {
            string remarkPrefix = string.Format("[{0}] - {1}: ", loggedInUser.userName, DateTime.Now);
            remarks.Text = string.Format("{0}\n{1}\n\n{2}", remarkPrefix,LINE_DELIMITER, remarks.Text);
            remarks.Focus();
            remarks.SelectionStart = remarkPrefix.Length ;

        }

        private void lnkAddRemark_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current =Cursors.Hand;
        }

        private void lnkAddRemark_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void chkViewAllMembers_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            btnPoke.Enabled = chkViewAllMembers.Checked;
            cboUsersSelector.Enabled = !chkViewAllMembers.Checked;
            using (new Splash(this))
            {
                refreshTaskGrid(true);
            }
            
        }

        private void btnPokeFromMeeting_ItemClick(object sender, ItemClickEventArgs e)
        {
            pokeAction();
        }

        private void btnMessage_ItemClick(object sender, ItemClickEventArgs e)
        {
            messageAction();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveGridLayout();
        }

        private void saveGridLayout()
        {
            gridView1.SaveLayoutToXml(string.Format(@"{0}\{1}", USER_INFO_DIRECTORY, USER_LAYOUT_FILE_NAME));
        }

       

        private void resetGridLayout()
        {
            File.Delete(string.Format(@"{0}\{1}", USER_INFO_DIRECTORY, USER_LAYOUT_FILE_NAME));
            gridInitialized = false;
            refreshTaskGrid(false);
        }

       

        private void btnResetDockingLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            resetDockLayout();
        }

        private void btnResetGridLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            resetGridLayout();
        }
    }
}