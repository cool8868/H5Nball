using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Dailycup;
using Games.NBall.Core.Match;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class DailycupBootstrapper : IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.DailycupGambleopen2,
                                                          DailycupAssociation.Instance.OpenGamble);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.DailycupGambleopen4,
                                                          DailycupAssociation.Instance.OpenGamble);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.DailycupGambleopen8,
                                                          DailycupAssociation.Instance.OpenGamble);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.DailycupCreate,
                                                          DailycupAssociation.Instance.CreateDailycup);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.DailycupRun,
                                                          DailycupAssociation.Instance.RunDailycup);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.DailycupSendPrize, DailycupAssociation.Instance.SendPrize);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.CreateProcessTable, MatchCore.JobCreateMatchTable);
            }
        }
    }
}
