
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.All_PlatMap 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AllPlatmapEntity
	{
		
		public AllPlatmapEntity()
		{
		}

		public AllPlatmapEntity(
		System.String platcode
,				System.String platname
,				System.String sharedomain
,				System.Int32 sessionmode
,				System.String appkey
,				System.String appsecret
,				System.String loginkey
,				System.String paykey
,				System.Int32 paypointrate
,				System.String platmainurl
,				System.String platapiurl
,				System.String payurl
,				System.String bbsurl
,				System.String navurl
,				System.String cdnurl
,				System.String chaturl
,				System.DateTime rowtime
		)
		{
			this.PlatCode = platcode;
			this.PlatName = platname;
			this.ShareDomain = sharedomain;
			this.SessionMode = sessionmode;
			this.AppKey = appkey;
			this.AppSecret = appsecret;
			this.LoginKey = loginkey;
			this.PayKey = paykey;
			this.PayPointRate = paypointrate;
			this.PlatMainUrl = platmainurl;
			this.PlatApiUrl = platapiurl;
			this.PayUrl = payurl;
			this.BbsUrl = bbsurl;
			this.NavUrl = navurl;
			this.CdnUrl = cdnurl;
			this.ChatUrl = chaturl;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///PlatCode
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String PlatCode {get ; set ;}

		///<summary>
		///PlatName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String PlatName {get ; set ;}

		///<summary>
		///ShareDomain
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ShareDomain {get ; set ;}

		///<summary>
		///SessionMode
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SessionMode {get ; set ;}

		///<summary>
		///AppKey
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String AppKey {get ; set ;}

		///<summary>
		///AppSecret
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String AppSecret {get ; set ;}

		///<summary>
		///LoginKey
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String LoginKey {get ; set ;}

		///<summary>
		///PayKey
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String PayKey {get ; set ;}

		///<summary>
		///PayPointRate
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 PayPointRate {get ; set ;}

		///<summary>
		///PlatMainUrl
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String PlatMainUrl {get ; set ;}

		///<summary>
		///PlatApiUrl
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String PlatApiUrl {get ; set ;}

		///<summary>
		///PayUrl
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String PayUrl {get ; set ;}

		///<summary>
		///BbsUrl
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String BbsUrl {get ; set ;}

		///<summary>
		///NavUrl
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String NavUrl {get ; set ;}

		///<summary>
		///CdnUrl
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String CdnUrl {get ; set ;}

		///<summary>
		///ChatUrl
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.String ChatUrl {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(17)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public AllPlatmapEntity Clone()
        {
            AllPlatmapEntity entity = new AllPlatmapEntity();
			entity.PlatCode = this.PlatCode;
			entity.PlatName = this.PlatName;
			entity.ShareDomain = this.ShareDomain;
			entity.SessionMode = this.SessionMode;
			entity.AppKey = this.AppKey;
			entity.AppSecret = this.AppSecret;
			entity.LoginKey = this.LoginKey;
			entity.PayKey = this.PayKey;
			entity.PayPointRate = this.PayPointRate;
			entity.PlatMainUrl = this.PlatMainUrl;
			entity.PlatApiUrl = this.PlatApiUrl;
			entity.PayUrl = this.PayUrl;
			entity.BbsUrl = this.BbsUrl;
			entity.NavUrl = this.NavUrl;
			entity.CdnUrl = this.CdnUrl;
			entity.ChatUrl = this.ChatUrl;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.All_PlatMap 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AllPlatmapResponse : BaseResponse<AllPlatmapEntity>
    {

    }
}

