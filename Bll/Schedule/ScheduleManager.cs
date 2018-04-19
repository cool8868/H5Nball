using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Bll.Schedule
{
    public class ScheduleManager : IDisposable
    {
        private bool _disableSchedule = false;
        #region .ctor

        public ScheduleManager(int p)
        {
            string d = ConfigurationManager.AppSettings["DisableSchedule"];
            if (!string.IsNullOrEmpty(d) && d == "1")
            {
                LogHelper.Insert("Schedule has disabled", LogType.Info);
                _disableSchedule = true;
            }
            Init();
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                _timer.Dispose();
                _timer = null;
            }
        }

        #endregion

        ~ScheduleManager()
        {
            Dispose();
        }
        #endregion

        #region Facade

        public static ScheduleManager Instance
        {
            get { return SingletonFactory<ScheduleManager>.SInstance; }
        }

        /// <summary>
        /// 计划任务执行者
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public delegate MessageCode ScheduleDelegateParam(string parameters);

        public delegate MessageCode ScheduleDelegateNextTime(DateTime nextTime);

        public delegate MessageCode ScheduleDelegate();

        public bool RegisterSchedule(EnumSchedule schedule, ScheduleDelegate scheduleDelegate)
        {
            if (_disableSchedule)
            {
                return false;
            }
            var config = ScheduleConfig.Instance.GetEntity(schedule);
            if (config == null)
            {
                SystemlogMgr.Error("Schedule register", "can't find schedule config,id:" + (int)schedule);
                return false;
            }
            ScheduleInfo info = new ScheduleInfo(scheduleDelegate, config);
            _rwl.AcquireWriterLock(Timeout.Infinite);
            try
            {
                _scheduleInfoDict.Add((int)schedule, info);
                LogHelper.Insert(schedule.ToString() + " register success.", LogType.Info);
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
            return true;
        }

        public bool RegisterSchedule(EnumSchedule schedule, ScheduleDelegateParam scheduleDelegate)
        {
            if (_disableSchedule)
                return false;
            var config = ScheduleConfig.Instance.GetEntity(schedule);
            if (config == null)
            {
                SystemlogMgr.Error("Schedule register", "can't find schedule config,id:" + (int)schedule);
                return false;
            }
            ScheduleInfo info = new ScheduleInfo(scheduleDelegate, config);
            _rwl.AcquireWriterLock(Timeout.Infinite);
            try
            {
                _scheduleInfoDict.Add((int)schedule, info);
                LogHelper.Insert(schedule.ToString() + " register success.", LogType.Info);
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
            return true;
        }

        public bool RegisterSchedule(EnumSchedule schedule, ScheduleDelegateNextTime scheduleDelegate)
        {
            if (_disableSchedule)
                return false;
            var config = ScheduleConfig.Instance.GetEntity(schedule);
            if (config == null)
            {
                SystemlogMgr.Error("Schedule register", "can't find schedule config,id:" + (int)schedule);
                return false;
            }
            ScheduleInfo info = new ScheduleInfo(scheduleDelegate, config);
            _rwl.AcquireWriterLock(Timeout.Infinite);
            try
            {
                _scheduleInfoDict.Add((int)schedule, info);
                LogHelper.Insert(schedule.ToString() + " register success.", LogType.Info);
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
            return true;
        }
        #endregion

        #region encapsulation
        private Timer _timer = null;
        private ReaderWriterLock _rwl = new ReaderWriterLock();
        private Dictionary<int, ScheduleInfo> _scheduleInfoDict;

        void Init()
        {
            LogHelper.Insert("Scheduler initializing ...", LogType.Info);

            ScheduleConfig.Instance.GetHashCode();
            _scheduleInfoDict = new Dictionary<int, ScheduleInfo>();
            _timer = new Timer(new TimerCallback(TimerCallback), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

            LogHelper.Insert("Scheduler init finish.", LogType.Info);
        }

        //0 for false, 1 for true
        private int _isTimerCallbacking = 0;
        private void TimerCallback(object state)
        {
            if (_disableSchedule)
            {
                return;
            }
            if (_scheduleInfoDict.Count <= 0)
                return;
            if (0 == Interlocked.Exchange(ref _isTimerCallbacking, 1))
            {
                //不可重入
                try
                {
                    DateTime nowTime = DateTime.Now;
                    _rwl.AcquireReaderLock(Timeout.Infinite);
                    try
                    {
                        foreach (var info in _scheduleInfoDict.Values)
                        {
                            info.Invoke(nowTime);
                        }
                    }
                    finally
                    {
                        _rwl.ReleaseReaderLock();
                    }
                }
                finally
                {
                    //Release the lock
                    Interlocked.Exchange(ref _isTimerCallbacking, 0);
                }
            }
        }
        #endregion

    }
}
