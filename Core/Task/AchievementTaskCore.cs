using System;
using System.Collections.Generic;
using System.Data.Common;
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
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Task;
using Games.NBall.Entity.Share;

namespace Games.NBall.Core.Task
{
    public class AchievementTaskCore : BaseSingleton
    {
        public AchievementTaskCore(int p)
            : base(p)
        {
        }

        public static AchievementTaskCore Instance
        {
            get { return SingletonFactory<AchievementTaskCore>.SInstance; }
        }

        /// <summary>
        /// 更新卡牌收集数量
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public MessageCode UpdatePlayCardCount(ItemPackageFrame package,string zoneId="")
        {
            if (package == null)
                return MessageCode.Success;
            var taskRecord = TaskRecordMgr.GetManagerTaskList(package.ManagerId,zoneId);
            if (taskRecord == null || taskRecord.Count == 0)
                return MessageCode.Success;
            foreach (var item in taskRecord)
            {
                var task = CacheFactory.TaskConfigCache.GetTask(item.TaskId);
                if (task == null)
                    continue;
                var require = CacheFactory.TaskConfigCache.GetTaskByRequire(item.TaskId);
                if (require == null)
                    continue;
                switch (require.RequireType)
                {
                    case (int) EnumTaskRequireFunc.PlayerCardStrengthCollection:
                        if (require.RequireSub >= 0 && require.RequireSub <= 4)
                        {
                            if (require.RequireThird >= 2 && require.RequireThird <= 9)
                            {
                                var number2 = package.GetStrengthCardCount(require.RequireSub, require.RequireThird);
                                if (number2 > 0)
                                    SavePackageTask(item, number2, task,zoneId);
                            }
                        }
                        break;
                    case (int) EnumTaskRequireFunc.PlayCardCollection:
                            var number1 = package.GetPlayCountByCardLevel(require.RequireSub);
                        if (number1 > 0)
                            SavePackageTask(item, number1, task, zoneId);
                        break;
                    case (int) EnumTaskRequireFunc.PlayerCardLevel:
                        int number =  package.GetLevelCardCount(require.RequireSub);
                        if (number > 0)
                            SavePackageTask(item, number, task, zoneId);
                        break;
                }
            }
            return MessageCode.Success;
        }


        void SavePackageTask(TaskRecordEntity taskRecord,int number,ConfigTaskEntity taskConfig,string zoneId="",DbTransaction trans=null)
        {
            if (taskRecord == null || taskConfig == null)
                return;
            if (taskRecord.Status != 0)
                return;
            if (taskRecord.CurTimes < number)
            {
                taskRecord.CurTimes = number;
                if (taskRecord.CurTimes >= taskConfig.Times)
                    taskRecord.Status = 1;
                taskRecord.UpdateTime = DateTime.Now;
                TaskRecordMgr.Update(taskRecord, trans,zoneId);
            }
        }


