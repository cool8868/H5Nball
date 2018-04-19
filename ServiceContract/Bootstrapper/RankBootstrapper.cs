using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
using Games.NBall.Core.Rank;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class RankBootstrapper: IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.CommonRanking,
                                                         RankThread.Instance.RunJob);
            }
        }
    
    }
}
