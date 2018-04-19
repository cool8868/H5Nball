
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_NPC 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicNpcEntity
	{
		
		public DicNpcEntity()
		{
		}

		public DicNpcEntity(
		System.Guid idx
,				System.Int32 type
,				System.String name
,				System.Int32 logo
,				System.Int32 formationid
,				System.Int32 formationlevel
,				System.Int32 teammemberlevel
,				System.Int32 playercardstrength
,				System.Int32 coachid
,				System.String dotalent
,				System.String dowill
,				System.String managerskill
,				System.Int32 comblevel
,				System.Int32 buff
,				System.Int32 propertypoint
,				System.Int32 tp1
,				System.Int32 te1
,				System.String ts1
,				System.Int32 tp2
,				System.Int32 te2
,				System.String ts2
,				System.Int32 tp3
,				System.Int32 te3
,				System.String ts3
,				System.Int32 tp4
,				System.Int32 te4
,				System.String ts4
,				System.Int32 tp5
,				System.Int32 te5
,				System.String ts5
,				System.Int32 tp6
,				System.Int32 te6
,				System.String ts6
,				System.Int32 tp7
,				System.Int32 te7
,				System.String ts7
		)
		{
			this.Idx = idx;
			this.Type = type;
			this.Name = name;
			this.Logo = logo;
			this.FormationId = formationid;
			this.FormationLevel = formationlevel;
			this.TeammemberLevel = teammemberlevel;
			this.PlayerCardStrength = playercardstrength;
			this.CoachId = coachid;
			this.DoTalent = dotalent;
			this.DoWill = dowill;
			this.ManagerSkill = managerskill;
			this.CombLevel = comblevel;
			this.Buff = buff;
			this.PropertyPoint = propertypoint;
			this.TP1 = tp1;
			this.TE1 = te1;
			this.TS1 = ts1;
			this.TP2 = tp2;
			this.TE2 = te2;
			this.TS2 = ts2;
			this.TP3 = tp3;
			this.TE3 = te3;
			this.TS3 = ts3;
			this.TP4 = tp4;
			this.TE4 = te4;
			this.TS4 = ts4;
			this.TP5 = tp5;
			this.TE5 = te5;
			this.TS5 = ts5;
			this.TP6 = tp6;
			this.TE6 = te6;
			this.TS6 = ts6;
			this.TP7 = tp7;
			this.TE7 = te7;
			this.TS7 = ts7;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///npc类型：1,巡回赛;2,挑战赛
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///npc名字
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///Logo
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Logo {get ; set ;}

		///<summary>
		///FormationId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 FormationId {get ; set ;}

		///<summary>
		///FormationLevel
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 FormationLevel {get ; set ;}

		///<summary>
		///TeammemberLevel
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 TeammemberLevel {get ; set ;}

		///<summary>
		///PlayerCardStrength
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PlayerCardStrength {get ; set ;}

		///<summary>
		///CoachId
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 CoachId {get ; set ;}

		///<summary>
		///DoTalent
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String DoTalent {get ; set ;}

		///<summary>
		///DoWill
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String DoWill {get ; set ;}

		///<summary>
		///ManagerSkill
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String ManagerSkill {get ; set ;}

		///<summary>
		///CombLevel
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 CombLevel {get ; set ;}

		///<summary>
		///Buff
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 Buff {get ; set ;}

		///<summary>
		///PropertyPoint
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 PropertyPoint {get ; set ;}

		///<summary>
		///TP1
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 TP1 {get ; set ;}

		///<summary>
		///TE1
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 TE1 {get ; set ;}

		///<summary>
		///TS1
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.String TS1 {get ; set ;}

		///<summary>
		///TP2
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 TP2 {get ; set ;}

		///<summary>
		///TE2
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 TE2 {get ; set ;}

		///<summary>
		///TS2
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.String TS2 {get ; set ;}

		///<summary>
		///TP3
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 TP3 {get ; set ;}

		///<summary>
		///TE3
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 TE3 {get ; set ;}

		///<summary>
		///TS3
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.String TS3 {get ; set ;}

		///<summary>
		///TP4
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int32 TP4 {get ; set ;}

		///<summary>
		///TE4
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Int32 TE4 {get ; set ;}

		///<summary>
		///TS4
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.String TS4 {get ; set ;}

		///<summary>
		///TP5
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Int32 TP5 {get ; set ;}

		///<summary>
		///TE5
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Int32 TE5 {get ; set ;}

		///<summary>
		///TS5
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.String TS5 {get ; set ;}

		///<summary>
		///TP6
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.Int32 TP6 {get ; set ;}

		///<summary>
		///TE6
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.Int32 TE6 {get ; set ;}

		///<summary>
		///TS6
		///</summary>
        [DataMember]
        [ProtoMember(33)]
		public System.String TS6 {get ; set ;}

		///<summary>
		///TP7
		///</summary>
        [DataMember]
        [ProtoMember(34)]
		public System.Int32 TP7 {get ; set ;}

		///<summary>
		///TE7
		///</summary>
        [DataMember]
        [ProtoMember(35)]
		public System.Int32 TE7 {get ; set ;}

		///<summary>
		///TS7
		///</summary>
        [DataMember]
        [ProtoMember(36)]
		public System.String TS7 {get ; set ;}
		#endregion
        
        #region Clone
        public DicNpcEntity Clone()
        {
            DicNpcEntity entity = new DicNpcEntity();
			entity.Idx = this.Idx;
			entity.Type = this.Type;
			entity.Name = this.Name;
			entity.Logo = this.Logo;
			entity.FormationId = this.FormationId;
			entity.FormationLevel = this.FormationLevel;
			entity.TeammemberLevel = this.TeammemberLevel;
			entity.PlayerCardStrength = this.PlayerCardStrength;
			entity.CoachId = this.CoachId;
			entity.DoTalent = this.DoTalent;
			entity.DoWill = this.DoWill;
			entity.ManagerSkill = this.ManagerSkill;
			entity.CombLevel = this.CombLevel;
			entity.Buff = this.Buff;
			entity.PropertyPoint = this.PropertyPoint;
			entity.TP1 = this.TP1;
			entity.TE1 = this.TE1;
			entity.TS1 = this.TS1;
			entity.TP2 = this.TP2;
			entity.TE2 = this.TE2;
			entity.TS2 = this.TS2;
			entity.TP3 = this.TP3;
			entity.TE3 = this.TE3;
			entity.TS3 = this.TS3;
			entity.TP4 = this.TP4;
			entity.TE4 = this.TE4;
			entity.TS4 = this.TS4;
			entity.TP5 = this.TP5;
			entity.TE5 = this.TE5;
			entity.TS5 = this.TS5;
			entity.TP6 = this.TP6;
			entity.TE6 = this.TE6;
			entity.TS6 = this.TS6;
			entity.TP7 = this.TP7;
			entity.TE7 = this.TE7;
			entity.TS7 = this.TS7;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_NPC 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicNpcResponse : BaseResponse<DicNpcEntity>
    {

    }
}

