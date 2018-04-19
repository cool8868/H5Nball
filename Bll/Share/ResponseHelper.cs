using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Match;
using log4net;
using System.Reflection;
using System.ServiceModel;
using System.Net.Sockets;
using Games.NBall.Entity.Enums;
using Games.NBall.Common;
using Games.NBall.ServiceEngine;
using System.Runtime.Caching;

namespace Games.NBall.Bll.Share
{
    /// <summary>
    /// Response辅助工具类
    /// </summary>
    public abstract class ResponseHelper
    {
        public static T CreateSuccess<T>() where T : IResponse, new()
        {
            T response = new T();
            response.Code = (int)MessageCode.Success;
            return response;
        }

        public static T Create<T>(MessageCode code) where T : IResponse, new()
        {
            T response = new T();
            response.Code = (int)code;
            return response;
        }

        public static T Create<T>(int code) where T : IResponse, new()
        {
            T response = new T();
            response.Code = code;
            return response;
        }

        public static T Create<T>(MessageCode code, string pr) where T : IResponse, new()
        {
            T response = new T();
            response.Code = (int)code;
            response.PR = pr;
            return response;
        }

        public static void Create<T>(T t, MessageCode code) where T : IResponse, new()
        {
            t.Code = (int)code;
        }

        public static ServiceResponse TrueServiceResonse()
        {
            ServiceResponse response = new ServiceResponse();
            response.Code = (int)MessageCode.Success;
            response.Data = true;
            return response;
        }

        public static MatchCreateResponse MatchCreateResponse(Guid matchId)
        {
            var response = new MatchCreateResponse();
            response.Code = (int)MessageCode.Success;
            response.Data = new MatchCreateEntity();
            response.Data.MatchId = matchId;
            return response;
        }

        public static T InvalidParameter<T>(string parameterName="") where T : IResponse, new()
        {
            var x= Create<T>(MessageCode.NbParameterError);
            x.PR = parameterName;
            return x;
        }

        public static T InvalidFunction<T>() where T : IResponse, new()
        {
            return Create<T>(MessageCode.NbFunctionNotOpen);
        }

        public static T Exception<T>() where T : IResponse, new()
        {
            return Create<T>(MessageCode.Exception);
        }

        private static T Ex<T>() 
        {
            dynamic t = (T)Activator.CreateInstance(typeof(T));
            t.Code = (int)MessageCode.Exception;
            return t;
        }

