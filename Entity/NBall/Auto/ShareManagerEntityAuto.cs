
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Share_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ShareManagerEntity
	{
		
		public ShareManagerEntity()
		{
		}

		public ShareManagerEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 sharetype
,				System.Int32 sharenumber
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ShareType = sharetype;
			this.ShareNumber = sharenumber;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///分享类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ShareType {get ; set ;}

		///<summary>
		///分享次数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ShareNumber {get ; set ;}

		///<summary>
		///更新时间
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ShareManagerEntity Clone()
        {
            ShareManagerEntity entity = new ShareManagerEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ShareType = this.ShareType;
			entity.ShareNumber = this.ShareNumber;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Share_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ShareManagerResponse : BaseResponse<ShareManagerEntity>
    {

    }
}
