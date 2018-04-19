
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.League_ManagerRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LeagueManagerrecordEntity
	{
		
		public LeagueManagerrecordEntity()
		{
		}

		public LeagueManagerrecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 laegueid
,				System.Guid leaguerecordid
,				System.Guid lastprizeleaguerecordid
,				System.Boolean sendfirstpassprize
,				System.Guid matchid
,				System.Int32 maxwheelnumber
,				System.Int32 score
,				System.Boolean islock
,				System.Boolean isstart
,				System.Boolean ispass
,				System.Int32 passnumber
,				System.Int32 matchnumber
,				System.Int32 winnumber
,				System.Int32 flatnumber
,				System.Int32 losenumber
,				System.Int32 goalsnumber
,				System.Int32 fumblenumber
,				System.DateTime updatetime
,				System.DateTime rowtime
,				System.Int32 fightdicid
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.LaegueId = laegueid;
			this.LeagueRecordId = leaguerecordid;
			this.LastPrizeLeagueRecordId = lastprizeleaguerecordid;
			this.SendFirstPassPrize = sendfirstpassprize;
			this.MatchId = matchid;
			this.MaxWheelNumber = maxwheelnumber;
			this.Score = score;
			this.IsLock = islock;
			this.IsStart = isstart;
			this.IsPass = ispass;
			this.PassNumber = passnumber;
			this.MatchNumber = matchnumber;
			this.WinNumber = winnumber;
			this.FlatNumber = flatnumber;
			this.LoseNumber = losenumber;
			this.GoalsNumber = goalsnumber;
			this.FumbleNumber = fumblenumber;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
			this.FightDicId = fightdicid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

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
		///联赛记录ID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid LeagueRecordId {get ; set ;}

		///<summary>
		///最后一次通关并且未领取奖励记录id
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Guid LastPrizeLeagueRecordId {get ; set ;}

		///<summary>
		///是否已领取首次通关奖励
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Boolean SendFirstPassPrize {get ; set ;}

		///<summary>
		///未确认的比赛
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Guid MatchId {get ; set ;}

		///<summary>
		///总比赛轮数
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 MaxWheelNumber {get ; set ;}

		///<summary>
		///本次联赛获得积分
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///是否解锁了联赛
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Boolean IsLock {get ; set ;}

		///<summary>
		///是否开始了本场联赛
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Boolean IsStart {get ; set ;}

		///<summary>
		///本次联赛是否通关了
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Boolean IsPass {get ; set ;}

		///<summary>
		///通关联赛次数
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 PassNumber {get ; set ;}

		///<summary>
		///本届联赛比赛次数
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 MatchNumber {get ; set ;}

		///<summary>
		///本届联赛胜场
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 WinNumber {get ; set ;}

		///<summary>
		///本届联赛平场
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 FlatNumber {get ; set ;}

		///<summary>
		///本届联赛负场
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 LoseNumber {get ; set ;}

		///<summary>
		///本届总进球数
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 GoalsNumber {get ; set ;}

		///<summary>
		///本届总失球数
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 FumbleNumber {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(20)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(21)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///FightDicId
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 FightDicId {get ; set ;}
		#endregion
        
        #region Clone
        public LeagueManagerrecordEntity Clone()
        {
            LeagueManagerrecordEntity entity = new LeagueManagerrecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.LaegueId = this.LaegueId;
			entity.LeagueRecordId = this.LeagueRecordId;
			entity.LastPrizeLeagueRecordId = this.LastPrizeLeagueRecordId;
			entity.SendFirstPassPrize = this.SendFirstPassPrize;
			entity.MatchId = this.MatchId;
			entity.MaxWheelNumber = this.MaxWheelNumber;
			entity.Score = this.Score;
			entity.IsLock = this.IsLock;
			entity.IsStart = this.IsStart;
			entity.IsPass = this.IsPass;
			entity.PassNumber = this.PassNumber;
			entity.MatchNumber = this.MatchNumber;
			entity.WinNumber = this.WinNumber;
			entity.FlatNumber = this.FlatNumber;
			entity.LoseNumber = this.LoseNumber;
			entity.GoalsNumber = this.GoalsNumber;
			entity.FumbleNumber = this.FumbleNumber;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
			entity.FightDicId = this.FightDicId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.League_ManagerRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LeagueManagerrecordResponse : BaseResponse<LeagueManagerrecordEntity>
    {

    }
}

