using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Match;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Crowd;

namespace Games.NBall.Core.CrossCrowd
{
    public partial class CrossCrowdThread
    {
        void RunPair()
        {
            try
            {
                if (CompetitorDic.Count < 2)
                    return;
                _needClearFightDic = false;
                _status = EnumLadderStatus.Grouping;
                CrosscrowdPairrecordEntity pairRecord = GetCompetitorToMatch();

                var playerNumber = pairRecord.FightList.Count;
                if (playerNumber % 2 != 0)//检查玩家数量是否是2的倍数
                {
                    //将天梯赛服务状态置为结束
                    _status = EnumLadderStatus.End;
                    //将上一轮比赛的经理池清空，暂时没有异常回退方案
                    ManagerFightDic = new Dictionary<Guid, CrowdHeartEntity>();
                    SystemlogMgr.Info("CrowdThread", "The player is " + playerNumber + " not multiple of 2");
                    return;
                }
                //开始分组
                var matchDic = new Dictionary<Guid, CrosscrowdMatchEntity>();

                Grouping(pairRecord, matchDic);
                RunPairAfter();

                _nbThreadPool.Add(() => RunMatch(matchDic, pairRecord));
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowd-RunPair", ex);
            }
            
        }

        void RunMatch(Dictionary<Guid, CrosscrowdMatchEntity> matchDic, CrosscrowdPairrecordEntity pairRecord)
        {
            _crowdProcess = new CrossCrowdProcess(matchDic, pairRecord, _crowdResurrectionCd, _crowdCd, doClearFightDic,_domainId);
            _crowdProcess.StartProcess();
        }

        CrosscrowdPairrecordEntity GetCompetitorToMatch()
        {
            var crowdPair = new CrosscrowdPairrecordEntity();
            lock (_competitorLock)
            {
                var totalCount = CompetitorDic.Count;
                var playerCount = totalCount;
                if (totalCount % 2 == 1)
                    playerCount = totalCount - 1;
                var fightList = new List<CrosscrowdManagerEntity>(playerCount);
                ManagerFightDic = new Dictionary<Guid, CrowdHeartEntity>(playerCount);
                int i = 0;
                CrosscrowdManagerEntity outManager = null;
                foreach (var dic in CompetitorDic)
                {
                    i++;
                    if (i <= playerCount)
                    {
                        //将经理推进比赛池
                        ManagerFightDic.Add(dic.Key, null);
                        fightList.Add(dic.Value);
                    }
                    else if (i == totalCount)
                    {
                        outManager = dic.Value;
                    }
                }
                _crowdInfo.PairCount++;
                crowdPair.FightList = fightList;
                crowdPair.PairIndex = _crowdInfo.PairCount;
                crowdPair.PlayerCount = playerCount;
                crowdPair.CrossCrowdId = _crowdInfo.Idx;
                PairReady(outManager);

            }
            return crowdPair;
        }

        void PairReady(CrosscrowdManagerEntity outManager)
        {
            _startTime = DateTime.Now;
            CompetitorDic = new ConcurrentDictionary<Guid, CrosscrowdManagerEntity>();
            if (outManager != null)
            {
                CompetitorDic.TryAdd(outManager.ManagerId, outManager);
            }
        }

        void RunPairAfter()
        {
            CrosscrowdInfoMgr.Update(_crowdInfo);
            //将服务状态置为结束
            _status = EnumLadderStatus.Running;
            _needClearFightDic = true;
            _clearTimer.Interval = ClearFightdicTime; //启动清除上一轮比赛池计时器
            _clearTimer.Start();
        }

        void Grouping(CrosscrowdPairrecordEntity pairRecord, Dictionary<Guid, CrosscrowdMatchEntity> matchDic)
        {
            var managerFightList = pairRecord.FightList;

            var totalCount = managerFightList.Count;
            int totalLoopCount = totalCount;
            int index = 0;
            while (totalCount > 1 && totalLoopCount > 0)
            {
                var template = GetGroupTemplate(totalCount);
                var loopCount = template.Length;
                for (int i = 0; i < loopCount; i = i + 2)
                {
                    BuildFightInfo(pairRecord.CrossCrowdId, pairRecord.PairIndex, managerFightList, index, i, template, matchDic);
                }
                index = index + template.Length;
                totalCount = totalCount - template.Length;
                totalLoopCount--;
            }
        }

