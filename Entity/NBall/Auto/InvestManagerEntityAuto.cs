
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Invest_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class InvestManagerEntity
	{
		
		public InvestManagerEntity()
		{
		}

		public InvestManagerEntity(
		System.Guid managerid
,				System.Int32 deposit
,				System.String stepstatus
,				System.Boolean themonthly
,				System.DateTime monthlytime
,				System.DateTime expirationtime
,				System.Boolean once
,				System.Int32 receivedcount
,				System.DateTime rowtime
,				System.Int32 depositcount
		)
		{
			this.ManagerId = managerid;
			this.Deposit = deposit;
			this.StepStatus = stepstatus;
			this.TheMonthly = themonthly;
			this.MonthlyTime = monthlytime;
			this.ExpirationTime = expirationtime;
			this.Once = once;
			this.ReceivedCount = receivedcount;
			this.RowTime = rowtime;
			this.DepositCount = depositcount;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///存的点券
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Deposit {get ; set ;}

		///<summary>
		///存入每档领取状态：0-默认 1-可领  2-已领取
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String StepStatus {get ; set ;}

		///<summary>
		///TheMonthly
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Boolean TheMonthly {get ; set ;}

		///<summary>
		///MonthlyTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime MonthlyTime {get ; set ;}

		///<summary>
		///ExpirationTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime ExpirationTime {get ; set ;}

		///<summary>
		///Once
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean Once {get ; set ;}

		///<summary>
		///ReceivedCount
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ReceivedCount {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///DepositCount
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 DepositCount {get ; set ;}
		#endregion
        
        #region Clone
        public InvestManagerEntity Clone()
        {
            InvestManagerEntity entity = new InvestManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.Deposit = this.Deposit;
			entity.StepStatus = this.StepStatus;
			entity.TheMonthly = this.TheMonthly;
			entity.MonthlyTime = this.MonthlyTime;
			entity.ExpirationTime = this.ExpirationTime;
			entity.Once = this.Once;
			entity.ReceivedCount = this.ReceivedCount;
			entity.RowTime = this.RowTime;
			entity.DepositCount = this.DepositCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Invest_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class InvestManagerResponse : BaseResponse<InvestManagerEntity>
    {

    }
}

