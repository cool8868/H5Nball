using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Ladder;

namespace Games.NBall.Core.CrossLadder
{
    public partial class CrossLadderThread
    {
        #region RunLadder
        /// <summary>
        /// Runs the arena.
        /// </summary>
        void RunLadder()
        {
            _needClearFightDic = false;
            CrossladderInfoEntity ladderInfo = GetCompetitorToMatch();

            //更新最近一次平均等待时间
            var avgWaitTime = CalAvgWaitSecond(ladderInfo.GroupingTime, ladderInfo.FightList);
            RecentlyAvgWaitSecond = avgWaitTime;

            //开始获取机器人
            BuildBot(ladderInfo);
            var playerNumber = ladderInfo.FightList.Count;
            if (playerNumber % 4 != 0)//检查玩家数量是否是4的倍数
            {
                //将天梯赛服务状态置为结束
                _status = EnumLadderStatus.End;
                //将上一轮比赛的经理池清空，暂时没有异常回退方案
                ManagerFightDic = new Dictionary<Guid, CrossLadderHeartEntity>();
                SystemlogMgr.Info("CrossLadderThread", "The player is " + playerNumber.ToString() + " not multiple of 4");
                return;
            }
            //开始分组
            var fightDic = new Dictionary<Guid, CrossladderMatchEntity>();

            Grouping(ladderInfo, fightDic);
            LadderRunEnd();
            //将天梯赛数据扔到Process中
            ladderInfo.CountdownTime = DateTime.Now;
            ladderInfo.PlayerNumber = playerNumber;
            ladderInfo.AvgWaitTime = avgWaitTime;

            _nbThreadPool.Add(() => RunMatch(fightDic, ladderInfo));
        }

        void LadderRunEnd()
        {
            //将天梯赛服务状态置为结束
            _status = EnumLadderStatus.End;
            _needClearFightDic = true;
            timer2.Interval = ClearFightdicTime; //启动清除上一轮比赛池计时器
            timer2.Start();
        }

        /// <summary>
        /// Cals the avg wait time.
        /// </summary>
        /// <returns></returns>
        int CalAvgWaitSecond(DateTime groupTime, List<CrossladderManagerEntity> fightList)
        {
            try
            {
                var totalWait = 0.0;
                int playerCout = 0;
                foreach (var item in fightList)
                {
                    if (item.IsBot == false)
                    {
                        playerCout++;
                        totalWait += groupTime.Subtract(item.RowTime).TotalSeconds;
                    }
                }
                return Convert.ToInt32(totalWait / playerCout);
            }
            catch
            {
                return 60;
            }

        }

        #endregion

        #region Grouping
        /// <summary>
        /// Groupings the specified arena ladder.
        /// </summary>
        /// <param name="ladderInfo">The arena ladder.</param>
        /// <param name="fightDic">The fight dic.</param>
        private void Grouping(CrossladderInfoEntity ladderInfo, Dictionary<Guid, CrossladderMatchEntity> fightDic)
        {
            var managerFightList = ladderInfo.FightList;
            managerFightList.Sort(new CompareArenaManager());

            //按4的倍数将玩家分组
            //改成每8个乱序，由于玩家数量是4的倍数
            //如果是奇数组，则先把第一组特殊处理，后面每两组乱序
            var groups = managerFightList.Count / 4;

            int i = 0;
            if (groups % 2 == 1) //奇数
            {
                int[] groupTemplate = GetGroupTemplate();
                BuildFightInfo(managerFightList, ladderInfo.Idx, i, 0, groupTemplate, fightDic);//第0组
                i = 1;
            }
            if (groups > 1)
            {
                int[] groupTemplate = GetGroupTemplate();
                for (; i < groups; i = i + 2)
                {
                    BuildFightInfo(managerFightList, ladderInfo.Idx, i, 0, groupTemplate, fightDic);
                    BuildFightInfo(managerFightList, ladderInfo.Idx, i, 1, groupTemplate, fightDic);
                }
            }
        }

