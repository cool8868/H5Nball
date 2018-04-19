using System;
using System.ServiceModel;
using System.Collections.ObjectModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace Games.NBall.ServiceEngine.Extensions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ErrorHandlerBehavior : Attribute, IServiceBehavior
    {
        Type errorHandlerType;

        public ErrorHandlerBehavior(Type errorHandlerType)
        {
            this.errorHandlerType = errorHandlerType;
        }

        void IServiceBehavior.Validate(ServiceDescription description,ServiceHostBase host) 
        {
            //foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)
            //{
            //    if (endpoint.Contract.Name.Equals("IMetadataExchange") &&
            //        endpoint.Contract.Namespace.Equals("http://schemas.microsoft.com/2006/04/mex"))
            //        continue;

            //    foreach (OperationDescription description in endpoint.Contract.Operations)
            //    {
            //        if (description.Faults.Count == 0)
            //        {
            //            throw new InvalidOperationException("FaultContractAttribute not found on this method");
            //        }
            //    }
            //}
        }

        void IServiceBehavior.AddBindingParameters(ServiceDescription description,ServiceHostBase host,Collection<ServiceEndpoint> endpoints,BindingParameterCollection parameters)
        {}

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
           
            IErrorHandler errorHandler;

            try
            {
                errorHandler = (IErrorHandler)Activator.CreateInstance(errorHandlerType);
            }
            catch (MissingMethodException e)
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorHandlerBehaviorAttribute constructor must have a public empty constructor.", e);
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorHandlerBehaviorAttribute constructor must implement System.ServiceModel.Dispatcher.IErrorHandler.", e);
            }

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                if (channelDispatcher != null)
                {
                    channelDispatcher.ErrorHandlers.Add(errorHandler);
                }
            }
        }
    }
}
       


