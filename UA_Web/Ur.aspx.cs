using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.UAFacade;
using Games.NBall.WebClient.Plat;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Common;
using System.Configuration;

namespace UA_Web
{
    public partial class Ur : System.Web.UI.Page
    {
        const string COLServer = "server";
        const string COLServerV2 = "server_id";
        public string Cdn = string.Empty;
        public string Version = string.Empty;
        public string NavSiteId=string.Empty;
        public string NavArgs=string.Empty;
        public string NavApiUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            bool navFlag = string.IsNullOrEmpty(ConfigurationManager.AppSettings["AutoNav"]);
            var platSession = GovApi.GetPlatSession();
            var zoneName = UAFactory.Instance.ZoneName;
            var zoneCache = FunctionAppCache.Instance.GetZone(zoneName);
            if (null == zoneCache)
            {
                redirectBack(platSession);
                return;
            }
            //if (string.IsNullOrEmpty(platSession.Uid) || null == zoneCache)
            //{
            //    redirectBack(platSession);
            //    return;
            //}
            this.NavSiteId = GovApi.GetNavSiteId();
            platSession.LastSiteId = this.NavSiteId;
            this.NavArgs = platSession.AuthArgs;
            this.NavApiUrl = GetNavApiUrl(platSession, navFlag);
            this.Cdn = zoneCache.Cdn;
            this.Version = zoneCache.ClientVersion;
            var req = HttpContext.Current.Request;
            string server = (req.QueryString[COLServerV2] ?? req.QueryString[COLServer]) ?? string.Empty;
            if (server == "-1")
                return;
            AllSitemapEntity site = null;
            if (!string.IsNullOrEmpty(server))
                site = SiteMapCache.Instance().GetPlatSiteConfig(server);
            if (null != site)
            {
                redirect(site, platSession);
                return;
            }
            if (!string.IsNullOrEmpty(platSession.LastSiteId))
                site = SiteMapCache.Instance().GetSiteConfig(platSession.LastSiteId);
            if (null != site)
            {
                redirect(site, platSession);
                return;
            }

            if (string.IsNullOrEmpty(server) && navFlag)
                return;
            site = SiteMapCache.Instance().GetSiteConfig();
            redirect(site, platSession);
        }
        void redirect(AllSitemapEntity site, IPlatSessionData platSession)
        {
          
            string url = string.Format(site.SiteLoginUrl, platSession.AuthArgs);
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TraceNav"]))
                LogHelper.Insert(string.Format("Redirect rawUrl:{0},newUrl:{1}", Request.Url.PathAndQuery, url), LogType.Error);
            HttpContext.Current.Response.RedirectPermanent(url, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        void redirectBack(IPlatSessionData platSession)
        {
            LogHelper.Insert(string.Format("RedirectBack rawUrl:{0},data:{1}", Request.Url.PathAndQuery, platSession), LogType.Error);
            var plat = SiteMapCache.Instance().GetPlatConfig();
            if (null != plat)
                HttpContext.Current.Response.Redirect(plat.PlatMainUrl, false);
        }
        string GetNavApiUrl(IPlatSessionData platSession, bool navFlag)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            int idx = url.LastIndexOf(@"/");
            if (idx > 0)
                url = url.Substring(0, idx + 1);
            if (navFlag)
                return string.Format("{0}api.ashx?action=sitelist&{1}", url, platSession.AuthArgs).Replace('&', '|');
            else
                return string.Concat(url, "api.ashx?action=sitelist");
        }
    }
}