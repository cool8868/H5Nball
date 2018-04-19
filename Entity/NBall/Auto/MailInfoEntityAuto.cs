
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Mail_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class MailInfoEntity
	{
		
		public MailInfoEntity()
		{
		}

		public MailInfoEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 mailtype
,				System.String contentstring
,				System.Byte[] attachment
,				System.Boolean hasattach
,				System.Boolean isread
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime expiredtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.MailType = mailtype;
			this.ContentString = contentstring;
			this.Attachment = attachment;
			this.HasAttach = hasattach;
			this.IsRead = isread;
			this.Status = status;
			this.RowTime = rowtime;
			this.ExpiredTime = expiredtime;
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
		///邮件类型，对应静态表获取静态数据
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MailType {get ; set ;}

		///<summary>
		///内容串，对应静态表拼接
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String ContentString {get ; set ;}

		///<summary>
		///附件串，参数以逗号分隔，多个附件用竖线分隔
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.Byte[] Attachment {get ; set ;}

		///<summary>
		///是否有附件,领取后变为false
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Boolean HasAttach {get ; set ;}

		///<summary>
		///阅读标识
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsRead {get ; set ;}

		///<summary>
		///状态，当mailtype=5时，status=0表示未抽卡
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///ExpiredTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime ExpiredTime {get ; set ;}
		#endregion
        
        #region Clone
        public MailInfoEntity Clone()
        {
            MailInfoEntity entity = new MailInfoEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.MailType = this.MailType;
			entity.ContentString = this.ContentString;
			entity.Attachment = this.Attachment;
			entity.HasAttach = this.HasAttach;
			entity.IsRead = this.IsRead;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.ExpiredTime = this.ExpiredTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Mail_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class MailInfoResponse : BaseResponse<MailInfoEntity>
    {

    }
}

