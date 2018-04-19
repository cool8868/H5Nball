
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_HistoryOfTheGap 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationHistoryofthegapEntity
	{
		
		public RevelationHistoryofthegapEntity()
		{
		}

		public RevelationHistoryofthegapEntity(
		System.Int32 customspass
,				System.Int32 schedule
,				System.String managername
,				System.Int32 goals
,				System.Int32 toconcede
,				System.Int32 historyofthegap
,				System.Int32 states
,				System.DateTime updatetime
		)
		{
			this.CustomsPass = customspass;
			this.Schedule = schedule;
			this.ManagerName = managername;
			this.Goals = goals;
			this.ToConcede = toconcede;
			this.HistoryOfTheGap = historyofthegap;
			this.States = states;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///关卡
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 CustomsPass {get ; set ;}

		///<summary>
		///进度
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///经理名
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///进球数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Goals {get ; set ;}

		///<summary>
		///失球数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ToConcede {get ; set ;}

		///<summary>
		///最大分差
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 HistoryOfTheGap {get ; set ;}

		///<summary>
		///States
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 States {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationHistoryofthegapEntity Clone()
        {
            RevelationHistoryofthegapEntity entity = new RevelationHistoryofthegapEntity();
			entity.CustomsPass = this.CustomsPass;
			entity.Schedule = this.Schedule;
			entity.ManagerName = this.ManagerName;
			entity.Goals = this.Goals;
			entity.ToConcede = this.ToConcede;
			entity.HistoryOfTheGap = this.HistoryOfTheGap;
			entity.States = this.States;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_HistoryOfTheGap 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationHistoryofthegapResponse : BaseResponse<RevelationHistoryofthegapEntity>
    {

    }
}
