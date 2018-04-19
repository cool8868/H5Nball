using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
using Games.NBall.Core.Statistic;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class StatisticBootstrapper : IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {

                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.StatisticCreate,
                                                          StatisticThread.Instance.JobCreateRecord);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.StatisticOnline,
                                                          StatisticThread.Instance.JobGetOnlineData);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.StatisticDetail,
                                                          StatisticThread.Instance.JobGetDetail);
                //ScheduleManager.Instance.RegisterSchedule(EnumSchedule.StatisticYesterdayKpi,StatisticThread.Instance.JobGetYesterdayKpi);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.StatisticTodayKpi,
                                                          StatisticThread.Instance.JobGetTodayKpi);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.StatisticTotal,
                                                          StatisticThread.Instance.JobGetTotal);
            }
        }
    }
}
