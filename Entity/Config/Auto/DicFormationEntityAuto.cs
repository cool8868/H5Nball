
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Formation 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicFormationEntity
	{
		
		public DicFormationEntity()
		{
		}

		public DicFormationEntity(
		System.Int32 idx
,				System.String formation
,				System.String name
,				System.Decimal buffperlevel
,				System.String description
		)
		{
			this.Idx = idx;
			this.Formation = formation;
			this.Name = name;
			this.BuffPerLevel = buffperlevel;
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
		///Formation
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Formation {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///每级增加的buff值，百分比
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Decimal BuffPerLevel {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public DicFormationEntity Clone()
        {
            DicFormationEntity entity = new DicFormationEntity();
			entity.Idx = this.Idx;
			entity.Formation = this.Formation;
			entity.Name = this.Name;
			entity.BuffPerLevel = this.BuffPerLevel;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Formation 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicFormationResponse : BaseResponse<DicFormationEntity>
    {

    }
}

