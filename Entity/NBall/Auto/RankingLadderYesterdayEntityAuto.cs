
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Ranking_LadderYesterday 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RankingLadderyesterdayEntity
	{
		
		public RankingLadderyesterdayEntity()
		{
		}

		public RankingLadderyesterdayEntity(
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
		///RankType
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
        public RankingLadderyesterdayEntity Clone()
        {
            RankingLadderyesterdayEntity entity = new RankingLadderyesterdayEntity();
			entity.Idx = this.Idx;
			entity.RankType = this.RankType;
			entity.ManagerId = this.ManagerId;
			entity.Rank = this.Rank;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Ranking_LadderYesterday 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RankingLadderyesterdayResponse : BaseResponse<RankingLadderyesterdayEntity>
    {

    }
}

