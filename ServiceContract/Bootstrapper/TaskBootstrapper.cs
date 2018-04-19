using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class TaskBootstrapper: IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                CacheFactory.TaskConfigCache.GetHashCode();
            }
        }
    }
}
