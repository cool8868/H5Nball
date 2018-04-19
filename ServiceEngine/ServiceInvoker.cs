using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Collections.Concurrent;
using System.ServiceModel.Channels;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TChannel"></typeparam>
    public class ServiceInvoker<TChannel> where TChannel : class
    {
        private static object syncLock = new object();

        /// <summary>
        /// ChannelFactory cache
        /// </summary>
        private static ConcurrentDictionary<string, ChannelFactory<TChannel>> channelFactoryCache = new ConcurrentDictionary<string, ChannelFactory<TChannel>>(StringComparer.OrdinalIgnoreCase);

        private ChannelFactory<TChannel> CreateFactory(string endpointName)
        {
            //return channelFactoryCache.GetOrAdd(endpointName, x => new ChannelFactory<TChannel>(endpointName));
            ChannelFactory<TChannel> value;
            if (channelFactoryCache.TryGetValue(endpointName, out value))
            {
                return value;
            }
            lock (syncLock)
            {
                return channelFactoryCache.GetOrAdd(endpointName, x => CreateFactoryImpl(endpointName));
            }
        }


        private ChannelFactory<TChannel> CreateFactoryImpl(string endpointName)
        {
            if (string.IsNullOrWhiteSpace(endpointName))
            {
                return new ChannelFactory<TChannel>("*");
            }
            // remove 
            return new ChannelFactory<TChannel>("*");
        }


        /// <summary>
        /// Invoke service
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="endpointName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        private TResult InvokeImpl<TResult>(string endpointName, Func<TChannel, TResult> function)
        {
            ChannelFactory<TChannel> channelFactory = CreateFactory(endpointName);
            if (channelFactory == null)
            {
                throw new Exception("can not create channel factory:" + typeof(TChannel));
            }

            TChannel proxy = channelFactory.CreateChannel();
            ICommunicationObject channel = proxy as ICommunicationObject;
            if (null == channel)
            {
                throw new ArgumentException(string.Format("Invalid proxy:{0}-{1}", typeof(TChannel), endpointName));
            }

            try
            {
                channel.Open();
                return function(proxy);
            }
            catch (TimeoutException)
            {
                //(endpointProxy as ICommunicationObject).Abort();
                channel.Abort();
                throw;
            }
            catch (CommunicationException)
            {
                //(endpointProxy as ICommunicationObject).Abort();
                channel.Abort();
                throw;
            }
            finally
            {
                //(endpointProxy as ICommunicationObject).Close();
                channel.Close();
            }
        }
        
        /// <summary>
        /// 按照默认设定调用wcf服务
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public TResult Invoke<TResult>(Func<TChannel, TResult> function)
        {
            return InvokeImpl(string.Empty, function);
        }

        

        /// <summary>
        /// LookupEndpoint
        /// </summary>
        /// <param name="endpointName"></param>
        /// <returns></returns>
        private string LookupEndpoint(string endpointName)
        {
            if (string.IsNullOrWhiteSpace(endpointName))
            {
                return "*";
            }

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ClientSection clientSection = ServiceModelSectionGroup.GetSectionGroup(config).Client;
            ChannelEndpointElement endpointElement = null;
            foreach (ChannelEndpointElement element in clientSection.Endpoints)
            {
                if (element.Name.EndsWith("_" + endpointName, StringComparison.OrdinalIgnoreCase)
                    && element.Contract == typeof(TChannel).FullName)
                {
                    endpointElement = element;
                    break;
                }
            }
            if (endpointElement == null)
            {
                return "*";
            }
            return endpointElement.Name;
        }


    }
}
