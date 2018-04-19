
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_MatchStat 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbMatchstatEntity
	{
		
		public NbMatchstatEntity()
		{
		}

		public NbMatchstatEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 matchtype
,				System.Int32 win
,				System.Int32 lose
,				System.Int32 draw
,				System.Int32 goals
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.MatchType = matchtype;
			this.Win = win;
			this.Lose = lose;
			this.Draw = draw;
			this.Goals = goals;
			this.UpdateTime = updatetime;
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
		///MatchType
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MatchType {get ; set ;}

		///<summary>
		///Win
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Win {get ; set ;}

		///<summary>
		///Lose
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Lose {get ; set ;}

		///<summary>
		///Draw
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Draw {get ; set ;}

		///<summary>
		///进球数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Goals {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public NbMatchstatEntity Clone()
        {
            NbMatchstatEntity entity = new NbMatchstatEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.MatchType = this.MatchType;
			entity.Win = this.Win;
			entity.Lose = this.Lose;
			entity.Draw = this.Draw;
			entity.Goals = this.Goals;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_MatchStat 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbMatchstatResponse : BaseResponse<NbMatchstatEntity>
    {

    }
}

