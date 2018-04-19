
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_SkillCardLevel 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigSkillcardlevelEntity
	{
		
		public ConfigSkillcardlevelEntity()
		{
		}

		public ConfigSkillcardlevelEntity(
		System.Int32 rowid
,				System.Int32 skillclass
,				System.Int32 skilllevel
,				System.Int32 minexp
,				System.Int32 maxexp
,				System.DateTime rowtime
		)
		{
			this.RowId = rowid;
			this.SkillClass = skillclass;
			this.SkillLevel = skilllevel;
			this.MinExp = minexp;
			this.MaxExp = maxexp;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///RowId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 RowId {get ; set ;}

		///<summary>
		///SkillClass
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SkillClass {get ; set ;}

		///<summary>
		///SkillLevel
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///MinExp
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MinExp {get ; set ;}

		///<summary>
		///经验值
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 MaxExp {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigSkillcardlevelEntity Clone()
        {
            ConfigSkillcardlevelEntity entity = new ConfigSkillcardlevelEntity();
			entity.RowId = this.RowId;
			entity.SkillClass = this.SkillClass;
			entity.SkillLevel = this.SkillLevel;
			entity.MinExp = this.MinExp;
			entity.MaxExp = this.MaxExp;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_SkillCardLevel 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigSkillcardlevelResponse : BaseResponse<ConfigSkillcardlevelEntity>
    {

    }
}

