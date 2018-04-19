using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.UAFacade.UABll;

namespace Games.NBall.UAFacade
{
    public class UAFactory
    {
        private static UAAdapter _adapter = null;
        private static string _factory;
        private static Dictionary<string, AllUaplatformEntity> _platformDic;
        private static int _timeout = 300;
        private static long _timeout24hour = 86400000;
        private static long _timeout30min = 1800000;
        private static string _platformUrl;
        private static string _errorPage;
        private static bool _uARequestRecord;
        private AllUaplatformEntity _defaultPlatform;
        private static UAFactory _instance;
        static object _lockObj = new object();
        private static Dictionary<string, AllZoneinfoEntity> _zoneDic;
        private static Dictionary<string, AllZoneinfoEntity> _zoneNameDic;

        private UAFactory()
        {
            Init();
        }

        public static UAFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            var a = new UAFactory();
                            _instance = a;
                        }
                    }
                }
                return _instance;
            }

        }

        void Init()
        {
            try
            {
                _errorPage = "/Error.aspx";
                _factory = ConfigurationManager.AppSettings["FactoryName"].ToLower();
                JumpScript = ConfigurationManager.AppSettings["JumpScript"];
                ZoneName = ConfigurationManager.AppSettings["ZoneName"].ToLower();
                var ualog = ConfigurationManager.AppSettings["OpenUALog"];
                _zoneDic = new Dictionary<string, AllZoneinfoEntity>();
                _zoneNameDic = new Dictionary<string, AllZoneinfoEntity>();
                if (!string.IsNullOrEmpty(ualog) && ualog == "1")
                    OpenUALog = true;
                var timeout = ConfigurationManager.AppSettings["UATimeOut"];
                var uARequestRecord = ConfigurationManager.AppSettings["UARequestRecord"];
                if (!string.IsNullOrEmpty(uARequestRecord) && uARequestRecord == "1")
                    _uARequestRecord = true;

                var zoneInfoList = AllZoneinfoMgr.GetByPlatform(FactoryName);
                foreach (var item in zoneInfoList)
                {
                    if (!_zoneDic.ContainsKey(item.ZoneName))
                        _zoneDic.Add(item.ZoneName, null);
                    _zoneDic[item.ZoneName] = item;
                    if (!_zoneNameDic.ContainsKey(item.PlatformCode + item.PlatformZoneName))
                        _zoneNameDic.Add(item.PlatformCode + item.PlatformZoneName, null);
                    _zoneNameDic[item.PlatformCode + item.PlatformZoneName] = item;
                }

                string className = _factory.ToLower();
                if (!string.IsNullOrEmpty(_factory))
                {
                    if (_factory.Contains("gov_"))
                    {
                        className = "gov";
                    }
                  
                }
                if (!string.IsNullOrEmpty(_factory))
                {
                    if (_factory.Contains("h5_a8"))
                    {
                        className = "A8";
                    }

                }
                if (!string.IsNullOrEmpty(_factory))
                {
                    if (_factory.Contains("txh5_a8"))
                    {
                        className = "wbTx";
                    }

                } 
                if (!string.IsNullOrEmpty(_factory))
                {
                    if (_factory.Contains("h5_wb"))
                    {
                        className = "wbTx";
                    }

                }
                if (!string.IsNullOrEmpty(_factory))
                {
                    if (_factory.Contains("h5_egret"))
                    {
                        className = "egret";
                    }

                }
                if (!string.IsNullOrEmpty(_factory))
                {
                    if (_factory.Contains("h5_bear"))
                    {
                        className = "bear";
                    }

                } 
                if (!string.IsNullOrEmpty(_factory))
                {
                    if (_factory.Contains("h5_qunhei"))
                    {
                        className = "qunhei";
                    }

                }
                if (!string.IsNullOrEmpty(timeout))
                {
                    var t = ConvertHelper.ConvertToInt(timeout);
                    if (t > 0)
                        _timeout = t;
                }
                _adapter = UAAdapter.GetInstance(className);
               
                var platforms = GetPlatformList(_factory);
                if (platforms == null || platforms.Count<=0)
                {
                    SystemlogMgr.Error("UaFactory create", "UaFactory config is null,factory:" + _factory);
                }
                else
                {
                    _platformDic = platforms.ToDictionary(d => d.PlatformCode, d => d);
                    _platformUrl = platforms[0].PlatformUrl;
                    _defaultPlatform = platforms[0];
                }
                if (IsTx)
                {

                    var appId = ConfigAppsettingMgr.GetById(20004);
                    if (appId != null)
                        _txAppId = ConvertHelper.ConvertToInt(appId.Value);
                    var appKey = ConfigAppsettingMgr.GetById(20005);
                    if (appKey != null)
                        _txAppKey = appKey.Value;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("UaFactory create", ex);
                throw ex;
            }
        }

        public bool IsDebug { get; set; }

        public UAAdapter Adapter
        {
            get { return _adapter; }
        }

        public bool OpenUALog { get; set; }

        public bool RequestRecord { get { return _uARequestRecord; } }

        public string PlatformUrl { get
        {
            if (string.IsNullOrEmpty(_platformUrl))
                return ErrorPage;
            else
            {
                return _platformUrl;
            }
        } }

        public string JumpScript { get; set; }

        public string ZoneName { get; set; }

        private int _txAppId;
        public int TxAppId{
            get { return _txAppId; }
        }

        private string _txAppKey;
        public string TxAppKey {
            get { return _txAppKey; }
        }

        public string FactoryName 
        {
            get { return _factory; }
        }
        public  bool IsH5A8
        {
            get { return ShareUtil.IsH5A8; }
        }
        public bool IsTx
        {
            get { return ShareUtil.IsTx; }
        }

        public bool IsEget
        {
            get { return ShareUtil.IsEgret; }
        }
        public bool IsBear
        {
            get { return ShareUtil.IsBear; }
        }

        public bool IsQunHei
        {
            get { return ShareUtil.IsQunHei; }
        }

        public string ErrorPage
        {
            get
            {
                var uri = HttpContext.Current.Request.Url.AbsoluteUri;
                if (uri.IndexOf('?') > 0)
                {
                    uri = uri.Substring(0, uri.IndexOf('?'));
                }
                uri = uri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                return uri + _errorPage; 
            }
        }

        public long  Timeout { get { return _timeout; } }
        public long Timeout30min {
            get { return _timeout30min; }
        }

        public long Timeout24Hour { get { return _timeout24hour; } }

        List<AllUaplatformEntity> GetPlatformList(string factoryCode)
        {
            try
            {
                return AllUaplatformMgr.GetByFactory(factoryCode);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return null;
            }
        }

        public string GetRedirectURL(string platForm, string redirectType)
        {
            if (string.IsNullOrEmpty(platForm))
            {
                return _platformUrl;
            }

            if (_platformDic.ContainsKey(platForm))
            {
                var platformEntity = _platformDic[platForm];
                switch (redirectType.ToLower())
                {
                    case "charge":
                        return platformEntity.ChargeUrl;
                        break;
                    case "useraction":
                        return platformEntity.UserActionUrl;
                        break;
                    default:
                        return platformEntity.PlatformUrl;
                        break;
                }
            }
            return _platformUrl;
        }


        public AllUaplatformEntity GetPlatform(string platformCode)
        {
            if (string.IsNullOrEmpty(platformCode))
                return _defaultPlatform;
            if (_platformDic.ContainsKey(platformCode))
                return _platformDic[platformCode];
            return _defaultPlatform;
        }

        public string GetPlatFormZoneName(string zoneName)
        {
            if (_zoneDic.ContainsKey(zoneName))
                return _zoneDic[zoneName].PlatformZoneName;
            return "";
        }

        public AllZoneinfoEntity GetZoneByZoneName(string zoneName)
        {
            if (_zoneDic.ContainsKey(zoneName))
                return _zoneDic[zoneName];
            return null;
        }

        public int GetPlatformId(string platformCode)
        {
            var platform = GetPlatform(platformCode);
            return platform == null ? 0 : platform.Idx;
        }

        public List<AllUaplatformEntity> GetAllPlatform()
        {
            List<AllUaplatformEntity> list = new List<AllUaplatformEntity>();
            foreach (var entity in _platformDic.Values)
            {
                list.Add(entity);
            }
            return list;
        }

        public string GetZoneName(string platformCode,string flatZoneName)
        {
            if (_zoneNameDic.ContainsKey(platformCode + flatZoneName))
                return _zoneNameDic[platformCode + flatZoneName].ZoneName;
            return "";
        }

        public AllZoneinfoEntity GetZone(string platformCode, string flatZoneName)
        {
            if (_zoneNameDic.ContainsKey(platformCode + flatZoneName))
                return _zoneNameDic[platformCode + flatZoneName];
            return null;
        }
    }
}