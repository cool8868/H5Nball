
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Statistic_Kpi 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class StatisticKpiEntity
	{
		
		public StatisticKpiEntity()
		{
		}

		public StatisticKpiEntity(
		System.Int64 idx
,				System.Int32 zoneid
,				System.String recordmonth
,				System.DateTime recorddate
,				System.Int32 totaluser
,				System.Int32 totalmanager
,				System.Int32 dau
,				System.Int32 duniqueip
,				System.Int32 dnewuser
,				System.Int32 dnewmanager
,				System.Int32 dlostuser7
,				System.Int32 dlostuser15
,				System.Int32 dlostuser30
,				System.Int32 retention2
,				System.Int32 retention3
,				System.Int32 retention4
,				System.Int32 retention5
,				System.Int32 retention6
,				System.Int32 retention7
,				System.Int32 retention15
,				System.Int32 retention30
,				System.Int32 acu
,				System.Int32 pcu
,				System.Int32 lcu
,				System.Int64 totalonline
,				System.Int32 wau
,				System.Int32 wlost
,				System.Int32 whonor
,				System.Int32 whonorlost
,				System.Int32 mau
,				System.Int32 payusercount
,				System.Int32 paycount
,				System.Int32 paytotal
,				System.Int64 paysum
,				System.Int32 payfirst
,				System.Int64 pointremain
,				System.Int64 pointconsume
,				System.Int64 pointcirculate
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Int32 getpoint
,				System.Int64 getcoin
,				System.Int64 coinconsume
,				System.Int32 energyconsume
		)
		{
			this.Idx = idx;
			this.ZoneId = zoneid;
			this.RecordMonth = recordmonth;
			this.RecordDate = recorddate;
			this.TotalUser = totaluser;
			this.TotalManager = totalmanager;
			this.Dau = dau;
			this.DUniqueIp = duniqueip;
			this.DNewUser = dnewuser;
			this.DNewManager = dnewmanager;
			this.DLostUser7 = dlostuser7;
			this.DLostUser15 = dlostuser15;
			this.DLostUser30 = dlostuser30;
			this.Retention2 = retention2;
			this.Retention3 = retention3;
			this.Retention4 = retention4;
			this.Retention5 = retention5;
			this.Retention6 = retention6;
			this.Retention7 = retention7;
			this.Retention15 = retention15;
			this.Retention30 = retention30;
			this.Acu = acu;
			this.Pcu = pcu;
			this.Lcu = lcu;
			this.TotalOnline = totalonline;
			this.Wau = wau;
			this.WLost = wlost;
			this.WHonor = whonor;
			this.WHonorLost = whonorlost;
			this.Mau = mau;
			this.PayUserCount = payusercount;
			this.PayCount = paycount;
			this.PayTotal = paytotal;
			this.PaySum = paysum;
			this.PayFirst = payfirst;
			this.PointRemain = pointremain;
			this.PointConsume = pointconsume;
			this.PointCirculate = pointcirculate;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.GetPoint = getpoint;
			this.GetCoin = getcoin;
			this.CoinConsume = coinconsume;
			this.EnergyConsume = energyconsume;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int64 Idx {get ; set ;}

		///<summary>
		///ZoneId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ZoneId {get ; set ;}

		///<summary>
		///所属月份
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String RecordMonth {get ; set ;}

		///<summary>
		///统计日期
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///TotalUser
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 TotalUser {get ; set ;}

		///<summary>
		///TotalManager
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 TotalManager {get ; set ;}

		///<summary>
		///日活跃用户
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Dau {get ; set ;}

		///<summary>
		///唯一Ip数
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 DUniqueIp {get ; set ;}

		///<summary>
		///新用户
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 DNewUser {get ; set ;}

		///<summary>
		///新经理
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 DNewManager {get ; set ;}

		///<summary>
		///流失用户7天
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 DLostUser7 {get ; set ;}

		///<summary>
		///流失用户15
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 DLostUser15 {get ; set ;}

		///<summary>
		///流失用户30
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 DLostUser30 {get ; set ;}

		///<summary>
		///次日留存
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 Retention2 {get ; set ;}

		///<summary>
		///3日留存
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Retention3 {get ; set ;}

		///<summary>
		///Retention4
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Retention4 {get ; set ;}

		///<summary>
		///Retention5
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 Retention5 {get ; set ;}

		///<summary>
		///Retention6
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 Retention6 {get ; set ;}

		///<summary>
		///Retention7
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 Retention7 {get ; set ;}

		///<summary>
		///Retention15
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 Retention15 {get ; set ;}

		///<summary>
		///Retention30
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 Retention30 {get ; set ;}

		///<summary>
		///平均同时在线
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 Acu {get ; set ;}

		///<summary>
		///最高同时在线
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 Pcu {get ; set ;}

		///<summary>
		///Lcu
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 Lcu {get ; set ;}

		///<summary>
		///总在线时长
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int64 TotalOnline {get ; set ;}

		///<summary>
		///周活跃用户
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Int32 Wau {get ; set ;}

		///<summary>
		///周流失用户
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Int32 WLost {get ; set ;}

		///<summary>
		///周忠诚用户数
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Int32 WHonor {get ; set ;}

		///<summary>
		///WHonorLost
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Int32 WHonorLost {get ; set ;}

		///<summary>
		///月活跃用户
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.Int32 Mau {get ; set ;}

		///<summary>
		///充值人数
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.Int32 PayUserCount {get ; set ;}

		///<summary>
		///PayCount
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.Int32 PayCount {get ; set ;}

		///<summary>
		///充值金额
		///</summary>
        [DataMember]
        [ProtoMember(33)]
		public System.Int32 PayTotal {get ; set ;}

		///<summary>
		///该服务器总充值
		///</summary>
        [DataMember]
        [ProtoMember(34)]
		public System.Int64 PaySum {get ; set ;}

		///<summary>
		///首充人数
		///</summary>
        [DataMember]
        [ProtoMember(35)]
		public System.Int32 PayFirst {get ; set ;}

		///<summary>
		///剩余点券
		///</summary>
        [DataMember]
        [ProtoMember(36)]
		public System.Int64 PointRemain {get ; set ;}

		///<summary>
		///消耗点券
		///</summary>
        [DataMember]
        [ProtoMember(37)]
		public System.Int64 PointConsume {get ; set ;}

		///<summary>
		///流通点券
		///</summary>
        [DataMember]
        [ProtoMember(38)]
		public System.Int64 PointCirculate {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(39)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(40)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///GetPoint
		///</summary>
        [DataMember]
        [ProtoMember(41)]
		public System.Int32 GetPoint {get ; set ;}

		///<summary>
		///GetCoin
		///</summary>
        [DataMember]
        [ProtoMember(42)]
		public System.Int64 GetCoin {get ; set ;}

		///<summary>
		///CoinConsume
		///</summary>
        [DataMember]
        [ProtoMember(43)]
		public System.Int64 CoinConsume {get ; set ;}

		///<summary>
		///EnergyConsume
		///</summary>
        [DataMember]
        [ProtoMember(44)]
		public System.Int32 EnergyConsume {get ; set ;}
		#endregion
        
        #region Clone
        public StatisticKpiEntity Clone()
        {
            StatisticKpiEntity entity = new StatisticKpiEntity();
			entity.Idx = this.Idx;
			entity.ZoneId = this.ZoneId;
			entity.RecordMonth = this.RecordMonth;
			entity.RecordDate = this.RecordDate;
			entity.TotalUser = this.TotalUser;
			entity.TotalManager = this.TotalManager;
			entity.Dau = this.Dau;
			entity.DUniqueIp = this.DUniqueIp;
			entity.DNewUser = this.DNewUser;
			entity.DNewManager = this.DNewManager;
			entity.DLostUser7 = this.DLostUser7;
			entity.DLostUser15 = this.DLostUser15;
			entity.DLostUser30 = this.DLostUser30;
			entity.Retention2 = this.Retention2;
			entity.Retention3 = this.Retention3;
			entity.Retention4 = this.Retention4;
			entity.Retention5 = this.Retention5;
			entity.Retention6 = this.Retention6;
			entity.Retention7 = this.Retention7;
			entity.Retention15 = this.Retention15;
			entity.Retention30 = this.Retention30;
			entity.Acu = this.Acu;
			entity.Pcu = this.Pcu;
			entity.Lcu = this.Lcu;
			entity.TotalOnline = this.TotalOnline;
			entity.Wau = this.Wau;
			entity.WLost = this.WLost;
			entity.WHonor = this.WHonor;
			entity.WHonorLost = this.WHonorLost;
			entity.Mau = this.Mau;
			entity.PayUserCount = this.PayUserCount;
			entity.PayCount = this.PayCount;
			entity.PayTotal = this.PayTotal;
			entity.PaySum = this.PaySum;
			entity.PayFirst = this.PayFirst;
			entity.PointRemain = this.PointRemain;
			entity.PointConsume = this.PointConsume;
			entity.PointCirculate = this.PointCirculate;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.GetPoint = this.GetPoint;
			entity.GetCoin = this.GetCoin;
			entity.CoinConsume = this.CoinConsume;
			entity.EnergyConsume = this.EnergyConsume;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Statistic_Kpi 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class StatisticKpiResponse : BaseResponse<StatisticKpiEntity>
    {

    }
}
