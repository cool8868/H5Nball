using System;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace Games.NBall.ServiceEngine.Extensions
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class OperationInterceptorBehaviorAttribute : Attribute, IOperationBehavior
    {
        protected abstract GenericInvoker CreateInvoker(IOperationInvoker oldInvoker);

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        { }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            IOperationInvoker oldInvoker = dispatchOperation.Invoker;
            dispatchOperation.Invoker = CreateInvoker(oldInvoker);
        }

        public void Validate(OperationDescription operationDescription)
        { }
    }
}
