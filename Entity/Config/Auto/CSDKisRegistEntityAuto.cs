
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CSDKisRegist 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CsdkisregistEntity
	{
		
		public CsdkisregistEntity()
		{
		}

		public CsdkisregistEntity(
		System.Int32 idx
,				System.String ret
,				System.String msg
,				System.String roleid
,				System.String rolename
,				System.String rolelevel
,				System.String serverno
,				System.String serverid
,				System.String servername
		)
		{
			this.idx = idx;
			this.ret = ret;
			this.msg = msg;
			this.roleId = roleid;
			this.roleName = rolename;
			this.roleLevel = rolelevel;
			this.serverNo = serverno;
			this.serverId = serverid;
			this.serverName = servername;
		}
		
		#region Public Properties
		
		///<summary>
		///idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 idx {get ; set ;}

		///<summary>
		///ret
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String ret {get ; set ;}

		///<summary>
		///msg
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String msg {get ; set ;}

		///<summary>
		///roleId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String roleId {get ; set ;}

		///<summary>
		///roleName
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String roleName {get ; set ;}

		///<summary>
		///roleLevel
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String roleLevel {get ; set ;}

		///<summary>
		///serverNo
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String serverNo {get ; set ;}

		///<summary>
		///serverId
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String serverId {get ; set ;}

		///<summary>
		///serverName
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String serverName {get ; set ;}
		#endregion
        
        #region Clone
        public CsdkisregistEntity Clone()
        {
            CsdkisregistEntity entity = new CsdkisregistEntity();
			entity.idx = this.idx;
			entity.ret = this.ret;
			entity.msg = this.msg;
			entity.roleId = this.roleId;
			entity.roleName = this.roleName;
			entity.roleLevel = this.roleLevel;
			entity.serverNo = this.serverNo;
			entity.serverId = this.serverId;
			entity.serverName = this.serverName;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CSDKisRegist 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CsdkisregistResponse : BaseResponse<CsdkisregistEntity>
    {

    }
}
