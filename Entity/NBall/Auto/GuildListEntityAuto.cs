
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Guild_List 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GuildListEntity
	{
		
		public GuildListEntity()
		{
		}

		public GuildListEntity(
		System.Int32 guildno
,				System.Guid guildid
,				System.String guildname
,				System.String icon
,				System.String logo
,				System.String intro
,				System.String note
,				System.Int32 guildlevel
,				System.Int32 guildactive
,				System.Int32 guildactivecost
,				System.Int32 cntmembers
,				System.Int32 maxmembers
,				System.Guid creatorid
,				System.String creatorname
,				System.DateTime createtime
,				System.Guid leaderid
,				System.String leadername
,				System.DateTime leadtime
,				System.Int32 leadtrack
,				System.Int32 gkpi
,				System.Int32 grank
,				System.Int32 lockflag
,				System.DateTime locktime
,				System.Int32 invalidflag
,				System.DateTime rowtimeup
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.GuildNo = guildno;
			this.GuildId = guildid;
			this.GuildName = guildname;
			this.Icon = icon;
			this.Logo = logo;
			this.Intro = intro;
			this.Note = note;
			this.GuildLevel = guildlevel;
			this.GuildActive = guildactive;
			this.GuildActiveCost = guildactivecost;
			this.CntMembers = cntmembers;
			this.MaxMembers = maxmembers;
			this.CreatorId = creatorid;
			this.CreatorName = creatorname;
			this.CreateTime = createtime;
			this.LeaderId = leaderid;
			this.LeaderName = leadername;
			this.LeadTime = leadtime;
			this.LeadTrack = leadtrack;
			this.GKpi = gkpi;
			this.GRank = grank;
			this.LockFlag = lockflag;
			this.LockTime = locktime;
			this.InvalidFlag = invalidflag;
			this.RowTimeUp = rowtimeup;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///GuildNo
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 GuildNo {get ; set ;}

		///<summary>
		///GuildId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid GuildId {get ; set ;}

		///<summary>
		///GuildName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String GuildName {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Icon {get ; set ;}

		///<summary>
		///Logo
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Logo {get ; set ;}

		///<summary>
		///Intro
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Intro {get ; set ;}

		///<summary>
		///Note
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String Note {get ; set ;}

		///<summary>
		///GuildLevel
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 GuildLevel {get ; set ;}

		///<summary>
		///GuildActive
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 GuildActive {get ; set ;}

		///<summary>
		///GuildActiveCost
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 GuildActiveCost {get ; set ;}

		///<summary>
		///CntMembers
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 CntMembers {get ; set ;}

		///<summary>
		///MaxMembers
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 MaxMembers {get ; set ;}

		///<summary>
		///CreatorId
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Guid CreatorId {get ; set ;}

		///<summary>
		///CreatorName
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String CreatorName {get ; set ;}

		///<summary>
		///CreateTime
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime CreateTime {get ; set ;}

		///<summary>
		///LeaderId
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Guid LeaderId {get ; set ;}

		///<summary>
		///LeaderName
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String LeaderName {get ; set ;}

		///<summary>
		///LeadTime
		///</summary>
        [DataMember]
        [ProtoMember(18)]
        [JsonIgnore]
		public System.DateTime LeadTime {get ; set ;}

		///<summary>
		///LeadTrack
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 LeadTrack {get ; set ;}

		///<summary>
		///GKpi
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 GKpi {get ; set ;}

		///<summary>
		///GRank
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 GRank {get ; set ;}

		///<summary>
		///LockFlag
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 LockFlag {get ; set ;}

		///<summary>
		///LockTime
		///</summary>
        [DataMember]
        [ProtoMember(23)]
        [JsonIgnore]
		public System.DateTime LockTime {get ; set ;}

		///<summary>
		///InvalidFlag
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 InvalidFlag {get ; set ;}

		///<summary>
		///RowTimeUp
		///</summary>
        [DataMember]
        [ProtoMember(25)]
        [JsonIgnore]
		public System.DateTime RowTimeUp {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(26)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(27)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public GuildListEntity Clone()
        {
            GuildListEntity entity = new GuildListEntity();
			entity.GuildNo = this.GuildNo;
			entity.GuildId = this.GuildId;
			entity.GuildName = this.GuildName;
			entity.Icon = this.Icon;
			entity.Logo = this.Logo;
			entity.Intro = this.Intro;
			entity.Note = this.Note;
			entity.GuildLevel = this.GuildLevel;
			entity.GuildActive = this.GuildActive;
			entity.GuildActiveCost = this.GuildActiveCost;
			entity.CntMembers = this.CntMembers;
			entity.MaxMembers = this.MaxMembers;
			entity.CreatorId = this.CreatorId;
			entity.CreatorName = this.CreatorName;
			entity.CreateTime = this.CreateTime;
			entity.LeaderId = this.LeaderId;
			entity.LeaderName = this.LeaderName;
			entity.LeadTime = this.LeadTime;
			entity.LeadTrack = this.LeadTrack;
			entity.GKpi = this.GKpi;
			entity.GRank = this.GRank;
			entity.LockFlag = this.LockFlag;
			entity.LockTime = this.LockTime;
			entity.InvalidFlag = this.InvalidFlag;
			entity.RowTimeUp = this.RowTimeUp;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Guild_List 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GuildListResponse : BaseResponse<GuildListEntity>
    {

    }
}
