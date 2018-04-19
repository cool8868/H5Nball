
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationNpcLink 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationnpclinkEntity
	{
		
		public ConfigRevelationnpclinkEntity()
		{
		}

		public ConfigRevelationnpclinkEntity(
		System.Int32 idx
,				System.Int32 stageid
,				System.Int32 smallclearanceid
,				System.Guid npcid
		)
		{
			this.Idx = idx;
			this.StageId = stageid;
			this.SmallClearanceId = smallclearanceid;
			this.NpcId = npcid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///StageId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 StageId {get ; set ;}

		///<summary>
		///SmallClearanceId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SmallClearanceId {get ; set ;}

		///<summary>
		///NpcId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid NpcId {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationnpclinkEntity Clone()
        {
            ConfigRevelationnpclinkEntity entity = new ConfigRevelationnpclinkEntity();
			entity.Idx = this.Idx;
			entity.StageId = this.StageId;
			entity.SmallClearanceId = this.SmallClearanceId;
			entity.NpcId = this.NpcId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationNpcLink 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationnpclinkResponse : BaseResponse<ConfigRevelationnpclinkEntity>
    {

    }
}

