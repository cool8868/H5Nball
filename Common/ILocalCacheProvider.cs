using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace Games.NBall.Common
{
    /// <summary>
    /// 本地缓存
    /// </summary>
    public interface ILocalCacheProvider
    {
        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        void Set(string key, object value, int expiration);

        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        bool Add(string key, object value, int expiration);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 删除缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);
    }
}
