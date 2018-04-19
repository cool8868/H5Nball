
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_LeagueFightMap 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLeaguefightmapEntity
	{
		
		public ConfigLeaguefightmapEntity()
		{
		}

		public ConfigLeaguefightmapEntity(
		System.Int32 idx
,				System.Int32 teamcount
,				System.Int32 templateid
,				System.Int32 roundindex
,				System.Int32 groupindex
,				System.Int32 team1
,				System.Int32 team2
		)
		{
			this.Idx = idx;
			this.TeamCount = teamcount;
			this.TemplateId = templateid;
			this.RoundIndex = roundindex;
			this.GroupIndex = groupindex;
			this.Team1 = team1;
			this.Team2 = team2;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///TeamCount
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TeamCount {get ; set ;}

		///<summary>
		///TemplateId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 TemplateId {get ; set ;}

		///<summary>
		///RoundIndex
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 RoundIndex {get ; set ;}

		///<summary>
		///GroupIndex
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 GroupIndex {get ; set ;}

		///<summary>
		///Team1
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Team1 {get ; set ;}

		///<summary>
		///Team2
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Team2 {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLeaguefightmapEntity Clone()
        {
            ConfigLeaguefightmapEntity entity = new ConfigLeaguefightmapEntity();
			entity.Idx = this.Idx;
			entity.TeamCount = this.TeamCount;
			entity.TemplateId = this.TemplateId;
			entity.RoundIndex = this.RoundIndex;
			entity.GroupIndex = this.GroupIndex;
			entity.Team1 = this.Team1;
			entity.Team2 = this.Team2;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_LeagueFightMap 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLeaguefightmapResponse : BaseResponse<ConfigLeaguefightmapEntity>
    {

    }
}

