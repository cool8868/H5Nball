using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Bll.Share
{
    /// <summary>
    /// 单例工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SingletonFactory<T> where T : class
    {
        public static T SInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = (T)Activator.CreateInstance(typeof(T), 257);
                        }
                    }
                }
                return _instance;
            }
        }

        #region encapsulation

        private static volatile T _instance;
        static readonly object _lockObj = new object();
        private SingletonFactory() { }

        #endregion
    }
}
