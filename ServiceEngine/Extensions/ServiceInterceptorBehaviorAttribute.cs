using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.Collections.ObjectModel;
using System.Linq;

namespace Games.NBall.ServiceEngine.Extensions
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class ServiceInterceptorBehaviorAttribute : Attribute, IServiceBehavior
    {
        protected abstract OperationInterceptorBehaviorAttribute CreateOperationInterceptor();

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase host)
        {
            foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)
            {
                if (endpoint.Contract.Name.Equals("IMetadataExchange"))
                {
                    continue;
                }

                foreach (OperationDescription operation in endpoint.Contract.Operations)
                {
                    if (operation.Behaviors.Find<OperationInterceptorBehaviorAttribute>() != null)
                    {
                        continue;
                    }
                    operation.Behaviors.Add(CreateOperationInterceptor());
                }
            }
        }
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { }
    }
}
