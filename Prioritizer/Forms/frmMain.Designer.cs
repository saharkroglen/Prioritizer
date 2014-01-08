namespace Prioritizer.Forms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::DevExpress.XtraBars.Demos.RibbonSimplePad.frmSplashScreen), true, true);
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.XtraBars.Alerter.AlertButton alertButton1 = new DevExpress.XtraBars.Alerter.AlertButton();
            DevExpress.XtraBars.Alerter.AlertButton alertButton2 = new DevExpress.XtraBars.Alerter.AlertButton();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.appMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.iSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.iExit = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.ribbonImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.iClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnPriorityDown = new DevExpress.XtraBars.BarButtonItem();
            this.iAbout = new DevExpress.XtraBars.BarButtonItem();
            this.siStatus = new DevExpress.XtraBars.BarStaticItem();
            this.alignButtonGroup = new DevExpress.XtraBars.BarButtonGroup();
            this.iBoldFontStyle = new DevExpress.XtraBars.BarButtonItem();
            this.iItalicFontStyle = new DevExpress.XtraBars.BarButtonItem();
            this.iUnderlinedFontStyle = new DevExpress.XtraBars.BarButtonItem();
            this.fontStyleButtonGroup = new DevExpress.XtraBars.BarButtonGroup();
            this.iLeftTextAlign = new DevExpress.XtraBars.BarButtonItem();
            this.iCenterTextAlign = new DevExpress.XtraBars.BarButtonItem();
            this.iRightTextAlign = new DevExpress.XtraBars.BarButtonItem();
            this.rgbiSkins = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.cboMeetings = new DevExpress.XtraBars.BarEditItem();
            this.repositoryCboMeetings1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.chkFinished = new DevExpress.XtraBars.BarCheckItem();
            this.chkCancelled = new DevExpress.XtraBars.BarCheckItem();
            this.cboMeetingCategory = new DevExpress.XtraBars.BarEditItem();
            this.repositoryCboMeetingCategory = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnAttendees = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddMeeting = new DevExpress.XtraBars.BarButtonItem();
            this.lblAttendees = new DevExpress.XtraBars.BarStaticItem();
            this.lblMeetingDate = new DevExpress.XtraBars.BarStaticItem();
            this.lblMeetingOwner = new DevExpress.XtraBars.BarStaticItem();
            this.btnSend = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefreshMeetings = new DevExpress.XtraBars.BarButtonItem();
            this.chkFinishedMeetings = new DevExpress.XtraBars.BarCheckItem();
            this.btnRefreshTaskList = new DevExpress.XtraBars.BarButtonItem();
            this.btnForwardMeeting = new DevExpress.XtraBars.BarButtonItem();
            this.btnLinkToMeeting = new DevExpress.XtraBars.BarButtonItem();
            this.btnLinkToMeeting2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.btnPriorityUp1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnManagerEmployeeHierarchy = new DevExpress.XtraBars.BarButtonItem();
            this.btnManageUsers = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveTo = new DevExpress.XtraBars.BarButtonItem();
            this.btnCopyTo = new DevExpress.XtraBars.BarButtonItem();
            this.btnCloneTask = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditAttachments = new DevExpress.XtraBars.BarButtonItem();
            this.mnuProjects = new DevExpress.XtraBars.BarSubItem();
            this.mnuAddProject = new DevExpress.XtraBars.BarButtonItem();
            this.mnuEditProjectList = new DevExpress.XtraBars.BarButtonItem();
            this.btnMeetingsList = new DevExpress.XtraBars.BarButtonItem();
            this.mnuMeetingSettings = new DevExpress.XtraBars.BarSubItem();
            this.btnAddMeetingCategory = new DevExpress.XtraBars.BarButtonItem();
            this.mnuMeetingCategories = new DevExpress.XtraBars.BarSubItem();
            this.btnEditCategories = new DevExpress.XtraBars.BarButtonItem();
            this.btnUserManagement = new DevExpress.XtraBars.BarButtonItem();
            this.btnChangePassword = new DevExpress.XtraBars.BarButtonItem();
            this.mnuAttachments = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.cboUsersSelector = new DevExpress.XtraBars.BarEditItem();
            this.repositoryUsers = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnGoToWeb = new DevExpress.XtraBars.BarButtonItem();
            this.btnPoke = new DevExpress.XtraBars.BarButtonItem();
            this.chkViewAllMembers = new DevExpress.XtraBars.BarCheckItem();
            this.btnPokeFromMeeting = new DevExpress.XtraBars.BarButtonItem();
            this.btnMessage = new DevExpress.XtraBars.BarButtonItem();
            this.btnResetGridLayout = new DevExpress.XtraBars.BarButtonItem();
            this.btnResetDockingLayout = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonImageCollectionLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.ribbonMiniToolbar1 = new DevExpress.XtraBars.Ribbon.RibbonMiniToolbar(this.components);
            this.ribbonUserTasks = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.attachmentsPage = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup9 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.homeRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.skinsRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup10 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.SettingsPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup8 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.AdminPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.UsersPage = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.projectsRibbon = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.helpRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.helpRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryCboUsers = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemSearchLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryCboMeetingCategory1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.tasksGrid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.PanelLeftPane = new DevExpress.XtraBars.Docking.DockPanel();
            this.PanelTaskList = new DevExpress.XtraBars.Docking.DockPanel();
            this.PanelTaskList_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.PanelBottomPane = new DevExpress.XtraBars.Docking.DockPanel();
            this.PanelRemarks = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.lnkAddRemark = new DevExpress.XtraEditors.LabelControl();
            this.remarks = new System.Windows.Forms.RichTextBox();
            this.txtAudit = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.updateLog = new DevExpress.XtraEditors.MemoEdit();
            this.PanelRightPane = new DevExpress.XtraBars.Docking.DockPanel();
            this.panelMeetingSummary_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.document3 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.document4 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.document1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timerConnectionStatus = new System.Windows.Forms.Timer(this.components);
            this.btnPriorityUp = new DevExpress.XtraBars.BarButtonItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.pmMain = new DevExpress.XtraBars.PopupMenu(this.components);
            this.alertReminder = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.btnConnectionStatus = new DevExpress.XtraEditors.SimpleButton();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.alertPoke = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryCboMeetings1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryCboMeetingCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollectionLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryCboUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryCboMeetingCategory1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tasksGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.PanelLeftPane.SuspendLayout();
            this.PanelTaskList.SuspendLayout();
            this.PanelTaskList_Container.SuspendLayout();
            this.PanelBottomPane.SuspendLayout();
            this.PanelRemarks.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.txtAudit.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateLog.Properties)).BeginInit();
            this.PanelRightPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.document3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmMain)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ApplicationButtonDropDownControl = this.appMenu;
            this.ribbonControl.ApplicationButtonText = null;
            this.ribbonControl.Controller = this.barAndDockingController1;
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Images = this.ribbonImageCollection;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.btnNew,
            this.iClose,
            this.btnPriorityDown,
            this.btnSave,
            this.iSaveAs,
            this.iExit,
            this.iAbout,
            this.siStatus,
            this.alignButtonGroup,
            this.iBoldFontStyle,
            this.iItalicFontStyle,
            this.iUnderlinedFontStyle,
            this.fontStyleButtonGroup,
            this.iLeftTextAlign,
            this.iCenterTextAlign,
            this.iRightTextAlign,
            this.rgbiSkins,
            this.cboMeetings,
            this.chkFinished,
            this.chkCancelled,
            this.cboMeetingCategory,
            this.btnAttendees,
            this.btnAddMeeting,
            this.lblAttendees,
            this.lblMeetingDate,
            this.lblMeetingOwner,
            this.btnSend,
            this.btnRefreshMeetings,
            this.chkFinishedMeetings,
            this.btnRefreshTaskList,
            this.btnForwardMeeting,
            this.btnLinkToMeeting,
            this.btnLinkToMeeting2,
            this.btnRefresh,
            this.btnPriorityUp1,
            this.btnManagerEmployeeHierarchy,
            this.btnManageUsers,
            this.btnMoveTo,
            this.btnCopyTo,
            this.btnCloneTask,
            this.btnEditAttachments,
            this.mnuProjects,
            this.mnuAddProject,
            this.mnuEditProjectList,
            this.btnMeetingsList,
            this.mnuMeetingSettings,
            this.btnAddMeetingCategory,
            this.mnuMeetingCategories,
            this.btnEditCategories,
            this.btnUserManagement,
            this.btnChangePassword,
            this.mnuAttachments,
            this.barButtonItem2,
            this.cboUsersSelector,
            this.btnGoToWeb,
            this.btnPoke,
            this.chkViewAllMembers,
            this.btnPokeFromMeeting,
            this.btnMessage,
            this.btnResetGridLayout,
            this.btnResetDockingLayout});
            this.ribbonControl.LargeImages = this.ribbonImageCollectionLarge;
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 137;
            this.ribbonControl.MiniToolbars.Add(this.ribbonMiniToolbar1);
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.PageHeaderItemLinks.Add(this.iAbout);
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonUserTasks,
            this.ribbonPage1,
            this.homeRibbonPage,
            this.SettingsPage,
            this.AdminPage,
            this.helpRibbonPage});
            this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryCboUsers,
            this.repositoryItemSearchLookUpEdit1,
            this.repositoryCboMeetingCategory1,
            this.repositoryItemCheckEdit3,
            this.repositoryCboMeetings1,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemComboBox1,
            this.repositoryItemGridLookUpEdit1,
            this.repositoryUsers});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl.Size = new System.Drawing.Size(1108, 145);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.Toolbar.ItemLinks.Add(this.btnNew);
            this.ribbonControl.Toolbar.ItemLinks.Add(this.iSaveAs);
            this.ribbonControl.Toolbar.ItemLinks.Add(this.btnRefresh);
            this.ribbonControl.Toolbar.ItemLinks.Add(this.iExit);
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            this.ribbonControl.SelectedPageChanging += new DevExpress.XtraBars.Ribbon.RibbonPageChangingEventHandler(this.ribbonControl_SelectedPageChanging);
            this.ribbonControl.SelectedPageChanged += new System.EventHandler(this.ribbonControl_SelectedPageChanged);
            this.ribbonControl.Click += new System.EventHandler(this.ribbonControl_Click);
            // 
            // appMenu
            // 
            this.appMenu.ItemLinks.Add(this.btnNew);
            this.appMenu.ItemLinks.Add(this.btnSave);
            this.appMenu.ItemLinks.Add(this.iSaveAs);
            this.appMenu.ItemLinks.Add(this.iExit);
            this.appMenu.Name = "appMenu";
            this.appMenu.Ribbon = this.ribbonControl;
            this.appMenu.ShowRightPane = true;
            // 
            // btnNew
            // 
            this.btnNew.Caption = "New";
            this.btnNew.Description = "Creates a new, blank file.";
            this.btnNew.Hint = "Creates new Action Item";
            this.btnNew.Id = 1;
            this.btnNew.ImageIndex = 0;
            this.btnNew.LargeImageIndex = 0;
            this.btnNew.Name = "btnNew";
            toolTipTitleItem1.Text = "Ctl+N";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.btnNew.SuperTip = superToolTip1;
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iNew_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "&Save";
            this.btnSave.Description = "Saves the active document.";
            this.btnSave.Hint = "Saves All Non-Saved Items";
            this.btnSave.Id = 16;
            this.btnSave.ImageIndex = 4;
            this.btnSave.LargeImageIndex = 4;
            this.btnSave.Name = "btnSave";
            toolTipTitleItem2.Text = "CTL+S";
            superToolTip2.Items.Add(toolTipTitleItem2);
            this.btnSave.SuperTip = superToolTip2;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // iSaveAs
            // 
            this.iSaveAs.Caption = "Save";
            this.iSaveAs.Description = "Saves All tasks";
            this.iSaveAs.Hint = "Saves All Non-Saved Items";
            this.iSaveAs.Id = 17;
            this.iSaveAs.ImageIndex = 5;
            this.iSaveAs.LargeImageIndex = 5;
            this.iSaveAs.Name = "iSaveAs";
            this.iSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iSaveAs_ItemClick);
            // 
            // iExit
            // 
            this.iExit.Caption = "Exit";
            this.iExit.Description = "Closes this program after prompting you to save unsaved data.";
            this.iExit.Hint = "Closes Prioritizer";
            this.iExit.Id = 20;
            this.iExit.ImageIndex = 6;
            this.iExit.LargeImageIndex = 6;
            this.iExit.Name = "iExit";
            this.iExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iExit_ItemClick);
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController1.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController1.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // ribbonImageCollection
            // 
            this.ribbonImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ribbonImageCollection.ImageStream")));
            this.ribbonImageCollection.Images.SetKeyName(0, "Ribbon_New_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(1, "Ribbon_Open_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(2, "Ribbon_Close_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(3, "Ribbon_Find_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(4, "Ribbon_Save_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(5, "Ribbon_SaveAs_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(6, "Ribbon_Exit_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(7, "Ribbon_Content_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(8, "Ribbon_Info_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(9, "Ribbon_Bold_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(10, "Ribbon_Italic_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(11, "Ribbon_Underline_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(12, "Ribbon_AlignLeft_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(13, "Ribbon_AlignCenter_16x16.png");
            this.ribbonImageCollection.Images.SetKeyName(14, "Ribbon_AlignRight_16x16.png");
            // 
            // iClose
            // 
            this.iClose.Caption = "&Close";
            this.iClose.Description = "Closes the active document.";
            this.iClose.Hint = "Closes the active document";
            this.iClose.Id = 3;
            this.iClose.ImageIndex = 2;
            this.iClose.LargeImageIndex = 2;
            this.iClose.Name = "iClose";
            this.iClose.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // btnPriorityDown
            // 
            this.btnPriorityDown.Caption = "Priority Down";
            this.btnPriorityDown.Description = "Lower Priority";
            this.btnPriorityDown.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPriorityDown.Glyph")));
            this.btnPriorityDown.Hint = "Lower Priority";
            this.btnPriorityDown.Id = 15;
            this.btnPriorityDown.LargeImageIndex = 3;
            this.btnPriorityDown.Name = "btnPriorityDown";
            this.btnPriorityDown.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnPriorityDown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPriorityDown_ItemClick);
            // 
            // iAbout
            // 
            this.iAbout.Caption = "About";
            this.iAbout.Description = "Displays general program information.";
            this.iAbout.Hint = "Displays general program information";
            this.iAbout.Id = 24;
            this.iAbout.ImageIndex = 8;
            this.iAbout.LargeImageIndex = 8;
            this.iAbout.Name = "iAbout";
            // 
            // siStatus
            // 
            this.siStatus.Id = 31;
            this.siStatus.Name = "siStatus";
            this.siStatus.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // alignButtonGroup
            // 
            this.alignButtonGroup.Caption = "Align Commands";
            this.alignButtonGroup.Id = 52;
            this.alignButtonGroup.ItemLinks.Add(this.iBoldFontStyle);
            this.alignButtonGroup.ItemLinks.Add(this.iItalicFontStyle);
            this.alignButtonGroup.ItemLinks.Add(this.iUnderlinedFontStyle);
            this.alignButtonGroup.Name = "alignButtonGroup";
            // 
            // iBoldFontStyle
            // 
            this.iBoldFontStyle.Caption = "Bold";
            this.iBoldFontStyle.Id = 53;
            this.iBoldFontStyle.ImageIndex = 9;
            this.iBoldFontStyle.Name = "iBoldFontStyle";
            // 
            // iItalicFontStyle
            // 
            this.iItalicFontStyle.Caption = "Italic";
            this.iItalicFontStyle.Id = 54;
            this.iItalicFontStyle.ImageIndex = 10;
            this.iItalicFontStyle.Name = "iItalicFontStyle";
            // 
            // iUnderlinedFontStyle
            // 
            this.iUnderlinedFontStyle.Caption = "Underlined";
            this.iUnderlinedFontStyle.Id = 55;
            this.iUnderlinedFontStyle.ImageIndex = 11;
            this.iUnderlinedFontStyle.Name = "iUnderlinedFontStyle";
            // 
            // fontStyleButtonGroup
            // 
            this.fontStyleButtonGroup.Caption = "Font Style";
            this.fontStyleButtonGroup.Id = 56;
            this.fontStyleButtonGroup.ItemLinks.Add(this.iLeftTextAlign);
            this.fontStyleButtonGroup.ItemLinks.Add(this.iCenterTextAlign);
            this.fontStyleButtonGroup.ItemLinks.Add(this.iRightTextAlign);
            this.fontStyleButtonGroup.Name = "fontStyleButtonGroup";
            // 
            // iLeftTextAlign
            // 
            this.iLeftTextAlign.Caption = "Left";
            this.iLeftTextAlign.Id = 57;
            this.iLeftTextAlign.ImageIndex = 12;
            this.iLeftTextAlign.Name = "iLeftTextAlign";
            // 
            // iCenterTextAlign
            // 
            this.iCenterTextAlign.Caption = "Center";
            this.iCenterTextAlign.Id = 58;
            this.iCenterTextAlign.ImageIndex = 13;
            this.iCenterTextAlign.Name = "iCenterTextAlign";
            // 
            // iRightTextAlign
            // 
            this.iRightTextAlign.Caption = "Right";
            this.iRightTextAlign.Id = 59;
            this.iRightTextAlign.ImageIndex = 14;
            this.iRightTextAlign.Name = "iRightTextAlign";
            // 
            // rgbiSkins
            // 
            this.rgbiSkins.Caption = "Skins";
            // 
            // 
            // 
            this.rgbiSkins.Gallery.AllowHoverImages = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rgbiSkins.Gallery.ColumnCount = 4;
            this.rgbiSkins.Gallery.FixedHoverImageSize = false;
            this.rgbiSkins.Gallery.ImageSize = new System.Drawing.Size(32, 17);
            this.rgbiSkins.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
            this.rgbiSkins.Gallery.RowCount = 4;
            this.rgbiSkins.Id = 60;
            this.rgbiSkins.Name = "rgbiSkins";
            // 
            // cboMeetings
            // 
            this.cboMeetings.Caption = "Choose Meeting:";
            this.cboMeetings.Edit = this.repositoryCboMeetings1;
            this.cboMeetings.Id = 69;
            this.cboMeetings.Name = "cboMeetings";
            this.cboMeetings.Width = 150;
            this.cboMeetings.EditValueChanged += new System.EventHandler(this.cboMeetings_EditValueChanged);
            // 
            // repositoryCboMeetings1
            // 
            this.repositoryCboMeetings1.AutoHeight = false;
            this.repositoryCboMeetings1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryCboMeetings1.Name = "repositoryCboMeetings1";
            this.repositoryCboMeetings1.NullText = "";
            // 
            // chkFinished
            // 
            this.chkFinished.Caption = "Finished";
            this.chkFinished.Glyph = ((System.Drawing.Image)(resources.GetObject("chkFinished.Glyph")));
            this.chkFinished.Hint = "Show Finished Tasks";
            this.chkFinished.Id = 71;
            this.chkFinished.Name = "chkFinished";
            this.chkFinished.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkFinished_CheckedChanged);
            this.chkFinished.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.chkFinished_ItemClick);
            // 
            // chkCancelled
            // 
            this.chkCancelled.Caption = "Cancelled";
            this.chkCancelled.Glyph = ((System.Drawing.Image)(resources.GetObject("chkCancelled.Glyph")));
            this.chkCancelled.Hint = "Show Cancelled Tasks";
            this.chkCancelled.Id = 72;
            this.chkCancelled.Name = "chkCancelled";
            this.chkCancelled.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.chkCancelled_ItemClick);
            // 
            // cboMeetingCategory
            // 
            this.cboMeetingCategory.Caption = "By Category:";
            this.cboMeetingCategory.Edit = this.repositoryCboMeetingCategory;
            this.cboMeetingCategory.Id = 73;
            this.cboMeetingCategory.Name = "cboMeetingCategory";
            this.cboMeetingCategory.Width = 120;
            this.cboMeetingCategory.EditValueChanged += new System.EventHandler(this.cboMeetingCategory_EditValueChanged);
            // 
            // repositoryCboMeetingCategory
            // 
            this.repositoryCboMeetingCategory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryCboMeetingCategory.Name = "repositoryCboMeetingCategory";
            this.repositoryCboMeetingCategory.NullText = "";
            // 
            // btnAttendees
            // 
            this.btnAttendees.Caption = "Attendees";
            this.btnAttendees.Glyph = ((System.Drawing.Image)(resources.GetObject("btnAttendees.Glyph")));
            this.btnAttendees.Hint = "Choose Meeting Attendees";
            this.btnAttendees.Id = 84;
            this.btnAttendees.Name = "btnAttendees";
            this.btnAttendees.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            this.btnAttendees.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAttendees.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAttendees_ItemClick);
            // 
            // btnAddMeeting
            // 
            this.btnAddMeeting.Caption = "New";
            this.btnAddMeeting.Glyph = ((System.Drawing.Image)(resources.GetObject("btnAddMeeting.Glyph")));
            this.btnAddMeeting.Hint = "Create New Meeting";
            this.btnAddMeeting.Id = 86;
            this.btnAddMeeting.Name = "btnAddMeeting";
            this.btnAddMeeting.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAddMeeting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddMeeting_ItemClick);
            // 
            // lblAttendees
            // 
            this.lblAttendees.Caption = "Attendees:";
            this.lblAttendees.Id = 87;
            this.lblAttendees.Name = "lblAttendees";
            this.lblAttendees.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblMeetingDate
            // 
            this.lblMeetingDate.Caption = "Meeting Date";
            this.lblMeetingDate.Id = 88;
            this.lblMeetingDate.Name = "lblMeetingDate";
            this.lblMeetingDate.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblMeetingOwner
            // 
            this.lblMeetingOwner.Caption = "Owner";
            this.lblMeetingOwner.Id = 89;
            this.lblMeetingOwner.Name = "lblMeetingOwner";
            this.lblMeetingOwner.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // btnSend
            // 
            this.btnSend.Caption = "Send";
            this.btnSend.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSend.Glyph")));
            this.btnSend.Hint = "Send Meeting by Mail";
            this.btnSend.Id = 90;
            this.btnSend.Name = "btnSend";
            this.btnSend.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnSend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSend_ItemClick);
            // 
            // btnRefreshMeetings
            // 
            this.btnRefreshMeetings.Caption = "Refresh Meeting List";
            this.btnRefreshMeetings.Glyph = ((System.Drawing.Image)(resources.GetObject("btnRefreshMeetings.Glyph")));
            this.btnRefreshMeetings.Id = 91;
            this.btnRefreshMeetings.Name = "btnRefreshMeetings";
            this.btnRefreshMeetings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefreshMeetings_ItemClick);
            // 
            // chkFinishedMeetings
            // 
            this.chkFinishedMeetings.Caption = "Show Finished";
            this.chkFinishedMeetings.Glyph = ((System.Drawing.Image)(resources.GetObject("chkFinishedMeetings.Glyph")));
            this.chkFinishedMeetings.Hint = "Show Also Meetings with No Pending Tasks";
            this.chkFinishedMeetings.Id = 93;
            this.chkFinishedMeetings.Name = "chkFinishedMeetings";
            this.chkFinishedMeetings.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFinishedMeetings_CheckedChanged);
            // 
            // btnRefreshTaskList
            // 
            this.btnRefreshTaskList.Caption = "Refresh";
            this.btnRefreshTaskList.Glyph = ((System.Drawing.Image)(resources.GetObject("btnRefreshTaskList.Glyph")));
            this.btnRefreshTaskList.Hint = "Refresh Task List";
            this.btnRefreshTaskList.Id = 94;
            this.btnRefreshTaskList.Name = "btnRefreshTaskList";
            this.btnRefreshTaskList.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem3.Text = "F5";
            superToolTip3.Items.Add(toolTipTitleItem3);
            this.btnRefreshTaskList.SuperTip = superToolTip3;
            this.btnRefreshTaskList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefreshTaskList_ItemClick);
            // 
            // btnForwardMeeting
            // 
            this.btnForwardMeeting.Caption = "Create Cont. Meeting";
            this.btnForwardMeeting.Glyph = ((System.Drawing.Image)(resources.GetObject("btnForwardMeeting.Glyph")));
            this.btnForwardMeeting.Id = 95;
            this.btnForwardMeeting.Name = "btnForwardMeeting";
            this.btnForwardMeeting.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnForwardMeeting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnForwardMeeting_ItemClick);
            // 
            // btnLinkToMeeting
            // 
            this.btnLinkToMeeting.Caption = "Link to Meeting";
            this.btnLinkToMeeting.Glyph = ((System.Drawing.Image)(resources.GetObject("btnLinkToMeeting.Glyph")));
            this.btnLinkToMeeting.Hint = "Link Selected Task to Meeting";
            this.btnLinkToMeeting.Id = 96;
            this.btnLinkToMeeting.Name = "btnLinkToMeeting";
            this.btnLinkToMeeting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLinkToMeeting_ItemClick);
            // 
            // btnLinkToMeeting2
            // 
            this.btnLinkToMeeting2.Caption = "Link to Meeting";
            this.btnLinkToMeeting2.Glyph = ((System.Drawing.Image)(resources.GetObject("btnLinkToMeeting2.Glyph")));
            this.btnLinkToMeeting2.Hint = "Link Selected Task to Meeting";
            this.btnLinkToMeeting2.Id = 97;
            this.btnLinkToMeeting2.Name = "btnLinkToMeeting2";
            this.btnLinkToMeeting2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLinkToMeeting2_ItemClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Refresh";
            this.btnRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Glyph")));
            this.btnRefresh.Id = 98;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // btnPriorityUp1
            // 
            this.btnPriorityUp1.Caption = "Priority Up";
            this.btnPriorityUp1.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPriorityUp1.Glyph")));
            this.btnPriorityUp1.Id = 99;
            this.btnPriorityUp1.Name = "btnPriorityUp1";
            this.btnPriorityUp1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPriorityUp1_ItemClick);
            // 
            // btnManagerEmployeeHierarchy
            // 
            this.btnManagerEmployeeHierarchy.Caption = "Hierarchy";
            this.btnManagerEmployeeHierarchy.Id = 103;
            this.btnManagerEmployeeHierarchy.Name = "btnManagerEmployeeHierarchy";
            this.btnManagerEmployeeHierarchy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnManagerEmployeeHierarchy_ItemClick);
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Caption = "Users";
            this.btnManageUsers.Id = 104;
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnManageUsers_ItemClick);
            // 
            // btnMoveTo
            // 
            this.btnMoveTo.Caption = "Move Task To";
            this.btnMoveTo.Id = 105;
            this.btnMoveTo.Name = "btnMoveTo";
            this.btnMoveTo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveTo_ItemClick);
            // 
            // btnCopyTo
            // 
            this.btnCopyTo.Caption = "Copy Task To";
            this.btnCopyTo.Id = 106;
            this.btnCopyTo.Name = "btnCopyTo";
            this.btnCopyTo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCopyTo_ItemClick);
            // 
            // btnCloneTask
            // 
            this.btnCloneTask.Caption = "Clone Task";
            this.btnCloneTask.Id = 107;
            this.btnCloneTask.Name = "btnCloneTask";
            this.btnCloneTask.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCloneTask_ItemClick);
            // 
            // btnEditAttachments
            // 
            this.btnEditAttachments.Caption = "Delete Attachments";
            this.btnEditAttachments.Id = 108;
            this.btnEditAttachments.Name = "btnEditAttachments";
            this.btnEditAttachments.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteAttachments_ItemClick);
            // 
            // mnuProjects
            // 
            this.mnuProjects.Caption = "Projects";
            this.mnuProjects.Id = 110;
            this.mnuProjects.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuAddProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuEditProjectList)});
            this.mnuProjects.Name = "mnuProjects";
            // 
            // mnuAddProject
            // 
            this.mnuAddProject.Caption = "Add Project";
            this.mnuAddProject.Id = 113;
            this.mnuAddProject.Name = "mnuAddProject";
            this.mnuAddProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuAddProject_ItemClick);
            // 
            // mnuEditProjectList
            // 
            this.mnuEditProjectList.Caption = "Edit Project List";
            this.mnuEditProjectList.Id = 114;
            this.mnuEditProjectList.Name = "mnuEditProjectList";
            this.mnuEditProjectList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuEditProjectList_ItemClick);
            // 
            // btnMeetingsList
            // 
            this.btnMeetingsList.Caption = "Edit Meetings";
            this.btnMeetingsList.Id = 115;
            this.btnMeetingsList.Name = "btnMeetingsList";
            this.btnMeetingsList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMeetingsList_ItemClick);
            // 
            // mnuMeetingSettings
            // 
            this.mnuMeetingSettings.Caption = "Meetings";
            this.mnuMeetingSettings.Glyph = ((System.Drawing.Image)(resources.GetObject("mnuMeetingSettings.Glyph")));
            this.mnuMeetingSettings.Id = 116;
            this.mnuMeetingSettings.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("mnuMeetingSettings.LargeGlyph")));
            this.mnuMeetingSettings.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefreshMeetings),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMeetingsList)});
            this.mnuMeetingSettings.Name = "mnuMeetingSettings";
            // 
            // btnAddMeetingCategory
            // 
            this.btnAddMeetingCategory.Caption = "Add Meeting Category";
            this.btnAddMeetingCategory.Id = 117;
            this.btnAddMeetingCategory.Name = "btnAddMeetingCategory";
            this.btnAddMeetingCategory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddMeetingCategory_ItemClick);
            // 
            // mnuMeetingCategories
            // 
            this.mnuMeetingCategories.Caption = "Meeting Categories";
            this.mnuMeetingCategories.Id = 119;
            this.mnuMeetingCategories.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("mnuMeetingCategories.LargeGlyph")));
            this.mnuMeetingCategories.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAddMeetingCategory),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEditCategories)});
            this.mnuMeetingCategories.Name = "mnuMeetingCategories";
            // 
            // btnEditCategories
            // 
            this.btnEditCategories.Caption = "Edit Categories";
            this.btnEditCategories.Id = 121;
            this.btnEditCategories.Name = "btnEditCategories";
            this.btnEditCategories.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditCategories_ItemClick);
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.Caption = "User Info";
            this.btnUserManagement.Id = 122;
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUserManagement_ItemClick);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Caption = "Change Password";
            this.btnChangePassword.Id = 123;
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChangePassword_ItemClick);
            // 
            // mnuAttachments
            // 
            this.mnuAttachments.Caption = "Attachments";
            this.mnuAttachments.Id = 124;
            this.mnuAttachments.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("mnuAttachments.LargeGlyph")));
            this.mnuAttachments.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEditAttachments),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.mnuAttachments.Name = "mnuAttachments";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Edit Attachments";
            this.barButtonItem2.Id = 125;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // cboUsersSelector
            // 
            this.cboUsersSelector.Caption = "User: ";
            this.cboUsersSelector.Edit = this.repositoryUsers;
            this.cboUsersSelector.Id = 129;
            this.cboUsersSelector.Name = "cboUsersSelector";
            this.cboUsersSelector.EditValueChanged += new System.EventHandler(this.barEditItem2_EditValueChanged);
            // 
            // repositoryUsers
            // 
            this.repositoryUsers.AutoHeight = false;
            this.repositoryUsers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryUsers.Name = "repositoryUsers";
            // 
            // btnGoToWeb
            // 
            this.btnGoToWeb.Caption = "Go To Web";
            this.btnGoToWeb.Id = 130;
            this.btnGoToWeb.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnGoToWeb.LargeGlyph")));
            this.btnGoToWeb.Name = "btnGoToWeb";
            this.btnGoToWeb.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSendTask_ItemClick);
            // 
            // btnPoke
            // 
            this.btnPoke.Caption = "Poke !";
            this.btnPoke.Enabled = false;
            this.btnPoke.Id = 131;
            this.btnPoke.LargeGlyph = global::Prioritizer.Properties.Resources.poke;
            this.btnPoke.Name = "btnPoke";
            this.btnPoke.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPoke_ItemClick);
            // 
            // chkViewAllMembers
            // 
            this.chkViewAllMembers.Caption = "View My Team";
            this.chkViewAllMembers.Id = 132;
            this.chkViewAllMembers.LargeGlyph = global::Prioritizer.Properties.Resources.team;
            this.chkViewAllMembers.Name = "chkViewAllMembers";
            this.chkViewAllMembers.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkViewAllMembers_CheckedChanged);
            // 
            // btnPokeFromMeeting
            // 
            this.btnPokeFromMeeting.Caption = "Poke !";
            this.btnPokeFromMeeting.Id = 133;
            this.btnPokeFromMeeting.LargeGlyph = global::Prioritizer.Properties.Resources.poke;
            this.btnPokeFromMeeting.Name = "btnPokeFromMeeting";
            this.btnPokeFromMeeting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPokeFromMeeting_ItemClick);
            // 
            // btnMessage
            // 
            this.btnMessage.Caption = "Send Message";
            this.btnMessage.Id = 134;
            this.btnMessage.LargeGlyph = global::Prioritizer.Properties.Resources.message48;
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMessage_ItemClick);
            // 
            // btnResetGridLayout
            // 
            this.btnResetGridLayout.Caption = "Reset Task Grid Layout";
            this.btnResetGridLayout.Id = 135;
            this.btnResetGridLayout.Name = "btnResetGridLayout";
            this.btnResetGridLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnResetGridLayout_ItemClick);
            // 
            // btnResetDockingLayout
            // 
            this.btnResetDockingLayout.Caption = "Reset Docking Layout";
            this.btnResetDockingLayout.Id = 136;
            this.btnResetDockingLayout.Name = "btnResetDockingLayout";
            this.btnResetDockingLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnResetDockingLayout_ItemClick);
            // 
            // ribbonImageCollectionLarge
            // 
            this.ribbonImageCollectionLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.ribbonImageCollectionLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ribbonImageCollectionLarge.ImageStream")));
            this.ribbonImageCollectionLarge.Images.SetKeyName(0, "Ribbon_New_32x32.png");
            this.ribbonImageCollectionLarge.Images.SetKeyName(1, "Ribbon_Open_32x32.png");
            this.ribbonImageCollectionLarge.Images.SetKeyName(2, "Ribbon_Close_32x32.png");
            this.ribbonImageCollectionLarge.Images.SetKeyName(3, "Ribbon_Find_32x32.png");
            this.ribbonImageCollectionLarge.Images.SetKeyName(4, "Ribbon_Save_32x32.png");
            this.ribbonImageCollectionLarge.Images.SetKeyName(5, "Ribbon_SaveAs_32x32.png");
            this.ribbonImageCollectionLarge.Images.SetKeyName(6, "Ribbon_Exit_32x32.png");
            this.ribbonImageCollectionLarge.Images.SetKeyName(7, "Ribbon_Content_32x32.png");
            this.ribbonImageCollectionLarge.Images.SetKeyName(8, "Ribbon_Info_32x32.png");
            // 
            // ribbonMiniToolbar1
            // 
            this.ribbonMiniToolbar1.ItemLinks.Add(this.alignButtonGroup);
            this.ribbonMiniToolbar1.ItemLinks.Add(this.fontStyleButtonGroup);
            // 
            // ribbonUserTasks
            // 
            this.ribbonUserTasks.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3,
            this.ribbonPageGroup2,
            this.attachmentsPage,
            this.ribbonPageGroup1,
            this.ribbonPageGroup9});
            this.ribbonUserTasks.Name = "ribbonUserTasks";
            this.ribbonUserTasks.Text = "User Tasks";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnRefreshTaskList);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnPriorityUp1);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnPriorityDown);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnLinkToMeeting2);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnMoveTo);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnCopyTo);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnCloneTask);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnPoke, "POKE USER ASSIGNED TO THIS TASK");
            this.ribbonPageGroup3.ItemLinks.Add(this.btnMessage);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Tasks";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.cboUsersSelector);
            this.ribbonPageGroup2.ItemLinks.Add(this.chkViewAllMembers);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Users";
            // 
            // attachmentsPage
            // 
            this.attachmentsPage.ItemLinks.Add(this.mnuAttachments);
            this.attachmentsPage.Name = "attachmentsPage";
            this.attachmentsPage.Text = "Misc";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.chkFinished);
            this.ribbonPageGroup1.ItemLinks.Add(this.chkCancelled);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Filter";
            // 
            // ribbonPageGroup9
            // 
            this.ribbonPageGroup9.ItemLinks.Add(this.btnGoToWeb);
            this.ribbonPageGroup9.Name = "ribbonPageGroup9";
            this.ribbonPageGroup9.Text = "Share";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup7,
            this.ribbonPageGroup4,
            this.ribbonPageGroup5,
            this.ribbonPageGroup6});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Meeting Tasks";
            // 
            // ribbonPageGroup7
            // 
            this.ribbonPageGroup7.ItemLinks.Add(this.btnAddMeeting);
            this.ribbonPageGroup7.ItemLinks.Add(this.btnAttendees);
            this.ribbonPageGroup7.ItemLinks.Add(this.btnForwardMeeting);
            this.ribbonPageGroup7.ItemLinks.Add(this.btnLinkToMeeting);
            this.ribbonPageGroup7.ItemLinks.Add(this.btnPokeFromMeeting);
            this.ribbonPageGroup7.Name = "ribbonPageGroup7";
            this.ribbonPageGroup7.Text = "Actions";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.cboMeetings);
            this.ribbonPageGroup4.ItemLinks.Add(this.mnuMeetingSettings);
            this.ribbonPageGroup4.ItemLinks.Add(this.mnuMeetingCategories);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Meetings";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.cboMeetingCategory);
            this.ribbonPageGroup5.ItemLinks.Add(this.chkFinishedMeetings);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "Filter";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.btnSend);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.Text = "Share";
            // 
            // homeRibbonPage
            // 
            this.homeRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.skinsRibbonPageGroup,
            this.ribbonPageGroup10});
            this.homeRibbonPage.Name = "homeRibbonPage";
            this.homeRibbonPage.Text = "Appearance";
            // 
            // skinsRibbonPageGroup
            // 
            this.skinsRibbonPageGroup.ItemLinks.Add(this.rgbiSkins);
            this.skinsRibbonPageGroup.Name = "skinsRibbonPageGroup";
            this.skinsRibbonPageGroup.ShowCaptionButton = false;
            this.skinsRibbonPageGroup.Text = "Skins";
            // 
            // ribbonPageGroup10
            // 
            this.ribbonPageGroup10.ItemLinks.Add(this.btnResetGridLayout);
            this.ribbonPageGroup10.ItemLinks.Add(this.btnResetDockingLayout);
            this.ribbonPageGroup10.Name = "ribbonPageGroup10";
            this.ribbonPageGroup10.Text = "Layout";
            // 
            // SettingsPage
            // 
            this.SettingsPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup8});
            this.SettingsPage.Name = "SettingsPage";
            this.SettingsPage.Text = "Settings";
            // 
            // ribbonPageGroup8
            // 
            this.ribbonPageGroup8.ItemLinks.Add(this.btnUserManagement);
            this.ribbonPageGroup8.ItemLinks.Add(this.btnChangePassword);
            this.ribbonPageGroup8.Name = "ribbonPageGroup8";
            this.ribbonPageGroup8.Text = "User";
            // 
            // AdminPage
            // 
            this.AdminPage.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AdminPage.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AdminPage.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AdminPage.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.AdminPage.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.AdminPage.Appearance.Options.UseBackColor = true;
            this.AdminPage.Appearance.Options.UseBorderColor = true;
            this.AdminPage.Appearance.Options.UseFont = true;
            this.AdminPage.Appearance.Options.UseForeColor = true;
            this.AdminPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.UsersPage,
            this.projectsRibbon});
            this.AdminPage.Name = "AdminPage";
            this.AdminPage.Text = "Admin";
            // 
            // UsersPage
            // 
            this.UsersPage.ItemLinks.Add(this.btnManagerEmployeeHierarchy);
            this.UsersPage.ItemLinks.Add(this.btnManageUsers);
            this.UsersPage.Name = "UsersPage";
            this.UsersPage.Text = "Users";
            // 
            // projectsRibbon
            // 
            this.projectsRibbon.ItemLinks.Add(this.mnuProjects);
            this.projectsRibbon.Name = "projectsRibbon";
            this.projectsRibbon.Text = "Projects";
            // 
            // helpRibbonPage
            // 
            this.helpRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.helpRibbonPageGroup});
            this.helpRibbonPage.Name = "helpRibbonPage";
            this.helpRibbonPage.Text = "Help";
            // 
            // helpRibbonPageGroup
            // 
            this.helpRibbonPageGroup.ItemLinks.Add(this.iAbout);
            this.helpRibbonPageGroup.Name = "helpRibbonPageGroup";
            this.helpRibbonPageGroup.Text = "Help";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryCboUsers
            // 
            this.repositoryCboUsers.AutoHeight = false;
            this.repositoryCboUsers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryCboUsers.Name = "repositoryCboUsers";
            this.repositoryCboUsers.NullText = "";
            // 
            // repositoryItemSearchLookUpEdit1
            // 
            this.repositoryItemSearchLookUpEdit1.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit1.Name = "repositoryItemSearchLookUpEdit1";
            this.repositoryItemSearchLookUpEdit1.View = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryCboMeetingCategory1
            // 
            this.repositoryCboMeetingCategory1.AutoHeight = false;
            this.repositoryCboMeetingCategory1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryCboMeetingCategory1.Name = "repositoryCboMeetingCategory1";
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.siStatus);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 644);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1108, 27);
            // 
            // tasksGrid
            // 
            this.tasksGrid.AllowDrop = true;
            this.tasksGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tasksGrid.Location = new System.Drawing.Point(8, 3);
            this.tasksGrid.MainView = this.gridView1;
            this.tasksGrid.Name = "tasksGrid";
            this.tasksGrid.Size = new System.Drawing.Size(804, 327);
            this.tasksGrid.TabIndex = 0;
            this.tasksGrid.ToolTipController = this.toolTipController1;
            this.tasksGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.tasksGrid.Click += new System.EventHandler(this.tasksGrid_Click);
            this.tasksGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.tasksGrid_DragDrop);
            this.tasksGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.tasksGrid_DragEnter);
            this.tasksGrid.DragOver += new System.Windows.Forms.DragEventHandler(this.tasksGrid_DragOver);
            this.tasksGrid.DragLeave += new System.EventHandler(this.tasksGrid_DragLeave);
            this.tasksGrid.DoubleClick += new System.EventHandler(this.tasksGrid_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridView1.GridControl = this.tasksGrid;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridView1.Images = this.imageList2;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "attach.png");
            this.imageList2.Images.SetKeyName(5, "");
            this.imageList2.Images.SetKeyName(6, "down.png");
            this.imageList2.Images.SetKeyName(7, "up.png");
            this.imageList2.Images.SetKeyName(8, "alarm_clock.png");
            this.imageList2.Images.SetKeyName(9, "");
            this.imageList2.Images.SetKeyName(10, "alarm_clock_red.png");
            // 
            // toolTipController1
            // 
            this.toolTipController1.AllowHtmlText = true;
            this.toolTipController1.InitialDelay = 300;
            this.toolTipController1.Rounded = true;
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.PanelLeftPane,
            this.PanelRightPane});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.dockManager1.EndDocking += new DevExpress.XtraBars.Docking.EndDockingEventHandler(this.dockManager1_EndDocking);
            // 
            // PanelLeftPane
            // 
            this.PanelLeftPane.Controls.Add(this.PanelTaskList);
            this.PanelLeftPane.Controls.Add(this.PanelBottomPane);
            this.PanelLeftPane.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.PanelLeftPane.ID = new System.Guid("9f785e8c-7115-468d-bc03-d4d12e795851");
            this.PanelLeftPane.Location = new System.Drawing.Point(0, 145);
            this.PanelLeftPane.Name = "PanelLeftPane";
            this.PanelLeftPane.OriginalSize = new System.Drawing.Size(823, 200);
            this.PanelLeftPane.Size = new System.Drawing.Size(823, 499);
            this.PanelLeftPane.Text = "panelContainer1";
            // 
            // PanelTaskList
            // 
            this.PanelTaskList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelTaskList.AutoScroll = true;
            this.PanelTaskList.Controls.Add(this.PanelTaskList_Container);
            this.PanelTaskList.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.PanelTaskList.ID = new System.Guid("922407a0-5757-4747-b965-439662f10d1e");
            this.PanelTaskList.Location = new System.Drawing.Point(0, 0);
            this.PanelTaskList.Name = "PanelTaskList";
            this.PanelTaskList.Options.ShowCloseButton = false;
            this.PanelTaskList.OriginalSize = new System.Drawing.Size(774, 360);
            this.PanelTaskList.Size = new System.Drawing.Size(823, 360);
            this.PanelTaskList.Text = "Tasks Grid";
            // 
            // PanelTaskList_Container
            // 
            this.PanelTaskList_Container.Controls.Add(this.tasksGrid);
            this.PanelTaskList_Container.Location = new System.Drawing.Point(4, 23);
            this.PanelTaskList_Container.Name = "PanelTaskList_Container";
            this.PanelTaskList_Container.Size = new System.Drawing.Size(815, 333);
            this.PanelTaskList_Container.TabIndex = 0;
            // 
            // PanelBottomPane
            // 
            this.PanelBottomPane.Controls.Add(this.PanelRemarks);
            this.PanelBottomPane.Controls.Add(this.txtAudit);
            this.PanelBottomPane.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.PanelBottomPane.FloatVertical = true;
            this.PanelBottomPane.ID = new System.Guid("e9911437-4f1f-4477-8939-31dee1236452");
            this.PanelBottomPane.Location = new System.Drawing.Point(0, 360);
            this.PanelBottomPane.Name = "PanelBottomPane";
            this.PanelBottomPane.OriginalSize = new System.Drawing.Size(823, 138);
            this.PanelBottomPane.Size = new System.Drawing.Size(823, 139);
            this.PanelBottomPane.Text = "panelContainer2";
            // 
            // PanelRemarks
            // 
            this.PanelRemarks.Controls.Add(this.dockPanel1_Container);
            this.PanelRemarks.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.PanelRemarks.FloatVertical = true;
            this.PanelRemarks.ID = new System.Guid("e079c064-72b1-4612-b233-ff821b11b573");
            this.PanelRemarks.Location = new System.Drawing.Point(0, 0);
            this.PanelRemarks.Name = "PanelRemarks";
            this.PanelRemarks.Options.ShowCloseButton = false;
            this.PanelRemarks.OriginalSize = new System.Drawing.Size(410, 138);
            this.PanelRemarks.Size = new System.Drawing.Size(410, 139);
            this.PanelRemarks.Text = "Task Body";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.lnkAddRemark);
            this.dockPanel1_Container.Controls.Add(this.remarks);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(402, 112);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // lnkAddRemark
            // 
            this.lnkAddRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkAddRemark.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkAddRemark.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.lnkAddRemark.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAddRemark.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lnkAddRemark.Location = new System.Drawing.Point(331, 0);
            this.lnkAddRemark.Name = "lnkAddRemark";
            this.lnkAddRemark.Size = new System.Drawing.Size(67, 13);
            this.lnkAddRemark.TabIndex = 12;
            this.lnkAddRemark.Text = "Add Comment";
            this.lnkAddRemark.Click += new System.EventHandler(this.lnkAddRemark_Click);
            this.lnkAddRemark.MouseLeave += new System.EventHandler(this.lnkAddRemark_MouseLeave);
            this.lnkAddRemark.MouseHover += new System.EventHandler(this.lnkAddRemark_MouseHover);
            // 
            // remarks
            // 
            this.remarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.remarks.Location = new System.Drawing.Point(3, 16);
            this.remarks.Name = "remarks";
            this.remarks.Size = new System.Drawing.Size(395, 93);
            this.remarks.TabIndex = 11;
            this.remarks.Text = "";
            this.remarks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.remarks_MouseUp);
            // 
            // txtAudit
            // 
            this.txtAudit.Controls.Add(this.controlContainer1);
            this.txtAudit.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.txtAudit.FloatVertical = true;
            this.txtAudit.ID = new System.Guid("8748f4bd-7900-4d69-8000-f07473c35dde");
            this.txtAudit.Location = new System.Drawing.Point(410, 0);
            this.txtAudit.Name = "txtAudit";
            this.txtAudit.Options.ShowCloseButton = false;
            this.txtAudit.OriginalSize = new System.Drawing.Size(413, 138);
            this.txtAudit.Size = new System.Drawing.Size(413, 139);
            this.txtAudit.Text = "Change Log";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.updateLog);
            this.controlContainer1.Location = new System.Drawing.Point(4, 23);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(405, 112);
            this.controlContainer1.TabIndex = 0;
            // 
            // updateLog
            // 
            this.updateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateLog.Location = new System.Drawing.Point(3, 3);
            this.updateLog.Name = "updateLog";
            this.updateLog.Size = new System.Drawing.Size(399, 106);
            this.updateLog.TabIndex = 11;
            // 
            // PanelRightPane
            // 
            this.PanelRightPane.AutoScroll = true;
            this.PanelRightPane.Controls.Add(this.panelMeetingSummary_Container);
            this.PanelRightPane.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.PanelRightPane.ID = new System.Guid("7639899b-d3a6-4206-823f-28d50242d23e");
            this.PanelRightPane.Location = new System.Drawing.Point(823, 145);
            this.PanelRightPane.Name = "PanelRightPane";
            this.PanelRightPane.Options.AllowFloating = false;
            this.PanelRightPane.Options.ShowCloseButton = false;
            this.PanelRightPane.OriginalSize = new System.Drawing.Size(204, 200);
            this.PanelRightPane.Size = new System.Drawing.Size(204, 499);
            this.PanelRightPane.Text = "Meeting Summary";
            // 
            // panelMeetingSummary_Container
            // 
            this.panelMeetingSummary_Container.Location = new System.Drawing.Point(4, 23);
            this.panelMeetingSummary_Container.Name = "panelMeetingSummary_Container";
            this.panelMeetingSummary_Container.Size = new System.Drawing.Size(196, 472);
            this.panelMeetingSummary_Container.TabIndex = 0;
            // 
            // document3
            // 
            this.document3.Caption = "Meeting Summary";
            this.document3.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document3.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            // 
            // document4
            // 
            this.document4.Caption = "Tasks Grid";
            this.document4.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document4.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            // 
            // documentManager1
            // 
            this.documentManager1.BarAndDockingController = this.barAndDockingController1;
            this.documentManager1.MdiParent = this;
            this.documentManager1.MenuManager = this.ribbonControl;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // document1
            // 
            this.document1.Caption = "Meeting Summary";
            this.document1.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "&Save";
            this.barButtonItem1.Description = "Saves the active document.";
            this.barButtonItem1.Hint = "Saves the active document";
            this.barButtonItem1.Id = 16;
            this.barButtonItem1.ImageIndex = 4;
            this.barButtonItem1.LargeImageIndex = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "User: ";
            this.barEditItem1.Edit = this.repositoryCboUsers;
            this.barEditItem1.Id = 67;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "Connect.png");
            this.imageList1.Images.SetKeyName(1, "Disconnect.png");
            // 
            // timerConnectionStatus
            // 
            this.timerConnectionStatus.Enabled = true;
            this.timerConnectionStatus.Interval = 30000;
            this.timerConnectionStatus.Tick += new System.EventHandler(this.timerConnectionStatus_Tick);
            // 
            // btnPriorityUp
            // 
            this.btnPriorityUp.Caption = "Priority Up";
            this.btnPriorityUp.Description = "Higher Priority";
            this.btnPriorityUp.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPriorityUp.Glyph")));
            this.btnPriorityUp.Hint = "Higher Priority";
            this.btnPriorityUp.Id = 2;
            this.btnPriorityUp.LargeImageIndex = 1;
            this.btnPriorityUp.Name = "btnPriorityUp";
            this.btnPriorityUp.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnPriorityUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPriorityUp1_ItemClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.HideSelection = false;
            this.richTextBox1.Location = new System.Drawing.Point(1027, 145);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(81, 499);
            this.richTextBox1.TabIndex = 21;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.HideSelection = false;
            this.richTextBox2.Location = new System.Drawing.Point(1027, 145);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(81, 499);
            this.richTextBox2.TabIndex = 22;
            this.richTextBox2.Text = "";
            // 
            // pmMain
            // 
            this.pmMain.Name = "pmMain";
            this.pmMain.Ribbon = this.ribbonControl;
            // 
            // alertReminder
            // 
            this.alertReminder.FormLocation = DevExpress.XtraBars.Alerter.AlertFormLocation.TopRight;
            this.alertReminder.AlertClick += new DevExpress.XtraBars.Alerter.AlertClickEventHandler(this.alertControl1_AlertClick);
            this.alertReminder.ButtonClick += new DevExpress.XtraBars.Alerter.AlertButtonClickEventHandler(this.alertControl1_AlertClick);
            // 
            // btnConnectionStatus
            // 
            this.btnConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnectionStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectionStatus.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btnConnectionStatus.Appearance.Options.UseFont = true;
            this.btnConnectionStatus.Appearance.Options.UseForeColor = true;
            this.btnConnectionStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnConnectionStatus.Image")));
            this.btnConnectionStatus.Location = new System.Drawing.Point(986, 54);
            this.btnConnectionStatus.Name = "btnConnectionStatus";
            this.btnConnectionStatus.Size = new System.Drawing.Size(118, 33);
            this.btnConnectionStatus.TabIndex = 27;
            this.btnConnectionStatus.Text = "Connected";
            this.btnConnectionStatus.Click += new System.EventHandler(this.btnConnectionStatus_Click);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Send";
            this.barButtonItem3.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.Glyph")));
            this.barButtonItem3.Hint = "Send Meeting by Mail";
            this.barButtonItem3.Id = 90;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Poke !";
            this.barButtonItem4.Enabled = false;
            this.barButtonItem4.Id = 131;
            this.barButtonItem4.LargeGlyph = global::Prioritizer.Properties.Resources.poke;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // alertPoke
            // 
            this.alertPoke.AllowHtmlText = true;
            this.alertPoke.AppearanceText.Options.UseTextOptions = true;
            this.alertPoke.AppearanceText.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.alertPoke.AutoFormDelay = 10000;
            this.alertPoke.AutoHeight = true;
            alertButton1.Hint = "Reply To Sender";
            alertButton1.Image = global::Prioritizer.Properties.Resources.Reply16;
            alertButton1.Name = "btnReply";
            alertButton2.Hint = "Dismiss";
            alertButton2.Image = global::Prioritizer.Properties.Resources.cancel16;
            alertButton2.Name = "btnDismiss";
            this.alertPoke.Buttons.Add(alertButton1);
            this.alertPoke.Buttons.Add(alertButton2);
            this.alertPoke.FormShowingEffect = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.SlideHorizontal;
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 671);
            this.Controls.Add(this.btnConnectionStatus);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.PanelRightPane);
            this.Controls.Add(this.PanelLeftPane);
            this.Controls.Add(this.ribbonControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(1124, 710);
            this.Name = "frmMain";
            this.Text = "Prioritizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryCboMeetings1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryCboMeetingCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonImageCollectionLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryCboUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryCboMeetingCategory1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tasksGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.PanelLeftPane.ResumeLayout(false);
            this.PanelTaskList.ResumeLayout(false);
            this.PanelTaskList_Container.ResumeLayout(false);
            this.PanelBottomPane.ResumeLayout(false);
            this.PanelRemarks.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            this.txtAudit.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updateLog.Properties)).EndInit();
            this.PanelRightPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.document3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmMain)).EndInit();
            this.ResumeLayout(false);

        }

      
        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem iClose;
        private DevExpress.XtraBars.BarButtonItem btnPriorityDown;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem iSaveAs;
        private DevExpress.XtraBars.BarButtonItem iExit;
        private DevExpress.XtraBars.BarButtonItem iAbout;
        private DevExpress.XtraBars.BarStaticItem siStatus;
        private DevExpress.XtraBars.BarButtonGroup alignButtonGroup;
        private DevExpress.XtraBars.BarButtonItem iBoldFontStyle;
        private DevExpress.XtraBars.BarButtonItem iItalicFontStyle;
        private DevExpress.XtraBars.BarButtonItem iUnderlinedFontStyle;
        private DevExpress.XtraBars.BarButtonGroup fontStyleButtonGroup;
        private DevExpress.XtraBars.BarButtonItem iLeftTextAlign;
        private DevExpress.XtraBars.BarButtonItem iCenterTextAlign;
        private DevExpress.XtraBars.BarButtonItem iRightTextAlign;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
        private DevExpress.XtraBars.Ribbon.RibbonPage homeRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup skinsRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPage helpRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup helpRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu appMenu;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.Utils.ImageCollection ribbonImageCollection;
        private DevExpress.Utils.ImageCollection ribbonImageCollectionLarge;
        private DevExpress.XtraGrid.GridControl tasksGrid;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.ControlContainer panelMeetingSummary_Container;
        private DevExpress.XtraBars.Docking.DockPanel PanelTaskList;
        private DevExpress.XtraBars.Docking.ControlContainer PanelTaskList_Container;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonUserTasks;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document3;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document4;
        public DevExpress.XtraBars.Docking.DockPanel PanelRightPane;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryCboUsers;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarEditItem cboMeetings;
        //private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryCboMeetings;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        public DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraBars.BarCheckItem chkFinished;
        private DevExpress.XtraBars.BarCheckItem chkCancelled;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryCboMeetingCategory1;
        private DevExpress.XtraBars.BarEditItem cboMeetingCategory;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryCboMeetingCategory;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryCboMeetings;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryCboMeetings1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraBars.BarButtonItem btnAttendees;
        private DevExpress.XtraBars.BarButtonItem btnAddMeeting;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraBars.BarStaticItem lblAttendees;
        private DevExpress.XtraBars.BarStaticItem lblMeetingDate;
        private DevExpress.XtraBars.BarStaticItem lblMeetingOwner;
        private DevExpress.XtraBars.Docking.DockPanel PanelRemarks
            ;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel txtAudit;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraEditors.MemoEdit updateLog;
        private DevExpress.XtraBars.Docking.DockPanel PanelLeftPane;
        private DevExpress.XtraBars.Docking.DockPanel PanelBottomPane;
        private DevExpress.XtraBars.BarButtonItem btnSend;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.BarButtonItem btnRefreshMeetings;
        private DevExpress.XtraBars.BarCheckItem chkFinishedMeetings;
        private DevExpress.XtraBars.BarButtonItem btnRefreshTaskList;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraBars.BarButtonItem btnForwardMeeting;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup7;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timerConnectionStatus;
        private DevExpress.XtraBars.BarButtonItem btnLinkToMeeting;
        private DevExpress.XtraBars.BarButtonItem btnLinkToMeeting2;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private DevExpress.XtraBars.BarButtonItem btnPriorityUp1;
        private DevExpress.XtraBars.BarButtonItem btnPriorityUp;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox remarks;
        private DevExpress.XtraBars.Ribbon.RibbonMiniToolbar ribbonMiniToolbar1;
        private DevExpress.XtraBars.PopupMenu pmMain;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraBars.Alerter.AlertControl alertReminder;
        private DevExpress.XtraBars.Ribbon.RibbonPage AdminPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup UsersPage;
        private DevExpress.XtraBars.BarButtonItem btnManagerEmployeeHierarchy;
        private DevExpress.XtraBars.BarButtonItem btnManageUsers;
        private DevExpress.XtraBars.BarButtonItem btnMoveTo;
        private DevExpress.XtraBars.BarButtonItem btnCopyTo;
        private DevExpress.XtraBars.BarButtonItem btnCloneTask;
        private DevExpress.XtraBars.BarButtonItem btnEditAttachments;
        private DevExpress.XtraBars.BarSubItem mnuProjects;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup attachmentsPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup projectsRibbon;
        private DevExpress.XtraBars.BarButtonItem mnuAddProject;
        private DevExpress.XtraBars.BarButtonItem mnuEditProjectList;
        private DevExpress.XtraBars.BarButtonItem btnMeetingsList;
        private DevExpress.XtraBars.BarSubItem mnuMeetingSettings;
        private DevExpress.XtraBars.BarButtonItem btnAddMeetingCategory;
        private DevExpress.XtraBars.BarSubItem mnuMeetingCategories;
        private DevExpress.XtraBars.BarButtonItem btnEditCategories;
        private DevExpress.XtraBars.BarButtonItem btnUserManagement;
        private DevExpress.XtraBars.BarButtonItem btnChangePassword;
        private DevExpress.XtraBars.Ribbon.RibbonPage SettingsPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup8;
        private DevExpress.XtraBars.BarSubItem mnuAttachments;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraEditors.SimpleButton btnConnectionStatus;
        //private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        //private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        //private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
        private DevExpress.XtraBars.BarEditItem cboUsersSelector;
        //private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryUsers;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryUsers;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup9;
        private DevExpress.XtraBars.BarButtonItem btnGoToWeb;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem btnPoke;
        private DevExpress.XtraEditors.LabelControl lnkAddRemark;
        private DevExpress.XtraBars.BarCheckItem chkViewAllMembers;
        private DevExpress.XtraBars.BarButtonItem btnPokeFromMeeting;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.Alerter.AlertControl alertPoke;
        private DevExpress.XtraBars.BarButtonItem btnMessage;
        private DevExpress.XtraBars.BarButtonItem btnResetGridLayout;
        private DevExpress.XtraBars.BarButtonItem btnResetDockingLayout;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup10;

    }
}
