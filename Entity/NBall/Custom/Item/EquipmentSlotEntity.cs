using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    /// <summary>
    /// 装备插槽
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class EquipmentSlotEntity
    {
        /// <summary>
        /// 插槽id
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public int SlotId { get; set; }

        /// <summary>
        /// 插槽颜色,EnumBallSoulColor
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int Color { get; set; }

        /// <summary>
        /// 球魂
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public BallSoulUsedEntity BallSoul { get; set; }

        public EquipmentSlotEntity Clone()
        {
            EquipmentSlotEntity entity = new EquipmentSlotEntity();
            entity.Color = this.Color;
            entity.SlotId = this.SlotId;
            if (BallSoul != null)
            {
                entity.BallSoul = BallSoul.Clone();
            }
            return entity;
        }
    }
}
