using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Online;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Match;
using Games.NBall.WebServerFacade;

namespace Games.NBall.Core.CrossLadder
{
    public class CrossLadderProcess
    {
        private Dictionary<Guid, CrossladderMatchEntity> _fightDic;
        private CrossladderInfoEntity _ladderInfo;
        private int _ladderProctiveScore; //新手保护分数线
        private static NBThreadPool _nbThreadPool;
        int winExp = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderWinExp);
        int drawExp = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderDrawExp);
        int loseExp = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderLoseExp);
        int winCoin = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderWinCoin);
        int drawCoin = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderDrawCoin);
        int loseCoin = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderLoseCoin);

        public CrossLadderProcess(Dictionary<Guid, CrossladderMatchEntity> fightDic, CrossladderInfoEntity ladderInfo, int ladderProctiveScore)
        {
            _nbThreadPool = new NBThreadPool(10);
            this._fightDic = fightDic;
            this._ladderProctiveScore = ladderProctiveScore;
            _ladderInfo = ladderInfo;
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
            if (_fightDic == null)
                return;
            foreach (var item in _fightDic.Values)
            {
                var matchHome = new MatchManagerInfo(item.HomeId,item.HomeSiteId, false, item.HomeIsBot);
                var matchAway = new MatchManagerInfo(item.AwayId,item.AwaySiteId, false, item.AwayIsBot);
                var matchData = new BaseMatchData((int)EnumMatchType.CrossLadder, item.Idx, matchHome, matchAway);
                matchData.ErrorCode = (int)MessageCode.MatchWait;

                MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);

                //使用多线程
                _nbThreadPool.Add(() => Fight(matchData, item));
            }

            _nbThreadPool.WaitAll();
            _ladderInfo.Groups = _fightDic.Count;
            _ladderInfo.RowTime = DateTime.Now;

            _ladderInfo.Season = CacheFactory.CrossLadderCache.GetCurrentSeasonIndex();
            _ladderInfo.Status = 2;
            _ladderInfo.UpdateTime = DateTime.Now;

            _ladderInfo.FightList = null;
            _fightDic = null;

            CrossladderInfoMgr.Insert(_ladderInfo);
            _ladderInfo = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matchData"></param>
        /// <param name="laddermatch"></param>
        void Fight(BaseMatchData matchData, CrossladderMatchEntity laddermatch)
        {
            if (laddermatch.HomeIsBot && laddermatch.AwayIsBot)
            {
                laddermatch.HomeScore = 0;
                laddermatch.AwayScore = 0;
                laddermatch.Status = (int)EnumLadderStatus.End;

                //保存比赛
                CrossladderMatchMgr.Insert(laddermatch);
                return;
            }
            else
            {
                try
                {
                    MatchCore.CreateMatch(matchData);

                    if (matchData.ErrorCode == (int)MessageCode.Success)
                    {
                        laddermatch.HomeScore = matchData.Home.Score;
                        laddermatch.AwayScore = matchData.Away.Score;
                        laddermatch.Status = (int)EnumLadderStatus.End;
                        CalPrizePoint(laddermatch);

                        int returnCode = -1;
                        //保存比赛
                        CrossladderMatchMgr.SaveMatch(laddermatch.DomainId, laddermatch.LadderId, laddermatch.HomeId, laddermatch.AwayId, laddermatch.HomeName, laddermatch.AwayName,
                            laddermatch.HomeLogo,laddermatch.AwayLogo,
                            laddermatch.HomeSiteId, laddermatch.AwaySiteId,
                            laddermatch.HomeLadderScore, laddermatch.AwayLadderScore, laddermatch.HomeScore, laddermatch.AwayScore,laddermatch.HomeCoin,laddermatch.AwayCoin,laddermatch.HomeExp,laddermatch.AwayExp,
                            laddermatch.HomeIsBot, laddermatch.AwayIsBot, laddermatch.GroupIndex, laddermatch.PrizeHomeScore, laddermatch.PrizeAwayScore,
                            laddermatch.RowTime, laddermatch.Idx, ref returnCode);
                        
                        if (!laddermatch.HomeIsBot)
                        {
                            WebServerHandler.AddManagerData(laddermatch.HomeId, laddermatch.HomeExp, laddermatch.HomeCoin, 0, laddermatch.HomeSiteId);
                            MatchCore.SaveMatchStat(laddermatch.HomeId, EnumMatchType.CrossLadder, laddermatch.HomeScore, laddermatch.AwayScore, laddermatch.HomeScore, laddermatch.HomeSiteId);
                            if (laddermatch.HomeIsHook)
                            {
                                CrossLadderManager.Instance.UpdateHookScore(laddermatch.HomeSiteId, laddermatch.HomeId, laddermatch.PrizeHomeScore,
                                    laddermatch.HomeScore > laddermatch.AwayScore, laddermatch.HomeScore,
                                    laddermatch.AwayScore, laddermatch.HomeName, laddermatch.AwayName,
                                    laddermatch.HomeCoin);
                            }
                        }
                        if (!laddermatch.AwayIsBot)
                        {
                            WebServerHandler.AddManagerData(laddermatch.AwayId, laddermatch.AwayExp, laddermatch.AwayCoin, 0, laddermatch.AwaySiteId);
                            MatchCore.SaveMatchStat(laddermatch.AwayId, EnumMatchType.CrossLadder, laddermatch.AwayScore, laddermatch.HomeScore, laddermatch.AwayScore, laddermatch.AwaySiteId);
                            if (laddermatch.AwayIsHook)
                            {
                                CrossLadderManager.Instance.UpdateHookScore(laddermatch.AwaySiteId, laddermatch.AwayId, laddermatch.PrizeAwayScore,
                                    laddermatch.AwayScore < laddermatch.HomeScore, laddermatch.HomeScore,
                                    laddermatch.AwayScore, laddermatch.HomeName, laddermatch.AwayName,
                                    laddermatch.AwayCoin);
                            }
                        }
                    }
                    MemcachedFactory.LadderMatchClient.Set(laddermatch.Idx, laddermatch);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("CrossLadderProcess-Fight", ex);
                }
                
            }
            matchData = null;
            laddermatch = null;
        }

        EnumWinType CalWinType(int homeScore, int awayScore)
        {
            if(homeScore>awayScore)
                return EnumWinType.Win;
            else if(homeScore==awayScore)
            {
                return EnumWinType.Draw;
            }
            else
            {
                return EnumWinType.Lose;
            }
        }

        /// <summary>
        /// 计算主队奖励积分，客队为主队奖励积分*-1.
        /// 获胜得分计算公式为x=min(21,max(1,11-int((A-B)/10)))
        /// 如果为平局，则计算公式为：x=min(21,max(1,11-int((A-B)/10)))-11;
        /// </summary>
        /// <param name="laddermatch">The laddermatch.</param>
        /// <returns></returns>
        int CalPrizePoint(CrossladderMatchEntity laddermatch)
        {
            var homeValid = CalValidPoint(laddermatch.HomeWinPercent, laddermatch.HomeLadderScore);
            var awayValid = CalValidPoint(laddermatch.AwayWinPercent, laddermatch.AwayLadderScore);
            var prizePoint = 0; //以主队为参照的奖励积分，客队*-1

            var validDiff = 0;
            if (laddermatch.HomeScore > laddermatch.AwayScore)
            {
                validDiff = homeValid - awayValid;
                prizePoint = Math.Min(20, Math.Max(2, 11 - validDiff / 20));
                laddermatch.HomeCoin = winCoin;
                laddermatch.HomeExp = winExp;
                laddermatch.AwayCoin = loseCoin;
                laddermatch.AwayExp = loseExp;
            }
            else if (laddermatch.HomeScore == laddermatch.AwayScore)
            {
                validDiff = homeValid - awayValid;
                prizePoint = Math.Min(20, Math.Max(2, 11 - validDiff / 20)) - 11;
                laddermatch.HomeCoin = drawCoin;
                laddermatch.HomeExp = drawExp;
                laddermatch.AwayCoin = drawCoin;
                laddermatch.AwayExp = drawExp;
            }
            else
            {
                validDiff = awayValid - homeValid;
                prizePoint = -1 * (Math.Min(20, Math.Max(2, 11 - validDiff / 20)));
                laddermatch.HomeCoin = loseCoin;
                laddermatch.HomeExp = loseExp;
                laddermatch.AwayCoin = winCoin;
                laddermatch.AwayExp = winExp;
            }

            if (prizePoint > 0)
            {
                //var buff = BuffRules.GetSummedBuff(laddermatch.HomeId, BuffCodeEnum.ArenaLadderScore);
                laddermatch.PrizeHomeScore = (int)(prizePoint * (1 + 0));// buff.PerBuffValue));

                if (laddermatch.AwayLadderScore > _ladderProctiveScore)
                    laddermatch.PrizeAwayScore = -1 * prizePoint;
            }
            else if (prizePoint < 0)
            {
                //var buff = BuffRules.GetSummedBuff(laddermatch.AwayId, BuffCodeEnum.ArenaLadderScore);
                laddermatch.PrizeAwayScore = -1 * (int)(prizePoint * (1 + 0));// buff.PerBuffValue));

                if (laddermatch.HomeLadderScore > _ladderProctiveScore)
                    laddermatch.PrizeHomeScore = prizePoint;
            }
            laddermatch.PrizeHomeScore = OnlineCore.Instance.CalIndulgeLadderScore(laddermatch.HomeId, laddermatch.PrizeHomeScore,laddermatch.HomeSiteId);
            laddermatch.PrizeAwayScore = OnlineCore.Instance.CalIndulgeLadderScore(laddermatch.AwayId, laddermatch.PrizeAwayScore,laddermatch.AwaySiteId);
            
            //SysteminfologMgr.Insert("CrossladderProcess-CalPrizePoint"
            //    , string.Format("homeScore[{0}]-awayScore[{1}]==homeValid[{2}]-awayValid[{3}]:prizePoint[{4}]", laddermatch.HomeScore, laddermatch.AwayScore, homeValid, awayValid, prizePoint));
            return prizePoint;
        }

        /// <summary>
        /// 计算有效积分.
        /// 实际天梯赛积分+（（胜率-60%）/40%）*50
        /// </summary>
        /// <param name="winRate">The win rate.</param>
        /// <param name="arenaScore">The arena score.</param>
        /// <returns></returns>
        int CalValidPoint(int winRate, int arenaScore)
        {
            try
            {
                var score = arenaScore;
                //var totalN = arenaManager.Win + arenaManager.Draw + arenaManager.Lose;
                //var rate = arenaManager.Win * 100 / (totalN == 0 ? 1 : totalN);
                return score + ((winRate - 60) / 40) * 50;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossladderProcess-BuildFightInfo", ex.Message, ex.StackTrace);
                return -1;
            }
        }
    }
}
