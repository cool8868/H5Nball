using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// 初始化
    /// </summary>
    public class ServiceBootstrapper : IBootstrapper
    {
        private static object syncLock = new object();

        public void Startup()
        {
            //lock (syncLock)
            //{
            //}
        }
    }
}
