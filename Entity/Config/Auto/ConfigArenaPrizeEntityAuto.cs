
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_ArenaPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigArenaprizeEntity
	{
		
		public ConfigArenaprizeEntity()
		{
		}

		public ConfigArenaprizeEntity(
		System.Int32 idx
,				System.Int32 startrank
,				System.Int32 endrank
,				System.Int32 prizetype
,				System.Int32 prizecode
,				System.Int32 prizenumber
		)
		{
			this.Idx = idx;
			this.StartRank = startrank;
			this.EndRank = endrank;
			this.PrizeType = prizetype;
			this.PrizeCode = prizecode;
			this.PrizeNumber = prizenumber;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///起始排名
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 StartRank {get ; set ;}

		///<summary>
		///截止排名
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 EndRank {get ; set ;}

		///<summary>
		///奖励类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///奖励code
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeCode {get ; set ;}

		///<summary>
		///奖励数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PrizeNumber {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigArenaprizeEntity Clone()
        {
            ConfigArenaprizeEntity entity = new ConfigArenaprizeEntity();
			entity.Idx = this.Idx;
			entity.StartRank = this.StartRank;
			entity.EndRank = this.EndRank;
			entity.PrizeType = this.PrizeType;
			entity.PrizeCode = this.PrizeCode;
			entity.PrizeNumber = this.PrizeNumber;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_ArenaPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigArenaprizeResponse : BaseResponse<ConfigArenaprizeEntity>
    {

    }
}
