using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Online;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Match;
using Games.NBall.WebServerFacade;

namespace Games.NBall.Core.CrossCrowd
{
    public class CrossCrowdProcess
    {
        private CrossCrowdThread.ClearFightDicDelegate _clearFightDicDelegate;
        private Dictionary<Guid, CrosscrowdMatchEntity> _matchDic;
        private CrosscrowdPairrecordEntity _pairRecord;
        private Dictionary<Guid, CrosscrowdManagerEntity> _managerDic;
        public EnumLadderStatus Status { get; set; }
        private int _crowdResurrectionCd;
        private int _crowdCd;
        private int _coinChargeSourceType;
        private int _domainId;
        public CrossCrowdProcess(Dictionary<Guid, CrosscrowdMatchEntity> matchDic, CrosscrowdPairrecordEntity pairRecord, int crowdResurrectionCd, int crowdCd, CrossCrowdThread.ClearFightDicDelegate clearDelegate,int domainId)
        {
            _domainId = domainId;
            _matchDic = matchDic;
            _pairRecord = pairRecord;
            _managerDic = pairRecord.FightList.ToDictionary(d => d.ManagerId, d => d);
            _crowdResurrectionCd = crowdResurrectionCd;
            _crowdCd = crowdCd;
            _clearFightDicDelegate = clearDelegate;
            _coinChargeSourceType = (int)EnumCoinChargeSourceType.CrossCrowd;
        }

        public void StartProcess()
        {
            RunMatch();
        }

