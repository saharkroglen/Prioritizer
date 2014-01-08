using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace PrioritizerService
{
    public enum HeaderTabs
    {
        Nothing,
        tabCampaigns,
        tabReports,
        tabAccount,
        tabHelp,
        tabSecurity,
        tabPrivacy,
        tabTerms

    }
    public partial class SiteStruct : System.Web.UI.MasterPage
    {
        public string VisibileOnNonPrintOnly;
        public string VisibileOnPrintOnly;

        /// <summary>
        /// Sets the tab to be highlighted.
        /// </summary>
        /// <param name="selectedTab">The tab to be highlighted</param>
        public void SetDisplayTab(HeaderTabs selectedTab)
        {
            ucSiteHeader.SetDisplayTab(selectedTab);
        }

        public void SetDefaultButton(string buttonID)
        {
            this.frmSite.DefaultButton = buttonID;
        }

        /// <summary>
        /// Sets the tab to be displayed.   
        /// </summary>
        /// <param name="selectedTab">The tab to be highlighted</param>
        public void HideTabs(HeaderTabs[] tabs2Hide)
        {
            ucSiteHeader.HideTabs(tabs2Hide);
            ucSiteFooter.HideLinks(tabs2Hide);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // clean cache of the browser.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

    }
}
