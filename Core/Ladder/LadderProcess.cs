using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
//using Games.NBall.Core.Active;
//using Games.NBall.Core.Activity;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Manager;
//using Games.NBall.Core.Online;
using Games.NBall.Core.Match;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Enums.Common;

namespace Games.NBall.Core.Ladder
{
    public class LadderProcess
    {
        private ConcurrentDictionary<Guid, LadderMatchEntity> _fightDic;
        private LadderInfoEntity _ladderInfo;
        private int _ladderProctiveScore; //新手保护分数线
        private static NBThreadPool _nbThreadPool;
        private bool isGuide;
        int winExp = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderWinExp);
        int drawExp = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderDrawExp);
        int loseExp = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderLoseExp);
        int winCoin = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderWinCoin);
        int drawCoin = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderDrawCoin);
        int loseCoin = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderLoseCoin);

        //int winExp = 0;
        //int drawExp = 0;
        //int loseExp = 0;
        //int winCoin = 0;
        //int drawCoin = 0;
        //int loseCoin = 0;

        public LadderProcess(ConcurrentDictionary<Guid, LadderMatchEntity> fightDic, LadderInfoEntity ladderInfo, int ladderProctiveScore, bool isGuide = false)
        {
            _nbThreadPool = new NBThreadPool(10);
            this._fightDic = fightDic;
            this._ladderProctiveScore = ladderProctiveScore;
            _ladderInfo = ladderInfo;
            this.isGuide = isGuide;
        }

        public void StartProcess()
        {
            RunMatch();

            //等待客户端倒计时完再启动
            //timer = new System.Timers.Timer { Interval = IntervalCaculater() };
            //timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            //timer.Start();
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
                var matchHome = new MatchManagerInfo(item.HomeId, false, item.HomeIsBot);
                var matchAway = new MatchManagerInfo(item.AwayId, false, item.AwayIsBot);
                if (isGuide)//引导。 必胜
                {
                    matchHome.BuffScale = 200;
                    matchAway.BuffScale = 50;
                }
                var matchData = new BaseMatchData((int)EnumMatchType.Ladder, item.Idx, matchHome, matchAway);
                matchData.ErrorCode = (int)MessageCode.MatchWait;

                MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);

                //使用多线程
                _nbThreadPool.Add(() => Fight(matchData, item));
            }

            _nbThreadPool.WaitAll();
            _ladderInfo.Groups = _fightDic.Count;
            _ladderInfo.RowTime = DateTime.Now;

            _ladderInfo.Season = 1;
            _ladderInfo.Status = 2;
            _ladderInfo.UpdateTime = DateTime.Now;

            _ladderInfo.FightList = null;
            _fightDic = null;

            LadderInfoMgr.Insert(_ladderInfo);
            _ladderInfo = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matchData"></param>
        /// <param name="laddermatch"></param>
        private void Fight(BaseMatchData matchData, LadderMatchEntity laddermatch)
        {
            try
            {
                if (laddermatch.HomeIsBot && laddermatch.AwayIsBot)
                {
                    laddermatch.HomeScore = 0;
                    laddermatch.AwayScore = 0;
                    laddermatch.Status = (int) EnumLadderStatus.End;

                    //保存比赛
                    LadderMatchMgr.Insert(laddermatch);
                    return;
                }
                else
                {
                    MatchCore.CreateMatch(matchData);
                    ////测试用 ------------
                    //matchData.ErrorCode = (int)MessageCode.Success;
                    //matchData.Home.Score = 5;
                    //matchData.Away.Score = 2;

                    if (matchData.ErrorCode == (int) MessageCode.Success)
                    {
                        laddermatch.HomeScore = matchData.Home.Score;
                        laddermatch.AwayScore = matchData.Away.Score;
                        laddermatch.Status = (int) EnumLadderStatus.End;
                        CalPrizePoint(laddermatch);

                        int returnCode = -1;
                        //保存比赛
                        LadderMatchMgr.SaveMatch(laddermatch.LadderId, laddermatch.HomeId, laddermatch.AwayId,
                            laddermatch.HomeName, laddermatch.AwayName,
                            laddermatch.HomeLadderScore, laddermatch.AwayLadderScore, laddermatch.HomeScore,
                            laddermatch.AwayScore, laddermatch.HomeCoin, laddermatch.AwayCoin, laddermatch.HomeExp,
                            laddermatch.AwayExp,
                            laddermatch.HomeIsBot, laddermatch.AwayIsBot, laddermatch.GroupIndex,
                            laddermatch.PrizeHomeScore, laddermatch.PrizeAwayScore,
                            laddermatch.RowTime, laddermatch.Idx, ref returnCode);

                        if (!laddermatch.HomeIsBot)
                        {
                            AddManagerData(laddermatch.HomeId, laddermatch.HomeExp, laddermatch.HomeCoin, 0,
                                EnumCoinChargeSourceType.Ladder, laddermatch.Idx.ToString() + "_home");
                        }
                        if (!laddermatch.AwayIsBot)
                        {
                            AddManagerData(laddermatch.AwayId, laddermatch.AwayExp, laddermatch.AwayCoin, 0,
                                EnumCoinChargeSourceType.Ladder, laddermatch.Idx.ToString() + "_away");
                        }
                        int homeWinType = (int) CalWinType(laddermatch.HomeScore, laddermatch.AwayScore);
                        int awayWinType = (int) CalWinType(laddermatch.AwayScore, laddermatch.HomeScore);

                        if (!laddermatch.HomeIsBot && homeWinType == (int) EnumWinType.Win) //胜场活动
                        {
                            ActivityExThread.Instance.LadderDayPrize(laddermatch.HomeId);

                        }
                        if (!laddermatch.AwayIsBot && awayWinType == (int) EnumWinType.Win) //胜场活动
                        {
                            ActivityExThread.Instance.LadderDayPrize(laddermatch.AwayId);
                        }
                        if (!laddermatch.HomeIsBot)
                        {

                            var homepop = TaskHandler.Instance.LadderFight(laddermatch.HomeId, homeWinType);
                            if (homepop != null)
                            {
                                MemcachedFactory.MatchPopClient.Set(laddermatch.HomeId, homepop);
                            }

                            MatchCore.SaveMatchStat(laddermatch.HomeId, EnumMatchType.Ladder, laddermatch.HomeScore,
                                laddermatch.AwayScore, laddermatch.HomeScore);
                            //ActivityExThread.Instance.Ladder(laddermatch.HomeId, laddermatch.HomeLadderScore + laddermatch.PrizeHomeScore, homeWinType);
                            //Games.NBall.Core.Guild.GuildMessage.Instance().LadderActive(laddermatch.HomeId);
                            //ActiveCore.Instance.AddActive(laddermatch.HomeId, EnumActiveType.Ladder, 1);
                            
                            //记录成就相关数据
                            AchievementTaskCore.Instance.UpdateLadderGoals(laddermatch.HomeId, laddermatch.HomeScore,
                                (EnumWinType) homeWinType, laddermatch.HomeLadderScore + laddermatch.PrizeHomeScore);
                            
                            if (laddermatch.HomeIsHook)
                            {
                                LadderThread.Instance.UpdateHookScore(laddermatch.HomeId, laddermatch.PrizeHomeScore,
                                    laddermatch.HomeScore > laddermatch.AwayScore, laddermatch.HomeScore,
                                    laddermatch.AwayScore, laddermatch.HomeName, laddermatch.AwayName,
                                    laddermatch.HomeCoin);
                            }
                        }
                        if (!laddermatch.AwayIsBot)
                        {
                            var awaypop = TaskHandler.Instance.LadderFight(laddermatch.AwayId, awayWinType);
                            if (awaypop != null)
                            {
                                MemcachedFactory.MatchPopClient.Set(laddermatch.AwayId, awaypop);
                            }
                            MatchCore.SaveMatchStat(laddermatch.AwayId, EnumMatchType.Ladder, laddermatch.AwayScore,
                                laddermatch.HomeScore, laddermatch.AwayScore);
                            //ActivityExThread.Instance.Ladder(laddermatch.AwayId, laddermatch.AwayLadderScore + laddermatch.PrizeAwayScore, awayWinType);
                            //Games.NBall.Core.Guild.GuildMessage.Instance().LadderActive(laddermatch.AwayId);
                            //ActiveCore.Instance.AddActive(laddermatch.AwayId, EnumActiveType.Ladder, 1);

                            //记录成就相关数据
                            AchievementTaskCore.Instance.UpdateLadderGoals(laddermatch.AwayId, laddermatch.AwayScore,
                                (EnumWinType) awayWinType, laddermatch.AwayLadderScore + laddermatch.PrizeAwayScore);
                            if (laddermatch.AwayIsHook)
                            {
                                LadderThread.Instance.UpdateHookScore(laddermatch.AwayId, laddermatch.PrizeAwayScore,
                                    laddermatch.AwayScore < laddermatch.HomeScore, laddermatch.HomeScore,
                                    laddermatch.AwayScore, laddermatch.HomeName, laddermatch.AwayName,
                                    laddermatch.AwayCoin);
                            }
                        }
                        if (!laddermatch.HomeIsBot)
                        {
                            var manager = ManagerCore.Instance.GetManager(laddermatch.HomeId);
                            if (manager != null)
                            {
                                int cd = LadderCore.Instance.LadderNotVipMatchCD;
                                if (manager.VipLevel > 0)
                                    cd = LadderCore.Instance.LadderVipMatchCD;
                                if (!LadderCore.Instance._ManagerMatchCD.ContainsKey(laddermatch.HomeId)) //加cd
                                    LadderCore.Instance._ManagerMatchCD.TryAdd(laddermatch.HomeId,
                                        DateTime.Now.AddSeconds(cd));
                                else
                                    LadderCore.Instance._ManagerMatchCD[laddermatch.HomeId] = DateTime.Now.AddSeconds(cd);
                            }
                        }
                        if (!laddermatch.AwayIsBot)
                        {
                            var manager = ManagerCore.Instance.GetManager(laddermatch.AwayId);
                            if (manager != null)
                            {
                                int cd = LadderCore.Instance.LadderNotVipMatchCD;
                                if (manager.VipLevel > 0)
                                    cd = LadderCore.Instance.LadderVipMatchCD;
                                if (!LadderCore.Instance._ManagerMatchCD.ContainsKey(laddermatch.AwayId)) //加cd
                                    LadderCore.Instance._ManagerMatchCD.TryAdd(laddermatch.AwayId,
                                        DateTime.Now.AddSeconds(cd));
                                else
                                    LadderCore.Instance._ManagerMatchCD[laddermatch.AwayId] = DateTime.Now.AddSeconds(cd);
                            }
                        }

                    }
                    MemcachedFactory.LadderMatchClient.Set<LadderMatchEntity>(laddermatch.Idx, laddermatch);
                }
                matchData = null;
                laddermatch = null;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("天梯比赛", ex);
            }
            if (laddermatch != null)
            {
                MemcachedFactory.LadderMatchClient.Delete(laddermatch.Idx);
                var match = LadderMatchMgr.GetById(laddermatch.Idx);
                MemcachedFactory.LadderMatchClient.Set(laddermatch.Idx, match);
            }
        }

        static void AddManagerData(Guid managerId, int prizeExp, int prizeCoin, int prizeSophisticate,
            EnumCoinChargeSourceType coinSourceType, string coinOrderId)
        {
            NbManagerEntity homeManager = ManagerCore.Instance.GetManager(managerId);
            ManagerUtil.AddManagerData(homeManager, prizeExp, prizeCoin, prizeSophisticate, coinSourceType, coinOrderId);
            ManagerUtil.SaveManagerData(homeManager);
            ManagerUtil.SaveManagerAfter(homeManager, true);
        }

        EnumWinType CalWinType(int homeScore, int awayScore)
        {
            if (homeScore > awayScore)
                return EnumWinType.Win;
            else if (homeScore == awayScore)
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
        int CalPrizePoint(LadderMatchEntity laddermatch)
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

            laddermatch.PrizeHomeScore = OnlineCore.Instance.CalIndulgeLadderScore(laddermatch.HomeId, laddermatch.PrizeHomeScore);
            laddermatch.PrizeAwayScore = OnlineCore.Instance.CalIndulgeLadderScore(laddermatch.AwayId, laddermatch.PrizeAwayScore);
            //加入防沉迷
            #region 加入防沉迷
            /*
            try
            {
                var homeIndulge = IndulgeMgr.GetManagerIndulgeStatus(laddermatch.HomeId);
                if (homeIndulge == 3)
                {
                    laddermatch.PrizeHomeScore = laddermatch.PrizeHomeScore / 2;
                }
                else if (homeIndulge == 5)
                {
                    laddermatch.PrizeHomeScore = 0;
                }
                var awayIndulge = IndulgeMgr.GetManagerIndulgeStatus(laddermatch.AwayId);
                if (awayIndulge == 3)
                {
                    laddermatch.PrizeAwayScore = laddermatch.PrizeAwayScore / 2;
                }
                else if (awayIndulge == 5)
                {
                    laddermatch.PrizeAwayScore = 0;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CalPrizePoint-Indulge", ex.Message, ex.StackTrace);
            }
             */
            #endregion

            //SysteminfologMgr.Insert("ArenaProcess-CalPrizePoint"
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
                SystemlogMgr.Error("ArenaProcess-BuildFightInfo", ex.Message, ex.StackTrace);
                return -1;
            }
        }
    }
}
