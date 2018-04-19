
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Friend_OpenBoxRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class FriendOpenboxrecordEntity
	{
		
		public FriendOpenboxrecordEntity()
		{
		}

		public FriendOpenboxrecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Guid friendid
,				System.Int32 prizetype
,				System.Int32 prizeitem
,				System.Int32 prizecount
,				System.DateTime rowtime
		)
		{
			this.idx = idx;
			this.ManagerId = managerid;
			this.FriendId = friendid;
			this.PrizeType = prizetype;
			this.PrizeItem = prizeitem;
			this.PrizeCount = prizecount;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 idx {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///FriendId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid FriendId {get ; set ;}

		///<summary>
		///PrizeType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///PrizeItem
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeItem {get ; set ;}

		///<summary>
		///PrizeCount
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PrizeCount {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public FriendOpenboxrecordEntity Clone()
        {
            FriendOpenboxrecordEntity entity = new FriendOpenboxrecordEntity();
			entity.idx = this.idx;
			entity.ManagerId = this.ManagerId;
			entity.FriendId = this.FriendId;
			entity.PrizeType = this.PrizeType;
			entity.PrizeItem = this.PrizeItem;
			entity.PrizeCount = this.PrizeCount;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Friend_OpenBoxRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class FriendOpenboxrecordResponse : BaseResponse<FriendOpenboxrecordEntity>
    {

    }
}

