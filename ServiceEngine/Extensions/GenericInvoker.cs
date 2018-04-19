using System;
using System.ServiceModel.Dispatcher;
using System.Diagnostics;

namespace Games.NBall.ServiceEngine.Extensions
{
    /// <summary>
    /// GenericInvoker
    /// </summary>
    public abstract class GenericInvoker : IOperationInvoker
    {
        readonly IOperationInvoker m_InnerInvoker;

        public GenericInvoker(IOperationInvoker innerInvoker)
        {
            Debug.Assert(innerInvoker != null);

            m_InnerInvoker = innerInvoker;
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
        { }

        /// <summary>
        /// Always called, even if operation had an exception
        /// </summary>
        /// <returns></returns>
        protected virtual void PostInvoke(object instance, object returnedValue, object[] outputs, Exception exception)
        { }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            PreInvoke(instance, inputs);
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
                exception = operationException;
                throw;
            }
            finally
            {
                PostInvoke(instance, returnedValue, outputParams, exception);
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
}
