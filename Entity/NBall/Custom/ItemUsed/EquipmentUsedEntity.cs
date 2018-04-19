using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom.ItemUsed
{
    /// <summary>
    /// 使用中的装备
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract]
    [KnownType(typeof(EquipmentProperty))]
    public class EquipmentUsedEntity : ItemUsedEntity
    {
        public EquipmentUsedEntity()
        {
            
        }

        public EquipmentUsedEntity(ItemInfoEntity item)
        {
            this.ItemId = item.ItemId;
            this.ItemCode = item.ItemCode;
            this.IsBinding = item.IsBinding;
            this.IsDeal = item.IsDeal;
            this.Property = item.ItemProperty.Clone() as EquipmentProperty;
        }

        /// <summary>
        /// 装备属性
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public EquipmentProperty Property { get; set; }
    }
}
