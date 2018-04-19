using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Bll.Frame;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response;
//using Games.NBall.Entity.Response.Frame;
using Games.NBall.Entity.Response.Match;

namespace Games.NBall.Core.Match
{
    public class MatchDataCore
    {
        #region .ctor
        public MatchDataCore(int p)
        {
            
        }
        #endregion

        #region Facade

        public static MatchDataCore Instance
        {
            get { return SingletonFactory<MatchDataCore>.SInstance; }
        }

        #region FightInfo

        public Match_FightinfoResponse GetFightInfo(Guid managerId, Guid awayId)
        {
            return GetFightInfoWithoutNpc(managerId, awayId, false, false);
        }
        //public Match_FightinfoResponse GetCrossFightInfo(Guid managerId, string siteId, Guid oppMid, string oppSiteId, int ownSide,bool awayIsBot)
        //{
        //    if (!CrossSiteCache.Instance().CheckSiteId(siteId)
        //        || !CrossSiteCache.Instance().CheckSiteId(oppSiteId))
        //        return ResponseHelper.Create<Match_FightinfoResponse>(MessageCode.InvalidCrossConfig);
        //    if (ownSide > 0)
        //        return GetFightInfo(oppMid, managerId, awayIsBot, false, false, oppSiteId, siteId);
        //    else
        //        return GetFightInfo(managerId, oppMid, false, awayIsBot, false, siteId, oppSiteId);
        //}

        public Match_FightinfoResponse GetLadderFightInfo(Guid matchId,Guid managerId)
        {
            var ladderMatch = MemcachedFactory.LadderMatchClient.Get<LadderMatchEntity>(matchId);
            if (ladderMatch == null)
                return ResponseHelper.InvalidParameter<Match_FightinfoResponse>();
            if (managerId == ladderMatch.HomeId && ladderMatch.HomeIsBot == false)
            {
                return GetFightInfoWithoutNpc(managerId, ladderMatch.AwayId, false, ladderMatch.AwayIsBot);
            }
            else if(managerId == ladderMatch.AwayId && ladderMatch.AwayIsBot==false)
            {
                return GetFightInfoWithoutNpc(managerId, ladderMatch.HomeId, false, ladderMatch.HomeIsBot);
            }
            else
            {
                return ResponseHelper.InvalidParameter<Match_FightinfoResponse>();
            }
        }

        //public Match_FightinfoResponse GetCrowdFightInfo(Guid matchId, Guid managerId)
        //{
        //    var match = MemcachedFactory.CrowdMatchClient.Get<CrowdMatchEntity>(matchId);
        //    if (match == null)
        //        return ResponseHelper.InvalidParameter<Match_FightinfoResponse>();
        //    if (managerId == match.HomeId)
        //    {
        //        return GetFightInfo(managerId, match.AwayId);
        //    }
        //    else if (managerId == match.AwayId)
        //    {
        //        return GetFightInfo(managerId, match.HomeId);
        //    }
        //    else
        //    {
        //        return ResponseHelper.InvalidParameter<Match_FightinfoResponse>();
        //    }
        //}

        /// <summary>
        /// 获取竞技场对阵
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public Match_FightinfoResponse GetArenaFightInfo(Guid matchId)
        {
           var matchData = MemcachedFactory.ArenaMatchClient.Get<BaseMatchData>(matchId);
            if(matchData==null)
                return ResponseHelper.Create<Match_FightinfoResponse>(MessageCode.NbParameterError);
            DicNpcEntity homeNpc = null;
            DicNpcEntity awayNpc = null;
            ArenaTeammemberFrame homeFrame = null;
            ArenaTeammemberFrame awayFrame = null;
            if (matchData.Home.IsNpc)
                homeNpc = CacheFactory.NpcdicCache.GetNpc(matchData.Home.ManagerId);
            else
                homeFrame = new ArenaTeammemberFrame(matchData.Home.ManagerId, (EnumArenaType)matchData.Home.ArenaType, matchData.Home.ZoneName);
            if (matchData.Away.IsNpc)
                awayNpc = CacheFactory.NpcdicCache.GetNpc(matchData.Away.ManagerId);
            else
                awayFrame = new ArenaTeammemberFrame(matchData.Away.ManagerId, (EnumArenaType)matchData.Away.ArenaType, matchData.Away.ZoneName);

            DTOBuffMemberView homeView, awayView;
            ArenaBuffDataCore.Instance().GetMembers(out homeView, out awayView, matchData.Home.ZoneName, matchData.Home.ManagerId,
                    matchData.Home.IsNpc, matchData.Away.ZoneName, matchData.Away.ManagerId, homeFrame, awayFrame,
                    matchData.Away.IsNpc, true, false);

            Match_FightManagerinfo home, away;
            if (matchData.Home.IsNpc)
                home = MatchDataHelper.GetFightinfo(homeNpc, homeView);
            else
                home = MatchDataHelper.GetFightinfoArena(homeFrame, false, homeView,matchData.Home.ZoneName);
            if (matchData.Away.IsNpc)
                away = MatchDataHelper.GetFightinfo(awayNpc, awayView);
            else
                away = MatchDataHelper.GetFightinfoArena(awayFrame, false, awayView, matchData.Away.ZoneName);
            if (home == null || away == null)
                return ResponseHelper.Create<Match_FightinfoResponse>(MessageCode.NbParameterError);
            var response = ResponseHelper.CreateSuccess<Match_FightinfoResponse>();
            response.Data = new Match_Fightinfo();
            response.Data.Home = home;
            response.Data.Away = away;
            return response;
        }

