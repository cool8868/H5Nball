
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Ranking_Yesterday 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RankingYesterdayEntity
	{
		
		public RankingYesterdayEntity()
		{
		}

		public RankingYesterdayEntity(
		System.Int32 idx
,				System.Int32 ranktype
,				System.Guid managerid
,				System.Int32 rank
		)
		{
			this.Idx = idx;
			this.RankType = ranktype;
			this.ManagerId = managerid;
			this.Rank = rank;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///排名类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 RankType {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///Rank
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Rank {get ; set ;}
		#endregion
        
        #region Clone
        public RankingYesterdayEntity Clone()
        {
            RankingYesterdayEntity entity = new RankingYesterdayEntity();
			entity.Idx = this.Idx;
			entity.RankType = this.RankType;
			entity.ManagerId = this.ManagerId;
			entity.Rank = this.Rank;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Ranking_Yesterday 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RankingYesterdayResponse : BaseResponse<RankingYesterdayEntity>
    {

    }
}

