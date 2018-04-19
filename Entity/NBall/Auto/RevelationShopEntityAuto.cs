
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_Shop 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationShopEntity
	{
		
		public RevelationShopEntity()
		{
		}

		public RevelationShopEntity(
		System.Guid managerid
,				System.String itemstring
,				System.String exchangestring
,				System.Int32 status
,				System.DateTime refreshdata
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.ItemString = itemstring;
			this.ExChangeString = exchangestring;
			this.Status = status;
			this.RefreshData = refreshdata;
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
		///ItemString
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String ItemString {get ; set ;}

		///<summary>
		///ExChangeString
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ExChangeString {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RefreshData
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RefreshData {get ; set ;}

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
        public RevelationShopEntity Clone()
        {
            RevelationShopEntity entity = new RevelationShopEntity();
			entity.ManagerId = this.ManagerId;
			entity.ItemString = this.ItemString;
			entity.ExChangeString = this.ExChangeString;
			entity.Status = this.Status;
			entity.RefreshData = this.RefreshData;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_Shop 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationShopResponse : BaseResponse<RevelationShopEntity>
    {

    }
}
