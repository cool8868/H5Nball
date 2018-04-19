///报名参加联赛，包括赛程中加入的接口
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Constellation;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response;
using MsEntLibWrapper.Data;
using ManagerUtil = Games.NBall.Core.Manager.ManagerUtil;

namespace Games.NBall.Core.League
{
    public class LeagueProcess
    {
        private static LeagueProcess instance;
        private LeagueProcess() { }
        public static LeagueProcess Instance 
　　    { 
　　        get 
　　        { 
　　            if (instance == null)
                  instance = new LeagueProcess(); 
　　            return instance; 
　　        }
　　    }
        

        #region 开启一个联赛

        /// <summary>
        /// 开启一个新的联赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <param name="leagueManagerRecord"></param>
        /// <returns></returns>
        public MessageCode StartLeague(Guid managerId, int leagueId,ref LeagueManagerrecordEntity leagueManagerRecord)
        {
            var recordList = LeagueManagerrecordMgr.GetManagerAllMark(managerId);
            foreach (var record in recordList)
            {
                if (record.LaegueId == leagueId)
                    leagueManagerRecord = record;
                if (record.IsStart)
                    return MessageCode.LeagueHasStart;
            }
            if (leagueManagerRecord == null)
                return MessageCode.NbParameterError;
            if (leagueManagerRecord.IsLock)
                return MessageCode.LeagueIdMarkNotLock;
            #region 去掉等级限制
            //var openLevel= LeagueCache.Instance.GetLeagueOpenLevel(leagueId);
            //if (openLevel == -1)
            //    return MessageCode.NbParameterError;
            //var manager = ManagerCore.Instance.GetManager(managerId);
            //if(manager==null)
            //    return MessageCode.NbParameterError;
            //if(manager.Level<openLevel)
            //    return MessageCode.LeagueIdMarkNotLock;
            #endregion
            DateTime date = DateTime.Now;
            var leagueRecordId = ShareUtil.GenerateComb();
            leagueManagerRecord.LeagueRecordId = leagueRecordId;
            //info.LastPrizeLeagueRecordId = new Guid();
            leagueManagerRecord.IsPass = false;
            leagueManagerRecord.IsStart = true;
            leagueManagerRecord.MatchNumber = 0;
            leagueManagerRecord.Score = 0;
            leagueManagerRecord.WinNumber = 0;
            leagueManagerRecord.FlatNumber = 0;
            leagueManagerRecord.LoseNumber = 0;
            leagueManagerRecord.GoalsNumber = 0;
            leagueManagerRecord.FumbleNumber = 0;
            leagueManagerRecord.UpdateTime = date;

            var npcCount = CacheFactory.LeagueCache.GetTeamCount(leagueId);
            var templateId = CacheFactory.LeagueCache.GetTemplateId(npcCount + 1);
            leagueManagerRecord.MaxWheelNumber = npcCount * 2;//总轮数
            leagueManagerRecord.FightDicId = templateId;
           
            LeagueRecordEntity entity = new LeagueRecordEntity(leagueRecordId, managerId, leagueId, 1, 0, 0, false, date, date, ShareUtil.BaseTime);
            entity.Idx = leagueRecordId;
            entity.ManagerId = managerId;
            entity.LaegueId = leagueId;
            entity.IsSend = false;
            entity.Rank = -1;
            entity.RowTime = date;
            entity.Schedule = 1;
            entity.UpdateTime = date;
            entity.Score = 0;
            var leagueWinCount = LeagueWincountrecordMgr.GetRecord(managerId, leagueId);
            if (leagueWinCount != null)
            {
                if (leagueWinCount.PrizeDate.Date != DateTime.Now.Date)
                {
                    leagueWinCount.WinCount1Status = 0;
                    leagueWinCount.WinCount2Status = 0;
                    leagueWinCount.WinCount3Status = 0;
                    leagueWinCount.UpdateTime = DateTime.Now;
                    leagueWinCount.PrizeDate = DateTime.Now;
                    leagueWinCount.PrizeStep = "0,0,0,0,0";
                }
                leagueWinCount.MaxWinCount = 0;
            }
            //更新当前联赛记录
            if (!LeagueManagerrecordMgr.Update(leagueManagerRecord))
                return MessageCode.FailUpdate;
            if (!LeagueRecordMgr.Insert(entity))
                return MessageCode.FailUpdate;
            var figMap = new LeagueFightMapFrame(managerId);
            figMap.ClearFightMapStartLeague(npcCount + 1);
            if (leagueWinCount != null)
            {
                if (!LeagueWincountrecordMgr.Update(leagueWinCount))
                    return MessageCode.FailUpdate;
            }
            return MessageCode.Success;
        }

        #endregion


        #region 重置联赛

