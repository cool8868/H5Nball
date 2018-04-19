using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// WCF Content数据
    /// </summary>
    public class ServiceContextData
    {
        public string SessionId { get; set; }
        public string Action { get; set; }
        public string EndpointAdress { get; set; }
        public string ContractName { get; set; }
        public string MessageId { get; set; }
        public string RemoteEndpointAddress { get; set; }

        public static ServiceContextData GetContextData()
        {
            ServiceContextData data = new ServiceContextData();
            OperationContext context = OperationContext.Current;

            RemoteEndpointMessageProperty endpoint = context == null ? null : context.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            data.RemoteEndpointAddress = endpoint == null ? string.Empty : endpoint.Address;
            data.Action = context == null ? string.Empty : context.IncomingMessageHeaders.Action;
            data.EndpointAdress = context == null ? string.Empty : context.EndpointDispatcher.EndpointAddress.ToString();
            data.ContractName = context == null ? string.Empty : context.EndpointDispatcher.ContractName;
            //data.SessionId = context == null ? string.Empty : context.SessionId;
            data.MessageId = context == null ? string.Empty : GetMessageId(context.IncomingMessageHeaders).ToString();
            //data.SessionId = context == null ? string.Empty : Guid.NewGuid().ToString();
            data.SessionId = Guid.NewGuid().ToString();
            return data;
        }

        private static Guid GetMessageId(MessageHeaders header)
        {
            if (header == null || header.MessageId == null)
            {
                return Guid.NewGuid();
            }
            Guid guid;
            if (header.MessageId.TryGetGuid(out guid))
            {
                return guid;
            }
            return Guid.NewGuid();
        }
    }

}
