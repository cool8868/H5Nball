
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_UserReg 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbUserregEntity
	{
		
		public NbUserregEntity()
		{
		}

		public NbUserregEntity(
		System.String account
,				System.String pwd
,				System.String name
,				System.String cert
,				System.DateTime birthday
,				System.DateTime recorddate
,				System.Int32 lastonlineminutes
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Account = account;
			this.Pwd = pwd;
			this.Name = name;
			this.Cert = cert;
			this.Birthday = birthday;
			this.RecordDate = recorddate;
			this.LastOnlineMinutes = lastonlineminutes;
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
		///Pwd
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Pwd {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///Cert
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Cert {get ; set ;}

		///<summary>
		///Birthday
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime Birthday {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///LastOnlineMinutes
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 LastOnlineMinutes {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public NbUserregEntity Clone()
        {
            NbUserregEntity entity = new NbUserregEntity();
			entity.Account = this.Account;
			entity.Pwd = this.Pwd;
			entity.Name = this.Name;
			entity.Cert = this.Cert;
			entity.Birthday = this.Birthday;
			entity.RecordDate = this.RecordDate;
			entity.LastOnlineMinutes = this.LastOnlineMinutes;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_UserReg 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbUserregResponse : BaseResponse<NbUserregEntity>
    {

    }
}
