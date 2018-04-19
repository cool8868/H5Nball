
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_ArenaNpcLink 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigArenanpclinkEntity
	{
		
		public ConfigArenanpclinkEntity()
		{
		}

		public ConfigArenanpclinkEntity(
		System.Guid npcid
,				System.Int32 idx
,				System.Int32 groupid
,				System.Int32 kpi
,				System.Int32 dangrading
		)
		{
			this.NpcId = npcid;
			this.Idx = idx;
			this.GroupId = groupid;
			this.Kpi = kpi;
			this.DanGrading = dangrading;
		}
		
		#region Public Properties
		
		///<summary>
		///NpcId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid NpcId {get ; set ;}

		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///GroupId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 GroupId {get ; set ;}

		///<summary>
		///Kpi
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Kpi {get ; set ;}

		///<summary>
		///DanGrading
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 DanGrading {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigArenanpclinkEntity Clone()
        {
            ConfigArenanpclinkEntity entity = new ConfigArenanpclinkEntity();
			entity.NpcId = this.NpcId;
			entity.Idx = this.Idx;
			entity.GroupId = this.GroupId;
			entity.Kpi = this.Kpi;
			entity.DanGrading = this.DanGrading;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_ArenaNpcLink 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigArenanpclinkResponse : BaseResponse<ConfigArenanpclinkEntity>
    {

    }
}
