using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;

namespace Games.NBall.Bll.Cache
{
    public class CacheManager
    {
        private Dictionary<int, ResetDelegate> _cacheDic; 
        #region .ctor

        public CacheManager(int p)
        {
            _cacheDic=new Dictionary<int, ResetDelegate>();
        }
        #endregion

        #region Facade

        public static CacheManager Instance
        {
            get { return SingletonFactory<CacheManager>.SInstance; }
        }
        
        public delegate bool ResetDelegate();

        public bool Register(EnumCacheType cacheType, ResetDelegate resetDelegate)
        {
            var iType = (int) cacheType;
            if (!_cacheDic.ContainsKey(iType))
            {
                _cacheDic.Add(iType, resetDelegate);
                LogHelper.Insert(cacheType.ToString() + " register success.", LogType.Info);
                return true;
            }
            return true;
        }

        public string ResetCache(int cacheType)
        {
            try
            {
                if (_cacheDic.ContainsKey(cacheType))
                {
                    if (_cacheDic[cacheType].Invoke())
                        return "success";
                    else
                    {
                        return "fail";
                    }
                }
                else
                {
                    foreach (var resetDelegate in _cacheDic)
                    {
                        LogHelper.Insert("cache:"+resetDelegate.Key,LogType.Info);
                    }
                    return "can't find cacheType[" + cacheType + "]";
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ResetCache", ex);
                return "exception:"+ex.Message;
            }
            
        }
        #endregion


    }
}
