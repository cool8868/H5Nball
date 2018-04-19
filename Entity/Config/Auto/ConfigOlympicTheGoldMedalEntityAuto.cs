
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_OlympicTheGoldMedal 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigOlympicthegoldmedalEntity
	{
		
		public ConfigOlympicthegoldmedalEntity()
		{
		}

		public ConfigOlympicthegoldmedalEntity(
		System.Int32 idx
,				System.Int32 gettype
,				System.Int32 thegoldmedalid
,				System.Int32 rate
		)
		{
			this.Idx = idx;
			this.GetType = gettype;
			this.TheGoldMedalId = thegoldmedalid;
			this.Rate = rate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///掉落类型 1= 金币球探 2=钻石球探 3=友情的球探 4=友谊赛 5=紫卡分解 6=橙卡元老分解
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 GetType {get ; set ;}

		///<summary>
		///金牌ID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 TheGoldMedalId {get ; set ;}

		///<summary>
		///概率
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Rate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigOlympicthegoldmedalEntity Clone()
        {
            ConfigOlympicthegoldmedalEntity entity = new ConfigOlympicthegoldmedalEntity();
			entity.Idx = this.Idx;
			entity.GetType = this.GetType;
			entity.TheGoldMedalId = this.TheGoldMedalId;
			entity.Rate = this.Rate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_OlympicTheGoldMedal 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigOlympicthegoldmedalResponse : BaseResponse<ConfigOlympicthegoldmedalEntity>
    {

    }
}
