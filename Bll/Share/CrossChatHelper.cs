//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Games.NBall.ChattingFacade;
//using Games.NBall.Entity.Enums;
//using Games.NBall.Entity.Response.Crowd;

//namespace Games.NBall.Bll.Share
//{
//    public class CrossChatHelper
//    {
//        static readonly ChattingProxy _proxy = new ChattingProxy();
//        static readonly NBThreadPool _threadPool = new NBThreadPool(10);

//        #region Pop

//        public static string BuildCrowdMatch(EnumWinType winType, string awayName, int homeScore, int awayScore)
//        {
//            switch (winType)
//            {
//                case EnumWinType.Win:
//                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrossCrowdMatchWin);
//                    break;
//                case EnumWinType.Draw:
//                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrossCrowdMatchDraw);
//                    break;
//                case EnumWinType.Lose:
//                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrossCrowdMatchLose);
//                    break;
//            }
//            return "";
//        }

//        public static void SendCrowdPop(Guid managerId, string msg,string zoneName)
//        {
//            SendPop(managerId, EnumPopType.CrossCrowdPop, msg, zoneName);
//        }

//        public static void SendCrowdPairPop(Guid managerId, CrowdHeartEntity heartEntity, string zoneName)
//        {
//            SendPop(managerId, EnumPopType.CrossCrowdPair, string.Format("{0}|{1}|{2}|{3}|{4}|{5}", heartEntity.MatchId, heartEntity.AwayName, heartEntity.AwayMorale, heartEntity.AwayLogo,heartEntity.AwayId,heartEntity.AwaySiteId),zoneName);
//        }

//        public static string BuildCrowdKill(string killName)
//        {
//            return string.Format("{1}@N,{0}", killName, (int)EnumPopType.CrossCrowdKill);
//        }

//        public static string BuildCrowdByKill(string killName)
//        {
//            return string.Format("{1}@N,{0}", killName, (int)EnumPopType.CrossCrowdByKill);
//        }

//        public static string BuildCrowdMatchPrize(int crowdScore, int coin, int honor)
//        {
//            return string.Format("{3}@S,{0}|C,{1}|H,{2}", crowdScore, coin, honor, (int)EnumPopType.CrossCrowdMatchPrize);
//        }

//        public static string BuildCrowdMoraleUp(int morale)
//        {
//            return string.Format("{1}@M,{0}", morale, (int)EnumPopType.CrossCrowdMoraleUp);
//        }

//        public static string BuildCrowdMoraleDown(int morale)
//        {
//            return string.Format("{1}@M,{0}", morale, (int)EnumPopType.CrossCrowdMoraleDown);
//        }

//        public static string BuildCrowdKillTogether(string killName)
//        {
//            return string.Format("{1}@N,{0}", killName, (int)EnumPopType.CrossCrowdKillTogether);
//        }

//        #region 巅峰之战


//        public static void SendPeakPop(Guid managerId, string msg, string zoneName)
//        {
//            SendPop(managerId, EnumPopType.CrossPeak, msg, zoneName);
//        }

//        /// <summary>
//        /// 巅峰之战挑战NPC推送
//        /// </summary>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <param name="integral"></param>
//        /// <param name="coin"></param>
//        /// <param name="reiki"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakPve(int homeGoals, int awayGoals, int integral, int coin, int reiki)
//        {
//            return string.Format("{0}@H,{1}|A,{2}|I,{3}|C,{4}|L,{5}", (int)EnumPopType.CrossPeakPve, homeGoals, awayGoals, integral, coin, reiki);
//        }

//        /// <summary>
//        /// 巅峰之战被挑战失败推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <param name="portName"></param>
//        /// <param name="managerId"></param>
//        /// <param name="siteId"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakByChallengeLose(string managerName, int homeGoals, int awayGoals, string portName, Guid managerId, string siteId)
//        {
//            return string.Format("{0}@M,{1}|H,{2}|A,{3}|K,{4}|C,{5}|S,{6}", (int)EnumPopType.CrossPeakByChallengeLose, managerName, homeGoals, awayGoals, portName, managerId, siteId);
//        }

