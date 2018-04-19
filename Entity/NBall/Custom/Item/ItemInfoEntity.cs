using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Newtonsoft.Json;
using ProtoBuf;

namespace Games.NBall.Entity
{

    [DataContract]
    [Serializable]
    [ProtoContract]
    [KnownType(typeof(EquipmentProperty))]
    [KnownType(typeof(PlayerCardProperty))]
    [KnownType(typeof(BallSoulProperty))]
    [KnownType(typeof(MallItemProperty))]
    [KnownType(typeof(PropertyPlusEntity))]
    [KnownType(typeof(StarskillPlusEntity))]
    [KnownType(typeof(PrecisionCastingPropertyEntity))]
    public class ItemInfoEntity
    {
        public ItemInfoEntity()
        {
        }

        public ItemInfoEntity(Guid itemId, int itemCode, int itemType)
        {
            this.ItemType = (int)itemType;
            this.ItemCount = 1;
            this.ItemCode = itemCode;
            this.ItemId = itemId;
            this.Status = 0;
        }

        public ItemInfoEntity(Guid itemId, int itemCode, int itemType, bool isDeal)
        {
            this.ItemType = (int)itemType;
            this.ItemCount = 1;
            this.ItemCode = itemCode;
            this.ItemId = itemId;
            this.Status = 0;
            this.IsDeal = isDeal;
        }

        public ItemInfoEntity(Guid itemId, int itemCode, EnumItemType itemType)
        :this(itemId,itemCode,(int)itemType)
        {
            
        }
        /// <summary>
        /// 获取球员卡强化等级
        /// </summary>
        /// <returns></returns>
        public int GetStrength()
        {
            if (ItemType == (int)EnumItemType.PlayerCard)
            {
                var property = ItemProperty as PlayerCardProperty;
                if (property != null)
                    return property.Strength;
            }
            else
            {
                return 0;
            }
            return 0;
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

        /// <summary>
        /// 是否可交易
        /// </summary>
        [DataMember]
        [ProtoMember(12)]
        public bool IsDeal { get; set; }

        #endregion

        public int SubType { get; set; }

        ///// <summary>
        ///// 是否是更新数量了，主要是针对商城道具添加
        ///// </summary>
        //public bool IsUpdateCount { get; set; }

        #region Clone
        public ItemInfoEntity Clone()
        {
            ItemInfoEntity entity = new ItemInfoEntity();
            entity.ItemId = this.ItemId;
            entity.ItemCode = this.ItemCode;
            entity.ItemType = this.ItemType;
            entity.ItemCount = this.ItemCount;
            entity.IsBinding = this.IsBinding;
            entity.IsDeal = this.IsDeal;
            entity.ItemProperty = this.ItemProperty.Clone();
            entity.GridIndex = this.GridIndex;
            entity.Status = this.Status;
            return entity;
        }
        #endregion

       

        /// <summary>
        /// 更新强化等级
        /// </summary>
        public void UpdateStrength(int strength)
        {
            if (ItemType == (int) EnumItemType.PlayerCard)
            {
                var property = ItemProperty as PlayerCardProperty;
                if (property != null)
                {
                    property.Strength=strength;
                }
            }
        }

        /// <summary>
        /// 强化等级+1，返回新等级
        /// </summary>
        /// <returns></returns>
        public int IncreaseStrength()
        {
            if (ItemType == (int)EnumItemType.PlayerCard)
            {
                var property = ItemProperty as PlayerCardProperty;
                if (property != null)
                {
                    property.Strength++;
                    return property.Strength;
                }
            }
            return 0;
        }

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
