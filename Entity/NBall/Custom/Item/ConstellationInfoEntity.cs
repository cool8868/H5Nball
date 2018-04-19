using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Newtonsoft.Json;
using ProtoBuf;
using System.Runtime.Serialization;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    [KnownType(typeof(MallItemProperty))]
    public class ConstellationInfoEntity
    {
          public ConstellationInfoEntity()
        {
        }

        public ConstellationInfoEntity(Guid itemId, int itemCode, int itemType)
        {
            this.ItemType = (int)itemType;
            this.ItemCount = 1;
            this.ItemCode = itemCode;
            this.ItemId = itemId;
            this.Status = 0;
        }

        public ConstellationInfoEntity(Guid itemId, int itemCode, EnumItemType itemType)
        :this(itemId,itemCode,(int)itemType)
        {
            
        }

        #region Public Properties

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
        ///堆叠数量
        ///</summary>
        [DataMember]
        [ProtoMember(3)]
        public System.Int32 ItemCount { get; set; }

        /// <summary>
        /// 物品类型
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int ItemType { get; set; }

        ///<summary>
        ///是否绑定
        ///</summary>
        [DataMember]
        [ProtoMember(5)]
        public System.Boolean IsBinding { get; set; }

        /// <summary>
        /// 物品属性
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public ItemProperty ItemProperty { get; set; }

        ///<summary>
        ///所属格数
        ///</summary>
        [DataMember]
        [ProtoMember(10)]
        public System.Int32 GridIndex { get; set; }

        ///<summary>
        ///锁定状态
        ///</summary>
        [DataMember]
        [ProtoMember(11)]
        public System.Int32 Status { get; set; }

        #endregion

        public int SubType { get; set; }

        ///// <summary>
        ///// 是否是更新数量了，主要是针对商城道具添加
        ///// </summary>
        //public bool IsUpdateCount { get; set; }

        #region Clone
        public ConstellationInfoEntity Clone()
        {
            ConstellationInfoEntity entity = new ConstellationInfoEntity();
            entity.ItemId = this.ItemId;
            entity.ItemCode = this.ItemCode;
            entity.ItemType = this.ItemType;
            entity.ItemCount = this.ItemCount;
            entity.IsBinding = this.IsBinding;

            entity.ItemProperty = this.ItemProperty.Clone();
            entity.GridIndex = this.GridIndex;
            entity.Status = this.Status;
            return entity;
        }
        #endregion

        /// <summary>
        /// 判断物品是否是包裹内的物品
        /// </summary>
        /// <param name="item">物品</param>
        /// <returns>相同返回true</returns>
        public bool Equals(ItemInfoEntity item)
        {
            return (this.GridIndex == item.GridIndex) && (this.ItemCode == item.ItemCode) && (this.ItemId == item.ItemId);
        }
    }
}
