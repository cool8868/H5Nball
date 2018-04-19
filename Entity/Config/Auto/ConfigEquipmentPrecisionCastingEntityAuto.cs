
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_EquipmentPrecisionCasting 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigEquipmentprecisioncastingEntity
	{
		
		public ConfigEquipmentprecisioncastingEntity()
		{
		}

		public ConfigEquipmentprecisioncastingEntity(
		System.Int32 idx
,				System.Int32 equipmentquality
,				System.Int32 propertyquality
,				System.Int32 propertytype
,				System.Int32 ratemin
,				System.Int32 ratemax
		)
		{
			this.Idx = idx;
			this.EquipmentQuality = equipmentquality;
			this.PropertyQuality = propertyquality;
			this.PropertyType = propertytype;
			this.RateMin = ratemin;
			this.RateMax = ratemax;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///EquipmentQuality
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 EquipmentQuality {get ; set ;}

		///<summary>
		///PropertyQuality
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PropertyQuality {get ; set ;}

		///<summary>
		///PropertyType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PropertyType {get ; set ;}

		///<summary>
		///RateMin
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 RateMin {get ; set ;}

		///<summary>
		///RateMax
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 RateMax {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigEquipmentprecisioncastingEntity Clone()
        {
            ConfigEquipmentprecisioncastingEntity entity = new ConfigEquipmentprecisioncastingEntity();
			entity.Idx = this.Idx;
			entity.EquipmentQuality = this.EquipmentQuality;
			entity.PropertyQuality = this.PropertyQuality;
			entity.PropertyType = this.PropertyType;
			entity.RateMin = this.RateMin;
			entity.RateMax = this.RateMax;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_EquipmentPrecisionCasting 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigEquipmentprecisioncastingResponse : BaseResponse<ConfigEquipmentprecisioncastingEntity>
    {

    }
}

