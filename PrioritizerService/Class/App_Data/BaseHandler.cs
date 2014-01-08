using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PrioritizerService.Web
{
    [DataContract]
    public enum Privileges
    {
        [EnumMember]
        admin = 1,
        [EnumMember]
        user = 2
    }
    public class BaseHandler
    {
        #region Inheritted Methods

        /// <summary>
        /// Returns if current user can enter this page
        /// </summary>
        /// <param name="userID">ID of user</param>
        /// <returns></returns>
        public bool CanViewPage(int userID, Privileges privilege)
        {
            return true;// new User(userID).IsHasPrivilege(privilege);
        }

        /// <summary>
        /// Returns the max number of minutes the cookie should stay alive
        /// </summary>
        /// <returns></returns>
        public int GetCookieTimeOut()
        {
            return ConfigValues.GetValue<int>(Configurations.CookiesTimeOut);
        }

        #endregion
    }
}
