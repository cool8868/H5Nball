
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.DailyCup_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DailycupInfoEntity
	{
		
		public DailycupInfoEntity()
		{
		}

		public DailycupInfoEntity(
		System.Int32 idx
,				System.Int32 round
,				System.Int32 opengambleround
,				System.DateTime attenddate
,				System.DateTime rundate
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.Round = round;
			this.OpenGambleRound = opengambleround;
			this.AttendDate = attenddate;
			this.RunDate = rundate;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///第几届
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///该杯赛共有几轮
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Round {get ; set ;}

		///<summary>
		///开奖到第几轮了
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 OpenGambleRound {get ; set ;}

		///<summary>
		///报名日期
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime AttendDate {get ; set ;}

		///<summary>
		///运行日期
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RunDate {get ; set ;}

		///<summary>
		///0，初始；1，报名截止；2，比赛结束；3，已发送金币奖励；4，已发送积分奖励
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Status {get ; set ;}

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
        public DailycupInfoEntity Clone()
        {
            DailycupInfoEntity entity = new DailycupInfoEntity();
			entity.Idx = this.Idx;
			entity.Round = this.Round;
			entity.OpenGambleRound = this.OpenGambleRound;
			entity.AttendDate = this.AttendDate;
			entity.RunDate = this.RunDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.DailyCup_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DailycupInfoResponse : BaseResponse<DailycupInfoEntity>
    {

    }
}

