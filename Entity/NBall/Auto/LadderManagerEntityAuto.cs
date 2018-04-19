
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Ladder_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LadderManagerEntity
	{
		
		public LadderManagerEntity()
		{
		}

		public LadderManagerEntity(
		System.Guid managerid
,				System.Int32 score
,				System.Int32 newlyscore
,				System.Int32 newlyhonor
,				System.Int32 honor
,				System.Int32 maxscore
,				System.Int32 matchtime
,				System.DateTime lastexchagetime
,				System.String exchangeids
,				System.String exchangedids
,				System.DateTime refreshdate
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Byte[] rowversion
,				System.Int32 refreshtimes
,				System.String equipmentproperties
,				System.String equipmentitems
,				System.Int32 laddercoin
		)
		{
			this.ManagerId = managerid;
			this.Score = score;
			this.NewlyScore = newlyscore;
			this.NewlyHonor = newlyhonor;
			this.Honor = honor;
			this.MaxScore = maxscore;
			this.MatchTime = matchtime;
			this.LastExchageTime = lastexchagetime;
			this.ExchangeIds = exchangeids;
			this.ExchangedIds = exchangedids;
			this.RefreshDate = refreshdate;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.RowVersion = rowversion;
			this.RefreshTimes = refreshtimes;
			this.EquipmentProperties = equipmentproperties;
			this.EquipmentItems = equipmentitems;
			this.LadderCoin = laddercoin;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///天梯积分
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///最近增加积分
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 NewlyScore {get ; set ;}

		///<summary>
		///最近兑换荣誉数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 NewlyHonor {get ; set ;}

		///<summary>
		///荣誉数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Honor {get ; set ;}

		///<summary>
		///最大积分
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 MaxScore {get ; set ;}

		///<summary>
		///今日比赛场次
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 MatchTime {get ; set ;}

		///<summary>
		///最近兑换时间
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime LastExchageTime {get ; set ;}

		///<summary>
		///ExchangeId,ItemCode|ExchangeId,ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String ExchangeIds {get ; set ;}

		///<summary>
		///已兑换的物品 ExchangeId,ItemCode|ExchangeId,ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String ExchangedIds {get ; set ;}

		///<summary>
		///RefreshDate
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime RefreshDate {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}

		///<summary>
		///RefreshTimes
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 RefreshTimes {get ; set ;}

		///<summary>
		///EquipmentProperties
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String EquipmentProperties {get ; set ;}

		///<summary>
		///EquipmentItems
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.String EquipmentItems {get ; set ;}

		///<summary>
		///LadderCoin
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 LadderCoin {get ; set ;}
		#endregion
        
        #region Clone
        public LadderManagerEntity Clone()
        {
            LadderManagerEntity entity = new LadderManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.Score = this.Score;
			entity.NewlyScore = this.NewlyScore;
			entity.NewlyHonor = this.NewlyHonor;
			entity.Honor = this.Honor;
			entity.MaxScore = this.MaxScore;
			entity.MatchTime = this.MatchTime;
			entity.LastExchageTime = this.LastExchageTime;
			entity.ExchangeIds = this.ExchangeIds;
			entity.ExchangedIds = this.ExchangedIds;
			entity.RefreshDate = this.RefreshDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.RowVersion = this.RowVersion;
			entity.RefreshTimes = this.RefreshTimes;
			entity.EquipmentProperties = this.EquipmentProperties;
			entity.EquipmentItems = this.EquipmentItems;
			entity.LadderCoin = this.LadderCoin;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Ladder_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LadderManagerResponse : BaseResponse<LadderManagerEntity>
    {

    }
}
