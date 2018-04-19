
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CrowdRankPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCrowdrankprizeEntity
	{
		
		public ConfigCrowdrankprizeEntity()
		{
		}

		public ConfigCrowdrankprizeEntity(
		System.Int32 idx
,				System.Int32 category
,				System.Int32 categorysub
,				System.Int32 type
,				System.Int32 subtype
,				System.Int32 rate
,				System.Int32 min
,				System.Int32 max
,				System.Int32 strength
,				System.Int32 count
,				System.Boolean isbinding
,				System.String description
		)
		{
			this.Idx = idx;
			this.Category = category;
			this.CategorySub = categorysub;
			this.Type = type;
			this.SubType = subtype;
			this.Rate = rate;
			this.Min = min;
			this.Max = max;
			this.Strength = strength;
			this.Count = count;
			this.IsBinding = isbinding;
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
		///Category
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Category {get ; set ;}

		///<summary>
		///CategorySub
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 CategorySub {get ; set ;}

		///<summary>
		///Type
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///SubType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///Rate
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Rate {get ; set ;}

		///<summary>
		///Min
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Min {get ; set ;}

		///<summary>
		///Max
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Max {get ; set ;}

		///<summary>
		///Strength
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Strength {get ; set ;}

		///<summary>
		///Count
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///IsBinding
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Boolean IsBinding {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCrowdrankprizeEntity Clone()
        {
            ConfigCrowdrankprizeEntity entity = new ConfigCrowdrankprizeEntity();
			entity.Idx = this.Idx;
			entity.Category = this.Category;
			entity.CategorySub = this.CategorySub;
			entity.Type = this.Type;
			entity.SubType = this.SubType;
			entity.Rate = this.Rate;
			entity.Min = this.Min;
			entity.Max = this.Max;
			entity.Strength = this.Strength;
			entity.Count = this.Count;
			entity.IsBinding = this.IsBinding;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CrowdRankPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCrowdrankprizeResponse : BaseResponse<ConfigCrowdrankprizeEntity>
    {

    }
}
