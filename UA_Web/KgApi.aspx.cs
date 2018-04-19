using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;

namespace UA_Web
{
    public partial class KgApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.QueryString["action"] ?? string.Empty;
            string query = Request.Url.Query.TrimStart('?');
            string text = string.Empty;
            switch (action.ToLower())
            {
                case "uploadrolename":
                    UploadroleName(action, query);
                    break;
            }
            Response.End();
        }

        private void UploadroleName(string action, string query)
        {
            
        }

        public string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
    }
}