        /// <summary>
        /// Runs the arena match.
        /// </summary>
        void RunMatch()
        {
            if (_matchDic == null)
                return;
            try
            {
                Status = EnumLadderStatus.Running;
                foreach (var item in _matchDic.Values)
                {
                    var matchHome = new MatchManagerInfo(item.HomeId,item.HomeSiteId);
                    var matchAway = new MatchManagerInfo(item.AwayId,item.AwaySiteId);
                    var matchData = new BaseMatchData((int)EnumMatchType.CrossCrowd, item.Idx, matchHome, matchAway);
                    matchData.ErrorCode = (int)MessageCode.MatchWait;
                    MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);
                    Fight(matchData, item);
                }

                _matchDic = null;
                _pairRecord.RowTime = DateTime.Now;
                CrosscrowdPairrecordMgr.Insert(_pairRecord);
                _pairRecord = null;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowdProcess-RunMatch", ex);
            }
            finally
            {
                Status = EnumLadderStatus.End;
                if (_clearFightDicDelegate != null)
                {
                    _clearFightDicDelegate();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matchData"></param>
        /// <param name="crowdMatch"></param>
        void Fight(BaseMatchData matchData, CrosscrowdMatchEntity crowdMatch)
        {
            try
            {
                MatchCore.CreateMatch(matchData);
                if (matchData.ErrorCode == (int)MessageCode.Success)
                {
                    crowdMatch.HomeScore = matchData.Home.Score;
                    crowdMatch.AwayScore = matchData.Away.Score;
                    crowdMatch.HomeName = matchData.Home.Name;
                    crowdMatch.AwayName = matchData.Away.Name;
                    crowdMatch.Status = 0;
                    SavePrize(crowdMatch);
                }
                MemcachedFactory.CrowdMatchClient.Set(crowdMatch.Idx, crowdMatch);

                matchData = null;
                crowdMatch = null;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowdProcess-Fight", ex);
            }

        }

        void SavePrize(CrosscrowdMatchEntity crowdMatch)
        {
            try
            {
                crowdMatch.HomePrizeScore = CacheFactory.CrowdCache.GetCrowdScore(crowdMatch.HomeScore);
                var costMorale = CacheFactory.CrowdCache.GetCostMorela(crowdMatch.AwayScore);
                var winType = ShareUtil.CalWinType(crowdMatch.HomeScore, crowdMatch.AwayScore);
                var matchPrize = CacheFactory.CrowdCache.GetMatchPrize(winType);
                var homeManager = _managerDic[crowdMatch.HomeId];
                homeManager.Morale -= costMorale;
                if (homeManager.Morale < 0)
                    homeManager.Morale = 0;

                int homecoin = matchPrize.Coin;
                int homehonor = matchPrize.Honor;
                OnlineCore.Instance.CalIndulgePrize(crowdMatch.HomeId, ref homecoin, ref homehonor,crowdMatch.HomeSiteId);
                crowdMatch.HomeMorale = homeManager.Morale;
                crowdMatch.HomePrizeCoin = homecoin;
                crowdMatch.HomePrizeHonor = homehonor;
                crowdMatch.HomeCostMorale = costMorale;

                crowdMatch.AwayPrizeScore = CacheFactory.CrowdCache.GetCrowdScore(crowdMatch.AwayScore);
                costMorale = CacheFactory.CrowdCache.GetCostMorela(crowdMatch.HomeScore);
                winType = ShareUtil.CalWinType(crowdMatch.AwayScore, crowdMatch.HomeScore);
                matchPrize = CacheFactory.CrowdCache.GetMatchPrize(winType);
                var awayManager = _managerDic[crowdMatch.AwayId];
                awayManager.Morale -= costMorale;
                if (awayManager.Morale < 0)
                    awayManager.Morale = 0;
                int awaycoin = matchPrize.Coin;
                int awayhonor = matchPrize.Honor;
                OnlineCore.Instance.CalIndulgePrize(crowdMatch.AwayId, ref awaycoin, ref awayhonor,crowdMatch.AwaySiteId);
                crowdMatch.AwayMorale = awayManager.Morale;
                crowdMatch.AwayPrizeCoin = awaycoin;
                crowdMatch.AwayPrizeHonor = awayhonor;
                crowdMatch.AwayCostMorale = costMorale;

                DateTime resurrectionTime = DateTime.Now.AddSeconds(_crowdResurrectionCd);
                DateTime nextMatchTime = DateTime.Now.AddSeconds(_crowdCd);
                CrosscrowdMatchMgr.Save(crowdMatch, resurrectionTime, nextMatchTime);

                SavePrizeAfter(crowdMatch, homeManager, awayManager);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowdProcess-SavePrize", ex);
            }

        }

        void SavePrizeAfter(CrosscrowdMatchEntity crowdMatch, CrosscrowdManagerEntity homeManager, CrosscrowdManagerEntity awayManager)
        {
            try
            {
                WebServerHandler.AddCoin2(crowdMatch.HomeSiteId, crowdMatch.HomeId, crowdMatch.HomePrizeCoin,
                    _coinChargeSourceType);
                WebServerHandler.AddCoin2(crowdMatch.AwaySiteId, crowdMatch.AwayId, crowdMatch.AwayPrizeCoin,
                    _coinChargeSourceType);

                string banner = "";
                string homePop = "";
                string awayPop = "";

                if (crowdMatch.HomeScore > crowdMatch.AwayScore)
                {
                    homePop += "&" + CrossCrowdMessage.BuildCrowdMatch(EnumWinType.Win, crowdMatch.AwayName, crowdMatch.HomeScore, crowdMatch.AwayScore);
                    awayPop += "&" + CrossCrowdMessage.BuildCrowdMatch(EnumWinType.Lose, crowdMatch.HomeName, crowdMatch.AwayScore, crowdMatch.HomeScore);
                    if (homeManager.WinningCount == 2)
                    {
                        banner += "&" + CrossCrowdMessage.BuildBannerCrowd3Win(crowdMatch.HomeName);
                    }
                    else if (homeManager.WinningCount == 4)
                    {
                        banner += "&" + CrossCrowdMessage.BuildBannerCrowd5Win(crowdMatch.HomeName);
                    }
                }
                else if (crowdMatch.AwayScore > crowdMatch.HomeScore)
                {
                    homePop += "&" + CrossCrowdMessage.BuildCrowdMatch(EnumWinType.Lose, crowdMatch.AwayName, crowdMatch.HomeScore, crowdMatch.AwayScore);
                    awayPop += "&" + CrossCrowdMessage.BuildCrowdMatch(EnumWinType.Win, crowdMatch.HomeName, crowdMatch.AwayScore, crowdMatch.HomeScore);
                    if (awayManager.WinningCount == 2)
                    {
                        banner += "&" + CrossCrowdMessage.BuildBannerCrowd3Win(crowdMatch.AwayName);
                    }
                    else if (awayManager.WinningCount == 4)
                    {
                        banner += "&" + CrossCrowdMessage.BuildBannerCrowd5Win(crowdMatch.AwayName);
                    }
                }
                else
                {
                    homePop += "&" + CrossCrowdMessage.BuildCrowdMatch(EnumWinType.Draw, crowdMatch.AwayName, crowdMatch.HomeScore, crowdMatch.AwayScore);
                    awayPop += "&" + CrossCrowdMessage.BuildCrowdMatch(EnumWinType.Draw, crowdMatch.HomeName, crowdMatch.AwayScore, crowdMatch.HomeScore);
                }
                homePop += "&" + CrossCrowdMessage.BuildCrowdMatchPrize(crowdMatch.HomePrizeScore, crowdMatch.HomePrizeCoin, crowdMatch.HomePrizeHonor);
                awayPop += "&" + CrossCrowdMessage.BuildCrowdMatchPrize(crowdMatch.AwayPrizeScore, crowdMatch.AwayPrizeCoin, crowdMatch.AwayPrizeHonor);

                var scoreDiv = crowdMatch.HomeScore - crowdMatch.AwayScore;
                if (scoreDiv >= 3)
                    banner += "&" + CrossCrowdMessage.BuildBannerCrowdWinOver(crowdMatch.HomeName, crowdMatch.HomeScore, crowdMatch.AwayScore, crowdMatch.AwayName);
                else if (scoreDiv < -2)
                    banner += "&" + CrossCrowdMessage.BuildBannerCrowdWinOver(crowdMatch.AwayName, crowdMatch.AwayScore, crowdMatch.HomeScore, crowdMatch.HomeName);

                if (crowdMatch.HomeMorale <= 0 && crowdMatch.AwayMorale <= 0)
                {
                    homePop += "&" + CrossCrowdMessage.BuildCrowdKillTogether(crowdMatch.AwayName);
                    awayPop += "&" + CrossCrowdMessage.BuildCrowdKillTogether(crowdMatch.HomeName);
                    banner += "&" + CrossCrowdMessage.BuildBannerCrowdKill(crowdMatch.HomeName, crowdMatch.AwayName);
                    banner += "&" + CrossCrowdMessage.BuildBannerCrowdKill(crowdMatch.AwayName, crowdMatch.HomeName);
                    crowdMatch.IsKill = true;
                }
                else if (crowdMatch.HomeMorale <= 0)
                {
                    awayPop += "&" + CrossCrowdMessage.BuildCrowdKill(crowdMatch.HomeName);
                    homePop += "&" + CrossCrowdMessage.BuildCrowdByKill(crowdMatch.AwayName);
                    banner += "&" + CrossCrowdMessage.BuildBannerCrowdKill(crowdMatch.AwayName, crowdMatch.HomeName);
                    crowdMatch.IsKill = true;
                }
                else if (crowdMatch.AwayMorale <= 0)
                {
                    awayPop += "&" + CrossCrowdMessage.BuildCrowdByKill(crowdMatch.HomeName);
                    homePop += "&" + CrossCrowdMessage.BuildCrowdKill(crowdMatch.AwayName);
                    banner += "&" + CrossCrowdMessage.BuildBannerCrowdKill(crowdMatch.HomeName, crowdMatch.AwayName);
                    crowdMatch.IsKill = true;
                }
                //LogHelper.Insert(string.Format("HomePop:{0},AwayPop:{1}",homePop,awayPop),LogType.Info);
                if (!string.IsNullOrEmpty(homePop))
                {
                    CrossCrowdMessage.SendCrowdPop(crowdMatch.HomeId, homePop.TrimStart('&'));
                }
                if (!string.IsNullOrEmpty(awayPop))
                {
                    CrossCrowdMessage.SendCrowdPop(crowdMatch.AwayId, awayPop.TrimStart('&'));
                }
                if (!string.IsNullOrEmpty(banner))
                {
                    CrossCrowdMessage.SendCrowdBanner(_domainId,banner.TrimStart('&'));
                }
                
            }
            catch (Exception ex)
            {

                SystemlogMgr.Error("CrossCrowdProcess-SavePrizeAfter", ex);
            }
        }
    }
}
