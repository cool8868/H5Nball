
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Equipment 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicEquipmentEntity
	{
		
		public DicEquipmentEntity()
		{
		}

		public DicEquipmentEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 suittype
,				System.Int32 suitid
,				System.Int32 quality
,				System.Int32 propertytype1
,				System.Int32 propertytype2
,				System.String description
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.SuitType = suittype;
			this.SuitId = suitid;
			this.Quality = quality;
			this.PropertyType1 = propertytype1;
			this.PropertyType2 = propertytype2;
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
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///套装类型：1,7件套套装;2,5件套套装;3,3件套套装;4,散装;
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SuitType {get ; set ;}

		///<summary>
		///SuitId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SuitId {get ; set ;}

		///<summary>
		///装备品质：1,史诗;2,精良;3,优质;4,普通;5,劣质;
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Quality {get ; set ;}

		///<summary>
		///属性加成类型1，绝对值
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PropertyType1 {get ; set ;}

		///<summary>
		///属性加成类型2，百分比
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 PropertyType2 {get ; set ;}

		///<summary>
		///装备描述
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public DicEquipmentEntity Clone()
        {
            DicEquipmentEntity entity = new DicEquipmentEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.SuitType = this.SuitType;
			entity.SuitId = this.SuitId;
			entity.Quality = this.Quality;
			entity.PropertyType1 = this.PropertyType1;
			entity.PropertyType2 = this.PropertyType2;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Equipment 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicEquipmentResponse : BaseResponse<DicEquipmentEntity>
    {

    }
}

