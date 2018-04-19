
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CrossSite 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCrosssiteEntity
	{
		
		public ConfigCrosssiteEntity()
		{
		}

		public ConfigCrosssiteEntity(
		System.Int32 id
,				System.Int32 domainid
,				System.String domainname
,				System.String siteid
,				System.String sitename
,				System.String showsiteid
,				System.String showsitename
,				System.Int32 invalidflag
,				System.DateTime rowtime
		)
		{
			this.Id = id;
			this.DomainId = domainid;
			this.DomainName = domainname;
			this.SiteId = siteid;
			this.SiteName = sitename;
			this.ShowSiteId = showsiteid;
			this.ShowSiteName = showsitename;
			this.InvalidFlag = invalidflag;
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
		///DomainId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DomainId {get ; set ;}

		///<summary>
		///DomainName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String DomainName {get ; set ;}

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
		///ShowSiteId
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String ShowSiteId {get ; set ;}

		///<summary>
		///ShowSiteName
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String ShowSiteName {get ; set ;}

		///<summary>
		///InvalidFlag
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 InvalidFlag {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCrosssiteEntity Clone()
        {
            ConfigCrosssiteEntity entity = new ConfigCrosssiteEntity();
			entity.Id = this.Id;
			entity.DomainId = this.DomainId;
			entity.DomainName = this.DomainName;
			entity.SiteId = this.SiteId;
			entity.SiteName = this.SiteName;
			entity.ShowSiteId = this.ShowSiteId;
			entity.ShowSiteName = this.ShowSiteName;
			entity.InvalidFlag = this.InvalidFlag;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CrossSite 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCrosssiteResponse : BaseResponse<ConfigCrosssiteEntity>
    {

    }
}

