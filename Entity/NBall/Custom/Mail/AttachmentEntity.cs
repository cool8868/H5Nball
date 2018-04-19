using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    [KnownType(typeof(AttachmentItemEntity))]
    [KnownType(typeof(AttachmentUsedItemEntity))]
    [KnownType(typeof(ItemUsedEntity))]
    [KnownType(typeof(PlayerCardUsedEntity))]
    [KnownType(typeof(EquipmentUsedEntity))]
    //[KnownType(typeof(MallItemUsedEntity))]
    [KnownType(typeof(BallSoulUsedEntity))]
    [KnownType(typeof(AttachmentEquipmentEntity))]
    [KnownType(typeof(EquipmentProperty))]
    public class MailAttachmentEntity
    {
        public MailAttachmentEntity()
        {
            Attachments = new List<AttachmentEntity>();
        }

        /// <summary>
        /// 附件列表
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public List<AttachmentEntity> Attachments { get; set; }

        /// <summary>
        /// 添加附件，点券和游戏币
        /// </summary>
        /// <param name="attachmentType"></param>
        /// <param name="count"></param>
        public bool AddAttachment(EnumAttachmentType attachmentType, int count)
        {
            Attachments.Add(new AttachmentEntity() { AttachmentType = (int)attachmentType, Count = count });
            return true;
        }
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="count"></param>
        /// <param name="itemCode"></param>
        /// <param name="isBinding"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public bool AddAttachment(int count, int itemCode, bool isBinding, int strength)
        {
            Attachments.Add(new AttachmentItemEntity(EnumAttachmentType.NewItem, count, itemCode, isBinding, strength));
            return true;
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="count"></param>
        /// <param name="itemCode"></param>
        /// <param name="isBinding"></param>
        /// <param name="strength"></param>
        /// <param name="isDeal"></param>
        /// <returns></returns>
        public bool AddAttachment(int count, int itemCode, bool isBinding, int strength,bool isDeal)
        {
            var attachment = new AttachmentItemEntity(EnumAttachmentType.NewItem, count, itemCode, isBinding, strength);
            attachment.IsDeal = isDeal;
            Attachments.Add(attachment);
            return true;
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="count"></param>
        /// <param name="itemCode"></param>
        /// <param name="isBinding"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool AddAttachmentEquipment(int count, int itemCode, bool isBinding, EquipmentProperty property)
        {
            Attachments.Add(new AttachmentEquipmentEntity(EnumAttachmentType.Equipment, count, itemCode, isBinding, property));
            return true;
        }
        /// <summary>
        /// 添加附件，已使用的物品，如拍卖行里的
        /// </summary>
        /// <param name="attachmentType"></param>
        /// <param name="count"></param>
        /// <param name="itemInfoEntity"></param>
        public bool AddAttachmentUsedItem(EnumAttachmentType attachmentType, int count, ItemInfoEntity itemInfoEntity)
        {
            ItemUsedEntity usedEntity = null;
            switch (attachmentType)
            {
                case EnumAttachmentType.UsedPlayerCard:
                    usedEntity = new PlayerCardUsedEntity(itemInfoEntity);
                    break;
                case EnumAttachmentType.UsedEquipment:
                    usedEntity = new EquipmentUsedEntity(itemInfoEntity);
                    break;
            }
            if (usedEntity == null)
                return false;
            return AddAttachmentUsedItem(attachmentType, count, usedEntity);
        }
        /// <summary>
        /// 添加附件，已使用的物品，如拍卖行里的
        /// </summary>
        /// <param name="attachmentType"></param>
        /// <param name="count"></param>
        /// <param name="itemUsedEntity"></param>
        /// <returns></returns>
        public bool AddAttachmentUsedItem(EnumAttachmentType attachmentType, int count, ItemUsedEntity itemUsedEntity)
        {
            var attachment = new AttachmentUsedItemEntity(attachmentType, count);
            attachment.ItemProperty = itemUsedEntity;
            Attachments.Add(attachment);
            return true;
        }

    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    [ProtoInclude(20, typeof(AttachmentItemEntity))]
    [ProtoInclude(21, typeof(AttachmentUsedItemEntity))]
    [ProtoInclude(22, typeof(AttachmentEquipmentEntity))]
    public class AttachmentEntity
    {
        public AttachmentEntity()
        {

        }
        public AttachmentEntity(string[] properties)
        {
            AttachmentType = ConvertHelper.ConvertToInt(properties[0]);
            Count = ConvertHelper.ConvertToInt(properties[1]);
        }

        /// <summary>
        /// 附件类型
        /// </summary>
        [DataMember]
        [ProtoMember(11)]
        public int AttachmentType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ProtoMember(12)]
        public int Count { get; set; }

        [DataMember]
        [ProtoMember(13)]
        public bool IsDeal { get; set; }

    }


    [DataContract]
    [Serializable]
    [ProtoContract]
    public class AttachmentItemEntity : AttachmentEntity
    {
        public AttachmentItemEntity()
        {

        }

        public AttachmentItemEntity(EnumAttachmentType attachmentType, int count, int itemCode, bool isBinding, int strength)
        {
            AttachmentType = (int)attachmentType;
            Count = count;
            ItemCode = itemCode;
            IsBinding = isBinding;
            Strength = strength;
        }
        /// <summary>
        /// ItemCode
        /// </summary>
        [DataMember]
        [ProtoMember(13)]
        public int ItemCode { get; set; }
        /// <summary>
        /// IsBinding
        /// </summary>
        [DataMember]
        [ProtoMember(14)]
        public bool IsBinding { get; set; }
        /// <summary>
        /// Strength
        /// </summary>
        [DataMember]
        [ProtoMember(15)]
        public int Strength { get; set; }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class AttachmentUsedItemEntity : AttachmentEntity
    {
        public AttachmentUsedItemEntity()
        {

        }

        public AttachmentUsedItemEntity(EnumAttachmentType attachmentType, int count)
        {
            AttachmentType = (int)attachmentType;
            Count = count;
        }

        public AttachmentUsedItemEntity(int attachmentType, int count, ItemUsedEntity itemUsedEntity)
        {
            AttachmentType = attachmentType;
            Count = count;
            ItemProperty = itemUsedEntity;
        }
        /// <summary>
        /// 物品属性
        /// </summary>
        [DataMember]
        [ProtoMember(13)]
        public ItemUsedEntity ItemProperty { get; set; }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class AttachmentEquipmentEntity : AttachmentEntity
    {
        public AttachmentEquipmentEntity()
        {

        }

        public AttachmentEquipmentEntity(EnumAttachmentType attachmentType, int count, int itemCode, bool isBinding, EquipmentProperty equipmentProperty)
        {
            AttachmentType = (int)attachmentType;
            ItemCode = itemCode;
            IsBinding = isBinding;
            Count = count;
            EquipmentProperty = equipmentProperty;
        }
        /// <summary>
        /// 物品属性
        /// </summary>
        [DataMember]
        [ProtoMember(15)]
        public EquipmentProperty EquipmentProperty { get; set; }

        /// <summary>
        /// ItemCode
        /// </summary>
        [DataMember]
        [ProtoMember(13)]
        public int ItemCode { get; set; }
        /// <summary>
        /// IsBinding
        /// </summary>
        [DataMember]
        [ProtoMember(14)]
        public bool IsBinding { get; set; }
    }

}
