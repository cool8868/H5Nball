
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gm_Log 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GmLogEntity
	{
		
		public GmLogEntity()
		{
		}

		public GmLogEntity(
		System.Int32 idx
,				System.String adminname
,				System.String ip
,				System.Int32 operationtype
,				System.String targetzoneid
,				System.String targetuser
,				System.String targetuserlist
,				System.String managername
,				System.Guid managerid
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.AdminName = adminname;
			this.Ip = ip;
			this.OperationType = operationtype;
			this.TargetZoneId = targetzoneid;
			this.TargetUser = targetuser;
			this.TargetUserList = targetuserlist;
			this.ManagerName = managername;
			this.ManagerId = managerid;
			this.Memo = memo;
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
		///AdminName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String AdminName {get ; set ;}

		///<summary>
		///Ip
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Ip {get ; set ;}

		///<summary>
		///OperationType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 OperationType {get ; set ;}

		///<summary>
		///TargetZoneId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String TargetZoneId {get ; set ;}

		///<summary>
		///TargetUser
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String TargetUser {get ; set ;}

		///<summary>
		///TargetUserList
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String TargetUserList {get ; set ;}

		///<summary>
		///ManagerName
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public GmLogEntity Clone()
        {
            GmLogEntity entity = new GmLogEntity();
			entity.Idx = this.Idx;
			entity.AdminName = this.AdminName;
			entity.Ip = this.Ip;
			entity.OperationType = this.OperationType;
			entity.TargetZoneId = this.TargetZoneId;
			entity.TargetUser = this.TargetUser;
			entity.TargetUserList = this.TargetUserList;
			entity.ManagerName = this.ManagerName;
			entity.ManagerId = this.ManagerId;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gm_Log 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GmLogResponse : BaseResponse<GmLogEntity>
    {

    }
}
