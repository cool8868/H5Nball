using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class SiteMapCache
    {
        #region Cache
        static readonly Dictionary<string, AllPlatmapEntity> s_dicPlats = new Dictionary<string, AllPlatmapEntity>();
        static readonly Dictionary<string, AllSitemapEntity> s_dicSites = new Dictionary<string, AllSitemapEntity>();
        static readonly Dictionary<string, AllSitemapEntity> s_dicPlatSites = new Dictionary<string, AllSitemapEntity>();
        static readonly Dictionary<string, AllUaplatformEntity> s_dicUaPlats = new Dictionary<string, AllUaplatformEntity>();

        private static readonly Dictionary<string, AllZoneinfoEntity> s_zoneDic =
            new Dictionary<string, AllZoneinfoEntity>();
        public static readonly string FactoryCode = string.Empty;
        public static readonly string SiteId = string.Empty;
        public readonly string PlatCode = string.Empty;
        public readonly AllZoneinfoEntity ZoneIno = null;
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile SiteMapCache s_instnce = null;
        public readonly bool InitFlag = false;
        public static SiteMapCache Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new SiteMapCache();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        static SiteMapCache()
        {
            string factCode = ConfigurationManager.AppSettings["FactoryName"] ?? string.Empty;
            string siteId = ConfigurationManager.AppSettings["ZoneName"] ?? string.Empty;
            FactoryCode = factCode.ToLower();
            SiteId = siteId;
        }
        private SiteMapCache()
        {
            try
            {
                s_dicPlats.Clear();
                s_dicSites.Clear();
                s_dicPlatSites.Clear();
                s_dicUaPlats.Clear();
                var plats = AllPlatmapMgr.GetAll();
                var sites = AllSitemapMgr.GetAll();
                var uaPlats = AllUaplatformMgr.GetAll();
                var zones = AllZoneinfoMgr.GetAll();
                string factCode = FactoryCode;
                AllPlatmapEntity root = null;
                foreach (var item in plats)
                {
                    item.PlatCode = item.PlatCode.ToLower();
                    s_dicPlats[item.PlatCode] = item;
                    if (null == root && string.Compare(factCode, item.PlatCode, true) == 0)
                        root = item;
                }
                foreach (var item in zones)
                {
                    if (string.Compare(item.ZoneName, SiteId, true) == 0)
                    {
                        PlatCode = item.PlatformCode;
                        ZoneIno = item;
                        break;
                    }
                    if (!s_zoneDic.ContainsKey(item.PlatformCode + item.ZoneName))
                        s_zoneDic.Add(item.PlatformCode + item.ZoneName, item);
                    s_zoneDic[item.PlatformCode + item.ZoneName] = item;
                }
                foreach (var item in sites)
                {
                    if (string.Compare(factCode, item.PlatCode, true) != 0)
                        continue;
                    s_dicSites[item.SiteId] = item;
                    s_dicPlatSites[item.PlatSiteId] = item;
                    if (null == root)
                        continue;
                    if (string.IsNullOrEmpty(item.PlatMainUrl))
                        item.PlatMainUrl = root.PlatMainUrl;
                    if (string.IsNullOrEmpty(item.PlatApiUrl))
                        item.PlatApiUrl = root.PlatApiUrl;
                    if (string.IsNullOrEmpty(item.PayUrl))
                        item.PayUrl = root.PayUrl;
                    if (string.IsNullOrEmpty(item.BbsUrl))
                        item.BbsUrl = root.BbsUrl;
                    if (string.IsNullOrEmpty(item.NavUrl))
                        item.NavUrl = root.NavUrl;
                    if (string.IsNullOrEmpty(item.CdnUrl))
                        item.CdnUrl = root.CdnUrl;
                    if (string.IsNullOrEmpty(item.ChatUrl))
                        item.ChatUrl = root.ChatUrl;
                }
                foreach (var item in uaPlats)
                {
                    s_dicUaPlats[item.PlatformCode.ToLower()] = item;
                }
                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SiteMapCache:Init", ex);
                this.InitFlag = false;
            }
        }
        #endregion

        #region Retrieve
        public AllPlatmapEntity GetGovConfig()
        {
            AllPlatmapEntity cfg;
            s_dicPlats.TryGetValue(EnumPlatCode.Gov, out cfg);
            return cfg;
        }
        public AllPlatmapEntity GetPlatConfig()
        {
            AllPlatmapEntity cfg;
            s_dicPlats.TryGetValue(FactoryCode, out cfg);
            return cfg;
        }
        public AllSitemapEntity GetSiteConfig()
        {
            return GetSiteConfig(SiteId);
        }
        public AllSitemapEntity GetSiteConfig(string siteId)
        {
            AllSitemapEntity cfg;
            s_dicSites.TryGetValue(siteId, out cfg);
            return cfg;
        }
        public AllSitemapEntity GetPlatSiteConfig(string platSiteId)
        {
            AllSitemapEntity cfg;
            s_dicPlatSites.TryGetValue(platSiteId, out cfg);
            return cfg;
        }
        public List<AllSitemapEntity> GetSites()
        {
            return s_dicSites.Values.ToList();
        }
        public AllUaplatformEntity GetUaPlatConfig(string uaPlatCode)
        {
            AllUaplatformEntity cfg;
            s_dicUaPlats.TryGetValue(uaPlatCode.ToLower(), out cfg);
            return cfg;
        }

        public string GetPlatZoneName(string platformCode, string zoneName)
        {
            if (s_zoneDic.ContainsKey(platformCode + zoneName))
                return s_zoneDic[platformCode + zoneName].PlatformZoneName;
            return "";
        }

        #endregion
    }
}
