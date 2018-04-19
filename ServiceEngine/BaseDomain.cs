using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using log4net;
using System.Reflection;
using System.ServiceModel;
using System.Net.Sockets;
using ProtoBuf;
using Games.NBall.Common;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// Domain对象基类
    /// </summary>
    public abstract class BaseDomain
    {
        private static ILog logger = LogManager.GetLogger(typeof(BaseDomain));
        private readonly static Lazy<ICacheProvider> cache = new Lazy<ICacheProvider>(() => ObjectContainer.Instance.GetService<ICacheProvider>(), LazyThreadSafetyMode.ExecutionAndPublication);
        /// <summary>
        /// mem缓存时间
        /// </summary>
        protected const int defaultCacheExpiration = 60 * 60 * 4;
        /// <summary>
        /// 字典数据对象
        /// </summary>
        //protected static IConfigService resource = ObjectContainer.Instance.GetService<IConfigService>();

        private static ConcurrentDictionary<Type, bool> atrrProtobufCache = new ConcurrentDictionary<Type, bool>();

        private static bool IsProtobuf(object value)
        {
            if (value == null)
            {
                return false;
            }

            return IsProtobuf(value.GetType());
        }

        private static bool IsProtobuf(Type type)
        {
            if (type == null)
            {
                return false;
            }

            bool isProtoContract = false;
            if (!atrrProtobufCache.TryGetValue(type, out isProtoContract))
            {
                Type elementType;
                if (type.IsGenericType)
                {
                    elementType = type.GetGenericArguments()[0];
                }
                else
                {
                    elementType = type;
                }

                isProtoContract = elementType.GetCustomAttributes(typeof(ProtoContractAttribute), true).Length > 0;
                atrrProtobufCache.TryAdd(type, isProtoContract);
            }
            return isProtoContract;
        }

        /// <summary>
        /// mem缓存时间
        /// </summary>
        protected virtual int CacheExpiration
        {
            get { return defaultCacheExpiration; }
        }

        /// <summary>
        /// mem对象
        /// </summary>
        protected virtual ICacheProvider Cache
        {
            get
            {
                return cache.Value;
            }
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected bool RemoveCache(string key)
        {
            return cache.Value.Remove(key);
        }

        /// <summary>
        /// 序列化某个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected object Serialize<T>(T value)
        {
            if (IsProtobuf(value))
            {
                return SerializationHelper.ToByte<T>(value);
            }
            return value;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected T Deserialize<T>(object value)
        {
            if (IsProtobuf(typeof(T)))
            {
                byte[] byteData = value as byte[];
                if (byteData != null)
                {
                    if (byteData.Length > 0)
                    {
                        return SerializationHelper.FromByte<T>(byteData);
                    }
                    return (T)Activator.CreateInstance(typeof(T));
                }
            }
            return (T)value;
        }

        /// <summary>
        /// 获取并且设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected T GetOrSet<T>(string key, Func<T> func)
        {
            T value;
            if (TryGet(key, out value))
            {
                return value;
            }
            value = func();
            SetCache<T>(key, value);
            return value;
        }

        /// <summary>
        /// 获取并且设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="cacheExpiration"></param>
        /// <returns></returns>
        protected T GetOrSet<T>(string key, Func<T> func, int cacheExpiration)
        {
            T value;
            if (TryGet(key, out value))
            {
                return value;
            }
            value = func();
            SetCache<T>(key, value, cacheExpiration);
            return value;
        }

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool TryGet<T>(string key, out T value)
        {
            value = default(T);
            object cachedData = null;
            bool result = cache.Value.TryGet(key, out cachedData);
            if (result)
            {
                value = Deserialize<T>(cachedData);
            }
            return result;
        }

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cas"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool TryGetWithCas<T>(string key, out ulong cas, out T value)
        {
            value = default(T);
            object cachedData = null;
            bool result = cache.Value.TryGetWithCas(key, out cas, out cachedData);
            if (result)
            {
                value = Deserialize<T>(cachedData);
            }
            return result;
        }

        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        protected bool AddCache<T>(string key, T value, int expiration)
        {
            return cache.Value.Add(key, Serialize(value), expiration);
        }

        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool AddCache<T>(string key, T value)
        {
            return cache.Value.Add(key, Serialize(value), CacheExpiration);
        }

        /// <summary>
        /// 重置缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        protected bool SetCache<T>(string key, T value, int expiration)
        {
            return cache.Value.Set(key, Serialize(value), expiration);
        }

        /// <summary>
        /// 重置缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool SetCache<T>(string key, T value)
        {
            return cache.Value.Set(key, Serialize(value), CacheExpiration);
        }

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetCache<T>(string key)
        {
            T value = default(T);
            TryGet<T>(key, out value);
            return value;
        }

        /// <summary>
        /// 获取并且重置缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <param name="successHandler"></param>
        /// <param name="errorHandler"></param>
        /// <returns></returns>
        protected bool CasUpdate<T>(string key, Func<bool> query, Func<T, bool> action, Func<T, bool> successHandler, Func<T, bool> errorHandler)
        {
            int count = 0;
            int limit = 5;
            T cacheValue = default(T);

            while (count < limit)
            {
                count++;
                ulong cas;
                ulong newCas;
                cas = 0;

                if (!TryGetWithCas<T>(key, out cas, out cacheValue))
                {
                    if (query == null || !query())
                    {
                        return false;
                    }
                    continue;
                }
                bool result = action(cacheValue);
                if (!result)
                {
                    return false;
                }
                if (cache.Value.SetWithCas(key, Serialize(cacheValue), CacheExpiration, cas, out newCas))
                {
                    if (successHandler != null)
                    {
                        return successHandler(cacheValue);
                    }
                    return true;
                }
            }
            if (count == limit)
            {
                LogHelper.LogError(logger, "CasUpdate",
                                   string.Format("CasUpdate重试次数超过限制,参数：key:{0},query:{1},action:{2},FunName:{3}",
                                                 key, query.ToString(), action.ToString(), action.Method.Name));
            }
            if (errorHandler != null)
            {
                return errorHandler(cacheValue);
            }
            return false;
        }
    }
}
