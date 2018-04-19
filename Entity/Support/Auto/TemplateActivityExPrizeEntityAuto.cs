
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Template_ActivityExPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TemplateActivityexprizeEntity
	{
		
		public TemplateActivityexprizeEntity()
		{
		}

		public TemplateActivityexprizeEntity(
		System.Int32 idx
,				System.Int32 excitingid
,				System.Int32 groupid
,				System.Int32 exstep
,				System.Int32 prizetype
,				System.Int32 subtype
,				System.Int32 thirdtype
,				System.Int32 minpower
,				System.Int32 maxpower
,				System.Int32 count
,				System.Int32 strength1
,				System.Int32 strength2
,				System.Boolean isbinding
		)
		{
			this.Idx = idx;
			this.ExcitingId = excitingid;
			this.GroupId = groupid;
			this.ExStep = exstep;
			this.PrizeType = prizetype;
			this.SubType = subtype;
			this.ThirdType = thirdtype;
			this.MinPower = minpower;
			this.MaxPower = maxpower;
			this.Count = count;
			this.Strength1 = strength1;
			this.Strength2 = strength2;
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
		///PrizeType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///SubType
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///ThirdType
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 ThirdType {get ; set ;}

		///<summary>
		///MinPower
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 MinPower {get ; set ;}

		///<summary>
		///MaxPower
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 MaxPower {get ; set ;}

		///<summary>
		///Count
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///Strength1
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Strength1 {get ; set ;}

		///<summary>
		///Strength2
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Strength2 {get ; set ;}

		///<summary>
		///IsBinding
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Boolean IsBinding {get ; set ;}
		#endregion
        
        #region Clone
        public TemplateActivityexprizeEntity Clone()
        {
            TemplateActivityexprizeEntity entity = new TemplateActivityexprizeEntity();
			entity.Idx = this.Idx;
			entity.ExcitingId = this.ExcitingId;
			entity.GroupId = this.GroupId;
			entity.ExStep = this.ExStep;
			entity.PrizeType = this.PrizeType;
			entity.SubType = this.SubType;
			entity.ThirdType = this.ThirdType;
			entity.MinPower = this.MinPower;
			entity.MaxPower = this.MaxPower;
			entity.Count = this.Count;
			entity.Strength1 = this.Strength1;
			entity.Strength2 = this.Strength2;
			entity.IsBinding = this.IsBinding;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Template_ActivityExPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TemplateActivityexprizeResponse : BaseResponse<TemplateActivityexprizeEntity>
    {

    }
}

