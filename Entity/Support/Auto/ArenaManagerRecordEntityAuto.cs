
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Arena_ManagerRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ArenaManagerrecordEntity
	{
		
		public ArenaManagerrecordEntity()
		{
		}

		public ArenaManagerrecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.String managername
,				System.String siteid
,				System.String zonename
,				System.Int32 integral
,				System.Int32 dangrading
,				System.Int32 arenatype
,				System.Int32 seasonid
,				System.Int32 rank
,				System.Boolean isprize
,				System.Int32 prizeid
,				System.DateTime prizetime
,				System.DateTime rowtime
,				System.Int32 domainid
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ManagerName = managername;
			this.SiteId = siteid;
			this.ZoneName = zonename;
			this.Integral = integral;
			this.DanGrading = dangrading;
			this.ArenaType = arenatype;
			this.SeasonId = seasonid;
			this.Rank = rank;
			this.IsPrize = isprize;
			this.PrizeId = prizeid;
			this.PrizeTime = prizetime;
			this.RowTime = rowtime;
			this.DomainId = domainid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///ManagerName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///ZoneName
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ZoneName {get ; set ;}

		///<summary>
		///Integral
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Integral {get ; set ;}

		///<summary>
		///DanGrading
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 DanGrading {get ; set ;}

		///<summary>
		///ArenaType
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ArenaType {get ; set ;}

		///<summary>
		///SeasonId
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 SeasonId {get ; set ;}

		///<summary>
		///Rank
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///IsPrize
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Boolean IsPrize {get ; set ;}

		///<summary>
		///PrizeId
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 PrizeId {get ; set ;}

		///<summary>
		///PrizeTime
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime PrizeTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///DomainId
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 DomainId {get ; set ;}
		#endregion
        
        #region Clone
        public ArenaManagerrecordEntity Clone()
        {
            ArenaManagerrecordEntity entity = new ArenaManagerrecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ManagerName = this.ManagerName;
			entity.SiteId = this.SiteId;
			entity.ZoneName = this.ZoneName;
			entity.Integral = this.Integral;
			entity.DanGrading = this.DanGrading;
			entity.ArenaType = this.ArenaType;
			entity.SeasonId = this.SeasonId;
			entity.Rank = this.Rank;
			entity.IsPrize = this.IsPrize;
			entity.PrizeId = this.PrizeId;
			entity.PrizeTime = this.PrizeTime;
			entity.RowTime = this.RowTime;
			entity.DomainId = this.DomainId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Arena_ManagerRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ArenaManagerrecordResponse : BaseResponse<ArenaManagerrecordEntity>
    {

    }
}