        public static T HandleTransactionError<T>(string functionName,int code,string errorMessage) where T : IResponse, new()
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                SystemlogMgr.Error(functionName, errorMessage);
            }
            return Create<T>(code);
        }

        public static void SaveTransactionError(string functionName,string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                SystemlogMgr.Error(functionName, errorMessage);
            }
        }

        #region
        public static RootResponse<T> CreateRoot<T>(int code)
        {
            var response = new RootResponse<T>();
            response.Code = code;
            return response;
        }
        public static RootResponse<T> CreateRoot<T>(MessageCode code)
        {
            var response = new RootResponse<T>();
            response.Code = (int)code;
            return response;
        }
        public static RootResponse<T> CreateRoot<T>(T data)
        {
            return CreateRoot<T>(MessageCode.Success, data);
        }
        public static RootResponse<T> CreateRoot<T>(MessageCode code, T data)
        {
            var response = new RootResponse<T>();
            response.Code = (int)code;
            response.Data = data;
            return response;
        }
        #endregion

        private static ILog logger = LogManager.GetLogger(typeof(ResponseHelper));

        /// <summary>
        /// 封装try catch
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="func">结果函数</param>
        /// <param name="args">参数值</param>
        /// <returns></returns>
        public static T TryCatch<T>(Func<T> func, params object[] args)
        {
            try
            {
                return func();
            }
            catch (TargetInvocationException tIEx)
            {
                LogEx(func.Method.Name, tIEx, args);
                return Ex<T>();
            }
            catch (EndpointNotFoundException eNFEx)
            {
                LogEx(func.Method.Name, eNFEx, args);
                return Ex<T>();
            }
            catch (SocketException sktEx)
            {
                LogEx(func.Method.Name, sktEx, args);
                return Ex<T>();
            }
            catch (NullReferenceException nullEx)
            {
                LogEx(func.Method.Name, nullEx, args);
                return Ex<T>();
            }
            catch (ArgumentOutOfRangeException aOutREx)
            {
                LogEx(func.Method.Name, aOutREx, args);
                return Ex<T>();
            }
            catch (ArgumentNullException anullEx)
            {
                LogEx(func.Method.Name, anullEx, args);
                return Ex<T>();
            }
            catch (NotImplementedException nIEx)
            {
                LogEx(func.Method.Name, nIEx, args);
                return Ex<T>();
            }
            catch (TimeoutException tmEx)
            {
                LogEx(func.Method.Name, tmEx, args);
                return Ex<T>();
            }
            catch (OutOfMemoryException oomEx)
            {
                LogEx(func.Method.Name, oomEx, args);
                return Ex<T>();
            }
            catch (Exception ex)
            {
                LogEx( func.Method.Name, ex, args);
                return Ex<T>();
            }
        }

        public static void TryCatch(Action func, params object[] args)
        {
            try
            {
                func();
            }
            catch (TargetInvocationException tIEx)
            {
                LogEx(func.Method.Name, tIEx, args);
            }
            catch (EndpointNotFoundException eNFEx)
            {
                LogEx(func.Method.Name, eNFEx, args);
            }
            catch (SocketException sktEx)
            {
                LogEx(func.Method.Name, sktEx, args);
            }
            catch (NullReferenceException nullEx)
            {
                LogEx(func.Method.Name, nullEx, args);
            }
            catch (ArgumentOutOfRangeException aOutREx)
            {
                LogEx(func.Method.Name, aOutREx, args);
            }
            catch (ArgumentNullException anullEx)
            {
                LogEx(func.Method.Name, anullEx, args);
            }
            catch (NotImplementedException nIEx)
            {
                LogEx(func.Method.Name, nIEx, args);
            }
            catch (TimeoutException tmEx)
            {
                LogEx(func.Method.Name, tmEx, args);
            }
            catch (OutOfMemoryException oomEx)
            {
                LogEx(func.Method.Name, oomEx, args);
            }
            catch (Exception ex)
            {
                LogEx(func.Method.Name, ex, args);
            }
        }

        public static void LogEx(string funcName, Exception ex, params object[] args)
        {
            try
            {
                LogHelper.LogException(logger,funcName, ex, args);

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Ex:[{0}],Func:[{1}]",ex.Message, funcName);
                if (args != null)
                {
                    sb.Append(",Args:[");
                    foreach (object arg in args)
                    {
                        sb.Append(arg);
                        sb.Append(";");
                    }
                    sb.Append("]");
                }
                SystemlogMgr.Error("Global-Service", sb.ToString(),ex.StackTrace);
                sb.Clear();
                sb = null;
            }
            catch (Exception ex1)
            {
                LogHelper.Insert(ex1);
            }
        }

        #region 写操作封装try catch，包含了写操作个人互斥功能
        private static ILocalCacheProvider localCache = ObjectContainer.Instance.GetService<ILocalCacheProvider>();
        private const int localExpiration = 120;//秒
        private const string MutexOperate = "ResponseHelper_Write_ManagerOperate_";

        /// <summary>
        /// 进入互斥操作
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static bool EnterMutexOperate(Guid managerId)
        {
            string key = string.Concat(MutexOperate, managerId);
            return localCache.Add(key, key, localExpiration);
        }

        /// <summary>
        /// 退出互斥操作
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static bool ExitMutexOperate(Guid managerId)
        {
            return localCache.Remove(string.Concat(MutexOperate, managerId));
        }

        /// <summary>
        /// 写操作封装try catch，包含了写操作个人互斥功能
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="managerId">互斥ID：经理ID</param>
        /// <param name="func">结果函数</param>
        /// <param name="args">参数值</param>
        /// <returns></returns>
        private static T TryWrite<T>(Guid managerId, Func<T> func, params object[] args) where T : IResponse, new()
        {
            bool isLocked = false;
            try
            {
                //是否有多线程同时操作
                isLocked = EnterMutexOperate(managerId);
                if (!isLocked)
                {
                    return Create<T>(MessageCode.Exception);
                }

                return func();
            }
            catch (TargetInvocationException tIEx)
            {
                LogEx(func.Method.Name, tIEx, args);
                return Ex<T>();
            }
            catch (EndpointNotFoundException eNFEx)
            {
                LogEx(func.Method.Name, eNFEx, args);
                return Ex<T>();
            }
            catch (SocketException sktEx)
            {
                LogEx(func.Method.Name, sktEx, args);
                return Ex<T>();
            }
            catch (NullReferenceException nullEx)
            {
                LogEx(func.Method.Name, nullEx, args);
                return Ex<T>();
            }
            catch (ArgumentOutOfRangeException aOutREx)
            {
                LogEx(func.Method.Name, aOutREx, args);
                return Ex<T>();
            }
            catch (ArgumentNullException anullEx)
            {
                LogEx(func.Method.Name, anullEx, args);
                return Ex<T>();
            }
            catch (NotImplementedException nIEx)
            {
                LogEx(func.Method.Name, nIEx, args);
                return Ex<T>();
            }
            catch (TimeoutException tmEx)
            {
                LogEx(func.Method.Name, tmEx, args);
                return Ex<T>();
            }
            catch (OutOfMemoryException oomEx)
            {
                LogEx(func.Method.Name, oomEx, args);
                return Ex<T>();
            }
            catch (Exception ex)
            {
                LogEx(func.Method.Name, ex, args);
                return Ex<T>();
            }
            finally
            {
                if (isLocked)
                {
                    try
                    {
                        ExitMutexOperate(managerId);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(logger, func.Method.Name, "退出互斥异常", ex);
                    }
                }
            }
        }

        #endregion
    }
}
