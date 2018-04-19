
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Arena_ExChangeRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ArenaExchangerecordEntity
	{
		
		public ArenaExchangerecordEntity()
		{
		}

		public ArenaExchangerecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 exitemcode
,				System.Int32 arenacoin
,				System.Int32 exitemcount
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ExItemCode = exitemcode;
			this.ArenaCoin = arenacoin;
			this.ExItemCount = exitemcount;
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
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///兑换物品code
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ExItemCode {get ; set ;}

		///<summary>
		///消耗竞技币
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ArenaCoin {get ; set ;}

		///<summary>
		///兑换物品数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ExItemCount {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ArenaExchangerecordEntity Clone()
        {
            ArenaExchangerecordEntity entity = new ArenaExchangerecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ExItemCode = this.ExItemCode;
			entity.ArenaCoin = this.ArenaCoin;
			entity.ExItemCount = this.ExItemCount;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Arena_ExChangeRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ArenaExchangerecordResponse : BaseResponse<ArenaExchangerecordEntity>
    {

    }
}
