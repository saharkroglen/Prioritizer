using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Online;

namespace PrioritizerService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public int minutes = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["min"] != null)
            {
                int result;
                if (Int32.TryParse(Request.QueryString["min"], out result))
                    minutes = result;
            }
        }
    }
}