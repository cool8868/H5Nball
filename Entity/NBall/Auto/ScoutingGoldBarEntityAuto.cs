
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Scouting_GoldBar 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ScoutingGoldbarEntity
	{
		
		public ScoutingGoldbarEntity()
		{
		}

		public ScoutingGoldbarEntity(
		System.Guid managerid
,				System.Int32 goldbarnumber
,				System.Int32 scoutingnumber
,				System.Int32 tennumber
,				System.Int32 status
,				System.DateTime updatetiem
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.GoldBarNumber = goldbarnumber;
			this.ScoutingNumber = scoutingnumber;
			this.TenNumber = tennumber;
			this.Status = status;
			this.UpdateTiem = updatetiem;
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
		///金条数量
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 GoldBarNumber {get ; set ;}

		///<summary>
		///抽卡次数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ScoutingNumber {get ; set ;}

		///<summary>
		///十连抽次数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 TenNumber {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTiem
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTiem {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ScoutingGoldbarEntity Clone()
        {
            ScoutingGoldbarEntity entity = new ScoutingGoldbarEntity();
			entity.ManagerId = this.ManagerId;
			entity.GoldBarNumber = this.GoldBarNumber;
			entity.ScoutingNumber = this.ScoutingNumber;
			entity.TenNumber = this.TenNumber;
			entity.Status = this.Status;
			entity.UpdateTiem = this.UpdateTiem;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Scouting_GoldBar 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ScoutingGoldbarResponse : BaseResponse<ScoutingGoldbarEntity>
    {

    }
}
