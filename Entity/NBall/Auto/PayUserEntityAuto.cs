
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Pay_User 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PayUserEntity
	{
		
		public PayUserEntity()
		{
		}

		public PayUserEntity(
		System.String account
,				System.Int32 point
,				System.Int32 bonus
,				System.Decimal totalcash
,				System.DateTime rowtime
,				System.Byte[] rowversion
,				System.Int32 chargepoint
,				System.Int32 bindpoint
		)
		{
			this.Account = account;
			this.Point = point;
			this.Bonus = bonus;
			this.TotalCash = totalcash;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
			this.ChargePoint = chargepoint;
			this.BindPoint = bindpoint;
		}
		
		#region Public Properties
		
		///<summary>
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String Account {get ; set ;}

		///<summary>
		///点券数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Point {get ; set ;}

		///<summary>
		///免费点券数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Bonus {get ; set ;}

		///<summary>
		///总充值金额
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Decimal TotalCash {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}

		///<summary>
		///ChargePoint
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 ChargePoint {get ; set ;}

		///<summary>
		///BindPoint
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 BindPoint {get ; set ;}
		#endregion
        
        #region Clone
        public PayUserEntity Clone()
        {
            PayUserEntity entity = new PayUserEntity();
			entity.Account = this.Account;
			entity.Point = this.Point;
			entity.Bonus = this.Bonus;
			entity.TotalCash = this.TotalCash;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
			entity.ChargePoint = this.ChargePoint;
			entity.BindPoint = this.BindPoint;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Pay_User 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PayUserResponse : BaseResponse<PayUserEntity>
    {

    }
}
