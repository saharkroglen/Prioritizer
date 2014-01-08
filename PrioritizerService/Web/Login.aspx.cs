using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrioritizerService
{
    public partial class Login : System.Web.UI.Page
    {
        private Online.PrioritizerService _prioritizerService = new Online.PrioritizerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Login2.Visible = false;
            }
            //    Response.Redirect("~/index.html");
        }


        private void Authenticate(AuthenticateEventArgs e)
        {
            string company = string.Empty;
            var ctl = Login2.FindControl("Company");
            if (ctl != null)
            {
                company = ((TextBox)ctl).Text;
            }

            if (_prioritizerService.Authenticate(Login2.UserName, Prioritizer.Shared.Utils.EncodePassword(Login2.Password), company) != null)
            {
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }
        }

     
        protected void Login2_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Authenticate(e);
        }
    }
}