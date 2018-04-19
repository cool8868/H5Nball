
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_League 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLeagueEntity
	{
		
		public ConfigLeagueEntity()
		{
		}

		public ConfigLeagueEntity(
		System.Int32 leagueid
,				System.String leaguename
,				System.Int32 level
		)
		{
			this.LeagueID = leagueid;
			this.LeagueName = leaguename;
			this.Level = level;
		}
		
		#region Public Properties
		
		///<summary>
		///LeagueID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 LeagueID {get ; set ;}

		///<summary>
		///LeagueName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String LeagueName {get ; set ;}

		///<summary>
		///开启等级
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Level {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLeagueEntity Clone()
        {
            ConfigLeagueEntity entity = new ConfigLeagueEntity();
			entity.LeagueID = this.LeagueID;
			entity.LeagueName = this.LeagueName;
			entity.Level = this.Level;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_League 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLeagueResponse : BaseResponse<ConfigLeagueEntity>
    {

    }
}

