
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Information_Popup 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class InformationPopupEntity
	{
		
		public InformationPopupEntity()
		{
		}

		public InformationPopupEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 poptype
,				System.String contentstring
,				System.Boolean isread
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.PopType = poptype;
			this.ContentString = contentstring;
			this.IsRead = isread;
			this.Status = status;
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
		///PopType
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PopType {get ; set ;}

		///<summary>
		///内容串，对应静态表拼接
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String ContentString {get ; set ;}

		///<summary>
		///阅读标识
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Boolean IsRead {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public InformationPopupEntity Clone()
        {
            InformationPopupEntity entity = new InformationPopupEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.PopType = this.PopType;
			entity.ContentString = this.ContentString;
			entity.IsRead = this.IsRead;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Information_Popup 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class InformationPopupResponse : BaseResponse<InformationPopupEntity>
    {

    }
}

