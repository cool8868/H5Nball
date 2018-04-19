using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Games.NBall.WebClient.Util
{
    public static class CryptoUtil
    {
        public static string GetSHA1(string input, string hexFormat = "x2")
        {
            var csp = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(input);
            var nBytes = csp.ComputeHash(bytes);
            csp.Clear();
            var sb = new StringBuilder();
            for (int i = 0; i < nBytes.Length; i++)
            {
                sb.Append(nBytes[i].ToString(hexFormat));
            }
            string cypto = sb.ToString();
            sb.Clear();
            return cypto;
        }
        public static string GetMD5(string input, string hexFormat = "x2")
        {
            var csp = new MD5CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(input);
            var nBytes = csp.ComputeHash(bytes);
            csp.Clear();
            var sb = new StringBuilder();
            for (int i = 0; i < nBytes.Length; i++)
            {
                sb.Append(nBytes[i].ToString(hexFormat));
            }
            string cypto = sb.ToString();
            sb.Clear();
            return cypto;
        }

        public static string FromBase64(string input)
        {
            var bytes=System.Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(bytes);
        }
        public static string ToBase64(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            return System.Convert.ToBase64String(bytes);
        }
    }
}
