using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrioritizerService.Model;
using Prioritizer.Shared.Model;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace  PrioritizerService
{
    public static class ServerUtils
    {
        public static Dictionary<Guid, DateTime> connectedUsers = new Dictionary<Guid, DateTime>();
        public static Dictionary<Guid, Users> _usersDict;
    }

    //public partial class Users
    //{
    //    public  string getHashCode()
    //    {
    //        return this.ID + "_" + this.TenantID;
            
    //    }
    //}
    public static class ExtensionMethods
    {
        public static string DomainUserKey(this Users user)
        {
            return user.ID + "@" + user.TenantID;
        }
    }
    public static class Cookies
    {
        #region SetCookie

        /// <summary>
        /// Set a value to a cookie
        /// </summary>
        /// <param name="httpResponse">The http response where to return the cookie</param>
        /// <param name="cookieName">Name of general cookie</param>
        /// <param name="cookieValue">string value to set the cookie</param>
        public static void SetCookie(HttpResponse httpResponse, string cookieName, string cookieValue)
        {
            httpResponse.Cookies[cookieName].Value = cookieValue;
            HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
            httpResponse.Cookies.Set(cookie);
        }

        /// <summary>
        /// Set a value to a cookie
        /// </summary>
        /// <param name="httpResponse">The http response where to return the cookie</param>
        /// <param name="cookieName">Name of general cookie</param>
        /// <param name="cookieValue">string value to set the cookie</param>
        public static void SetCookie(HttpResponse httpResponse, string cookieName, string cookieValue, DateTime? expireDate)
        {
            HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
            if (expireDate.HasValue)
                cookie.Expires = expireDate.Value;
            else
                cookie.Expires = DateTime.Now.AddYears(1);
            httpResponse.Cookies.Set(cookie);
        }

        #endregion

        #region GetCookie

        /// <summary>
        /// Returns the string value in the cookie
        /// </summary>
        /// <param name="httpRequest">The request where the cookie exist</param>
        /// <param name="cookieName">Name of the cookie</param>
        /// <returns></returns>
        public static string GetCookie(HttpRequest httpRequest, HttpResponse httpResponse, string cookieName)
        {
            if (httpRequest.Cookies[cookieName] == null || httpRequest.Cookies[cookieName].Value == null)
            {
                if (httpResponse.Cookies[cookieName] == null || httpResponse.Cookies[cookieName].Value == null)
                    return null;

                return httpResponse.Cookies[cookieName].Value;
            }
            return httpRequest.Cookies[cookieName].Value;
        }

        #endregion

        #region Remove Cookies

        /// <summary>
        /// Deletes  cookie 
        /// </summary>
        /// <param name="response"></param>
        public static void ClearCookie(HttpResponse response, string cookieName)
        {
            response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
        }

        #endregion
    }

    public static class Serialization
    {
        #region Binary Serialization

        public static string BinSerialise(object objToSerialize)
        {
            return Convert.ToBase64String(BinSerialise2Bytes(objToSerialize));
        }

        public static byte[] BinSerialise2Bytes(object objToSerialize)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formater = new BinaryFormatter();
            formater.Serialize(stream, objToSerialize);
            return stream.ToArray();
        }

        public static T BinDesiralize<T>(string serializedObj)
        {
            try
            {
                return BinDesiralize<T>(Convert.FromBase64String(serializedObj));
            }
            catch (Exception ex)
            {
                throw ex;
                return default(T);
            }
        }

        public static T BinDesiralize<T>(byte[] serializedObj)
        {
            try
            {
                MemoryStream stream = new MemoryStream(serializedObj);
                BinaryFormatter formater = new BinaryFormatter();
                return (T)formater.Deserialize(stream);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static void saveDatasetToFile(string filePath, DataSet ds)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                string binarySerialized = Serialization.BinSerialise(ds);
                byte[] dBytes;
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                dBytes = enc.GetBytes(binarySerialized);
                fs.Write(dBytes, 0, dBytes.Count());
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save DataSet to file: '" + filePath + "'", ex);
            }
        }

        public static DataSet LoadDatasetFromFile(string filePath)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                string str = enc.GetString(bytes);
                DataSet ds = Serialization.BinDesiralize<DataSet>(str);
                fs.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load DataSet from file: '" + filePath + "'", ex);
            }
        }

        #endregion
    }
}