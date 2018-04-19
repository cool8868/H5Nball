
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Strength 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigStrengthEntity
	{
		
		public ConfigStrengthEntity()
		{
		}

		public ConfigStrengthEntity(
		System.Int32 idx
,				System.Int32 cardlevel
,				System.Int32 source
,				System.Int32 target
,				System.Int32 result
,				System.Int32 rate
,				System.Int32 coin
,				System.Int32 protectpoint
,				System.Int32 showrate
		)
		{
			this.Idx = idx;
			this.CardLevel = cardlevel;
			this.Source = source;
			this.Target = target;
			this.Result = result;
			this.Rate = rate;
			this.Coin = coin;
			this.ProtectPoint = protectpoint;
			this.ShowRate = showrate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///球员卡颜色
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CardLevel {get ; set ;}

		///<summary>
		///源卡强化级别
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Source {get ; set ;}

		///<summary>
		///目标卡强化级别
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Target {get ; set ;}

		///<summary>
		///结果强化级别
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Result {get ; set ;}

		///<summary>
		///成功率(0-10000)
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Rate {get ; set ;}

		///<summary>
		///消耗游戏币
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///ProtectPoint
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ProtectPoint {get ; set ;}

		///<summary>
		///ShowRate
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 ShowRate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigStrengthEntity Clone()
        {
            ConfigStrengthEntity entity = new ConfigStrengthEntity();
			entity.Idx = this.Idx;
			entity.CardLevel = this.CardLevel;
			entity.Source = this.Source;
			entity.Target = this.Target;
			entity.Result = this.Result;
			entity.Rate = this.Rate;
			entity.Coin = this.Coin;
			entity.ProtectPoint = this.ProtectPoint;
			entity.ShowRate = this.ShowRate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Strength 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigStrengthResponse : BaseResponse<ConfigStrengthEntity>
    {

    }
}

