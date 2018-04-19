
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Arena_Shop 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ArenaShopEntity
	{
		
		public ArenaShopEntity()
		{
		}

		public ArenaShopEntity(
		System.Guid managerid
,				System.String itemstring
,				System.String exchangerecord
,				System.DateTime refreshtime
,				System.Int32 refreshnumber
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.ItemString = itemstring;
			this.ExChangeRecord = exchangerecord;
			this.RefreshTime = refreshtime;
			this.RefreshNumber = refreshnumber;
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
		///兑换记录   0,0,0,0,0,0 或 1,1,1,1,1,1
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ExChangeRecord {get ; set ;}

		///<summary>
		///RefreshTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RefreshTime {get ; set ;}

		///<summary>
		///刷新次数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 RefreshNumber {get ; set ;}

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
        public ArenaShopEntity Clone()
        {
            ArenaShopEntity entity = new ArenaShopEntity();
			entity.ManagerId = this.ManagerId;
			entity.ItemString = this.ItemString;
			entity.ExChangeRecord = this.ExChangeRecord;
			entity.RefreshTime = this.RefreshTime;
			entity.RefreshNumber = this.RefreshNumber;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Arena_Shop 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ArenaShopResponse : BaseResponse<ArenaShopEntity>
    {

    }
}
