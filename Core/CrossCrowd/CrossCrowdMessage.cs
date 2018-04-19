using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Core.CrossCrowd
{
    public class CrossCrowdMessage
    {
        private static ConcurrentDictionary<int,string> _bannerMsgDic=new ConcurrentDictionary<int, string>(); 

        #region Pop

        public static string BuildCrowdMatch(EnumWinType winType, string awayName, int homeScore, int awayScore)
        {
            switch (winType)
            {
                case EnumWinType.Win:
                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrossCrowdMatchWin);
                    break;
                case EnumWinType.Draw:
                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrossCrowdMatchDraw);
                    break;
                case EnumWinType.Lose:
                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrossCrowdMatchLose);
                    break;
            }
            return "";
        }

        public static string BuildCrowdKill(string killName)
        {
            return string.Format("{1}@N,{0}", killName, (int)EnumPopType.CrossCrowdKill);
        }

        public static string BuildCrowdByKill(string killName)
        {
            return string.Format("{1}@N,{0}", killName, (int)EnumPopType.CrossCrowdByKill);
        }

        public static string BuildCrowdMatchPrize(int crowdScore, int coin, int honor)
        {
            return string.Format("{3}@S,{0}|C,{1}|H,{2}", crowdScore, coin, honor, (int)EnumPopType.CrossCrowdMatchPrize);
        }

        public static string BuildCrowdMoraleUp(int morale)
        {
            return string.Format("{1}@M,{0}", morale, (int)EnumPopType.CrossCrowdMoraleUp);
        }

        public static string BuildCrowdMoraleDown(int morale)
        {
            return string.Format("{1}@M,{0}", morale, (int)EnumPopType.CrossCrowdMoraleDown);
        }

        public static string BuildCrowdKillTogether(string killName)
        {
            return string.Format("{1}@N,{0}", killName, (int)EnumPopType.CrossCrowdKillTogether);
        }

        public static void SendCrowdPop(Guid managerId, string msg)
        {
            MemcachedFactory.CrowdMessageClient.Set(managerId, msg);
        }
        #endregion

        #region Banner

        public static void InitBanner(int domainId)
        {
            _bannerMsgDic.TryAdd(domainId, "");
        }

        public static void ClearBanner(int domainId)
        {
            _bannerMsgDic[domainId] = "";
        }

        public static string GetBanner(int domainId)
        {
            var msg = string.Empty;
            _bannerMsgDic.TryGetValue(domainId, out msg);
            return msg;
        }

        public static string BuildBannerCrowdKill( string managerName, string byName)
        {
            return string.Format("{2}@N,{0}|M,{1}", managerName, byName, (int)EnumBannerType.CrossCrowdKill);
        }

        public static string BuildBannerCrowd3Win(string managerName)
        {
            return string.Format("{1}@N,{0}", managerName, (int)EnumBannerType.CrossCrowd3Win);
        }

        public static string BuildBannerCrowd5Win( string managerName)
        {
            return string.Format("{1}@N,{0}", managerName, (int)EnumBannerType.CrossCrowd5Win);
        }

        public static string BuildBannerCrowdWinOver( string managerName, int homeScore, int awayScore, string awayName)
        {
            return string.Format("{4}@N,{0}|H,{1}|A,{2}|M,{3}", managerName, homeScore, awayScore, awayName, (int)EnumBannerType.CrossCrowdWinOver);
        }

        public static void SendCrowdBanner(int domainId, string msg)
        {
            _bannerMsgDic[domainId]= msg;
        }
        
        #endregion
    }
}
