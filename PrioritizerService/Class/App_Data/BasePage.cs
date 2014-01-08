using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Specialized;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;

namespace PrioritizerService.Web
{
    [Serializable]
    public class UserState
    {
        #region Properties

        /// <summary>
        /// ID of User
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// UserName of user
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Full name of user
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// The type of user
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// The time zone id of the user.
        /// </summary>
        public string TimeZoneID { get; set; }
        /// <summary>
        /// Email address of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The time when the user started using the application
        /// </summary>
        public DateTime SessionStartTime { get; private set; }
        /// <summary>
        /// The calculation of the user parameters.
        /// needed for chcking if the user state object was changed.
        /// </summary>
        public long CheckedSum { get; private set; }

        public bool IsTempPassword { get; set; }

        public bool IsOldPassword { get; set; }

        /// <summary>
        /// ID of account to which the user should be treated in the application as if he belongs.
        /// </summary>
        public int AccountID_Current { get; set; }

        /// <summary>
        /// Name of account to which the user should be treated in the application as if he belongs.
        /// </summary>
        public string AccountName_Current { get; set; }

        /// <summary>
        /// ID of account to which the user is realy belong.
        /// </summary>
        public int AccountID_Original { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the time and checksum properties
        /// </summary>
        public void UpdateUserState()
        {
            SessionStartTime = DateTime.Now;
            CheckedSum = GetCheckSum();
        }

        /// <summary>
        ///TODO: Returns an algorithm result of the usersate properties
        /// </summary>
        /// <returns></returns>
        public long GetCheckSum()
        {
            long CS = 0;
            long pp, i;

            CS = CS ^ ((ID % 257) * 10376339);
            CS = CS ^ SessionStartTime.Ticks;
            CS = CS ^ ((UserType % 257) * 14470447);
            pp = 139;
            for (i = 0; i < FullName.Length; i++)
            {
                CS = CS ^ (pp * FullName.ToCharArray()[i]);
                pp *= 139;
                pp = pp & 0x00ffffff;
            }
            pp = 79;

            for (i = 0; i < Email.Length; i++)
            {
                CS = CS ^ (pp * Email.ToCharArray()[i]);
                pp *= 79;
                pp = pp & 0x00ffffff;
            }

            return CS;
        }

        #endregion
    }
    public enum PageMode
    {
        New,
        Edit,
        Delete,
        OldPass,
        TempPass,
        SelfEdit,
        CopyCode,
        SavedReport,
        SaveAReport,
        CreateReport,
        ReportsList,
        TasksList,
        CampaignDashboard,
        Nothing
    }
    public abstract class BasePage : ParentBasePage
    {
        #region Private Members

        public string VisibileOnNonPrintOnly;
        public string VisibileOnPrintOnly;

        /// <summary>
        /// The name of cookie where we store the user state parameters
        /// </summary>
        protected string UserStateCookieName
        {
            get
            {
                return ConfigurationManager.AppSettings["UserStateCookieName"].ToString();
            }
        }
        protected string ReferrerCookieName
        {
            get
            {
                return ConfigurationManager.AppSettings["ReferrerCookieName"].ToString();
            }
        }
        private BaseHandler FormHandler = new BaseHandler();

        #endregion

        #region Private Methods
        #region Login

        /// <summary>
        /// Reveres the string of cookie to UserSate object
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private UserState GetStateFromCookieValueOrRequest(string cookieValue)
        {
            string serUserState;

            if (QSUserStateDirect != null && QSUserStateDirect != "")
            {
                //serUserState = Encryption.Decrypt(QSUserStateDirect);
                serUserState = QSUserStateDirect;
            }
            else if (cookieValue != null && cookieValue != "")
            {
                //serUserState = Encryption.Decrypt(cookieValue);
                serUserState = cookieValue;
            }
            else
            {
                return null;
            }

            return Serialization.BinDesiralize<UserState>(serUserState);
        }
        /// <summary>
        /// Returns the UserStae object as string
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        private string GetValue4Cookie(UserState us)
        {
            string str;
            str = Serialization.BinSerialise(us);
            //str = Encryption.Encrypt(str);
            return str;
        }

