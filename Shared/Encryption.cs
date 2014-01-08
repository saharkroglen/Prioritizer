using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Prioritizer.Shared
{
    /// <summary>
    /// Handles:1. one way encryption, means, hashing text which cannot be traced back, by .Net cryptography.
    ///         2. TwoWayEncryption, means, Encryption and Decryption givven text, Using private key.
    /// </summary>
    public static class Encryption
    {
        #region One Way Encryption

        static SHA1 sha ;
        //static SHA256 sha = new SHA256CryptoServiceProvider();

        /// <summary>
        /// Returns encrypted text as byte array. Note: this cannot be decrypted back.
        /// </summary>
        /// <param name="textToHash"></param>
        /// <returns></returns>
        public static byte[] GetHashed(string textToHash)
        {
            sha = new SHA1CryptoServiceProvider();
            //Convert the givven text to byte array
            byte[] arrToHash = Encoding.UTF8.GetBytes(textToHash);

            byte[] hashedByte = sha.ComputeHash(arrToHash);

            //Clear the input byte array memory
            Array.Clear(arrToHash, 0, arrToHash.Length);

            return hashedByte;
        }

        /// <summary>
        /// Returns encrypted text as string. Note: this cannot be decrypted back.
        /// </summary>
        /// <param name="textToHash"></param>
        /// <returns></returns>
        public static string GetHashedString(string textToHash)
        {
            return Convert.ToBase64String(GetHashed(textToHash));
        }

        #endregion
        #region Two Way Encryption

        //Secret keys for the encryption algorythm
        const string DESKey = "YOSSILEV";
        const string DESIV = "HAISCHWARTZ";
        //The crypto provider to use.
        static DESCryptoServiceProvider des;

        static byte[] key;
        static byte[] IV;
        static byte[] inputByteArray;

        /// <summary>
        /// Returns string after symetric encryption
        /// </summary>
        /// <param name="textToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string textToEncrypt)
        {
            return textToEncrypt;
            des = new DESCryptoServiceProvider();
            
            try
            {

                key = Encoding.UTF8.GetBytes(DESKey);

                IV = Encoding.UTF8.GetBytes(DESIV);

                inputByteArray = Encoding.UTF8.GetBytes(textToEncrypt);

                MemoryStream ms = new MemoryStream(); 
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }


        }
        /// <summary>
        /// Returns a string after decrypting the givven text
        /// </summary>
        /// <param name="textToDecrypt"></param>
        /// <returns></returns>
        public static string Decrypt(string textToDecrypt)
        {
            return textToDecrypt;
            try
            {
                //textToDecrypt = textToDecrypt.Replace(" ", "");
                key = Encoding.UTF8.GetBytes(DESKey);

                IV = Encoding.UTF8.GetBytes(DESIV);

                int len = textToDecrypt.Length;
                inputByteArray = Convert.FromBase64String(textToDecrypt);


                des = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream(); 
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                
                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8; 
                return encoding.GetString(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        #endregion
    }
}
