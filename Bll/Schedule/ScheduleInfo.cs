using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Bll.Schedule
{
    internal class ScheduleInfo
    {
        private int _scheduleTimes;

        private string _parameters;

        private int _invokedTimes = 0;
        private int _maxRetryTimes = 0;
        private bool _enable = true;
        private bool _isIntensive = false;

        private DateTime _nextInvokedDateTime = DateTime.MinValue;

        private ConfigScheduleEntity _scheduleConfig;
        private MonitorScheduleEntity _monitor;
        private BaseScheduleTimeConfig _timeConfig;
        private BaseScheduleLauncher _launcher;

        private void Init(ConfigScheduleEntity configSchedule)
        {
            _monitor = SystemlogMgr.GetScheduleMonitor(configSchedule.Idx);
            if (_monitor == null)
            {
                throw new Exception("[ScheduleInfo] no monitor,schedule id:" + configSchedule.Idx);
            }
            _monitor.ScheduleTimeType = configSchedule.TimeType;
            _scheduleConfig = configSchedule;
            _scheduleTimes = configSchedule.Times;
            bool isChampion = CheckIsChampion(configSchedule.Idx);
            switch (configSchedule.TimeType)
            {
                default:
                case (int)EnumScheduleTimeType.TimeTable:
                    _timeConfig = new ScheduleTimeConfigTimeTable(configSchedule.Setting, isChampion);
                    break;
                case (int)EnumScheduleTimeType.Interval:
                    _timeConfig = new ScheduleTimeConfigInterval(configSchedule.Setting);
                    break;
            }
            if (_scheduleConfig.RetryTimes > 0)
                _maxRetryTimes = _scheduleConfig.RetryTimes - 1;
            if (configSchedule.RunDelay >= 0)
                _nextInvokedDateTime = DateTime.Now.AddMinutes(configSchedule.RunDelay);
            else
            {
                _nextInvokedDateTime = _timeConfig.GetNext(DateTime.Now);
            }
            if (_scheduleConfig.TimeType == (int)EnumScheduleTimeType.Interval && _timeConfig.GetIntervalTime() < 1000)
            {
                _isIntensive = true;
            }
            Log("下次时间" + _nextInvokedDateTime);
        }

        bool CheckIsChampion(int idx)
        {
            if (idx >= 47 && idx <= 61)
                return true;
            return false;
        }

        internal ScheduleInfo(ScheduleManager.ScheduleDelegate scheduleDelegate, ConfigScheduleEntity configSchedule)
        {
            if (scheduleDelegate == null)
                throw new Exception("Init ScheduleInfo fail,scheduleDelegate is null");
            _launcher = new ScheduleLauncher(scheduleDelegate);
            Init(configSchedule);
        }

        internal ScheduleInfo(ScheduleManager.ScheduleDelegateParam scheduleDelegate, ConfigScheduleEntity configSchedule)
        {
            if (scheduleDelegate == null)
                throw new Exception("Init ScheduleInfo fail,scheduleDelegate is null");
            _launcher = new ScheduleLauncherParam(scheduleDelegate, configSchedule.Parameters);
            Init(configSchedule);
        }

        internal ScheduleInfo(ScheduleManager.ScheduleDelegateNextTime scheduleDelegate, ConfigScheduleEntity configSchedule)
        {
            if (scheduleDelegate == null)
                throw new Exception("Init ScheduleInfo fail,scheduleDelegate is null");
            Init(configSchedule);
            _launcher = new ScheduleLauncherNextTime(scheduleDelegate, _timeConfig.GetIntervalTime());

        }

        private int _syncInvoke = 0;
        internal void Invoke(DateTime datetime)
        {
            // considered occasion that _scheduleTimes == -1 which means infinite
            if (!IsEnable || (_scheduleTimes >= 0 && _invokedTimes >= _scheduleTimes))
            {
                return;
            }
            if (datetime < _nextInvokedDateTime)
                return;
            if (0 == Interlocked.Exchange(ref _syncInvoke, 1))
            {
                try
                {
                    InvokeImmediately(datetime);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("Schedule info Invoke", ex);
                }
                finally
                {
                    Interlocked.Exchange(ref _syncInvoke, 0);
                }
            }
        }

        void Log(string msg)
        {
            if (_scheduleConfig.Idx >= 47 && _scheduleConfig.Idx < 100)
            {
                LogHelper.Insert(msg, LogType.Info);
            }
        }

        void LogWithTime(string msg)
        {
            //Log(string.Format("{0},at {1}", msg, DateTime.Now));
        }

        internal void InvokeImmediately(DateTime now)
        {
            bool invokeResult = false;
            try
            {
                LogWithTime("Schedule InvokeImmediately start");
                MonitorStart(now);
                MessageCode result = MessageCode.NbUpdateFail;
                while (result != MessageCode.Success && _monitor.RetryTimes <= _maxRetryTimes)
                {
                    LogWithTime("Schedule Excute");
                    result = _launcher.Excute();
                    if (result == MessageCode.Success)
                    {
                        _monitor.Status = (int)EnumScheduleStatus.Succeed;
                    }
                    else
                    {
                        _monitor.RetryTimes++;
                        if (_monitor.RetryTimes <= _maxRetryTimes)
                        {
                            _monitor.Status = (int)EnumScheduleStatus.Retry;
                        }
                        else
                        {
                            _monitor.Status = (int)EnumScheduleStatus.Fail;
                        }
                    }

                }
                invokeResult = true;
                _nextInvokedDateTime = _timeConfig.GetNext(now);
                MonitorFinish((int)result);
                LogWithTime("Schedule InvokeImmediately finish");
            }
            catch (Exception e)
            {
                SystemlogMgr.Error("Schedule InvokeImmediately", e);
            }

            if (invokeResult)
            {
                if (_scheduleTimes > 0)
                {
                    _invokedTimes++;
                }
            }
        }

        internal bool IsEnable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        void MonitorStart(DateTime now)
        {
            _monitor.Status = (int)EnumScheduleStatus.Start;
            _monitor.StartTime = now;
            _monitor.RetryTimes = 0;
            if (_isIntensive)
            {
                if (_monitor.SuccessTimes % 10 != 0)
                    return;
            }
            SystemlogMgr.ScheduleStart(_monitor.Idx, _monitor.Status, _monitor.StartTime);
        }

        void MonitorFinish(int returnCode)
        {
            _monitor.EndTime = DateTime.Now;
            _monitor.NextInvokeTime = _nextInvokedDateTime;
            if (_monitor.Status != (int)EnumScheduleStatus.Succeed)
            {
                _monitor.FailTimes++;
                _monitor.LastFailTime = _monitor.EndTime;
            }
            else
            {
                _monitor.SuccessTimes++;
            }
            if (_isIntensive)
            {
                if (_monitor.SuccessTimes % 10 != 0)
                    return;
            }
            SystemlogMgr.ScheduleFinish(_monitor, returnCode);
        }
    }
}
