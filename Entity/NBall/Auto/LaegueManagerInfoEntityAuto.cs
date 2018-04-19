
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Laegue_ManagerInfo 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LaegueManagerinfoEntity
	{
		
		public LaegueManagerinfoEntity()
		{
		}

		public LaegueManagerinfoEntity(
		System.Guid managerid
,				System.Int32 sumscore
,				System.String exchangeids
,				System.String exchangedids
,				System.String equipmentitems
,				System.String equipmentproperties
,				System.DateTime refreshdate
,				System.Int32 refreshtimes
,				System.DateTime updatetime
,				System.DateTime rowtime
,				System.Int32 dailywincount
,				System.DateTime dailywinupdatetime
		)
		{
			this.ManagerId = managerid;
			this.SumScore = sumscore;
			this.ExchangeIds = exchangeids;
			this.ExchangedIds = exchangedids;
			this.EquipmentItems = equipmentitems;
			this.EquipmentProperties = equipmentproperties;
			this.RefreshDate = refreshdate;
			this.RefreshTimes = refreshtimes;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
			this.DailyWinCount = dailywincount;
			this.DailyWinUpdateTime = dailywinupdatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///联赛积分
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SumScore {get ; set ;}

		///<summary>
		///可兑换物品 ExchangeId,ItemCode|ExchangeId,ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ExchangeIds {get ; set ;}

		///<summary>
		///已兑换的物品 ExchangeId,ItemCode|ExchangeId,ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String ExchangedIds {get ; set ;}

		///<summary>
		///EquipmentItems
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String EquipmentItems {get ; set ;}

		///<summary>
		///EquipmentProperties
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String EquipmentProperties {get ; set ;}

		///<summary>
		///刷新时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RefreshDate {get ; set ;}

		///<summary>
		///刷新次数
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 RefreshTimes {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///DailyWinCount
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 DailyWinCount {get ; set ;}

		///<summary>
		///DailyWinUpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime DailyWinUpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public LaegueManagerinfoEntity Clone()
        {
            LaegueManagerinfoEntity entity = new LaegueManagerinfoEntity();
			entity.ManagerId = this.ManagerId;
			entity.SumScore = this.SumScore;
			entity.ExchangeIds = this.ExchangeIds;
			entity.ExchangedIds = this.ExchangedIds;
			entity.EquipmentItems = this.EquipmentItems;
			entity.EquipmentProperties = this.EquipmentProperties;
			entity.RefreshDate = this.RefreshDate;
			entity.RefreshTimes = this.RefreshTimes;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
			entity.DailyWinCount = this.DailyWinCount;
			entity.DailyWinUpdateTime = this.DailyWinUpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Laegue_ManagerInfo 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LaegueManagerinfoResponse : BaseResponse<LaegueManagerinfoEntity>
    {

    }
}

