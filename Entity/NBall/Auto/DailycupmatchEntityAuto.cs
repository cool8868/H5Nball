
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.DailyCup_Match 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DailycupMatchEntity
	{
		
		public DailycupMatchEntity()
		{
		}

		public DailycupMatchEntity(
		System.Guid idx
,				System.Int32 dailycupid
,				System.Guid homemanager
,				System.Guid awaymanager
,				System.String homename
,				System.String awayname
,				System.String homelogo
,				System.String awaylogo
,				System.Int32 homelevel
,				System.Int32 awaylevel
,				System.Int32 homepower
,				System.Int32 awaypower
,				System.Int32 homeworldscore
,				System.Int32 awayworldscore
,				System.Int32 homescore
,				System.Int32 awayscore
,				System.Int32 round
,				System.Int32 chipincount
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.DailyCupId = dailycupid;
			this.HomeManager = homemanager;
			this.AwayManager = awaymanager;
			this.HomeName = homename;
			this.AwayName = awayname;
			this.HomeLogo = homelogo;
			this.AwayLogo = awaylogo;
			this.HomeLevel = homelevel;
			this.AwayLevel = awaylevel;
			this.HomePower = homepower;
			this.AwayPower = awaypower;
			this.HomeWorldScore = homeworldscore;
			this.AwayWorldScore = awayworldscore;
			this.HomeScore = homescore;
			this.AwayScore = awayscore;
			this.Round = round;
			this.ChipInCount = chipincount;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///杯赛ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DailyCupId {get ; set ;}

		///<summary>
		///主队经理的GUID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid HomeManager {get ; set ;}

		///<summary>
		///客队经理的GUID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid AwayManager {get ; set ;}

		///<summary>
		///主队经理名
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String HomeName {get ; set ;}

		///<summary>
		///客队经理名
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String AwayName {get ; set ;}

		///<summary>
		///HomeLogo
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String HomeLogo {get ; set ;}

		///<summary>
		///AwayLogo
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String AwayLogo {get ; set ;}

		///<summary>
		///主队等级
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 HomeLevel {get ; set ;}

		///<summary>
		///客队等级
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 AwayLevel {get ; set ;}

		///<summary>
		///主队综合实力
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 HomePower {get ; set ;}

		///<summary>
		///客队综合实力
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 AwayPower {get ; set ;}

		///<summary>
		///主队世界杯积分
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 HomeWorldScore {get ; set ;}

		///<summary>
		///客队世界杯积分
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 AwayWorldScore {get ; set ;}

		///<summary>
		///主队比分
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 HomeScore {get ; set ;}

		///<summary>
		///客队比分
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 AwayScore {get ; set ;}

		///<summary>
		///轮次
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 Round {get ; set ;}

		///<summary>
		///投注数量
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 ChipInCount {get ; set ;}

		///<summary>
		///状态:0,正常；
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///创建时间
		///</summary>
        [DataMember]
        [ProtoMember(20)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DailycupMatchEntity Clone()
        {
            DailycupMatchEntity entity = new DailycupMatchEntity();
			entity.Idx = this.Idx;
			entity.DailyCupId = this.DailyCupId;
			entity.HomeManager = this.HomeManager;
			entity.AwayManager = this.AwayManager;
			entity.HomeName = this.HomeName;
			entity.AwayName = this.AwayName;
			entity.HomeLogo = this.HomeLogo;
			entity.AwayLogo = this.AwayLogo;
			entity.HomeLevel = this.HomeLevel;
			entity.AwayLevel = this.AwayLevel;
			entity.HomePower = this.HomePower;
			entity.AwayPower = this.AwayPower;
			entity.HomeWorldScore = this.HomeWorldScore;
			entity.AwayWorldScore = this.AwayWorldScore;
			entity.HomeScore = this.HomeScore;
			entity.AwayScore = this.AwayScore;
			entity.Round = this.Round;
			entity.ChipInCount = this.ChipInCount;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.DailyCup_Match 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DailycupMatchResponse : BaseResponse<DailycupMatchEntity>
    {

    }
}