        /// <summary>
        /// Builds the fight info.
        /// </summary>
        /// <param name="managerFightList">The manager fight list.</param>
        /// <param name="ladderId">The ladder id.</param>
        /// <param name="group">The group.</param>
        /// <param name="teamIndex">Index of the team.</param>
        /// <param name="groupTemplate">The group template.</param>
        /// <param name="fightDic">The fight dic.</param>
        private void BuildFightInfo(List<CrossladderManagerEntity> managerFightList, Guid ladderId, int group, int teamIndex, int[] groupTemplate, Dictionary<Guid, CrossladderMatchEntity> fightDic)
        {
            try
            {
                var home1 = managerFightList[group * 4 + groupTemplate[teamIndex * 4 + 0]];
                var away1 = managerFightList[group * 4 + groupTemplate[teamIndex * 4 + 1]];

                var home2 = managerFightList[group * 4 + groupTemplate[teamIndex * 4 + 2]];
                var away2 = managerFightList[group * 4 + groupTemplate[teamIndex * 4 + 3]];
                //组织对战实体
                BuildMatchInfo(ladderId, group, home1, away1, fightDic);

                BuildMatchInfo(ladderId, group, home2, away2, fightDic);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossLadderThread-BuildFightInfo", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Builds the match info.
        /// </summary>
        /// <param name="ladderId">The ladder id.</param>
        /// <param name="group">The group.</param>
        /// <param name="home">The home.</param>
        /// <param name="away">The away.</param>
        /// <param name="fightDic">The fight dic.</param>
        private void BuildMatchInfo(Guid ladderId, int group, CrossladderManagerEntity home, CrossladderManagerEntity away, Dictionary<Guid, CrossladderMatchEntity> fightDic)
        {
            try
            {
                //筛选条件，当两个玩家分差超过阀值，将他们扔到排队池里继续等待
                //int tmpScore = home.Score - away.Score;
                //if (tmpScore <= _arenaLowScore || tmpScore >= _arenaHighScore)
                //{
                //    if (!home.IsBot)
                //        CrossLadderCore.Instance.PushFightToCompetitor(home.Clone());
                //    if (!away.IsBot)
                //        CrossLadderCore.Instance.PushFightToCompetitor(away.Clone());
                //    return;
                //}

                var matchId = ShareUtil.GenerateComb();
                var ladderMatch = new CrossladderMatchEntity(home, away, matchId, ladderId, group + 1);
                ladderMatch.HomeName = ShareUtil.GetCrossManagerNameByZoneId(home.SiteId, home.Name);
                ladderMatch.AwayName = ShareUtil.GetCrossManagerNameByZoneId(away.SiteId, away.Name);
                ladderMatch.DomainId = _domainId;
                fightDic.Add(ladderMatch.Idx, ladderMatch);

                MemcachedFactory.LadderMatchClient.Set(ladderMatch.Idx, ladderMatch);
                //更新经理-比赛关联字典
                if (!home.IsBot)
                {
                    var homeHeart = new CrossLadderHeartEntity(matchId, away.ManagerId, away.SiteId, away.IsBot,away.Kpi);
                    ManagerFightDic[home.ManagerId] = homeHeart;
                    MemcachedFactory.LadderHeartClient.Set(home.ManagerId, homeHeart);
                }
                if (!away.IsBot)
                {
                    var awayHeart = new CrossLadderHeartEntity(matchId, home.ManagerId, home.SiteId, home.IsBot, away.Kpi);
                    ManagerFightDic[away.ManagerId] = awayHeart;
                    MemcachedFactory.LadderHeartClient.Set(away.ManagerId, awayHeart);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossLadderThread-BuildMatchInfo", ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region FilterHighScore
        /// <summary>
        /// 将太高分的玩家筛选出去
        /// </summary>
        private void FilterHighScore(CrossladderInfoEntity ladderInfo)
        {
            //换规则先不用这个了
            //var managerFightList = ladderInfo.FightList;
            //var competitorCount = managerFightList.Count;
            //try
            //{
            //    managerFightList.Sort(new CompareArenaManager());
            //    List<CrossladderManagerEntity> tmpFightList = new List<CrossladderManagerEntity>();
            //    for (int i = competitorCount - 1; i >= 1; ) //最后一个不管
            //    {
            //        if (managerFightList[i].Score - managerFightList[i - 1].Score >= _arenaHighScore)//两两相比
            //        {
            //            CrossLadderCore.Instance.PushFightToCompetitor(managerFightList[i].Clone());
            //            i--;
            //        }
            //        else
            //        {
            //            tmpFightList.Add(managerFightList[i].Clone());
            //            tmpFightList.Add(managerFightList[i-1].Clone());
            //            i = i - 2;
            //        }
            //    }
            //    tmpFightList.Add(managerFightList[0].Clone());
            //    ladderInfo.FightList = tmpFightList;
            //}
            //catch (Exception ex)
            //{
            //    SystemlogMgr.Error("CrossLadderThread-BuildBot", ex.Message, ex.StackTrace);
            //}

        }
        #endregion

        #region BuildBot
        /// <summary>
        /// Builds the bot.
        /// </summary>
        private void BuildBot(CrossladderInfoEntity ladderInfo)
        {
            var managerFightList = ladderInfo.FightList;
            var competitorCount = managerFightList.Count;

            try
            {
                var botCount = 4 - competitorCount % 4;
                if (botCount > 0 && botCount < 4)
                {
                    managerFightList.Sort(new CompareArenaManager());

                    var minScore = managerFightList[0].Score;
                    var maxScore = managerFightList[competitorCount - 1].Score;

                    var botList = CrossladderManagerMgr.GetBot(botCount, minScore, maxScore);
                    if (botList != null)
                    {
                        foreach (var list in botList)
                        {
                            list.Name = _botName;
                            list.ShowName = _botName;
                            list.IsBot = true;
                            managerFightList.Add(list);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossLadderThread-BuildBot", ex.Message, ex.StackTrace);
            }

        }
        #endregion

        #region GroupTemplate
        private int[] GetGroupTemplate()
        {
            int templateIndex = RandomHelper.GetInt32WithoutMax(0, _groupTemplates.Count);
            return _groupTemplates[templateIndex];
        }

        private void InitGroupTemplate()
        {
            _groupTemplates = new Dictionary<int, int[]>();
            _groupTemplates.Add(0, new int[] { 0, 3, 1, 2, 4, 5, 6, 7 });
            _groupTemplates.Add(1, new int[] { 0, 2, 1, 3, 4, 7, 5, 6 });
            _groupTemplates.Add(2, new int[] { 0, 1, 2, 3, 4, 5, 7, 6 });
            _groupTemplates.Add(3, new int[] { 0, 2, 1, 3, 4, 6, 5, 7 });
        }
        #endregion

        #region RunMatch
        void RunMatch(Dictionary<Guid, CrossladderMatchEntity> fightDic, CrossladderInfoEntity ladderInfo)
        {
            var process = new CrossLadderProcess(fightDic, ladderInfo, _ladderProctiveScore);
            process.StartProcess();
        }
        #endregion

        #region Back service
        
        /// <summary>
        /// 将加入本轮天梯赛的经理推进比赛池.
        /// </summary>
        /// <returns></returns>
        public CrossladderInfoEntity GetCompetitorToMatch()
        {
            //将状态置为分组
            var fightList = new List<CrossladderManagerEntity>();
            var arenaLadder = new CrossladderInfoEntity();
            lock (_competitorLock)
            {
                ManagerFightDic = new Dictionary<Guid, CrossLadderHeartEntity>();
                foreach (var dic in CompetitorDic)
                {
                    //将经理推进比赛池
                    ManagerFightDic.Add(dic.Key, null);
                    fightList.Add(dic.Value);
                }
                _status = EnumLadderStatus.Grouping;
                arenaLadder.Idx = ShareUtil.GenerateComb();
                arenaLadder.FightList = fightList;
                arenaLadder.StartTime = _startTime;
                arenaLadder.GroupingTime = DateTime.Now;

                //开始新一轮报名
                CreateLadder();
            }

            return arenaLadder;
        }

        /// <summary>
        /// 将已进入比赛池的经理重新丢回排序池.
        /// </summary>
        /// <param name="arenaCompetitor">The arena competitor.</param>
        public void PushFightToCompetitor(CrossladderManagerEntity arenaCompetitor)
        {
            lock (_competitorLock)
            {
                if (ManagerFightDic != null && ManagerFightDic.ContainsKey(arenaCompetitor.ManagerId))
                {
                    ManagerFightDic.Remove(arenaCompetitor.ManagerId);
                    if (!CompetitorDic.ContainsKey(arenaCompetitor.ManagerId))
                    {
                        if (_playerNum == 0)
                            _startTime = DateTime.Now;
                        CompetitorDic.Add(arenaCompetitor.ManagerId, arenaCompetitor);
                        _playerNum++;
                    }
                }

            }
        }
        #endregion
    }
}
