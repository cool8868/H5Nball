
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.League_NpcManager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LeagueNpcmanagerEntity
	{
		
		public LeagueNpcmanagerEntity()
		{
		}

		public LeagueNpcmanagerEntity(
		System.Int32 idx
,				System.Guid laeguerecordid
,				System.Guid npcid
,				System.String npcname
,				System.Int32 score
,				System.Int32 matchnumber
,				System.Int32 winnumber
,				System.Int32 flatnumber
,				System.Int32 losenumber
,				System.Int32 goalsnumber
,				System.Int32 fumblenumber
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.LaegueRecordId = laeguerecordid;
			this.NpcId = npcid;
			this.NpcName = npcname;
			this.Score = score;
			this.MatchNumber = matchnumber;
			this.WinNumber = winnumber;
			this.FlatNumber = flatnumber;
			this.LoseNumber = losenumber;
			this.GoalsNumber = goalsnumber;
			this.FumbleNumber = fumblenumber;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///联赛记录ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid LaegueRecordId {get ; set ;}

		///<summary>
		///NPCId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid NpcId {get ; set ;}

		///<summary>
		///NPC经理名
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String NpcName {get ; set ;}

		///<summary>
		///获得的积分
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///比赛次数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 MatchNumber {get ; set ;}

		///<summary>
		///胜利次数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 WinNumber {get ; set ;}

		///<summary>
		///打平次数
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 FlatNumber {get ; set ;}

		///<summary>
		///打输次数
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 LoseNumber {get ; set ;}

		///<summary>
		///总进球数
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 GoalsNumber {get ; set ;}

		///<summary>
		///总失球数
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 FumbleNumber {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public LeagueNpcmanagerEntity Clone()
        {
            LeagueNpcmanagerEntity entity = new LeagueNpcmanagerEntity();
			entity.Idx = this.Idx;
			entity.LaegueRecordId = this.LaegueRecordId;
			entity.NpcId = this.NpcId;
			entity.NpcName = this.NpcName;
			entity.Score = this.Score;
			entity.MatchNumber = this.MatchNumber;
			entity.WinNumber = this.WinNumber;
			entity.FlatNumber = this.FlatNumber;
			entity.LoseNumber = this.LoseNumber;
			entity.GoalsNumber = this.GoalsNumber;
			entity.FumbleNumber = this.FumbleNumber;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.League_NpcManager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LeagueNpcmanagerResponse : BaseResponse<LeagueNpcmanagerEntity>
    {

    }
}

