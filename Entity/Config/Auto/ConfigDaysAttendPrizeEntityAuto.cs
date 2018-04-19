
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_DaysAttendPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigDaysattendprizeEntity
	{
		
		public ConfigDaysattendprizeEntity()
		{
		}

		public ConfigDaysattendprizeEntity(
		System.Int32 idx
,				System.Int32 prizetype
,				System.Int32 prizeitemcode
,				System.Int32 count
,				System.Boolean hasdouble
,				System.Int32 doubleviplevel
,				System.Boolean isbinding
		)
		{
			this.Idx = idx;
			this.PrizeType = prizetype;
			this.PrizeItemCode = prizeitemcode;
			this.Count = count;
			this.HasDouble = hasdouble;
			this.DoubleVipLevel = doubleviplevel;
			this.IsBinding = isbinding;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///奖励类型 1点券 2物品
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///奖励物品
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeItemCode {get ; set ;}

		///<summary>
		///数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///是否有双倍奖励
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Boolean HasDouble {get ; set ;}

		///<summary>
		///双倍奖励Vip等级
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 DoubleVipLevel {get ; set ;}

		///<summary>
		///是否为绑定物品
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsBinding {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigDaysattendprizeEntity Clone()
        {
            ConfigDaysattendprizeEntity entity = new ConfigDaysattendprizeEntity();
			entity.Idx = this.Idx;
			entity.PrizeType = this.PrizeType;
			entity.PrizeItemCode = this.PrizeItemCode;
			entity.Count = this.Count;
			entity.HasDouble = this.HasDouble;
			entity.DoubleVipLevel = this.DoubleVipLevel;
			entity.IsBinding = this.IsBinding;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_DaysAttendPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigDaysattendprizeResponse : BaseResponse<ConfigDaysattendprizeEntity>
    {

    }
}

