
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_SkillsTree 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicSkillstreeEntity
	{
		
		public DicSkillstreeEntity()
		{
		}

		public DicSkillstreeEntity(
		System.String skillcode
,				System.String skillname
,				System.Int32 managertype
,				System.Int32 series
,				System.Int32 requiremanagerlevel
,				System.String description
,				System.String condition
,				System.Int32 conditionpoint
,				System.Int32 maxpoint
,				System.Int32 opener
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.SkillCode = skillcode;
			this.SkillName = skillname;
			this.ManagerType = managertype;
			this.Series = series;
			this.RequireManagerLevel = requiremanagerlevel;
			this.Description = description;
			this.Condition = condition;
			this.ConditionPoint = conditionpoint;
			this.MaxPoint = maxpoint;
			this.Opener = opener;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///SkillCode
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///SkillName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SkillName {get ; set ;}

		///<summary>
		///ManagerType
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ManagerType {get ; set ;}

		///<summary>
		///Series
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Series {get ; set ;}

		///<summary>
		///RequireManagerLevel
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 RequireManagerLevel {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Description {get ; set ;}

		///<summary>
		///Condition
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String Condition {get ; set ;}

		///<summary>
		///ConditionPoint
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ConditionPoint {get ; set ;}

		///<summary>
		///MaxPoint
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 MaxPoint {get ; set ;}

		///<summary>
		///Opener
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Opener {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicSkillstreeEntity Clone()
        {
            DicSkillstreeEntity entity = new DicSkillstreeEntity();
			entity.SkillCode = this.SkillCode;
			entity.SkillName = this.SkillName;
			entity.ManagerType = this.ManagerType;
			entity.Series = this.Series;
			entity.RequireManagerLevel = this.RequireManagerLevel;
			entity.Description = this.Description;
			entity.Condition = this.Condition;
			entity.ConditionPoint = this.ConditionPoint;
			entity.MaxPoint = this.MaxPoint;
			entity.Opener = this.Opener;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_SkillsTree 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicSkillstreeResponse : BaseResponse<DicSkillstreeEntity>
    {

    }
}

