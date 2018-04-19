
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CrowdScore 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCrowdscoreEntity
	{
		
		public ConfigCrowdscoreEntity()
		{
		}

		public ConfigCrowdscoreEntity(
		System.Int32 idx
,				System.Int32 score
		)
		{
			this.Idx = idx;
			this.Score = score;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///Score
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Score {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCrowdscoreEntity Clone()
        {
            ConfigCrowdscoreEntity entity = new ConfigCrowdscoreEntity();
			entity.Idx = this.Idx;
			entity.Score = this.Score;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CrowdScore 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCrowdscoreResponse : BaseResponse<ConfigCrowdscoreEntity>
    {

    }
}
