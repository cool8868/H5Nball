
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.League_FightMap 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LeagueFightmapEntity
	{
		
		public LeagueFightmapEntity()
		{
		}

		public LeagueFightmapEntity(
		System.Guid managerid
,				System.Guid leagurecordid
,				System.Byte[] fightmapstring
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.LeaguRecordId = leagurecordid;
			this.FightMapString = fightmapstring;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///LeaguRecordId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid LeaguRecordId {get ; set ;}

		///<summary>
		///FightMapString
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.Byte[] FightMapString {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public LeagueFightmapEntity Clone()
        {
            LeagueFightmapEntity entity = new LeagueFightmapEntity();
			entity.ManagerId = this.ManagerId;
			entity.LeaguRecordId = this.LeaguRecordId;
			entity.FightMapString = this.FightMapString;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.League_FightMap 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LeagueFightmapResponse : BaseResponse<LeagueFightmapEntity>
    {

    }
}

