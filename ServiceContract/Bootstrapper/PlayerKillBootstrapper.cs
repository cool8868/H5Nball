using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Core.FriendShip;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class PlayerKillBootstrapper : IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                CacheFactory.PlayerKillCache.GetHashCode();
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.UpdatePKOpponent,
                                                          PlayerKillCore.Instance.JobUpdateOpponent);
            }
        }

    }
}
