using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;
using Newtonsoft.Json;
using Games.NBall.UAFacade;
using Games.NBall.WebClient;
using Games.NBall.WebClient.Util;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Weibo.Data;

namespace UA_Web
{
    /// <summary>
    /// Api 的摘要说明
    /// </summary>
    public class Api : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"] ?? string.Empty;
            string query = context.Request.Url.Query.TrimStart('?');
            string text = string.Empty;
            switch (action.ToLower())
            {
                case "sitelist":
                    text = GetSiteList();
                    break;
                case DefineWyxUri.CastUserShow:
                    text = GetWyxApi(query, action, DefineWyxUri.User_show);
                    break;
                case DefineWyxUri.CastRoleUpdate:
                    text = PostWyxApi(query, action, DefineWyxUri.Ingame_RoleUpdate);
                    break;
            }
            if (!string.IsNullOrEmpty(text))
                context.Response.Write(text);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

   
        string GetSiteList()
        {
            var siteList = GovApi.GetSiteList();
            return JsonConvert.SerializeObject(siteList);
        }

        string GetWyxApi(string query,string castUrl, string rawUrl)
        {
            query = query.Replace(string.Concat("action=", castUrl), string.Empty);
            string url = string.Format("{0}{1}.json?{2}", DefineWyxUri.PREFIXWyxUriV1, rawUrl, query);
            return WebUtil.WebGet(url, 3000);
        }
        string PostWyxApi(string query, string castUrl, string rawUrl)
        {
            query = query.Replace(string.Concat("action=", castUrl), string.Empty).TrimStart('&');
            string url = string.Format("{0}{1}.json", DefineWyxUri.PREFIXWyxUriV1, rawUrl);
            var req = WebUtil.CreateHttpRequest(url, EnumWebRequestMethod.POST, 3000);
            WebUtil.PostRequestText(req, query, Encoding.UTF8);
            return WebUtil.GetResonseText(req);
        }
    }
}