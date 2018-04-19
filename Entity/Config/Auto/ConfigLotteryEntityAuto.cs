
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Lottery 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLotteryEntity
	{
		
		public ConfigLotteryEntity()
		{
		}

		public ConfigLotteryEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 type
,				System.Int32 subtype
,				System.Int32 minlevel
,				System.Int32 maxlevel
,				System.Int32 minvip
,				System.Int32 maxvip
,				System.DateTime mintime
,				System.DateTime maxtime
,				System.Int32 strength
,				System.Boolean isbinding
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.Type = type;
			this.SubType = subtype;
			this.MinLevel = minlevel;
			this.MaxLevel = maxlevel;
			this.MinVip = minvip;
			this.MaxVip = maxvip;
			this.MinTime = mintime;
			this.MaxTime = maxtime;
			this.Strength = strength;
			this.IsBinding = isbinding;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///抽奖库名称
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///抽奖类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///抽奖二级分类
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///经理等级min
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 MinLevel {get ; set ;}

		///<summary>
		///经理等级max
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 MaxLevel {get ; set ;}

		///<summary>
		///Vip等级min
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 MinVip {get ; set ;}

		///<summary>
		///Vip等级max
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 MaxVip {get ; set ;}

		///<summary>
		///MinTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime MinTime {get ; set ;}

		///<summary>
		///MaxTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime MaxTime {get ; set ;}

		///<summary>
		///强化等级
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Strength {get ; set ;}

		///<summary>
		///是否绑定
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Boolean IsBinding {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLotteryEntity Clone()
        {
            ConfigLotteryEntity entity = new ConfigLotteryEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.Type = this.Type;
			entity.SubType = this.SubType;
			entity.MinLevel = this.MinLevel;
			entity.MaxLevel = this.MaxLevel;
			entity.MinVip = this.MinVip;
			entity.MaxVip = this.MaxVip;
			entity.MinTime = this.MinTime;
			entity.MaxTime = this.MaxTime;
			entity.Strength = this.Strength;
			entity.IsBinding = this.IsBinding;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Lottery 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLotteryResponse : BaseResponse<ConfigLotteryEntity>
    {

    }
}