        /// <summary>
        /// 更新天梯赛单场进球及赛季最高胜场
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="goals"></param>
        /// <returns></returns>
        public MessageCode UpdateLadderGoals(Guid managerId, int goals, EnumWinType winType,int score)
        {
            try
            {
                var achievementManager = AchievementManagerMgr.GetById(managerId);
                if (achievementManager == null)
                    return MessageCode.NbParameterError;

                bool needUpdate = false;
                if (goals > achievementManager.MaxLadderGoals)
                {
                    achievementManager.MaxLadderGoals = goals;
                    needUpdate = true;
                }
                if (winType == EnumWinType.Win)
                {
                    var ladderManager = LadderCore.Instance.GetLadderManager(managerId);
                    if (ladderManager != null)
                    {
                        var season = ladderManager.Season;
                        if (season == null)
                            SystemlogMgr.Error("赛季=null", "赛季=null");
                        needUpdate = true;
                        if (achievementManager.LadderSeason == season.Idx)
                            achievementManager.LadderWin++;
                        else
                        {
                            achievementManager.LadderSeason = season.Idx;
                            achievementManager.LadderWin = 1;
                        }
                        if (achievementManager.LadderWin > achievementManager.MaxLadderWin)
                            achievementManager.MaxLadderWin = achievementManager.LadderWin;
                    }
                }

                UpdateAchievementTask(managerId, false, EnumTaskRequireFunc.LadderMatchGoals, false, false, goals, false);
                UpdateAchievementTask(managerId, false, EnumTaskRequireFunc.LadderMatchCount, false, false, 1, true);
                if (winType == EnumWinType.Win)
                {
                    UpdateAchievementTask(managerId, false, EnumTaskRequireFunc.LadderWinCountSeason, false, false, 1, true);
                }
                UpdateAchievementTask(managerId, false, EnumTaskRequireFunc.LadderScore, false, false, score, false);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("更新天梯赛单场进球及赛季最高胜场", ex);
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 更新PK赛单场进球及单日最多进球
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="goals"></param>
        /// <returns></returns>
        public MessageCode UpdatePkMatchGoals(Guid managerId, int goals)
        {
            var achievementManager = AchievementManagerMgr.GetById(managerId);
            if (achievementManager == null)
                return MessageCode.NbParameterError;

            if (goals > achievementManager.MaxPkMatchGoals)
            {
                achievementManager.MaxPkMatchGoals = goals;
            }
            var date = DateTime.Now.Date;
            if (achievementManager.DayPkMatchDate.Date == date)
                achievementManager.DayPkMatchGoals += goals;
            else
            {
                achievementManager.DayPkMatchDate = date;
                achievementManager.DayPkMatchGoals = goals;
            }

            if (achievementManager.DayPkMatchGoals > achievementManager.MaxDayPkMatchGoals)
                achievementManager.MaxDayPkMatchGoals = achievementManager.DayPkMatchGoals;

            UpdateAchievementTask(managerId,false, EnumTaskRequireFunc.PkMatchGoals, false, false, goals, false);
            UpdateAchievementTask(managerId,true, EnumTaskRequireFunc.PkMatchDayGoals, false, false, goals, true);

            //if (AchievementManagerMgr.Update(achievementManager))
            //    return MessageCode.Success;
            //else
                return MessageCode.FailUpdate;
        }

        /// <summary>
        /// 好友赛最高连胜
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="winType"></param>
        /// <returns></returns>
        public MessageCode UpdateFriendMatchComb(Guid managerId, EnumWinType winType, DbTransaction trans = null)
        {
            var achievementManager = AchievementManagerMgr.GetById(managerId);
            if (achievementManager == null)
                return MessageCode.NbParameterError;
            bool isClear = false;
            if (winType == EnumWinType.Win)
            {
                achievementManager.FriendWinComb++;
            }
            else
            {
                achievementManager.FriendWinComb = 0;
                isClear = true;
            }

            UpdateAchievementTask(managerId,false, EnumTaskRequireFunc.FriendMatchWin, false,isClear, 1, true);
            UpdateAchievementTask(managerId, false, EnumTaskRequireFunc.FriendMatchCount, false, false, 1, true);
            UpdateAchievementTask(managerId, false, EnumTaskRequireFunc.PkOrFriendMatchCount, false, false, 1, true);
            if (achievementManager.FriendWinComb > achievementManager.MaxFriendWinComb)
                achievementManager.MaxFriendWinComb = achievementManager.FriendWinComb;
            //if (AchievementManagerMgr.Update(achievementManager, trans))
            //    return MessageCode.Success;
            //else
                return MessageCode.FailUpdate;
        }

        /// <summary>
        /// 更新最高杯赛排名
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public MessageCode UpdateDailyCupRank(Guid managerId, int rank)
        {
            if (rank == 0)
                return MessageCode.Success;
            var taskRecord = TaskRecordMgr.GetManagerTaskList(managerId);
            if (taskRecord == null || taskRecord.Count == 0)
                return MessageCode.Success;
            foreach (var item in taskRecord)
            {
                var task = CacheFactory.TaskConfigCache.GetTask(item.TaskId);
                if (task == null)
                    continue;
                var require = CacheFactory.TaskConfigCache.GetTaskByRequire(item.TaskId);
                if (require == null)
                    continue;
                if (require.RequireType == (int) EnumTaskRequireFunc.DailyCupRank)
                {
                    if (rank <= require.RequireSub)
                    {
                        item.CurTimes = rank;
                        item.UpdateTime = DateTime.Now;
                        item.Status = 1;
                        TaskRecordMgr.Update(item);
                    }
                }
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 更新联赛冠军积分
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public MessageCode UpdateLeagueScore(Guid managerId,int leagueId, int score)
        {
            var achievementManager = AchievementManagerMgr.GetById(managerId);
            if (achievementManager == null)
                return MessageCode.NbParameterError;
            int reqthird = 0;

            bool needUpdate = false;
            switch (leagueId)
            {
                case 1:
                    if (score > achievementManager.LeagueScore1)
                    {
                        achievementManager.LeagueScore1 = score;
                        needUpdate = true;
                    }
                    reqthird = 80;
                    break;
                case 2:
                    if (score > achievementManager.LeagueScore2)
                    {
                        achievementManager.LeagueScore2 = score;
                        needUpdate = true;
                    }
                    
                    reqthird = 80;
                    break;
                case 3:
                    if (score > achievementManager.LeagueScore3)
                    {
                        achievementManager.LeagueScore3 = score;
                        needUpdate = true;
                    }
                    reqthird = 90;
                    break;
                case 4:
                    if (score > achievementManager.LeagueScore4)
                    {
                        achievementManager.LeagueScore4 = score;
                        needUpdate = true;
                    }
                    reqthird = 100;
                    break;
                case 5:
                    if (score > achievementManager.LeagueScore5)
                    {
                        achievementManager.LeagueScore5 = score;
                        needUpdate = true;
                    }
                    reqthird = 100;
                    break;
                case 6:
                    if (score > achievementManager.LeagueScore6)
                    {
                        achievementManager.LeagueScore6 = score;
                        needUpdate = true;
                    }
                    reqthird = 95;
                    break;
                case 7:
                    if (score > achievementManager.LeagueScore7)
                    {
                        achievementManager.LeagueScore7 = score;
                        needUpdate = true;
                    }
                    reqthird = 106;
                    break;
                case 8:
                    if (score > achievementManager.LeagueScore8)
                    {
                        achievementManager.LeagueScore8 = score;
                        needUpdate = true;
                    }
                    reqthird = 110;
                    break;
            }

            UpdateAchievementTask(managerId,false, EnumTaskRequireFunc.LeagueChampionScore, false,false,score, false, leagueId, reqthird);

            //if (needUpdate)
            //{
            //    if (AchievementManagerMgr.Update(achievementManager))
            //        return MessageCode.Success;
            //    else
            //        return MessageCode.FailUpdate;
            //}
            return MessageCode.Success;
        }

        /// <summary>
        /// 更新成就
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="isOddDays">是否是单日</param>
        /// <param name="type">任务类型</param>
        /// <param name="isRank">是否是排名</param>
        /// <param name="isClear">是否清空数据</param>
        /// <param name="curTime">数量</param>
        /// <param name="isOverlay">是否叠加</param>
        /// <param name="reqsub">二级类型</param>
        /// <param name="reqthird">三级类型</param>
        /// <param name="overstate">四级类型</param>
        public void UpdateAchievementTask(Guid managerId,bool isOddDays, EnumTaskRequireFunc type,bool isRank,bool isClear, int curTime, bool isOverlay, int reqsub = 0,
            int reqthird = 0, int overstate = 0, List<TaskRecordEntity> taskRecord = null,DbTransaction trans =null)
        {
            var taskList = CacheFactory.TaskConfigCache.GetAchievementTask(type, reqsub, reqthird, overstate);
            if(taskRecord == null)
               taskRecord = TaskRecordMgr.GetManagerTaskList(managerId);
            bool isUpdate = true;
            foreach (var item in taskRecord)
            {
                isUpdate = true;
                try
                {
                    if (item.Status != 0)
                        continue;
                    if (!taskList.Exists(r => r.TaskId == item.TaskId))
                        continue;
                    var taskConfig = CacheFactory.TaskConfigCache.GetTask(item.TaskId);
                    if (taskConfig == null)
                        continue;
                    if (isOddDays)
                    {
                        if (item.UpdateTime.Date != DateTime.Now.Date)
                        {
                            item.CurTimes = 0;
                        }
                    }
                    if (isOverlay)
                    {
                        item.CurTimes += curTime;
                    }
                    else
                    {
                        if (isRank)
                        {
                            if (item.CurTimes > curTime)
                                item.CurTimes = curTime;
                            else
                                isUpdate = false;
                        }
                        else
                        {
                            if (item.CurTimes < curTime)
                                item.CurTimes = curTime;
                            else
                                isUpdate = false;
                        }

                    }
                    if (isClear)
                        item.CurTimes = 0;

                    if (taskConfig.Times <= item.CurTimes)
                    {
                        item.Status = 1;
                        if (item.CurTimes > taskConfig.Times)
                            item.CurTimes = taskConfig.Times;
                    }
                    item.UpdateTime = DateTime.Now;
                    if (isUpdate)
                        TaskRecordMgr.Update(item, trans);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("增加成就任务记录", ex);
                }
            }
        }

        /// <summary>
        /// 更新成就背包
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="isOddDays">是否是单日</param>
        /// <param name="type">任务类型</param>
        /// <param name="isRank">是否是排名</param>
        /// <param name="isClear">是否清空数据</param>
        /// <param name="curTime">数量</param>
        /// <param name="isOverlay">是否叠加</param>
        /// <param name="reqsub">二级类型</param>
        /// <param name="reqthird">三级类型</param>
        /// <param name="overstate">四级类型</param>
        public void UpdateAchievementTaskPackage(Guid managerId, bool isOddDays, EnumTaskRequireFunc type, bool isRank, bool isClear, int curTime, bool isOverlay, int reqsub = 0,
            int reqthird = 0, int overstate = 0, List<TaskRecordEntity> taskRecord = null, DbTransaction trans = null)
        {
            var taskList = CacheFactory.TaskConfigCache.GetAchievementTask(type, reqsub, reqthird, overstate);
            if (taskRecord == null)
                taskRecord = TaskRecordMgr.GetManagerTaskList(managerId);
            bool isUpdate = true;
            foreach (var item in taskRecord)
            {
                try
                {
                    if (item.Status != 0)
                        continue;
                    if (!taskList.Exists(r => r.TaskId == item.TaskId))
                        continue;
                    var taskConfig = CacheFactory.TaskConfigCache.GetTask(item.TaskId);
                    if (taskConfig == null)
                        continue;
                    if (isOddDays)
                    {
                        if (item.UpdateTime.Date != DateTime.Now.Date)
                        {
                            item.CurTimes = 0;
                        }
                    }
                    if (isOverlay)
                        item.CurTimes += curTime;
                    else
                    {
                        if (isRank)
                        {
                            if (item.CurTimes > curTime)
                                item.CurTimes = curTime;
                            else
                                isUpdate = false;
                        }
                        else
                        {
                            if (item.CurTimes < curTime)
                                item.CurTimes = curTime;
                            else
                                isUpdate = false;
                        }

                    }
                    if (isClear)
                        item.CurTimes = 0;

                    if (taskConfig.Times <= item.CurTimes)
                    {
                        item.Status = 1;
                        if (item.CurTimes > taskConfig.Times)
                            item.CurTimes = taskConfig.Times;
                    }
                    item.UpdateTime = DateTime.Now;
                    if (isUpdate)
                        TaskRecordMgr.Update(item, trans);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("增加成就任务记录", ex);
                }
            }
        }
    }
}
