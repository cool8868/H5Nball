using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class TaskConfigCache:BaseSingleton
    {

        public TaskConfigCache(int p)
            :base(p)
        {
            InitCache();
        }

        #region encapsulation
        /// <summary>
        /// taskid->entity
        /// </summary>
        private Dictionary<int, ConfigTaskEntity> _taskDic;
        /// <summary>
        /// taskType->parentId->entity
        /// 不包括每日任务
        /// </summary>
        private Dictionary<int, List<ConfigTaskEntity>> _taskParentDic;
        /// <summary>
        /// 成就任务
        /// </summary>
        private Dictionary<int, List<ConfigTaskrequireEntity>> _taskAchievement;

        /// <summary>
        /// 成就任务对应
        /// </summary>
        private Dictionary<int, ConfigTaskrequireEntity> _taskRequire;

        /// <summary>
        /// managerLevel->entity
        /// 只要经理等级就能开放的任务
        /// </summary>
        private Dictionary<int, List<ConfigTaskEntity>> _managerlevelDic;
        /// <summary>
        /// 每日任务列表
        /// </summary>
        private Dictionary<int, ConfigTaskEntity> _dailyTaskDic;

        /// <summary>
        /// 每日任务列表
        /// </summary>
        private Dictionary<int, ConfigTaskEntity> _dailyRandomTaskDic;

        private readonly int _dailytaskManagerLevel = 16;

        void InitCache()
        {
            LogHelper.Insert("Task cache init start", LogType.Info);
            var list = ConfigTaskMgr.GetAllForCache();
            var requireList = ConfigTaskrequireMgr.GetAll();
            _taskDic = list.ToDictionary(d=>d.Idx,d=>d);
            _taskParentDic =new Dictionary<int, List<ConfigTaskEntity>>();
            _taskAchievement = new Dictionary<int, List<ConfigTaskrequireEntity>>();
            _taskRequire = new Dictionary<int, ConfigTaskrequireEntity>();
            _managerlevelDic = new Dictionary<int, List<ConfigTaskEntity>>();
            int dailytaskType = (int) EnumTaskType.Daily;
            _dailyTaskDic = new Dictionary<int, ConfigTaskEntity>();
            _dailyRandomTaskDic = new Dictionary<int, ConfigTaskEntity>();
            //_dailytaskList2 = list.FindAll(d => d.TaskType == dailytaskType);
            foreach (var entity in _taskDic.Values)
            {
                entity.RequireList = new List<ConfigTaskrequireEntity>();
                entity.RequireFuncDic = new Dictionary<int, int>();
                if (entity.TaskType != dailytaskType && entity.TaskType != (int) EnumTaskType.DailyRandom)
                {
                    if (entity.ParentId != 0)
                    {
                        if (!_taskParentDic.ContainsKey(entity.ParentId))
                            _taskParentDic.Add(entity.ParentId, new List<ConfigTaskEntity>());
                        _taskParentDic[entity.ParentId].Add(entity);
                    }
                    else
                    {
                        if (!_managerlevelDic.ContainsKey(entity.ManagerLevel))
                        {
                            _managerlevelDic.Add(entity.ManagerLevel, new List<ConfigTaskEntity>());
                        }
                        _managerlevelDic[entity.ManagerLevel].Add(entity);
                    }
                    if (entity.TaskType == (int) EnumTaskType.Achievement)
                    {
                        if (!_taskAchievement.ContainsKey(entity.Idx))
                        {
                            var task = requireList.Find(r => r.TaskId == entity.Idx);
                            if (task != null)
                            {
                                var key = CreateKey(task.RequireType, task.RequireSub, task.RequireThird, task.OverState);
                                if (!_taskAchievement.ContainsKey(key))
                                    _taskAchievement.Add(key, new List<ConfigTaskrequireEntity>());
                                _taskAchievement[key].Add(task);
                               
                            }
                        }
                    }
                }
                else if (entity.TaskType == dailytaskType)
                {
                    if (!_dailyTaskDic.ContainsKey(entity.Idx))
                    {
                        _dailyTaskDic.Add(entity.Idx, entity);
                    }
                }
                else if (entity.TaskType == (int) EnumTaskType.DailyRandom)
                {
                    if (!_dailyRandomTaskDic.ContainsKey(entity.Idx))
                    {
                        _dailyRandomTaskDic.Add(entity.Idx, entity);
                    }
                }
            }

            foreach (var item in requireList)
            {
                if (!_taskRequire.ContainsKey(item.TaskId))
                    _taskRequire.Add(item.TaskId, item);
            }

            var list2 = ConfigTaskrequireMgr.GetAll();

            foreach (var entity in list2)
            {
                var task = _taskDic[entity.TaskId];
                if (!string.IsNullOrEmpty(task.RequireFuncs))
                    task.RequireFuncs += ",";
                task.RequireFuncs += entity.RequireType;
                task.RequireList.Add(entity);
                if(!task.RequireFuncDic.ContainsKey(entity.RequireType))
                    task.RequireFuncDic.Add(entity.RequireType,0);
            }
            LogHelper.Insert("Task cache init end", LogType.Info);
        }
        #endregion

        #region Facade

        public static TaskConfigCache Instance
        {
            get { return SingletonFactory<TaskConfigCache>.SInstance; }
        }

        public ConfigTaskEntity GetTask(int taskId)
        {
            if (_taskDic.ContainsKey(taskId))
                return _taskDic[taskId];
            return null;
        }

        public int CreateKey(int reqtype,int reqsub,int reqthird,int overstate)
        {
            return reqtype*1000000 + reqsub*10000 + reqthird*100 + overstate;
        }

        public ConfigTaskEntity RandomDailyTask(int curTask)
        {
            if (_dailyRandomTaskDic.ContainsKey(curTask))
                return _dailyRandomTaskDic[curTask];
            return null;
        }

        public ConfigTaskrequireEntity GetTaskByRequire(int taskId)
        {
            if (_taskRequire.ContainsKey(taskId))
                return _taskRequire[taskId];
            return null;
        }

        /// <summary>
        /// 根据require获取任务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reqsub"></param>
        /// <param name="reqthird"></param>
        /// <param name="overstate"></param>
        /// <returns></returns>
        public List<ConfigTaskrequireEntity> GetAchievementTask(EnumTaskRequireFunc type, int reqsub = 0,
            int reqthird = 0, int overstate = 0)
        {
            var key = CreateKey((int)type, reqsub, reqthird, overstate);
            if (_taskAchievement.ContainsKey(key))
                return _taskAchievement[key];
            return new List<ConfigTaskrequireEntity>();
        }

        public string RandomDailyTaskList(int count)
        {
            var taskList = new List<int>();
            taskList.AddRange(_dailyTaskDic.Values.Select(entity => entity.Idx));

            if (_dailyRandomTaskDic.Count <= count)
                taskList.AddRange(_dailyRandomTaskDic.Values.Select(entity => entity.Idx));
            else
            {
                var allDailyRadomTaskList = RandomHelper.GetRandomTestSortList(_dailyRandomTaskDic.Values.ToList());
                for (int i = 0; i < count; i++)
                {
                    taskList.Add(allDailyRadomTaskList[i].Idx);
                }
            }

            return string.Join(",", taskList);
        }

        public List<ConfigTaskEntity> GetNextTask(int taskType, int taskId)
        {
            if (_taskParentDic.ContainsKey(taskId))
                return _taskParentDic[taskId];
            return null;
        }

        public List<ConfigTaskEntity> GetLevelOpenTasks(int managerLevel)
        {
            if (_managerlevelDic.ContainsKey(managerLevel))
                return _managerlevelDic[managerLevel];
            return null;
        }
        #endregion
    }
}
