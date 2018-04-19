
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_Hook 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationHookEntity
	{
		
		public RevelationHookEntity()
		{
		}

		public RevelationHookEntity(
		System.Guid hookid
,				System.Guid managerid
,				System.Int32 markid
,				System.Int32 schedule
,				System.String scorelog
,				System.String itemstring
,				System.Int32 status
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.HookId = hookid;
			this.ManagerId = managerid;
			this.MarkId = markid;
			this.Schedule = schedule;
			this.ScoreLog = scorelog;
			this.ItemString = itemstring;
			this.Status = status;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///HookId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid HookId {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///关卡ID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MarkId {get ; set ;}

		///<summary>
		///当前进度
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///比分记录
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ScoreLog {get ; set ;}

		///<summary>
		///得到的物品
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String ItemString {get ; set ;}

		///<summary>
		///状态
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationHookEntity Clone()
        {
            RevelationHookEntity entity = new RevelationHookEntity();
			entity.HookId = this.HookId;
			entity.ManagerId = this.ManagerId;
			entity.MarkId = this.MarkId;
			entity.Schedule = this.Schedule;
			entity.ScoreLog = this.ScoreLog;
			entity.ItemString = this.ItemString;
			entity.Status = this.Status;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_Hook 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationHookResponse : BaseResponse<RevelationHookEntity>
    {

    }
}
