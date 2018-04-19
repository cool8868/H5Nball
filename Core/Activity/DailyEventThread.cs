using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;

namespace Games.NBall.Core.Activity
{
    public class DailyEventThread : IDisposable
    {
        private Dictionary<int, DailyEventInfo> _dailyeventDic;
        private Timer _timer = null;
        private ReaderWriterLock _rwl = new ReaderWriterLock();
        private bool _disableSchedule = false;
        #region .ctor
        public DailyEventThread(int p)
        {
            string d = ConfigurationManager.AppSettings["DisableSchedule"];
            if (!string.IsNullOrEmpty(d) && d == "1")
            {
                LogHelper.Insert("Schedule has disabled", LogType.Info);
                _disableSchedule = true;
            }
            _dailyeventDic = new Dictionary<int, DailyEventInfo>();
            _timer = new Timer(new TimerCallback(TimerCallback), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
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

        ~DailyEventThread()
        {
            Dispose();
        }
        #endregion

        #region Facade
        public static DailyEventThread Instance
        {
            get { return SingletonFactory<DailyEventThread>.SInstance; }
        }

        public List<MonitorDailyeventEntity> GetListForShow()
        {
            try
            {
                var list = MonitorDailyeventMgr.GetListForShow(ShareUtil.ZoneId);
                if (list != null)
                {
                    foreach (var entity in list)
                    {
                        double date = Convert.ToDouble((DateTime.Now.Date - entity.OpenTime.Date).Days);
                        if (date < 0)
                            date = 0;
                        entity.OpenTimeTick = ShareUtil.GetTimeTick(entity.OpenTime.AddDays(date));
                        entity.StartTimeTick = ShareUtil.GetTimeTick(entity.StartTime.AddDays(date));
                        entity.EndTimeTick = ShareUtil.GetTimeTick(entity.EndTime.AddDays(date));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("DailyEventThread-GetListForShow", ex);
                return null;
            }
            
        }

        public delegate MessageCode DailyeventStartDelegate(DateTime startTime,DateTime endTime);
        
        private int _isTimerCallbacking = 0;
        private void TimerCallback(object state)
        {
            if (_disableSchedule)
            {
                return;
            }
            if(_dailyeventDic.Count<=0)
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
                        foreach (var info in _dailyeventDic.Values)
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

        public bool RegisterEvent(EnumDailyevent eventEnum, DailyeventStartDelegate startDelegate)
        {
            var eventType = (int) eventEnum;
            if (_dailyeventDic.ContainsKey(eventType))
                return true;
            var config = CacheFactory.ActivityCache.GetDailyevent(eventType);
            if (config == null)
            {
                SystemlogMgr.Error("Dailyevent register", "can't find dailyevent config,id:" + eventType);
                return false;
            }
            DateTime curDate = DateTime.Today;
            _rwl.AcquireWriterLock(Timeout.Infinite);
            try
            {
                var dailyEvent = MonitorDailyeventMgr.GetByZoneEvent(ShareUtil.ZoneId, eventType);
                if (dailyEvent == null)
                {
                    var lastDate = curDate.AddDays(-1);
                    dailyEvent=new MonitorDailyeventEntity();
                    dailyEvent.EventType = eventType;
                    dailyEvent.ZoneId = ShareUtil.ZoneId;
                    dailyEvent.OpenTime =CalTime(lastDate,config.OpenHour,config.OpenMinute);
                    dailyEvent.StartTime = CalTime(lastDate, config.StartHour, config.StartMinute);
                    dailyEvent.EndTime = CalTime(lastDate, config.EndHour, config.EndMinute);
                    dailyEvent.RecordDate = lastDate;
                    dailyEvent.NextInvokeTime = dailyEvent.OpenTime.AddDays(1);
                    dailyEvent.Status = 0;
                    dailyEvent.UpdateTime = DateTime.Now;
                    MonitorDailyeventMgr.Insert(dailyEvent);
                }
                _dailyeventDic.Add(eventType, new DailyEventInfo(dailyEvent, config, startDelegate));
                LogHelper.Insert(eventEnum.ToString() + " register success.", LogType.Info);
                return true;
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
            
        }
        #endregion

        #region encapsulation

        public static DateTime CalTime(DateTime curDate, int hour, int minute)
        {
            return curDate.AddHours(hour).AddMinutes(minute);
        }
        #endregion
    }

    public class DailyEventInfo
    {

        private DailyEventThread.DailyeventStartDelegate _startDelegate;
        private MonitorDailyeventEntity _dailyeventEntity;
        private ConfigDailyeventtimeEntity _config;
        private DateTime _nextInvokeTime;
        private DateTime _endTime;
        public DailyEventInfo()
        {
            
        }

        public DailyEventInfo(MonitorDailyeventEntity dailyeventEntity, ConfigDailyeventtimeEntity config, DailyEventThread.DailyeventStartDelegate startDelegate)
        {
            _dailyeventEntity = dailyeventEntity;
            _startDelegate = startDelegate;
            _config = config;
            CalNextTime(DateTime.Now);
        }
        private int _syncInvoke = 0;
        public void Invoke(DateTime curTime)
        {

            if (curTime < _nextInvokeTime)
                return;
            else if (curTime >= _endTime)
            {
                CalNextTime(curTime);
                return;
            }
            if (0 == Interlocked.Exchange(ref _syncInvoke, 1))
            {
                try
                {
                    DateTime curDate = curTime.Date;
                    var openTime = DailyEventThread.CalTime(curDate, _config.OpenHour, _config.OpenMinute);
                    var startTime = DailyEventThread.CalTime(curDate, _config.StartHour, _config.StartMinute);
                    var endTime = DailyEventThread.CalTime(curDate, _config.EndHour, _config.EndMinute);
                    _dailyeventEntity.OpenTime = openTime;
                    _dailyeventEntity.StartTime = startTime;
                    _dailyeventEntity.EndTime = endTime;
                    _dailyeventEntity.RecordDate = curDate;
                    try
                    {
                        _startDelegate(startTime, endTime);
                    }
                    catch (Exception ex)
                    {
                        SystemlogMgr.Error("Dailyevent info Invoke inner", ex);
                    }
                    
                    MonitorDailyeventMgr.Save(_dailyeventEntity.ZoneId, _dailyeventEntity.EventType,
                        _dailyeventEntity.OpenTime, _dailyeventEntity.StartTime, _dailyeventEntity.EndTime,
                        _dailyeventEntity.RecordDate);
                    CalNextTime(curTime);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("Dailyevent info Invoke", ex);
                }
                finally
                {
                    Interlocked.Exchange(ref _syncInvoke, 0);
                }
            }
        }

        void CalNextTime(DateTime curTime)
        {
            DateTime curDate = curTime.Date;
            if (_dailyeventEntity.RecordDate != curTime.Date)
            {
                var openTime = DailyEventThread.CalTime(curDate, _config.OpenHour, _config.OpenMinute);
                var endTime = DailyEventThread.CalTime(curDate, _config.EndHour, _config.EndMinute);
                if (curTime >= openTime)
                {
                    if (curTime > endTime)
                    {
                        openTime = openTime.AddDays(1);
                        endTime = endTime.AddDays(1);
                    }
                }
                _nextInvokeTime = openTime;
                _endTime = endTime;
            }
            else
            {
                var nextDate = curDate.AddDays(1);
                _nextInvokeTime = DailyEventThread.CalTime(nextDate, _config.OpenHour, _config.OpenMinute);
                _endTime = DailyEventThread.CalTime(nextDate, _config.EndHour, _config.EndMinute);
            }
        }
    }

}
