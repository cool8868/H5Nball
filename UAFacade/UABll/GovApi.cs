using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Entity.Response;
using Games.NBall.WebClient.Plat;
using Games.NBall.Common;

namespace Games.NBall.UAFacade
{
    public static class GovApi
    {
        public static IPlatSessionData GetPlatSession()
        {
            return UAFactory.Instance.Adapter.GetPlatSesion(-1);
        }
        public static string GetNavSiteId()
        {
            var session = UAFactory.Instance.Adapter.GetPlatSesion(0);
            return session.LastSiteId ?? string.Empty;
        }
        public static string GetNavArgs()
        {
            var session = GetPlatSession();
            return session.AuthArgs ?? string.Empty;
        }
        public static string GetNavUrl()
        {
            var cfg = SiteMapCache.Instance().GetSiteConfig();
            if (null == cfg)
                return string.Empty;
            var session = GetPlatSession();
            return string.Format(cfg.NavUrl, session.AuthArgs).Replace('&', '|');
        }
        public static string GetBbsUrl()
        {
            var cfg = SiteMapCache.Instance().GetSiteConfig();
            if (null == cfg)
                return string.Empty;
            return cfg.BbsUrl;
        }
        public static string GetPlatAppId()
        {
            var cfg = SiteMapCache.Instance().GetSiteConfig();
            if (null == cfg)
                return string.Empty;
            return cfg.SiteApiUrl;
        }
        public static List<DTOSiteItem> GetSiteList()
        {
            var list = new List<DTOSiteItem>();
            try
            {
                string args = GetNavArgs();
                var sites = SiteMapCache.Instance().GetSites();
                DTOSiteItem obj = null;
                foreach (var item in sites)
                {
                    obj = new DTOSiteItem();
                    obj.SiteId = item.SiteId;
                    obj.SiteName = item.SiteName;
                    //obj.LoginUrl = string.Format(item.SiteLoginUrl, args);
                    //obj.SiteState = item.AsSiteState.ToString();
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "GovApi:GetSiteList");
            }
            return list;
        }



        
    }
}
