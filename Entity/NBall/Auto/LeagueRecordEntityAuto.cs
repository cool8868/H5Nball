
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.League_Record 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LeagueRecordEntity
	{
		
		public LeagueRecordEntity()
		{
		}

		public LeagueRecordEntity(
		System.Guid idx
,				System.Guid managerid
,				System.Int32 laegueid
,				System.Int32 schedule
,				System.Int32 score
,				System.Int32 rank
,				System.Boolean issend
,				System.DateTime updatetime
,				System.DateTime rowtime
,				System.DateTime prizetime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.LaegueId = laegueid;
			this.Schedule = schedule;
			this.Score = score;
			this.Rank = rank;
			this.IsSend = issend;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
			this.PrizeTime = prizetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///联赛ID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 LaegueId {get ; set ;}

		///<summary>
		///进度
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///获得积分，用于查看记录
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///排名，用户查看记录
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///是否发奖
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsSend {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///通关奖励领取时间
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime PrizeTime {get ; set ;}
		#endregion
        
        #region Clone
        public LeagueRecordEntity Clone()
        {
            LeagueRecordEntity entity = new LeagueRecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.LaegueId = this.LaegueId;
			entity.Schedule = this.Schedule;
			entity.Score = this.Score;
			entity.Rank = this.Rank;
			entity.IsSend = this.IsSend;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
			entity.PrizeTime = this.PrizeTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.League_Record 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LeagueRecordResponse : BaseResponse<LeagueRecordEntity>
    {

    }
}

