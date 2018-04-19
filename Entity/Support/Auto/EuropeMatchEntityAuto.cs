
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Europe_Match 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class EuropeMatchEntity
	{
		
		public EuropeMatchEntity()
		{
		}

		public EuropeMatchEntity(
		System.Int32 matchid
,				System.String homename
,				System.String awayname
,				System.DateTime matchdate
,				System.DateTime matchtime
,				System.Int32 homegoals
,				System.Int32 awaygoals
,				System.Int32 resulttype
,				System.Int32 states
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.MatchId = matchid;
			this.HomeName = homename;
			this.AwayName = awayname;
			this.MatchDate = matchdate;
			this.MatchTime = matchtime;
			this.HomeGoals = homegoals;
			this.AwayGoals = awaygoals;
			this.ResultType = resulttype;
			this.States = states;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///MatchId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 MatchId {get ; set ;}

		///<summary>
		///HomeName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String HomeName {get ; set ;}

		///<summary>
		///AwayName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String AwayName {get ; set ;}

		///<summary>
		///比赛日期
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.DateTime MatchDate {get ; set ;}

		///<summary>
		///比赛时间
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime MatchTime {get ; set ;}

		///<summary>
		///HomeGoals
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 HomeGoals {get ; set ;}

		///<summary>
		///AwayGoals
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 AwayGoals {get ; set ;}

		///<summary>
		///比赛结果类型 1主胜 2平  3客胜
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ResultType {get ; set ;}

		///<summary>
		///状态  0初始  1可竞猜 2比赛中 4发奖完成
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 States {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public EuropeMatchEntity Clone()
        {
            EuropeMatchEntity entity = new EuropeMatchEntity();
			entity.MatchId = this.MatchId;
			entity.HomeName = this.HomeName;
			entity.AwayName = this.AwayName;
			entity.MatchDate = this.MatchDate;
			entity.MatchTime = this.MatchTime;
			entity.HomeGoals = this.HomeGoals;
			entity.AwayGoals = this.AwayGoals;
			entity.ResultType = this.ResultType;
			entity.States = this.States;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Europe_Match 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class EuropeMatchResponse : BaseResponse<EuropeMatchEntity>
    {

    }
}
