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
    public partial class FooterUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            //this.lblFotterYear.Text = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// Sets the tab arr to be disabled.
        /// </summary>
        /// <param name="selectedTab">The tab to be highlighted</param>
        public void HideLinks(HeaderTabs[] tabs2Hide)
        {
            foreach (HeaderTabs link in tabs2Hide)
            {
                Control control = this.FindControl(link.ToString());
                if (control == null)
                    continue;

                HtmlControl htmlControl = (HtmlControl)control;
                htmlControl.Visible = false;
            }
        }
    }
}