        public MessageCode ResetLeague(Guid managerId,int leagueId,int point)
        {
            DateTime date = DateTime.Now;
            var managerRecord = LeagueManagerrecordMgr.GetManagerMarkInfo(managerId, leagueId);
            managerRecord.LeagueRecordId = new Guid();//00000000-0000-0000-0000-000000000000
            //managerRecord.LastPrizeLeagueRecordId = new Guid();
            managerRecord.IsPass = false;
            managerRecord.IsStart = false;
            managerRecord.MatchNumber = 0;
            managerRecord.Score = 0;
            managerRecord.WinNumber = 0;
            managerRecord.FlatNumber = 0;
            managerRecord.LoseNumber = 0;
            managerRecord.GoalsNumber = 0;
            managerRecord.FumbleNumber = 0;
            managerRecord.MaxWheelNumber = 0;
            managerRecord.UpdateTime = date;
            managerRecord.FightDicId = 0;
            var fightMap = new LeagueFightMapFrame(managerId);
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                MessageCode messCode = MessageCode.NbUpdateFail;
                do
                {
                    if (point > 0)
                    {
                        messCode = PayCore.Instance.GambleConsume(managerId, 50, ShareUtil.GenerateComb(),
                            EnumConsumeSourceType.LeagueReset, transactionManager.TransactionObject);
                        if (messCode != MessageCode.Success)
                            break;
                    }
                    messCode = fightMap.ClearFightMap(transactionManager.TransactionObject);
                    if (messCode != MessageCode.Success)
                        break;
                    if (!LeagueManagerrecordMgr.Update(managerRecord, transactionManager.TransactionObject))
                        break;
                    messCode = MessageCode.Success;
                } while (false);
                if (messCode != MessageCode.Success)
                {
                    transactionManager.Rollback();
                    return messCode;
                }
                transactionManager.Commit();
            }
            return MessageCode.Success;
        }

        #endregion

