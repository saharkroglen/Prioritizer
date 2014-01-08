using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrioritizerService.Web;

namespace PrioritizerService.Web
{
    public class LoginHandler : BaseHandler
    {
        #region Public Methods

        /// <summary>
        /// Returns an object, which contains the status of his login and the user object if exist
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckLogin(string username, string password, string accountID)
        {
            return false;
        }

        /// <summary>
        /// Saves the time the user entered to application
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int? SaveUserLogin(int userID)
        {
            return 0;
        }

        #endregion
    }
}
