using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Rank;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.ServiceEngine;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.CrossLadder
{
    public class CrossLadderCore
    {


        private readonly int _ladderRegisterScore;
        private readonly int _ladderExchangeShowDay;

        #region .ctor
        public CrossLadderCore(int p)
        {
            _ladderRegisterScore =
            CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterLadderScore);
            _ladderExchangeShowDay =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderExchangeShowDay);
        }
        #endregion

        #region Front interface
        public static CrossLadderCore Instance
        {
            get { return SingletonFactory<CrossLadderCore>.SInstance; }
        }

        public LadderExchangeResponse Exchange(string siteId,Guid managerId, int exchangeIdx)
        {
            var response1 = GetLadderManager(siteId,managerId);
            if (response1.Code!=ShareUtil.SuccessCode)
                return ResponseHelper.Create<LadderExchangeResponse>(response1.Code);
            if (response1.Data == null)
                return ResponseHelper.InvalidParameter<LadderExchangeResponse>();
            var manager = response1.Data;
            var exchangeCache = CacheFactory.CrossLadderCache.GetExchangeEntity(exchangeIdx);
            if (exchangeCache == null)
                return ResponseHelper.InvalidParameter<LadderExchangeResponse>();
            if (manager.Honor < exchangeCache.CostHonor)
                return ResponseHelper.Create<LadderExchangeResponse>(MessageCode.LadderExchangeHonorShortage);
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CrossLadderExchange,siteId);
            var code = package.AddItem(exchangeCache.ItemCode,true,false);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<LadderExchangeResponse>(code);
            manager.Honor = manager.Honor - exchangeCache.CostHonor;
            manager.UpdateTime = DateTime.Now;
            var record = new CrossladderExchangerecordEntity()
            {
                SiteId = siteId,
                CostHonor = exchangeCache.CostHonor,
                ItemCode = exchangeCache.ItemCode,
                ManagerId = managerId,
                RowTime = DateTime.Now
            };
            code = SaveExchange(manager, package, record);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<LadderExchangeResponse>(code);
            else
            {
                package.Shadow.Save();
                var response = ResponseHelper.CreateSuccess<LadderExchangeResponse>();
                response.Data = new LadderExchangeEntity() { CurHonor = manager.Honor, ItemCode = exchangeCache.ItemCode };
                return response;
            }
        }

        public int GetLadderRank(Guid managerId)
        {
            var rankEntity = RankLadderThread.Instance.GetMyRank(managerId,(int)EnumRankType.LadderRank);
            if (rankEntity != null)
            {
                return rankEntity.Rank;
            }
            else
            {
                return 0;
            }
        }

        public MessageCodeResponse BuyStamina(string siteId, Guid managerId)
        {
            var response1 = GetLadderManager(siteId,managerId);
            if (response1 == null)
                return ResponseHelper.InvalidParameter<MessageCodeResponse>();
            if (response1.Code != ShareUtil.SuccessCode)
            {
                return ResponseHelper.Create<MessageCodeResponse>(response1.Code);
            }
            
            var mallDirect = new CrossMallDirectFrame(siteId, managerId, EnumConsumeSourceType.CrossLadderStamina);
            var checkCode = mallDirect.Check();
            if (checkCode != MessageCode.Success)
                return ResponseHelper.Create<MessageCodeResponse>(checkCode);
            var ladderManager = response1.Data;
            ladderManager.StaminaBuy = ladderManager.StaminaBuy + 1;
            ladderManager.Stamina = ladderManager.Stamina + 1;
            if (!CrossladderManagerMgr.Update(ladderManager))
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail);
            }

            checkCode = mallDirect.Save(Guid.NewGuid().ToString());
            if (checkCode != MessageCode.Success)
            {
                SystemlogMgr.Error("CrossLadder-BuyStamina fail",string.Format("ManagerId:{0}",managerId));
            }
            return ResponseHelper.CreateSuccess<MessageCodeResponse>();
        }

        public CrossladderManagerResponse GetManagerInfo(string siteId,Guid managerId)
        {
            var response = GetLadderManager(siteId, managerId);
            if (response.Code != ShareUtil.SuccessCode)
                return response;
            var season = CacheFactory.CrossLadderCache.GetCurrentSeason();
            season.StartTick = ShareUtil.GetTimeTick(season.Startdate);
            season.EndTick = ShareUtil.GetTimeTick(season.Enddate.Date.AddDays(1).AddSeconds(-1));
            response.Data.Season = season;
            var rankEntity = CrossRankThread.Instance.GetMyRank(siteId,managerId, (int)EnumRankType.CrossLadderRank);
            if (rankEntity != null)
            {
                response.Data.MyRank = rankEntity.Rank;
                response.Data.YesterdayRank = rankEntity.YesterdayRank;
            }
            else
            {
                response.Data.MyRank = -1;
                response.Data.YesterdayRank = -1;
            }
            response.Data.WinRate = ManagerUtil.GetWinRate(managerId,EnumMatchType.CrossLadder,siteId);
            return response;
        }

        public CrossladderManagerResponse GetLadderManager(string siteId, Guid managerId)
        {
            try
            {
                var ladderManager = CrossladderManagerMgr.GetById(managerId);
                if (ladderManager == null)
                {
                    if (!ManagerUtil.CheckFunction(siteId, managerId, EnumOpenFunction.CrossLadder))
                    {
                        return ResponseHelper.Create<CrossladderManagerResponse>(MessageCode.NbFunctionNotOpen);
                    }
                    var nbManager = NbManagerMgr.GetById(managerId, siteId);
                    if (nbManager == null)
                        return ResponseHelper.Create<CrossladderManagerResponse>(MessageCode.MissManager);
                    int domainId = 0;
                    int honor = 0;
                    CrossladderManagerMgr.GetOldHonor(managerId, ref honor);
                    CrossSiteCache.Instance().TryGetDomainId(siteId, out domainId);
                    ladderManager = new CrossladderManagerEntity();
                    ladderManager.DomainId = domainId;
                    ladderManager.SiteId = siteId;
                    ladderManager.SiteName = CacheFactory.FunctionAppCache.GetCrossZoneName(siteId);
                    ladderManager.ManagerId = managerId;
                    ladderManager.Name = nbManager.Name;
                    ladderManager.SiteId = siteId;
                    ladderManager.Logo = nbManager.Logo;
                    ladderManager.Score = _ladderRegisterScore;
                    ladderManager.LastExchageTime = ShareUtil.BaseTime;
                    ladderManager.RowTime = DateTime.Now;
                    ladderManager.UpdateTime = DateTime.Now;
                    ladderManager.Honor = honor;
                    ladderManager.MaxScore = _ladderRegisterScore;
                    ladderManager.DailyMaxScore = _ladderRegisterScore;
                    ladderManager.Stamina = 50;
                    CrossladderManagerMgr.Insert(ladderManager);
                }
                if (ladderManager.Stamina < 0)
                    ladderManager.Stamina = 0;
                if (ladderManager.StaminaBuy < 0)
                    ladderManager.StaminaBuy = 0;
                var response = ResponseHelper.CreateSuccess<CrossladderManagerResponse>();
                response.Data = ladderManager;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetCrossLadderManager", ex);
                return ResponseHelper.Create<CrossladderManagerResponse>(MessageCode.Exception);
            }
            
        }

        public LadderMatchEntityListResponse GetMatchList(Guid managerId)
        {
            var response= ResponseHelper.CreateSuccess<LadderMatchEntityListResponse>();
            response.Data = new LadderMatchEntityList();
            var list = CrossladderMatchMgr.GetFiveMatch(managerId);
            if (list != null)
            {
                response.Data.Matchs = new List<LadderMatchEntityView>(list.Count);
                foreach (var match in list)
                {
                    string awayName = "";
                    EnumWinType winType;//0：负; 1：胜;2：平
                    int prizeScore;
                    string score = ""; //比分 2:3
                    if (managerId == match.HomeId && match.HomeIsBot==false)
                    {
                        awayName = match.AwayName;
                        prizeScore = match.PrizeHomeScore;
                        score = string.Format("{0}:{1}", match.HomeScore, match.AwayScore);
                        winType = ShareUtil.CalWinType(match.HomeScore, match.AwayScore);
                    }
                    else
                    {
                        if (match.AwayIsBot)
                        {
                            continue;
                        }
                        awayName = match.HomeName;
                        prizeScore = match.PrizeAwayScore;
                        score = string.Format("{0}:{1}", match.AwayScore, match.HomeScore);
                        winType = ShareUtil.CalWinType(match.AwayScore, match.HomeScore);
                    }
                    response.Data.Matchs.Add(BuildMatchView(match.Idx,awayName,prizeScore,score,winType));
                }
            }
            return response;
        }

        public CrossladderMatchResponse GetMatch(Guid managerId, Guid matchId)
        {
            var match= MemcachedFactory.LadderMatchClient.Get<CrossladderMatchEntity>(matchId);
            if (match == null)
            {
                match = CrossladderMatchMgr.GetById(matchId);
                if (match == null)
                {
                    return ResponseHelper.InvalidParameter<CrossladderMatchResponse>();
                }
            }
            var response = ResponseHelper.CreateSuccess<CrossladderMatchResponse>();
            response.Data = match;
            return response;
        }

        #endregion

        
        #region encapsulation

        MessageCode SaveExchange(CrossladderManagerEntity ladderManager, ItemPackageFrame package, CrossladderExchangerecordEntity ladderExchangerecord)
        {
            if (ladderManager == null || package == null || ladderExchangerecord == null)
            {
                return MessageCode.NbUpdateFail;
            }
            try
            {
                if(!CrossladderManagerMgr.Update(ladderManager))
                    return MessageCode.NbUpdateFail;
                if(!package.Save())
                    return MessageCode.NbUpdateFail;
                if(!CrossladderExchangerecordMgr.Insert(ladderExchangerecord))
                    return MessageCode.NbUpdateFail;
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossLadder-SaveExchange", ex);
                return MessageCode.Exception;
            }
        }

        LadderMatchEntityView BuildMatchView(Guid matchId,string awayName,int prizeScore,string score,EnumWinType winType)
        {
            var matchView = new LadderMatchEntityView();
            matchView.AwayName = awayName;
            matchView.MatchId = matchId;
            matchView.PrizeScore = prizeScore;
            matchView.ScoreView = score;
            matchView.WinType = (int)winType;
            return matchView;
        }

        LadderManagerEntity InnerGetLadderManager(Guid managerId)
        {
            return LadderManagerMgr.GetById(managerId, _ladderRegisterScore);
        }

        #endregion
    }
}
