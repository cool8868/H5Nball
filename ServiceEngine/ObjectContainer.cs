using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using log4net;
using Games.NBall.Common;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// 对象容器
    /// </summary>
    public class ObjectContainer : IDisposable
    {
        private static ILog logger = LogManager.GetLogger(typeof(ObjectContainer));

        private ObjectContainer()
        {
            Setup();
        }

        private static object syncLock = new object();
        private static Lazy<ObjectContainer> instance = new Lazy<ObjectContainer>(() => new ObjectContainer());
        private IWindsorContainer container;

        public IWindsorContainer Container
        {
            get { return container; }
        }

        /// <summary>
        /// 容器实例
        /// </summary>
        public static ObjectContainer Instance
        {
            get { return instance.Value; }
        }

        private void Setup()
        {
            container = BootstrapContainer();
        }

        /// <summary>
        /// 注册服务类型
        /// </summary>
        /// <returns></returns>
        private IWindsorContainer BootstrapContainer()
        {
            try
            {
                return new WindsorContainer()
                      .Install(
                    //Configuration.FromAppConfig(),
                    FromAssembly.This()
                    //FromAssembly.Named("The9.CSCenter.Service"),
                    //FromAssembly.InDirectory(new AssemblyFilter("Extensions"))
                      );
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "ObjectContainer.BootstrapContainer", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>() where T : class
        {
            T obj = default(T);

            try
            {
                obj = container.GetService<T>();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(logger, "ObjectContainer.GetService", ex);
            }

            if (obj == null)
            {
                LogHelper.LogError(logger, "ObjectContainer.GetService", "can not create instance", typeof(T).ToString());
            }

            return obj;
        }


        #region Dispose
        public void Dispose()
        {
            lock (syncLock)
            {
                if (container == null)
                {
                    return;
                }
                container.Dispose();
                container = null;
            }
        }

        #endregion
    }
}
