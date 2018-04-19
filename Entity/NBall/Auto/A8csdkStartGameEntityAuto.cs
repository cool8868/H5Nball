
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.A8csdk_StartGame 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class A8csdkStartgameEntity
	{
		
		public A8csdkStartgameEntity()
		{
		}

		public A8csdkStartgameEntity(
		System.String openid
,				System.String state
,				System.String serverid
,				System.String pf
,				System.String sessionid
,				System.String jsneed
,				System.String nickname
,				System.String common
		)
		{
			this.OpenId = openid;
			this.State = state;
			this.ServerId = serverid;
			this.Pf = pf;
			this.SessionId = sessionid;
			this.JsNeed = jsneed;
			this.NickName = nickname;
			this.Common = common;
		}
		
		#region Public Properties
		
		///<summary>
		///OpenId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String OpenId {get ; set ;}

		///<summary>
		///State
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String State {get ; set ;}

		///<summary>
		///ServerId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ServerId {get ; set ;}

		///<summary>
		///Pf
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Pf {get ; set ;}

		///<summary>
		///SessionId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String SessionId {get ; set ;}

		///<summary>
		///JsNeed
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String JsNeed {get ; set ;}

		///<summary>
		///NickName
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String NickName {get ; set ;}

		///<summary>
		///Common
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String Common {get ; set ;}
		#endregion
        
        #region Clone
        public A8csdkStartgameEntity Clone()
        {
            A8csdkStartgameEntity entity = new A8csdkStartgameEntity();
			entity.OpenId = this.OpenId;
			entity.State = this.State;
			entity.ServerId = this.ServerId;
			entity.Pf = this.Pf;
			entity.SessionId = this.SessionId;
			entity.JsNeed = this.JsNeed;
			entity.NickName = this.NickName;
			entity.Common = this.Common;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.A8csdk_StartGame 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class A8csdkStartgameResponse : BaseResponse<A8csdkStartgameEntity>
    {

    }
}
