
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossCrowd_PairRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrosscrowdPairrecordEntity
	{
		
		public CrosscrowdPairrecordEntity()
		{
		}

		public CrosscrowdPairrecordEntity(
		System.Int32 idx
,				System.Int32 crosscrowdid
,				System.Int32 pairindex
,				System.Int32 playercount
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.CrossCrowdId = crosscrowdid;
			this.PairIndex = pairindex;
			this.PlayerCount = playercount;
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
		///CrossCrowdId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CrossCrowdId {get ; set ;}

		///<summary>
		///PairIndex
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PairIndex {get ; set ;}

		///<summary>
		///PlayerCount
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PlayerCount {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrosscrowdPairrecordEntity Clone()
        {
            CrosscrowdPairrecordEntity entity = new CrosscrowdPairrecordEntity();
			entity.Idx = this.Idx;
			entity.CrossCrowdId = this.CrossCrowdId;
			entity.PairIndex = this.PairIndex;
			entity.PlayerCount = this.PlayerCount;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossCrowd_PairRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrosscrowdPairrecordResponse : BaseResponse<CrosscrowdPairrecordEntity>
    {

    }
}
