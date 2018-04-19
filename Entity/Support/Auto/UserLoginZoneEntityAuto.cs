
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.UserLogin_Zone 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class UserloginZoneEntity
	{
		
		public UserloginZoneEntity()
		{
		}

		public UserloginZoneEntity(
		System.String account
,				System.String platform
,				System.DateTime lastlogintime
,				System.String loginsties
,				System.DateTime rowtime
		)
		{
			this.Account = account;
			this.Platform = platform;
			this.LastLoginTime = lastlogintime;
			this.LoginSties = loginsties;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String Account {get ; set ;}

		///<summary>
		///平台
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Platform {get ; set ;}

		///<summary>
		///最后登录时间
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime LastLoginTime {get ; set ;}

		///<summary>
		///登录过的区  s1,s2,s3
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String LoginSties {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public UserloginZoneEntity Clone()
        {
            UserloginZoneEntity entity = new UserloginZoneEntity();
			entity.Account = this.Account;
			entity.Platform = this.Platform;
			entity.LastLoginTime = this.LastLoginTime;
			entity.LoginSties = this.LoginSties;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.UserLogin_Zone 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class UserloginZoneResponse : BaseResponse<UserloginZoneEntity>
    {

    }
}

