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
using PrioritizerService.Web;

namespace PrioritizerService
{
    public partial class SiteHeaderUC : System.Web.UI.UserControl
    {
        private BaseHandler FormHandler = new BaseHandler();
        #region Public Members

        /// <summary>
        /// Sets the tab to be highlighted.
        /// </summary>
        /// <param name="selectedTab">The tab to be highlighted</param>
        public void SetDisplayTab(HeaderTabs selectedTab)
        {
            string selected = Enum.GetName(typeof(HeaderTabs), selectedTab);
            string[] names = Enum.GetNames(typeof(HeaderTabs));

            foreach (string name in names)
            {
                Control control = this.FindControl(name);
                if (control == null)
                    continue;

                HtmlControl htmlControl = (HtmlControl)control;

                if (name == selected)
                    htmlControl.Attributes.Add("class", "tabOn");
                else
                    htmlControl.Attributes.Add("class", "tabOff");
            }
        }

        /// <summary>
        /// Sets the tab arr to be disabled.
        /// </summary>
        /// <param name="selectedTab">The tab to be highlighted</param>
        public void HideTabs(HeaderTabs[] tabs2Hide)
        {
            foreach (HeaderTabs tab in tabs2Hide)
            {
                Control control = this.FindControl(tab.ToString());
                if (control == null)
                    continue;
                
                HtmlControl htmlControl = (HtmlControl)control;
                htmlControl.Visible=false;
            }
        }

        public string getDBVersion()
        {
            return "version bla";
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lnkLogedOut_Click(object sender, EventArgs e)
        {
            Response.Cookies[ConfigurationManager.AppSettings["UserStateCookieName"].ToString()].Expires = DateTime.Now.AddDays(-1);
            //Cookies.ClearCookie(Response,ConfigurationManager.AppSettings["UserStateCookieName"].ToString());
            Response.Redirect(ConfigurationManager.AppSettings["LoginPage"].ToString());
        }
        
        protected void lnkOperations_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operations.aspx");
        }

        #endregion
    }
}