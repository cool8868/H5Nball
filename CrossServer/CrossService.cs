using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.ServiceContract;
using log4net;

namespace Games.NBall.CrossServer
{
    public partial class CrossService : ServiceBase
    {
        public CrossService()
        {
            InitializeComponent();
        }

        #region EventHandler
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
        #endregion

        protected override void OnStart(string[] args)
        {
            StartService(args);
        }

        protected override void OnStop()
        {
            StopService();
        }

        #region
        static ILog logger = LogManager.GetLogger(typeof(CrossService));
        HostNBallService host;
        public void StartService(string[] args)
        {
            logger.Info("Starting service...");
            AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            AppDomain.CurrentDomain.ProcessExit -= new EventHandler(CurrentDomain_ProcessExit);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            logger.Info("Bootstrapper...");
            try
            {
                new HostBootstrapper().Startup();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                throw;
            }

            logger.Info("Starting HostService...");
            try
            {
                host = new HostNBallService();
                host.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                throw;
            }
            logger.Info("HostService is started.");
        }
        public void StopService()
        {
            logger.Info("Stopping service...");

            logger.Info("Finallizer...");
            new HostFinallizer().Terminate();

            logger.Info("stopping HostService...");

            if (host != null)
            {
                host.Stop();
            }

            logger.Info("HostService is stopped.");
        }
        #endregion
    }
}