        #region 打比赛
        /// <summary>
        ///  联赛打比赛
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LeagueFightResultResponse Fight(Guid managerId, int leagueId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.Create<LeagueFightResultResponse>(MessageCode.NbParameterError);
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            if (managerExtra == null)
                return ResponseHelper.Create<LeagueFightResultResponse>(MessageCode.NbParameterError);
            if(managerExtra.Stamina<5)
                return ResponseHelper.Create<LeagueFightResultResponse>(MessageCode.LeagueStaminaNotEnough);
            var currectLeague = LeagueManagerrecordMgr.GetManagerMarkInfo(managerId, leagueId);
            if (currectLeague == null)
                return ResponseHelper.Create<LeagueFightResultResponse>(MessageCode.NbParameterError);
            var leagueRecordInfo = LeagueRecordMgr.GetById(currectLeague.LeagueRecordId);
            if (leagueRecordInfo == null)
                return ResponseHelper.Create<LeagueFightResultResponse>(MessageCode.NbParameterError);
            if (leagueRecordInfo.Schedule > currectLeague.MaxWheelNumber)
                return ResponseHelper.Create<LeagueFightResultResponse>(MessageCode.LeagueHavePass);
              //扣除行动力
            var code = ManagerCore.Instance.SubStamina(managerExtra, 5, manager.Level,manager.VipLevel);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<LeagueFightResultResponse>(code);
            //遍历对阵
            int botStatus = 0;//0=主队和客队都是NPC 1 我是主队 2 我是客队
            var npchome = Guid.Empty;
            var npcaway = Guid.Empty;
            var matchId = ShareUtil.GenerateComb();
            //获取对阵记录
            var fightMap = new LeagueFightMapFrame(managerId);
            //获取对阵
            var pairList = CreateFightMap(currectLeague.FightDicId, leagueRecordInfo.Schedule);
            BaseMatchData managerMatch = null;
            foreach (var item in pairList)
            {
                botStatus = 0;
                if (item.HomeId == 0)
                {
                    botStatus = 1;
                    npchome = managerId;
                    npcaway = CacheFactory.LeagueCache.GetLeagueNpcGuid(leagueId, item.AwayId);
                }
                else if (item.AwayId == 0)
                {
                    botStatus = 2;
                    npchome = CacheFactory.LeagueCache.GetLeagueNpcGuid(leagueId, item.HomeId);
                    npcaway = managerId;
                }
                else
                {
                    npchome = CacheFactory.LeagueCache.GetLeagueNpcGuid(leagueId, item.HomeId);
                    npcaway = CacheFactory.LeagueCache.GetLeagueNpcGuid(leagueId, item.AwayId);
                }
                SingleMatch(leagueRecordInfo.Schedule, npchome, npcaway, item.HomeId, item.AwayId, leagueRecordInfo.LaegueId, matchId, botStatus, fightMap, ref managerMatch);
            }
            if (managerMatch == null)
                return ResponseHelper.Create<LeagueFightResultResponse>(MessageCode.MatchCreateFail);
            LeagueWincountrecordEntity leagueWincountRecord = null;
           
            
            //记录比赛数据
            if (managerMatch.Away.IsBot)
                MatchCore.SaveMatchStat(managerId, EnumMatchType.League, managerMatch.Home.Score, managerMatch.Away.Score, managerMatch.Home.Score);
            else
                MatchCore.SaveMatchStat(managerId, EnumMatchType.League, managerMatch.Away.Score, managerMatch.Home.Score, managerMatch.Away.Score);
            var response = ResponseHelper.CreateSuccess<LeagueFightResultResponse>();
            response.Data = new LeagueFightResult
            {
                HomeGoals = managerMatch.Home.Score,
                AwayGoals = managerMatch.Away.Score,
                PrizeList = new List<LeaguePrizeEntity>()
            };
            int star = 0;
            if (managerMatch.Home.Score > managerMatch.Away.Score)
            {
                if (managerMatch.Home.ManagerId == managerId)
                {
                    star = managerMatch.Home.Score - managerMatch.Away.Score;
                    response.Data.Result = 0;
                }
                else
                    response.Data.Result = 2;
            }
              
            else if (managerMatch.Home.Score == managerMatch.Away.Score)
                response.Data.Result = 1;
            else
            {
                if (managerMatch.Away.ManagerId == managerId)
                {
                    star = managerMatch.Away.Score - managerMatch.Home.Score;
                    response.Data.Result = 0;
                }
                else
                    response.Data.Result = 2;
            }
            var winType = CalWinType(response.Data.Result);
            LaegueManagerinfoEntity leagueManagerInfo = null;
            if (winType == EnumWinType.Win)
            {
                 leagueManagerInfo = LaegueManagerinfoMgr.GetById(managerId);
                if (leagueManagerInfo.DailyWinUpdateTime.Date != DateTime.Now.Date)
                    leagueManagerInfo.DailyWinCount = 1;
                else
                    leagueManagerInfo.DailyWinCount++;
                leagueManagerInfo.DailyWinUpdateTime = DateTime.Now;
            }

            //更新胜场奖励
            star = star > 3 ? 3 : star;
            star = star < 0 ? 0 : star;
            UpdateWincountRecord(managerId, leagueId, star, ref leagueWincountRecord);
            currectLeague.MatchId = matchId;
            currectLeague.UpdateTime = DateTime.Now;
            var result = MatchConfirm(manager, leagueRecordInfo, currectLeague, leagueManagerInfo, managerMatch, fightMap, managerExtra, leagueWincountRecord);
            if (result.Code != (int) MessageCode.Success)
                return ResponseHelper.Create<LeagueFightResultResponse>(result.Code);
            var pop = TaskHandler.Instance.LeagueFight(managerId, (int)winType);
            if (pop != null)
            {
                MemcachedFactory.MatchPopClient.Set(managerId, pop);
            }
            response.Data.StarNumber = star;
            response.Data.Stamina = managerExtra.Stamina;
            response.Data.PrizeList = result.Data.PrizeList;
            response.Data.VipExp = result.Data.VipExp;
            response.Data.MatchId = managerMatch.MatchId;
            return response;
        }
      