        #endregion
        #endregion

        #region Inheritted Properties

        /// <summary>
        /// The current user state details
        /// </summary>
        protected string prevPageState
        {
            get { return Cookies.GetCookie(Request, Response, "prevPageState"); }
            set { Cookies.SetCookie(Response, "prevPageState", value, null); }
        }

        /// <summary>
        /// The current user state details
        /// </summary>
        public UserState userState
        {
            get { return GetStateFromCookieValueOrRequest(Cookies.GetCookie(Request, Response, UserStateCookieName)); }
            set { Cookies.SetCookie(Response, UserStateCookieName, GetValue4Cookie(value), null); }
        }

        /// <summary>
        /// The referrer page saved in cookie
        /// </summary>
        protected string referrerCookie
        {
            get { return Cookies.GetCookie(Request, Response, ReferrerCookieName); }
            set { Cookies.SetCookie(Response, ReferrerCookieName, value,null); }
        }

        protected bool QSPrintVersion
        {
            get
            {
                if (QueryString == null || QueryString == "")
                    return false;

                string mode = GetQueryStringValue(QueryString, QueryStringKeys.PrintVersion);
                if (mode != null && mode.CompareTo("Yes") == 0)
                    return true;
                return false;
            }
        }

        protected PageMode PageMode
        {
            get
            {
                if (QueryString == null || QueryString == "")
                    return PageMode.Nothing;

                string mode = GetQueryStringValue(QueryString, QueryStringKeys.mode);
                if (mode == null || mode == "")
                    return PageMode.Nothing;
                return (PageMode)Enum.Parse(typeof(PageMode), mode);
            }
        }

