using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
using System.Collections.Concurrent;
using System.Text;
using log4net;
using Games.NBall.Common;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceProxy<T> : RealProxy
    {
        private static ILog logger = LogManager.GetLogger(typeof(RealProxy));
        /// <summary>
        /// 
        /// </summary>
        private string endpointName;
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointName"></param>
        private ServiceProxy(string endpointName)
            :base(typeof(T))
        {            
            this.endpointName = endpointName;
        }

        /// <summary>
        /// 
        /// </summary>
        private ServiceProxy()
            :base(typeof(T))
        {                        
        }

        /// <summary>
        /// 
        /// </summary>
        private static ConcurrentDictionary<string, ChannelFactory<T>> channelFactoryCache = new ConcurrentDictionary<string, ChannelFactory<T>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ChannelFactory<T> CreateFactory()
        {
            string key = endpointName ?? string.Empty;
            //return channelFactoryCache.GetOrAdd(endpointName, x => new ChannelFactory<T>(endpointName));
            ChannelFactory<T> value;
            if(channelFactoryCache.TryGetValue(key, out value))
            {
                if (value.State != CommunicationState.Faulted)
                {
                    return value;
                }
            }
            lock (channelFactoryCache)
            {
                if (string.IsNullOrWhiteSpace(endpointName))
                {
                    value = channelFactoryCache.GetOrAdd(key, x => new ChannelFactory<T>("*"));
                }
                else
                {
                    value = channelFactoryCache.GetOrAdd(key, x => new ChannelFactory<T>(endpointName));
                }
            }
            return value;
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointName"></param>
        /// <returns></returns>
        public static T Create(string endpointName)
        {
            if (string.IsNullOrWhiteSpace(endpointName))
            {
                throw new ArgumentNullException("endpointName");
            }
            return (T)(new ServiceProxy<T>(endpointName).GetTransparentProxy());
        }   

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T Create()
        {
            return (T)(new ServiceProxy<T>().GetTransparentProxy());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            T channel = CreateFactory().CreateChannel();
            IMethodCallMessage methodCall = (IMethodCallMessage)msg;
            IMethodReturnMessage methodReturn = null;
            object[] copiedArgs = Array.CreateInstance(typeof(object), methodCall.Args.Length) as object[];
            methodCall.Args.CopyTo(copiedArgs, 0);
            try
            {
                object returnValue = methodCall.MethodBase.Invoke(channel, copiedArgs);
                methodReturn = new ReturnMessage(returnValue, copiedArgs, copiedArgs.Length, methodCall.LogicalCallContext, methodCall);                
            }            
            catch (Exception ex)
            {
                if (ex.InnerException is CommunicationException || ex.InnerException is TimeoutException)
                {
                    (channel as ICommunicationObject).Abort();
                }

                if (ex.InnerException != null)
                {
                    methodReturn = new ReturnMessage(ex.InnerException, methodCall);
                }
                else
                {
                    methodReturn = new ReturnMessage(ex, methodCall);
                }

                StringBuilder sbr=new StringBuilder();

                IDictionary proDic = msg.Properties;

                IDictionaryEnumerator proDicEnumerator = (IDictionaryEnumerator) proDic.GetEnumerator();
                int maxCount = 100;
                int num = 0;
                while (proDicEnumerator.MoveNext()&& num<maxCount)
                {
                    num++;
                    Object key = proDicEnumerator.Key;
                    String keyName = key.ToString();
                    Object keyValue = proDicEnumerator.Value;
                    sbr.AppendFormat(";{0},{1}", keyName, keyValue);
                }
                LogHelper.LogError(logger, "Invoke", sbr.ToString(), ex);

            }
            finally
            {
                var co=channel as ICommunicationObject;
                try
                {
                    co.Close();
                }
                catch (TimeoutException)
                {
                    co.Abort();
                }
                catch (FaultException)
                {
                    co.Abort();
                }
                catch (CommunicationException)
                {
                    co.Abort();
                }
                catch (Exception)
                {
                    co.Abort();
                }
            }

            return methodReturn;
        }
    }

}
