using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
//using Games.NBall.Core.Match;
using Games.NBall.Core.Manager;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.Entity.Share;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Ladder
{
    public class LadderThread
    {
        #region Fields
        private List<ArenaCondition> _conditions;
        //clearFightlist锁
        private object _clearFightlistLock = new object();

        //分组模板
        private Dictionary<int, int[]> _groupTemplates;

        private System.Timers.Timer timer2;
        private const double ClearFightdicTime = 60000;//清空上一轮比赛池时间，毫秒

        private string _botName = "";
        private NBThreadPool _nbThreadPool;

        public int _ladderProctiveScore; //新手保护分数线

        private bool _needClearFightDic = false;
        
        private int _ladderHookCD;
        private int _hookExpired;
        private ConcurrentDictionary<Guid, LadderHookEntity> _hookDic;
        private List<LadderHookEntity> _finishList;
        /// <summary>
        /// 挂机记录
        /// </summary>
        private ConcurrentDictionary<Guid, List<LadderHook>> _hookListDic;

        private LadderMatchMarqueeResponse _matchMarqueeResponse = ResponseHelper.CreateSuccess<LadderMatchMarqueeResponse>();
        #endregion

        #region .ctor
        public LadderThread(int p)
        {
            Initialize();
        }
        #endregion

        #region Initialize

        void Initialize()
        {
            _hookListDic = new ConcurrentDictionary<Guid, List<LadderHook>>();
            _ladderHookCD = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderHookCD);
            _hookExpired = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderHookExpired);
            _botName = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.BotName);
            _ladderProctiveScore = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderProctiveScore);
            //初始化分组模板
            InitGroupTemplate();
            LadderCore.Instance.CreateLadder();
            #region Init Condition dic
            string condition = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.LadderMatchCondition);
            _conditions = new List<ArenaCondition>();
            if (!string.IsNullOrEmpty(condition))
            {
                string[] tempConditions = condition.Split('|');
                foreach (var s in tempConditions)
                {
                    string[] temp = s.Split(',');
                    _conditions.Add(new ArenaCondition(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1])));
                }
            }
            #endregion

            _nbThreadPool = new NBThreadPool(5);
            timer2 = new Timer { Interval = ClearFightdicTime };
            timer2.Elapsed += new ElapsedEventHandler(ClearFightDic);
            CreateHook();
        }

        private void CreateHook()
        {
            var list = LadderHookMgr.GetList();
            _hookDic = new ConcurrentDictionary<Guid, LadderHookEntity>();
            foreach (var entity in list)
            {
                if (HookEndCheck(entity))
                    HookEnd(entity.ManagerId, EnumHookStatus.Exception);
                else
                {
                    AddToDic(entity);
                }
            }
        }

        #endregion

        #region Facade

        public static LadderThread Instance
        {
            get { return SingletonFactory<LadderThread>.SInstance; }
        }

        public MessageCode CheckStatusJob()
        {
            try
            {
                CheckStatus();
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CheckStatusJob", ex);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 停止所有挂机
        /// </summary>
        /// <returns></returns>
        public MessageCode StopHookJob()
        {
            try
            {
                var hooklist = _hookDic.Values.ToList();
                foreach (var item in hooklist)
                {
                    try
                    {
                        if (HookEnd(item.ManagerId, EnumHookStatus.Stop))
                        {
                            LadderCore.Instance.LeaveLadder(item.ManagerId);
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemlogMgr.Error("停止天梯赛挂机", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CheckStatusJob", ex);
                return MessageCode.Exception;
            }
            CreateHook();
            return MessageCode.Success;
        }

        public MessageCode ScoreToHonorJob()
        {
            var season = CacheFactory.SeasonCache.GetCurrentSeason();
            if (season == null)
            {
                return MessageCode.LadderNoSeason;
            }

            var curDate = DateTime.Today;
            if (season.Idx == 1 && curDate == season.Startdate)
                return MessageCode.LadderSeasonDonotNeedSend;
            int isNewSeason = 0;
            int curSeason = season.Idx;
            if (season.Startdate == curDate && season.Idx > 1)
            {
                isNewSeason = 1;
                curSeason = curSeason - 1;
            }
            LadderInfoMgr.ScoreToHonor(DateTime.Today, curSeason, isNewSeason);
            if (isNewSeason == 1)
            {
                try
                {
                    var curSeasonEntity = CacheFactory.SeasonCache.GetEntity(curSeason);
                    List<LadderManagerhistoryEntity> managers = null;
                    if (curSeasonEntity.Status == 1)
                    {
                        managers = LadderManagerhistoryMgr.GetPrizeManager(curSeason);
                    }
                    else
                    {
                        managers = LadderManagerhistoryMgr.GetPrizeManagerAll(curSeason);
                    }
                    if (managers != null)
                    {
                        List<MailBuilder> mails = new List<MailBuilder>(managers.Count);
                        foreach (var manager in managers)
                        {
                            SendPrize(manager, curSeasonEntity.Status, ref mails);
                            if (manager.Rank < 3)
                            {
                                NbManagerhonorMgr.Add(manager.ManagerId, (int)EnumMatchType.Ladder, 0,
                                    manager.Season,
                                    manager.Rank);
                            }
                        }

                        var mailDataTable = MailCore.BuildMailBulkTable(mails);
                        LadderSqlHelper.SaveManagerPrize(managers, mailDataTable);
                    }

                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("ScoreToHonorJob SendPrize", ex);
                    return MessageCode.Exception;
                }
            }
            return MessageCode.Success;
        }
        /// <summary>
        /// 合区所需的提前发放天梯赛奖励
        /// </summary>
        /// <returns></returns>
        public MessageCode ScoreToHonorJobMergeZone()
        {
            var season = CacheFactory.SeasonCache.GetCurrentSeason();
            if (season == null)
            {
                return MessageCode.LadderNoSeason;
            }

            var curDate = DateTime.Today;
            if (season.Idx == 1 && curDate == season.Startdate)
                return MessageCode.LadderSeasonDonotNeedSend;
            int isNewSeason = 1;
            int curSeason = season.Idx;
            if (season.Startdate == curDate && season.Idx > 1)
            {
                isNewSeason = 1;
                curSeason = curSeason - 1;
            }
            LadderInfoMgr.ScoreToHonorMergeZone(DateTime.Today, curSeason);
            if (isNewSeason == 1)
            {
                try
                {
                    var curSeasonEntity = CacheFactory.SeasonCache.GetEntity(curSeason);
                    var managers = LadderManagerhistoryMgr.GetPrizeManager(curSeason);
                    if (managers != null)
                    {
                        List<MailBuilder> mails = new List<MailBuilder>(managers.Count);
                        foreach (var manager in managers)
                        {
                            SendPrize(manager, curSeasonEntity.Status, ref mails);
                            if (manager.Rank < 3)
                            {
                                NbManagerhonorMgr.Add(manager.ManagerId, (int)EnumMatchType.Ladder, 0, manager.Season,
                                                        manager.Rank);
                            }
                        }

                        var mailDataTable = MailCore.BuildMailBulkTable(mails);
                        LadderSqlHelper.SaveManagerPrize(managers, mailDataTable);
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("ScoreToHonorJob SendPrize", ex);
                    return MessageCode.Exception;
                }
            }
            return MessageCode.Success;
        }

        public MessageCode HookJob()
        {
            try
            {
                _finishList = new List<LadderHookEntity>();

                foreach (var entity in _hookDic.Values)
                {
                    Hook(entity);
                }
                doFinish();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("HookJob", ex);
                return MessageCode.Exception;
            }

            return MessageCode.Success;
        }

        public void SendPrize(LadderManagerhistoryEntity manager, int seasonStatus, ref List<MailBuilder> mails)
        {
            try
            {
                if (string.IsNullOrEmpty(manager.PrizeItems))
                {
                    manager.PrizeItems = "";
                    MailBuilder mail = null;
                    if (seasonStatus == 1)
                    {
                        //var lotterys = CacheFactory.LadderCache.GetRankPrize(manager.Rank);
                        //mail = new MailBuilder(manager, lotterys);
                        //foreach (var entity in lotterys)
                        //{
                        //    manager.PrizeItems += string.Format("{0},{1},{2}|", entity.PrizeItemCode,
                        //        entity.Strength, entity.IsBinding ? 1 : 0);
                        //}
                        int packId = CacheFactory.LadderCache.GetRankPrizeNew(manager.Rank);
                        if (packId <= 0)
                        {
                            SystemlogMgr.Info("LadderSendPrize", "no packid for rank:" + manager.Rank);
                            return;
                        }
                        mail = new MailBuilder(manager);
                        var code = MallCore.Instance.BuildPackMail(packId, ref mail);
                        if (code != MessageCode.Success)
                        {
                            SystemlogMgr.Info("LadderSendPrize", "build pack fail rank:" + manager.Rank + ",packId:" + packId);
                            return;
                        }
                        manager.PrizeItems = "pack:" + packId;
                    }
                    else
                    {
                        int packId = CacheFactory.LadderCache.GetRankPrizeSevenNew(manager.Rank);
                        if (packId <= 0)
                        {
                            SystemlogMgr.Info("LadderSendPrize", "no packid for rank:" + manager.Rank);
                            return;
                        }
                        mail = new MailBuilder(manager);
                        var code = MallCore.Instance.BuildPackMail(packId, ref mail);
                        if (code != MessageCode.Success)
                        {
                            SystemlogMgr.Info("LadderSendPrize", "build pack fail rank:" + manager.Rank + ",packId:" + packId);
                            return;
                        }
                        manager.PrizeItems = "pack:" + packId;
                    }
                    manager.PrizeItems = manager.PrizeItems.TrimEnd('|');
                    manager.UpdateTime = DateTime.Now;
                    mails.Add(mail);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("LadderSendPrize", ex);
            }
        }

        public MessageCode GetMatchMarqueeJob()
        {
            try
            {
                _matchMarqueeResponse = ResponseHelper.CreateSuccess<LadderMatchMarqueeResponse>();
                _matchMarqueeResponse.Data = new LadderMatchMarqueeData();
                _matchMarqueeResponse.Data.Matchs = LadderMatchMgr.GetMatchTop10();
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetMatchMarqueeJob", ex);
                return MessageCode.Exception;
            }
        }
        #endregion

        public LadderMatchMarqueeResponse GetMatchMarqueeResponse()
        {
            return _matchMarqueeResponse;
        }

        #region ClearFightDic
        void ClearFightDic(object sender, ElapsedEventArgs e)
        {
            timer2.Stop();
            if (!_needClearFightDic)
                return;
            try
            {
                //定时将上一轮比赛的经理池清空，以免经理没办法参加下一轮比赛
                LadderCore.Instance.ManagerFightDic = new Dictionary<Guid, Guid>();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("LadderThread-ClearFightdic", ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region CheckStatus
        /// <summary>
        /// 检查是否达到进入配对条件.
        /// </summary>
        void CheckStatus()
        {
            if (LadderCore.Instance.Status == EnumLadderStatus.Grouping)//前一轮天梯赛还在分组中，请勿打扰
                return;

            double timeInterval = DateTime.Now.Subtract(LadderCore.Instance.StartTime).TotalSeconds;
            int playerNumber = LadderCore.Instance.CompetitorDic.Count;

            //SystemlogMgr.Info("LadderThread-CheckStatus", string.Format("当前天梯赛状态：时间{0}s,人数{1}", timeInterval, playerNumber));

            foreach (var condition in _conditions)
            {
                if (timeInterval >= condition.WaitTime && playerNumber >= condition.PlayerNumber)
                {
                    timer2.Stop();
                    RunLadder();
                    break;
                }
            }
        }
        #endregion

        #region RunLadder
        /// <summary>
        /// Runs the arena.
        /// </summary>
        void RunLadder()
        {
            _needClearFightDic = false;
            LadderInfoEntity ladderInfo = LadderCore.Instance.GetCompetitorToMatch();

            //更新最近一次平均等待时间
            var avgWaitTime = CalAvgWaitSecond(ladderInfo.GroupingTime, ladderInfo.FightList);
            LadderCore.Instance.RecentlyAvgWaitSecond = avgWaitTime;

            //开始获取机器人
            BuildBot(ladderInfo);
            var playerNumber = ladderInfo.FightList.Count;
            if (playerNumber % 4 != 0)//检查玩家数量是否是4的倍数
            {
                //将天梯赛服务状态置为结束
                LadderCore.Instance.Status = EnumLadderStatus.End;
                //将上一轮比赛的经理池清空，暂时没有异常回退方案
                LadderCore.Instance.ManagerFightDic = new Dictionary<Guid, Guid>();
                SystemlogMgr.Info("LadderThread", "The player is " + playerNumber.ToString() + " not multiple of 4");
                return;
            }
            //开始分组
            var fightDic = new ConcurrentDictionary<Guid, LadderMatchEntity>();

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
            LadderCore.Instance.Status = EnumLadderStatus.End;
            _needClearFightDic = true;
            timer2.Interval = ClearFightdicTime; //启动清除上一轮比赛池计时器
            timer2.Start();
        }

        /// <summary>
        /// Cals the avg wait time.
        /// </summary>
        /// <returns></returns>
        int CalAvgWaitSecond(DateTime groupTime, List<LadderManagerEntity> fightList)
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
        private void Grouping(LadderInfoEntity ladderInfo, ConcurrentDictionary<Guid, LadderMatchEntity> fightDic)
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
        private void BuildFightInfo(List<LadderManagerEntity> managerFightList, Guid ladderId, int group, int teamIndex, int[] groupTemplate, ConcurrentDictionary<Guid, LadderMatchEntity> fightDic)
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
                SystemlogMgr.Error("LadderThread-BuildFightInfo", ex.Message, ex.StackTrace);
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
        private void BuildMatchInfo(Guid ladderId, int group, LadderManagerEntity home, LadderManagerEntity away, ConcurrentDictionary<Guid, LadderMatchEntity> fightDic)
        {
            try
            {
                //筛选条件，当两个玩家分差超过阀值，将他们扔到排队池里继续等待
                //int tmpScore = home.Score - away.Score;
                //if (tmpScore <= _arenaLowScore || tmpScore >= _arenaHighScore)
                //{
                //    if (!home.IsBot)
                //        LadderCore.Instance.PushFightToCompetitor(home.Clone());
                //    if (!away.IsBot)
                //        LadderCore.Instance.PushFightToCompetitor(away.Clone());
                //    return;
                //}

                var matchId = ShareUtil.GenerateComb();
                var ladderMatch = new LadderMatchEntity(home, away, matchId, ladderId, group + 1);
                fightDic.TryAdd(ladderMatch.Idx, ladderMatch);

                MemcachedFactory.LadderMatchClient.Set(ladderMatch.Idx, ladderMatch);
                //更新经理-比赛关联字典
                if (!home.IsBot)
                    LadderCore.Instance.ManagerFightDic[home.ManagerId] = matchId;
                if (!away.IsBot)
                    LadderCore.Instance.ManagerFightDic[away.ManagerId] = matchId;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("LadderThread-BuildMatchInfo", ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region FilterHighScore
        /// <summary>
        /// 将太高分的玩家筛选出去
        /// </summary>
        private void FilterHighScore(LadderInfoEntity ladderInfo)
        {
            //换规则先不用这个了
            //var managerFightList = ladderInfo.FightList;
            //var competitorCount = managerFightList.Count;
            //try
            //{
            //    managerFightList.Sort(new CompareArenaManager());
            //    List<LadderManagerEntity> tmpFightList = new List<LadderManagerEntity>();
            //    for (int i = competitorCount - 1; i >= 1; ) //最后一个不管
            //    {
            //        if (managerFightList[i].Score - managerFightList[i - 1].Score >= _arenaHighScore)//两两相比
            //        {
            //            LadderCore.Instance.PushFightToCompetitor(managerFightList[i].Clone());
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
            //    SystemlogMgr.Error("LadderThread-BuildBot", ex.Message, ex.StackTrace);
            //}

        }
        #endregion

        #region BuildBot
        /// <summary>
        /// Builds the bot.
        /// </summary>
        private  void BuildBot(LadderInfoEntity ladderInfo)
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

                    var botList = LadderManagerMgr.GetBot(botCount, minScore, maxScore);
                    if (botList != null)
                    {
                        foreach (var list in botList)
                        {
                            list.Name = _botName;
                            list.IsBot = true;
                            managerFightList.Add(list);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("LadderThread-BuildBot", ex.Message, ex.StackTrace);
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
        void RunMatch(ConcurrentDictionary<Guid, LadderMatchEntity> fightDic, LadderInfoEntity ladderInfo)
        {
            var process = new LadderProcess(fightDic, ladderInfo, _ladderProctiveScore);
            process.StartProcess();
        }
        #endregion

        #region Hook
        public LadderHookInfoResponse GetHookInfoResponse(Guid managerId)
        {
            LadderHookEntity hook = null;
            _hookDic.TryGetValue(managerId, out hook);
            return GetHookInfoResponse(managerId, hook, true);
        }

        public LadderHookInfoResponse GetHookInfoResponse(Guid managerId, LadderHookEntity hook, bool isHook = false)
        {
            if (hook != null)
            {
                if (hook.Status >= (int)EnumHookStatus.Run)
                {
                    return BuildHookInfoResponse(hook, isHook);
                }
            }
            var response = ResponseHelper.CreateSuccess<LadderHookInfoResponse>();
            response.Data = new LadderHookInfoEntity();
            return response;

        }

        public LadderHookInfoResponse StartHook(Guid managerId, int maxTimes, int minScore, int maxScore,int winTimes)
        {
            if (maxTimes < 1 && minScore < 1 && maxScore < 1 && winTimes < 1)
            {
                return ResponseHelper.Create<LadderHookInfoResponse>(MessageCode.NbParameterError);
            }
            if (IsExists(managerId))
            {
                return ResponseHelper.Create<LadderHookInfoResponse>(MessageCode.Success);
            }

            var hook = LadderHookMgr.GetById(managerId);
            var nextMatchTime = DateTime.Now.AddSeconds(_ladderHookCD);
            var curTime = DateTime.Now;
            hook.UpdateTime = curTime;
            hook.RowTime = curTime;
            hook.NextMatchTime = nextMatchTime;
            hook.Status = 0;
            hook.CurTimes = 0;
            hook.CurWiningTimes = 0;
            hook.MaxTimes = maxTimes;
            hook.MinScore = minScore;
            hook.MaxScore = maxScore;
            hook.MaxWiningTimes = winTimes;
            hook.Expired = curTime.AddMinutes(_hookExpired);
            if (LadderHookMgr.Update(hook))
            {
                AddToDic(hook);
                return BuildHookInfoResponse(hook, true);
            }
            else
            {
                return ResponseHelper.Create<LadderHookInfoResponse>(MessageCode.NbUpdateFail);
            }
        }

        public MessageCodeResponse StopHook(Guid managerId)
        {
            if (!IsExists(managerId))
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
            }
            if (HookEnd(managerId, EnumHookStatus.Stop))
            {
                LadderCore.Instance.LeaveLadder(managerId);
                return ResponseHelper.CreateSuccess<MessageCodeResponse>();
            }
            return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail);
        }

        public bool IsExists(Guid managerId)
        {
            return _hookDic.ContainsKey(managerId);
        }
        /// <summary>
        /// 更新挂机信息
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="score">获得积分</param>
        /// <param name="isWin">是否胜利</param>
        /// <param name="homtScore">主队进球数</param>
        /// <param name="awayScore">客队进球数</param>
        /// <param name="homeName">主队名</param>
        /// <param name="awayName">客队名</param>
        /// <param name="myCoin">获得金币</param>
        public void UpdateHookScore(Guid managerId, int score,bool isWin,int homtScore,int awayScore,string homeName,string awayName,int myCoin)
        {
            try
            {
                LadderHookEntity entity = null;
                _hookDic.TryGetValue(managerId, out entity);
                if (entity != null)
                {
                    if (entity.LadderManager != null)
                    {
                        entity.LadderManager.Score += score;
                        entity.Score = entity.LadderManager.Score;
                    }
                    else
                    {
                        entity.Score += score;
                    }
                    entity.CurTimes++;
                    if (isWin)
                        entity.CurWiningTimes++;
                    try
                    {

                    if (_hookListDic == null)
                        _hookListDic = new ConcurrentDictionary<Guid, List<LadderHook>>();
                    if (!_hookListDic.ContainsKey(managerId))
                        _hookListDic.TryAdd(managerId, new List<LadderHook>());
                    if (_hookListDic[managerId].Count >= 10)
                        _hookListDic[managerId].RemoveAt(0);
                    LadderHook hookrecord = new LadderHook();
                    hookrecord.HomeName = homeName;
                    hookrecord.AwayName = awayName;
                    hookrecord.HomeScore = homtScore;
                    hookrecord.AwayScore = awayScore;
                    hookrecord.MyCoin = myCoin;
                    hookrecord.MyIntegral = score;
                    _hookListDic[managerId].Add(hookrecord);
                    }
                    catch (Exception e)
                    {

                    }
                    if (HookEndCheck(entity))
                    {
                        HookEnd(managerId, entity.Status);
                    }
                    else
                    {
                        LadderHookMgr.Update(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("UpdateLadderHookInfo", ex);
            }
        }

        void Hook(LadderHookEntity entity)
        {
            if (HookEndCheck(entity))
            {
                _finishList.Add(entity);
                return;
            }
            DateTime compareTime = DateTime.Now;
            if (compareTime < entity.NextMatchTime)
                return;
            entity.NextMatchTime = compareTime.AddSeconds(_ladderHookCD);
            LadderCore.Instance.HookAttend(entity);
        }

        void doFinish()
        {
            if (_finishList != null && _finishList.Count > 0)
            {
                foreach (var entity in _finishList)
                {
                    HookEnd(entity.ManagerId, entity.Status);
                }
            }
        }

        bool AddToDic(LadderHookEntity entity)
        {
            var manager = ManagerCore.Instance.GetManager(entity.ManagerId);
            if (manager == null)
                return false;
            var arenaManager =LadderCore.Instance.InnerGetLadderManager(entity.ManagerId);
            if (arenaManager == null)
                return false;
            arenaManager.IsBot = false;
            arenaManager.Name = manager.Name;
            arenaManager.UpdateTime = DateTime.Now;
            arenaManager.HasTask = true;
            arenaManager.IsHook = true;
            entity.Score = arenaManager.Score;
            entity.Status = (int)EnumHookStatus.Run;
            entity.LadderManager = arenaManager;

            if (_hookDic.ContainsKey(entity.ManagerId))
                _hookDic[entity.ManagerId] = entity;
            else
            {
                _hookDic.TryAdd(entity.ManagerId, entity);
            }
            
            return true;
        }
        
        bool HookEnd(Guid managerId, EnumHookStatus hookStatus)
        {
            return HookEnd(managerId, (int)hookStatus);
        }

        bool HookEnd(Guid managerId, int hookStatus)
        {
            if (LadderHookMgr.End(managerId, hookStatus))
            {
                var e = new LadderHookEntity();
                _hookDic.TryRemove(managerId,out e);
                return true;
            }
            return false;
        }

        bool HookEndCheck(LadderHookEntity entity)
        {
            if (entity.Status != (int)EnumHookStatus.Run)
            {
                return true;
            }
            if (entity.Expired <= DateTime.Now)
                return true;
            int local = entity.Status;
            entity.Status = (int)EnumHookStatus.Finish;
            if (entity.MaxTimes>0 && entity.CurTimes >= entity.MaxTimes)
                return true;
            if (entity.MaxWiningTimes > 0 && entity.CurWiningTimes >= entity.MaxWiningTimes)
                return true;
            if (entity.Score > 0)
            {
                if (entity.MaxScore > 0 && entity.Score >= entity.MaxScore)
                    return true;
                if (entity.MinScore > 0 && entity.Score <= entity.MinScore)
                    return true;
            }
            if (entity.MaxTimes < 1 && entity.MinScore < 1 && entity.MaxScore < 1 && entity.MaxWiningTimes < 1)
            {
                return true;
            }
            entity.Status = local;
            return false;
        }

        LadderHookInfoResponse BuildHookInfoResponse(LadderHookEntity entity, bool isHook)
        {
            var response = ResponseHelper.CreateSuccess<LadderHookInfoResponse>();
            response.Data = new LadderHookInfoEntity();
            response.Data.IsHook = isHook;
            response.Data.CurTimes = entity.CurTimes;
            response.Data.CurWiningTimes = entity.CurWiningTimes;
            response.Data.MaxScore = entity.MaxScore;
            response.Data.MaxTimes = entity.MaxTimes;
            response.Data.MinScore = entity.MinScore;
            response.Data.LadderHookList = new List<LadderHook>();
            if (_hookListDic.ContainsKey(entity.ManagerId))
                response.Data.LadderHookList = _hookListDic[entity.ManagerId];
            var curTime = DateTime.Now;
            response.Data.NextMatchWaitSeconds = isHook ? ShareUtil.CalWaitTime(entity.NextMatchTime, curTime) : 0;
            response.Data.ExpiredTick = ShareUtil.GetTimeTick(entity.Expired);
            return response;
        }
        #endregion
    }

    /// <summary>
    /// 竞技场比赛条件实体
    /// </summary>
    public struct ArenaCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArenaCondition"/> struct.
        /// </summary>
        /// <param name="waitTime">The wait time.</param>
        /// <param name="playerNumber">The player number.</param>
        public ArenaCondition(int waitTime, int playerNumber)
        {
            this.WaitTime = waitTime;
            this.PlayerNumber = playerNumber;
        }

        public int WaitTime;

        public int PlayerNumber;
    }

    /// <summary>
    /// 比较天梯赛经理积分
    /// </summary>
    public class CompareArenaManager : IComparer<LadderManagerEntity>
    {
        /// <summary>
        /// 比较两个选手的积分
        /// 判断score，小的排前面
        /// </summary>
        /// <param name="competitorx">The competitorx.</param>
        /// <param name="competitory">The competitory.</param>
        /// <returns></returns>
        public int Compare(LadderManagerEntity competitorx, LadderManagerEntity competitory)
        {
            if (competitorx == null) 
            {
                if (competitory == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (competitory == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    //判断score，小的排前面
                    var scorex = (int)competitorx.Score;
                    var scorey = (int)competitory.Score;
                    if (scorex < scorey)
                    {
                        return -1;
                    }
                    else if (scorex == scorey)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
    }
}