//        /// <summary>
//        /// 巅峰之战被挑战胜利推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <param name="portName"></param>
//        /// <param name="managerId"></param>
//        /// <param name="siteId"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakByChallengeWin(string managerName, int homeGoals, int awayGoals, string portName, Guid managerId, string siteId)
//        {
//            return string.Format("{0}@M,{1}|H,{2}|A,{3}|K,{4}|C,{5}|S,{6}", (int)EnumPopType.CrossPeakByChallengeWin, managerName, homeGoals, awayGoals, portName, managerId, siteId);
//        }

//        /// <summary>
//        /// 巅峰之战主队挑战失败推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakChallengeLose(string managerName, int homeGoals, int awayGoals)
//        {
//            return string.Format("{0}@M,{1}|H,{2}|A,{3}", (int)EnumPopType.CrossPeakChallengeLose, managerName, homeGoals, awayGoals);
//        }

//        /// <summary>
//        /// 巅峰之战主队挑战成功推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakChallengeWin(string managerName, int homeGoals, int awayGoals)
//        {
//            return string.Format("{0}@M,{1}|H,{2}|A,{3}", (int)EnumPopType.CrossPeakChallengeWin, managerName, homeGoals, awayGoals);
//        }

//        /// <summary>
//        /// 巅峰之战击杀推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakKill(string managerName)
//        {
//            return string.Format("{0}@M,{1}", (int)EnumPopType.CrossPeakKill, managerName);
//        }

//        /// <summary>
//        /// 巅峰之战被击杀推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="portName"></param>
//        /// <param name="managerId"></param>
//        /// <param name="siteId"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakByKill(string managerName, string portName, Guid managerId, string siteId)
//        {
//            return string.Format("{0}@M,{1}|K,{2}|C,{3}|S,{4}", (int)EnumPopType.CrossPeakByKill, managerName, portName, managerId, siteId);
//        }

//        /// <summary>
//        /// 巅峰之战玉石俱焚推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakCrashAndBurn(string managerName)
//        {
//            return string.Format("{0}@M,{1}", (int)EnumPopType.CrossPeakCrashAndBurn, managerName);
//        }

//        /// <summary>
//        /// 巅峰之战绝杀推送
//        /// </summary>
//        /// <param name="itemName"></param>
//        /// <returns></returns>
//        public static string BuildCrossPeakFinalHit(string itemName)
//        {
//            return string.Format("{0}@ItemName,{1}", (int)EnumPopType.CrossPeakFinalHit,itemName);
//        }

//        #endregion

//        private static void SendPop(Guid managerId, EnumPopType popType, string content, string zoneName)
//        {
//            _threadPool.Add(() => doSendPop(managerId, popType, content,zoneName));
//        }

//        public static void doSendPop(Guid managerId, EnumPopType popType, string content,string zoneName)
//        {
//            try
//            {
//                _proxy.SendPopChannelMessage(managerId, (int)popType, content,zoneName);
//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("doSendPop", ex);
//            }
//        }


//        #endregion

//        #region Banner

//        public static void SendCrowdBanner(int domainId,string msg)
//        {
//            SendBanner(domainId,Guid.NewGuid(), EnumBannerType.CrossCrowdBanner, msg);
//        }

//        public static void SendBannerCrowdEnd(int domainId)
//        {
//            SendBanner(domainId,Guid.NewGuid(), EnumBannerType.CrossCrowdEnd, "");
//        }

//        public static void SendBannerCrowdOpen(int domainId)
//        {
//            SendBanner(domainId,Guid.NewGuid(), EnumBannerType.CrossCrowdOpen, "");
//        }

//        public static string BuildBannerCrowdKill(string managerName, string byName)
//        {
//            return string.Format("{2}@N,{0}|M,{1}", managerName, byName, (int)EnumBannerType.CrossCrowdKill);
//        }

//        public static string BuildBannerCrowd3Win(string managerName)
//        {
//            return string.Format("{1}@N,{0}", managerName, (int)EnumBannerType.CrossCrowd3Win);
//        }

//        public static string BuildBannerCrowd5Win(string managerName)
//        {
//            return string.Format("{1}@N,{0}", managerName, (int)EnumBannerType.CrossCrowd5Win);
//        }

