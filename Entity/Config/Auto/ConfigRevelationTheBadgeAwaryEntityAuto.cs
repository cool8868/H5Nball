
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationTheBadgeAwary 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationthebadgeawaryEntity
	{
		
		public ConfigRevelationthebadgeawaryEntity()
		{
		}

		public ConfigRevelationthebadgeawaryEntity(
		System.Int32 idx
,				System.Int32 playersid
,				System.String personalresume
,				System.Int32 thebadgeid
		)
		{
			this.Idx = idx;
			this.PlayersId = playersid;
			this.PersonalResume = personalresume;
			this.TheBadgeID = thebadgeid;
		}
		
		#region Public Properties
		
		///<summary>
		///关卡
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///球员ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PlayersId {get ; set ;}

		///<summary>
		///个人履历
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String PersonalResume {get ; set ;}

		///<summary>
		///徽章ID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 TheBadgeID {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationthebadgeawaryEntity Clone()
        {
            ConfigRevelationthebadgeawaryEntity entity = new ConfigRevelationthebadgeawaryEntity();
			entity.Idx = this.Idx;
			entity.PlayersId = this.PlayersId;
			entity.PersonalResume = this.PersonalResume;
			entity.TheBadgeID = this.TheBadgeID;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationTheBadgeAwary 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationthebadgeawaryResponse : BaseResponse<ConfigRevelationthebadgeawaryEntity>
    {

    }
}

