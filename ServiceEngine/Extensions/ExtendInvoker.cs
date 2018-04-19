using Games.NBall.Common;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Games.NBall.ServiceEngine.Extensions
{
    /// <summary>
    /// ExtendInvoker
    /// </summary>
    public class ExtendInvoker : IOperationInvoker
    {
        readonly IOperationInvoker m_InnerInvoker;
        ServiceContextData serviceContext;
        static Type responseType = typeof(IResponse);
        static long timeSpanTimeout = Convert.ToInt64(TimeSpan.FromMinutes(60).TotalSeconds);

        #region private methods
        /// <summary>
        /// 拼装参数信息
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private string ArgumentsInfo(object[] arguments)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("arguments:");
            if (arguments != null)
            {
                foreach (object argument in arguments)
                {
                    sb.Append(argument);
                    sb.Append(";");
                }
            }
            return sb.ToString();
        }

        protected string ReadMessageHeader(string key)
        {
            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader(key, ServiceConfig.NameSpace);
            if (index < 0)
            {
                return string.Empty;
            }
            return OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
        }

        protected string ReadMessageProperty(string key)
        {
            object value;
            if (OperationContext.Current.IncomingMessageProperties.TryGetValue(key, out value))
            {
                return Convert.ToString(value);
            }
            return string.Empty;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="service"></param>
        /// <param name="action"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        private bool Authorize(ServiceContextData serviceContext, string appId)
        {
            return true;
        }

        /// <summary>
        /// GetRemoteAddress
        /// </summary>
        /// <returns></returns>
        private string GetRemoteAddress()
        {
            RemoteEndpointMessageProperty endpoint = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return endpoint.Address;
        }
        #endregion

        public ExtendInvoker(IOperationInvoker innerInvoker)
        {
            Debug.Assert(innerInvoker != null);

            m_InnerInvoker = innerInvoker;
        }

        private MethodInfo invokerMethodinfo = null;
        protected MethodInfo InvokerMethodInfo
        {
            get
            {
                if (invokerMethodinfo == null)
                {
                    PropertyInfo p = m_InnerInvoker.GetType().GetProperty("Method");
                    invokerMethodinfo = p.GetValue(m_InnerInvoker, null) as MethodInfo;
                }
                return invokerMethodinfo;
            }
        }

        public virtual object[] AllocateInputs()
        {
            return m_InnerInvoker.AllocateInputs();
        }
        /// <summary>
        /// Exceptions here will abort the call
        /// </summary>
        /// <returns></returns>
        protected virtual void PreInvoke(object instance, object[] inputs)
        {
            //WcfTracerData data = new WcfTracerData();
            //data.SetContextData();
            //data.Arguments = ArgumentsInfo(inputs);
            //data.AppendToLog();

        }

        /// <summary>
        /// Always called, even if operation had an exception
        /// </summary>
        /// <returns></returns>
        protected virtual void PostInvoke(object instance, object returnedValue, object[] outputs, Exception exception)
        {
            //WcfTracerData data = new WcfTracerData();
            //data.SetContextData();
            //data.Arguments = ArgumentsInfo(outputs);
            //if (exception != null)
            //{
            //    data.Exception = exception.ToString();
            //}
            //if (returnedValue != null)
            //{
            //    data.ReturnedValue = returnedValue.ToString();
            //}
            //data.AppendToLog();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            //SyncMethodInvoker methodType = m_InnerInvoker as SyncMethodInvoker;

            //trace args
            serviceContext = ServiceContextData.GetContextData();
            WcfTracerData preInvokerTracer = new WcfTracerData();
            preInvokerTracer.ContextData = serviceContext;
            preInvokerTracer.Arguments = ArgumentsInfo(inputs);
            preInvokerTracer.AppendToLog();

            //PreInvoke(instance, inputs);

            object returnedValue = null;
            object[] outputParams = new object[] { };
            Exception exception = null;

            try
            {
                returnedValue = m_InnerInvoker.Invoke(instance, inputs, out outputParams);
                outputs = outputParams;
                return returnedValue;
            }
            catch (Exception operationException)
            {
                outputs = outputParams;
                exception = operationException;

                if (InvokerMethodInfo.ReturnType.GetInterface(responseType.FullName, false) != null)
                {
                    IResponse response = Activator.CreateInstance(InvokerMethodInfo.ReturnType) as IResponse;
                    if (response != null)
                    {
                        response.Code = ResponseCode.Exception;
                    }
                    returnedValue = response;
                    outputs = outputParams;
                    return returnedValue;
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                PostInvoke(instance, returnedValue, outputParams, exception);

                //trace result
                WcfTracerData postInvokerTracer = new WcfTracerData();
                postInvokerTracer.ContextData = serviceContext;
                postInvokerTracer.Arguments = ArgumentsInfo(outputParams);
                if (exception != null)
                {
                    postInvokerTracer.Exception = exception.ToString();
                }
                if (returnedValue != null)
                {
                    postInvokerTracer.ReturnedValue = returnedValue.ToString();
                }

                //IResponse r = returnedValue as IResponse;
                //if (r != null)
                //{
                //    if (r.Code != ResponseCode.Ok)
                //    {
                //        Debug.Write(r.Code);
                //    }
                //}

                postInvokerTracer.AppendToLog();
            }
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            PreInvoke(instance, inputs);
            return m_InnerInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            object returnedValue = null;
            object[] outputParams = { };
            Exception exception = null;

            try
            {
                returnedValue = m_InnerInvoker.InvokeEnd(instance, out outputs, result);
                outputs = outputParams;
                return returnedValue;
            }
            catch (Exception operationException)
            {
                exception = operationException;
                throw;
            }
            finally
            {
                PostInvoke(instance, returnedValue, outputParams, exception);
            }
        }

        public bool IsSynchronous
        {
            get
            {
                return m_InnerInvoker.IsSynchronous;
            }
        }
    }

    public class ExtendInvokerAttribute : Attribute, IOperationBehavior
    {
        protected ExtendInvoker CreateInvoker(IOperationInvoker oldInvoker)
        {
            return new ExtendInvoker(oldInvoker);
        }

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

    public class ServiceExtendInvokerAttribute : Attribute, IServiceBehavior
    {
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

        protected ExtendInvokerAttribute CreateOperationInterceptor()
        {
            return new ExtendInvokerAttribute();
        }
    }

    public class ExtendInvokerElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ServiceExtendInvokerAttribute); }
        }

        protected override object CreateBehavior()
        {
            return new ServiceExtendInvokerAttribute();
        }
    }
}
