
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Online_LockManager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class OnlineLockmanagerEntity
	{
		
		public OnlineLockmanagerEntity()
		{
		}

		public OnlineLockmanagerEntity(
		System.Int64 id
,				System.Guid managerid
,				System.String managername
,				System.Int32 locktype
,				System.String lockoperator
,				System.DateTime lockdate
,				System.String lockmemo
,				System.Boolean breakflag
,				System.DateTime prebreakdate
,				System.String breakoperator
,				System.DateTime breakdate
,				System.String breakmemo
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Id = id;
			this.ManagerId = managerid;
			this.ManagerName = managername;
			this.LockType = locktype;
			this.LockOperator = lockoperator;
			this.LockDate = lockdate;
			this.LockMemo = lockmemo;
			this.BreakFlag = breakflag;
			this.PreBreakDate = prebreakdate;
			this.BreakOperator = breakoperator;
			this.BreakDate = breakdate;
			this.BreakMemo = breakmemo;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int64 Id {get ; set ;}

		///<summary>
		///经理Id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///经理名称
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///封停类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 LockType {get ; set ;}

		///<summary>
		///封停GM
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String LockOperator {get ; set ;}

		///<summary>
		///封停日期
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime LockDate {get ; set ;}

		///<summary>
		///封停原因
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String LockMemo {get ; set ;}

		///<summary>
		///解除标记
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean BreakFlag {get ; set ;}

		///<summary>
		///预计解除日期
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime PreBreakDate {get ; set ;}

		///<summary>
		///解封GM
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String BreakOperator {get ; set ;}

		///<summary>
		///解封日期
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime BreakDate {get ; set ;}

		///<summary>
		///解封原因
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String BreakMemo {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public OnlineLockmanagerEntity Clone()
        {
            OnlineLockmanagerEntity entity = new OnlineLockmanagerEntity();
			entity.Id = this.Id;
			entity.ManagerId = this.ManagerId;
			entity.ManagerName = this.ManagerName;
			entity.LockType = this.LockType;
			entity.LockOperator = this.LockOperator;
			entity.LockDate = this.LockDate;
			entity.LockMemo = this.LockMemo;
			entity.BreakFlag = this.BreakFlag;
			entity.PreBreakDate = this.PreBreakDate;
			entity.BreakOperator = this.BreakOperator;
			entity.BreakDate = this.BreakDate;
			entity.BreakMemo = this.BreakMemo;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Online_LockManager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class OnlineLockmanagerResponse : BaseResponse<OnlineLockmanagerEntity>
    {

    }
}

