
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.A_MoveDataToServeRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AMovedatatoserverecordEntity
	{
		
		public AMovedatatoserverecordEntity()
		{
		}

		public AMovedatatoserverecordEntity(
		System.Int32 idx
,				System.String sourcedbfullname
,				System.String oldzonename
,				System.String account
,				System.Guid managerid
,				System.String name
,				System.Int32 level
,				System.Int32 mod
,				System.String targetaccount
,				System.String newname
,				System.Int32 status
,				System.Int32 returnvalue
,				System.String returnmessage
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Guid bindcode
		)
		{
			this.Idx = idx;
			this.SourceDbFullName = sourcedbfullname;
			this.OldZoneName = oldzonename;
			this.Account = account;
			this.ManagerId = managerid;
			this.Name = name;
			this.Level = level;
			this.Mod = mod;
			this.TargetAccount = targetaccount;
			this.NewName = newname;
			this.Status = status;
			this.ReturnValue = returnvalue;
			this.ReturnMessage = returnmessage;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.BindCode = bindcode;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///SourceDbFullName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SourceDbFullName {get ; set ;}

		///<summary>
		///OldZoneName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String OldZoneName {get ; set ;}

		///<summary>
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Account {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Name {get ; set ;}

		///<summary>
		///Level
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///Mod
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Mod {get ; set ;}

		///<summary>
		///TargetAccount
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String TargetAccount {get ; set ;}

		///<summary>
		///NewName
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String NewName {get ; set ;}

		///<summary>
		///状态：0，初始；1，成功；-1，失败；
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///ReturnValue
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 ReturnValue {get ; set ;}

		///<summary>
		///ReturnMessage
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String ReturnMessage {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///BindCode
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Guid BindCode {get ; set ;}
		#endregion
        
        #region Clone
        public AMovedatatoserverecordEntity Clone()
        {
            AMovedatatoserverecordEntity entity = new AMovedatatoserverecordEntity();
			entity.Idx = this.Idx;
			entity.SourceDbFullName = this.SourceDbFullName;
			entity.OldZoneName = this.OldZoneName;
			entity.Account = this.Account;
			entity.ManagerId = this.ManagerId;
			entity.Name = this.Name;
			entity.Level = this.Level;
			entity.Mod = this.Mod;
			entity.TargetAccount = this.TargetAccount;
			entity.NewName = this.NewName;
			entity.Status = this.Status;
			entity.ReturnValue = this.ReturnValue;
			entity.ReturnMessage = this.ReturnMessage;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.BindCode = this.BindCode;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.A_MoveDataToServeRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AMovedatatoserverecordResponse : BaseResponse<AMovedatatoserverecordEntity>
    {

    }
}

