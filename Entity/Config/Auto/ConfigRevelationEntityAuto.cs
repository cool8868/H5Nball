
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Revelation 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationEntity
	{
		
		public ConfigRevelationEntity()
		{
		}

		public ConfigRevelationEntity(
		System.Int32 idx
,				System.Int32 markid
,				System.Int32 schedule
,				System.Guid npcid
,				System.Int32 markplayerid
,				System.String markplayer
,				System.String describe
,				System.String teamname
,				System.String opponentteamname
,				System.String formation
,				System.String opponentformation
,				System.String passprizeitem
,				System.String firstpassitem
,				System.Int32 couragenumber
,				System.String story
		)
		{
			this.Idx = idx;
			this.MarkId = markid;
			this.Schedule = schedule;
			this.NpcId = npcid;
			this.MarkPlayerId = markplayerid;
			this.MarkPlayer = markplayer;
			this.Describe = describe;
			this.TeamName = teamname;
			this.OpponentTeamName = opponentteamname;
			this.Formation = formation;
			this.OpponentFormation = opponentformation;
			this.PassPrizeItem = passprizeitem;
			this.FirstPassItem = firstpassitem;
			this.CourageNumber = couragenumber;
			this.Story = story;
		}
		
		#region Public Properties
		
		///<summary>
		///关卡ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///大关卡
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 MarkId {get ; set ;}

		///<summary>
		///小关卡
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///NPCID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid NpcId {get ; set ;}

		///<summary>
		///关卡球星ID
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 MarkPlayerId {get ; set ;}

		///<summary>
		///关卡球星 
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String MarkPlayer {get ; set ;}

		///<summary>
		///描述
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String Describe {get ; set ;}

		///<summary>
		///球队
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String TeamName {get ; set ;}

		///<summary>
		///对手
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String OpponentTeamName {get ; set ;}

		///<summary>
		///球队阵型
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String Formation {get ; set ;}

		///<summary>
		///对手阵型
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String OpponentFormation {get ; set ;}

		///<summary>
		///通关奖励物品串
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String PassPrizeItem {get ; set ;}

		///<summary>
		///首次通关奖励物品
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String FirstPassItem {get ; set ;}

		///<summary>
		///获得勇气值数量
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 CourageNumber {get ; set ;}

		///<summary>
		///故事
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String Story {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationEntity Clone()
        {
            ConfigRevelationEntity entity = new ConfigRevelationEntity();
			entity.Idx = this.Idx;
			entity.MarkId = this.MarkId;
			entity.Schedule = this.Schedule;
			entity.NpcId = this.NpcId;
			entity.MarkPlayerId = this.MarkPlayerId;
			entity.MarkPlayer = this.MarkPlayer;
			entity.Describe = this.Describe;
			entity.TeamName = this.TeamName;
			entity.OpponentTeamName = this.OpponentTeamName;
			entity.Formation = this.Formation;
			entity.OpponentFormation = this.OpponentFormation;
			entity.PassPrizeItem = this.PassPrizeItem;
			entity.FirstPassItem = this.FirstPassItem;
			entity.CourageNumber = this.CourageNumber;
			entity.Story = this.Story;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Revelation 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationResponse : BaseResponse<ConfigRevelationEntity>
    {

    }
}
