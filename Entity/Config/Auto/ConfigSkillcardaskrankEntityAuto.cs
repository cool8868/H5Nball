
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_SkillCardAskRank 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigSkillcardaskrankEntity
	{
		
		public ConfigSkillcardaskrankEntity()
		{
		}

		public ConfigSkillcardaskrankEntity(
		System.Int32 npcid
,				System.String npcname
,				System.Int32 askrank
,				System.Int32 opencostgolditemno
,				System.Int32 opencosttype
,				System.Int32 opencostvalue
,				System.Int32 costgolditemno
,				System.Int32 costtype
,				System.Int32 costvalue
,				System.Decimal succrate
,				System.String skillratemap
,				System.DateTime rowtime
		)
		{
			this.NpcId = npcid;
			this.NpcName = npcname;
			this.AskRank = askrank;
			this.OpenCostGoldItemNo = opencostgolditemno;
			this.OpenCostType = opencosttype;
			this.OpenCostValue = opencostvalue;
			this.CostGoldItemNo = costgolditemno;
			this.CostType = costtype;
			this.CostValue = costvalue;
			this.SuccRate = succrate;
			this.SkillRateMap = skillratemap;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///NpcId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 NpcId {get ; set ;}

		///<summary>
		///NpcName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String NpcName {get ; set ;}

		///<summary>
		///等级
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 AskRank {get ; set ;}

		///<summary>
		///OpenCostGoldItemNo
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 OpenCostGoldItemNo {get ; set ;}

		///<summary>
		///OpenCostType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 OpenCostType {get ; set ;}

		///<summary>
		///OpenCostValue
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 OpenCostValue {get ; set ;}

		///<summary>
		///CostGoldItemNo
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 CostGoldItemNo {get ; set ;}

		///<summary>
		///CostType
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 CostType {get ; set ;}

		///<summary>
		///CostValue
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 CostValue {get ; set ;}

		///<summary>
		///晋级概率
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Decimal SuccRate {get ; set ;}

		///<summary>
		///技能卡概率
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String SkillRateMap {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigSkillcardaskrankEntity Clone()
        {
            ConfigSkillcardaskrankEntity entity = new ConfigSkillcardaskrankEntity();
			entity.NpcId = this.NpcId;
			entity.NpcName = this.NpcName;
			entity.AskRank = this.AskRank;
			entity.OpenCostGoldItemNo = this.OpenCostGoldItemNo;
			entity.OpenCostType = this.OpenCostType;
			entity.OpenCostValue = this.OpenCostValue;
			entity.CostGoldItemNo = this.CostGoldItemNo;
			entity.CostType = this.CostType;
			entity.CostValue = this.CostValue;
			entity.SuccRate = this.SuccRate;
			entity.SkillRateMap = this.SkillRateMap;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_SkillCardAskRank 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigSkillcardaskrankResponse : BaseResponse<ConfigSkillcardaskrankEntity>
    {

    }
}

