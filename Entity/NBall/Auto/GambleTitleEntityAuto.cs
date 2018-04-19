
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gamble_Title 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GambleTitleEntity
	{
		
		public GambleTitleEntity()
		{
		}

		public GambleTitleEntity(
		System.Guid idx
,				System.String title
,				System.Int32 isofficial
,				System.Guid resultflagid
,				System.DateTime starttime
,				System.DateTime stoptime
,				System.DateTime opentime
,				System.Int32 currentcount
,				System.Int32 status
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.Idx = idx;
			this.Title = title;
			this.IsOfficial = isofficial;
			this.ResultFlagId = resultflagid;
			this.StartTime = starttime;
			this.StopTime = stoptime;
			this.OpenTime = opentime;
			this.CurrentCount = currentcount;
			this.Status = status;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///竞猜场次主题
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Title {get ; set ;}

		///<summary>
		///是否只能官方参与
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 IsOfficial {get ; set ;}

		///<summary>
		///最终胜负选项
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid ResultFlagId {get ; set ;}

		///<summary>
		///开始竞猜时间
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///竞猜截至时间
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime StopTime {get ; set ;}

		///<summary>
		///开奖时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime OpenTime {get ; set ;}

		///<summary>
		///当前参与人数（最多3人）
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 CurrentCount {get ; set ;}

		///<summary>
		///状态，0为初始，1为开奖中,2为已开奖
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///创建时间
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///Version
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public GambleTitleEntity Clone()
        {
            GambleTitleEntity entity = new GambleTitleEntity();
			entity.Idx = this.Idx;
			entity.Title = this.Title;
			entity.IsOfficial = this.IsOfficial;
			entity.ResultFlagId = this.ResultFlagId;
			entity.StartTime = this.StartTime;
			entity.StopTime = this.StopTime;
			entity.OpenTime = this.OpenTime;
			entity.CurrentCount = this.CurrentCount;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gamble_Title 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GambleTitleResponse : BaseResponse<GambleTitleEntity>
    {

    }
}
