using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取对阵记录
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class RevelationMyHistoryGoals
    {
        /// <summary>
        /// 对阵记录
        /// </summary>
        [ProtoMember(1)]
        [DataMember]
        public Dictionary<int, Dictionary<int,MyHistoryGoalsEntity>> History { get; set; }

    }

    /// <summary>
    /// 对阵
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class MyHistoryGoalsEntity
    {
        /// <summary>
        /// 进球数
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int Goals { get; set; }
        /// <summary>
        /// 失球数
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public int ToConcede { get; set; }
    }
}
