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

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class ActivityBootstrapper : IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.MonthCard, VipCore.Instance.MonthCardSendPrize);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.WeeklyRefresh, VipCore.Instance.RefreshVipPackageJob);

                ActivityExThread.Instance.Start();

                //ScheduleManager.Instance.RegisterSchedule(EnumSchedule.SendActivityExPrize,
                //                                          ActivityExThread.Instance.RunSend);

                //ScheduleManager.Instance.RegisterSchedule(EnumSchedule.Active, ActiveCore.Instance.EverydayRefresh);
            }
        }
    }
}
