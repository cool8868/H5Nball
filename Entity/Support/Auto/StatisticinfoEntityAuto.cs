
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Statistic_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class StatisticInfoEntity
	{
		
		public StatisticInfoEntity()
		{
		}

		public StatisticInfoEntity(
		System.Int32 zoneid
,				System.Int32 totaluser
,				System.Int32 totalmanager
,				System.Int64 totalpay
,				System.Int64 pointremain
,				System.Int32 pcu
,				System.Int32 acu
,				System.Int64 onlineminutes
,				System.DateTime updatetime
		)
		{
			this.ZoneId = zoneid;
			this.TotalUser = totaluser;
			this.TotalManager = totalmanager;
			this.TotalPay = totalpay;
			this.PointRemain = pointremain;
			this.Pcu = pcu;
			this.Acu = acu;
			this.OnlineMinutes = onlineminutes;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///区id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 ZoneId {get ; set ;}

		///<summary>
		///用户数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TotalUser {get ; set ;}

		///<summary>
		///经理数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 TotalManager {get ; set ;}

		///<summary>
		///充值数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int64 TotalPay {get ; set ;}

		///<summary>
		///剩余点券
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int64 PointRemain {get ; set ;}

		///<summary>
		///Pcu
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Pcu {get ; set ;}

		///<summary>
		///Acu
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Acu {get ; set ;}

		///<summary>
		///总在线时长
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int64 OnlineMinutes {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public StatisticInfoEntity Clone()
        {
            StatisticInfoEntity entity = new StatisticInfoEntity();
			entity.ZoneId = this.ZoneId;
			entity.TotalUser = this.TotalUser;
			entity.TotalManager = this.TotalManager;
			entity.TotalPay = this.TotalPay;
			entity.PointRemain = this.PointRemain;
			entity.Pcu = this.Pcu;
			entity.Acu = this.Acu;
			entity.OnlineMinutes = this.OnlineMinutes;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Statistic_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class StatisticInfoResponse : BaseResponse<StatisticInfoEntity>
    {

    }
}
