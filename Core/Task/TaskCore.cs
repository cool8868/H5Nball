using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.ManagerSkill;
//using Games.NBall.Core.Online;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom.Task;
using Games.NBall.Entity.Response.Item;
using Games.NBall.Entity.Response.Task;
using Games.NBall.Entity.Share;
using MsEntLibWrapper.Data;
//using Games.NBall.Core.Active;

namespace Games.NBall.Core.Task
{
    public class TaskCore : BaseSingleton
    {
        readonly int _taskDailyOpenLevel;
        readonly int _taskDailyCount;
        readonly int _taskDailyRefreshMallcode;
        private readonly Dictionary<int, List<int>> _guideFinishPrize;
        readonly int _guidePrizeSecondId;
        readonly int _guidePrizeSecondStrength;
        private readonly int _guideFinishTaskId;
        private readonly int _dailyTaskFinishPoint;
        private readonly int _dailyStartTaskId = 3001;
        public TaskCore(int p)
            : base(p)
        {
            _taskDailyRefreshMallcode =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.TaskDailyRefreshMallcode);
            _dailyTaskFinishPoint = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailyTaskFinishPoint);
            _taskDailyOpenLevel = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.TaskDailyOpenLevel);
            _taskDailyCount = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.TaskDailyCount);
            
            var guidePrizeList = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.GuidePrizeList);
            var guidePrize = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.GuidePrizeSecond);
            var gg = guidePrize.Split(',');
            _guidePrizeSecondId = Convert.ToInt32(gg[0]);
            _guidePrizeSecondStrength = Convert.ToInt32(gg[1]);

            _guideFinishTaskId = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.GuideFinishTaskId);
            var ss = guidePrizeList.Split('|');
            _guideFinishPrize = new Dictionary<int, List<int>>();
            foreach (var s in ss)
            {
                var ps = s.Split(',');
                int ptype = Convert.ToInt32(ps[0]);
                int pvalue = Convert.ToInt32(ps[1]);
                if (!_guideFinishPrize.ContainsKey(ptype))
                    _guideFinishPrize.Add(ptype, new List<int>());
                _guideFinishPrize[ptype].Add(pvalue);
            }
        }

        #region Facade

        public static TaskCore Instance
        {
            get { return SingletonFactory<TaskCore>.SInstance; }
        }

        public TaskListResponse GetTaskListResponse(Guid managerId)
        {
            var list = GetTaskList(managerId);
            var response = ResponseHelper.CreateSuccess<TaskListResponse>();
            response.Data = new TaskListEntity();
            foreach (var item in list)
            {
                if (item.Status != 0)
                    continue;
                try
                {
                    var config = CacheFactory.TaskConfigCache.GetTask(item.TaskId);
                    if (CheckHandleOpenTaskStatus(config))
                    {
                        if (config.RecordPeriod == 1)
                        {
                            if (item.UpdateTime.Date != DateTime.Now.Date)
                            {
                                item.UpdateTime = DateTime.Now;
                                item.CurTimes = 0;
                                TaskRecordMgr.Update(item);
                            }
                        }
                        else if (config.RecordPeriod == 2)
                        {
                            var season = CacheFactory.SeasonCache.GetCurrentSeason();
                            if (!(item.UpdateTime.Date >= season.Startdate.Date &&
                                item.UpdateTime.Date <= season.Enddate.Date))
                            {
                                item.UpdateTime = DateTime.Now;
                                item.CurTimes = 0;
                                TaskRecordMgr.Update(item);
                            }
                        }
                        else if (config.RecordPeriod == 3)
                        {
                            if (item.CurTimes > 0)
                                continue;
                            var ladderManager = LadderCore.Instance.GetLadderManager(managerId);
                            if (ladderManager == null)
                                continue;
                            if (item.CurTimes < ladderManager.Score)
                            {
                                item.UpdateTime = DateTime.Now;
                                item.CurTimes = ladderManager.Score;
                                TaskRecordMgr.Update(item);
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            response.Data.Tasks = BuildResponseList(list);
            GetDailyTask(managerId, response.Data);
            return response;
        }

        public TaskListResponse GetTaskCompleteListResponse(Guid managerId)
        {
            var list = GetTaskList(managerId);
            var taskEntity = new TaskListEntity();
            taskEntity.Tasks = BuildResponseList(list);
            GetDailyTask(managerId, taskEntity);


            var response = ResponseHelper.CreateSuccess<TaskListResponse>();
            response.Data = new TaskListEntity();
            response.Data.Tasks = new List<TaskEntity>();
            foreach (var task in taskEntity.Tasks)
            {
                if(task.Status==(int)EnumTaskStatus.Done)
                    response.Data.Tasks.Add(task); 
            }

            response.Data.DailyTasks = new List<TaskEntity>();
            foreach (var task in taskEntity.DailyTasks)
            {
                if (task.Status == (int)EnumTaskStatus.Done)
                    response.Data.DailyTasks.Add(task);
            }

            return response;
        }

        public bool HasTaskComplete(Guid managerId)
        {
            var list = GetTaskList(managerId);
            var taskEntity = new TaskListEntity();
            taskEntity.Tasks = BuildResponseList(list);
            GetDailyTask(managerId, taskEntity);
            if (taskEntity.Tasks != null && taskEntity.Tasks.Count > 0)
            {
                if (taskEntity.Tasks.Exists(r => r.Status == (int) EnumTaskStatus.Done))
                    return true;
            }
            if ( taskEntity.DailyTasks!=null &&taskEntity.DailyTasks.Count > 0)
            {
                if (taskEntity.DailyTasks.Exists(r => r.Status == (int)EnumTaskStatus.Done))
                    return true;
            }
            return false;
        }


        public TaskListEntity GetTaskListShow(Guid managerId)
        {
            var taskShow = new TaskListEntity();
            var list = GetTaskList(managerId);
            taskShow.Tasks = BuildResponseList(list);
            GetDailyTask(managerId, taskShow);
            return taskShow;
        }

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="managerId">经理id</param>
        /// <param name="recordId">任务记录表id</param>
        /// <returns></returns>
        public SubmitTaskResponse SubmitTask(Guid managerId, int recordId)
        {
            var task = TaskRecordMgr.GetById(recordId);
            if (task == null || task.ManagerId != managerId)
            {
                return ResponseHelper.InvalidParameter<SubmitTaskResponse>();
            }
            List<TaskRecordEntity> newTasks;
            NbManagerEntity manager;
            ItemPackageFrame package;
            NbManagerextraEntity managerextra;
            ConfigTaskEntity taskConfig;
            int prizeExp;
            int prizeCoin;
            int prizeItemCode;
            int prizePoint;
            int prizeFriendShipPoint;
            var code = doSubmitTask(managerId, task.Status, task.TaskId, out newTasks, out manager, out package, out managerextra, out taskConfig, out prizeExp, out prizeCoin, out prizeItemCode, out prizePoint,out prizeFriendShipPoint);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<SubmitTaskResponse>(code);
            }
            PayUserEntity payUserEntity = PayUserMgr.GetById(manager.Account);
            code = SaveSubmit(recordId, newTasks, prizeExp, prizeCoin, prizeItemCode, manager, package, managerextra, prizePoint, prizeFriendShipPoint, ref payUserEntity);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<SubmitTaskResponse>(code);
            }
            else
            {
                package.Shadow.Save();
                AchievementTaskCore.Instance.UpdatePlayCardCount(package);
                manager.HasOpenTask = false;
                var pop = ManagerUtil.SaveManagerAfter(manager);
                var response = ResponseHelper.CreateSuccess<SubmitTaskResponse>();
                response.Data = new SubmitTaskEntity();
                BuildSubmitResponse(response.Data, taskConfig, manager, newTasks);
                response.Data.PopMsg = pop;
                if (prizePoint > 0)
                {
                    //ChatHelper.SendBindPoint(manager.Idx, payUserEntity.BindPoint);
                }

                return response;
            }
        }

        public MessageCodeResponse ReceiveGuidePrize(Guid managerId)
        {
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            if (managerExtra == null)
                return ResponseHelper.InvalidParameter<MessageCodeResponse>("managerExtra");
            if (managerExtra.CanReceiveGuidePrize == false)
                return ResponseHelper.InvalidParameter<MessageCodeResponse>("HasGuidePrize");
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TaskPrize);
            var code = package.AddItem(_guidePrizeSecondId, _guidePrizeSecondStrength, true,false);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<MessageCodeResponse>(code);
            managerExtra.HasGuidePrize = false;
            code = SaveGuidePrize(package, managerExtra);
            if (code == MessageCode.Success)
            {
                package.Shadow.Save();
            }
            return ResponseHelper.Create<MessageCodeResponse>(code);
        }

        public void GetLevelOpenTasks(Guid managerId, int managerLevel, ref List<TaskRecordEntity> newTasks, string zoneId = "")
        {
            var pendingList = TaskRecordMgr.GetPending(managerId, managerLevel, zoneId);
            if (pendingList != null && pendingList.Count > 0)
            {
                foreach (var pending in pendingList)
                {
                    if (pending.ManagerLevel <= managerLevel)
                    {
                        pending.Status = 0;
                        pending.IsFromPending = true;
                        newTasks.Add(pending);
                    }
                }
            }
            var openList = CacheFactory.TaskConfigCache.GetLevelOpenTasks(managerLevel);
            if (openList != null && openList.Count > 0)
            {
                foreach (var entity in openList)
                {
                    newTasks.Add(BuildLevelopenTask(entity.Idx, managerId, managerLevel));
                }
            }
        }

        /// <summary>
        /// 检查激活的任务是否可直接完成(限只需完成1次的成就任务)
        /// </summary>
        /// <param name="record"></param>
        public void HandleOpenTaskStatus(TaskRecordEntity record, string zoneId = "")
        {
            if (record.IsPending)
                return;
            var config = CacheFactory.TaskConfigCache.GetTask(record.TaskId);
            if (CheckHandleOpenTaskStatus(config))
            {
                doHandleOpenTaskStatus(record, config, zoneId);
            }
        }

        bool CheckHandleOpenTaskStatus(ConfigTaskEntity config)
        {
            if (config == null)
                return false;
            if (config.TaskType == (int)EnumTaskType.Achievement)
                return true;
            return false;
        }

        void doHandleOpenTaskStatus(TaskRecordEntity record, ConfigTaskEntity taskCache, string zoneId)
        {
            try
            {
                var stepArray = TaskHandler.Instance.BuildStepArray(record.StepRecord, taskCache.RequireList.Count);
                AchievementManagerEntity achievementManager = null;
                for (int i = 0; i < taskCache.RequireList.Count; i++)
                {
                    var require = taskCache.RequireList[i];
                    //卡牌收集类成就任务
                    switch (require.RequireType)
                    {
                        case (int)EnumTaskRequireFunc.PlayCardCollection:
                            if (require.RequireSub != 0 && require.RequireSub <= 4)
                            {
                                if(achievementManager ==null)
                                  achievementManager =  AchievementManagerMgr.GetById(record.ManagerId);
                                if (achievementManager != null)
                                {
                                    switch (require.RequireSub)
                                    {
                                        case 4:
                                            record.CurTimes = achievementManager.PurpleCardCount;
                                            break;
                                        case 3:
                                            record.CurTimes = achievementManager.OrangeCardCount;
                                            break;
                                        case 2:
                                            record.CurTimes = achievementManager.SilverCardCount;
                                            break;
                                        case 1:
                                            record.CurTimes = achievementManager.GoldCardCount;
                                            break;
                                    }
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.PlayerCardLevel:
                            if (require.RequireSub != 0 && require.RequireSub <= 30)
                            {
                                if (achievementManager == null)
                                    achievementManager = AchievementManagerMgr.GetById(record.ManagerId);
                                if (achievementManager != null)
                                {
                                    switch (require.RequireSub)
                                    {
                                        case 5:
                                            record.CurTimes = achievementManager.Level5CardCount;
                                            break;
                                        case 10:
                                            record.CurTimes = achievementManager.Level10CardCount;
                                            break;
                                        case 20:
                                            record.CurTimes = achievementManager.Level20CardCount;
                                            break;
                                        case 30:
                                            record.CurTimes = achievementManager.Level30CardCount;
                                            break;
                                    }
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.PkMatchGoals:
                            if (achievementManager == null)
                                achievementManager = AchievementManagerMgr.GetById(record.ManagerId);
                            if (achievementManager != null)
                            {
                                if (achievementManager.MaxPkMatchGoals >= record.CurTimes)
                                {
                                    record.CurTimes = achievementManager.MaxPkMatchGoals;
                                    achievementManager.MaxPkMatchGoals = 0;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.PkMatchDayGoals:
                            if (achievementManager == null)
                                achievementManager = AchievementManagerMgr.GetById(record.ManagerId);
                            if (achievementManager != null)
                            {
                                if (achievementManager.DayPkMatchGoals >= record.CurTimes)
                                {
                                    record.CurTimes = achievementManager.DayPkMatchGoals;
                                    achievementManager.DayPkMatchGoals = 0;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.LadderWinCountSeason:
                            if (achievementManager == null)
                                achievementManager = AchievementManagerMgr.GetById(record.ManagerId);
                            if (achievementManager != null)
                            {
                                if (achievementManager.MaxLadderWin >= record.CurTimes)
                                {
                                    record.CurTimes = achievementManager.MaxLadderWin;
                                    achievementManager.MaxLadderWin = 0;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.LadderMatchGoals:
                            if (achievementManager == null)
                                achievementManager = AchievementManagerMgr.GetById(record.ManagerId);
                            if (achievementManager != null)
                            {
                                if (achievementManager.MaxLadderGoals >= record.CurTimes)
                                {
                                    record.CurTimes = achievementManager.MaxLadderGoals;
                                    achievementManager.MaxLadderGoals = 0;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.FriendMatchWin:
                            if (achievementManager == null)
                                achievementManager = AchievementManagerMgr.GetById(record.ManagerId);
                            if (achievementManager != null)
                            {
                                if (achievementManager.MaxFriendWinComb >= record.CurTimes)
                                {
                                    record.CurTimes = achievementManager.MaxFriendWinComb;
                                    achievementManager.MaxFriendWinComb = 0;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.DailyCupRank:
                            if (achievementManager == null)
                                achievementManager = AchievementManagerMgr.GetById(record.ManagerId);
                            if (achievementManager != null)
                            {
                                if (achievementManager.MaxDailyCupRank <= require.RequireSub)
                                {
                                    record.CurTimes = 1;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.LeagueChampionScore:
                            if (achievementManager == null)
                                achievementManager = AchievementManagerMgr.GetById(record.ManagerId);
                            if (achievementManager != null)
                            {
                                switch (require.RequireSub)
                                {
                                    case 1:
                                        if (achievementManager.LeagueScore1 >= require.RequireThird)
                                        {
                                            record.CurTimes = 1;
                                            achievementManager.LeagueScore1 = 0;
                                            stepArray[i] = "1";
                                        }
                                        break;
                                    case 2:
                                        if (achievementManager.LeagueScore2 >= require.RequireThird)
                                        {
                                            record.CurTimes = 1;
                                            achievementManager.LeagueScore2 = 0;
                                            stepArray[i] = "1";
                                        }
                                        break;
                                    case 3:
                                        if (achievementManager.LeagueScore3 >= require.RequireThird)
                                        {
                                            record.CurTimes = 1;
                                            achievementManager.LeagueScore3 = 0;
                                            stepArray[i] = "1";
                                        }
                                        break;
                                    case 4:
                                        if (achievementManager.LeagueScore4 >= require.RequireThird)
                                        {
                                            record.CurTimes = 1;
                                            achievementManager.LeagueScore4 = 0;
                                            stepArray[i] = "1";
                                        }
                                        break;
                                    case 5:
                                        if (achievementManager.LeagueScore5 >= require.RequireThird)
                                        {
                                            record.CurTimes = 1;
                                            achievementManager.LeagueScore5 = 0;
                                            stepArray[i] = "1";
                                        }
                                        break;
                                    case 6:
                                        if (achievementManager.LeagueScore6 >= require.RequireThird)
                                        {
                                            record.CurTimes = 1;
                                            achievementManager.LeagueScore6 = 0;
                                            stepArray[i] = "1";
                                        }
                                        break;
                                    case 7:
                                        if (achievementManager.LeagueScore7 >= require.RequireThird)
                                        {
                                            record.CurTimes = 1;
                                            achievementManager.LeagueScore7 = 0;
                                            stepArray[i] = "1";
                                        }
                                        break;
                                    case 8:
                                        if (achievementManager.LeagueScore8 >= require.RequireThird)
                                        {
                                            record.CurTimes = 1;
                                            achievementManager.LeagueScore8 = 0;
                                            stepArray[i] = "1";
                                        }
                                        break;
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.PkOrFriendMatchCount:
                            var matchStat = NbMatchstatMgr.GetByManager(record.ManagerId);
                            if (matchStat != null)
                            {
                                var count = 0;
                                foreach (var nbMatchstatEntity in matchStat)
                                {
                                    if (nbMatchstatEntity.MatchType == (int)EnumMatchType.PlayerKill
                                        || nbMatchstatEntity.MatchType == (int)EnumMatchType.Friend)
                                        count += nbMatchstatEntity.TotalCount;
                                }
                                if (count >= record.CurTimes)
                                {
                                    record.CurTimes = count;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.LadderMatchCount:
                            var matchStat1 = NbMatchstatMgr.GetByManagerAndType(record.ManagerId, (int)EnumMatchType.Ladder);
                            if (matchStat1 != null)
                            {
                                if (matchStat1.TotalCount >= record.CurTimes)
                                {
                                    record.CurTimes = matchStat1.TotalCount;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.FriendMatchCount:
                            var matchStat2 = NbMatchstatMgr.GetByManagerAndType(record.ManagerId, (int)EnumMatchType.Friend);
                            if (matchStat2 != null)
                            {
                                if (matchStat2.TotalCount >= record.CurTimes)
                                {
                                    record.CurTimes = matchStat2.TotalCount;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.LadderScore:
                            var ladderInfo = LadderCore.Instance.GetLadderManager(record.ManagerId);
                            if (ladderInfo != null)
                            {
                                if (ladderInfo.MaxScore >= record.CurTimes)
                                {
                                    record.CurTimes = ladderInfo.MaxScore;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
                        case (int)EnumTaskRequireFunc.PlayerCardStrengthCollection:
                            var package = ItemCore.Instance.GetPackage(record.ManagerId,
                                EnumTransactionType.AchievementUpdate);
                            if (package != null)
                            {
                                var count = package.GetStrengthCardCount(require.RequireSub, require.RequireThird);
                                if (count >= record.CurTimes)
                                {
                                    record.CurTimes = count;
                                    stepArray[i] = "1";
                                }
                            }
                            break;
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
                    if (record.CurTimes >= taskCache.Times)
                    {
                        record.Status = (int)EnumTaskStatus.Done;
                    }
                    record.StepRecord = "";
                }
                else
                {
                    record.StepRecord = string.Join(",", stepArray);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Task Handler", ex);
            }
        }

      
        public MessageCode CheckGuideBuff(int guideTaskRecordId, Guid managerId, ref string guideBuff, out TaskRecordEntity task, out NbManagerextraEntity extra)
        {
            task = TaskRecordMgr.GetById(guideTaskRecordId);
            extra = null;
            if (task == null || task.ManagerId != managerId)
                return MessageCode.NbParameterError;
            if (task.Status == (int)EnumTaskStatus.Done)
                return MessageCode.NbParameterError;
            var taskConfig = CacheFactory.TaskConfigCache.GetTask(task.TaskId);
            guideBuff = taskConfig.GuideBuff;
            if (!string.IsNullOrEmpty(guideBuff))
            {
                extra = ManagerCore.Instance.GetManagerExtra(managerId);
                if (extra.GuideBuffRecord.Contains(guideBuff))
                    return MessageCode.NbParameterError;
            }
            else
            {
                return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }
        #endregion

        #region encapsulation
        
        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="status"></param>
        /// <param name="taskId"></param>
        /// <param name="newTasks"></param>
        /// <param name="manager"></param>
        /// <param name="package"></param>
        /// <param name="pending"></param>
        /// <param name="taskConfig"></param>
        /// <returns></returns>
        MessageCode doSubmitTask(Guid managerId, int status, int taskId, out List<TaskRecordEntity> newTasks, out NbManagerEntity manager, out ItemPackageFrame package, out NbManagerextraEntity managerextra
            , out ConfigTaskEntity taskConfig, out int prizeExp, out int prizeCoin, out int prizeItemCode, out int point,out int prizeFriendShipPoint)
        {
            package = null;
            newTasks = null;
            manager = null;
            managerextra = null;
            taskConfig = null;
            prizeExp = 0;
            prizeCoin = 0;
            prizeItemCode = 0;
            point = 0;
            prizeFriendShipPoint = 0;

            if (status != (int)EnumTaskStatus.Done)
            {
                return MessageCode.TaskSubmitInvalid;
            }
            taskConfig = CacheFactory.TaskConfigCache.GetTask(taskId);
            if (taskConfig == null)
                return MessageCode.TaskNoConfig;
            manager = ManagerCore.Instance.GetManager(managerId);
            prizeExp = taskConfig.PrizeExp;
            prizeCoin = taskConfig.PrizeCoin;
            if (taskConfig.TaskType == (int)EnumTaskType.Daily)
            {
                //prizeExp = prizeExp * (10 + manager.Level) / 10;
                //prizeCoin = prizeCoin * (10 + manager.Level) / 10;
                
            }
            int guideFinishPrizeCoin = 0;
            //bool isGuideFinish = taskId == _guideFinishTaskId;
            bool isGuideFinish = false;
            if (isGuideFinish)
            {
                managerextra = ManagerCore.Instance.GetManagerExtra(managerId);
                managerextra.HasGuidePrize = true;
                managerextra.GuidePrizeExpired = DateTime.Today.AddDays(6);
                managerextra.GuidePrizeCount = 1;
                managerextra.GuidePrizeLastDate = DateTime.Today;
                package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TaskPrize);
                foreach (var i in _guideFinishPrize)
                {
                    switch (i.Key)
                    {
                        case 1:
                            foreach (var i1 in i.Value)
                            {
                                var code = package.AddItem(i1, true,false);
                                if (code != MessageCode.Success)
                                    return code;
                            }
                            break;
                        case 2:
                            foreach (var i1 in i.Value)
                            {
                                var itemCodeP = CacheFactory.LotteryCache.LotteryByLib(i1);
                                if (itemCodeP > 0)
                                {
                                    var code1 = package.AddItem(itemCodeP, true,false);
                                    if (code1 != MessageCode.Success)
                                        return code1;
                                }
                            }
                            break;
                        case 3:
                            foreach (var i1 in i.Value)
                            {
                                guideFinishPrizeCoin += i1;
                            }
                            break;
                        case 4:
                            foreach (var i1 in i.Value)
                            {
                                var code = package.AddItem(i1, true,false);
                                if (code != MessageCode.Success)
                                    return code;
                            }
                            break;
                    }
                }
            }
            else if (taskConfig.PrizeItemCode > 0)
            {
                if (!OnlineCore.Instance.CheckIndulgeNoPrize(manager))
                {
                    prizeItemCode = taskConfig.PrizeItemCode;
                    package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TaskPrize);
                    var itemBinding = ShareUtil.GetItemBinding(EnumItemPrizeType.TaskPrize);
                    var list = GiftPackPrizeCache.Instance.GetGiftPackPrize(prizeItemCode);
                    if (list == null)
                        return MessageCode.InvalidArgs;
                    foreach (var entity in list)
                    {
                        int itemcode = 0;
                        if (entity.PrizeType == (int)EnumGiftPackPrize.Item)
                        {
                            itemcode = entity.SubType;
                        }
                        else if (entity.PrizeType == (int)EnumGiftPackPrize.RandomItem)
                        {
                            itemcode = CacheFactory.LotteryCache.LotteryByLib(entity.SubType);

                        }
                        else if (entity.PrizeType == (int)EnumGiftPackPrize.Point)
                        {
                            point = entity.Count;
                            continue;
                        }
                        else if (entity.PrizeType == (int)EnumGiftPackPrize.Coin)
                        {
                            prizeCoin += entity.Count;
                            continue;
                        }
                        else if (entity.PrizeType == (int) EnumGiftPackPrize.FriendshipPoint)
                        {
                            prizeFriendShipPoint += entity.Count;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                        var messageCode = package.AddItems(itemcode, entity.Count, itemBinding,false);
                        if (messageCode != MessageCode.Success)
                            return messageCode;
                    }
                }
            }
            OnlineCore.Instance.CalIndulgePrize(manager, ref prizeExp, ref prizeCoin);
            ManagerUtil.AddManagerData(manager, prizeExp, prizeCoin + guideFinishPrizeCoin, 0, EnumCoinChargeSourceType.Task, taskId.ToString());

            newTasks = new List<TaskRecordEntity>();
            if (manager.IsLevelup)
            {
                GetLevelOpenTasks(manager.Idx, manager.Level, ref newTasks);
            }
            if (taskConfig.OpenFunc > 0)
                ManagerUtil.SetOpenFunction(manager, taskConfig.OpenFunc);

            var nextTaskConfigs = CacheFactory.TaskConfigCache.GetNextTask(taskConfig.TaskType, taskId);
            if (nextTaskConfigs != null)
            {
                foreach (var configTaskEntity in nextTaskConfigs)
                {
                    if (manager.Level >= configTaskEntity.ManagerLevel)
                    {
                        newTasks.Add(BuildLevelopenTask(configTaskEntity.Idx, managerId, configTaskEntity.ManagerLevel));
                    }
                    else
                    {
                        newTasks.Add(BuildPendingTask(configTaskEntity.Idx, configTaskEntity.ManagerLevel, managerId));
                    }
                }
            }
            //if (newTasks != null && newTasks.Count > 0)
            //{
            //    foreach (var entity in newTasks)
            //    {
            //        HandleOpenTaskStatus(entity);
            //    }
            //}
            return MessageCode.Success;
        }


        void BuildSubmitResponse(SubmitTaskEntity data, ConfigTaskEntity taskConfig, NbManagerEntity manager, List<TaskRecordEntity> newTasks)
        {
            data.PrizeCoin = taskConfig.PrizeCoin;
            data.PrizeExp = taskConfig.PrizeExp;
            data.PrizeItemCode = taskConfig.PrizeItemCode;
            data.ManagerCoin = -1;
            data.ManagerExp = -1;
            data.ManagerLevel = -1;
            data.IsLevelup = false;
            data.LevelupExp = -1;
            data.NewTasks = BuildResponseList(newTasks);
        }


        #region 每日任务

        public TaskDailyrecordEntity GetDailyRecord(Guid managerId)
        {
            var daily = TaskDailyrecordMgr.GetById(managerId);
            if (daily == null)
            {
                daily = CreateDailyTask(managerId);
                if (!TaskDailyrecordMgr.Insert(daily))
                    return null;
            }
            else
            {
                var curDate = DateTime.Today;
                if (daily.RecordDate != curDate)
                {
                    daily.TaskIds = CacheFactory.TaskConfigCache.RandomDailyTaskList(5);
                    daily.DailyCount = 0;
                    daily.RecordDate = curDate;
                    daily.CurTimes = "0,0,0,0,0,0,0,0";
                    daily.DoneParam = new byte[0];
                    daily.Status = "0,0,0,0,0,0,0,0";
                    daily.StepRecords = "|||||||";
                    daily.UpdateTime = DateTime.Now;
                    daily.IsReceive = false;
                    daily.FinishCount = 0;
                    TaskDailyrecordMgr.Update(daily);
                }
            }
            return daily;
        }
        /// <summary>
        /// 检查是否开放每日任务
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool CheckOpenDailyTask(int level)
        {
            return level == _taskDailyOpenLevel;
        }

        public TaskDailyrecordEntity CreateDailyTask(Guid managerId)
        {
            var entity = new TaskDailyrecordEntity();
            entity.RecordDate = DateTime.Now.Date;
            entity.DailyCount = 0;
            entity.TaskIds = CacheFactory.TaskConfigCache.RandomDailyTaskList(5);
            entity.CurTimes = "0,0,0,0,0,0,0,0";
            entity.DoneParam = new byte[0];
            entity.ManagerId = managerId;
            entity.RowTime = DateTime.Now;
            entity.Status = "0,0,0,0,0,0,0,0";
            entity.StepRecords = "|||||||";
            entity.UpdateTime = DateTime.Now;
            entity.IsReceive = false;
            entity.FinishCount = 0;
            return entity;
        }


        /// <summary>
        /// 获取每日任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        void GetDailyTask(Guid managerId, TaskListEntity taskListEntity)
        {
            try
            {
                var daily = GetDailyRecord(managerId);
                if (daily == null)
                {
                    taskListEntity.FinishCount = -1;
                    return;
                }
                taskListEntity.DailyCount = daily.DailyCount;
                taskListEntity.MaxDailyCount = _taskDailyCount;
                taskListEntity.FinishCount = daily.FinishCount;
                taskListEntity.IsReceive = daily.IsReceive;
                if (daily.DailyCount < _taskDailyCount)
                {
                    taskListEntity.DailyTasks = BuildDailyTaskList(daily);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetDailyTask,managerId:" + managerId.ToString(), ex);
            }
        }

        public List<TaskEntity> BuildDailyTaskList(TaskDailyrecordEntity task)
        {
            var list = new List<TaskEntity>();
            var taskIds = task.TaskIds.Split(',');
            var taskStauts = task.Status.Split(',');
            var taskCurTimes = task.CurTimes.Split(',');
            var taskStepRecords = task.StepRecords.Split('|');
            for (int i = 0; i < taskIds.Count(); i++)
            {
                list.Add(new TaskEntity
                {
                    Idx = i + 1,
                    TaskId = Convert.ToInt32(taskIds[i]),
                    Status = Convert.ToInt32(taskStauts[i]),
                    CurTimes = Convert.ToInt32(taskCurTimes[i]),
                    StepRecord = taskStepRecords[i]
                });
            }
            return list;
        }

        public SubmitDailyTaskResponse SubmitDailyTask(Guid managerId, int taskId)
        {
            var task = TaskDailyrecordMgr.GetById(managerId);
            if (task == null)
            {
                return ResponseHelper.InvalidParameter<SubmitDailyTaskResponse>();
            }
            List<TaskRecordEntity> newTasks;
            NbManagerEntity manager;
            ItemPackageFrame package;
            NbManagerextraEntity managerextra;
            ConfigTaskEntity taskConfig;
            int prizeExp;
            int prizeCoin;
            int prizeItemCode;
            int prizePoint;
            int prizeFriendShipPoint;
            var taskList = BuildDailyTaskList(task);
            var currectTask = taskList.Find(t => t.TaskId == taskId);
            var currectIndex = taskList.FindIndex(t => t.TaskId == taskId);
            if (currectTask == null)
                return ResponseHelper.InvalidParameter<SubmitDailyTaskResponse>();

            var code = doSubmitTask(managerId, currectTask.Status, currectTask.TaskId, out newTasks, out manager, out package, out managerextra, out taskConfig, out prizeExp, out prizeCoin, out prizeItemCode, out prizePoint, out prizeFriendShipPoint);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<SubmitDailyTaskResponse>(code);
            }
            if (manager == null)
            {
                manager = ManagerCore.Instance.GetManager(managerId);
            }
            task.DailyCount++;
            task.FinishCount++;

            //修改当前任务的状态为完成
            currectTask.Status = 2;
            var allStatus = task.Status.Split(',');
            allStatus[currectIndex] = "2";
            task.Status = string.Join(",", allStatus);

            //保存任务情况
            PayUserEntity payUserEntity = PayUserMgr.GetById(manager.Account);
            code = SaveSubmit(task, currectTask.TaskId, currectTask.StepRecord, currectTask.CurTimes, prizeExp, prizeCoin, prizeItemCode, newTasks, manager, package, managerextra, prizePoint, prizeFriendShipPoint, ref payUserEntity);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<SubmitDailyTaskResponse>(code);
            }
            else
            {
                package.Shadow.Save();
                AchievementTaskCore.Instance.UpdatePlayCardCount(package);
                manager.HasOpenTask = false;
                var pop = ManagerUtil.SaveManagerAfter(manager);

                var response = ResponseHelper.CreateSuccess<SubmitDailyTaskResponse>();
                response.Data = new SubmitDailyTaskEntity();
                BuildSubmitResponse(response.Data, taskConfig, manager, newTasks);
                response.Data.DailyCount = task.DailyCount;
                response.Data.MaxDailyCount = _taskDailyCount;
                response.Data.FinishCount = task.FinishCount;
                response.Data.IsReceive = task.IsReceive;
                if (task.DailyCount < _taskDailyCount)
                {
                    response.Data.DailyTasks = BuildDailyTaskList(task);
                }
                //ActiveCore.Instance.AddActive(managerId, EnumActiveType.EverydayMission, 1);
                response.Data.PopMsg = pop;
                return response;
            }
        }

        //void NewDailyTask(TaskDailyrecordEntity task)
        //{
        //    var dailyTask = CacheFactory.TaskConfigCache.RandomDailyTask(task.TaskId);
        //    if (dailyTask != null)
        //    {
        //        task.TaskId = dailyTask.Idx;
        //        task.Status = 0;
        //        task.CurTimes = 0;
        //        task.DoneParam = new byte[0];
        //        task.StepRecord = "";
        //        task.UpdateTime = DateTime.Now;
        //    }
        //    else
        //    {
        //        task.Status = 2;
        //    }
        //}

        DailyTaskResponse BuildDailyTaskResponse(TaskDailyrecordEntity task)
        {
            var response = ResponseHelper.CreateSuccess<DailyTaskResponse>();
            response.Data = new DailyTaskEntity();
            response.Data.DailyCount = task.DailyCount;
            response.Data.MaxDailyCount = _taskDailyCount;
            response.Data.FinishCount = task.FinishCount;
            response.Data.IsReceive = task.IsReceive;
            response.Data.Tasks = BuildDailyTaskList(task);
            return response;
        }

        #endregion

        List<TaskEntity> BuildResponseList(List<TaskRecordEntity> tasks)
        {
            if (tasks == null)
                return null;
            var list = new List<TaskEntity>(tasks.Count);
            foreach (var entity in tasks)
            {
                list.Add(new TaskEntity() { Idx = entity.Idx, TaskId = entity.TaskId, Status = entity.Status, CurTimes = entity.CurTimes });
            }
            return list;
        }

        TaskRecordEntity BuildLevelopenTask(int taskId, Guid managerId, int managerLevel)
        {
            var entity = InnerBuildTask(taskId, managerLevel, managerId);
            entity.Status = 0;
            return entity;
        }

        TaskRecordEntity BuildPendingTask(int taskId, int needLevel, Guid managerId)
        {
            var entity = InnerBuildTask(taskId, needLevel, managerId);
            entity.Status = -1;
            entity.IsPending = true;
            return entity;
        }

        private TaskRecordEntity InnerBuildTask(int taskId, int needLevel, Guid managerId)
        {
            var entity = new TaskRecordEntity();
            entity.TaskId = taskId;
            entity.CurTimes = 0;
            entity.DoneParam = new byte[0];
            entity.ManagerId = managerId;
            entity.RowTime = DateTime.Now;
            entity.StepRecord = "";
            entity.ManagerLevel = needLevel;
            entity.UpdateTime = DateTime.Now;
            return entity;
        }

        List<TaskRecordEntity> GetTaskList(Guid managerId)
        {
            return TaskRecordMgr.GetByManager(managerId);
        }

        MessageCode SaveSubmit(TaskDailyrecordEntity dailyrecord, int prizePoint, int prizeExp, int prizeCoin, int prizeItemCode, List<TaskRecordEntity> newTasks, NbManagerEntity manager, ItemPackageFrame package, NbManagerextraEntity managerextra, int bindPoint,int prizeFriendShipPoint, ref PayUserEntity payUserEntity)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveSubmit(transactionManager.TransactionObject, newTasks, manager, package, managerextra, bindPoint,prizeFriendShipPoint, ref payUserEntity);
                    if (messageCode != ShareUtil.SuccessCode)
                    {
                        transactionManager.Rollback();
                    }
                    int returnCode = -2;
                    TaskDailyrecordMgr.FinishPrize(dailyrecord.ManagerId, prizeExp, prizeCoin, prizeItemCode,
                        prizePoint, dailyrecord.RecordDate, dailyrecord.RowVersion, ref returnCode,
                        transactionManager.TransactionObject);
                    if (returnCode != 0)
                    {
                        transactionManager.Rollback();
                    }
                    //messageCode = PayCore.Instance.AddBonus(manager.Account, prizePoint,
                    //    EnumChargeSourceType.DailyTaskFinishPrize,string.Format("{0}@{1:yyyyMMdd}",manager.Idx,dailyrecord.RecordDate)
                    //    , transactionManager.TransactionObject);
                    //if (messageCode != ShareUtil.SuccessCode)

                    if (messageCode == ShareUtil.SuccessCode)
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
                SystemlogMgr.Error("TaskSaveSubmit", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode SaveSubmit(TaskDailyrecordEntity dailyrecord, int taskId, string stepRecord, int curTimes, int prizeExp, int prizeCoin, int prizeItemCode, List<TaskRecordEntity> newTasks, NbManagerEntity manager, ItemPackageFrame package, NbManagerextraEntity managerextra, int point, int prizeFriendShipPoint, ref PayUserEntity payUserEntity)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveSubmit(transactionManager.TransactionObject, newTasks, manager, package, managerextra, point,prizeFriendShipPoint, ref payUserEntity);
                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        int returnCode = -2;
                        TaskDailyrecordMgr.Submit(dailyrecord.ManagerId, taskId, stepRecord, curTimes, dailyrecord.StepRecords, dailyrecord.CurTimes, dailyrecord.Status, prizeExp, prizeCoin, prizeItemCode, dailyrecord.FinishCount, ref returnCode,
                                                  transactionManager.TransactionObject);
                        if (returnCode == 0)
                        {
                            transactionManager.Commit();
                        }
                        else
                        {
                            transactionManager.Rollback();
                        }
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
                SystemlogMgr.Error("TaskSaveSubmit", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode SaveSubmit(int recordId, List<TaskRecordEntity> newTasks, int prizeExp, int prizeCoin, int prizeItemCode, NbManagerEntity manager, ItemPackageFrame package, NbManagerextraEntity managerextra, int point,int prizeFriendShipPoint, ref PayUserEntity payUserEntity)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveSubmit(transactionManager.TransactionObject, newTasks, manager, package, managerextra, point, prizeFriendShipPoint, ref payUserEntity);
                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        int returnCode = -2;
                        TaskRecordMgr.Submit(recordId, prizeExp, prizeCoin, prizeItemCode, ref returnCode, transactionManager.TransactionObject);
                        if (returnCode == 0)
                        {
                            transactionManager.Commit();
                        }
                        else
                        {
                            transactionManager.Rollback();
                        }
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
                SystemlogMgr.Error("TaskSaveSubmit", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SaveSubmit(DbTransaction transaction, List<TaskRecordEntity> newTasks, NbManagerEntity manager, ItemPackageFrame package, NbManagerextraEntity managerextra, int point,int prizeFriendShipPoint, ref PayUserEntity payUserEntity)
        {
            if (!ManagerUtil.SaveManagerData(manager, managerextra, false, transaction))
                return MessageCode.NbUpdateFailManager;
            if (newTasks != null && newTasks.Count > 0)
            {
                foreach (var entity in newTasks)
                {
                    if (!TaskRecordMgr.Add(entity, transaction))
                        return MessageCode.NbUpdateFail;
                }
            }
            if (package != null)
            {
                if (!package.SaveTask(transaction))
                {
                    return MessageCode.NbUpdateFailPackage;
                }
            }
            if (managerextra != null)
            {
                if (
                    !NbManagerextraMgr.UpdateGuidePrize(managerextra.ManagerId, managerextra.HasGuidePrize,
                                                        managerextra.GuidePrizeExpired, managerextra.GuidePrizeCount, managerextra.GuidePrizeLastDate, transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
            }
            if (point > 0)
            {
                //任务奖励点券
                PayCore.Instance.AddBonus(manager.Idx, point, EnumChargeSourceType.TaskPrize,
                    ShareUtil.GenerateComb().ToString(), transaction);

                payUserEntity = PayCore.Instance.GetPayUser(manager.Idx);


                //if (payUserEntity == null)
                //{
                //    payUserEntity = new PayUserEntity();
                //    payUserEntity.Account = manager.Account;
                //    payUserEntity.IsNew = true;
                //    payUserEntity.BindPoint += bindPoint;
                //    payUserEntity.RowTime = DateTime.Now;
                //}
                //else
                //{
                //    payUserEntity.BindPoint += bindPoint;
                //}
                //if (payUserEntity.IsNew)
                //{
                //    if (!PayUserMgr.Insert(payUserEntity, transaction))
                //        return MessageCode.NbUpdateFail;
                //}
                //else
                //{
                //    if (!PayUserMgr.Update(payUserEntity, transaction))
                //        return MessageCode.NbUpdateFail;
                //}
            }
            if (prizeFriendShipPoint > 0)
            {
               var code= ManagerCore.Instance.UpdateFriendShipPoint(manager, prizeFriendShipPoint, EnumActionType.Add,
                    transaction);
                if (code != MessageCode.Success)
                    return code;
            }


            return MessageCode.Success;
        }

        MessageCode SaveGuidePrize(ItemPackageFrame package, NbManagerextraEntity managerextra)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveGuidePrize(transactionManager.TransactionObject, package, managerextra);
                    if (messageCode == ShareUtil.SuccessCode)
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
                SystemlogMgr.Error("SaveGuidePrize", ex);
                return MessageCode.Exception;
            }
        }
        MessageCode Tran_SaveGuidePrize(DbTransaction transaction, ItemPackageFrame package, NbManagerextraEntity managerextra)
        {
            if (package != null)
            {
                if (!package.SaveTask(transaction))
                {
                    return MessageCode.NbUpdateFailPackage;
                }
            }
            if (managerextra != null)
            {
                if (
                    !NbManagerextraMgr.UpdateGuidePrize(managerextra.ManagerId, managerextra.HasGuidePrize,
                                                        managerextra.GuidePrizeExpired, managerextra.GuidePrizeCount, managerextra.GuidePrizeLastDate, transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
            }
            return MessageCode.Success;
        }
        #endregion
    }
}
