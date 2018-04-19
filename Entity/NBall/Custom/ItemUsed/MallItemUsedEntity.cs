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
    [KnownType(typeof(MallItemProperty))]
    public class MallItemUsedEntity : ItemUsedEntity
    {
        public MallItemUsedEntity()
        {

        }

        public MallItemUsedEntity(ItemInfoEntity item)
        {
            this.ItemId = item.ItemId;
            this.ItemCode = item.ItemCode;
            this.IsBinding = item.IsBinding;
            this.IsDeal = item.IsDeal;
            this.Property = item.ItemProperty.Clone() as MallItemProperty;
        }

        /// <summary>
        /// 属性
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public MallItemProperty Property { get; set; }
    }
}
