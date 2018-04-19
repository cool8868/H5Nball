
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.All_Database 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AllDatabaseEntity
	{
		
		public AllDatabaseEntity()
		{
		}

		public AllDatabaseEntity(
		System.Int32 idx
,				System.String zonename
,				System.String dbtype
,				System.String dbservername
,				System.String dbname
,				System.String userid
,				System.String password
		)
		{
			this.Idx = idx;
			this.ZoneName = zonename;
			this.DBType = dbtype;
			this.DBServerName = dbservername;
			this.DBName = dbname;
			this.UserId = userid;
			this.Password = password;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ZoneName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String ZoneName {get ; set ;}

		///<summary>
		///DBType
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String DBType {get ; set ;}

		///<summary>
		///DBServerName
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String DBServerName {get ; set ;}

		///<summary>
		///DBName
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String DBName {get ; set ;}

		///<summary>
		///UserId
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String UserId {get ; set ;}

		///<summary>
		///Password
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String Password {get ; set ;}
		#endregion
        
        #region Clone
        public AllDatabaseEntity Clone()
        {
            AllDatabaseEntity entity = new AllDatabaseEntity();
			entity.Idx = this.Idx;
			entity.ZoneName = this.ZoneName;
			entity.DBType = this.DBType;
			entity.DBServerName = this.DBServerName;
			entity.DBName = this.DBName;
			entity.UserId = this.UserId;
			entity.Password = this.Password;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.All_Database 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AllDatabaseResponse : BaseResponse<AllDatabaseEntity>
    {

    }
}

