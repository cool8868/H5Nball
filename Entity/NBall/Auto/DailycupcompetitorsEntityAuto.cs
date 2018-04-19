
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.DailyCup_Competitors 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DailycupCompetitorsEntity
	{
		
		public DailycupCompetitorsEntity()
		{
		}

		public DailycupCompetitorsEntity(
		System.Int32 idx
,				System.Int32 dailycupid
,				System.Guid managerid
,				System.String managername
,				System.String logo
,				System.Int32 maxround
,				System.Int32 wincount
,				System.Int32 rank
,				System.Int32 prizescore
,				System.Int32 prizesophisticate
,				System.Int32 prizecoin
,				System.String prizeitems
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.DailyCupId = dailycupid;
			this.ManagerId = managerid;
			this.ManagerName = managername;
			this.Logo = logo;
			this.MaxRound = maxround;
			this.WinCount = wincount;
			this.Rank = rank;
			this.PrizeScore = prizescore;
			this.PrizeSophisticate = prizesophisticate;
			this.PrizeCoin = prizecoin;
			this.PrizeItems = prizeitems;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///每日杯赛id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DailyCupId {get ; set ;}

		///<summary>
		///报名经理id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///经理名 
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///Logo
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Logo {get ; set ;}

		///<summary>
		///MaxRound
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 MaxRound {get ; set ;}

		///<summary>
		///WinCount
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 WinCount {get ; set ;}

		///<summary>
		///排名
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///获得的积分
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 PrizeScore {get ; set ;}

		///<summary>
		///PrizeSophisticate
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 PrizeSophisticate {get ; set ;}

		///<summary>
		///PrizeCoin
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 PrizeCoin {get ; set ;}

		///<summary>
		///奖励物品
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String PrizeItems {get ; set ;}

		///<summary>
		///状态：0，初始；1，已发奖；
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DailycupCompetitorsEntity Clone()
        {
            DailycupCompetitorsEntity entity = new DailycupCompetitorsEntity();
			entity.Idx = this.Idx;
			entity.DailyCupId = this.DailyCupId;
			entity.ManagerId = this.ManagerId;
			entity.ManagerName = this.ManagerName;
			entity.Logo = this.Logo;
			entity.MaxRound = this.MaxRound;
			entity.WinCount = this.WinCount;
			entity.Rank = this.Rank;
			entity.PrizeScore = this.PrizeScore;
			entity.PrizeSophisticate = this.PrizeSophisticate;
			entity.PrizeCoin = this.PrizeCoin;
			entity.PrizeItems = this.PrizeItems;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.DailyCup_Competitors 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DailycupCompetitorsResponse : BaseResponse<DailycupCompetitorsEntity>
    {

    }
}

