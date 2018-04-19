
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_Main 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationMainEntity
	{
		
		public RevelationMainEntity()
		{
		}

		public RevelationMainEntity(
		System.Guid managerid
,				System.Int32 challengesnums
,				System.Int32 buythenumber
,				System.Int32 courage
,				System.DateTime onhookcd
,				System.DateTime failcd
,				System.DateTime refreshtime
,				System.Int32 states
,				System.Byte[] rowversion
		)
		{
			this.ManagerId = managerid;
			this.ChallengesNums = challengesnums;
			this.BuyTheNumber = buythenumber;
			this.Courage = courage;
			this.OnHookCD = onhookcd;
			this.FailCD = failcd;
			this.RefreshTime = refreshtime;
			this.States = states;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///今天挑战次数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ChallengesNums {get ; set ;}

		///<summary>
		///购买次数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 BuyTheNumber {get ; set ;}

		///<summary>
		///勇气值
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Courage {get ; set ;}

		///<summary>
		///挂机CD时间
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime OnHookCD {get ; set ;}

		///<summary>
		///挑战失败CD时间
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime FailCD {get ; set ;}

		///<summary>
		///刷新时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.DateTime RefreshTime {get ; set ;}

		///<summary>
		///States
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 States {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationMainEntity Clone()
        {
            RevelationMainEntity entity = new RevelationMainEntity();
			entity.ManagerId = this.ManagerId;
			entity.ChallengesNums = this.ChallengesNums;
			entity.BuyTheNumber = this.BuyTheNumber;
			entity.Courage = this.Courage;
			entity.OnHookCD = this.OnHookCD;
			entity.FailCD = this.FailCD;
			entity.RefreshTime = this.RefreshTime;
			entity.States = this.States;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_Main 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationMainResponse : BaseResponse<RevelationMainEntity>
    {

    }
}

