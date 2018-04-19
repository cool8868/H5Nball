
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_StarSkills 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicStarskillsEntity
	{
		
		public DicStarskillsEntity()
		{
		}

		public DicStarskillsEntity(
		System.Int32 idx
,				System.Int32 playerid
,				System.String skillcode
,				System.Int32 acttype
,				System.String name
,				System.Int32 strength
,				System.String pluscode
,				System.Boolean isvalid
,				System.String description
,				System.String plusdescription
		)
		{
			this.Idx = idx;
			this.PlayerId = playerid;
			this.SkillCode = skillcode;
			this.ActType = acttype;
			this.Name = name;
			this.Strength = strength;
			this.PlusCode = pluscode;
			this.IsValid = isvalid;
			this.Description = description;
			this.PlusDescription = plusdescription;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///球员id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PlayerId {get ; set ;}

		///<summary>
		///技能编号
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///动作类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ActType {get ; set ;}

		///<summary>
		///名称
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Name {get ; set ;}

		///<summary>
		///需要强化等级
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Strength {get ; set ;}

		///<summary>
		///附加技能编号
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String PlusCode {get ; set ;}

		///<summary>
		///是否有效
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean IsValid {get ; set ;}

		///<summary>
		///描述
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String Description {get ; set ;}

		///<summary>
		///附加技能描述
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String PlusDescription {get ; set ;}
		#endregion
        
        #region Clone
        public DicStarskillsEntity Clone()
        {
            DicStarskillsEntity entity = new DicStarskillsEntity();
			entity.Idx = this.Idx;
			entity.PlayerId = this.PlayerId;
			entity.SkillCode = this.SkillCode;
			entity.ActType = this.ActType;
			entity.Name = this.Name;
			entity.Strength = this.Strength;
			entity.PlusCode = this.PlusCode;
			entity.IsValid = this.IsValid;
			entity.Description = this.Description;
			entity.PlusDescription = this.PlusDescription;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_StarSkills 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicStarskillsResponse : BaseResponse<DicStarskillsEntity>
    {

    }
}

