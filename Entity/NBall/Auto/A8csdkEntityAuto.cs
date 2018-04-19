
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.A8csdk 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class A8csdkEntity
	{
		
		public A8csdkEntity()
		{
		}

		public A8csdkEntity(
		System.Int32 idx
,				System.String gameorderid
,				System.Int32 price
,				System.String goodsname
,				System.String goodsid
,				System.String title
,				System.String csdkid
,				System.String channelalias
,				System.String subchannel
,				System.String serverid
,				System.String servername
,				System.String roleid
,				System.String rolename
,				System.String rolelevel
,				System.String sessionid
,				System.String model
,				System.String release
,				System.String deviceid
,				System.String ext
,				System.String uid
		)
		{
			this.Idx = idx;
			this.GameOrderId = gameorderid;
			this.Price = price;
			this.GoodsName = goodsname;
			this.GoodsId = goodsid;
			this.Title = title;
			this.CsdkId = csdkid;
			this.ChannelAlias = channelalias;
			this.SubChannel = subchannel;
			this.ServerId = serverid;
			this.ServerName = servername;
			this.RoleId = roleid;
			this.RoleName = rolename;
			this.RoleLevel = rolelevel;
			this.SessionId = sessionid;
			this.Model = model;
			this.Release = release;
			this.DeviceId = deviceid;
			this.Ext = ext;
			this.Uid = uid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///GameOrderId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String GameOrderId {get ; set ;}

		///<summary>
		///Price
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Price {get ; set ;}

		///<summary>
		///GoodsName
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String GoodsName {get ; set ;}

		///<summary>
		///GoodsId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String GoodsId {get ; set ;}

		///<summary>
		///Title
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Title {get ; set ;}

		///<summary>
		///CsdkId
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String CsdkId {get ; set ;}

		///<summary>
		///ChannelAlias
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String ChannelAlias {get ; set ;}

		///<summary>
		///SubChannel
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String SubChannel {get ; set ;}

		///<summary>
		///ServerId
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String ServerId {get ; set ;}

		///<summary>
		///ServerName
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String ServerName {get ; set ;}

		///<summary>
		///RoleId
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String RoleId {get ; set ;}

		///<summary>
		///RoleName
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String RoleName {get ; set ;}

		///<summary>
		///RoleLevel
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String RoleLevel {get ; set ;}

		///<summary>
		///SessionId
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String SessionId {get ; set ;}

		///<summary>
		///Model
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.String Model {get ; set ;}

		///<summary>
		///Release
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String Release {get ; set ;}

		///<summary>
		///DeviceId
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.String DeviceId {get ; set ;}

		///<summary>
		///Ext
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.String Ext {get ; set ;}

		///<summary>
		///Uid
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.String Uid {get ; set ;}
		#endregion
        
        #region Clone
        public A8csdkEntity Clone()
        {
            A8csdkEntity entity = new A8csdkEntity();
			entity.Idx = this.Idx;
			entity.GameOrderId = this.GameOrderId;
			entity.Price = this.Price;
			entity.GoodsName = this.GoodsName;
			entity.GoodsId = this.GoodsId;
			entity.Title = this.Title;
			entity.CsdkId = this.CsdkId;
			entity.ChannelAlias = this.ChannelAlias;
			entity.SubChannel = this.SubChannel;
			entity.ServerId = this.ServerId;
			entity.ServerName = this.ServerName;
			entity.RoleId = this.RoleId;
			entity.RoleName = this.RoleName;
			entity.RoleLevel = this.RoleLevel;
			entity.SessionId = this.SessionId;
			entity.Model = this.Model;
			entity.Release = this.Release;
			entity.DeviceId = this.DeviceId;
			entity.Ext = this.Ext;
			entity.Uid = this.Uid;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.A8csdk 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class A8csdkResponse : BaseResponse<A8csdkEntity>
    {

    }
}
