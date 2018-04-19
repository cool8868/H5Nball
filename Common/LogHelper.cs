using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace Games.NBall.Common
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public static class LogHelper
    {

        /// <summary>
        /// Insert the log.
        /// </summary>
        /// <param name="exception">Represents the system exception.</param>
        public static void Insert(Exception exception)
        {
            Insert(exception, String.Empty, LogType.Error);
        }

        /// <summary>
        /// Insert the log.
        /// </summary>
        /// <param name="exception">Represents the system exception.</param>
        /// <param name="message">Represents the information.</param>
        public static void Insert(Exception exception, string message)
        {
            Insert(exception, message, LogType.Error);
        }

        /// <summary>
        /// Insert the log.
        /// </summary>
        /// <param name="exception">Represents the system exception.</param>
        /// <param name="message">Represents the information.</param>
        /// <param name="logType">Represents the log's type.</param>
        public static void Insert(Exception exception, string message, LogType logType)
        {
            InternalInsert(exception, message, logType);
        }

        /// <summary>
        /// Insert the log.
        /// </summary>
        /// <param name="message">Represents the information.</param>
        [Conditional("DEBUG")]
        public static void Insert(string message)
        {
            Debug.WriteLine(message);
            InternalInsert(null, message, LogType.Debug);
        }

        /// <summary>
        /// Insert the log.
        /// </summary>
        /// <param name="message">Represents the information.</param>
        /// <param name="logType">Represents the log's type.</param>
        public static void Insert(string message, LogType logType)
        {
            InternalInsert(null, message, logType);
        }

        #region encapsulation

        static LogHelper()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Insert the parameters to log.
        /// </summary>
        /// <param name="exception">Represents the exception.</param>
        /// <param name="message">Represents the information.</param>
        /// <param name="logType">Represents the log type.</param>
        private static void InternalInsert(Exception exception, string message, LogType logType)
        {

#if DEBUG
            Debug.WriteLine(message);
            Debug.WriteLine(exception);
#endif
            var logger = LogManager.GetLogger("root");
            if (logType == LogType.Debug)
            {
                logger.Debug(message, exception);
                return;
            }

            if (logType == LogType.Info)
            {
                logger.Info(message, exception);
                return;
            }

            if (logType == LogType.Error)
            {
                if (exception == null)
                {
                    logger.Error(message);
                }
                else
                {
                    logger.Error(message, exception);
                }
                return;
            }
        }

        /// <summary>
        /// Build the log information string.
        /// </summary>
        /// <param name="exception">Represents the exception.</param>
        /// <param name="message">Represents the message.</param>
        /// <param name="logType">Represents the log type.</param>
        /// <returns>The log information which composed with all the parameters.</returns>
        private static string BuildLogInformation(Exception exception, string message, LogType logType)
        {
            return String.Format("Time:{0}, Info:{1}, Type:{2}, Exception:{3}, Stack:{4}", DateTime.Now, message, logType, exception, (exception != null) ? exception.StackTrace : string.Empty);
        }

        #endregion

        #region public methods
        /// <summary>
        /// 记录Exception信息
        /// </summary>
        /// <param name="log"></param>
        /// <param name="function"></param>
        /// <param name="e"></param>
        /// <param name="args"></param>
        public static void LogException(ILog log, string function, Exception e, params object[] args)
        {
            if (!log.IsErrorEnabled)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("error at: {0}; exception: {1};", function, e.ToString());
            Append(sb, args);
            log.Error(sb.ToString());
        }

        /// <summary>
        /// 记录Error级信息
        /// </summary>
        /// <param name="function"></param>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogError(ILog log, string function, string message, params object[] args)
        {
            if (!log.IsErrorEnabled)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("error at: {0}; error: {1};", function, message);
            Append(sb, args);
            log.Error(sb.ToString());
        }

        private static void Append(StringBuilder sb, params object[] args)
        {
            if (args != null)
            {
                foreach (object arg in args)
                {
                    sb.Append(arg);
                    sb.Append(";");
                }
            }
        }

        /// <summary>
        /// 记录Debug级信息
        /// </summary>
        /// <param name="function"></param>
        /// <param name="log"></param>
        /// <param name="args"></param>
        public static void LogDebug(ILog log, string function, params object[] args)
        {
            if (!log.IsDebugEnabled)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("debug at: {0}; ", function);
            Append(sb, args);
            log.Debug(sb.ToString());
        }

        /// <summary>
        /// 记录Info级信息
        /// </summary>
        /// <param name="log"></param>
        /// <param name="function"></param>
        /// <param name="args"></param>
        public static void LogInfo(ILog log, string function, params object[] args)
        {
            if (!log.IsInfoEnabled)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("debug at: {0}; ", function);
            Append(sb, args);
            log.Info(sb.ToString());
        }
        #endregion
    }

    /// <summary>
    /// Represents the Log's type
    /// </summary>
    public enum LogType
    {

        /// <summary>
        /// Debug
        /// </summary>
        Debug,

        /// <summary>
        /// Info
        /// </summary>
        Info,

        /// <summary>
        /// Error
        /// </summary>
        Error
    }
}
