using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace Games.NBall.Common
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class MemoryCacheProvider : ILocalCacheProvider
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        protected ObjectCache cache;

        /// <summary>
        /// 
        /// </summary>
        public MemoryCacheProvider()
        {
            cache = MemoryCache.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public MemoryCacheProvider(string name)
        {
            cache = new MemoryCache(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        public void Set(string key, object value, int expiration)
        {
            if (value == null)
            {
                return;
            }

            CacheItemPolicy policy = new CacheItemPolicy();

            //使用绝对过期时间
            //policy.AbsoluteExpiration = DateTime.Now.AddSeconds(expiration);
            //policy.SlidingExpiration = MemoryCache.NoSlidingExpiration;
            //使用相对过期时间
            policy.AbsoluteExpiration = MemoryCache.InfiniteAbsoluteExpiration;
            policy.SlidingExpiration = new TimeSpan(0,0,expiration);
            
            cache.Set(key, value, policy);
        }

        public bool Add(string key, object value, int expiration)
        {
            if (value == null)
            {
                return false;
            }

            CacheItemPolicy policy = new CacheItemPolicy();

            //使用绝对过期时间
            //policy.AbsoluteExpiration = DateTime.Now.AddSeconds(expiration);
            //policy.SlidingExpiration = MemoryCache.NoSlidingExpiration;
            //使用相对过期时间
            policy.AbsoluteExpiration = MemoryCache.InfiniteAbsoluteExpiration;
            policy.SlidingExpiration = new TimeSpan(0, 0, expiration);

            return cache.Add(key, value, policy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return cache.Get(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            cache.Remove(key);
            return true;
        }
    }
}
