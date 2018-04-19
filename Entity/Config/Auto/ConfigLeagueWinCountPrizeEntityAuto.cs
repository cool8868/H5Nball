
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_LeagueWinCountPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLeaguewincountprizeEntity
	{
		
		public ConfigLeaguewincountprizeEntity()
		{
		}

		public ConfigLeaguewincountprizeEntity(
		System.Int32 idx
,				System.Int32 leagueid
,				System.Int32 wincount
,				System.Int32 prizepoint
		)
		{
			this.Idx = idx;
			this.LeagueId = leagueid;
			this.WinCount = wincount;
			this.PrizePoint = prizepoint;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///联赛类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 LeagueId {get ; set ;}

		///<summary>
		///获胜场次
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 WinCount {get ; set ;}

		///<summary>
		///奖励钻石数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizePoint {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLeaguewincountprizeEntity Clone()
        {
            ConfigLeaguewincountprizeEntity entity = new ConfigLeaguewincountprizeEntity();
			entity.Idx = this.Idx;
			entity.LeagueId = this.LeagueId;
			entity.WinCount = this.WinCount;
			entity.PrizePoint = this.PrizePoint;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_LeagueWinCountPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLeaguewincountprizeResponse : BaseResponse<ConfigLeaguewincountprizeEntity>
    {

    }
}

