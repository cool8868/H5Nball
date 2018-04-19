using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core;
using System.Threading;
using Enyim.Caching;
using log4net;
using Enyim.Caching.Memcached;
using Games.NBall.Common;

namespace Games.NBall.ServiceEngine
{

    /// <summary>
    /// EnyimMem cache provider
    /// </summary>
    [CastleComponent(typeof(ICacheProvider), Lifestyle = LifestyleType.Singleton)]
    public class EnyimMemcachedProvider : ICacheProvider, IDisposable
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(EnyimMemcachedProvider));
        //private static MemcachedClient defaultCache = new MemcachedClient();
        private static Lazy<MemcachedClient> clientInstance = new Lazy<MemcachedClient>(CreateClient, LazyThreadSafetyMode.ExecutionAndPublication);
        //private static string nameSpace = ConfigurationManager.AppSettings["version"];
        //private static string siteId = ConfigurationManager.AppSettings["siteId"];

        public void Dispose()
        {
            try
            {
                if (clientInstance.IsValueCreated)
                {
                    clientInstance.Value.Dispose();
                }
            }
            catch(Exception ex )
            {
                LogHelper.LogError(logger, "EnyimMemcachedProvider.Dispose", "Dispose异常", ex);   
            }
        }

        private static string ContactKey(string key)
        {
            //return string.Concat(siteId, nameSpace, key);
            return key;
        }

        private static MemcachedClient CreateClient()
        {
            MemcachedClient client = null;
            try
            {
                client = new MemcachedClient();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.CreateClient", ex);
                throw;
            }
            return client;
        }

        public bool Set(string key, object value, int expiration)
        {
            if (value == null || string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            try
            {
                //使用绝对过期时间
                //return clientInstance.Value.Store(StoreMode.Set, ContactKey(key), value, DateTime.Now.AddSeconds(expiration));
                //使用相对过期时间
                return clientInstance.Value.Store(StoreMode.Set, ContactKey(key), value, new TimeSpan(0, 0,expiration));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.Set", ex, key, expiration);
                return false;
            }
        }

        public bool SetWithCas(string key, object value, int expiration, ulong cas, out ulong newCas)
        {
            newCas = 0;
            if (value == null || string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            try
            {
                //使用绝对过期时间
                //CasResult<bool> result = clientInstance.Value.Cas(StoreMode.Set, ContactKey(key), value, DateTime.Now.AddSeconds(expiration), cas);
                //使用相对过期时间
                CasResult<bool> result = clientInstance.Value.Cas(StoreMode.Set, ContactKey(key), value,new TimeSpan(0,0,expiration), cas);

                newCas = result.Cas;
                return result.Result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.SetWithCas", ex, key, expiration, cas);
                return false;
            }
        }

        public bool Add(string key, object value, int expiration)
        {
            if (value == null || string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            try
            {
                //使用绝对过期时间
                //return clientInstance.Value.Store(StoreMode.Add, ContactKey(key), value, DateTime.Now.AddSeconds(expiration));
                //使用相对过期时间
                return clientInstance.Value.Store(StoreMode.Add, ContactKey(key), value,new TimeSpan(0,0,expiration));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.Add", ex, key, expiration);
                return false;
            }
        }

        public IDictionary<string, object> Get(IEnumerable<string> key)
        {
            if (key == null)
            {
                return null;
            }

            try
            {
                return clientInstance.Value.Get(key);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.Get", ex, key);
                throw;
            }
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default(T);
            }

            try
            {
                return (T)clientInstance.Value.Get<T>(ContactKey(key));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.Get", ex, key, typeof(T));
                throw;
            }
        }

        public object Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            try
            {
                return clientInstance.Value.Get(ContactKey(key));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.Get", ex, key);
                throw;
            }
        }

        public void GetWithCas(string key, out ulong cas, out object value)
        {
            value = null;
            cas = 0;
            try
            {
                CasResult<object> result = clientInstance.Value.GetWithCas(ContactKey(key));
                cas = result.Cas;
                value = result.Result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.GetWithCas", ex, key);
            }
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            try
            {
                return clientInstance.Value.Remove(ContactKey(key));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.Remove", ex, key);
                return false;
            }
        }

        public bool TryGet(string key, out object value)
        {
            value = null;
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            try
            {
                bool result = clientInstance.Value.TryGet(ContactKey(key), out value);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.TryGet", ex, key);
                return false;
            }
        }

        public bool TryGetWithCas(string key, out ulong cas, out object value)
        {
            value = null;
            cas = 0;
            try
            {
                CasResult<object> result;
                bool success = clientInstance.Value.TryGetWithCas(ContactKey(key), out result);
                cas = result.Cas;
                value = result.Result;
                return success;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "EnyimMemcachedProvider.TryGetWithCas", ex, key);
                return false;
            }
        }

    }
}
