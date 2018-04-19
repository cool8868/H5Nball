
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Pay_ChargeHistory 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PayChargehistoryEntity
	{
		
		public PayChargehistoryEntity()
		{
		}

		public PayChargehistoryEntity(
		System.String idx
,				System.String account
,				System.Int32 sourcetype
,				System.String billingid
,				System.Int32 point
,				System.Int32 bonus
,				System.Decimal cash
,				System.Boolean isfirst
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Int32 mallcode
,				System.Int32 states
		)
		{
			this.Idx = idx;
			this.Account = account;
			this.SourceType = sourcetype;
			this.BillingId = billingid;
			this.Point = point;
			this.Bonus = bonus;
			this.Cash = cash;
			this.IsFirst = isfirst;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.MallCode = mallcode;
			this.States = states;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String Idx {get ; set ;}

		///<summary>
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Account {get ; set ;}

		///<summary>
		///来源类型：0，充值；1，联赛竞猜；2，邮件收取
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SourceType {get ; set ;}

		///<summary>
		///订单id
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String BillingId {get ; set ;}

		///<summary>
		///点券数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Point {get ; set ;}

		///<summary>
		///赠送点数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Bonus {get ; set ;}

		///<summary>
		///支付现金数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Decimal Cash {get ; set ;}

		///<summary>
		///是否首充
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean IsFirst {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///MallCode
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 MallCode {get ; set ;}

		///<summary>
		///States
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 States {get ; set ;}
		#endregion
        
        #region Clone
        public PayChargehistoryEntity Clone()
        {
            PayChargehistoryEntity entity = new PayChargehistoryEntity();
			entity.Idx = this.Idx;
			entity.Account = this.Account;
			entity.SourceType = this.SourceType;
			entity.BillingId = this.BillingId;
			entity.Point = this.Point;
			entity.Bonus = this.Bonus;
			entity.Cash = this.Cash;
			entity.IsFirst = this.IsFirst;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.MallCode = this.MallCode;
			entity.States = this.States;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Pay_ChargeHistory 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PayChargehistoryResponse : BaseResponse<PayChargehistoryEntity>
    {

    }
}
