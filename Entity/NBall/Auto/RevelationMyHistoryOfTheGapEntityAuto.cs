
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_MyHistoryOfTheGap 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationMyhistoryofthegapEntity
	{
		
		public RevelationMyhistoryofthegapEntity()
		{
		}

		public RevelationMyhistoryofthegapEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 mark
,				System.Int32 schedule
,				System.Int32 goals
,				System.Int32 toconcede
,				System.Int32 historyofthegap
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.Mark = mark;
			this.Schedule = schedule;
			this.Goals = goals;
			this.ToConcede = toconcede;
			this.HistoryOfTheGap = historyofthegap;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///关卡ID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Mark {get ; set ;}

		///<summary>
		///小关卡ID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///进球数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Goals {get ; set ;}

		///<summary>
		///失球数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ToConcede {get ; set ;}

		///<summary>
		///最大分差
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 HistoryOfTheGap {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationMyhistoryofthegapEntity Clone()
        {
            RevelationMyhistoryofthegapEntity entity = new RevelationMyhistoryofthegapEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.Mark = this.Mark;
			entity.Schedule = this.Schedule;
			entity.Goals = this.Goals;
			entity.ToConcede = this.ToConcede;
			entity.HistoryOfTheGap = this.HistoryOfTheGap;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_MyHistoryOfTheGap 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationMyhistoryofthegapResponse : BaseResponse<RevelationMyhistoryofthegapEntity>
    {

    }
}

