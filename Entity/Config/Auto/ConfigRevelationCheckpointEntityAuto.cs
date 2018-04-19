
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationCheckpoint 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationcheckpointEntity
	{
		
		public ConfigRevelationcheckpointEntity()
		{
		}

		public ConfigRevelationcheckpointEntity(
		System.Int32 mark
,				System.Int32 smallclearance
,				System.String checkpointplayers
,				System.String describe
,				System.String thestory
,				System.String team
,				System.String againsttheteam
,				System.Int32 formation
,				System.Int32 thegoalkeeperid
,				System.String thegoalkeepername
,				System.Int32 playersid1
,				System.String playersname1
,				System.Int32 playersid2
,				System.String playersname2
,				System.Int32 playersid3
,				System.String playersname3
,				System.Int32 playersid4
,				System.String playersname4
,				System.Int32 playersid5
,				System.String playersname5
,				System.Int32 playersid6
,				System.String playersname6
,				System.Int32 playersid7
,				System.String playersname7
,				System.Int32 playersid8
,				System.String playersname8
,				System.Int32 playersid9
,				System.String playersname9
,				System.Int32 playersid10
,				System.String playersname10
		)
		{
			this.Mark = mark;
			this.SmallClearance = smallclearance;
			this.CheckpointPlayers = checkpointplayers;
			this.Describe = describe;
			this.TheStory = thestory;
			this.Team = team;
			this.AgainstTheTeam = againsttheteam;
			this.Formation = formation;
			this.TheGoalkeeperID = thegoalkeeperid;
			this.TheGoalkeeperName = thegoalkeepername;
			this.PlayersID1 = playersid1;
			this.PlayersName1 = playersname1;
			this.PlayersID2 = playersid2;
			this.PlayersName2 = playersname2;
			this.PlayersID3 = playersid3;
			this.PlayersName3 = playersname3;
			this.PlayersID4 = playersid4;
			this.PlayersName4 = playersname4;
			this.PlayersID5 = playersid5;
			this.PlayersName5 = playersname5;
			this.PlayersID6 = playersid6;
			this.PlayersName6 = playersname6;
			this.PlayersID7 = playersid7;
			this.PlayersName7 = playersname7;
			this.PlayersID8 = playersid8;
			this.PlayersName8 = playersname8;
			this.PlayersID9 = playersid9;
			this.PlayersName9 = playersname9;
			this.PlayersID10 = playersid10;
			this.PlayersName10 = playersname10;
		}
		
		#region Public Properties
		
		///<summary>
		///大关
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Mark {get ; set ;}

		///<summary>
		///小关卡
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SmallClearance {get ; set ;}

		///<summary>
		///关卡球员
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String CheckpointPlayers {get ; set ;}

		///<summary>
		///描述
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Describe {get ; set ;}

		///<summary>
		///故事
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String TheStory {get ; set ;}

		///<summary>
		///所在球队
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Team {get ; set ;}

		///<summary>
		///对阵球队
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String AgainstTheTeam {get ; set ;}

		///<summary>
		///阵形
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Formation {get ; set ;}

		///<summary>
		///门将ID
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 TheGoalkeeperID {get ; set ;}

		///<summary>
		///门将名字
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String TheGoalkeeperName {get ; set ;}

		///<summary>
		///球员ID1
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 PlayersID1 {get ; set ;}

		///<summary>
		///球员名字1
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String PlayersName1 {get ; set ;}

		///<summary>
		///球员ID2
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 PlayersID2 {get ; set ;}

		///<summary>
		///球员名字2
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String PlayersName2 {get ; set ;}

		///<summary>
		///球员ID3
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 PlayersID3 {get ; set ;}

		///<summary>
		///球员名字3
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.String PlayersName3 {get ; set ;}

		///<summary>
		///球员ID4
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 PlayersID4 {get ; set ;}

		///<summary>
		///球员名字4
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.String PlayersName4 {get ; set ;}

		///<summary>
		///球员ID5
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 PlayersID5 {get ; set ;}

		///<summary>
		///球员名字5
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.String PlayersName5 {get ; set ;}

		///<summary>
		///球员ID6
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 PlayersID6 {get ; set ;}

		///<summary>
		///球员名字6
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.String PlayersName6 {get ; set ;}

		///<summary>
		///球员ID7
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 PlayersID7 {get ; set ;}

		///<summary>
		///球员名字7
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.String PlayersName7 {get ; set ;}

		///<summary>
		///球员ID8
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int32 PlayersID8 {get ; set ;}

		///<summary>
		///球员名字8
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.String PlayersName8 {get ; set ;}

		///<summary>
		///球员ID9
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Int32 PlayersID9 {get ; set ;}

		///<summary>
		///球员名字9
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.String PlayersName9 {get ; set ;}

		///<summary>
		///球员ID10
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Int32 PlayersID10 {get ; set ;}

		///<summary>
		///球员名字10
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.String PlayersName10 {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationcheckpointEntity Clone()
        {
            ConfigRevelationcheckpointEntity entity = new ConfigRevelationcheckpointEntity();
			entity.Mark = this.Mark;
			entity.SmallClearance = this.SmallClearance;
			entity.CheckpointPlayers = this.CheckpointPlayers;
			entity.Describe = this.Describe;
			entity.TheStory = this.TheStory;
			entity.Team = this.Team;
			entity.AgainstTheTeam = this.AgainstTheTeam;
			entity.Formation = this.Formation;
			entity.TheGoalkeeperID = this.TheGoalkeeperID;
			entity.TheGoalkeeperName = this.TheGoalkeeperName;
			entity.PlayersID1 = this.PlayersID1;
			entity.PlayersName1 = this.PlayersName1;
			entity.PlayersID2 = this.PlayersID2;
			entity.PlayersName2 = this.PlayersName2;
			entity.PlayersID3 = this.PlayersID3;
			entity.PlayersName3 = this.PlayersName3;
			entity.PlayersID4 = this.PlayersID4;
			entity.PlayersName4 = this.PlayersName4;
			entity.PlayersID5 = this.PlayersID5;
			entity.PlayersName5 = this.PlayersName5;
			entity.PlayersID6 = this.PlayersID6;
			entity.PlayersName6 = this.PlayersName6;
			entity.PlayersID7 = this.PlayersID7;
			entity.PlayersName7 = this.PlayersName7;
			entity.PlayersID8 = this.PlayersID8;
			entity.PlayersName8 = this.PlayersName8;
			entity.PlayersID9 = this.PlayersID9;
			entity.PlayersName9 = this.PlayersName9;
			entity.PlayersID10 = this.PlayersID10;
			entity.PlayersName10 = this.PlayersName10;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationCheckpoint 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationcheckpointResponse : BaseResponse<ConfigRevelationcheckpointEntity>
    {

    }
}

