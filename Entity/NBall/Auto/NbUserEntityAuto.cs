
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_User 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbUserEntity
	{
		
		public NbUserEntity()
		{
		}

		public NbUserEntity(
		System.String account
,				System.String source
,				System.DateTime lastlogintime
,				System.String lastloginip
,				System.DateTime lastlogindate
,				System.Int32 continuingloginday
,				System.String registerip
,				System.DateTime registerdate
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Account = account;
			this.Source = source;
			this.LastLoginTime = lastlogintime;
			this.LastLoginIp = lastloginip;
			this.LastLoginDate = lastlogindate;
			this.ContinuingLoginDay = continuingloginday;
			this.RegisterIp = registerip;
			this.RegisterDate = registerdate;
			this.Status = status;
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
		///用户来源
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Source {get ; set ;}

		///<summary>
		///最近登录时间
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime LastLoginTime {get ; set ;}

		///<summary>
		///最近登录ip
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String LastLoginIp {get ; set ;}

		///<summary>
		///LastLoginDate
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime LastLoginDate {get ; set ;}

		///<summary>
		///ContinuingLoginDay
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ContinuingLoginDay {get ; set ;}

		///<summary>
		///注册时的ip
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String RegisterIp {get ; set ;}

		///<summary>
		///RegisterDate
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RegisterDate {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public NbUserEntity Clone()
        {
            NbUserEntity entity = new NbUserEntity();
			entity.Account = this.Account;
			entity.Source = this.Source;
			entity.LastLoginTime = this.LastLoginTime;
			entity.LastLoginIp = this.LastLoginIp;
			entity.LastLoginDate = this.LastLoginDate;
			entity.ContinuingLoginDay = this.ContinuingLoginDay;
			entity.RegisterIp = this.RegisterIp;
			entity.RegisterDate = this.RegisterDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_User 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbUserResponse : BaseResponse<NbUserEntity>
    {

    }
}
