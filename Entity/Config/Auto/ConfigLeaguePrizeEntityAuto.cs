
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_LeaguePrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLeagueprizeEntity
	{
		
		public ConfigLeagueprizeEntity()
		{
		}

		public ConfigLeagueprizeEntity(
		System.Int32 idx
,				System.Int32 leagueid
,				System.Int32 resulttype
,				System.Int32 prizetype
,				System.Int32 itemcode
,				System.Int32 count
,				System.Boolean isbinding
		)
		{
			this.Idx = idx;
			this.LeagueID = leagueid;
			this.ResultType = resulttype;
			this.PrizeType = prizetype;
			this.ItemCode = itemcode;
			this.Count = count;
			this.IsBindIng = isbinding;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///LeagueID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 LeagueID {get ; set ;}

		///<summary>
		///1:胜，2：平，3：负，4：第一次获得冠军，5：第一次之后获得冠军
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ResultType {get ; set ;}

		///<summary>
		///1：经验，2：金币，3：联赛积分,4:点卷，5：物品，6：卡库
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///Count
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///IsBindIng
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsBindIng {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLeagueprizeEntity Clone()
        {
            ConfigLeagueprizeEntity entity = new ConfigLeagueprizeEntity();
			entity.Idx = this.Idx;
			entity.LeagueID = this.LeagueID;
			entity.ResultType = this.ResultType;
			entity.PrizeType = this.PrizeType;
			entity.ItemCode = this.ItemCode;
			entity.Count = this.Count;
			entity.IsBindIng = this.IsBindIng;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_LeaguePrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLeagueprizeResponse : BaseResponse<ConfigLeagueprizeEntity>
    {

    }
}

