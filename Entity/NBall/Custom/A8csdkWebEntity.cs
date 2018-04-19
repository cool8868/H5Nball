
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
	[DataContract]
	[Serializable]
    [ProtoContract]
	public class A8csdkWebEntity
	{
		
		public A8csdkWebEntity()
		{
		}

		public A8csdkWebEntity(
					System.String gameorderid
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
        
    
		
	}
	
	
  
}
