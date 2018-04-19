using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;

namespace Games.NBall.Bll.Cache
{
    public class CrossSiteCache
    {
        #region Cache
        const int ROOTDomainId = 0;
        static readonly List<int> s_lstDomains = new List<int>();
        static readonly List<ConfigCrosssiteEntity> s_lstSites = new List<ConfigCrosssiteEntity>();
        static readonly Dictionary<int, List<string>> s_dicDomains = new Dictionary<int, List<string>>();
        static readonly Dictionary<string, ConfigCrosssiteEntity> s_dicSites = new Dictionary<string, ConfigCrosssiteEntity>();
        static readonly List<string> s_lstSitesName = new List<string>();
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile CrossSiteCache s_instnce = null;
        public readonly bool InitFlag = false;
        public static CrossSiteCache Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new CrossSiteCache();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private CrossSiteCache()
        {
            try
            {
                s_lstDomains.Clear();
                s_dicDomains.Clear();
                s_dicSites.Clear();
                s_lstSites.Clear();
                ConfigCrosssiteEntity siteItem = null;
                var sites = ConfigCrosssiteMgr.GetAll();
                var zones = AllZoneinfoMgr.GetAllForFactory();
                foreach (var item in sites)
                {
                    s_dicSites[item.SiteId] = item;
                    if (!s_dicDomains.ContainsKey(item.DomainId))
                        s_dicDomains[item.DomainId] = new List<string>();
                    s_dicDomains[item.DomainId].Add(item.SiteId);
                    s_lstSites.Add(item);
                }
                foreach (var item in zones)
                {
                    if (s_dicSites.ContainsKey(item.ZoneName))
                        continue;
                    siteItem = GetSiteItem(item);

                    s_dicSites[siteItem.SiteId] = siteItem;
                    if (!s_dicDomains.ContainsKey(siteItem.DomainId))
                        s_dicDomains[siteItem.DomainId] = new List<string>();
                    s_dicDomains[siteItem.DomainId].Add(siteItem.SiteId);
                    s_lstSites.Add(siteItem);
                    s_lstSitesName.Add(item.ZoneName);
                }
                foreach (var val in s_dicDomains.Keys)
                {
                    s_lstDomains.Add(val);
                }
                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossSiteCache:Init", ex);
                this.InitFlag = false;
            }
        }
        #endregion

        #region Retrieve
        public List<int> GetDomainList()
        {
            return s_lstDomains;
        }
        public List<ConfigCrosssiteEntity> GetSiteList()
        {
            return s_lstSites;
        }

        public List<string> GetSiteListName()
        {
            return s_lstSitesName;
        }

        public List<string> GetSiteListByDomain(int domainId)
        {
            List<string> rst;
            s_dicDomains.TryGetValue(domainId, out rst);
            return rst;
        }
        public bool CheckSiteId(string siteId)
        {
            if (string.IsNullOrEmpty(siteId))
                return true;
            return s_dicSites.ContainsKey(siteId);
        }
        public bool TryGetSite(string siteId, out ConfigCrosssiteEntity cfg)
        {
            return s_dicSites.TryGetValue(siteId, out cfg);
        }
        public bool TryGetDomainId(string siteId, out int domainId)
        {
            domainId = 0;
            ConfigCrosssiteEntity obj;
            if (!s_dicSites.TryGetValue(siteId, out obj))
                return false;
            domainId = obj.DomainId;
            return true;
        }
        #endregion

        ConfigCrosssiteEntity GetSiteItem(AllZoneinfoEntity src)
        {
            if (null == src)
                return null;
            var obj = new ConfigCrosssiteEntity();
            obj.DomainId = ROOTDomainId;
            obj.DomainName = "Root";
            obj.SiteId = src.ZoneName;
            obj.SiteName = src.CrossName;
            obj.ShowSiteId = src.CrossName;
            obj.ShowSiteName = src.CrossName;
            return obj;
        }
    }
}
