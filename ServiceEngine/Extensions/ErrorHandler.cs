using System;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using log4net;
using Games.NBall.Common;

namespace Games.NBall.ServiceEngine.Extensions
{
    public class ErrorHandler : IErrorHandler
    {
        ILog logger = LogManager.GetLogger(typeof(ErrorHandler));

        void IErrorHandler.ProvideFault(Exception error, MessageVersion version, ref Message msg)
        {
            //var f = new GameFault();
            //var fe = new FaultException<GameFault>(f);
            //var fault = fe.CreateMessageFault();
            //msg = Message.CreateMessage(version, fault, AppConfig.NameSpace);
        }

        bool IErrorHandler.HandleError(Exception error)
        {
            try
            {
                //StackFrame frame = new StackFrame(1);
                //Type serviceType = frame.GetMethod().ReflectedType;

                //string targetSite = string.Format("{0}:{1}", error.TargetSite.DeclaringType, error.TargetSite.Name);
                string method = ErrorHandlerHelper.GetServiceMethod(error);
                //string file = ErrorHandlerHelper.GetFileName(error);
                //int line = ErrorHandlerHelper.GetLineNumber(error);
                //string message = error.Message;
                //string detail = error.ToString();

                //if (error.Message.IndexOf("套接字连接已中止") >= 0 || error.Message.IndexOf("The socket connection was aborted") >= 0)
                //{
                //    return false;
                //}

                LogHelper.LogError(logger, "HandleError", 
                    string.Format("服务端异常,targetSite:{0}.{1}; method:{2}; message:{3}",
                    error.TargetSite.DeclaringType, error.TargetSite.Name, method,
                    error.ToString()),
                    error);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logger, "HandleError", "HandleError异常", ex);
            }

            // Returning true indicates you performed your behavior.
            return false;
        }
    }
}

