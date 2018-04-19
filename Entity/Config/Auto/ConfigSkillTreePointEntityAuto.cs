
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_SkillTreePoint 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigSkilltreepointEntity
	{
		
		public ConfigSkilltreepointEntity()
		{
		}

		public ConfigSkilltreepointEntity(
		System.Int32 managerlevel
,				System.Int32 sumskillpoint
,				System.Int32 addskillpoint
		)
		{
			this.ManagerLevel = managerlevel;
			this.SumSkillPoint = sumskillpoint;
			this.AddSkillPoint = addskillpoint;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerLevel
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 ManagerLevel {get ; set ;}

		///<summary>
		///总天赋点数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SumSkillPoint {get ; set ;}

		///<summary>
		///升级获得的点数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 AddSkillPoint {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigSkilltreepointEntity Clone()
        {
            ConfigSkilltreepointEntity entity = new ConfigSkilltreepointEntity();
			entity.ManagerLevel = this.ManagerLevel;
			entity.SumSkillPoint = this.SumSkillPoint;
			entity.AddSkillPoint = this.AddSkillPoint;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_SkillTreePoint 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigSkilltreepointResponse : BaseResponse<ConfigSkilltreepointEntity>
    {

    }
}

