
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.DailyAttendance_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DailyattendanceManagerEntity
	{
		
		public DailyattendanceManagerEntity()
		{
		}

		public DailyattendanceManagerEntity(
		System.Int64 idx
,				System.Guid managerid
,				System.String name
,				System.Int32 attendtimes
,				System.Int32 month
,				System.Boolean isattend
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.Name = name;
			this.AttendTimes = attendtimes;
			this.Month = month;
			this.IsAttend = isattend;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int64 Idx {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///AttendTimes
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 AttendTimes {get ; set ;}

		///<summary>
		///Month
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Month {get ; set ;}

		///<summary>
		///IsAttend
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Boolean IsAttend {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public DailyattendanceManagerEntity Clone()
        {
            DailyattendanceManagerEntity entity = new DailyattendanceManagerEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.Name = this.Name;
			entity.AttendTimes = this.AttendTimes;
			entity.Month = this.Month;
			entity.IsAttend = this.IsAttend;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.DailyAttendance_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DailyattendanceManagerResponse : BaseResponse<DailyattendanceManagerEntity>
    {

    }
}

