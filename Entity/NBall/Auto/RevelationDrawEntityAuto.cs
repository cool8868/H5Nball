
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_Draw 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationDrawEntity
	{
		
		public RevelationDrawEntity()
		{
		}

		public RevelationDrawEntity(
		System.Guid drawid
,				System.Guid managerid
,				System.Int32 markid
,				System.Int32 schedule
,				System.String allitemstring
,				System.String prizeitemstring
,				System.Int32 opennumber
,				System.Int32 status
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.DrawId = drawid;
			this.ManagerId = managerid;
			this.MarkId = markid;
			this.Schedule = schedule;
			this.AllItemString = allitemstring;
			this.PrizeItemString = prizeitemstring;
			this.OpenNumber = opennumber;
			this.Status = status;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///DrawId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid DrawId {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///MarkId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MarkId {get ; set ;}

		///<summary>
		///Schedule
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///翻牌的串
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String AllItemString {get ; set ;}

		///<summary>
		///翻出的牌
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String PrizeItemString {get ; set ;}

		///<summary>
		///开启次数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 OpenNumber {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationDrawEntity Clone()
        {
            RevelationDrawEntity entity = new RevelationDrawEntity();
			entity.DrawId = this.DrawId;
			entity.ManagerId = this.ManagerId;
			entity.MarkId = this.MarkId;
			entity.Schedule = this.Schedule;
			entity.AllItemString = this.AllItemString;
			entity.PrizeItemString = this.PrizeItemString;
			entity.OpenNumber = this.OpenNumber;
			entity.Status = this.Status;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_Draw 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationDrawResponse : BaseResponse<RevelationDrawEntity>
    {

    }
}
