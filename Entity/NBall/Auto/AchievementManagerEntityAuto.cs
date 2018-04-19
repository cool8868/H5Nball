
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Achievement_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AchievementManagerEntity
	{
		
		public AchievementManagerEntity()
		{
		}

		public AchievementManagerEntity(
		System.Guid managerid
,				System.Int32 purplecardcount
,				System.Int32 orangecardcount
,				System.Int32 silvercardcount
,				System.Int32 goldcardcount
,				System.Int32 maxladdergoals
,				System.Int32 maxpkmatchgoals
,				System.Int32 daypkmatchgoals
,				System.DateTime daypkmatchdate
,				System.Int32 maxdaypkmatchgoals
,				System.Int32 maxladderwin
,				System.Int32 ladderwin
,				System.Int32 ladderseason
,				System.Int32 friendwincomb
,				System.Int32 maxfriendwincomb
,				System.Int32 maxdailycuprank
,				System.Int32 level5cardcount
,				System.Int32 level10cardcount
,				System.Int32 level20cardcount
,				System.Int32 level30cardcount
,				System.Int32 leaguescore1
,				System.Int32 leaguescore2
,				System.Int32 leaguescore3
,				System.Int32 leaguescore4
,				System.Int32 leaguescore5
,				System.Int32 leaguescore6
,				System.Int32 leaguescore7
,				System.Int32 leaguescore8
		)
		{
			this.ManagerId = managerid;
			this.PurpleCardCount = purplecardcount;
			this.OrangeCardCount = orangecardcount;
			this.SilverCardCount = silvercardcount;
			this.GoldCardCount = goldcardcount;
			this.MaxLadderGoals = maxladdergoals;
			this.MaxPkMatchGoals = maxpkmatchgoals;
			this.DayPkMatchGoals = daypkmatchgoals;
			this.DayPkMatchDate = daypkmatchdate;
			this.MaxDayPkMatchGoals = maxdaypkmatchgoals;
			this.MaxLadderWin = maxladderwin;
			this.LadderWin = ladderwin;
			this.LadderSeason = ladderseason;
			this.FriendWinComb = friendwincomb;
			this.MaxFriendWinComb = maxfriendwincomb;
			this.MaxDailyCupRank = maxdailycuprank;
			this.Level5CardCount = level5cardcount;
			this.Level10CardCount = level10cardcount;
			this.Level20CardCount = level20cardcount;
			this.Level30CardCount = level30cardcount;
			this.LeagueScore1 = leaguescore1;
			this.LeagueScore2 = leaguescore2;
			this.LeagueScore3 = leaguescore3;
			this.LeagueScore4 = leaguescore4;
			this.LeagueScore5 = leaguescore5;
			this.LeagueScore6 = leaguescore6;
			this.LeagueScore7 = leaguescore7;
			this.LeagueScore8 = leaguescore8;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///紫卡数量
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PurpleCardCount {get ; set ;}

		///<summary>
		///橙卡数量
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 OrangeCardCount {get ; set ;}

		///<summary>
		///银卡数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SilverCardCount {get ; set ;}

		///<summary>
		///金卡数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 GoldCardCount {get ; set ;}

		///<summary>
		///单场天梯赛最多进球
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 MaxLadderGoals {get ; set ;}

		///<summary>
		///单场PK赛最多进球
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 MaxPkMatchGoals {get ; set ;}

		///<summary>
		///单日Pk赛进球数
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 DayPkMatchGoals {get ; set ;}

		///<summary>
		///PK赛统计日期
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime DayPkMatchDate {get ; set ;}

		///<summary>
		///单日PK赛最高进球总数
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 MaxDayPkMatchGoals {get ; set ;}

		///<summary>
		///单赛季天梯赛最高胜场
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 MaxLadderWin {get ; set ;}

		///<summary>
		///单赛季天梯赛胜场
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 LadderWin {get ; set ;}

		///<summary>
		///天梯赛赛季
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 LadderSeason {get ; set ;}

		///<summary>
		///好友赛当前连胜场次
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 FriendWinComb {get ; set ;}

		///<summary>
		///好友赛最多连胜
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 MaxFriendWinComb {get ; set ;}

		///<summary>
		/// 
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 MaxDailyCupRank {get ; set ;}

		///<summary>
		///Level5CardCount
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 Level5CardCount {get ; set ;}

		///<summary>
		///Level10CardCount
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 Level10CardCount {get ; set ;}

		///<summary>
		///Level20CardCount
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 Level20CardCount {get ; set ;}

		///<summary>
		///Level30CardCount
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 Level30CardCount {get ; set ;}

		///<summary>
		///联赛1冠军积分 
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 LeagueScore1 {get ; set ;}

		///<summary>
		///联赛2冠军积分 
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 LeagueScore2 {get ; set ;}

		///<summary>
		///联赛3冠军积分 
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 LeagueScore3 {get ; set ;}

		///<summary>
		///联赛4冠军积分 
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 LeagueScore4 {get ; set ;}

		///<summary>
		///联赛5冠军积分 
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int32 LeagueScore5 {get ; set ;}

		///<summary>
		///联赛6冠军积分 
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Int32 LeagueScore6 {get ; set ;}

		///<summary>
		///联赛7冠军积分 
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Int32 LeagueScore7 {get ; set ;}

		///<summary>
		///联赛8冠军积分 
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Int32 LeagueScore8 {get ; set ;}
		#endregion
        
        #region Clone
        public AchievementManagerEntity Clone()
        {
            AchievementManagerEntity entity = new AchievementManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.PurpleCardCount = this.PurpleCardCount;
			entity.OrangeCardCount = this.OrangeCardCount;
			entity.SilverCardCount = this.SilverCardCount;
			entity.GoldCardCount = this.GoldCardCount;
			entity.MaxLadderGoals = this.MaxLadderGoals;
			entity.MaxPkMatchGoals = this.MaxPkMatchGoals;
			entity.DayPkMatchGoals = this.DayPkMatchGoals;
			entity.DayPkMatchDate = this.DayPkMatchDate;
			entity.MaxDayPkMatchGoals = this.MaxDayPkMatchGoals;
			entity.MaxLadderWin = this.MaxLadderWin;
			entity.LadderWin = this.LadderWin;
			entity.LadderSeason = this.LadderSeason;
			entity.FriendWinComb = this.FriendWinComb;
			entity.MaxFriendWinComb = this.MaxFriendWinComb;
			entity.MaxDailyCupRank = this.MaxDailyCupRank;
			entity.Level5CardCount = this.Level5CardCount;
			entity.Level10CardCount = this.Level10CardCount;
			entity.Level20CardCount = this.Level20CardCount;
			entity.Level30CardCount = this.Level30CardCount;
			entity.LeagueScore1 = this.LeagueScore1;
			entity.LeagueScore2 = this.LeagueScore2;
			entity.LeagueScore3 = this.LeagueScore3;
			entity.LeagueScore4 = this.LeagueScore4;
			entity.LeagueScore5 = this.LeagueScore5;
			entity.LeagueScore6 = this.LeagueScore6;
			entity.LeagueScore7 = this.LeagueScore7;
			entity.LeagueScore8 = this.LeagueScore8;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Achievement_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AchievementManagerResponse : BaseResponse<AchievementManagerEntity>
    {

    }
}

