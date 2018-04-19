
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationInfoEntity
	{
		
		public RevelationInfoEntity()
		{
		}

		public RevelationInfoEntity(
		System.Guid managerid
,				System.Int32 courage
,				System.Int32 lockmark
,				System.Int32 presentmark
,				System.Int32 schedule
,				System.Boolean isgeneralmark
,				System.String passlog
,				System.String firstpasslog
,				System.String daymatchlog
,				System.Int32 morale
,				System.Boolean ishavedraw
,				System.Guid drawid
,				System.Boolean ishook
,				System.Guid hookid
,				System.DateTime refreshdata
,				System.Int32 status
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.Courage = courage;
			this.LockMark = lockmark;
			this.PresentMark = presentmark;
			this.Schedule = schedule;
			this.IsGeneralMark = isgeneralmark;
			this.PassLog = passlog;
			this.FirstPassLog = firstpasslog;
			this.DayMatchLog = daymatchlog;
			this.Morale = morale;
			this.IsHaveDraw = ishavedraw;
			this.DrawId = drawid;
			this.IsHook = ishook;
			this.HookId = hookid;
			this.RefreshData = refreshdata;
			this.Status = status;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///勇气值
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Courage {get ; set ;}

		///<summary>
		///解锁到了哪个关卡
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 LockMark {get ; set ;}

		///<summary>
		///当前比赛的关卡
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PresentMark {get ; set ;}

		///<summary>
		///当前的小关卡
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///是否通过关卡
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Boolean IsGeneralMark {get ; set ;}

		///<summary>
		///通关记录
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String PassLog {get ; set ;}

		///<summary>
		///首次通关记录
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String FirstPassLog {get ; set ;}

		///<summary>
		///当天的通关记录
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String DayMatchLog {get ; set ;}

		///<summary>
		///士气
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Morale {get ; set ;}

		///<summary>
		///是否有翻牌
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Boolean IsHaveDraw {get ; set ;}

		///<summary>
		///翻牌记录ID
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Guid DrawId {get ; set ;}

		///<summary>
		///是否有挂机
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Boolean IsHook {get ; set ;}

		///<summary>
		///挂机ID
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Guid HookId {get ; set ;}

		///<summary>
		///刷新的日期
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.DateTime RefreshData {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(17)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(18)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationInfoEntity Clone()
        {
            RevelationInfoEntity entity = new RevelationInfoEntity();
			entity.ManagerId = this.ManagerId;
			entity.Courage = this.Courage;
			entity.LockMark = this.LockMark;
			entity.PresentMark = this.PresentMark;
			entity.Schedule = this.Schedule;
			entity.IsGeneralMark = this.IsGeneralMark;
			entity.PassLog = this.PassLog;
			entity.FirstPassLog = this.FirstPassLog;
			entity.DayMatchLog = this.DayMatchLog;
			entity.Morale = this.Morale;
			entity.IsHaveDraw = this.IsHaveDraw;
			entity.DrawId = this.DrawId;
			entity.IsHook = this.IsHook;
			entity.HookId = this.HookId;
			entity.RefreshData = this.RefreshData;
			entity.Status = this.Status;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationInfoResponse : BaseResponse<RevelationInfoEntity>
    {

    }
}
