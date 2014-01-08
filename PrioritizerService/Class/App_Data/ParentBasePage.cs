using System;
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
using System.Collections.Generic;
using Prioritizer.Shared;

namespace PrioritizerService.Web
{
    public class ParentBasePage : System.Web.UI.Page
    {
        #region Private Members

        private BaseHandler FormHandler = new BaseHandler();

        #endregion

        #region Base query strings

        /// <summary>
        /// Returns the whole querystring after decryption as a long one string
        /// </summary>
        protected string QueryString
        {
            get
            {
                if (Request.QueryString.ToString() == "")
                    return "";

                return Encryption.Decrypt(Server.UrlDecode(Request.QueryString.ToString()));
            }
        }

        /// <summary>
        /// Returns the value of a key from the query string
        /// </summary>
        /// <param name="queryString">The whole query string</param>
        /// <param name="key">The key to get its value</param>
        /// <returns></returns>
        protected string GetQueryStringValue(string queryString, QueryStringKeys key)
        {
            return GetQueryStringValue(queryString, key.ToString());
        }

        protected string GetQueryStringValue(string queryString, string key)
        {
            List<string> sepList = new List<string>();
            sepList.Add("&");
            //sepList.Add("=");
            string[] sep = sepList.ToArray();

            string[] pairs = queryString.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            List<string> sepList2 = new List<string>();
            sepList2.Add("=");
            string[] sep2 = sepList2.ToArray();

            Dictionary<string, string> queryPairs = new Dictionary<string, string>();
            string itemKey, value;
            foreach (string item in pairs)
            {
                //                string[] items = item.Split(sep2, StringSplitOptions.None);
                //                queryPairs.Add(items[0].Trim(), items[1].Trim());
                int ind = item.IndexOf('=');
                if (ind > 0)
                {
                    itemKey = item.Substring(0, ind).Trim();
                    value = item.Substring(ind + 1).Trim();
                    if (!queryPairs.ContainsKey(itemKey))
                        queryPairs.Add(itemKey, value);
                }
            }
            value = null;
            queryPairs.TryGetValue(key, out value);

            return value;
        }

     
        /// <summary>
        /// The account obj behind the name in the querystring. If null or unrecognized, returns 3GV account
        /// </summary>
        //protected WCFUtil.Account getAccountByName(string accountName)
        //{       
        //        WCFUtil.Account accountRes = null;
        //        if (!string.IsNullOrEmpty(accountName))
        //        {
        //            try
        //            {
        //                accountRes = FormHandler.GetAccountByName(accountName);
        //            }
        //            catch (Exception ex)
        //            {
        //                //use inigma
        //            }
        //        }
        //        if (accountRes == null)
        //            accountRes = new WCFUtil.Account() { ID = (int)Accounts.inigma, Name = Accounts.inigma.ToString(), ParentAccountID = null };
        //        return accountRes;
            
        //}

        #endregion
        #region General

        /// <summary>
        /// Redirects to page which presents error message that the user tried to get to a page with data he is not allowed to view
        /// </summary>
        protected void Redirect2NotAllowedPage()
        {
            Redirect2NotAllowedPage("You are not allowed to view the data you've requested");
        }

        protected void Redirect2NotAllowedPage(string errorMessage)
        {
            Response.Redirect(ConfigurationManager.AppSettings["NotAllowedPage"].ToString() + "?" + QueryStringKeys.ErrorMessage.ToString() + "=" +
                Server.UrlEncode(Encryption.Encrypt(errorMessage)));
        }

        /// <summary>
        /// Changing and set the controls headers betweens the site header 
        /// witch is the default header to unsecure header.
        /// </summary>
        /// <param name="controlName"></param>
        //protected void SetHeader(string controlName,bool hideHeaderTopLinks)
        //{
        //    ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl(controlName);
        //    if (cph == null)
        //        return;

        //    cph.Controls.Clear();

        //    LoginHeaderUC loginHeader = (LoginHeaderUC)LoadControl("LoginHeaderUC.ascx");
            
        //    if (hideHeaderTopLinks) 
        //        loginHeader.HeaderTopLinks.Visible = false;

        //    cph.Controls.Add(loginHeader);
        //}

        /// <summary>
        /// Changing and set the controls headers betweens the site footer 
        /// witch is the default footer to unsecure header.
        /// </summary>
        /// <param name="controlName"></param>
        protected void SetFooter(string controlName)
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl(controlName);
            if (cph == null)
                return;

            cph.Controls.Clear();

            LoginFooterUC loginFooter = (LoginFooterUC)LoadControl("LoginFooterUC.ascx");
            cph.Controls.Add(loginFooter);
        }
        
        #endregion
    }

    public enum QueryStringKeys
    {
        mode,
        LOBID,
        campaignID,
        codeID,
        UID,
        imageID,
        ErrorMessage,
        // pop chart image parameters
        LevelType,
        RootObjectID,
        PeriodMode,
        PredefinedPeriod,
        Width,
        Height,
        BarHeight,
        StartDate,
        EndDate,
        TimeZoneID,
        ChartType,
        Subject,
        ActivityStatus,
        BaseUrl,
        segmentID,
        deviceID,
        locationName,
        mediaID,
        UserStateDirect,
        INGUID,
        PrevPage,
        //Reports parameters
        ReportSubjectType,
        MonthlyPeriod,
        Year,
        HitLogPeriod,
        StatisticsReportGrouping,
        ReportID,
        DataMode,
        SaveAsATask,
        ScheduledPeriodID,
        ReportParamsID,
        //ZeroRated
        ZeroRatedID,
        ZeroRatedFilter,
        ZeroRatedSortExpression,
        ZeroRatedSortDirection,
        PrintVersion,
        ExtraButton,
        VState,
        RootSegmentID,
        SegmentGeneration,
        SegmentTitle,
        SegmentGenerationIndicator,
        SoftwareLineID,
        INGSID,
        ApplicationID,
        ErrorDetails,
        CountryID,
        HitMapPeriod,
        accountname,
        AdvertisementID,
        MessageLabelID,
        LanguageID
    }
}



