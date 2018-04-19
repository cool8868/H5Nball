using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class FunctionAppCache
    {
        #region .ctor
        public FunctionAppCache(int p)
        {
            InitCache();
        }
        #endregion

        #region Facade

        public static FunctionAppCache Instance { get { return SingletonFactory<FunctionAppCache>.SInstance; } }

        public int GetFunctionId(string name)
        {
            if (_functionDic.ContainsKey(name))
                return _functionDic[name];
            else
            {
                return AddFunctionToDb(name);
            }
        }

        public int GetAppId(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 0;
            if (_appDic.ContainsKey(name))
                return _appDic[name];
            else
            {
                return 0;
            }
        }

        public string GetCrossZoneName(string zone)
        {
            var zoneEntity = GetZone(zone);
            if (zoneEntity != null)
                return zoneEntity.CrossName;
            else
            {
                return "";
            }
        }

        public AllZoneinfoEntity GetZone(string zone)
        {
            zone = zone.ToLower();
            if (_zoneDic.ContainsKey(zone))
                return _zoneDic[zone];
            else
            {
                SystemlogMgr.Error("GetZone", "can't find zone info,code:" + zone);
                return null;
            }
        }

        public AllZoneinfoEntity GetZoneByPlatform(string platformCode, string platformZoneName)
        {
            var key = BuildZoneKey(platformCode, platformZoneName);
            if (_zonePlatformDic.ContainsKey(key))
                return _zonePlatformDic[key];
            else
            {
                SystemlogMgr.Error("GetZone", "can't find zone info,code:" + platformCode + ",zonename:" + platformZoneName);
                return null;
            }
        }

        public int GetZoneId(string zone)
        {
            zone = zone.ToLower();
            if (zone == "cross")
                return 9999;
            if (_zoneDic.ContainsKey(zone))
                return _zoneDic[zone].Idx;
            else
            {
                SystemlogMgr.Error("GetZoneId", "can't find zone info,code:" + zone);
                return 0;
            }
        }

        public bool CheckOpenIndulge(string zone)
        {
            var zoneEntity = GetZone(zone);
            if (zoneEntity != null)
                return zoneEntity.OpenIndulge;
            else
            {
                return false;
            }
        }
        #endregion

        #region encapsulation

        private Dictionary<string, int> _appDic;
        private Dictionary<string, int> _functionDic;
        private Dictionary<string, AllZoneinfoEntity> _zoneDic;
        private Dictionary<string, AllZoneinfoEntity> _zonePlatformDic;

        static object _lockFunction = new object();
        void InitCache()
        {
            LogHelper.Insert("Function app cache init start", LogType.Info);
            var list = AllAppMgr.GetAllForFactory();
            _appDic = list.ToDictionary(d => d.Name, d => d.Idx);

            var list2 = AllLogfunctionMgr.GetAllForFactory();
            _functionDic = list2.ToDictionary(d => d.Name, d => d.Idx);

            var list3 = AllZoneinfoMgr.GetAllForFactory();
            foreach (var entity in list3)
            {
                entity.ApiHost = entity.ApiUrl.ToLower().Replace("http://", "").TrimEnd('/');
            }
            _zoneDic = list3.ToDictionary(d => d.ZoneName.ToLower(), d => d);
            _zonePlatformDic = list3.ToDictionary(d => BuildZoneKey(d.PlatformCode, d.PlatformZoneName), d => d);
            LogHelper.Insert("Function app cache init end", LogType.Info);
        }

        string BuildZoneKey(string platformCode, string platformZoneName)
        {
            return platformCode.ToLower() + "_" + platformZoneName.ToLower();
        }

        int AddFunctionToDb(string functionName)
        {
            lock (_lockFunction)
            {
                if (!_functionDic.ContainsKey(functionName))
                {
                    var entity = AllLogfunctionMgr.AddNewForFactory(functionName, DateTime.Now);
                    if (entity != null)
                    {
                        _functionDic.Add(functionName, entity.Idx);
                        return entity.Idx;
                    }
                }
                else
                {
                    return _functionDic[functionName];
                }
            }
            return 0;
        }
        #endregion


    }
}
