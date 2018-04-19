using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;

namespace Games.NBall.WebServerFacade
{
    public class ZoneCache
    {
        private Dictionary<string, string> _zoneWebServerDic;
        private Dictionary<string, string> _zoneWebServerPlatformDic;
        private Dictionary<string, string> _zoneIdPlatDic;
        private Dictionary<string, string> _zoneApiUrlDic;
        private static ZoneCache _instance;
        static object _lockObj = new object();
        private ZoneCache()
        {
            Init();
        }

        public static ZoneCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            var a = new ZoneCache();
                            _instance = a;
                        }
                    }
                }
                return _instance;
            }

        }

        public string GetWebServerUrl(string zoneName)
        {
            if (!string.IsNullOrEmpty(zoneName))
            {
                zoneName = zoneName.ToLower();
                if (_zoneWebServerDic.ContainsKey(zoneName))
                    return _zoneWebServerDic[zoneName];
            }
            
            return "";
        }

        public string GetWebServerUrlForPlatform(string platformCode, string zoneName)
        {
            var key = BuildZoneKey(platformCode, zoneName);
            if (!string.IsNullOrEmpty(key))
            {
                if (_zoneWebServerPlatformDic.ContainsKey(key))
                    return _zoneWebServerPlatformDic[key];
            }
            return "";
        }
        public string GetPlatZoneId(string zoneName)
        {
            if (!string.IsNullOrEmpty(zoneName))
            {
                zoneName = zoneName.ToLower();
                if (_zoneIdPlatDic.ContainsKey(zoneName))
                    return _zoneIdPlatDic[zoneName];
            }
            return "";
        }

        public string GetApiUrl(string zoneName)
        {
            if (!string.IsNullOrEmpty(zoneName))
            {
                zoneName = zoneName.ToLower();
                if (_zoneApiUrlDic.ContainsKey(zoneName))
                    return _zoneApiUrlDic[zoneName];
            }
            return "";
        }

        private void Init()
        {
            try
            {
                var list = AllZoneinfoMgr.GetAllForFactory();
                _zoneWebServerDic = list.ToDictionary(d => d.ZoneName.ToLower(), d => d.WebServerUrl);
                _zoneWebServerPlatformDic = list.ToDictionary(d => BuildZoneKey(d.PlatformCode, d.PlatformZoneName),
                    d => d.WebServerUrl);
                _zoneIdPlatDic = list.ToDictionary(d => d.ZoneName.ToLower(), d => d.PlatformZoneName.ToLower());
                _zoneApiUrlDic = list.ToDictionary(d => d.ZoneName.ToLower(), d => d.ApiUrl);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }

        }

        string BuildZoneKey(string platformCode, string platformZoneName)
        {
            return platformCode.ToLower() + "_" + platformZoneName.ToLower();
        }
    }
}
