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
    /// 使用中的球员卡
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract]
    [KnownType(typeof(PlayerCardProperty))]
    public class PlayerCardUsedEntity : ItemUsedEntity
    {
        public PlayerCardUsedEntity()
        {
            
        }

        public PlayerCardUsedEntity(ItemInfoEntity item)
        {
            this.ItemId = item.ItemId;
            this.ItemCode = item.ItemCode;
            this.IsBinding = item.IsBinding;
            this.IsDeal = item.IsDeal;
            this.Property = item.ItemProperty.Clone() as PlayerCardProperty;
        }

        /// <summary>
        /// 球员卡属性
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public PlayerCardProperty Property { get; set; }
    }
}
