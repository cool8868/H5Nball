
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Template_ActivityExDetail 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TemplateActivityexdetailEntity
	{
		
		public TemplateActivityexdetailEntity()
		{
		}

		public TemplateActivityexdetailEntity(
		System.Int32 idx
,				System.Int32 excitingid
,				System.Int32 groupid
,				System.Int32 exstep
,				System.Int32 count
,				System.Int32 condition
,				System.Int32 conditionsub
,				System.Int32 effecttype
,				System.Int32 effectrate
,				System.Int32 effectvalue
		)
		{
			this.Idx = idx;
			this.ExcitingId = excitingid;
			this.GroupId = groupid;
			this.ExStep = exstep;
			this.Count = count;
			this.Condition = condition;
			this.ConditionSub = conditionsub;
			this.EffectType = effecttype;
			this.EffectRate = effectrate;
			this.EffectValue = effectvalue;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ExcitingId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ExcitingId {get ; set ;}

		///<summary>
		///GroupId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 GroupId {get ; set ;}

		///<summary>
		///ExStep
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ExStep {get ; set ;}

		///<summary>
		///Count
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///Condition
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Condition {get ; set ;}

		///<summary>
		///ConditionSub
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 ConditionSub {get ; set ;}

		///<summary>
		///EffectType
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 EffectType {get ; set ;}

		///<summary>
		///EffectRate
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 EffectRate {get ; set ;}

		///<summary>
		///EffectValue
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 EffectValue {get ; set ;}
		#endregion
        
        #region Clone
        public TemplateActivityexdetailEntity Clone()
        {
            TemplateActivityexdetailEntity entity = new TemplateActivityexdetailEntity();
			entity.Idx = this.Idx;
			entity.ExcitingId = this.ExcitingId;
			entity.GroupId = this.GroupId;
			entity.ExStep = this.ExStep;
			entity.Count = this.Count;
			entity.Condition = this.Condition;
			entity.ConditionSub = this.ConditionSub;
			entity.EffectType = this.EffectType;
			entity.EffectRate = this.EffectRate;
			entity.EffectValue = this.EffectValue;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Template_ActivityExDetail 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TemplateActivityexdetailResponse : BaseResponse<TemplateActivityexdetailEntity>
    {

    }
}

