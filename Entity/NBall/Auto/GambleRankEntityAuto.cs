
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gamble_Rank 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GambleRankEntity
	{
		
		public GambleRankEntity()
		{
		}

		public GambleRankEntity(
		System.Guid managerid
,				System.String managername
,				System.Int32 rankindex
,				System.Int32 ranktype
,				System.Int32 wintotalmoney
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.ManagerName = managername;
			this.RankIndex = rankindex;
			this.RankType = ranktype;
			this.WinTotalMoney = wintotalmoney;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///经理名
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///RankIndex
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 RankIndex {get ; set ;}

		///<summary>
		///RankType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 RankType {get ; set ;}

		///<summary>
		///WinTotalMoney
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 WinTotalMoney {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public GambleRankEntity Clone()
        {
            GambleRankEntity entity = new GambleRankEntity();
			entity.ManagerId = this.ManagerId;
			entity.ManagerName = this.ManagerName;
			entity.RankIndex = this.RankIndex;
			entity.RankType = this.RankType;
			entity.WinTotalMoney = this.WinTotalMoney;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gamble_Rank 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GambleRankResponse : BaseResponse<GambleRankEntity>
    {

    }
}
