using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    /// <summary>
    /// 提升属性
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class PropertyPlusEntity
    {
        public PropertyPlusEntity()
        {
            
        }

        public PropertyPlusEntity(EnumPlusType plusType, int propertyId,int plusValue)
        {
            this.PlusType = (int)plusType;
            this.PropertyId = propertyId;
            this.PlusValue = plusValue;
        }

        /// <summary>
        /// 加成属性id
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int PropertyId { get; set; }
        /// <summary>
        /// 加成类型
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public int PlusType { get; set; }
        /// <summary>
        /// 加成值
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public double PlusValue { get; set; }

        public PropertyPlusEntity Clone()
        {
            PropertyPlusEntity entity = new PropertyPlusEntity();
            entity.PropertyId = this.PropertyId;
            entity.PlusType = this.PlusType;
            entity.PlusValue = this.PlusValue;
            return entity;
        }
    }
}