        public Match_FightinfoResponse GetLeagueFightInfo(Guid matchId, Guid managerId)
        {
            LeagueEncounterEntity matchInfo = LeagueEncounterMgr.GetById(matchId);

            DicNpcEntity homeNpc = null; 
            DicNpcEntity awayNpc = null; 
            if(matchInfo.HomeIsNpc)
                homeNpc = CacheFactory.NpcdicCache.GetNpc(matchInfo.HomeId);
            if(matchInfo.AwayIsNpc)
                awayNpc = CacheFactory.NpcdicCache.GetNpc(matchInfo.AwayId);

            DTOBuffMemberView homeView, awayView;
            BuffDataCore.Instance().GetMembers(out homeView, out awayView,
                "", matchInfo.HomeId, matchInfo.HomeIsNpc, "", matchInfo.AwayId, matchInfo.AwayIsNpc, true, false);

            Match_FightManagerinfo home, away;
            if (matchInfo.HomeIsNpc)
                home = MatchDataHelper.GetFightinfo(homeNpc, homeView);
            else
                home = MatchDataHelper.GetFightinfo(matchInfo.HomeId, false, homeView, "");
            if (matchInfo.AwayIsNpc)
                away = MatchDataHelper.GetFightinfo(awayNpc, awayView);
            else
                away = MatchDataHelper.GetFightinfo(matchInfo.AwayId, false, awayView, "");
            if (home == null || away == null)
                return ResponseHelper.Create<Match_FightinfoResponse>(MessageCode.NbParameterError);

            var response = ResponseHelper.CreateSuccess<Match_FightinfoResponse>();
            response.Data = new Match_Fightinfo();
            response.Data.Home = home;
            response.Data.Away = away;
            return response;
        }

        public Match_FightinfoResponse GetFightInfoWithoutNpc(Guid managerId, Guid awayId, bool isHomeBot, bool isAwayBot, bool syncAwayFlag = false, string homeSiteId = "", string awaySiteId = "")
        {
           
            DTOBuffMemberView homeView, awayView;
            BuffDataCore.Instance().GetMembers(out homeView, out awayView,
                homeSiteId, managerId, false, awaySiteId, awayId, false, true, syncAwayFlag);
            Match_FightManagerinfo home = MatchDataHelper.GetFightinfo(managerId, isHomeBot, homeView, homeSiteId);
            Match_FightManagerinfo away = MatchDataHelper.GetFightinfo(awayId, isAwayBot, awayView, awaySiteId);
            if (home == null || away == null)
                return ResponseHelper.Create<Match_FightinfoResponse>(MessageCode.NbParameterError);

            var response = ResponseHelper.CreateSuccess<Match_FightinfoResponse>();
            response.Data = new Match_Fightinfo();
            response.Data.Home = home;
            response.Data.Away = away;
            return response;
        }


