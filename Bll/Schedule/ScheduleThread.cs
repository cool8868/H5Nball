using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;

namespace Games.NBall.Bll.Schedule
{
    public class ScheduleThread
    {
        #region .ctor

        private NBThreadPool _threadPool;

        public ScheduleThread(int p)
        {
            _threadPool = new NBThreadPool(10);
        }
        
        #endregion

        #region Facade
        public static ScheduleThread Instance
        {
            get { return SingletonFactory<ScheduleThread>.SInstance; }
        }

        public void AddWork(System.Action action)
        {
            _threadPool.Add(action);
        }
        #endregion

    }
}
