
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.PlayerKill_Match 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PlayerkillMatchEntity
	{
		
		public PlayerkillMatchEntity()
		{
		}

		public PlayerkillMatchEntity(
		System.Guid idx
,				System.Guid homeid
,				System.Guid awayid
,				System.String homename
,				System.String awayname
,				System.Int32 homescore
,				System.Int32 awayscore
,				System.Int32 prizeexp
,				System.Int32 prizecoin
,				System.Int32 prizeitemcode
,				System.String prizeitemstring
,				System.Int32 prizeitemcount
,				System.Boolean isrevenge
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.HomeId = homeid;
			this.AwayId = awayid;
			this.HomeName = homename;
			this.AwayName = awayname;
			this.HomeScore = homescore;
			this.AwayScore = awayscore;
			this.PrizeExp = prizeexp;
			this.PrizeCoin = prizecoin;
			this.PrizeItemCode = prizeitemcode;
			this.PrizeItemString = prizeitemstring;
			this.PrizeItemCount = prizeitemcount;
			this.IsRevenge = isrevenge;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///比赛id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///主队经理id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid HomeId {get ; set ;}

		///<summary>
		///客队 id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid AwayId {get ; set ;}

		///<summary>
		///主队名
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String HomeName {get ; set ;}

		///<summary>
		///客队名
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String AwayName {get ; set ;}

		///<summary>
		///主队比分
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 HomeScore {get ; set ;}

		///<summary>
		///客队比分
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 AwayScore {get ; set ;}

		///<summary>
		///PrizeExp
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PrizeExp {get ; set ;}

		///<summary>
		///PrizeCoin
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 PrizeCoin {get ; set ;}

		///<summary>
		///PrizeItemCode
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 PrizeItemCode {get ; set ;}

		///<summary>
		///PrizeItemString
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String PrizeItemString {get ; set ;}

		///<summary>
		///PrizeItemCount
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 PrizeItemCount {get ; set ;}

		///<summary>
		///IsRevenge
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Boolean IsRevenge {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///记录时间
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public PlayerkillMatchEntity Clone()
        {
            PlayerkillMatchEntity entity = new PlayerkillMatchEntity();
			entity.Idx = this.Idx;
			entity.HomeId = this.HomeId;
			entity.AwayId = this.AwayId;
			entity.HomeName = this.HomeName;
			entity.AwayName = this.AwayName;
			entity.HomeScore = this.HomeScore;
			entity.AwayScore = this.AwayScore;
			entity.PrizeExp = this.PrizeExp;
			entity.PrizeCoin = this.PrizeCoin;
			entity.PrizeItemCode = this.PrizeItemCode;
			entity.PrizeItemString = this.PrizeItemString;
			entity.PrizeItemCount = this.PrizeItemCount;
			entity.IsRevenge = this.IsRevenge;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.PlayerKill_Match 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PlayerkillMatchResponse : BaseResponse<PlayerkillMatchEntity>
    {

    }
}