        //protected ReportSubjectType QSReportSubjectType
        //{
        //    get
        //    {
        //        string result = GetQueryStringValue(QueryString, QueryStringKeys.ReportSubjectType);
        //        if (result == null || result == "")
        //            return ReportSubjectType.Main;
        //        return (ReportSubjectType)Enum.Parse(typeof(ReportSubjectType), GetQueryStringValue(QueryString,
        //             QueryStringKeys.ReportSubjectType));
        //    }
        //}
        //protected StatisticsLevelType QSLevelType
        //{
        //    get
        //    {
        //        string result = GetQueryStringValue(QueryString, QueryStringKeys.LevelType);
        //        if (result == null || result == "")
        //            return StatisticsLevelType.Folder;
        //        return (StatisticsLevelType)Enum.Parse(typeof(StatisticsLevelType), GetQueryStringValue(QueryString,
        //             QueryStringKeys.LevelType));
        //    }
        //}
        protected int QSRootObjectID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.RootObjectID);
                if (result == null || result == "")
                    return 0;
                return int.Parse(result);
            }
        }
        protected DateTime? QSStartDate
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.StartDate);
                if (result == null || result == "")
                    return null;
                DateTime date;
                if(!DateTime.TryParse(result,out date))
                    return null;
                return date;
            }
        }
        protected DateTime? QSEndDate
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.EndDate);
                if (result == null || result == "")
                    return null;
                DateTime date;
                if (!DateTime.TryParse(result, out date))
                    return null;
                return date;
            }
        }
        //protected ActivityStatus QSActivityStatus
        //{
        //    get
        //    {
        //        string result = GetQueryStringValue(QueryString, QueryStringKeys.ActivityStatus);
        //        if(result == null || result == "")
        //            return GetDefaultActivityStatus();
        //        return (ActivityStatus)Enum.Parse(typeof(ActivityStatus), result);
        //    }
        //}
        //protected PopChartSubject QSSubject
        //{
        //    get
        //    {
        //        string result = GetQueryStringValue(QueryString, QueryStringKeys.Subject);
        //        if (result == null || result == "")
        //            return PopChartSubject.DailyScans;
        //        return (PopChartSubject)Enum.Parse(typeof(PopChartSubject), result);
        //    }
        //}
        protected string QSBaseUrl
        {
            get
            {
                return GetQueryStringValue(QueryString, QueryStringKeys.BaseUrl);
            }
        }
        protected string QSUserStateDirect
        {
            get
            {
                return GetQueryStringValue(QueryString, QueryStringKeys.UserStateDirect);
            }
        }
        //protected PopChartTypes QSChartType
        //{
        //    get
        //    {
        //        string result = GetQueryStringValue(QueryString, QueryStringKeys.ChartType);
        //        if(result == null || result == "")
        //            return PopChartTypes.Pie;
        //        return (PopChartTypes)Enum.Parse(typeof(PopChartTypes), result);
        //    }
        //}
        //protected PeriodMode QSPeriodMode
        //{
        //    get
        //    {
        //        string result = GetQueryStringValue(QueryString, QueryStringKeys.PeriodMode);
        //        if (result == null || result == "")
        //            return GetDefaultPeriodMode();
        //        return (PeriodMode)Enum.Parse(typeof(PeriodMode), result);
        //    }
        //}
        //protected PredefinedPeriod QSPredefinedPeriod
        //{
        //    get
        //    {
        //        string result = GetQueryStringValue(QueryString, QueryStringKeys.PredefinedPeriod);
        //        if (result == null || result == "")
        //            return GetDefaultPredefinedPeriod();
        //        return (PredefinedPeriod)Enum.Parse(typeof(PredefinedPeriod), result);
        //    }
        //}
        protected int? QSWidth
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.Width);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int? QSHeight
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.Height);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int? QSBarHeight
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.BarHeight);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int QSExtraButton
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.ExtraButton);
                if (result == null || result == "")
                    return 0;
                int id;
                if (!int.TryParse(result, out id))
                    return 0;
                return id;
            }
        }

        protected string QSSegmentTitle
        {
            get
            {
                return GetQueryStringValue(QueryString, QueryStringKeys.SegmentTitle);
            }
        }

        protected int QSRootSegmentID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.RootSegmentID);
                if (result == null || result == "")
                    return 0;
                int id;
                if (!int.TryParse(result, out id))
                    return 0;
                return id;
            }
        }

        protected int QSSegmentGeneration
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.SegmentGeneration);
                if (result == null || result == "")
                    return 0;
                int id;
                if (!int.TryParse(result, out id))
                    return 0;
                return id;
            }
        }

        protected string QSVState
        {
            get
            {
                return GetQueryStringValue(QueryString, QueryStringKeys.VState);
            }
        }
        protected int? QSYear
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.Year);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        /// <summary>
        /// The time zone id to use for the user
        /// </summary>
        protected string userTimeZone
        {
            get
            {
                string timeZoneID = userState.TimeZoneID; ;
                if (timeZoneID == null || timeZoneID == "")
                    timeZoneID = TimeZoneInfo.Local.Id;
                return timeZoneID;
            }
        }
        protected int? QSFolderID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.LOBID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int? QSCampaignID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.campaignID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int? QSCodeID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.codeID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected string QSPrevPage
        {
            get
            {
                return GetQueryStringValue(QueryString, QueryStringKeys.PrevPage);
            }
        }
        protected int? QSZeroRatedID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.ZeroRatedID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int? QSReportID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.ReportID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        //protected DataMode QSDataMode
        //{
        //    get
        //    {
        //        string result = GetQueryStringValue(QueryString, QueryStringKeys.DataMode);
        //        if (result == null || result == "")
        //            return DataMode.Online;
        //        return (DataMode)Enum.Parse(typeof(DataMode), GetQueryStringValue(QueryString,
        //             QueryStringKeys.DataMode));
        //    }
        //}
        protected int? QSDeviceID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.deviceID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int? QSSoftwareLineID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.SoftwareLineID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int? QSApplicationID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.ApplicationID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }
        protected int? QSAdvertisementID
        {
            get
            {
                string result = GetQueryStringValue(QueryString, QueryStringKeys.AdvertisementID);
                if (result == null || result == "")
                    return null;
                int id;
                if (!int.TryParse(result, out id))
                    return null;
                return id;
            }
        }

        /// <summary>
        /// Keeps in Session the current filter to use for statistics pages
        /// </summary>
        //protected IStatisticsFilter qsFilter
        //{
        //    get{ return Session["StatisticsFilter"] as IStatisticsFilter;}
        //    set{ Session["StatisticsFilter"] = value;}
        //}

        #endregion

        #region Inheritted Methods
        
        #region Login

        /// <summary>
        /// Forces to invoke the Secure page method
        /// </summary>
        protected abstract void InitializePage();

        /// <summary>
        /// The methods which should be invoked in every page load of a secured page
        /// </summary>
        protected void SecurePage()
        {
            // Forcing SSL Work
            //FormHandler.ForceSSL(Request, Response);

            //// Chack Browser compatibility
            //if (!FormHandler.CheckBrowserType(Request))
            //{
            //    string errorMessage = ConfigurationManager.AppSettings["SupportedBrowserMsg"];
            //    Redirect2NotAllowedPage(errorMessage);
            //}

            UserState us = userState;
            VerifyLogin(us);
            UpdateCookieUserState();
            SetUserDisplayName(us);
            SetImpersonatedAccountName(us);
            HideTabs();
            HideLinks(us);
            SetPrintVersionVars();
        }

        /// <summary>
        /// Set variable print when the page mode is printable
        /// </summary>
        protected void SetPrintVersionVars()
        {
            if (QSPrintVersion)
            {
                
                VisibileOnNonPrintOnly = "none";
                VisibileOnPrintOnly = "block";
            }
            else
            {
                VisibileOnNonPrintOnly = "block";
                VisibileOnPrintOnly = "none";
            }
            if (this.Page.Master != null)
            {
                ((SiteStruct)this.Page.Master).VisibileOnNonPrintOnly = VisibileOnNonPrintOnly;
                ((SiteStruct)this.Page.Master).VisibileOnPrintOnly = VisibileOnPrintOnly;
            }
        }

        /// <summary>
        /// Chaek that user cookies exists
        /// Check that the date is not earlier than 20 m'
        /// Compare the check sum to the cookie check sum
        /// </summary>
        protected void VerifyLogin(UserState us)
        {
            if ((us == null) ||
               (us.SessionStartTime.AddMinutes(new BaseHandler().GetCookieTimeOut()) < DateTime.Now) ||
               (us.CheckedSum != us.GetCheckSum()))

                Response.Redirect(ConfigurationManager.AppSettings["LoginPage"].ToString());
            
            //Check if the user should be redirected to change password
            string targetFile = HttpContext.Current.Request.ServerVariables["URL"].ToString();
            //if (!targetFile.Contains(ConfigurationManager.AppSettings["ChangePassPage"].ToString()))
            //{
            //    if (us.IsTempPassword)
            //        Redirect2ChangePassword(PageMode.TempPass);
            //    if (us.IsOldPassword)
            //        Redirect2ChangePassword(PageMode.OldPass);
            //}

            // If o.k,updates few of the UserSate object properties
            UpdateCookieUserState();
        }

        /// <summary>
        /// Updates few of the Cookie UserSate object properties.
        /// sould by invocet in evry submit of the page
        /// </summary>
        protected void UpdateCookieUserState()
        {
            UserState u = userState;
            
            if (u == null)
                return;

            u.UpdateUserState();
            userState = u;
        }


        /// <summary>
        /// Updates few of the Cookie prevPage value
        /// sould by invocet in evry submit of the page
        /// </summary>
        protected void UpdatePrevPage()
        {
            if (Request.UrlReferrer != null)
                prevPageState = Request.UrlReferrer.ToString();
        }

        
        /// <summary>
        /// Updates in cookie of user state the properties which indicates if user needed to change password
        /// </summary>
        /// <param name="changePass"></param>
        protected void UpdateCookieUserStateChangePass(bool changePass)
        {
            UserState u = userState;

            if (u == null)
                return;

            u.IsOldPassword = changePass;
            u.IsTempPassword = changePass;

            userState = u;
        }

        /// <summary>
        /// Show the name of the user at the page header
        /// </summary>
        /// <param name="us"></param>
        protected void SetUserDisplayName(UserState us)
        {
            try
            {
                if (us == null)
                    return;

                if (Master == null)
                    return;

                ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("cphSiteHeader");
                if (cph == null)
                    return;

                Control uc = cph.FindControl("ucSiteHeader");
                if (uc == null)
                    return;

                Control c = uc.FindControl("lblLogedUser");
                if (c == null)
                    return;

                ((Label)c).Text = "User: " + us.UserName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Show the name of the account the user is logged to at the page header
        /// </summary>
        /// <param name="us"></param>
        protected void SetImpersonatedAccountName(UserState us)
        {
            try
            {
                if (us == null)
                    return;

                if (Master == null)
                    return;

                ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("cphSiteHeader");
                if (cph == null)
                    return;

                Control uc = cph.FindControl("ucSiteHeader");
                if (uc == null)
                    return;

                Control c = uc.FindControl("lblImpersonatedAccount");
                if (c == null)
                    return;

                ((Label)c).Text = "Account: " + us.AccountName_Current;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hide links from header or footer with user type check.
        /// </summary>
        private void HideLinks(UserState us)
        {
            try
            {
                if (us == null)
                    return;

                if (Master == null)
                    return;

                ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("cphSiteHeader");
                if (cph == null)
                    return;

                Control uc = cph.FindControl("ucSiteHeader");
                if (uc == null)
                    return;

                //if (FormHandler.CanViewPage(us.ID, Privileges.Page_Operations))
                //{
                //    Control c = uc.FindControl("lnkOperations");
                //    if (c == null)
                //        return;

                //    Control spacer = uc.FindControl("lblOperationSpacer");
                //    if (spacer == null)
                //        return;

                //    ((LinkButton)c).Visible = true;
                //    ((Label)spacer).Visible = true;
                //}
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void HideTabs()
        {
            //if (userState == null)
            //    return;

            //List<HeaderTabs> tabs = new List<HeaderTabs>();
            //if(UserTypesToHideTabs().Contains((UserTypes)userState.UserType))
            //{
            //    tabs.Add(HeaderTabs.tabCampaigns);
            //    tabs.Add(HeaderTabs.tabReports);
            //    if (Page.Master != null)
            //        ((SiteStruct)Page.Master).HideTabs(tabs.ToArray());
            //}
        }

        //private List<UserTypes> UserTypesToHideTabs()
        //{
        //    List<UserTypes> list = new List<UserTypes>();
        //    list.Add(UserTypes.Operations);
        //    list.Add(UserTypes.Customization);
        //    return list;
        //}

        protected void Redirect2HomePage()
        {
            if (userState == null)
                return;

            Response.Redirect(GetHomePage());
        }

        protected string GetHomePage()
        {
            //if ((UserTypes)userState.UserType == UserTypes.Operations || (UserTypes)userState.UserType == UserTypes.Customization)
            //    return ConfigurationManager.AppSettings["OperPage"].ToString();
            //else
            //    return ConfigurationManager.AppSettings["HomePage"].ToString();
            return @"http://google.com/";
        }
        
        #endregion

        #region General

        /// <summary>
        /// this function take any page if is not change is password to change passwod page
        /// </summary>
        /// <param name="mode"></param>
        //protected void Redirect2ChangePassword(PageMode mode)
        //{
        //    Response.Redirect(ConfigurationManager.AppSettings["ChangePassPage"].ToString() +
        //            "?" + Server.UrlEncode(Encryption.Encrypt("mode=" + mode)));
        //}

        /// <summary>
        /// Every page should invoke this methods to set the tab to be highlighted.
        /// </summary>
        /// <param name="selectedTab">The tab to be highlighted</param>
        protected abstract void SetDisplayTab();

        /// <summary>
        /// Shows message in a lable text
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="message"></param>
        protected void ShowMessage(Label lbl, string message)
        {
            lbl.Text = message;
        }

        #endregion

        //#region Statistics

        ///// <summary>
        ///// Returns the default filter to give if no other filter exist
        ///// </summary>
        ///// <returns></returns>
        //protected IStatisticsFilter GetDefaultFilter()
        //{
        //    return new StatisticsFilter(GetDefaultPeriodMode(), GetDefaultPredefinedPeriod(), null, null, GetDefaultActivityStatus());
        //}

        //private PeriodMode GetDefaultPeriodMode()
        //{
        //    return PeriodMode.Predefined;
        //}

        //private PredefinedPeriod GetDefaultPredefinedPeriod()
        //{
        //    return PredefinedPeriod.LastMonth;
        //}

        //private ActivityStatus GetDefaultActivityStatus()
        //{
        //    return ActivityStatus.All;
        //}

        //#endregion

        //#region Devices

        ///// <summary>
        ///// 
        ///// </summary>
        //protected void setDeviceWizardPrevPage(int nextStep)
        //{
        //    if (Request.UrlReferrer != null)
        //    {
        //        switch (nextStep)
        //        {
        //            case 2:
        //                Cookies.SetCookie(Response, "deviceWizardStep2", Request.UrlReferrer.ToString(), null);
        //                break;
        //            case 3:
        //                Cookies.SetCookie(Response, "deviceWizardStep3", Request.UrlReferrer.ToString(), null);
        //                break;
        //        }
        //        //get {  }
        //    }
        //}

        //protected string getDeviceWizardPrevPage(int currentStep)
        //{
        //    string prevPage = "";
        //    switch (currentStep)
        //    {
        //        case 2:
        //            prevPage = Cookies.GetCookie(Request, Response, "deviceWizardStep2");
        //            Cookies.SetCookie(Response, "deviceWizardStep2", "", null);
        //            return prevPage;
        //            break;
        //        case 3:
        //            prevPage = Cookies.GetCookie(Request, Response, "deviceWizardStep3");
        //            Cookies.SetCookie(Response, "deviceWizardStep3", "", null);
        //            return prevPage;
        //            break;
        //    }
        //    return "";
        //    //get { return Cookies.GetCookie(Request, Response, "prevPageState"); }        
        //}

        //#endregion

        #endregion

        #region save/restore page state methods
        
           /// <summary>
            /// The persisted PageState.
            /// This is non-null if a postback is being emulated from the persisted PageState.
            /// </summary>
            private PageState pageState = null;

            /// <summary>
            /// Contains the URL to redirect to
            /// </summary>
            private string redirectSavingPageStateURL = null;

            /// <summary>
            /// Constructor
            /// </summary>
            public BasePage()
            {
                // The following statement must be uncommented if running under .NET 2.0 to avoid an
                // "Invalid postback or callback argument" exception when restoring the page state.
                EnableEventValidation=false;
            }

            /// <summary>
            /// Indicates whether the current page is being or has been restored from a persisted state
            /// </summary>
            public bool IsRestoredPageState
            {
                get
                {
                    return pageState != null;
                }
            }

            /// <summary>
            /// Returns the post data from the persisted PageState if it exists, otherwise the actual post data from Request.Form
            /// </summary>
            public NameValueCollection PostData
            {
                get
                {
                    return IsRestoredPageState ? pageState.PostData : Request.Form;
                }
            }

            /// <summary>
            /// Returns the data passed back from the sub-page which can be used to make changes to the saved page state.
            /// This data is passed back via the query string.
            /// </summary>
            public NameValueCollection PassBackData
            {
                get
                {
                    return IsRestoredPageState ? pageState.PassBackData : null;
                }
            }

            /// <summary>
            /// Call this method instead of Response.Redirect(url) to cause this page to restore its current state when it is next displayed.
            /// </summary>
            public void RedirectSavingPageState(string url)
            {
                redirectSavingPageStateURL = url;
            }

            /// <summary>
            /// Call this method to redirect to the specified relative URL,
            /// specifying whether to restore the page's saved state
            /// (assuming its state was saved when it was last shown).
            /// The specified URL is relative to that of the current request.
            /// This method is usually called from a "sub-page", which doesn't extend this class. 
            /// </summary>
            public static void RedirectToSavedPage(string url, bool restorePageState)
            {
                if (!restorePageState) RemoveSavedPageState(url);
                HttpContext.Current.Response.Redirect(url);
            }

            /// <summary>
            /// Call this method to clear the saved PageState of the page with the specified relative URL.
            /// This ensures that the next redirect to the specified page will not revert to the saved state.
            /// The specified URL is relative to that of the current request.
            /// </summary>
            public static void RemoveSavedPageState(string url)
            {
                RemoveSavedPageState(new Uri(HttpContext.Current.Request.Url, url));
            }

            /// <summary>
            /// This method is called by the framework after the Init event to determine whether a postback is being performed.
            /// The Page.IsPostBack property returns true iff this method returns a non-null.
            /// </summary>
            /*
            protected override NameValueCollection DeterminePostBackMode()
            {
                pageState = LoadPageState(Request.Url);
                NameValueCollection normalReturnObject = base.DeterminePostBackMode();
                if (!IsRestoredPageState) return normalReturnObject; // default to normal behaviour if there is no persisted pagestate.
                if (normalReturnObject != null)
                {
                    // this is a normal postback, so don't use persisted page state
                    pageState = null;
                    RemoveSavedPageState(Request.Url); // clear the page state from the persistence medium so it is not used again
                    return normalReturnObject;
                }
                // If we get to this point, we want to restore the persisted page state.
                // Save PassBackData if we have not already done so:
                if (pageState.PassBackData == null)
                {
                    pageState.PassBackData = Request.QueryString;
                    // call SavePageState again in case the change we just made is not persisted purely in memory:
                    SavePageState(pageState.Url, pageState);
                }
                // Check whether the current request URL matches the persisted URL:
                if (pageState.Url.AbsoluteUri != Request.Url.AbsoluteUri)
                {
                    // The url, and hence the query string, doesn't match the one in the page state, so reload this page immediately with the persisted URL.
                    Response.Redirect(pageState.Url.AbsoluteUri, true);
                }
                RemoveSavedPageState(Request.Url); // clear the page state from the persistence medium so it is not used again
                // This method must return something other than null, otherwise the framework does not call
                // the LoadPageStateFromPersistenceMedium() method and IsPostBack will return false.
                // Request.Form is an empty object of the correct type to achieve this.
                return Request.Form;
            }
             * */

            /// <summary>
            /// This method is called by the framework after DeterminePostBackMode(), but before custom event handling.
            /// It returns the view state that the framework uses to restore the state of the controls.
            /// </summary>
            protected override object LoadPageStateFromPersistenceMedium()
            {
                if (!IsRestoredPageState) return base.LoadPageStateFromPersistenceMedium(); // default to normal behaviour if we don't want to restore the persisted page state
                return pageState.ViewStateObject; // otherwise return the ViewStateObject contained in the persisted pageState
            }

            /// <summary>
            /// This method is called by the framework after LoadPageStateFromPersistenceMedium() to raise the Load event.
            /// Controls are populated with data from PostData here because it has to happen after
            /// the framework has loaded the view state, which happens after the execution of the
            /// LoadPageStateFromPersistenceMedium() method.
            /// </summary>
            override protected void OnLoad(EventArgs e)
            {
                // The following code is meant to emulate what ASP.NET does "automagically" for us when it
                // populates the controls with post data before processing the events.
                // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconreceivingpostbackdatachangednotifications.asp
                // The difference is that this one populates them with our persisted post data instead of the
                // actual post data from Request.Form.
                if (IsRestoredPageState)
                {
                    // Populate controls with PostData, saving a list of those that were modified:
                    ArrayList modifiedControls = new ArrayList();
                    LoadPostData(this, modifiedControls);
                    // Raise PostDataChanged event on all modified controls:
                    foreach (IPostBackDataHandler control in modifiedControls)
                        control.RaisePostDataChangedEvent();
                }
                base.OnLoad(e);
            }

            /// <summary>
            /// This method performs depth-first recursion on all controls contained in the specified control,
            /// calling the framework's LoadPostData on each and adding those modified to the modifiedControls list.
            /// </summary>
            private void LoadPostData(Control control, ArrayList modifiedControls)
            {
                // Perform recursion of child controls:
                foreach (Control childControl in control.Controls)
                    LoadPostData(childControl, modifiedControls);
                // Load the post data for this control:
                if (control is IPostBackDataHandler)
                {
                    // Get the value of the control's name attribute, which is the GroupName of radio buttons,
                    // or the same as the UniqueID attribute for all other controls:
                    string nameAttribute = (control is RadioButton) ? ((RadioButton)control).GroupName : control.UniqueID;
                    if (control is CheckBoxList)
                    {
                        // CheckBoxLists also require special handling:
                        int i = 0;
                        foreach (ListItem listItem in ((ListControl)control).Items)
                            if (PostData[nameAttribute + ':' + (i++)] != null)
                            {
                                listItem.Selected = true;
                                modifiedControls.Add(control);
                            }
                    }
                    else
                    {
                        // Don't process this control if its key isn't in the PostData, as the
                        // LoadPostData implementation of some controls throws an exception in this case.
                        if (PostData[nameAttribute] == null) return;
                        // Call the framework's LoadPostData on this control using the name attribute as the post data key:
                        if (((IPostBackDataHandler)control).LoadPostData(nameAttribute, PostData))
                            modifiedControls.Add(control);
                    }
                }
            }

            /// <summary>
            /// This method is called by the framework between the PreRender and Render events.
            /// It is only called if this page is to be redisplayed, not if Response.Redirect has been called.
            /// To ensure it is called before we redirect, we must postpone the Response.Redirect call until now.
            /// </summary>
            protected override void SavePageStateToPersistenceMedium(object viewState)
            {
                if (redirectSavingPageStateURL == null)
                {
                    // default to normal behaviour
                    base.SavePageStateToPersistenceMedium(viewState);
                }
                else
                {
                    // persist the current state and redirect to the new page
                    SavePageState(Request.Url, new PageState(viewState, Request.Form, Request.Url));
                    Response.Redirect(redirectSavingPageStateURL);
                }
            }

            /// <summary>
            /// Override this method to load the state from a persistence medium other than the Session object.
            /// </summary>
            protected static PageState LoadPageState(Uri pageURL)
            {
                return (PageState)HttpContext.Current.Session[GetPageStateKey(pageURL)];
            }

            /// <summary>
            /// Override this method to save the state to a persistence medium other than the Session object.
            /// </summary>
            protected static void SavePageState(Uri pageURL, PageState pageState)
            {
                HttpContext.Current.Session[GetPageStateKey(pageURL)] = pageState;
            }

            /// <summary>
            /// Override this method to remove the state from a persistence medium other than the Session object.
            /// </summary>
            protected static void RemoveSavedPageState(Uri pageURL)
            {
                SavePageState(pageURL, null);
            }

            /// <summary>
            /// Returns a key which will uniquely identify a page in a global namespace based on its URL.
            /// </summary>
            private static string GetPageStateKey(Uri pageURL)
            {
                return "_PAGE_STATE_" + pageURL.AbsolutePath;
            }

        #endregion
        }

        /// <summary>
        /// This is the object stored in the persistence medium, containing the view state, post data, and URL.
        /// </summary>
        [Serializable]
        public class PageState
        {
            private SerializableViewState serializableViewState;
            public NameValueCollection PostData;
            public Uri Url;

            // PassBackData is used to store data that is passed back to the parent page from the sub-page:
            public NameValueCollection PassBackData;

            public PageState(object viewStateObject, NameValueCollection postData, Uri url)
            {
                serializableViewState = new SerializableViewState(viewStateObject);
                PostData = postData;
                Url = url;
            }

            public object ViewStateObject
            {
                get
                {
                    return serializableViewState.ViewStateObject;
                }
            }
        }

        /// <summary>
        /// This is a simple wrapper around the view state object to make it serializable.
        /// </summary>
        [Serializable]
        public class SerializableViewState : ISerializable
        {
            public object ViewStateObject;

            private const string ViewStateStringKey = "ViewStateString";

            public SerializableViewState(object viewStateObject)
            {
                ViewStateObject = viewStateObject;
            }

            public SerializableViewState(SerializationInfo info, StreamingContext context)
            {
                ViewStateObject = new LosFormatter().Deserialize(info.GetString(ViewStateStringKey));
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                StringWriter stringWriter = new StringWriter();
                new LosFormatter().Serialize(stringWriter, ViewStateObject);
                info.AddValue(ViewStateStringKey, stringWriter.ToString());
            }
      
    }
}
