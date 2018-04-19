using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom.Item
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class PrecisionCastingPropertyEntity
    {
        public PrecisionCastingPropertyEntity() 
        {
            
        }

        public PrecisionCastingPropertyEntity(int propertyId, double plusValue, int propertyQuality)
        {
            this.PropertyId = propertyId;
            this.PlusValue = plusValue;
            this.PropertyQuality = propertyQuality;
        }

        /// <summary>
        /// 精铸属性id
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int PropertyId { get; set; }

        /// <summary>
        /// 加成值 
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public double PlusValue { get; set; }

        /// <summary>
        /// 属性品质，绿 蓝 紫 橙
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public int PropertyQuality { get; set; }

        public PrecisionCastingPropertyEntity Clone()
        {
            PrecisionCastingPropertyEntity entity = new PrecisionCastingPropertyEntity();
            entity.PropertyId = this.PropertyId;
            entity.PlusValue = this.PlusValue;
            entity.PropertyQuality = this.PropertyQuality;
            return entity;
        }
    }
}
