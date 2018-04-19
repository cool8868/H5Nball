using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom.ItemUsed
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    [ProtoInclude(30,typeof(PlayerCardUsedEntity))]
    [ProtoInclude(31, typeof(EquipmentUsedEntity))]
    [ProtoInclude(33, typeof(BallSoulUsedEntity))]
    public class ItemUsedEntity
    {
        ///<summary>
        ///ItemId
        ///</summary>
        [DataMember]
        [ProtoMember(1)]
        public System.Guid ItemId { get; set; }

        ///<summary>
        ///ItemCode
        ///</summary>
        [DataMember]
        [ProtoMember(2)]
        public System.Int32 ItemCode { get; set; }

        ///<summary>
        ///是否绑定
        ///</summary>
        [DataMember]
        [ProtoMember(3)]
        public System.Boolean IsBinding { get; set; }

        /// <summary>
        /// 是否可交易
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public bool IsDeal { get; set; }
    }
}
