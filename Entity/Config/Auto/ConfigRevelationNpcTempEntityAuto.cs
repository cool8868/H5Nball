
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationNpcTemp 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationnpctempEntity
	{
		
		public ConfigRevelationnpctempEntity()
		{
		}

		public ConfigRevelationnpctempEntity(
		System.Int32 idx
,				System.Int32 markid
,				System.Int32 schedule
,				System.String opponentteamname
,				System.Int32 formationid
,				System.Int32 playerlevel
,				System.Int32 playercardstrength
,				System.Int32 buff
,				System.Int32 p1
,				System.Int32 p2
,				System.Int32 p3
,				System.Int32 p4
,				System.Int32 p5
,				System.Int32 p6
,				System.Int32 p7
		)
		{
			this.Idx = idx;
			this.MarkId = markid;
			this.Schedule = schedule;
			this.OpponentTeamName = opponentteamname;
			this.FormationID = formationid;
			this.PlayerLevel = playerlevel;
			this.PlayerCardStrength = playercardstrength;
			this.Buff = buff;
			this.P1 = p1;
			this.P2 = p2;
			this.P3 = p3;
			this.P4 = p4;
			this.P5 = p5;
			this.P6 = p6;
			this.P7 = p7;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///MarkId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 MarkId {get ; set ;}

		///<summary>
		///Schedule
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///OpponentTeamName
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String OpponentTeamName {get ; set ;}

		///<summary>
		///FormationID
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 FormationID {get ; set ;}

		///<summary>
		///PlayerLevel
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PlayerLevel {get ; set ;}

		///<summary>
		///PlayerCardStrength
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 PlayerCardStrength {get ; set ;}

		///<summary>
		///Buff
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Buff {get ; set ;}

		///<summary>
		///P1
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 P1 {get ; set ;}

		///<summary>
		///P2
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 P2 {get ; set ;}

		///<summary>
		///P3
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 P3 {get ; set ;}

		///<summary>
		///P4
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 P4 {get ; set ;}

		///<summary>
		///P5
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 P5 {get ; set ;}

		///<summary>
		///P6
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 P6 {get ; set ;}

		///<summary>
		///P7
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 P7 {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationnpctempEntity Clone()
        {
            ConfigRevelationnpctempEntity entity = new ConfigRevelationnpctempEntity();
			entity.Idx = this.Idx;
			entity.MarkId = this.MarkId;
			entity.Schedule = this.Schedule;
			entity.OpponentTeamName = this.OpponentTeamName;
			entity.FormationID = this.FormationID;
			entity.PlayerLevel = this.PlayerLevel;
			entity.PlayerCardStrength = this.PlayerCardStrength;
			entity.Buff = this.Buff;
			entity.P1 = this.P1;
			entity.P2 = this.P2;
			entity.P3 = this.P3;
			entity.P4 = this.P4;
			entity.P5 = this.P5;
			entity.P6 = this.P6;
			entity.P7 = this.P7;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationNpcTemp 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationnpctempResponse : BaseResponse<ConfigRevelationnpctempEntity>
    {

    }
}
