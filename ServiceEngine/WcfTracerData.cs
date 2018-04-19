using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Games.NBall.ServiceEngine
{
    /// <summary>
    /// WCF调用信息
    /// </summary>
    public class WcfTracerData
    {
        public WcfTracerData()
        {
            this.Time = DateTime.Now;
        }
        public ServiceContextData ContextData { get; set; }
        public string Arguments { get; set; }
        public DateTime Time { get; set; }
        public string ReturnedValue { get; set; }
        public string Exception { get; set; }
    }

    public static class WcfTracerDataExtension
    {
        public static void AppendToLog(this WcfTracerData data)
        {
            //WcfTracerBufferSqlAppender.Instance.Append(data);
        }
    }
}
