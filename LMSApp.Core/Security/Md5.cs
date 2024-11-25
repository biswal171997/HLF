using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LMSApp.Core
{
    /// <summary>
    /// MD5 encryption
    /// </summary>
    public class Md5Hash
    {
        /// <summary>
        /// MD5 encryption
        /// </summary>
        /// <param name="str">Encrypted character</param>
        /// <param name="code">Encrypted digits 16/32</param>
        /// <returns></returns>
        public static string Md5(string str, int code)
        {
            byte[] bytes;
            using (var md5 = MD5.Create())
            {
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            }

            var result = new StringBuilder();
            foreach (byte t in bytes)
            {
                result.Append(t.ToString("X2"));
            }
            if (code == 16)
            {
                return result.ToString().Substring(8, 16);
            }
            return result.ToString();
        }
    }
    public class SHA512Hash
    {
        
        public static string SHa512(string str)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
