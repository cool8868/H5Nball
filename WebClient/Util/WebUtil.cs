using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Net;
using Games.NBall.WebClient.Data;

namespace Games.NBall.WebClient.Util
{
    public static class WebUtil
    {
        #region Request
        public static HttpWebRequest CreateHttpRequest(string baseUrl, EnumWebRequestMethod method, NameValueCollection getArgs, int timeOut = 20000)
        {
            var uri = new Uri(baseUrl);
            string queryStr = BuildEscapeDataString(getArgs);
            if (!string.IsNullOrEmpty(queryStr))
                baseUrl = string.Format("{0}{1}{2}", baseUrl, uri.Query.Length == 0 ? "?" : "&", queryStr);
            return WebUtil.CreateHttpRequest(baseUrl, method, timeOut);
        }
        public static HttpWebRequest CreateHttpRequest(string url, EnumWebRequestMethod method = EnumWebRequestMethod.GET, int timeOut = 20000)
        {
            var req = HttpWebRequest.CreateHttp(url);
            req.Method = method.ToString();
            if (method == EnumWebRequestMethod.POST)
                req.ContentType = EnumWebContentType.PostContentType;
            req.ServicePoint.Expect100Continue = false;
            req.Timeout = timeOut;
            req.UserAgent = EnumWebUserAgent.Mozilla40;
            return req;
        }
        public static void PostRequestText(WebRequest req, string postData, Encoding encoding)
        {
            var bytes = encoding.GetBytes(postData);
            req.ContentLength = bytes.Length;
            using (var stream = req.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
        #endregion

        #region Response
        public static string WebGet(string baseUrl,int timeOut, NameValueCollection getArgs = null)
        {
            var req = CreateHttpRequest(baseUrl, EnumWebRequestMethod.GET, getArgs, timeOut);
            return WebUtil.GetResonseText(req);
        }
        public static string WebPost(string url, string data, int timeOut)
        {
            var req = WebUtil.CreateHttpRequest(url, EnumWebRequestMethod.POST, timeOut);
            WebUtil.PostRequestText(req, data, Encoding.UTF8);
            return WebUtil.GetResonseText(req);
        }
        public static string WebPost(string baseUrl, int timeOut, NameValueCollection getArgs = null, NameValueCollection postArgs = null)
        {
            var req = CreateHttpRequest(baseUrl, EnumWebRequestMethod.POST, getArgs, timeOut);
            string formStr = BuildEscapeDataString(postArgs);
            WebUtil.PostRequestText(req, formStr, Encoding.UTF8);
            formStr = null;
            return WebUtil.GetResonseText(req);
        }
        public static string GetResonseText(WebRequest req)
        {
            return GetResonseText(req.GetResponse());
        }
        public static string GetResonseText(WebRequest req, Encoding encoding)
        {
            return GetResonseText(req.GetResponse(), encoding);
        }
        public static string GetResonseText(WebResponse resp)
        {
            return GetResonseText(resp, Encoding.UTF8);
        }
        public static string GetResonseText(WebResponse resp, Encoding encoding)
        {
            string respTxt = string.Empty;
            using (var stream = resp.GetResponseStream())
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    respTxt = reader.ReadToEnd();
                }
            }
            resp.Close();
            return respTxt;
        }
        #endregion

        #region BuildArgsString
        public static string BuildEscapeDataString(NameValueCollection args)
        {
            if (null == args || args.Count == 0)
                return string.Empty;
            string key, val;
            var sb = new StringBuilder();
            for (int i = 0; i < args.Count; i++)
            {
                key = args.GetKey(i);
                val = args.Get(i);
                if (null == key || null == val)
                    continue;
                key = Uri.EscapeDataString(key);
                val = Uri.EscapeDataString(val);
                if (sb.Length == 0)
                    sb.AppendFormat("{0}={1}", key, val);
                else
                    sb.AppendFormat("&{0}={1}", key, val);
            }
            var str = sb.ToString();
            sb.Clear();
            return str;
        }
        public static string BuildEscapeDataString(IDictionary<string, string> args)
        {
            if (null == args || args.Count == 0)
                return string.Empty;
            string key, val;
            var sb = new StringBuilder();
            foreach (var kvp in args)
            {
                key = Uri.EscapeDataString(kvp.Key);
                val = Uri.EscapeDataString(kvp.Value);
                if (sb.Length == 0)
                    sb.AppendFormat("{0}={1}", key, val);
                else
                    sb.AppendFormat("&{0}={1}", key, val);
            }
            var str = sb.ToString();
            sb.Clear();
            return str;
        }
        #endregion
    }
}
