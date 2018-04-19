
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_LeagueStar 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLeaguestarEntity
	{
		
		public ConfigLeaguestarEntity()
		{
		}

		public ConfigLeaguestarEntity(
		System.Int32 idx
,				System.Int32 leagueid
,				System.Int32 starnumber
,				System.Int32 prizelevel
,				System.Int32 prizetype
,				System.Int32 subtype
,				System.Int32 count
		)
		{
			this.Idx = idx;
			this.LeagueId = leagueid;
			this.StarNumber = starnumber;
			this.PrizeLevel = prizelevel;
			this.PrizeType = prizetype;
			this.SubType = subtype;
			this.Count = count;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///联赛ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 LeagueId {get ; set ;}

		///<summary>
		///需要星星数量
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 StarNumber {get ; set ;}

		///<summary>
		///奖励等级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeLevel {get ; set ;}

		///<summary>
		///奖励物品类型 1钻石 2金币 3物品
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///物品code
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///数量
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Count {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLeaguestarEntity Clone()
        {
            ConfigLeaguestarEntity entity = new ConfigLeaguestarEntity();
			entity.Idx = this.Idx;
			entity.LeagueId = this.LeagueId;
			entity.StarNumber = this.StarNumber;
			entity.PrizeLevel = this.PrizeLevel;
			entity.PrizeType = this.PrizeType;
			entity.SubType = this.SubType;
			entity.Count = this.Count;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_LeagueStar 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLeaguestarResponse : BaseResponse<ConfigLeaguestarEntity>
    {

    }
}

