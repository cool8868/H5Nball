using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.DynamicProxy;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Games.NBall.Common;


namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// 服务注册类
    /// </summary>
    public class ServiceInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().BasedOn<IInterceptor>()
                );
            container.AddFacility<TypedFactoryFacility>();
            
            
            //container.Register(AllTypes.FromThisAssembly()
            //                    .Where(Component.IsInNamespace("The9.CSCenter.Service"))
            //                    .Where(t => t.Name.EndsWith("Provider") && Component.IsCastleComponent(t)));

            container.Register(AllTypes.FromThisAssembly()
                                .Where(Component.IsInNamespace("Games.NBall.ServiceEngine"))
                                .Where(t => t.Name.EndsWith("Provider") && Component.IsCastleComponent(t)));
            //container.Install(Configuration.FromXmlFile("component.config"));

            container.Register(Component.For<ILocalCacheProvider>()
                                .ImplementedBy<MemoryCacheProvider>()
                                );
        }

        #endregion
    }
}
