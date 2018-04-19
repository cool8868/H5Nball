
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Teammember_Grow 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TeammemberGrowEntity
	{
		
		public TeammemberGrowEntity()
		{
		}

		public TeammemberGrowEntity(
		System.Guid idx
,				System.Guid managerid
,				System.Int32 growlevel
,				System.Int32 grownum
,				System.Int32 daygrowcount
,				System.Int32 dayfastgrowcount
,				System.Int32 dayfreefastgrowcount
,				System.DateTime recorddate
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.GrowLevel = growlevel;
			this.GrowNum = grownum;
			this.DayGrowCount = daygrowcount;
			this.DayFastGrowCount = dayfastgrowcount;
			this.DayFreeFastGrowCount = dayfreefastgrowcount;
			this.RecordDate = recorddate;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///球员ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///球员成长阶段
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 GrowLevel {get ; set ;}

		///<summary>
		///累计成长值
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 GrowNum {get ; set ;}

		///<summary>
		///当天使用普通成长次数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 DayGrowCount {get ; set ;}

		///<summary>
		///当天使用快速成长次数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 DayFastGrowCount {get ; set ;}

		///<summary>
		///当天使用免费快速成长次数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 DayFreeFastGrowCount {get ; set ;}

		///<summary>
		///记录日期
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public TeammemberGrowEntity Clone()
        {
            TeammemberGrowEntity entity = new TeammemberGrowEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.GrowLevel = this.GrowLevel;
			entity.GrowNum = this.GrowNum;
			entity.DayGrowCount = this.DayGrowCount;
			entity.DayFastGrowCount = this.DayFastGrowCount;
			entity.DayFreeFastGrowCount = this.DayFreeFastGrowCount;
			entity.RecordDate = this.RecordDate;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Teammember_Grow 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TeammemberGrowResponse : BaseResponse<TeammemberGrowEntity>
    {

    }
}

