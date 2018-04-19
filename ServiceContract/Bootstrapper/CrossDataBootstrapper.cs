using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Activity;
using Games.NBall.Core.CrossCrowd;
using Games.NBall.Core.CrossLadder;
using Games.NBall.Core.Match;
using Games.NBall.Core.Rank;
using Games.NBall.Core.Robot;
using Games.NBall.Core.UerCrossPara;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.ServiceEngine;
using Games.NBall.Bll;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class CrossDataBootstrapper : IBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                CacheFactory.AppsettingCache.GetHashCode();
                CacheFactory.PlayersdicCache.GetHashCode();
                CacheFactory.ItemsdicCache.GetHashCode();
                MatchThread.Instance.GetHashCode();

                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.CrossScoreToHonorPrize,
                                                         CrossLadderManager.Instance.ScoreToHonorJob);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.CrossMatchMarquee,
                                                        CrossLadderManager.Instance.GetMatchMarqueeJob);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.CreateProcessTable2, MatchCore.JobCrossCreateMatchTable);

                CrossCrowdManager.Instance.GetHashCode();
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.CrossRankShow, CrossRankThread.Instance.RunJob);

                DailyEventThread.Instance.RegisterEvent(EnumDailyevent.CrossCrowd, CrossCrowdManager.Instance.StartJob);

                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.CrossCrowdPrize, CrossCrowdThread.SendPrize);

                
                CacheFactory.NpcdicCache.GetHashCode();



                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.ArenaDayRefresh, ArenaCore.Instance.DayRefresh);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.ArenaDaySendPrize, ArenaCore.Instance.SendPrize);

                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.LadderStopHook,
                                                        CrossLadderManager.Instance.StopHookJob);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.TourHook,
                                                         CrossLadderManager.Instance.HookJob);

                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.TransferRefreshStatus,
                                                         Games.NBall.Core.Transfer.TransferCore.Instance.RefreshStatus);


                //ScheduleManager.Instance.RegisterSchedule(EnumSchedule.RefreshLegendCode, RefreshLegendCode);

                RobotCore.Instance.GetHashCode();

                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.CrossActivity, CrossActivityCore.Instance.Refresh);

            }
        }

        private Games.NBall.Entity.Enums.MessageCode RefreshLegendCode()
        {
            DateTime date = DateTime.Now.AddDays(1);
            var count = ConfigEverydayactivitylegendMgr.GetByTime(date.Date);
            if (count == 0)
            {
                int legendCode = 0;
                int legendDebrisCode = 0;
                int index = 50;
                var top5List = ConfigEverydayactivitylegendMgr.GetTop5(date.AddDays(-5), date);
                if (top5List != null && top5List.Count > 0)
                {
                    do
                    {
                        var legendLottery = CacheFactory.AppsettingCache.GetAppSettingToInt(
                            Games.NBall.Entity.Enums.EnumAppsetting.RandomLegendDebrisCode, 199);
                        legendCode = CacheFactory.LotteryCache.LotteryByLib(legendLottery);
                        if (legendCode>0 && top5List.Find(r => r.LegendCode == legendCode) == null)
                            break;
                        index--;
                    } while (index > 0);
                }
                if (legendCode > 0)
                {
                    legendDebrisCode = CacheFactory.ItemsdicCache.LotteryTheContractId(legendCode % 100000);
                    if (legendDebrisCode == 0)
                        return Entity.Enums.MessageCode.NbParameterError;
                }

                ConfigEverydayactivitylegendEntity entity = new ConfigEverydayactivitylegendEntity(date.Date,
                    legendCode, legendDebrisCode);
                if (!ConfigEverydayactivitylegendMgr.Insert(entity))
                    return Entity.Enums.MessageCode.NbUpdateFail;
            }
            return Entity.Enums.MessageCode.Success;
        }
    }
}