        private void BuildFightInfo(int crowdId, int pairIndex, List<CrosscrowdManagerEntity> managerFightList, int index, int loopCount, int[] groupTemplate, Dictionary<Guid, CrosscrowdMatchEntity> matchDic)
        {
            try
            {
                var home1 = managerFightList[index + groupTemplate[loopCount + 0]];
                var away1 = managerFightList[index + groupTemplate[loopCount + 1]];
                //组织对战实体
                BuildMatchInfo(crowdId, pairIndex, home1, away1, matchDic);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowdThread-BuildFightInfo", ex.Message, ex.StackTrace);
            }
        }

        private void BuildMatchInfo(int crowdId, int pairIndex, CrosscrowdManagerEntity home, CrosscrowdManagerEntity away, Dictionary<Guid, CrosscrowdMatchEntity> matchDic)
        {
            try
            {
                var matchId = ShareUtil.GenerateComb();
                var match = new CrosscrowdMatchEntity(home, away, matchId, crowdId, pairIndex);
                matchDic.Add(match.Idx, match);

                MemcachedFactory.CrowdMatchClient.Set(match.Idx, match);
                //更新经理-比赛关联字典
                var homeHeart = new CrowdHeartEntity(matchId, away.ShowName,away.Logo,away.Morale,away.ManagerId,away.SiteId);
                var awayHeart = new CrowdHeartEntity(matchId, home.ShowName, home.Logo, home.Morale, home.ManagerId, home.SiteId);
                ManagerFightDic[home.ManagerId] = homeHeart;
                ManagerFightDic[away.ManagerId] = awayHeart;
                //try
                //{
                //    CrossChatHelper.SendCrowdPairPop(home.ManagerId, homeHeart, home.SiteId);
                //    CrossChatHelper.SendCrowdPairPop(away.ManagerId, awayHeart, away.SiteId);
                //}
                //catch (Exception ex)
                //{
                //    SystemlogMgr.Error("CrossCrowdThread-SendCrowdPairPop", ex);
                //}
                MatchCdHandler.SetCd(home.ManagerId, EnumMatchType.CrossCrowd, _crowdCd);
                MatchCdHandler.SetCd(away.ManagerId, EnumMatchType.CrossCrowd, _crowdCd);
                try
                {
                    MemcachedFactory.CrowdHeartClient.Set(home.ManagerId, homeHeart);
                    MemcachedFactory.CrowdHeartClient.Set(away.ManagerId, awayHeart);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("CrossCrowdThread-BuildMatchInfo-Send", ex);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowdThread-BuildMatchInfo", ex);
            }
        }

        #region GroupTemplate
        private int[] GetGroupTemplate(int count)
        {
            int templateIndex = 0;
            if (count >= 8)
            {
                count = 8;
                templateIndex = RandomHelper.GetInt32WithoutMax(0, 4);
            }
            else if (count >= 4)
            {
                count = 4;
                templateIndex = RandomHelper.GetInt32WithoutMax(0, 4);
            }
            else
            {
                count = 2;
            }
            return _groupTemplates[count][templateIndex];
        }

        private void InitGroupTemplate()
        {
            _groupTemplates = new Dictionary<int, Dictionary<int, int[]>>(3);
            _groupTemplates.Add(8, new Dictionary<int, int[]>(4));
            _groupTemplates.Add(4, new Dictionary<int, int[]>(4));
            _groupTemplates.Add(2, new Dictionary<int, int[]>(2));
            _groupTemplates[8].Add(0, new int[] { 0, 4, 5, 3, 1, 2, 6, 7 });
            _groupTemplates[8].Add(1, new int[] { 1, 3, 4, 5, 7, 0, 2, 6 });
            _groupTemplates[8].Add(2, new int[] { 0, 5, 7, 1, 2, 4, 3, 6 });
            _groupTemplates[8].Add(3, new int[] { 2, 0, 1, 3, 4, 6, 5, 7 });

            _groupTemplates[4].Add(0, new int[] { 0, 3, 1, 2 });
            _groupTemplates[4].Add(1, new int[] { 0, 2, 1, 3 });
            _groupTemplates[4].Add(2, new int[] { 0, 1, 3, 2 });
            _groupTemplates[4].Add(3, new int[] { 1, 2, 0, 3 });

            _groupTemplates[2].Add(0, new int[] { 0, 1 });
        }
        #endregion
    }
}
