using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
//using Games.NBall.Core.Active;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Dailycup;
//using Games.NBall.Core.Statistic;
using Games.NBall.Core.Vip;
using Games.NBall.ServiceEngine;
using Games.NBall.Core;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class ArenaBootstrapper : IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.PenaltyKickRank, PenaltyKickCore.Instance.SetRank);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.PenaltyKickPrize, PenaltyKickCore.Instance.SendPrize);
            }
        }
    }
}
