
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ActivityPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicActivityprizeEntity
	{
		
		public DicActivityprizeEntity()
		{
		}

		public DicActivityprizeEntity(
		System.Int32 idx
,				System.Int32 activityid
,				System.Int32 activitystep
,				System.Int32 prizetype
,				System.Int32 subtype
,				System.Int32 count
,				System.Int32 strength
,				System.Boolean isbinding
		)
		{
			this.Idx = idx;
			this.ActivityId = activityid;
			this.ActivityStep = activitystep;
			this.PrizeType = prizetype;
			this.SubType = subtype;
			this.Count = count;
			this.Strength = strength;
			this.IsBinding = isbinding;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///活动id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ActivityId {get ; set ;}

		///<summary>
		///活动步骤
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ActivityStep {get ; set ;}

		///<summary>
		///奖励类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///二级类型
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///强化级别
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Strength {get ; set ;}

		///<summary>
		///是否绑定
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean IsBinding {get ; set ;}
		#endregion
        
        #region Clone
        public DicActivityprizeEntity Clone()
        {
            DicActivityprizeEntity entity = new DicActivityprizeEntity();
			entity.Idx = this.Idx;
			entity.ActivityId = this.ActivityId;
			entity.ActivityStep = this.ActivityStep;
			entity.PrizeType = this.PrizeType;
			entity.SubType = this.SubType;
			entity.Count = this.Count;
			entity.Strength = this.Strength;
			entity.IsBinding = this.IsBinding;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ActivityPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicActivityprizeResponse : BaseResponse<DicActivityprizeEntity>
    {

    }
}

