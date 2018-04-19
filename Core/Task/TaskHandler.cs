using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.Ladder;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Task;
using Games.NBall.Entity.Share;

namespace Games.NBall.Core.Task
{
    public class TaskHandler : BaseSingleton
    {
        private static NBThreadPool _nbThreadPool;
        public TaskHandler(int p)
            : base(p)
        {
            _nbThreadPool = new NBThreadPool(10);
        }

        #region Facade

        public static TaskHandler Instance
        {
            get { return SingletonFactory<TaskHandler>.SInstance; }
        }
       
        /// <summary>
        /// 替换球员上场
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> SolutionChangePlayer(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.SolutionChangePlayer, 1);
        }
        /// <summary>
        /// 强化球员卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="cardLevel"></param>
        /// <param name="strength"></param>
        public List<PopMessageEntity> PandoraStrength(Guid managerId, int cardLevel, int strength)
        {
            return Handler(managerId, EnumTaskRequireFunc.PandoraStrength, 1, cardLevel, strength);
        }
        /// <summary>
        /// 训练球员
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="level"></param>
        public List<PopMessageEntity> TeammemberTrain(Guid managerId, int level, bool sendByChat)
        {
            return Handler(managerId, EnumTaskRequireFunc.TeammemberTrain, 1, level, 0, "", sendByChat);
        }
        /// <summary>
        /// 帮助好友加速训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="byName"></param>
        public List<PopMessageEntity> TeammemberTrainHelp(Guid managerId, string byName)
        {
            return Handler(managerId, EnumTaskRequireFunc.TeammemberTrainHelp, 1, 0, 0, byName);
        }
        /// <summary>
        /// 报名杯赛
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> DailycupAttend(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.DailycupAttend, 1);
        }
        /// <summary>
        /// 竞猜杯赛
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> DailycupGamble(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.DailycupGamble, 1);
        }
        /// <summary>
        /// 球探抽卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="cardLevel"></param>
        public List<PopMessageEntity> ScoutingLottery(Guid managerId, int count)
        {
            return Handler(managerId, EnumTaskRequireFunc.ScoutingLottery, count);
        }

        /// <summary>
        /// 装备技能
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> SkillSet(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.SkillSet, 1);
        }
        /// <summary>
        /// 选择天赋
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> TalentSelect(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.TalentSelect, 1);
        }
        /// <summary>
        /// 参加天梯赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="winType"></param>
        public List<PopMessageEntity> LadderFight(Guid managerId, int winType)
        {
            return Handler(managerId, EnumTaskRequireFunc.LadderFight, 1, winType, 0, "", true);
        }

        /// <summary>
        /// 游戏分享
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> Share(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.ShareGame, 1);
        }

        /// <summary>
        /// 参加联赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="winType"></param>
        /// <returns></returns>
        public List<PopMessageEntity> LeagueFight(Guid managerId, int winType)
        {
            return Handler(managerId, EnumTaskRequireFunc.LeagueFight, 1, winType, 0, "", true);
        }


        /// <summary>
        /// 参加竞技场
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="winType"></param>
        public void ArenaFight(Guid managerId, int winType)
        {
            Handler(managerId, EnumTaskRequireFunc.Arena, 1, winType, 0, "", true);
        }

        
        /// <summary>
        /// 镶嵌球魂
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> PandoraMosaicBallsoul(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.PandoraMosaicBallsoul, 1);
        }
        /// <summary>
        /// 进行球员成长
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> TeammemberGrow(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.TeammemberGrow, 1);
        }
        /// <summary>
        /// 分解球员卡
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> PandoraDecompose(Guid managerId, int count)
        {
            return Handler(managerId, EnumTaskRequireFunc.PandoraDecompose, count);
        }
        /// <summary>
        /// 进行球员突破
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="stage"></param>
        public List<PopMessageEntity> TeammemberGrowup(Guid managerId, int stage)
        {
            return Handler(managerId, EnumTaskRequireFunc.TeammemberGrowup, 1, stage);
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> QuickTrain(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.QuickTrain, 1);
        }

        /// <summary>
        /// PK赛胜利
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="winType"></param>
        public List<PopMessageEntity> PlayerKillFight(Guid managerId, int winType)
        {
            return Handler(managerId, EnumTaskRequireFunc.PlayerKillFight, 1, winType, 0, "", true);
        }





        /// <summary>
        /// 卡牌收集
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> PlayCardCollection(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.PlayCardCollection, 1);
        }

        /// <summary>
        /// 友谊赛或者好友赛场次
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> PkOrFriendMatchCount(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.PkOrFriendMatchCount, 1);
        }

        /// <summary>
        /// 天梯赛场次
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> LadderMatchCount(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.LadderMatchCount, 1);
        }

        /// <summary>
        /// 好友赛场次
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> FriendMatchCount(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.FriendMatchCount, 1);
        }

        /// <summary>
        /// pk赛单场进球
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> PkMatchGoals(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.PkMatchGoals, 1);
        }
        /// <summary>
        /// pk赛单日进球
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> PkMatchDayGoals(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.PkMatchDayGoals, 1);
        }

        /// <summary>
        /// 单赛季天梯胜场数
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> LadderWinCountSeason(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.LadderWinCountSeason, 1);
        }

        /// <summary>
        /// 天梯赛单场进球
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> LadderMatchGoals(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.LadderMatchGoals, 1);
        }

        /// <summary>
        /// 天梯赛积分
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> LadderScore(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.LadderScore, 1);
        }
        
        /// <summary>
        ///  好友赛连胜场数
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> FriendMatchWin(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.FriendMatchWin, 1);
        }
         
        /// <summary>
        ///  强化球员卡收集
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> PlayerCardStrengthCollection(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.PlayerCardStrengthCollection, 1);
        }

        /// <summary>
        ///  球员等级收集
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> PlayerCardLevel(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.PlayerCardLevel, 1);
        }

        /// <summary>
        ///  杯赛排名
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> DailyCupRank(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.DailyCupRank, 1);
        }

        /// <summary>
        ///  联赛冠军积分
        /// </summary>
        /// <param name="managerId"></param>
        public List<PopMessageEntity> LeagueChampionScore(Guid managerId)
        {
            return Handler(managerId, EnumTaskRequireFunc.LeagueChampionScore, 1);
        }
        

        #endregion

        #region encapsulation
        /// <summary>
        /// 任务处理
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="requireType">任务</param>
        /// <param name="subParam"></param>
        /// <param name="thirdParam"></param>
        /// <param name="doneKey"></param>
        List<PopMessageEntity> Handler(Guid managerId, EnumTaskRequireFunc requireType, int count, int subParam = 0, int thirdParam = 0, string doneKey = "", bool sendByChat = false)
        {
            List<PopMessageEntity> popList = new List<PopMessageEntity>();
            try
            {
                var list = GetTaskList(managerId);

                foreach (var entity in list)
                {
                    var pop = Handler(entity, requireType, count, subParam, thirdParam, doneKey, sendByChat);
                    if (pop != null)
                        popList.Add(pop);
                }

                var pop2 = HandlerDaily(managerId, requireType, count, subParam, thirdParam, doneKey, sendByChat);
                if (pop2 != null)
                    popList.Add(pop2);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Task Handler", ex);
            }
            return popList;
        }

        PopMessageEntity HandlerDaily(Guid managerId, EnumTaskRequireFunc requireType, int count, int subParam = 0,
                          int thirdParam = 0, string doneKey = "", bool sendByChat = false)
        {
            var daily = TaskCore.Instance.GetDailyRecord(managerId);
            if (daily != null )
            {
                return Handler(daily, requireType, count, subParam, thirdParam, doneKey, sendByChat);
            }
            return null;
        }

        private PopMessageEntity Handler(TaskDailyrecordEntity entity, EnumTaskRequireFunc requireType, int count, int subParam = 0,
                             int thirdParam = 0, string doneKey = "", bool sendByChat = false)
        {
            try
            {
                var taskList = TaskCore.Instance.BuildDailyTaskList(entity);
                string allStepRecords = "";
                string allStatus = "";
                string allCurtimes = "";
                byte[] doneParam = new byte[0];
                bool needUpdate = false;
                foreach (var taskEntity in taskList)
                {
                    string stepRecord = taskEntity.StepRecord;
                    int curTimes = taskEntity.CurTimes;
                    int status = taskEntity.Status;
                    if (status == (int) EnumTaskStatus.Init)
                    {
                        if (doHandler(taskEntity.TaskId, count, ref doneParam, ref stepRecord, ref curTimes, ref status,
                            requireType, subParam,
                            thirdParam, doneKey))
                        {
                            needUpdate = true;
                        }
                    }
                    allStepRecords += stepRecord + "|";
                    allStatus += status + ",";
                    allCurtimes += curTimes + ",";
                }
                entity.DoneParam = doneParam;
                entity.StepRecords = allStepRecords.Remove(allStepRecords.LastIndexOf('|'),1);
                entity.CurTimes = allCurtimes.TrimEnd(',');
                entity.Status = allStatus.TrimEnd(',');
                if (needUpdate)
                {
                    if (TaskDailyrecordMgr.Update(entity))
                    {
                        //if (status == (int)EnumTaskStatus.Done)
                        //    return ChatHelper.SendTaskFinishPop(entity.ManagerId, entity.TaskId, sendByChat);
                        //else if (curTimes != oldTimes)
                        //{
                        //    return ChatHelper.SendTaskProgressPop(entity.ManagerId, entity.TaskId, curTimes, sendByChat);
                        //}
                    }
                }
                
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Task Handler", ex);
            }
            return null;
        }

        private PopMessageEntity Handler(TaskRecordEntity entity, EnumTaskRequireFunc requireType, int count, int subParam = 0,
                             int thirdParam = 0, string doneKey = "", bool sendByChat = false)
        {
            try
            {
                if (entity.Status == (int)EnumTaskStatus.Done || entity.Status == (int)EnumTaskStatus.Pending)
                    return null;
                byte[] doneParam = entity.DoneParam;
                string stepRecord = entity.StepRecord;
                int oldTimes = entity.CurTimes;
                int curTimes = entity.CurTimes;
                int status = entity.Status;
                if (doTaskHandler(entity.ManagerId, entity.TaskId, count, ref doneParam, ref stepRecord, ref curTimes, ref status, requireType, subParam,
                            thirdParam, doneKey))
                {
                    entity.DoneParam = doneParam;
                    entity.StepRecord = stepRecord;
                    entity.CurTimes = curTimes;
                    entity.Status = status;
                    if (TaskRecordMgr.Update(entity))
                    {
                        //if (status == (int)EnumTaskStatus.Done)
                        //{
                        //    return ChatHelper.SendTaskFinishPop(entity.ManagerId, entity.TaskId, sendByChat);
                        //}
                        //else if (curTimes != oldTimes)
                        //{
                        //    return ChatHelper.SendTaskProgressPop(entity.ManagerId, entity.TaskId, curTimes, sendByChat);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Task Handler", ex);
            }
            return null;
        }

        bool doTaskHandler(Guid managerId, int taskId, int count, ref byte[] doneParam, ref string stepRecord, ref int curTimes, ref int status,
            EnumTaskRequireFunc requireType, int subParam = 0, int thirdParam = 0, string doneKey = "")
        {
            var taskCache = CacheFactory.TaskConfigCache.GetTask(taskId);
            if (taskCache == null)
            {
                SystemlogMgr.Error("TaskHandler", "no task config taskid:" + taskId);
                return false;
            }
            if (taskCache.TaskType == (int) EnumTaskType.Achievement)
            {
                return doAchievementHandler(managerId, taskId, count, ref doneParam, ref stepRecord, ref curTimes, ref status, requireType,
                    subParam,
                    thirdParam, doneKey);
            }
            else
            {
                return doHandler(taskId, count, ref doneParam, ref stepRecord, ref curTimes, ref status, requireType,
                    subParam,
                    thirdParam, doneKey);
            }
        }
        //成就任务
        private bool doAchievementHandler(Guid managerId,int taskId, int count, ref byte[] doneParam, ref string stepRecord, ref int curTimes, ref int status,
            EnumTaskRequireFunc requireType, int subParam = 0, int thirdParam = 0, string doneKey = "")
        {
            try
            {
                var taskCache = CacheFactory.TaskConfigCache.GetTask(taskId);
                if (taskCache == null)
                {
                    SystemlogMgr.Error("TaskHandler", "no task config taskid:" + taskId);
                    return false;
                }
                int tempRequireType = (int)requireType;
                if (!taskCache.RequireFuncDic.ContainsKey(tempRequireType))
                    return false;
                TaskDoneRecordEntity doneRecordEntity = null;
                if (taskCache.UniqueConstraint)
                {
                    if (doneParam == null || doneParam.Length <= 0)
                    {
                        doneRecordEntity = new TaskDoneRecordEntity(taskCache.RequireList.Count);
                    }
                    else
                    {
                        doneRecordEntity = SerializationHelper.FromByte<TaskDoneRecordEntity>(doneParam);
                    }
                }
                var stepArray = BuildStepArray(stepRecord, taskCache.RequireList.Count);
                bool sync = false;
                for (int i = 0; i < taskCache.RequireList.Count; i++)
                {
                    if (doneRecordEntity != null)
                    {
                        if (doneRecordEntity.Records[i].Params.Contains(doneKey))
                        {
                            continue;
                        }
                    }
                    var require = taskCache.RequireList[i];
                    if (require.RequireType == (int)EnumTaskRequireFunc.PlayCardCollection)
                    {
                        if (require.RequireSub != 0 && require.RequireSub <= 4)
                        {
                            var achievementManager = AchievementManagerMgr.GetById(managerId);
                            if (achievementManager != null)
                            {
                                switch (require.RequireSub)
                                {
                                    case 4:
                                        curTimes = achievementManager.PurpleCardCount;
                                        break;
                                    case 3:
                                        curTimes = achievementManager.OrangeCardCount;
                                        break;
                                    case 2:
                                        curTimes = achievementManager.SilverCardCount;
                                        break;
                                    case 1:
                                        curTimes = achievementManager.GoldCardCount;
                                        break;
                                }
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.PlayerCardLevel)
                    {
                        if (require.RequireSub != 0 && require.RequireSub <= 30)
                        {
                            var achievementManager = AchievementManagerMgr.GetById(managerId);
                            if (achievementManager != null)
                            {
                                switch (require.RequireSub)
                                {
                                    case 5:
                                        curTimes = achievementManager.Level5CardCount;
                                        break;
                                    case 10:
                                        curTimes = achievementManager.Level10CardCount;
                                        break;
                                    case 20:
                                        curTimes = achievementManager.Level20CardCount;
                                        break;
                                    case 30:
                                        curTimes = achievementManager.Level30CardCount;
                                        break;
                                }
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.LeagueChampionScore)
                    {
                        if (require.RequireSub != 0 && require.RequireSub <= 8)
                        {
                            var achievementManager = AchievementManagerMgr.GetById(managerId);
                            if (achievementManager != null)
                            {
                                switch (require.RequireSub)
                                {
                                    case 1:
                                        if (achievementManager.LeagueScore1 >= require.RequireThird)
                                            curTimes = 1;
                                        break;
                                    case 2:
                                        if (achievementManager.LeagueScore2 >= require.RequireThird)
                                            curTimes = 1;
                                        break;
                                    case 3:
                                        if (achievementManager.LeagueScore3 >= require.RequireThird)
                                            curTimes = 1;
                                        break;
                                    case 4:
                                        if (achievementManager.LeagueScore4 >= require.RequireThird)
                                            curTimes = 1;
                                        break;
                                    case 5:
                                        if (achievementManager.LeagueScore5 >= require.RequireThird)
                                            curTimes = 1;
                                        break;
                                    case 6:
                                        if (achievementManager.LeagueScore6 >= require.RequireThird)
                                            curTimes = 1;
                                        break;
                                    case 7:
                                        if (achievementManager.LeagueScore7 >= require.RequireThird)
                                            curTimes = 1;
                                        break;
                                    case 8:
                                        if (achievementManager.LeagueScore8 >= require.RequireThird)
                                            curTimes = 1;
                                        break;
                                }
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.DailyCupRank)
                    {
                        var achievementManager = AchievementManagerMgr.GetById(managerId);
                        if (achievementManager != null)
                        {
                            if (achievementManager.MaxDailyCupRank <= require.RequireSub)
                            {
                                curTimes = 1;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    if (require.RequireType == (int)EnumTaskRequireFunc.PkMatchGoals)
                    {
                        var achievementManager = AchievementManagerMgr.GetById(managerId);
                        if (achievementManager != null)
                        {
                            if (achievementManager.MaxPkMatchGoals >= curTimes)
                            {
                                curTimes = achievementManager.MaxPkMatchGoals;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.PkMatchDayGoals)
                    {
                        var achievementManager = AchievementManagerMgr.GetById(managerId);
                        if (achievementManager != null)
                        {
                            if (achievementManager.DayPkMatchGoals >= curTimes)
                            {
                                curTimes = achievementManager.DayPkMatchGoals;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.LadderWinCountSeason)
                    {
                        var achievementManager = AchievementManagerMgr.GetById(managerId);
                        if (achievementManager != null)
                        {
                            if (achievementManager.MaxLadderWin >= curTimes)
                            {
                                curTimes = achievementManager.MaxLadderWin;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.LadderMatchGoals)
                    {
                        var achievementManager = AchievementManagerMgr.GetById(managerId);
                        if (achievementManager != null)
                        {
                            if (achievementManager.MaxLadderGoals >= curTimes)
                            {
                                curTimes = achievementManager.MaxLadderGoals;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.FriendMatchWin)
                    {
                        var achievementManager = AchievementManagerMgr.GetById(managerId);
                        if (achievementManager != null)
                        {
                            if (achievementManager.MaxFriendWinComb >= curTimes)
                            {
                                curTimes = achievementManager.MaxFriendWinComb;
                                stepArray[i] = "1";
                            }
                        }
                    }

                    else if (require.RequireType == (int)EnumTaskRequireFunc.PkOrFriendMatchCount)
                    {
                        var matchStat = NbMatchstatMgr.GetByManager(managerId);
                        if (matchStat != null)
                        {
                            var matchCount = 0;
                            foreach (var nbMatchstatEntity in matchStat)
                            {
                                if (nbMatchstatEntity.MatchType == (int)EnumMatchType.PlayerKill
                                    || nbMatchstatEntity.MatchType == (int)EnumMatchType.Friend)
                                    matchCount += nbMatchstatEntity.TotalCount;
                            }
                            if (matchCount >= curTimes)
                            {
                                curTimes = matchCount;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.LadderMatchCount)
                    {
                        var matchStat = NbMatchstatMgr.GetByManagerAndType(managerId, (int)EnumMatchType.Ladder);
                        if (matchStat != null)
                        {
                            if (matchStat.TotalCount >= curTimes)
                            {
                                curTimes = matchStat.TotalCount;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.FriendMatchCount)
                    {
                        var matchStat = NbMatchstatMgr.GetByManagerAndType(managerId, (int)EnumMatchType.Friend);
                        if (matchStat != null)
                        {
                            if (matchStat.TotalCount >= curTimes)
                            {
                                curTimes = matchStat.TotalCount;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.LadderScore)
                    {
                        var ladderInfo = LadderCore.Instance.GetLadderManager(managerId);
                        if (ladderInfo != null)
                        {
                            if (ladderInfo.MaxScore >= require.RequireSub)
                            {
                                curTimes = ladderInfo.MaxScore;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    else if (require.RequireType == (int)EnumTaskRequireFunc.PlayerCardStrengthCollection)
                    {
                        var package = ItemCore.Instance.GetPackage(managerId,
                            EnumTransactionType.AchievementUpdate);
                        if (package != null)
                        {
                            var cardCount = package.GetStrengthCardCount(require.RequireSub, require.RequireThird);
                            if (cardCount >= curTimes)
                            {
                                curTimes = cardCount;
                                stepArray[i] = "1";
                            }
                        }
                    }
                    if (doneRecordEntity != null)
                    {
                        doneRecordEntity.Records[i].Add(doneKey);
                    }
                }
                bool isDone = true;
                foreach (var s in stepArray)
                {
                    if (s != "1")
                    {
                        isDone = false;
                        break;
                    }
                }

                if (isDone)
                {
                    sync = true;
                    if (curTimes >= taskCache.Times)
                    {
                        status = (int)EnumTaskStatus.Done;
                    }
                    stepRecord = "";
                }
                else
                {
                    stepRecord = string.Join(",", stepArray);
                }
                if (sync)
                {
                    if (doneRecordEntity != null)
                    {
                        doneParam = SerializationHelper.ToByte(doneRecordEntity);
                    }
                }
                return sync;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Task Handler", ex);
                return false;
            }
        }

        bool doHandler(int taskId, int count, ref byte[] doneParam, ref string stepRecord, ref int curTimes, ref int status,
            EnumTaskRequireFunc requireType, int subParam = 0, int thirdParam = 0, string doneKey = "")
        {
            try
            {
                if (count <= 0)//次数，对应一次分解6张卡这种情况
                    count = 1;
                var taskCache = CacheFactory.TaskConfigCache.GetTask(taskId);
                if (taskCache == null)
                {
                    SystemlogMgr.Error("TaskHandler", "no task config taskid:" + taskId);
                    return false;
                }
                int tempRequireType = (int)requireType;
                if (!taskCache.RequireFuncDic.ContainsKey(tempRequireType))
                    return false;
                TaskDoneRecordEntity doneRecordEntity = null;
                if (taskCache.UniqueConstraint)
                {
                    if (doneParam == null || doneParam.Length <= 0)
                    {
                        doneRecordEntity = new TaskDoneRecordEntity(taskCache.RequireList.Count);
                    }
                    else
                    {
                        doneRecordEntity = SerializationHelper.FromByte<TaskDoneRecordEntity>(doneParam);
                    }
                }
                var stepArray = BuildStepArray(stepRecord, taskCache.RequireList.Count);
                bool sync = false;
                for (int i = 0; i < taskCache.RequireList.Count; i++)
                {
                    if (doneRecordEntity != null)
                    {
                        if (doneRecordEntity.Records[i].Params.Contains(doneKey))
                        {
                            continue;
                        }
                    }
                    var taskrequireEntity = taskCache.RequireList[i];
                    //处理连胜
                    if (requireType == EnumTaskRequireFunc.LadderFight
                        || requireType == EnumTaskRequireFunc.FriendMatchCount)
                    {
                        if (taskrequireEntity.RequireType == (int)requireType)
                        {
                            if (taskrequireEntity.RequireSub == (int)EnumTaskWinType.WinningStreak)
                            {
                                if (subParam == (int)EnumWinType.Win)
                                {
                                    subParam = (int)EnumTaskWinType.WinningStreak;
                                }
                                else
                                {
                                    subParam = 0;
                                    sync = true;
                                    curTimes = 0;
                                }
                            }
                        }
                    }
                    if (CheckTaskRequire(taskrequireEntity, requireType, subParam, thirdParam))
                    {
                        if (doneRecordEntity != null)
                        {
                            doneRecordEntity.Records[i].Add(doneKey);
                        }
                        stepArray[i] = "1";
                        sync = true;
                    }
                }
                bool isDone = true;
                foreach (var s in stepArray)
                {
                    if (s != "1")
                    {
                        isDone = false;
                        break;
                    }
                }

                if (isDone)
                {
                    sync = true;
                    curTimes += count;
                    if (curTimes >= taskCache.Times)
                    {
                        status = (int)EnumTaskStatus.Done;
                    }
                    stepRecord = "";
                }
                else
                {
                    stepRecord = string.Join(",", stepArray);
                }
                if (sync)
                {
                    if (doneRecordEntity != null)
                    {
                        doneParam = SerializationHelper.ToByte(doneRecordEntity);
                    }
                }
                return sync;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Task Handler", ex);
                return false;
            }
        }


        /// <summary>
        /// 获取任务执行情况数组
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string[] BuildStepArray(string stepRecord, int count)
        {
            string[] stepArray = null;
            if (!string.IsNullOrEmpty(stepRecord))
            {
                stepArray = stepRecord.Split(',');
            }
            else
            {
                stepArray = new string[count];
            }

            if (stepArray.Length < count)
            {
                var newArray = new string[count];
                for (int i = 0; i < stepArray.Length; i++)
                {
                    newArray[i] = stepArray[i];
                }
                stepArray = newArray;
            }
            else if (stepArray.Length > count)
            {
                var newArray = new string[count];
                for (int i = 0; i < count; i++)
                {
                    newArray[i] = stepArray[i];
                }
                stepArray = newArray;
            }
            return stepArray;
        }

        List<TaskRecordEntity> GetTaskList(Guid managerId)
        {
            return TaskRecordMgr.GetByManager(managerId);
        }

        bool CheckTaskRequire(ConfigTaskrequireEntity taskrequireEntity, EnumTaskRequireFunc requireType, int subParam = 0, int thirdParam = 0)
        {
            if (taskrequireEntity.RequireType == (int)requireType)
            {
                if (CheckTaskRequire(taskrequireEntity.OverState, taskrequireEntity.RequireSub, subParam)
                    && CheckTaskRequire(taskrequireEntity.OverState, taskrequireEntity.RequireThird, thirdParam))
                    return true;
            }
            return false;
        }

        private bool CheckTaskRequire(int overState, int require, int userData)
        {
            if (require == 0)
                return true;
            if (overState == 0)
            {
                return require == userData;
            }
            else if (overState < 0)
            {
                return userData <= require;
            }
            else
            {
                return userData >= require;
            }
        }

        #endregion
    }
}