        //public Match_FightinfoResponse GetFightInfo(Guid managerId, Guid awayId, bool isHomeBot, bool isAwayBot, bool syncAwayFlag = false, string homeSiteId = "", string awaySiteId = "")
        //{
        //    DicNpcEntity homeNpc = null;
        //    DicNpcEntity awayNpc = null;
        //    if (string.IsNullOrEmpty(homeSiteId))
        //    {
        //        homeNpc = CacheFactory.NpcdicCache.GetNpc(managerId);
        //        awayNpc = CacheFactory.NpcdicCache.GetNpc(awayId);
        //    }
        //    bool isHomeNpc = null != homeNpc;
        //    bool isAwayNpc = null != awayNpc;
        //    DTOBuffMemberView homeView, awayView;
        //    BuffDataCore.Instance().GetMembers(out homeView, out awayView,
        //        homeSiteId, managerId, isHomeNpc, awaySiteId, awayId, isAwayNpc, true, syncAwayFlag);
        //    Match_FightManagerinfo home, away;
        //    if (isHomeNpc)
        //        home = MatchDataHelper.GetFightinfo(homeNpc, homeView);
        //    else
        //        home = MatchDataHelper.GetFightinfo(managerId, isHomeBot, homeView, homeSiteId);
        //    if (isAwayNpc)
        //        away = MatchDataHelper.GetFightinfo(awayNpc, awayView);
        //    else
        //        away = MatchDataHelper.GetFightinfo(awayId, isAwayBot, awayView, awaySiteId);
        //    if (home == null || away == null)
        //        return ResponseHelper.Create<Match_FightinfoResponse>(MessageCode.NbParameterError);

        //    var response = ResponseHelper.CreateSuccess<Match_FightinfoResponse>();
        //    response.Data = new Match_Fightinfo();
        //    response.Data.Home = home;
        //    response.Data.Away = away;
        //    return response;
        //}

        //public Match_FightManagerinfo GetFightManagerFightInfo(Guid managerId, bool isBot = false)
        //{
        //    if (CacheFactory.NpcdicCache.IsNpc(managerId))
        //    {
        //        return CacheFactory.NpcdicCache.GetFightManagerinfo(managerId);
        //    }
        //    else
        //    {
        //        return MatchDataHelper.GetFightinfo(managerId, isBot);
        //    }
        //}
        #endregion

        #region GetProcess
        public MatchProcessResponse GetCrossMatchProcess(Guid matchId, int matchType)
        {
            var baseData = MemcachedFactory.MatchClient.Get<BaseMatchData>(matchId);
            if (baseData != null)
            {
                if (baseData.ErrorCode != (int)MessageCode.Success)
                    return ResponseHelper.Create<MatchProcessResponse>(baseData.ErrorCode);
            }
            var process = MemcachedFactory.MatchProcessClient.Get<byte[]>(matchId);
            if (null == process)
            {
                var dateChar = ShareUtil.GetDateFromComb(matchId);
                var match = MatchprocessMgr.GetByMatchId(dateChar, matchType, matchId);
                if (null != match)
                    process = match.Process;
            }
            if (null == process)
                return ResponseHelper.Create<MatchProcessResponse>(MessageCode.MatchMiss);
            var response = ResponseHelper.CreateSuccess<MatchProcessResponse>();
            response.Data = process;
            return response;
        }
        public byte[] GetMatchProcess(Guid matchId,int matchType)
        {
            try
            {
                var process = GetProcess(matchId, matchType);
                if (process == null)
                    return null;
                else
                {
                    return process.Process;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("MatchDataCore-GetMatchProcess", ex);
                return null;
            }
        }
        #endregion

        #region SaveProcess
        public void SaveProcess(Guid matchId, int matchType, byte[] process, DateTime rowTime)
        {
            string dateChar  = "";
            dateChar = ShareUtil.GetDateFromComb(matchId);
            //var todayChar = rowTime.ToString("yyyyMMdd");
            //if (todayChar != dateChar)
            //    dateChar = todayChar;

            MatchprocessMgr.Save(matchType, process, rowTime, dateChar, matchId);
        }
        #endregion

        #region MatchTest

        public MatchCreateResponse MatchTest(Guid managerId)
        {
            return null;
            //var result = MatchCore.Play(managerId, 1);
            //var response = ResponseHelper.Create<MatchCreateResponse>(result.ErrorCode);
            //if (result.ErrorCode == 0)
            //{
            //    response.Data = result.MatchId;
            //}
            //return response;
        }

        #endregion

        #endregion

        #region encapsulation

        MatchprocessEntity GetProcess(Guid matchId, int matchType)
        {
            string dateChar = "";
            dateChar = ShareUtil.GetDateFromComb(matchId);

            //return MatchprocessMgr.GetByMatchId("20140402", (int)EnumMatchType.Tour, new Guid("D2184144-8A13-45D2-BC50-A30D000567AC"));
            return MatchprocessMgr.GetByMatchId(dateChar, matchType, matchId);
        }
        #endregion
    }
}
