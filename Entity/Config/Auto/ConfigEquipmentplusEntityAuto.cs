
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_EquipmentPlus 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigEquipmentplusEntity
	{
		
		public ConfigEquipmentplusEntity()
		{
		}

		public ConfigEquipmentplusEntity(
		System.Int32 idx
,				System.Int32 quality
,				System.Int32 plusvaluemin
,				System.Int32 plusvaluemax
,				System.Int32 plusratemin
,				System.Int32 plusratemax
,				System.Int32 slotmin
,				System.Int32 slotmax
,				System.Int32 washmallcode
,				System.Int32 lockmallcode
,				System.Int32 starskillplusrate
		)
		{
			this.Idx = idx;
			this.Quality = quality;
			this.PlusValueMin = plusvaluemin;
			this.PlusValueMax = plusvaluemax;
			this.PlusRateMin = plusratemin;
			this.PlusRateMax = plusratemax;
			this.SlotMin = slotmin;
			this.SlotMax = slotmax;
			this.WashMallCode = washmallcode;
			this.LockMallCode = lockmallcode;
			this.StarSkillPlusRate = starskillplusrate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///品质
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Quality {get ; set ;}

		///<summary>
		///加成值min
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PlusValueMin {get ; set ;}

		///<summary>
		///加成值max
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PlusValueMax {get ; set ;}

		///<summary>
		///加成百分比min
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PlusRateMin {get ; set ;}

		///<summary>
		///加成百分比max
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PlusRateMax {get ; set ;}

		///<summary>
		///插槽数量min
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 SlotMin {get ; set ;}

		///<summary>
		///插槽数量max
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 SlotMax {get ; set ;}

		///<summary>
		///WashMallCode
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 WashMallCode {get ; set ;}

		///<summary>
		///LockMallCode
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 LockMallCode {get ; set ;}

		///<summary>
		///附加球星技能概率0-10000
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 StarSkillPlusRate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigEquipmentplusEntity Clone()
        {
            ConfigEquipmentplusEntity entity = new ConfigEquipmentplusEntity();
			entity.Idx = this.Idx;
			entity.Quality = this.Quality;
			entity.PlusValueMin = this.PlusValueMin;
			entity.PlusValueMax = this.PlusValueMax;
			entity.PlusRateMin = this.PlusRateMin;
			entity.PlusRateMax = this.PlusRateMax;
			entity.SlotMin = this.SlotMin;
			entity.SlotMax = this.SlotMax;
			entity.WashMallCode = this.WashMallCode;
			entity.LockMallCode = this.LockMallCode;
			entity.StarSkillPlusRate = this.StarSkillPlusRate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_EquipmentPlus 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigEquipmentplusResponse : BaseResponse<ConfigEquipmentplusEntity>
    {

    }
}

