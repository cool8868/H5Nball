
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Pay_ConsumeHistory 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PayConsumehistoryEntity
	{
		
		public PayConsumehistoryEntity()
		{
		}

		public PayConsumehistoryEntity(
		System.Guid idx
,				System.String account
,				System.Guid managerid
,				System.Int32 point
,				System.Int32 bonus
,				System.Int32 sourcetype
,				System.String sourceid
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.Account = account;
			this.ManagerId = managerid;
			this.Point = point;
			this.Bonus = bonus;
			this.SourceType = sourcetype;
			this.SourceId = sourceid;
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
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Account {get ; set ;}

		///<summary>
		///经理id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///消耗点数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Point {get ; set ;}

		///<summary>
		///消耗赠送点数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Bonus {get ; set ;}

		///<summary>
		///消费来源类型,1:商城；2:联赛竞猜
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 SourceType {get ; set ;}

		///<summary>
		///订单id
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String SourceId {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public PayConsumehistoryEntity Clone()
        {
            PayConsumehistoryEntity entity = new PayConsumehistoryEntity();
			entity.Idx = this.Idx;
			entity.Account = this.Account;
			entity.ManagerId = this.ManagerId;
			entity.Point = this.Point;
			entity.Bonus = this.Bonus;
			entity.SourceType = this.SourceType;
			entity.SourceId = this.SourceId;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Pay_ConsumeHistory 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PayConsumehistoryResponse : BaseResponse<PayConsumehistoryEntity>
    {

    }
}
