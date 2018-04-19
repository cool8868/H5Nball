using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom.Item;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    /// <summary>
    /// 装备的属性
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class EquipmentProperty : ItemProperty
    {
        /// <summary>
        /// 基础加成
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public List<PropertyPlusEntity> PropertyPluses { get; set; }

        /// <summary>
        /// 附加球星技能加成
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public string StarskillPlus { get; set; }

        /// <summary>
        /// 装备插槽列表
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public List<EquipmentSlotEntity> EquipmentSlots { get; set; }

        /// <summary>
        /// 装备进阶等级
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int Level { get; set; }

        /// <summary>
        /// 精铸属性
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public List<PrecisionCastingPropertyEntity> PrecisionCastingPropertis { get; set; }


        public  override ItemProperty Clone()
        {
            EquipmentProperty entity = new EquipmentProperty();
            if (this.PropertyPluses != null)
            {
                entity.PropertyPluses = new List<PropertyPlusEntity>(PropertyPluses.Count);
                foreach (var propertyPlusEntity in PropertyPluses)
                {
                    entity.PropertyPluses.Add(propertyPlusEntity.Clone());
                }
            }
            if (!string.IsNullOrEmpty(StarskillPlus))
            {
                entity.StarskillPlus = this.StarskillPlus;
            }
            if (this.EquipmentSlots != null)
            {
                entity.EquipmentSlots = new List<EquipmentSlotEntity>(EquipmentSlots.Count);
                foreach (var equipmentHoleEntity in EquipmentSlots)
                {
                    entity.EquipmentSlots.Add(equipmentHoleEntity.Clone());
                }
            }
            if (this.Level > 0)
            {
                entity.Level = Level;
            }

            if (this.PrecisionCastingPropertis != null)
            {
                entity.PrecisionCastingPropertis = new List<PrecisionCastingPropertyEntity>(PrecisionCastingPropertis.Count);
                foreach (var precisioncastingProerty in PrecisionCastingPropertis)
                {
                    entity.PrecisionCastingPropertis.Add(precisioncastingProerty);
                }
            }
            return entity;
        }
    }
}
