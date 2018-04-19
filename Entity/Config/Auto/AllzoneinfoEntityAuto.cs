
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.All_Zoneinfo 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AllZoneinfoEntity
	{
		
		public AllZoneinfoEntity()
		{
		}

		public AllZoneinfoEntity(
		System.Int32 idx
,				System.String platformcode
,				System.String zonename
,				System.String platformzonename
,				System.String name
,				System.String apiurl
,				System.String chargeurl
,				System.String webserverurl
,				System.Int32 isdebug
,				System.Int32 isopencharge
,				System.String chatip
,				System.Int32 chatport
,				System.String clientversion
,				System.String cdn
,				System.String crossname
,				System.Boolean openindulge
,				System.String txfooter
,				System.String gamename
,				System.Int32 states
,				System.String maintain
		)
		{
			this.Idx = idx;
			this.PlatformCode = platformcode;
			this.ZoneName = zonename;
			this.PlatformZoneName = platformzonename;
			this.Name = name;
			this.ApiUrl = apiurl;
			this.ChargeUrl = chargeurl;
			this.WebServerUrl = webserverurl;
			this.IsDebug = isdebug;
			this.IsOpenCharge = isopencharge;
			this.ChatIp = chatip;
			this.ChatPort = chatport;
			this.ClientVersion = clientversion;
			this.Cdn = cdn;
			this.CrossName = crossname;
			this.OpenIndulge = openindulge;
			this.TxFooter = txfooter;
			this.GameName = gamename;
			this.States = states;
			this.Maintain = maintain;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///PlatformCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String PlatformCode {get ; set ;}

		///<summary>
		///ZoneName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ZoneName {get ; set ;}

		///<summary>
		///PlatformZoneName
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String PlatformZoneName {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Name {get ; set ;}

		///<summary>
		///ApiUrl
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String ApiUrl {get ; set ;}

		///<summary>
		///ChargeUrl
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String ChargeUrl {get ; set ;}

		///<summary>
		///WebServerUrl
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String WebServerUrl {get ; set ;}

		///<summary>
		///IsDebug
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 IsDebug {get ; set ;}

		///<summary>
		///IsOpenCharge
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 IsOpenCharge {get ; set ;}

		///<summary>
		///ChatIp
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String ChatIp {get ; set ;}

		///<summary>
		///ChatPort
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 ChatPort {get ; set ;}

		///<summary>
		///ClientVersion
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String ClientVersion {get ; set ;}

		///<summary>
		///Cdn
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String Cdn {get ; set ;}

		///<summary>
		///CrossName
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String CrossName {get ; set ;}

		///<summary>
		///OpenIndulge
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Boolean OpenIndulge {get ; set ;}

		///<summary>
		///TxFooter
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String TxFooter {get ; set ;}

		///<summary>
		///GameName
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.String GameName {get ; set ;}

		///<summary>
		///区状态
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 States {get ; set ;}

		///<summary>
		///维护说明
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.String Maintain {get ; set ;}
		#endregion
        
        #region Clone
        public AllZoneinfoEntity Clone()
        {
            AllZoneinfoEntity entity = new AllZoneinfoEntity();
			entity.Idx = this.Idx;
			entity.PlatformCode = this.PlatformCode;
			entity.ZoneName = this.ZoneName;
			entity.PlatformZoneName = this.PlatformZoneName;
			entity.Name = this.Name;
			entity.ApiUrl = this.ApiUrl;
			entity.ChargeUrl = this.ChargeUrl;
			entity.WebServerUrl = this.WebServerUrl;
			entity.IsDebug = this.IsDebug;
			entity.IsOpenCharge = this.IsOpenCharge;
			entity.ChatIp = this.ChatIp;
			entity.ChatPort = this.ChatPort;
			entity.ClientVersion = this.ClientVersion;
			entity.Cdn = this.Cdn;
			entity.CrossName = this.CrossName;
			entity.OpenIndulge = this.OpenIndulge;
			entity.TxFooter = this.TxFooter;
			entity.GameName = this.GameName;
			entity.States = this.States;
			entity.Maintain = this.Maintain;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.All_Zoneinfo 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AllZoneinfoResponse : BaseResponse<AllZoneinfoEntity>
    {

    }
}

