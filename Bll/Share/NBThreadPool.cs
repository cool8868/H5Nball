using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amib.Threading;

namespace Games.NBall.Bll.Share
{
    /// <summary>
    /// NBThreadPool
    /// </summary>
    public class NBThreadPool
    {
        #region .ctor
        readonly SmartThreadPool _stp;
        public NBThreadPool(int maxWorkerThreads)
        {
            _stp = new SmartThreadPool(SmartThreadPool.DefaultIdleTimeout, maxWorkerThreads);
        }
        #endregion

        #region WorkItem
        public void Add(System.Action action)
        {
            _stp.QueueWorkItem(new Amib.Threading.Action(action));
        }
        public void WaitAll()
        {
            _stp.WaitForIdle();
        }
        #endregion
    }
}
