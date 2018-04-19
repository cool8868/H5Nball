
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Arena_SeasonInfo 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ArenaSeasoninfoEntity
	{
		
		public ArenaSeasoninfoEntity()
		{
		}

		public ArenaSeasoninfoEntity(
		System.Int32 idx
,				System.DateTime preparetime
,				System.DateTime starttime
,				System.DateTime endtime
,				System.Int32 arenatype
,				System.Int32 status
,				System.Boolean isprize
,				System.DateTime prizetime
,				System.Guid onchampionid
,				System.String onchampionname
,				System.String onchampionzonename
,				System.Guid thekingid
,				System.String thekingname
,				System.String thekingzonename
,				System.Int32 thekingchampionnumber
,				System.DateTime updatetime
,				System.DateTime rowtime
,				System.Int32 domainid
,				System.Int32 seasonid
		)
		{
			this.Idx = idx;
			this.PrepareTime = preparetime;
			this.StartTime = starttime;
			this.EndTime = endtime;
			this.ArenaType = arenatype;
			this.Status = status;
			this.IsPrize = isprize;
			this.PrizeTime = prizetime;
			this.OnChampionId = onchampionid;
			this.OnChampionName = onchampionname;
			this.OnChampionZoneName = onchampionzonename;
			this.TheKingId = thekingid;
			this.TheKingName = thekingname;
			this.TheKingZoneName = thekingzonename;
			this.TheKingChampionNumber = thekingchampionnumber;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
			this.DomainId = domainid;
			this.SeasonId = seasonid;
		}
		
		#region Public Properties
		
		///<summary>
		///赛季ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///PrepareTime
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.DateTime PrepareTime {get ; set ;}

		///<summary>
		///StartTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///EndTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///ArenaType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ArenaType {get ; set ;}

		///<summary>
		///竞技场状态  0=准备中 1=比赛中 2=结束
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///IsPrize
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsPrize {get ; set ;}

		///<summary>
		///PrizeTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime PrizeTime {get ; set ;}

		///<summary>
		///上届冠军ID
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Guid OnChampionId {get ; set ;}

		///<summary>
		///上届冠军名字
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String OnChampionName {get ; set ;}

		///<summary>
		///上届冠军所在区
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String OnChampionZoneName {get ; set ;}

		///<summary>
		///王者之师经理ID
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Guid TheKingId {get ; set ;}

		///<summary>
		///王者之师名字
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String TheKingName {get ; set ;}

		///<summary>
		///王者之师所在区
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String TheKingZoneName {get ; set ;}

		///<summary>
		///王者之师冠军次数
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 TheKingChampionNumber {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(17)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///DomainId
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 DomainId {get ; set ;}

		///<summary>
		///SeasonId
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 SeasonId {get ; set ;}
		#endregion
        
        #region Clone
        public ArenaSeasoninfoEntity Clone()
        {
            ArenaSeasoninfoEntity entity = new ArenaSeasoninfoEntity();
			entity.Idx = this.Idx;
			entity.PrepareTime = this.PrepareTime;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.ArenaType = this.ArenaType;
			entity.Status = this.Status;
			entity.IsPrize = this.IsPrize;
			entity.PrizeTime = this.PrizeTime;
			entity.OnChampionId = this.OnChampionId;
			entity.OnChampionName = this.OnChampionName;
			entity.OnChampionZoneName = this.OnChampionZoneName;
			entity.TheKingId = this.TheKingId;
			entity.TheKingName = this.TheKingName;
			entity.TheKingZoneName = this.TheKingZoneName;
			entity.TheKingChampionNumber = this.TheKingChampionNumber;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
			entity.DomainId = this.DomainId;
			entity.SeasonId = this.SeasonId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Arena_SeasonInfo 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ArenaSeasoninfoResponse : BaseResponse<ArenaSeasoninfoEntity>
    {

    }
}
