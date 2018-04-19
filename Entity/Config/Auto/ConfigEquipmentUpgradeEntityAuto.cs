
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_EquipmentUpgrade 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigEquipmentupgradeEntity
	{
		
		public ConfigEquipmentupgradeEntity()
		{
		}

		public ConfigEquipmentupgradeEntity(
		System.Int32 idx
,				System.Int32 equipquality
,				System.Int32 sourcelevel
,				System.Int32 targetlevel
,				System.Int32 faillevel
,				System.Int32 protectedlevel
,				System.Int32 protectedconsume
,				System.Int32 propertynum
,				System.Int32 rate
,				System.Int32 coin
,				System.Int32 itemcode1
,				System.Int32 itemcount1
,				System.Int32 itemcode2
,				System.Int32 itemcount2
,				System.Int32 itemcode3
,				System.Int32 itemcount3
		)
		{
			this.Idx = idx;
			this.EquipQuality = equipquality;
			this.SourceLevel = sourcelevel;
			this.TargetLevel = targetlevel;
			this.FailLevel = faillevel;
			this.ProtectedLevel = protectedlevel;
			this.ProtectedConsume = protectedconsume;
			this.PropertyNum = propertynum;
			this.Rate = rate;
			this.Coin = coin;
			this.ItemCode1 = itemcode1;
			this.ItemCount1 = itemcount1;
			this.ItemCode2 = itemcode2;
			this.ItemCount2 = itemcount2;
			this.ItemCode3 = itemcode3;
			this.ItemCount3 = itemcount3;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///装备类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 EquipQuality {get ; set ;}

		///<summary>
		///SourceLevel
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SourceLevel {get ; set ;}

		///<summary>
		///TargetLevel
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 TargetLevel {get ; set ;}

		///<summary>
		///FailLevel
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 FailLevel {get ; set ;}

		///<summary>
		///ProtectedLevel
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ProtectedLevel {get ; set ;}

		///<summary>
		///ProtectedConsume
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 ProtectedConsume {get ; set ;}

		///<summary>
		///PropertyNum
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PropertyNum {get ; set ;}

		///<summary>
		///进阶概率
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Rate {get ; set ;}

		///<summary>
		///进阶消耗金币
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///ItemCode1
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 ItemCode1 {get ; set ;}

		///<summary>
		///ItemCount1
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 ItemCount1 {get ; set ;}

		///<summary>
		///ItemCode2
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 ItemCode2 {get ; set ;}

		///<summary>
		///ItemCount2
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 ItemCount2 {get ; set ;}

		///<summary>
		///ItemCode3
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 ItemCode3 {get ; set ;}

		///<summary>
		///ItemCount3
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 ItemCount3 {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigEquipmentupgradeEntity Clone()
        {
            ConfigEquipmentupgradeEntity entity = new ConfigEquipmentupgradeEntity();
			entity.Idx = this.Idx;
			entity.EquipQuality = this.EquipQuality;
			entity.SourceLevel = this.SourceLevel;
			entity.TargetLevel = this.TargetLevel;
			entity.FailLevel = this.FailLevel;
			entity.ProtectedLevel = this.ProtectedLevel;
			entity.ProtectedConsume = this.ProtectedConsume;
			entity.PropertyNum = this.PropertyNum;
			entity.Rate = this.Rate;
			entity.Coin = this.Coin;
			entity.ItemCode1 = this.ItemCode1;
			entity.ItemCount1 = this.ItemCount1;
			entity.ItemCode2 = this.ItemCode2;
			entity.ItemCount2 = this.ItemCount2;
			entity.ItemCode3 = this.ItemCode3;
			entity.ItemCount3 = this.ItemCount3;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_EquipmentUpgrade 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigEquipmentupgradeResponse : BaseResponse<ConfigEquipmentupgradeEntity>
    {

    }
}