        /// <summary>
        /// 单场比赛
        /// </summary>
        /// <param name="round"></param>
        /// <param name="homeGuid"></param>
        /// <param name="awayGuid"></param>
        /// <param name="homeId"></param>
        /// <param name="awayId"></param>
        /// <param name="leagueId"></param>
        /// <param name="matchId"></param>
        /// <param name="npcStatus"></param>
        /// <param name="fightMap"></param>
        /// <param name="managerMatch"></param>
        public MessageCode SingleMatch(int round,Guid homeGuid,Guid awayGuid,int homeId,int awayId, int leagueId,Guid matchId,int npcStatus,LeagueFightMapFrame fightMap,ref BaseMatchData managerMatch)
        {
            try
            {
                bool homeIsBot = true;
                bool awayIsBot = true;
                switch (npcStatus)
                {
                    case 1:
                        homeIsBot = false;
                        break;
                    case 2:
                        awayIsBot = false;
                        break;
                    default:
                        matchId = ShareUtil.GenerateComb();
                        break;
                }
                //构建主队
                var matchHome = new MatchManagerInfo(homeGuid, homeIsBot, false);
                ////构建客队
                var matchAway = new MatchManagerInfo(awayGuid, awayIsBot, false);

                ////创建一场比赛
                var matchData = new BaseMatchData((int)EnumMatchType.League, matchId, matchHome, matchAway);
                //比赛数据
                matchData.ErrorCode = (int)MessageCode.MatchWait;
                matchData.RowTime = DateTime.Now;
                MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);
                if (npcStatus == 0)
                {
                    matchData.ErrorCode = (int)MessageCode.Success;
                    matchData.Home.Score = CacheFactory.LeagueCache.GetGoalsMap(leagueId, homeId);
                    matchData.Away.Score = CacheFactory.LeagueCache.GetGoalsMap(leagueId, awayId); 
                }
                else
                {
                    MatchCore.CreateMatch(matchData);
                    if (matchData.ErrorCode != (int)MessageCode.Success)
                        return (MessageCode)matchData.ErrorCode;
                    //测试用 ------------
                    //matchData.ErrorCode = (int)MessageCode.Success;
                    //matchData.Home.Score = 5;
                    //matchData.Away.Score = 2;
                    //-------------------
                        managerMatch = matchData;
                }
                int homeGoals = matchData.Home.Score;
                int awayGoals = matchData.Away.Score;
                SaveMatchScore(leagueId, round, homeId, awayId, homeGoals, awayGoals, fightMap, npcStatus);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("League.Match", ex);
                return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 累加赛季比赛数据
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="round"></param>
        /// <param name="homeId"></param>
        /// <param name="awayId"></param>
        /// <param name="homeGoals"></param>
        /// <param name="awayGoals"></param>
        /// <param name="fightMap"></param>
        /// <param name="npcStatus"></param>
        /// <returns></returns>
        public void SaveMatchScore(int leagueId,int round, int homeId, int awayId, int homeGoals, int awayGoals,
            LeagueFightMapFrame fightMap, int npcStatus)
        {
            int homeWinType = 0;
            int awayWinType = 0;
            if (homeGoals > awayGoals)
            {
                homeWinType = 1;
                awayWinType = 3;
            }
            else if (homeGoals == awayGoals)
            {
                homeWinType = 2;
                awayWinType = 2;
            }
            else
            {
                homeWinType = 3;
                awayWinType = 1;
            }
            int homeScore = 0;
            int awayScore = 0;
           
            if (homeGoals > awayGoals)
                homeScore = 3;
            else if (homeGoals < awayGoals)
                awayScore = 3;
            else
            {
                homeScore = 1;
                awayScore = 1;
            }
            //设置对阵记录
            fightMap.SetFightMap(round, homeId, awayId, homeGoals, awayGoals);
            //设置排名
            fightMap.SetRankScore(homeId, homeScore, homeWinType, homeGoals, awayGoals);
            fightMap.SetRankScore(awayId, awayScore, awayWinType, awayGoals, homeGoals);
        }


        /// <summary>
        /// 发放奖励
        /// </summary>
        /// <param name="prizeList"></param>
        /// <param name="manager"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public MessageCode SendPrize(List<ConfigLeagueprizeEntity> prizeList, NbManagerEntity manager, int leagueId,ref int exp,ref int coin,ref int score,ref ItemPackageFrame package,ref int point,ref int vipExp)
        { 
            if (manager == null)
                return MessageCode.NbParameterError;
            foreach (var prize in prizeList)
            {
                switch (prize.PrizeType)
                {
                    case (int)EnumLeaguePrize.Exp://经验
                    {
                        //是否有vip效果
                        var vipRate = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel,
                            EnumVipEffect.PkOrLeagueExpPlus);
                        var totalPlusRate = vipRate / 100.00;
                        var prizeExp = (int)(prize.Count * (1 + totalPlusRate));
                        exp = prizeExp;
                        vipExp = prizeExp - prize.Count;
                        break;
                    }
                    case (int)EnumLeaguePrize.Coin://金币
                        coin = prize.Count;
                        break;
                    case (int)EnumLeaguePrize.Score://联赛积分
                        score = prize.Count;
                        //欧洲杯狂欢
                        ActivityExThread.Instance.EuropeCarnival(3, ref score);
                        break;
                    case (int)EnumLeaguePrize.Item://道具
                        //获取背包
                        if(package == null)
                          package = ItemCore.Instance.GetPackage(manager.Idx, EnumTransactionType.LeaguePrize);
                        if (package == null)
                            return MessageCode.NbNoPackage;
                        var result = package.AddItems(prize.ItemCode, prize.Count, 1, prize.IsBindIng,false);
                        if (result != MessageCode.Success)
                            return result;
                        break;

                    case (int)EnumLeaguePrize.Point://钻石
                        point = prize.Count;
                        break;
                }
            }
            return MessageCode.Success;
        }

        #endregion

        private bool UpdateWincountRecord(Guid managerId, int leagueId, int starNumber, ref LeagueWincountrecordEntity leagueWincountRecord)
        {
            if(leagueWincountRecord ==null)
                leagueWincountRecord = LeagueWincountrecordMgr.GetRecord(managerId, leagueId);
            leagueWincountRecord.MaxWinCount += starNumber;
            var winConfig = CacheFactory.LeagueCache.GetLeagueStar(leagueId);
            var prizeStatus = leagueWincountRecord.PrizeStep.Split(',');
            for (int i = 0; i < winConfig.Count; i++)
            {
                if (prizeStatus.Length < i + 1)
                    prizeStatus[i] = "0";
                if (leagueWincountRecord.MaxWinCount >= winConfig[i].StarNumber)
                {
                    if (prizeStatus[i] == "0")
                        prizeStatus[i] = "1";
                }
            }
            leagueWincountRecord.PrizeStep = string.Join(",", prizeStatus);
            leagueWincountRecord.UpdateTime = DateTime.Now;
            return true;
        }

