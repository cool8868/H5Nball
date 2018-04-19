
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Pay_Continuing 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PayContinuingEntity
	{
		
		public PayContinuingEntity()
		{
		}

		public PayContinuingEntity(
		System.String account
,				System.DateTime lastpaydate
,				System.Int32 continuingday
,				System.DateTime startdate
,				System.DateTime enddate
,				System.Int32 totalpoint
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Account = account;
			this.LastPayDate = lastpaydate;
			this.ContinuingDay = continuingday;
			this.StartDate = startdate;
			this.EndDate = enddate;
			this.TotalPoint = totalpoint;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String Account {get ; set ;}

		///<summary>
		///LastPayDate
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.DateTime LastPayDate {get ; set ;}

		///<summary>
		///ContinuingDay
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ContinuingDay {get ; set ;}

		///<summary>
		///StartDate
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime StartDate {get ; set ;}

		///<summary>
		///EndDate
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime EndDate {get ; set ;}

		///<summary>
		///TotalPoint
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 TotalPoint {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public PayContinuingEntity Clone()
        {
            PayContinuingEntity entity = new PayContinuingEntity();
			entity.Account = this.Account;
			entity.LastPayDate = this.LastPayDate;
			entity.ContinuingDay = this.ContinuingDay;
			entity.StartDate = this.StartDate;
			entity.EndDate = this.EndDate;
			entity.TotalPoint = this.TotalPoint;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Pay_Continuing 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PayContinuingResponse : BaseResponse<PayContinuingEntity>
    {

    }
}
