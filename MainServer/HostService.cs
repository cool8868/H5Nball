using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Games.NBall.Common;
using Games.NBall.ServiceContract;
using log4net;

namespace MainServer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HostService : ServiceBase
    {
        static ILog logger = LogManager.GetLogger(typeof(HostService));

        /// <summary>
        /// 
        /// </summary>
        public HostService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            StartService(args);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
            StopService();
        }

        #region local
        /// <summary>
        /// 服务实例
        /// </summary>
        HostNBallService host;
        #endregion

        /// <summary>
        /// 开始服务
        /// </summary>
        /// <param name="args"></param>
        public void StartService(string[] args)
        {
            logger.Info("Starting service...");

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

        /// <summary>
        /// 停止服务
        /// </summary>
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
    }
}
