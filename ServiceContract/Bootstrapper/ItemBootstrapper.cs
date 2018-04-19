using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class ItemBootstrapper : IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                CacheFactory.AppsettingCache.GetHashCode();
                CacheFactory.PlayersdicCache.GetHashCode();
                CacheFactory.ItemsdicCache.GetHashCode();
                CacheFactory.PandoraCache.GetHashCode();
            }
        }
    }
}
