using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    /// <summary>
    /// 使用中的球魂
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract]
    [KnownType(typeof(BallSoulProperty))]
    public class BallSoulUsedEntity : ItemUsedEntity
    {
        public BallSoulUsedEntity()
        {
            
        }

        public BallSoulUsedEntity(ItemInfoEntity item)
        {
            this.ItemId = item.ItemId;
            this.ItemCode = item.ItemCode;
            this.IsBinding = item.IsBinding;
            this.Property = item.ItemProperty.Clone() as BallSoulProperty;
        }

        /// <summary>
        /// 球魂属性
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public BallSoulProperty Property { get; set; }

        public BallSoulUsedEntity Clone()
        {
            BallSoulUsedEntity entity = new BallSoulUsedEntity();
            entity.ItemId = ItemId;
            entity.ItemCode = ItemCode;
            entity.IsBinding = IsBinding;
            if (Property != null)
                entity.Property = Property.Clone() as BallSoulProperty;
            return entity;
        }
    }
}
