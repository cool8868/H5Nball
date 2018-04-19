
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Statistic_Detail 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class StatisticDetailEntity
	{
		
		public StatisticDetailEntity()
		{
		}

		public StatisticDetailEntity(
		System.Int64 idx
,				System.Int32 zoneid
,				System.Int32 analysetype
,				System.DateTime recorddate
,				System.Int32 totalvalue
,				System.Int32 minvalue
,				System.DateTime mintime
,				System.Int32 maxvalue
,				System.DateTime maxtime
,				System.Int32 hour0
,				System.Int32 hour1
,				System.Int32 hour2
,				System.Int32 hour3
,				System.Int32 hour4
,				System.Int32 hour5
,				System.Int32 hour6
,				System.Int32 hour7
,				System.Int32 hour8
,				System.Int32 hour9
,				System.Int32 hour10
,				System.Int32 hour11
,				System.Int32 hour12
,				System.Int32 hour13
,				System.Int32 hour14
,				System.Int32 hour15
,				System.Int32 hour16
,				System.Int32 hour17
,				System.Int32 hour18
,				System.Int32 hour19
,				System.Int32 hour20
,				System.Int32 hour21
,				System.Int32 hour22
,				System.Int32 hour23
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ZoneId = zoneid;
			this.AnalyseType = analysetype;
			this.RecordDate = recorddate;
			this.TotalValue = totalvalue;
			this.MinValue = minvalue;
			this.MinTime = mintime;
			this.MaxValue = maxvalue;
			this.MaxTime = maxtime;
			this.Hour0 = hour0;
			this.Hour1 = hour1;
			this.Hour2 = hour2;
			this.Hour3 = hour3;
			this.Hour4 = hour4;
			this.Hour5 = hour5;
			this.Hour6 = hour6;
			this.Hour7 = hour7;
			this.Hour8 = hour8;
			this.Hour9 = hour9;
			this.Hour10 = hour10;
			this.Hour11 = hour11;
			this.Hour12 = hour12;
			this.Hour13 = hour13;
			this.Hour14 = hour14;
			this.Hour15 = hour15;
			this.Hour16 = hour16;
			this.Hour17 = hour17;
			this.Hour18 = hour18;
			this.Hour19 = hour19;
			this.Hour20 = hour20;
			this.Hour21 = hour21;
			this.Hour22 = hour22;
			this.Hour23 = hour23;
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
		///ZoneId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ZoneId {get ; set ;}

		///<summary>
		///AnalyseType
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 AnalyseType {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///TotalValue
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 TotalValue {get ; set ;}

		///<summary>
		///MinValue
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 MinValue {get ; set ;}

		///<summary>
		///MinTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime MinTime {get ; set ;}

		///<summary>
		///MaxValue
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 MaxValue {get ; set ;}

		///<summary>
		///MaxTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime MaxTime {get ; set ;}

		///<summary>
		///Hour0
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Hour0 {get ; set ;}

		///<summary>
		///Hour1
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Hour1 {get ; set ;}

		///<summary>
		///Hour2
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Hour2 {get ; set ;}

		///<summary>
		///Hour3
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Hour3 {get ; set ;}

		///<summary>
		///Hour4
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 Hour4 {get ; set ;}

		///<summary>
		///Hour5
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Hour5 {get ; set ;}

		///<summary>
		///Hour6
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Hour6 {get ; set ;}

		///<summary>
		///Hour7
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 Hour7 {get ; set ;}

		///<summary>
		///Hour8
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 Hour8 {get ; set ;}

		///<summary>
		///Hour9
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 Hour9 {get ; set ;}

		///<summary>
		///Hour10
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 Hour10 {get ; set ;}

		///<summary>
		///Hour11
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 Hour11 {get ; set ;}

		///<summary>
		///Hour12
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 Hour12 {get ; set ;}

		///<summary>
		///Hour13
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 Hour13 {get ; set ;}

		///<summary>
		///Hour14
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 Hour14 {get ; set ;}

		///<summary>
		///Hour15
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int32 Hour15 {get ; set ;}

		///<summary>
		///Hour16
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Int32 Hour16 {get ; set ;}

		///<summary>
		///Hour17
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Int32 Hour17 {get ; set ;}

		///<summary>
		///Hour18
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Int32 Hour18 {get ; set ;}

		///<summary>
		///Hour19
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Int32 Hour19 {get ; set ;}

		///<summary>
		///Hour20
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.Int32 Hour20 {get ; set ;}

		///<summary>
		///Hour21
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.Int32 Hour21 {get ; set ;}

		///<summary>
		///Hour22
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.Int32 Hour22 {get ; set ;}

		///<summary>
		///Hour23
		///</summary>
        [DataMember]
        [ProtoMember(33)]
		public System.Int32 Hour23 {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(34)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public StatisticDetailEntity Clone()
        {
            StatisticDetailEntity entity = new StatisticDetailEntity();
			entity.Idx = this.Idx;
			entity.ZoneId = this.ZoneId;
			entity.AnalyseType = this.AnalyseType;
			entity.RecordDate = this.RecordDate;
			entity.TotalValue = this.TotalValue;
			entity.MinValue = this.MinValue;
			entity.MinTime = this.MinTime;
			entity.MaxValue = this.MaxValue;
			entity.MaxTime = this.MaxTime;
			entity.Hour0 = this.Hour0;
			entity.Hour1 = this.Hour1;
			entity.Hour2 = this.Hour2;
			entity.Hour3 = this.Hour3;
			entity.Hour4 = this.Hour4;
			entity.Hour5 = this.Hour5;
			entity.Hour6 = this.Hour6;
			entity.Hour7 = this.Hour7;
			entity.Hour8 = this.Hour8;
			entity.Hour9 = this.Hour9;
			entity.Hour10 = this.Hour10;
			entity.Hour11 = this.Hour11;
			entity.Hour12 = this.Hour12;
			entity.Hour13 = this.Hour13;
			entity.Hour14 = this.Hour14;
			entity.Hour15 = this.Hour15;
			entity.Hour16 = this.Hour16;
			entity.Hour17 = this.Hour17;
			entity.Hour18 = this.Hour18;
			entity.Hour19 = this.Hour19;
			entity.Hour20 = this.Hour20;
			entity.Hour21 = this.Hour21;
			entity.Hour22 = this.Hour22;
			entity.Hour23 = this.Hour23;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Statistic_Detail 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class StatisticDetailResponse : BaseResponse<StatisticDetailEntity>
    {

    }
}
