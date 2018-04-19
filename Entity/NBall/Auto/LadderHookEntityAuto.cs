
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Ladder_Hook 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LadderHookEntity
	{
		
		public LadderHookEntity()
		{
		}

		public LadderHookEntity(
		System.Guid managerid
,				System.Int32 curtimes
,				System.Int32 curwiningtimes
,				System.Int32 maxtimes
,				System.Int32 minscore
,				System.Int32 maxscore
,				System.Int32 maxwiningtimes
,				System.DateTime nextmatchtime
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.DateTime expired
		)
		{
			this.ManagerId = managerid;
			this.CurTimes = curtimes;
			this.CurWiningTimes = curwiningtimes;
			this.MaxTimes = maxtimes;
			this.MinScore = minscore;
			this.MaxScore = maxscore;
			this.MaxWiningTimes = maxwiningtimes;
			this.NextMatchTime = nextmatchtime;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.Expired = expired;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///CurTimes
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CurTimes {get ; set ;}

		///<summary>
		///CurWiningTimes
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 CurWiningTimes {get ; set ;}

		///<summary>
		///MaxTimes
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MaxTimes {get ; set ;}

		///<summary>
		///MinScore
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 MinScore {get ; set ;}

		///<summary>
		///MaxScore
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 MaxScore {get ; set ;}

		///<summary>
		///MaxWiningTimes
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 MaxWiningTimes {get ; set ;}

		///<summary>
		///NextMatchTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime NextMatchTime {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///Expired
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime Expired {get ; set ;}
		#endregion
        
        #region Clone
        public LadderHookEntity Clone()
        {
            LadderHookEntity entity = new LadderHookEntity();
			entity.ManagerId = this.ManagerId;
			entity.CurTimes = this.CurTimes;
			entity.CurWiningTimes = this.CurWiningTimes;
			entity.MaxTimes = this.MaxTimes;
			entity.MinScore = this.MinScore;
			entity.MaxScore = this.MaxScore;
			entity.MaxWiningTimes = this.MaxWiningTimes;
			entity.NextMatchTime = this.NextMatchTime;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.Expired = this.Expired;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Ladder_Hook 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LadderHookResponse : BaseResponse<LadderHookEntity>
    {

    }
}
