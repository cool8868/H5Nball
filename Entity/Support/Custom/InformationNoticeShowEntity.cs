using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Support.Custom
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class InformationNoticeShowEntity
    {
        ///<summary>
        ///Idx
        ///</summary>
        [DataMember]
        [ProtoMember(1)]
        public System.Int32 Idx { get; set; }

        ///<summary>
        ///NoticeType
        ///</summary>
        [DataMember]
        [ProtoMember(2)]
        public System.Int32 NoticeType { get; set; }

        ///<summary>
        ///ContentString
        ///</summary>
        [DataMember]
        [ProtoMember(4)]
        public System.String ContentString { get; set; }

        /// <summary>
        /// 开始时间tick
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public long StartTimeTick { get; set; }

        /// <summary>
        /// 结束时间tick
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public long EndTimeTick { get; set; }

        /// <summary>
        /// 刷新频率
        /// </summary>
        [DataMember]
        [ProtoMember(7)]
        public int Frequency { get; set; }
    }
}
