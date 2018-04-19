using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.MatchFacade;

namespace Games.NBall.Core.Match
{
    public class MatchCore
    {
        public static BaseMatchData CreateMatch(EnumMatchType matchType, Guid homeId, Guid awayId)
        {
            var matchId = ShareUtil.GenerateComb();
            var stateObj = new BaseMatchData((int)matchType, matchId, homeId, awayId);

            MatchThread.Instance.CreateMatch(stateObj);
            return stateObj;
        }

        public static void CreateMatch(BaseMatchData stateObj)
        {
            MatchThread.Instance.CreateMatch(stateObj);
        }

        public static MessageCode CreateMatchAsyn(EnumMatchType matchType,Guid matchId, Guid homeId, Guid awayId, MatchThread.MatchCallback callback)
        {
            var stateObj = new BaseMatchData((int)matchType, matchId, homeId, awayId);
            return CreateMatchAsyn(stateObj, callback);
        }

        public static MessageCode CreateMatchAsyn(BaseMatchData matchData, MatchThread.MatchCallback callback)
        {
            return MatchThread.Instance.CreateMatchAsyn(matchData, callback);
        }
        public static MessageCode CreateMatchAsyn(BaseMatchData matchData, MatchThread.MatchStateCallback callback, object matchState = null)
        {
            return MatchThread.Instance.CreateMatchAsyn(matchData, callback, matchState);
        }

        //public static MessageCode CreateMatchTourAsyn(Guid matchId, Guid homeId, int stageId, bool hasTask, MatchThread.MatchCallback callback, bool isElite = false, int guideTaskRecordId = 0)// bool isMopUp,
        //{
        //    var stateObj = new TourMatchData(matchId, homeId, Guid.Empty, stageId);
        //    stateObj.GuideTaskRecordId = guideTaskRecordId;
        //    stateObj.HasTask = hasTask;
        //    if (isElite)
        //    {
        //        var elite = CacheFactory.TourdicCache.GetTourElite(stageId);
        //        if (elite == null)
        //        {
        //            return MessageCode.TourStageNotExists;
        //        }
        //        else
        //        {
        //            stateObj.Away.ManagerId = elite.NpcId;
        //            stateObj.MatchType = (int)EnumMatchType.TourElite;
        //        }
        //    }
        //    else
        //    {
        //        var stage = CacheFactory.TourdicCache.GetStage(stageId);
        //        if (stage == null)
        //        {
        //            return MessageCode.TourStageNotExists;
        //        }
        //        else
        //        {
        //            if (guideTaskRecordId > 0)
        //                stateObj.IsGuide = true;
        //            stateObj.Away.ManagerId = stage.NpcId;
        //        }
        //    }
        //    return CreateMatchAsyn(stateObj, callback);
        //}

        //public static MessageCode CreateMatchChallengeAsyn(Guid matchId, Guid homeId,int buff,WorldchallengeRecordEntity recordEntity, bool hasTask, MatchThread.MatchCallback callback)
        //{
        //    var stateObj = new WChallengeMatchData(matchId, homeId, recordEntity.NpcId);
        //    stateObj.HasTask = hasTask;
        //    stateObj.WorldchallengeRecord = recordEntity;
        //    stateObj.Home.BuffScale = buff;
        //    return CreateMatchAsyn(stateObj, callback);
        //}

        public static MessageCode CreateMatchFriendAsyn(Guid matchId, Guid managerId, Guid awayId, FriendManagerEntity friendManager, MatchThread.MatchCallback callback)
        {
            var stateObj = new FriendMatchData(matchId, managerId, awayId);
            stateObj.HasTask = false;
            stateObj.FriendRecord = friendManager;
            return CreateMatchAsyn(stateObj, callback);
        }

        public static MessageCode CreateMatchPkAsyn(Guid matchId, Guid managerId, Guid awayId, long revengeRecordId, int dayWinTimes, MatchThread.MatchCallback callback,bool hasTask)
        {
            var stateObj = new PkMatchData(matchId, managerId, awayId,revengeRecordId,dayWinTimes);
            stateObj.HasTask = hasTask;
            return CreateMatchAsyn(stateObj, callback);
        }

        #region SaveMatchStat
        public static void SaveMatchStat(Guid managerId, EnumMatchType matchType, int homeScore, int awayScore, int goals, string siteId = "")
        {
            SaveMatchStat(managerId, (int)matchType, homeScore, awayScore, goals, siteId);
        }

        public static void SaveMatchStat(Guid managerId, int matchType, int homeScore, int awayScore, int goals, string siteId)
        {
            int win = 0;
            int lose = 0;
            int draw = 0;
            if (homeScore > awayScore)   
                win++;
            else if (homeScore == awayScore)
                draw++;
            else
            {
                lose++;
            }
            SaveMatchStat(managerId, matchType, win, lose, draw, goals,siteId);
        }

        public static void SaveMatchStat(Guid managerId, EnumMatchType matchType, int win, int lose, int draw, int goals, string siteId = "")
        {
            SaveMatchStat(managerId, (int)matchType, win, lose, draw, goals, siteId);
        }

        public static void SaveMatchStat(Guid managerId, int matchType, int win, int lose,int draw,int goals, string siteId="")
        {
            try
            {
                NbMatchstatMgr.Save(managerId, matchType, win, lose, draw, goals, null, siteId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveMatchStat", ex);
            }
        }
        #endregion

        public static MessageCode JobCreateMatchTable()
        {
            try
            {
                MatchprocessMgr.Job_CreateProcessTable();
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("JobCreateMatchTable", ex);
                return MessageCode.Exception;
            }
        }

        public static MessageCode JobCrossCreateMatchTable()
        {
            try
            {
                MatchprocessMgr.Job_CrossCreateProcessTable();
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("JobCrossCreateMatchTable", ex);
                return MessageCode.Exception;
            }
        }
    }
}