//        public static string BuildBannerCrowdWinOver(string managerName, int homeScore, int awayScore, string awayName)
//        {
//            return string.Format("{4}@N,{0}|H,{1}|A,{2}|M,{3}", managerName, homeScore, awayScore, awayName, (int)EnumBannerType.CrossCrowdWinOver);
//        }


//        #region 巅峰之战

//        /// <summary>
//        /// 巅峰之战消息
//        /// </summary>
//        /// <param name="domainId"></param>
//        /// <param name="msg"></param>
//        public static void BuildPeak(int domainId, string msg)
//        {
//            SendBanner(domainId, Guid.NewGuid(), EnumBannerType.CrossPeak, msg);
//        }

//        /// <summary>
//        /// 巅峰之战活动开启-推送
//        /// </summary>
//        /// <returns></returns>
//        public static void BuildPeakStart(int domainId)
//        {
//            SendBanner(domainId, Guid.NewGuid(), EnumBannerType.CrossPeakStart, "");
//        }

//        /// <summary>
//        /// 巅峰之战活动结束 -推送
//        /// </summary>
//        /// <param name="domainId"></param>
//        public static void BuildPeakEnd(int domainId)
//        {
//            SendBanner(domainId, Guid.NewGuid(), EnumBannerType.CrossPeakEnd, "");
//        }

//        /// <summary>
//        /// 巅峰之战连续杀人推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="killCount"></param>
//        /// <param name="portName"></param>
//        /// <param name="managerId"></param>
//        /// <param name="siteId"></param>
//        /// <returns></returns>
//        public static string BuildBannerCrossPeakJoinKill(string managerName,int killCount,string portName,Guid managerId,string siteId)
//        {
//            return string.Format("{0}@M,{1}|N,{2}|K,{3}|C,{4}|S,{5}", (int)EnumBannerType.CrossPeakJoinKill, managerName, killCount, portName, managerId, siteId);
//        }

//        /// <summary>
//        /// 巅峰之战绝杀推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="itemName"></param>
//        /// <returns></returns>
//        public static string BuildBannerCrossPeakFinalHit(string managerName,string itemName)
//        {
//            return string.Format("{0}@M,{1}|ItemName,{2}", (int)EnumBannerType.CrossPeakFinalHit, managerName,itemName);
//        }

//        /// <summary>
//        /// 巅峰之战世界奖励推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="point"></param>
//        /// <param name="itemName"></param>
//        /// <returns></returns>
//        public static string BuildBannerCrossPeakWoldPrize(string managerName,int point, string itemName)
//        {
//            return string.Format("{0}@M,{1}|P,{2}|ItemName,{3}", (int)EnumBannerType.CrossPeakWoldPrize, managerName,point, itemName);
//        }

//        /// <summary>
//        /// 巅峰之战经理信息推送
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="siteId"></param>
//        /// <param name="bossHp"></param>
//        /// <param name="managerHp"></param>
//        /// <param name="bossIntegral"></param>
//        /// <param name="nameIsRed"></param>
//        /// <param name="states"></param>
//        /// <param name="challengeCd"></param>
//        /// <param name="resurgenceTime"></param>
//        /// <param name="jobTime"></param>
//        /// <returns></returns>
//        public static string BuildBannerPeakManagerInfo(Guid managerId, string siteId, int bossHp, int managerHp, int bossIntegral, bool nameIsRed, int states, long challengeCd, long resurgenceTime, long jobTime)
//        {
//            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",(int)EnumBannerType.CrossPeakManagerInfo, managerId, siteId, bossHp, managerHp, bossIntegral, nameIsRed, states, challengeCd, resurgenceTime, jobTime);
//        }

//        #endregion


//        private static void SendBanner(int domainId,Guid managerId, EnumBannerType bannerType, string content)
//        {
//            _threadPool.Add(() => doSendBanner(domainId,managerId, bannerType, content));
//        }

//        public static void doSendBanner(int domainId, Guid managerId, EnumBannerType bannerType, string content)
//        {
//            try
//            {
//                _proxy.SendBannerChannelMessage(managerId, (int)bannerType, content, "All."+domainId);
//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("doSendBanner", ex);
//            }
//        }
//        #endregion
//    }
//}
