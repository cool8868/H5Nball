
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.All_SiteMap 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AllSitemapEntity
	{
		
		public AllSitemapEntity()
		{
		}

		public AllSitemapEntity(
		System.Int32 id
,				System.String platcode
,				System.String platsiteid
,				System.String siteid
,				System.String sitename
,				System.DateTime pendstarttime
,				System.DateTime pendendtime
,				System.String sitestate
,				System.String platmainurl
,				System.String platapiurl
,				System.String payurl
,				System.String bbsurl
,				System.String navurl
,				System.String cdnurl
,				System.String chaturl
,				System.String sitemainurl
,				System.String siteloginurl
,				System.String siteapiurl
,				System.String sitesvcurl
,				System.DateTime rowtime
		)
		{
			this.Id = id;
			this.PlatCode = platcode;
			this.PlatSiteId = platsiteid;
			this.SiteId = siteid;
			this.SiteName = sitename;
			this.PendStartTime = pendstarttime;
			this.PendEndTime = pendendtime;
			this.SiteState = sitestate;
			this.PlatMainUrl = platmainurl;
			this.PlatApiUrl = platapiurl;
			this.PayUrl = payurl;
			this.BbsUrl = bbsurl;
			this.NavUrl = navurl;
			this.CdnUrl = cdnurl;
			this.ChatUrl = chaturl;
			this.SiteMainUrl = sitemainurl;
			this.SiteLoginUrl = siteloginurl;
			this.SiteApiUrl = siteapiurl;
			this.SiteSvcUrl = sitesvcurl;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Id {get ; set ;}

		///<summary>
		///PlatCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String PlatCode {get ; set ;}

		///<summary>
		///PlatSiteId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String PlatSiteId {get ; set ;}

		///<summary>
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///SiteName
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String SiteName {get ; set ;}

		///<summary>
		///PendStartTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime PendStartTime {get ; set ;}

		///<summary>
		///PendEndTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime PendEndTime {get ; set ;}

		///<summary>
		///SiteState
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String SiteState {get ; set ;}

		///<summary>
		///PlatMainUrl
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String PlatMainUrl {get ; set ;}

		///<summary>
		///PlatApiUrl
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String PlatApiUrl {get ; set ;}

		///<summary>
		///PayUrl
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String PayUrl {get ; set ;}

		///<summary>
		///BbsUrl
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String BbsUrl {get ; set ;}

		///<summary>
		///NavUrl
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String NavUrl {get ; set ;}

		///<summary>
		///CdnUrl
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String CdnUrl {get ; set ;}

		///<summary>
		///ChatUrl
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String ChatUrl {get ; set ;}

		///<summary>
		///SiteMainUrl
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.String SiteMainUrl {get ; set ;}

		///<summary>
		///SiteLoginUrl
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String SiteLoginUrl {get ; set ;}

		///<summary>
		///SiteApiUrl
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.String SiteApiUrl {get ; set ;}

		///<summary>
		///SiteSvcUrl
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.String SiteSvcUrl {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(20)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public AllSitemapEntity Clone()
        {
            AllSitemapEntity entity = new AllSitemapEntity();
			entity.Id = this.Id;
			entity.PlatCode = this.PlatCode;
			entity.PlatSiteId = this.PlatSiteId;
			entity.SiteId = this.SiteId;
			entity.SiteName = this.SiteName;
			entity.PendStartTime = this.PendStartTime;
			entity.PendEndTime = this.PendEndTime;
			entity.SiteState = this.SiteState;
			entity.PlatMainUrl = this.PlatMainUrl;
			entity.PlatApiUrl = this.PlatApiUrl;
			entity.PayUrl = this.PayUrl;
			entity.BbsUrl = this.BbsUrl;
			entity.NavUrl = this.NavUrl;
			entity.CdnUrl = this.CdnUrl;
			entity.ChatUrl = this.ChatUrl;
			entity.SiteMainUrl = this.SiteMainUrl;
			entity.SiteLoginUrl = this.SiteLoginUrl;
			entity.SiteApiUrl = this.SiteApiUrl;
			entity.SiteSvcUrl = this.SiteSvcUrl;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.All_SiteMap 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AllSitemapResponse : BaseResponse<AllSitemapEntity>
    {

    }
}

