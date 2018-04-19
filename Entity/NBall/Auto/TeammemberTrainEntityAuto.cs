
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Teammember_Train 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TeammemberTrainEntity
	{
		
		public TeammemberTrainEntity()
		{
		}

		public TeammemberTrainEntity(
		System.Guid idx
,				System.Guid managerid
,				System.Int32 playerid
,				System.Int32 level
,				System.Int32 exp
,				System.Int32 trainstamina
,				System.Int32 trainstate
,				System.DateTime starttime
,				System.DateTime settletime
,				System.Int32 status
,				System.DateTime rowtime
,				System.Int32 traintype
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.PlayerId = playerid;
			this.Level = level;
			this.EXP = exp;
			this.TrainStamina = trainstamina;
			this.TrainState = trainstate;
			this.StartTime = starttime;
			this.SettleTime = settletime;
			this.Status = status;
			this.RowTime = rowtime;
			this.TrainType = traintype;
		}
		
		#region Public Properties
		
		///<summary>
		///球员唯一id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///经理id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///pid
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PlayerId {get ; set ;}

		///<summary>
		///球员训练等级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///球员训练经验
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 EXP {get ; set ;}

		///<summary>
		///球员训练体力
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 TrainStamina {get ; set ;}

		///<summary>
		///训练状态：0,初始;1,正在训练;2,恢复体力
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 TrainState {get ; set ;}

		///<summary>
		///开始训练时间/开始恢复体力时间
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///上一次结算时间
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime SettleTime {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///TrainType
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 TrainType {get ; set ;}
		#endregion
        
        #region Clone
        public TeammemberTrainEntity Clone()
        {
            TeammemberTrainEntity entity = new TeammemberTrainEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.PlayerId = this.PlayerId;
			entity.Level = this.Level;
			entity.EXP = this.EXP;
			entity.TrainStamina = this.TrainStamina;
			entity.TrainState = this.TrainState;
			entity.StartTime = this.StartTime;
			entity.SettleTime = this.SettleTime;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.TrainType = this.TrainType;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Teammember_Train 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TeammemberTrainResponse : BaseResponse<TeammemberTrainEntity>
    {

    }
}

