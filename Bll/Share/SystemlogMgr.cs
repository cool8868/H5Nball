using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Schedule;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Bll.Share
{
    public class SystemlogMgr
    {
        private static string _logSwitch = ConfigurationManager.AppSettings["LogSwitch"];
        private static NBThreadPool _threadPool = new NBThreadPool(10);
        private static string _terminalIP = ShareUtil.GetServerIp();


        /// <summary>
        /// Errors the specified function name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="msg">The MSG.</param>
        public static void Error(string functionName, string msg)
        {
            if (_logSwitch != "1")
            {
                LogHelper.Insert(functionName + msg, LogType.Error);
                return;
            }
            Error(functionName, msg, "");
        }

        public static void ErrorByZone(string functionName, string msg, string zoneId)
        {
            if (_logSwitch != "1")
            {
                LogHelper.Insert(functionName + msg, LogType.Error);
                return;
            }
            ErrorByZone(functionName, msg, "", zoneId);
        }

        /// <summary>
        /// 记录错误日志信息
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        public static void Error(string functionName, Exception e)
        {
            if (_logSwitch != "1")
            {
                LogHelper.Insert(e, functionName);
                return;
            }
            Error(functionName, e.Message, e.StackTrace);
        }
        /// <summary>
        /// 记录错误日志信息
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        public static void ErrorByZone(string functionName, Exception e, string zoneId = "")
        {
            if (_logSwitch != "1")
            {
                LogHelper.Insert(e, functionName);
                return;
            }
            ErrorByZone(functionName, e.Message, e.StackTrace, zoneId);
        }
        /// <summary>
        /// 记录错误日志信息
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        public static void Error(string functionName, string msg, Exception e)
        {
            if (_logSwitch != "1")
            {
                LogHelper.Insert(e, functionName);
                return;
            }
            msg = string.Format("({0}){1}", msg, e.Message ?? "");
            Error(functionName, msg, e.StackTrace);
        }

        /// <summary>
        /// 记录错误日志信息
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        public static void ErrorByZone(string functionName, string msg, Exception e, string zoneId)
        {
            if (_logSwitch != "1")
            {
                LogHelper.Insert(e, functionName);
                return;
            }
            msg = string.Format("({0}){1}", msg, e.Message ?? "");
            ErrorByZone(functionName, msg, e.StackTrace, zoneId);
        }

        /// <summary>
        /// Errors the specified function name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        public static void Error(string functionName, string errorMessage, string stackTrace)
        {
            if (_logSwitch != "1")
                return;
            _threadPool.Add(() => SaveError(functionName, errorMessage, stackTrace, ""));

        }
        /// <summary>
        /// Errors the specified function name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        public static void ErrorByZone(string functionName, string errorMessage, string stackTrace, string zoneId)
        {
            if (_logSwitch != "1")
                return;
            _threadPool.Add(() => SaveError(functionName, errorMessage, stackTrace, zoneId));

        }
        static void SaveError(string functionName, string errorMessage, string stackTrace, string zoneId)
        {
            try
            {
                var systemerrorlogEntity = new LogErrorEntity()
                {
                    AppId = ShareUtil.AppId,
                    TerminalIP = _terminalIP,
                    FunctionId = FunctionAppCache.Instance.GetFunctionId(functionName),
                    ModuleId = 1,
                    Message = errorMessage,
                    StackTrace = stackTrace ?? "",
                    RowTime = DateTime.Now
                };
                LogErrorMgr.Insert(systemerrorlogEntity, null, zoneId);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }

        /// <summary>
        /// 记录信息.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="message">The message.</param>
        public static void Info(string functionName, string message, string zoneId = "")
        {
            if (_logSwitch != "1")
                return;
            _threadPool.Add(() => SaveInfo(functionName, message, zoneId));
        }

        static void SaveInfo(string functionName, string message, string zoneId = "")
        {
            try
            {
                var systeminfologEntity = new LogInfoEntity()
                {
                    AppId = ShareUtil.AppId,
                    TerminalIP = _terminalIP,
                    FunctionId = FunctionAppCache.Instance.GetFunctionId(functionName),
                    ModuleId = 1,
                    Message = message,
                    RowTime = DateTime.Now
                };
                LogInfoMgr.Insert(systeminfologEntity, null, zoneId);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }

        public static MonitorScheduleEntity GetScheduleMonitor(int scheduleId)
        {
            var entity = MonitorScheduleMgr.GetByZone(ShareUtil.ZoneId, scheduleId, ShareUtil.AppId, _terminalIP);
            if (entity == null)
                return null;
            if (entity.AppId != ShareUtil.AppId || entity.TerminalIp != _terminalIP)
            {
                entity.AppId = ShareUtil.AppId;
                entity.TerminalIp = _terminalIP;
                MonitorScheduleMgr.UpdateApp(entity.Idx, ShareUtil.AppId, _terminalIP);
            }
            return entity;
        }

        static void doSaveScheduleLog(LogScheduleEntity log)
        {
            try
            {
                LogScheduleMgr.Insert(log);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }

        public static void ScheduleStart(int recordId, int status, DateTime startTime)
        {
            try
            {
                MonitorScheduleMgr.Start(recordId, status, startTime);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }

        public static void ScheduleFinish(MonitorScheduleEntity schedule, int returnCode)
        {
            try
            {
                MonitorScheduleMgr.Finish(schedule.Idx, schedule.Status, schedule.NextInvokeTime, schedule.EndTime, schedule.LastFailTime, schedule.SuccessTimes, schedule.FailTimes, schedule.RetryTimes);
                if (schedule.ScheduleTimeType == (int)EnumScheduleTimeType.TimeTable || schedule.Status != (int)EnumScheduleStatus.Succeed)
                {
                    doSaveScheduleLog(new LogScheduleEntity()
                    {
                        AppId = schedule.AppId,
                        EndTime = schedule.EndTime,
                        NextInvokeTime = schedule.NextInvokeTime,
                        RetryTimes = schedule.RetryTimes,
                        ReturnCode = returnCode,
                        ScheduleId = schedule.ScheduleId,
                        StartTime = schedule.StartTime,
                        Status = schedule.Status,
                        TerminalIp = schedule.TerminalIp
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }
    }
}
