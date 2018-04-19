
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Statistic_Online 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class StatisticOnlineEntity
	{
		
		public StatisticOnlineEntity()
		{
		}

		public StatisticOnlineEntity(
		System.Int64 idx
,				System.Int32 zoneid
,				System.DateTime recorddate
,				System.Int64 totalminutes
,				System.Int32 totalvalue
,				System.Int32 recordcount
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
,				System.Int32 count0
,				System.Int32 count1
,				System.Int32 count2
,				System.Int32 count3
,				System.Int32 count4
,				System.Int32 count5
,				System.Int32 count6
,				System.Int32 count7
,				System.Int32 count8
,				System.Int32 count9
,				System.Int32 count10
,				System.Int32 count11
,				System.Int32 count12
,				System.Int32 count13
,				System.Int32 count14
,				System.Int32 count15
,				System.Int32 count16
,				System.Int32 count17
,				System.Int32 count18
,				System.Int32 count19
,				System.Int32 count20
,				System.Int32 count21
,				System.Int32 count22
,				System.Int32 count23
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ZoneId = zoneid;
			this.RecordDate = recorddate;
			this.TotalMinutes = totalminutes;
			this.TotalValue = totalvalue;
			this.RecordCount = recordcount;
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
			this.Count0 = count0;
			this.Count1 = count1;
			this.Count2 = count2;
			this.Count3 = count3;
			this.Count4 = count4;
			this.Count5 = count5;
			this.Count6 = count6;
			this.Count7 = count7;
			this.Count8 = count8;
			this.Count9 = count9;
			this.Count10 = count10;
			this.Count11 = count11;
			this.Count12 = count12;
			this.Count13 = count13;
			this.Count14 = count14;
			this.Count15 = count15;
			this.Count16 = count16;
			this.Count17 = count17;
			this.Count18 = count18;
			this.Count19 = count19;
			this.Count20 = count20;
			this.Count21 = count21;
			this.Count22 = count22;
			this.Count23 = count23;
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
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///TotalMinutes
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int64 TotalMinutes {get ; set ;}

		///<summary>
		///TotalValue
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 TotalValue {get ; set ;}

		///<summary>
		///RecordCount
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 RecordCount {get ; set ;}

		///<summary>
		///MinValue
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 MinValue {get ; set ;}

		///<summary>
		///MinTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime MinTime {get ; set ;}

		///<summary>
		///MaxValue
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 MaxValue {get ; set ;}

		///<summary>
		///MaxTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime MaxTime {get ; set ;}

		///<summary>
		///Hour0
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Hour0 {get ; set ;}

		///<summary>
		///Hour1
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Hour1 {get ; set ;}

		///<summary>
		///Hour2
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Hour2 {get ; set ;}

		///<summary>
		///Hour3
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 Hour3 {get ; set ;}

		///<summary>
		///Hour4
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Hour4 {get ; set ;}

		///<summary>
		///Hour5
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Hour5 {get ; set ;}

		///<summary>
		///Hour6
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 Hour6 {get ; set ;}

		///<summary>
		///Hour7
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 Hour7 {get ; set ;}

		///<summary>
		///Hour8
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 Hour8 {get ; set ;}

		///<summary>
		///Hour9
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 Hour9 {get ; set ;}

		///<summary>
		///Hour10
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 Hour10 {get ; set ;}

		///<summary>
		///Hour11
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 Hour11 {get ; set ;}

		///<summary>
		///Hour12
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 Hour12 {get ; set ;}

		///<summary>
		///Hour13
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 Hour13 {get ; set ;}

		///<summary>
		///Hour14
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int32 Hour14 {get ; set ;}

		///<summary>
		///Hour15
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Int32 Hour15 {get ; set ;}

		///<summary>
		///Hour16
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Int32 Hour16 {get ; set ;}

		///<summary>
		///Hour17
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Int32 Hour17 {get ; set ;}

		///<summary>
		///Hour18
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Int32 Hour18 {get ; set ;}

		///<summary>
		///Hour19
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.Int32 Hour19 {get ; set ;}

		///<summary>
		///Hour20
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.Int32 Hour20 {get ; set ;}

		///<summary>
		///Hour21
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.Int32 Hour21 {get ; set ;}

		///<summary>
		///Hour22
		///</summary>
        [DataMember]
        [ProtoMember(33)]
		public System.Int32 Hour22 {get ; set ;}

		///<summary>
		///Hour23
		///</summary>
        [DataMember]
        [ProtoMember(34)]
		public System.Int32 Hour23 {get ; set ;}

		///<summary>
		///Count0
		///</summary>
        [DataMember]
        [ProtoMember(35)]
		public System.Int32 Count0 {get ; set ;}

		///<summary>
		///Count1
		///</summary>
        [DataMember]
        [ProtoMember(36)]
		public System.Int32 Count1 {get ; set ;}

		///<summary>
		///Count2
		///</summary>
        [DataMember]
        [ProtoMember(37)]
		public System.Int32 Count2 {get ; set ;}

		///<summary>
		///Count3
		///</summary>
        [DataMember]
        [ProtoMember(38)]
		public System.Int32 Count3 {get ; set ;}

		///<summary>
		///Count4
		///</summary>
        [DataMember]
        [ProtoMember(39)]
		public System.Int32 Count4 {get ; set ;}

		///<summary>
		///Count5
		///</summary>
        [DataMember]
        [ProtoMember(40)]
		public System.Int32 Count5 {get ; set ;}

		///<summary>
		///Count6
		///</summary>
        [DataMember]
        [ProtoMember(41)]
		public System.Int32 Count6 {get ; set ;}

		///<summary>
		///Count7
		///</summary>
        [DataMember]
        [ProtoMember(42)]
		public System.Int32 Count7 {get ; set ;}

		///<summary>
		///Count8
		///</summary>
        [DataMember]
        [ProtoMember(43)]
		public System.Int32 Count8 {get ; set ;}

		///<summary>
		///Count9
		///</summary>
        [DataMember]
        [ProtoMember(44)]
		public System.Int32 Count9 {get ; set ;}

		///<summary>
		///Count10
		///</summary>
        [DataMember]
        [ProtoMember(45)]
		public System.Int32 Count10 {get ; set ;}

		///<summary>
		///Count11
		///</summary>
        [DataMember]
        [ProtoMember(46)]
		public System.Int32 Count11 {get ; set ;}

		///<summary>
		///Count12
		///</summary>
        [DataMember]
        [ProtoMember(47)]
		public System.Int32 Count12 {get ; set ;}

		///<summary>
		///Count13
		///</summary>
        [DataMember]
        [ProtoMember(48)]
		public System.Int32 Count13 {get ; set ;}

		///<summary>
		///Count14
		///</summary>
        [DataMember]
        [ProtoMember(49)]
		public System.Int32 Count14 {get ; set ;}

		///<summary>
		///Count15
		///</summary>
        [DataMember]
        [ProtoMember(50)]
		public System.Int32 Count15 {get ; set ;}

		///<summary>
		///Count16
		///</summary>
        [DataMember]
        [ProtoMember(51)]
		public System.Int32 Count16 {get ; set ;}

		///<summary>
		///Count17
		///</summary>
        [DataMember]
        [ProtoMember(52)]
		public System.Int32 Count17 {get ; set ;}

		///<summary>
		///Count18
		///</summary>
        [DataMember]
        [ProtoMember(53)]
		public System.Int32 Count18 {get ; set ;}

		///<summary>
		///Count19
		///</summary>
        [DataMember]
        [ProtoMember(54)]
		public System.Int32 Count19 {get ; set ;}

		///<summary>
		///Count20
		///</summary>
        [DataMember]
        [ProtoMember(55)]
		public System.Int32 Count20 {get ; set ;}

		///<summary>
		///Count21
		///</summary>
        [DataMember]
        [ProtoMember(56)]
		public System.Int32 Count21 {get ; set ;}

		///<summary>
		///Count22
		///</summary>
        [DataMember]
        [ProtoMember(57)]
		public System.Int32 Count22 {get ; set ;}

		///<summary>
		///Count23
		///</summary>
        [DataMember]
        [ProtoMember(58)]
		public System.Int32 Count23 {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(59)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public StatisticOnlineEntity Clone()
        {
            StatisticOnlineEntity entity = new StatisticOnlineEntity();
			entity.Idx = this.Idx;
			entity.ZoneId = this.ZoneId;
			entity.RecordDate = this.RecordDate;
			entity.TotalMinutes = this.TotalMinutes;
			entity.TotalValue = this.TotalValue;
			entity.RecordCount = this.RecordCount;
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
			entity.Count0 = this.Count0;
			entity.Count1 = this.Count1;
			entity.Count2 = this.Count2;
			entity.Count3 = this.Count3;
			entity.Count4 = this.Count4;
			entity.Count5 = this.Count5;
			entity.Count6 = this.Count6;
			entity.Count7 = this.Count7;
			entity.Count8 = this.Count8;
			entity.Count9 = this.Count9;
			entity.Count10 = this.Count10;
			entity.Count11 = this.Count11;
			entity.Count12 = this.Count12;
			entity.Count13 = this.Count13;
			entity.Count14 = this.Count14;
			entity.Count15 = this.Count15;
			entity.Count16 = this.Count16;
			entity.Count17 = this.Count17;
			entity.Count18 = this.Count18;
			entity.Count19 = this.Count19;
			entity.Count20 = this.Count20;
			entity.Count21 = this.Count21;
			entity.Count22 = this.Count22;
			entity.Count23 = this.Count23;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Statistic_Online 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class StatisticOnlineResponse : BaseResponse<StatisticOnlineEntity>
    {

    }
}
