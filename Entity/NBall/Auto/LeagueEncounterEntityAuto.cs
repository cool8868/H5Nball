
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.League_Encounter 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LeagueEncounterEntity
	{
		
		public LeagueEncounterEntity()
		{
		}

		public LeagueEncounterEntity(
		System.Guid matchid
,				System.Guid leaguerecordid
,				System.String homename
,				System.String awayname
,				System.Int32 wheelnumber
,				System.Guid homeid
,				System.Guid awayid
,				System.Boolean homeisnpc
,				System.Boolean awayisnpc
,				System.Boolean ismatch
,				System.Boolean rematched
,				System.Boolean confirmed
,				System.Int32 homegoals
,				System.Int32 awaygoals
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.MatchId = matchid;
			this.LeagueRecordId = leaguerecordid;
			this.HomeName = homename;
			this.AwayName = awayname;
			this.WheelNumber = wheelnumber;
			this.HomeId = homeid;
			this.AwayId = awayid;
			this.HomeIsNpc = homeisnpc;
			this.AwayIsNpc = awayisnpc;
			this.IsMatch = ismatch;
			this.ReMatched = rematched;
			this.Confirmed = confirmed;
			this.HomeGoals = homegoals;
			this.AwayGoals = awaygoals;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///比赛ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid MatchId {get ; set ;}

		///<summary>
		///联赛记录ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid LeagueRecordId {get ; set ;}

		///<summary>
		///主队经理名
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String HomeName {get ; set ;}

		///<summary>
		///客队经理名
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String AwayName {get ; set ;}

		///<summary>
		///轮数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 WheelNumber {get ; set ;}

		///<summary>
		///主队经理ID
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Guid HomeId {get ; set ;}

		///<summary>
		///客队经理ID
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Guid AwayId {get ; set ;}

		///<summary>
		///主队是否是NPC
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean HomeIsNpc {get ; set ;}

		///<summary>
		///客队是否是NPC
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Boolean AwayIsNpc {get ; set ;}

		///<summary>
		///是否比赛了
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Boolean IsMatch {get ; set ;}

		///<summary>
		///是否已重赛过
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Boolean ReMatched {get ; set ;}

		///<summary>
		///是否已确认过
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Boolean Confirmed {get ; set ;}

		///<summary>
		///主队进球数
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 HomeGoals {get ; set ;}

		///<summary>
		///客队进球数
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 AwayGoals {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public LeagueEncounterEntity Clone()
        {
            LeagueEncounterEntity entity = new LeagueEncounterEntity();
			entity.MatchId = this.MatchId;
			entity.LeagueRecordId = this.LeagueRecordId;
			entity.HomeName = this.HomeName;
			entity.AwayName = this.AwayName;
			entity.WheelNumber = this.WheelNumber;
			entity.HomeId = this.HomeId;
			entity.AwayId = this.AwayId;
			entity.HomeIsNpc = this.HomeIsNpc;
			entity.AwayIsNpc = this.AwayIsNpc;
			entity.IsMatch = this.IsMatch;
			entity.ReMatched = this.ReMatched;
			entity.Confirmed = this.Confirmed;
			entity.HomeGoals = this.HomeGoals;
			entity.AwayGoals = this.AwayGoals;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.League_Encounter 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LeagueEncounterResponse : BaseResponse<LeagueEncounterEntity>
    {

    }
}
