using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// 终止接口
    /// </summary>
    public class ServiceFinallizer : IFinallizer
    {
        private static object syncLock = new object();

        /// <summary>
        /// 结束
        /// </summary>
        public void Terminate()
        {
            //lock (syncLock)
            //{
                
            //}
        }
    }
}
