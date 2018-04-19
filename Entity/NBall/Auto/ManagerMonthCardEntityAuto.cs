
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Manager_MonthCard 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ManagerMonthcardEntity
	{
		
		public ManagerMonthcardEntity()
		{
		}

		public ManagerMonthcardEntity(
		System.Guid managerid
,				System.Int32 buynumber
,				System.DateTime buytime
,				System.DateTime duetotime
,				System.DateTime prizedate
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.BuyNumber = buynumber;
			this.BuyTime = buytime;
			this.DueToTime = duetotime;
			this.PrizeDate = prizedate;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///BuyNumber
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 BuyNumber {get ; set ;}

		///<summary>
		///BuyTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime BuyTime {get ; set ;}

		///<summary>
		///DueToTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime DueToTime {get ; set ;}

		///<summary>
		///PrizeDate
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.DateTime PrizeDate {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ManagerMonthcardEntity Clone()
        {
            ManagerMonthcardEntity entity = new ManagerMonthcardEntity();
			entity.ManagerId = this.ManagerId;
			entity.BuyNumber = this.BuyNumber;
			entity.BuyTime = this.BuyTime;
			entity.DueToTime = this.DueToTime;
			entity.PrizeDate = this.PrizeDate;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Manager_MonthCard 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ManagerMonthcardResponse : BaseResponse<ManagerMonthcardEntity>
    {

    }
}