        #region 确认比赛结果
        /// <summary>
        /// 确认比赛结果
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="leagueRecordInfo"></param>
        /// <returns></returns>
        public LeaguePrizeResponse MatchConfirm(NbManagerEntity manager, LeagueRecordEntity leagueRecordInfo, LeagueManagerrecordEntity leagueManagerRecord, LaegueManagerinfoEntity lagueManagerInfo, BaseMatchData matchData, LeagueFightMapFrame fightMap, NbManagerextraEntity managerExtra,LeagueWincountrecordEntity leagueWinCount)
        {
           
            //发放玩家单场比赛奖励---------------------------------------------------
            int winType = 0;
            if (matchData.Home.ManagerId == manager.Idx) //玩家为主队
            {
                if (matchData.Home.Score > matchData.Away.Score) //主队胜
                    winType = 1;
                else if (matchData.Home.Score == matchData.Away.Score) //平
                    winType = 2;
                else //负
                    winType = 3;
            }
            else //玩家为客队
            {
                if (matchData.Home.Score < matchData.Away.Score)//客队胜
                    winType = 1;
                else if (matchData.Home.Score == matchData.Away.Score) //平
                    winType = 2;
                else//负
                    winType = 3;
            }
            var managerPrizes = LeagueCache.Instance.GetLeaguePrize(leagueRecordInfo.LaegueId,winType);

            int exp = 0;
            int coin = 0;
            int score = 0;
            ItemPackageFrame package = null;
            int point = 0;
            int vipExp = 0;
            SendPrize(managerPrizes, manager, leagueRecordInfo.LaegueId, ref exp, ref coin, ref score, ref package,
                ref point, ref vipExp);
            ManagerUtil.AddManagerData(manager, exp, coin, 0, EnumCoinChargeSourceType.LeaguePrize, ShareUtil.CreateSequential().ToString());

            if(lagueManagerInfo ==null)
                lagueManagerInfo = LaegueManagerinfoMgr.GetById(manager.Idx);
            lagueManagerInfo.SumScore += score;

            leagueManagerRecord.MatchId = new Guid();
            leagueManagerRecord.UpdateTime = DateTime.Now;
            bool isLastWheel = false;
            leagueManagerRecord.Score += score;
            //本联赛最后一轮
            if (leagueRecordInfo.Schedule >= leagueManagerRecord.MaxWheelNumber)
            {
                isLastWheel = true;
                leagueManagerRecord.IsPass = true;
                leagueManagerRecord.PassNumber += 1;
            }
            int myRank = 0;
            int myScore = 0;
            //更新排名
            fightMap.UpdateRankList();
            fightMap.GetRank(ref myRank, ref myScore);

            leagueRecordInfo.Score += score;
            leagueRecordInfo.Rank = myRank;
            //----------------------------
            leagueRecordInfo.Schedule++;
            leagueRecordInfo.IsSend = true;
            leagueRecordInfo.UpdateTime = DateTime.Now;
            //本联赛最后一轮，如果是冠军解锁下一轮
            bool isUpdatenextManagerRecord = false;
            LeagueManagerrecordEntity nextManagerRecord = null;
            if (isLastWheel)
            {
                if (leagueRecordInfo.Rank == 1)
                {
                    leagueManagerRecord.LastPrizeLeagueRecordId = leagueManagerRecord.LeagueRecordId;
                    if (leagueRecordInfo.LaegueId != 8)
                    {
                        nextManagerRecord = LeagueManagerrecordMgr.GetManagerMarkInfo(manager.Idx,
                            leagueRecordInfo.LaegueId + 1);
                        nextManagerRecord.IsLock = false;
                        isUpdatenextManagerRecord = true;
                    }
                    //记录成就相关数据
                    AchievementTaskCore.Instance.UpdateLeagueScore(manager.Idx, leagueRecordInfo.LaegueId,
                        myScore);
                    TaskHandler.Instance.LeagueChampionScore(manager.Idx);
                }
            }
           
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Main)))
            {
                transactionManager.BeginTransaction();
                var messageCode = SaveMatchConfirm(manager, package, leagueRecordInfo,
                    nextManagerRecord, leagueManagerRecord, point, isUpdatenextManagerRecord, lagueManagerInfo, fightMap,managerExtra,leagueWinCount, transactionManager.TransactionObject);
                if (messageCode == MessageCode.Success)
                {
                    transactionManager.Commit();
                }
                else
                {
                    transactionManager.Rollback();
                    return ResponseHelper.Create<LeaguePrizeResponse>(messageCode);
                }
            }
            //奖励结果
            var response = ResponseHelper.CreateSuccess<LeaguePrizeResponse>();
            response.Data = new LeaguePrizes
            {
                PrizeList = new List<LeaguePrizeEntity>()
            };
            foreach (var prize in managerPrizes)
            {
                var entity = new LeaguePrizeEntity();
                entity.PrizeType = prize.PrizeType;
                entity.ItemCode = prize.ItemCode;
                entity.Count = prize.Count;
                response.Data.PrizeList.Add(entity);
            }
            response.Data.VipExp = vipExp;
            return response;
        }

        private MessageCode SaveMatchConfirm(NbManagerEntity manager, ItemPackageFrame package, LeagueRecordEntity leagueRecordInfo,
            LeagueManagerrecordEntity nextManagerRecord, LeagueManagerrecordEntity leagueManagerRecord, int point,
            bool isUpdatenextManagerRecord, LaegueManagerinfoEntity leagueManagerInfo, LeagueFightMapFrame fightMap, NbManagerextraEntity managerExtra, LeagueWincountrecordEntity leagueWinCount, DbTransaction trans)
        {
            if (!ManagerUtil.SaveManagerData(manager, managerExtra,trans))
                return MessageCode.NbUpdateFail;
            if (!NbManagerextraMgr.Update(managerExtra, trans))
                return MessageCode.NbUpdateFail;
            if (package != null)
            {
                if (!package.Save(trans))
                    return MessageCode.NbUpdateFail;
                package.Shadow.Save();
            }
            if (!LeagueRecordMgr.Update(leagueRecordInfo, trans))
                return MessageCode.NbUpdateFail;
            if (isUpdatenextManagerRecord)
            {
                if(nextManagerRecord!= null)
                    if (!LeagueManagerrecordMgr.Update(nextManagerRecord, trans))
                        return MessageCode.NbUpdateFail;
            }
            if (!LaegueManagerinfoMgr.Update(leagueManagerInfo, trans))
                return MessageCode.NbUpdateFail;
            if (!LeagueManagerrecordMgr.Update(leagueManagerRecord, trans))
                return MessageCode.NbUpdateFail;
            if (point > 0)
            {
                var code = PayCore.Instance.AddBonus(manager.Idx, point, EnumChargeSourceType.LeaguePrize,
                    ShareUtil.GenerateComb().ToString(), trans);
                return code;
            }
            if (!fightMap.SaveFIghtMap(trans))
                return MessageCode.NbUpdateFail;
            if (!LeagueWincountrecordMgr.Update(leagueWinCount, trans))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        #endregion

        #region 发放冠军奖励

        public LeagueAllPrizeInfoResponse GetAllPrizeInfo(Guid managerId)
        {
            var leagueInfo = LeagueManagerrecordMgr.GetManagerAllMark(managerId);
            var prizeInfoList = new List<LeagueAllPrizeInfoEntity>();

            foreach (var entity in leagueInfo)
            {
                var prizeInfo = new LeagueAllPrizeInfoEntity();
                prizeInfo.LeagueRecordId = entity.LastPrizeLeagueRecordId;
                prizeInfo.LeagueId = entity.LaegueId;
                prizeInfo.FirstPass = !entity.SendFirstPassPrize;
                //已领取过首次通关奖励
                if (entity.SendFirstPassPrize)
                    prizeInfo.Status = 2;
                else
                {
                    //不能领取的
                    if (entity.LastPrizeLeagueRecordId == new Guid())
                        prizeInfo.Status = 0;
                    else
                        prizeInfo.Status = 1;
                }
                prizeInfoList.Add(prizeInfo);
            }
            var response = ResponseHelper.CreateSuccess<LeagueAllPrizeInfoResponse>();
            response.Data = new LeagueAllPrizeInfoList
            {
                PrizeInfoList = prizeInfoList
            };
            return response;
        }

        /// <summary>
        /// 领取赛季奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueRecordId"></param>
        /// <returns></returns>
        public LeaguePrizeResponse GetRankPrize(Guid managerId, Guid leagueRecordId)
        {
            var leagueRecordInfo = LeagueRecordMgr.GetById(leagueRecordId);
            if (leagueRecordInfo == null)
                return ResponseHelper.Create<LeaguePrizeResponse>(MessageCode.NbParameterError);
            var npcCount = CacheFactory.LeagueCache.GetTeamCount(leagueRecordInfo.LaegueId);
            int maxWheel = npcCount*2;
            //最后一轮
            if (leagueRecordInfo.Schedule <= maxWheel)
                return ResponseHelper.Create<LeaguePrizeResponse>(MessageCode.LeagueNoEnd);

            //玩家记录
            var leagueManagerRecord = LeagueManagerrecordMgr.GetManagerMarkInfo(managerId,
                    leagueRecordInfo.LaegueId);

            if (leagueRecordInfo.PrizeTime == DateTime.Today)
                return ResponseHelper.Create<LeaguePrizeResponse>(MessageCode.LeaguePrizeReceivedToday);

            if (leagueRecordInfo.Rank == 1)//冠军奖励
            {
                if (leagueManagerRecord.SendFirstPassPrize) //首次通关才有奖励
                     return ResponseHelper.Create<LeaguePrizeResponse>(MessageCode.LeaguePrizeReceivedToday);
                leagueManagerRecord.SendFirstPassPrize = true; 
                //排名奖励
                var rankPrizes = LeagueCache.Instance.GetLeaguePrize(leagueRecordInfo.LaegueId,5);
                int exp = 0;
                int coin = 0;
                int score = 0;
                int vipExp = 0;
                ItemPackageFrame package = null;
                int point = 0;
                var manager = ManagerCore.Instance.GetManager(managerId);
                SendPrize(rankPrizes, manager, leagueRecordInfo.LaegueId, ref exp, ref coin, ref score, ref package, ref point,ref vipExp);
                leagueRecordInfo.IsSend = true;
                leagueRecordInfo.Schedule = -1;
                leagueRecordInfo.PrizeTime = DateTime.Today;
                leagueRecordInfo.UpdateTime = DateTime.Now;

                leagueManagerRecord.Score += score;


                var leagueManager = LaegueManagerinfoMgr.GetById(managerId);
                leagueManager.SumScore += score;

                ManagerUtil.AddManagerData(manager, exp, coin, 0, EnumCoinChargeSourceType.LeaguePrize, ShareUtil.CreateSequential().ToString());

                leagueManagerRecord.LastPrizeLeagueRecordId = new Guid();
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Main)))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = SaveRankPrize(manager, leagueRecordInfo, leagueManagerRecord, leagueManager, package, point, transactionManager.TransactionObject);
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<LeaguePrizeResponse>(messageCode);
                    }
                }
                //leagueManagerRecord.LastPrizeLeagueRecordId = leagueManagerRecord.LeagueRecordId;

                //奖励结果
                var response = ResponseHelper.CreateSuccess<LeaguePrizeResponse>();
                response.Data = new LeaguePrizes
                {
                    PrizeList = new List<LeaguePrizeEntity>()
                };
                foreach (var prize in rankPrizes)
                {
                    var entity = new LeaguePrizeEntity();
                    entity.PrizeType = prize.PrizeType;
                    entity.ItemCode = prize.PrizeType;
                    entity.Count = prize.Count;
                    response.Data.PrizeList.Add(entity);
                }
                response.Data.VipExp = vipExp;
                return response;
            }
            return ResponseHelper.Create<LeaguePrizeResponse>(MessageCode.LeagueNotChampion);
           
        }

        private MessageCode SaveRankPrize(NbManagerEntity manager, LeagueRecordEntity leagueRecord, LeagueManagerrecordEntity mLeagueRecord, LaegueManagerinfoEntity leagueManagerinfo, ItemPackageFrame package, int point, DbTransaction trans)
        {
            if (!ManagerUtil.SaveManagerData(manager,null,trans))
                return MessageCode.NbUpdateFail;
            if (!LeagueRecordMgr.Update(leagueRecord, trans))
                return MessageCode.NbUpdateFail;
            if (!LeagueManagerrecordMgr.Update(mLeagueRecord, trans))
                return MessageCode.NbUpdateFail;
            if (!LaegueManagerinfoMgr.Update(leagueManagerinfo, trans))
                return MessageCode.NbUpdateFail;
            if (package != null)
            {
                if (!package.Save(trans))
                    return MessageCode.NbUpdateFail;
                package.Shadow.Save();
            }
            if (point > 0)
            {
                var code = PayCore.Instance.AddBonus(manager.Idx, point, EnumChargeSourceType.LeaguePrize,
                    ShareUtil.GenerateComb().ToString(), trans);
                return code;
            }
            return MessageCode.Success;
        }

        #endregion


        #region 领取胜场奖励

        public LeaguePrizeResponse GetWincountPrize(Guid managerId, int leagueId, int countType)
        {
            var leagueWincountRecord = LeagueWincountrecordMgr.GetRecord(managerId, leagueId);
            if (leagueWincountRecord == null)
                return ResponseHelper.InvalidParameter<LeaguePrizeResponse>();
            //获取所有奖励
           var winPrize = CacheFactory.LeagueCache.GetLeagueStar(leagueId);
           if (winPrize == null || winPrize.Count <= 0)
               return ResponseHelper.InvalidParameter<LeaguePrizeResponse>();
            //获取这一档的奖励
            var prizeInfo = winPrize.Find(r => r.PrizeLevel == countType);
            if (prizeInfo == null)
                return ResponseHelper.InvalidParameter<LeaguePrizeResponse>();
            var prizeStatus = leagueWincountRecord.PrizeStep.Split(',');
            if (prizeStatus.Length < countType || prizeStatus[countType - 1] =="0")
                return ResponseHelper.Create<LeaguePrizeResponse>(MessageCode.LeagueWincountPrizeCannotReceive);
            if (prizeStatus[countType - 1] == "2")
                return ResponseHelper.Create<LeaguePrizeResponse>(MessageCode.LeagueWincountPrizeReceived);
            prizeStatus[countType - 1] = "2";
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.LeaguePrize);
            if(package == null)
                return ResponseHelper.InvalidParameter<LeaguePrizeResponse>();
            int point = 0;
            int coin = 0;
            NbManagerEntity manager = null;
            if (prizeInfo.PrizeType == (int)EnumActivityExPrizeType.Item) //道具
            {
                var result = package.AddItems(prizeInfo.SubType, prizeInfo.Count, 1,false,false);
                if (result != MessageCode.Success)
                     return ResponseHelper.Create<LeaguePrizeResponse>(result);
            }
            else if (prizeInfo.PrizeType == (int)EnumActivityExPrizeType.Point)

            {
                point = prizeInfo.Count;
            }
            else if (prizeInfo.PrizeType == (int)EnumActivityExPrizeType.Coin)
            {
                coin += prizeInfo.Count;
                manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return ResponseHelper.InvalidParameter<LeaguePrizeResponse>();
            }
            leagueWincountRecord.PrizeStep = string.Join(",", prizeStatus);
            leagueWincountRecord.UpdateTime = DateTime.Now;
            //获取奖励
            var prizeInfoResponse = new LeaguePrizeEntity()
            {
                PrizeType = prizeInfo.PrizeType,
                Count = prizeInfo.Count
            };

            var code = SaveWincountPrize(manager,leagueWincountRecord, package, point, coin);

            if (code != MessageCode.Success)
                return ResponseHelper.Create<LeaguePrizeResponse>(code);
            
            var response = ResponseHelper.CreateSuccess<LeaguePrizeResponse>();
            response.Data = new LeaguePrizes() { PrizeList = new List<LeaguePrizeEntity>() };
            response.Data.PrizeList.Add(prizeInfoResponse);

            return response;

        }

        public MessageCode SaveWincountPrize(NbManagerEntity manager,LeagueWincountrecordEntity leagueWincountRecord,ItemPackageFrame package, int point,int coin)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Main)))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveWincountPrizePrize(transactionManager.TransactionObject,manager, leagueWincountRecord, package, point,coin);
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.ErrorByZone("SavePrize", ex);
                return MessageCode.Exception;
            }
        }

        private MessageCode Tran_SaveWincountPrizePrize(DbTransaction transaction,NbManagerEntity manager, LeagueWincountrecordEntity leagueWincountRecord, ItemPackageFrame package, int point,int coin)
        {
            if (point > 0)
            {
                var code = PayCore.Instance.AddBonus(leagueWincountRecord.ManagerId, point,
                    EnumChargeSourceType.LeaguePrize, ShareUtil.GenerateComb().ToString(), transaction);
                if (code != MessageCode.Success)
                    return code;
            }else if (coin > 0)
            {
                var code = ManagerCore.Instance.AddCoin(manager, coin, EnumCoinChargeSourceType.LeaguePrize,
                    ShareUtil.GenerateComb().ToString(), transaction);
                if (code != MessageCode.Success)
                    return code;
            }
            else
            {
                if (!package.Save(transaction))
                    return MessageCode.FailUpdate;
                package.Shadow.Save();
            }

            if (!LeagueWincountrecordMgr.Update(leagueWincountRecord, transaction))
                return MessageCode.FailUpdate;
            return MessageCode.Success;

        }

        #endregion


        /// <summary>
        /// 获取对阵
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<LeagueFightMap> CreateFightMap(int templateId, int round)
        {
            List<LeagueFightMap> result = new List<LeagueFightMap>();
            //获取对阵详情
            var pairList = CacheFactory.LeagueCache.GetFightMap(templateId, round);
            foreach (var item in pairList)
            {
                LeagueFightMap entity = new LeagueFightMap();
                entity.HomeId = item.Team1;
                entity.AwayId = item.Team2;
                result.Add(entity);
            }
            return result;
        }

        EnumWinType CalWinType(int resultType)
        {
            if (resultType == 0)
                return EnumWinType.Win;
            else if (resultType == 1)
                return EnumWinType.Draw;
            else
                return EnumWinType.Lose;
        }


    }
}
