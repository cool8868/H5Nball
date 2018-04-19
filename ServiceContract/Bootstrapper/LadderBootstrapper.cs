using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Ladder;
//using Games.NBall.Core.Match;
using Games.NBall.Core.Rank;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Bootstrapper
{
    public class LadderBootstrapper : IBootstrapper
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
                CacheFactory.NpcdicCache.GetHashCode();

                LadderThread.Instance.GetHashCode();
                //MatchThread.Instance.GetHashCode();
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.LadderStopHook,
                                                        LadderThread.Instance.StopHookJob);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.LadderCheckStatus,
                                                         LadderThread.Instance.CheckStatusJob);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.LadderScoreToHonorPrize,
                                                         LadderThread.Instance.ScoreToHonorJob);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.TourHook,
                                                         LadderThread.Instance.HookJob);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.LadderRankShow,
                                                         RankLadderThread.Instance.RunJob);
                ScheduleManager.Instance.RegisterSchedule(EnumSchedule.LadderMatchMarquee,
                                                         LadderThread.Instance.GetMatchMarqueeJob);
            }
        }
    }
}
