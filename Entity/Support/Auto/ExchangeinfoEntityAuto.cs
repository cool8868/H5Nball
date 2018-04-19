
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Exchange_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ExchangeInfoEntity
	{
		
		public ExchangeInfoEntity()
		{
		}

		public ExchangeInfoEntity(
		System.String idx
,				System.Int32 exchangetype
,				System.Int32 zonename
,				System.String account
,				System.Guid managerid
,				System.Int32 atzoneid
,				System.Int32 packid
,				System.Int32 batchid
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Byte[] rowversion
,				System.String platformcode
		)
		{
			this.Idx = idx;
			this.ExchangeType = exchangetype;
			this.ZoneName = zonename;
			this.Account = account;
			this.ManagerId = managerid;
			this.AtZoneId = atzoneid;
			this.PackId = packid;
			this.BatchId = batchid;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.RowVersion = rowversion;
			this.PlatformCode = platformcode;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String Idx {get ; set ;}

		///<summary>
		///礼包类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ExchangeType {get ; set ;}

		///<summary>
		///针对区id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ZoneName {get ; set ;}

		///<summary>
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Account {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///兑换用户所在区
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 AtZoneId {get ; set ;}

		///<summary>
		///对应礼包id
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 PackId {get ; set ;}

		///<summary>
		///兑换批次
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 BatchId {get ; set ;}

		///<summary>
		///状态：0，初始；1，已使用 
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}

		///<summary>
		///PlatformCode
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String PlatformCode {get ; set ;}
		#endregion
        
        #region Clone
        public ExchangeInfoEntity Clone()
        {
            ExchangeInfoEntity entity = new ExchangeInfoEntity();
			entity.Idx = this.Idx;
			entity.ExchangeType = this.ExchangeType;
			entity.ZoneName = this.ZoneName;
			entity.Account = this.Account;
			entity.ManagerId = this.ManagerId;
			entity.AtZoneId = this.AtZoneId;
			entity.PackId = this.PackId;
			entity.BatchId = this.BatchId;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.RowVersion = this.RowVersion;
			entity.PlatformCode = this.PlatformCode;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Exchange_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ExchangeInfoResponse : BaseResponse<ExchangeInfoEntity>
    {

    }
}
