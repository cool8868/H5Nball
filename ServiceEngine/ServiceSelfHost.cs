using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using log4net;

using Games.NBall.ServiceEngine.Extensions;
using System.ServiceModel.Channels;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// WCF服务host
    /// </summary>
    public class ServiceSelfHost 
    {
        private Dictionary<string, ServiceHost> serviceHosts = new Dictionary<string, ServiceHost>();
        private static ILog logger = LogManager.GetLogger(typeof(ServiceSelfHost));
        private static object lockSync = new object();
        private bool started = false;

        protected virtual IEnumerable<Type> GetHostServiceType()
        {
            var query = from type in Assembly.GetExecutingAssembly().GetTypes()
                        where type.GetInterfaces().Contains(typeof(IHostService))
                        select type;

            return query;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Stop()
        {
            StopService();
        }


        protected void StopService()
        {
            lock (lockSync)
            {
                if (started)
                {
                    logger.Info("service is stopping");
                    CloseAllHost();
                    //if (WcfTracerBufferSqlAppender.Enable)
                    //{
                    //    WcfTracerBufferSqlAppender.Instance.Flush();
                    //}

                    AppDomain.CurrentDomain.ProcessExit -= new EventHandler(CurrentDomain_ProcessExit);
                    AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    started = false;
                    logger.Info("service is stopped");
                }
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                logger.ErrorFormat("terminate:{0}; error:{1}", e.IsTerminating, ex.ToString());
            }
            else
            {
                logger.ErrorFormat("terminate:{0}; ", e.IsTerminating);
            }
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            StopService();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start()
        {
            System.Threading.ThreadPool.SetMinThreads(Environment.ProcessorCount * 2, Environment.ProcessorCount * 32);
            //System.Threading.ThreadPool.GetMinThreads(out a, out b);

            //tracerAppender = new WcfTracerBufferSqlAppender();
            //tracerAppender.ConnectionStringName = Constants.GMDbConnectionStringName;

            lock (lockSync)
            {
                if (!started)
                {
                    logger.Info("service is starting");

                    StartAllHost();

                    AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    started = true;
                    logger.Info("service is started");
                }
            }
        }

        private void SetupHost(ServiceHost host)
        {
            //DiscoveryHelper.EnableDiscovery(host, true);
            //host.AddDefaultEndpoints();
        }

        private void CloseAllHost()
        {
            foreach (ServiceHost host in serviceHosts.Values)
            {
                logger.InfoFormat("host {0} is closing", host.Description);
                host.Close();
            }
        }

        private void StartAllHost()
        {
            var types = GetHostServiceType();

            foreach (var type in types)
            {
                string name = type.FullName;
                Type implementationType = type;

                ServiceHost host = new ServiceHost(implementationType);
                SetupHost(host);
                host.Closed += new EventHandler(ServiceHost_Closed);
                host.Faulted += new EventHandler(ServiceHost_Faulted);

                host.Open();
                logger.InfoFormat("service {0} is opened.", name);
                serviceHosts.Add(name, host);

                string info = host.Status();
                logger.Info(info);
            }
        }

        private void ServiceHost_Closed(object sender, EventArgs e)
        {
            ServiceHost host = sender as ServiceHost;
            Debug.Assert(host != null, "host is null");

            logger.InfoFormat("host {0} closed", host.Description);
        }

        private void ServiceHost_Faulted(object sender, EventArgs e)
        {
            ServiceHost host = sender as ServiceHost;
            Debug.Assert(host != null, "host is null");

            logger.InfoFormat("host {0} Faulted", host.Description);
        }

    }
}
