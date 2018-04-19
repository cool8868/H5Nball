
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_LeagueMark 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLeaguemarkEntity
	{
		
		public ConfigLeaguemarkEntity()
		{
		}

		public ConfigLeaguemarkEntity(
		System.Guid idx
,				System.Int32 teamid
,				System.String teamname
,				System.Int32 leagueid
,				System.Int32 buff
,				System.Int32 managerlevel
,				System.Int32 formation
,				System.Int32 playerlevel
,				System.Int32 playercardlevel
,				System.Int32 coach
,				System.String talent
,				System.Int32 player1id
,				System.Int32 equipment1id
,				System.String skill1id
,				System.Int32 player2id
,				System.Int32 equipment2id
,				System.String skill2id
,				System.Int32 player3id
,				System.Int32 equipment3id
,				System.String skill3id
,				System.Int32 player4id
,				System.Int32 equipment4id
,				System.String skill4id
,				System.Int32 player5id
,				System.Int32 equipment5id
,				System.String skill5id
,				System.Int32 player6id
,				System.Int32 equipment6id
,				System.String skill6id
,				System.Int32 player7id
,				System.Int32 equipment7id
,				System.String skill7id
		)
		{
			this.Idx = idx;
			this.TeamId = teamid;
			this.TeamName = teamname;
			this.LeagueId = leagueid;
			this.Buff = buff;
			this.ManagerLevel = managerlevel;
			this.Formation = formation;
			this.PlayerLevel = playerlevel;
			this.PlayerCardLevel = playercardlevel;
			this.Coach = coach;
			this.Talent = talent;
			this.Player1Id = player1id;
			this.Equipment1Id = equipment1id;
			this.Skill1Id = skill1id;
			this.Player2Id = player2id;
			this.Equipment2Id = equipment2id;
			this.Skill2Id = skill2id;
			this.Player3Id = player3id;
			this.Equipment3Id = equipment3id;
			this.Skill3Id = skill3id;
			this.Player4Id = player4id;
			this.Equipment4Id = equipment4id;
			this.Skill4Id = skill4id;
			this.Player5Id = player5id;
			this.Equipment5Id = equipment5id;
			this.Skill5Id = skill5id;
			this.Player6Id = player6id;
			this.Equipment6Id = equipment6id;
			this.Skill6Id = skill6id;
			this.Player7Id = player7id;
			this.Equipment7Id = equipment7id;
			this.Skill7Id = skill7id;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///球队ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TeamId {get ; set ;}

		///<summary>
		///球队名
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String TeamName {get ; set ;}

		///<summary>
		///联赛ID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 LeagueId {get ; set ;}

		///<summary>
		///Buff
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Buff {get ; set ;}

		///<summary>
		///经理开放等级
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ManagerLevel {get ; set ;}

		///<summary>
		///阵形
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Formation {get ; set ;}

		///<summary>
		///球员等级
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PlayerLevel {get ; set ;}

		///<summary>
		///球员卡等级
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 PlayerCardLevel {get ; set ;}

		///<summary>
		///教练
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Coach {get ; set ;}

		///<summary>
		///天赋
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String Talent {get ; set ;}

		///<summary>
		///球员ID1
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Player1Id {get ; set ;}

		///<summary>
		///球员1的装备Code
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Equipment1Id {get ; set ;}

		///<summary>
		///球员1的技能
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String Skill1Id {get ; set ;}

		///<summary>
		///球员2
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Player2Id {get ; set ;}

		///<summary>
		///Equipment2Id
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Equipment2Id {get ; set ;}

		///<summary>
		///Skill2Id
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String Skill2Id {get ; set ;}

		///<summary>
		///Player3Id
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 Player3Id {get ; set ;}

		///<summary>
		///Equipment3Id
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 Equipment3Id {get ; set ;}

		///<summary>
		///Skill3Id
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.String Skill3Id {get ; set ;}

		///<summary>
		///Player4Id
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 Player4Id {get ; set ;}

		///<summary>
		///Equipment4Id
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 Equipment4Id {get ; set ;}

		///<summary>
		///Skill4Id
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.String Skill4Id {get ; set ;}

		///<summary>
		///Player5Id
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 Player5Id {get ; set ;}

		///<summary>
		///Equipment5Id
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int32 Equipment5Id {get ; set ;}

		///<summary>
		///Skill5Id
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.String Skill5Id {get ; set ;}

		///<summary>
		///Player6Id
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Int32 Player6Id {get ; set ;}

		///<summary>
		///Equipment6Id
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Int32 Equipment6Id {get ; set ;}

		///<summary>
		///Skill6Id
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.String Skill6Id {get ; set ;}

		///<summary>
		///Player7Id
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.Int32 Player7Id {get ; set ;}

		///<summary>
		///Equipment7Id
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.Int32 Equipment7Id {get ; set ;}

		///<summary>
		///Skill7Id
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.String Skill7Id {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLeaguemarkEntity Clone()
        {
            ConfigLeaguemarkEntity entity = new ConfigLeaguemarkEntity();
			entity.Idx = this.Idx;
			entity.TeamId = this.TeamId;
			entity.TeamName = this.TeamName;
			entity.LeagueId = this.LeagueId;
			entity.Buff = this.Buff;
			entity.ManagerLevel = this.ManagerLevel;
			entity.Formation = this.Formation;
			entity.PlayerLevel = this.PlayerLevel;
			entity.PlayerCardLevel = this.PlayerCardLevel;
			entity.Coach = this.Coach;
			entity.Talent = this.Talent;
			entity.Player1Id = this.Player1Id;
			entity.Equipment1Id = this.Equipment1Id;
			entity.Skill1Id = this.Skill1Id;
			entity.Player2Id = this.Player2Id;
			entity.Equipment2Id = this.Equipment2Id;
			entity.Skill2Id = this.Skill2Id;
			entity.Player3Id = this.Player3Id;
			entity.Equipment3Id = this.Equipment3Id;
			entity.Skill3Id = this.Skill3Id;
			entity.Player4Id = this.Player4Id;
			entity.Equipment4Id = this.Equipment4Id;
			entity.Skill4Id = this.Skill4Id;
			entity.Player5Id = this.Player5Id;
			entity.Equipment5Id = this.Equipment5Id;
			entity.Skill5Id = this.Skill5Id;
			entity.Player6Id = this.Player6Id;
			entity.Equipment6Id = this.Equipment6Id;
			entity.Skill6Id = this.Skill6Id;
			entity.Player7Id = this.Player7Id;
			entity.Equipment7Id = this.Equipment7Id;
			entity.Skill7Id = this.Skill7Id;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_LeagueMark 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLeaguemarkResponse : BaseResponse<ConfigLeaguemarkEntity>
    {

    }
}

