using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using Shared;
using System.Reflection;
using System.ComponentModel;

namespace Prioritizer.Shared
{
    
    public static class Utils
    {
        //public static Dictionary<Guid, DateTime> connectedUsers = new Dictionary<Guid, DateTime>();
        //public static Dictionary<Guid, Users> _usersDict;
        public static List<ImportanceItem> GetImportanceList()
        {
            List<ImportanceItem> list = new List<ImportanceItem>();
            Array values = Enum.GetValues(typeof(enTaskImportance));
            Array names = Enum.GetNames(typeof(enTaskImportance));
            for(int i=0 ; i<values.Length; i++)
            {
                list.Add(new ImportanceItem() {ID= Convert.ToInt16(values.GetValue(i)), Name= names.GetValue(i).ToString()});
            }
            return list;
        }

        public static string getMoodName(enPokeMood mood)
        {
            switch (mood)
            {
                case enPokeMood.friendly:
                    return "Friendly Reminder";
                    break;
                case enPokeMood.surprised:
                    return "Surprised Poke";
                    break;
                case enPokeMood.frustrated:
                    return "Frustrated Poke";
                    break;
                case enPokeMood.mad:
                    return "Angry Poke";
                    break;
            }
            return null;
        }

        public static void SaveFileContent(string filePath, string fileContent)
        {
            using (FileStream fs = File.Create(filePath)) { }
            if (File.Exists(filePath))
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.Write(fileContent);
                }
            }
        }

        public static void SaveBinaryToFile(string filePath, byte[] b)
        {
            try
            {
                var bw = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate));
                bw.Write(b);
                bw.Close();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.Message, ex);
            }
        }

        public static void SaveFileContent(string directory, string fileName, string fileContent)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            SaveFileContent(string.Format(@"{0}\{1}",directory,fileName),fileContent);
            
        }

        public static string LoadFile(string filePath)
        {

            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        public static bool IsValidEmailFormat(string s) 
        {
            try
            {
                MailAddress mail = new MailAddress(s);
            }
            catch 
            {
                return false;
            }
            return true;
        }
        public static string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash    (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
                
        public static void createDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
       

    }

    public static class Parser
    {
        const string tryParseName = "TryParse";

        /// <summary>
        /// Returns the parsed value of a givven object to givven data type
        /// </summary>
        /// <typeparam name="T">The data type to parse the object to</typeparam>
        /// <param name="toParse">The object to be parsed</param>
        /// <returns></returns>
        public static T ToType<T>(object toParse) where T : struct
        {
            T result = default(T);
            try
            {
                //Iterate by reflection through the class methods available on the type parameter, 
                //and return the first TryParse method available
                MethodInfo tryParseMethod = null;
                tryParseMethod = typeof(T).GetMethods().Where(mi => mi.Name == tryParseName).FirstOrDefault();

                if (tryParseMethod != null)
                {
                    //Invoke the try parse method
                    object[] args = { toParse, result };
                    tryParseMethod.Invoke(null, args);
                    result = (T)args[1];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }

}
