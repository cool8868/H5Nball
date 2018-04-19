
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Europe_GambleRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class EuropeGamblerecordEntity
	{
		
		public EuropeGamblerecordEntity()
		{
		}

		public EuropeGamblerecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 matchid
,				System.Int32 gambletype
,				System.Int32 point
,				System.Int32 returnpoint
,				System.Boolean issendprize
,				System.Boolean isgamblecorrect
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.MatchId = matchid;
			this.GambleType = gambletype;
			this.Point = point;
			this.ReturnPoint = returnpoint;
			this.IsSendPrize = issendprize;
			this.IsGambleCorrect = isgamblecorrect;
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
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///MatchId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MatchId {get ; set ;}

		///<summary>
		///竞猜类型 1主队胜 2平 3客队胜
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 GambleType {get ; set ;}

		///<summary>
		///押注点卷
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Point {get ; set ;}

		///<summary>
		///ReturnPoint
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ReturnPoint {get ; set ;}

		///<summary>
		///是否发奖
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsSendPrize {get ; set ;}

		///<summary>
		///是否竞猜正确
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean IsGambleCorrect {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public EuropeGamblerecordEntity Clone()
        {
            EuropeGamblerecordEntity entity = new EuropeGamblerecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.MatchId = this.MatchId;
			entity.GambleType = this.GambleType;
			entity.Point = this.Point;
			entity.ReturnPoint = this.ReturnPoint;
			entity.IsSendPrize = this.IsSendPrize;
			entity.IsGambleCorrect = this.IsGambleCorrect;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Europe_GambleRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class EuropeGamblerecordResponse : BaseResponse<EuropeGamblerecordEntity>
    {

    }
}

