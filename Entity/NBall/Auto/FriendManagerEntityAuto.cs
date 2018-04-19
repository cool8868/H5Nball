
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Friend_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class FriendManagerEntity
	{
		
		public FriendManagerEntity()
		{
		}

		public FriendManagerEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Guid friendid
,				System.Int32 intimacy
,				System.Int32 dayintimacy
,				System.Int32 helptraincount
,				System.Int32 dayhelptraincount
,				System.Int32 dayopenboxcount
,				System.Int32 matchcount
,				System.Int32 daymatchcount
,				System.DateTime recorddate
,				System.Int32 status
,				System.DateTime rowtime
,				System.Byte[] rowversion
,				System.DateTime openboxtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.FriendId = friendid;
			this.Intimacy = intimacy;
			this.DayIntimacy = dayintimacy;
			this.HelpTrainCount = helptraincount;
			this.DayHelpTrainCount = dayhelptraincount;
			this.DayOpenBoxCount = dayopenboxcount;
			this.MatchCount = matchcount;
			this.DayMatchCount = daymatchcount;
			this.RecordDate = recorddate;
			this.Status = status;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
			this.OpenBoxTime = openboxtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///经理id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///好友id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid FriendId {get ; set ;}

		///<summary>
		///亲密度
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Intimacy {get ; set ;}

		///<summary>
		///今日增加亲密度
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 DayIntimacy {get ; set ;}

		///<summary>
		///帮助好友训练次数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 HelpTrainCount {get ; set ;}

		///<summary>
		///今天帮助好友训练次数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 DayHelpTrainCount {get ; set ;}

		///<summary>
		///DayOpenBoxCount
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 DayOpenBoxCount {get ; set ;}

		///<summary>
		///好友对战次数
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 MatchCount {get ; set ;}

		///<summary>
		///今天好友对战次数
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 DayMatchCount {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///状态,0:表示好友，1：表示黑名单，2：表示被对方加入黑名单
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///时间
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///时间戳
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}

		///<summary>
		///OpenBoxTime
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime OpenBoxTime {get ; set ;}
		#endregion
        
        #region Clone
        public FriendManagerEntity Clone()
        {
            FriendManagerEntity entity = new FriendManagerEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.FriendId = this.FriendId;
			entity.Intimacy = this.Intimacy;
			entity.DayIntimacy = this.DayIntimacy;
			entity.HelpTrainCount = this.HelpTrainCount;
			entity.DayHelpTrainCount = this.DayHelpTrainCount;
			entity.DayOpenBoxCount = this.DayOpenBoxCount;
			entity.MatchCount = this.MatchCount;
			entity.DayMatchCount = this.DayMatchCount;
			entity.RecordDate = this.RecordDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
			entity.OpenBoxTime = this.OpenBoxTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Friend_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class FriendManagerResponse : BaseResponse<FriendManagerEntity>
    {

    }
}

