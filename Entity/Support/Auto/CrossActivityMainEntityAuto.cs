
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossActivity_Main 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossactivityMainEntity
	{
		
		public CrossactivityMainEntity()
		{
		}

		public CrossactivityMainEntity(
		System.Int32 idx
,				System.Int32 domainid
,				System.DateTime starttime
,				System.DateTime endtime
,				System.Int32 goldbarnumber
,				System.DateTime goldbarrefresh
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.DomainId = domainid;
			this.StartTime = starttime;
			this.EndTime = endtime;
			this.GoldBarNumber = goldbarnumber;
			this.GoldBarRefresh = goldbarrefresh;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///跨服域
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DomainId {get ; set ;}

		///<summary>
		///活动开始时间
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///活动结束时间
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///剩余金条数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 GoldBarNumber {get ; set ;}

		///<summary>
		///金条刷新时间间隔
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime GoldBarRefresh {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrossactivityMainEntity Clone()
        {
            CrossactivityMainEntity entity = new CrossactivityMainEntity();
			entity.Idx = this.Idx;
			entity.DomainId = this.DomainId;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.GoldBarNumber = this.GoldBarNumber;
			entity.GoldBarRefresh = this.GoldBarRefresh;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossActivity_Main 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossactivityMainResponse : BaseResponse<CrossactivityMainEntity>
    {

    }
}
