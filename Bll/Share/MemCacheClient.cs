using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.ServiceEngine;

namespace Games.NBall.Bll.Share
{
    public class MemCacheClient : BaseDomain
    {
        private string _cacheCode;
        private int _seconds = -1;
        public MemCacheClient(string cacheCode)
        {
            BuildCacheCode(cacheCode);
        }

        public MemCacheClient(string cacheCode,int seconds)
        {
            BuildCacheCode(cacheCode);
            _seconds = seconds;
        }

        void BuildCacheCode(string cacheCode)
        {
            var zoneName = ShareUtil.ZoneName;
            if (string.IsNullOrEmpty(zoneName))
                zoneName = "";
            this._cacheCode = zoneName + "_" + cacheCode;
        }

        public bool Set<T>(string key, T value, int seconds=-1)
        {
            if (seconds == -1)
                seconds = _seconds;
            try
            {
                return SetCache<T>(_cacheCode.ConcatWith(key), value, seconds);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }

        public T Get<T>(string key)
        {
            try
            {
                return GetCache<T>(_cacheCode.ConcatWith(key));
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return default(T);
            }
        }

        public bool Set<T>(Guid key, T value, int seconds = -1)
        {
            return Set<T>(key.ToString(), value,seconds);
        }

        public T Get<T>(Guid key)
        {
            return Get<T>(key.ToString());
        }

        public bool Set<T>(int key, T value, int seconds)
        {
            return Set<T>(key.ToString(), value, seconds);
        }

        public T Get<T>(int key)
        {
                return Get<T>(key.ToString());
        }

        public bool Delete(string key)
        {
            try
            {
                return RemoveCache(_cacheCode.ConcatWith(key));
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }

        public bool Delete(Guid key)
        {
            return Delete(key.ToString());
        }

        public bool Delete(int key)
        {
            return Delete(key.ToString());
        }
    }
}
