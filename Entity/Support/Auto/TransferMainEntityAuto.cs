
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Transfer_Main 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TransferMainEntity
	{
		
		public TransferMainEntity()
		{
		}

		public TransferMainEntity(
		System.Guid transferid
,				System.Int32 domainid
,				System.Int32 itemcode
,				System.String itemname
,				System.Byte[] itemprop
,				System.String sellname
,				System.Guid sellid
,				System.String sellzonename
,				System.Int32 price
,				System.String dealendname
,				System.String dealendzonename
,				System.Int32 dealendprice
,				System.Guid dealendid
,				System.DateTime transferstarttime
,				System.DateTime transferduration
,				System.Int32 status
,				System.Int32 poundage
,				System.Int32 getprice
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.TransferId = transferid;
			this.DomainId = domainid;
			this.ItemCode = itemcode;
			this.ItemName = itemname;
			this.ItemProp = itemprop;
			this.SellName = sellname;
			this.SellId = sellid;
			this.SellZoneName = sellzonename;
			this.Price = price;
			this.DealEndName = dealendname;
			this.DealEndZoneName = dealendzonename;
			this.DealEndPrice = dealendprice;
			this.DealEndId = dealendid;
			this.TransferStartTime = transferstarttime;
			this.TransferDuration = transferduration;
			this.Status = status;
			this.Poundage = poundage;
			this.GetPrice = getprice;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///排名ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid TransferId {get ; set ;}

		///<summary>
		///域ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DomainId {get ; set ;}

		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///物品名称
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String ItemName {get ; set ;}

		///<summary>
		///物品属性
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.Byte[] ItemProp {get ; set ;}

		///<summary>
		///出售人名字
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String SellName {get ; set ;}

		///<summary>
		///出售人ID
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Guid SellId {get ; set ;}

		///<summary>
		///出售人区 
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String SellZoneName {get ; set ;}

		///<summary>
		///起拍价格
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Price {get ; set ;}

		///<summary>
		///成交人名字
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String DealEndName {get ; set ;}

		///<summary>
		///成交人区ID
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String DealEndZoneName {get ; set ;}

		///<summary>
		///成交价格
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 DealEndPrice {get ; set ;}

		///<summary>
		///成交人ID
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Guid DealEndId {get ; set ;}

		///<summary>
		///拍卖开始时间
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime TransferStartTime {get ; set ;}

		///<summary>
		///持续时间 秒
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime TransferDuration {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///手续费
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 Poundage {get ; set ;}

		///<summary>
		///得到的金条
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 GetPrice {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(19)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(20)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public TransferMainEntity Clone()
        {
            TransferMainEntity entity = new TransferMainEntity();
			entity.TransferId = this.TransferId;
			entity.DomainId = this.DomainId;
			entity.ItemCode = this.ItemCode;
			entity.ItemName = this.ItemName;
			entity.ItemProp = this.ItemProp;
			entity.SellName = this.SellName;
			entity.SellId = this.SellId;
			entity.SellZoneName = this.SellZoneName;
			entity.Price = this.Price;
			entity.DealEndName = this.DealEndName;
			entity.DealEndZoneName = this.DealEndZoneName;
			entity.DealEndPrice = this.DealEndPrice;
			entity.DealEndId = this.DealEndId;
			entity.TransferStartTime = this.TransferStartTime;
			entity.TransferDuration = this.TransferDuration;
			entity.Status = this.Status;
			entity.Poundage = this.Poundage;
			entity.GetPrice = this.GetPrice;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Transfer_Main 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TransferMainResponse : BaseResponse<TransferMainEntity>
    {

    }
}
