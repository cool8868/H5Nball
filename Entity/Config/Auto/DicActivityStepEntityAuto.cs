
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ActivityStep 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicActivitystepEntity
	{
		
		public DicActivitystepEntity()
		{
		}

		public DicActivitystepEntity(
		System.Int32 idx
,				System.Int32 activityid
,				System.Int32 activitystep
,				System.String condition
,				System.String description
		)
		{
			this.Idx = idx;
			this.ActivityId = activityid;
			this.ActivityStep = activitystep;
			this.Condition = condition;
			this.Description = description;
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
		///条件
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Condition {get ; set ;}

		///<summary>
		///活动步骤描述
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public DicActivitystepEntity Clone()
        {
            DicActivitystepEntity entity = new DicActivitystepEntity();
			entity.Idx = this.Idx;
			entity.ActivityId = this.ActivityId;
			entity.ActivityStep = this.ActivityStep;
			entity.Condition = this.Condition;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ActivityStep 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicActivitystepResponse : BaseResponse<DicActivitystepEntity>
    {

    }
}

