
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_LeagueGoals 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLeaguegoalsEntity
	{
		
		public ConfigLeaguegoalsEntity()
		{
		}

		public ConfigLeaguegoalsEntity(
		System.Int32 idx
,				System.Int32 templateid
,				System.Int32 npcid
,				System.Int32 mingoals
,				System.Int32 maxgoals
		)
		{
			this.Idx = idx;
			this.TemplateId = templateid;
			this.NpcId = npcid;
			this.MinGoals = mingoals;
			this.MaxGoals = maxgoals;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///TemplateId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TemplateId {get ; set ;}

		///<summary>
		///NpcId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 NpcId {get ; set ;}

		///<summary>
		///MinGoals
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MinGoals {get ; set ;}

		///<summary>
		///MaxGoals
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 MaxGoals {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLeaguegoalsEntity Clone()
        {
            ConfigLeaguegoalsEntity entity = new ConfigLeaguegoalsEntity();
			entity.Idx = this.Idx;
			entity.TemplateId = this.TemplateId;
			entity.NpcId = this.NpcId;
			entity.MinGoals = this.MinGoals;
			entity.MaxGoals = this.MaxGoals;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_LeagueGoals 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLeaguegoalsResponse : BaseResponse<ConfigLeaguegoalsEntity>
    {

    }
}

