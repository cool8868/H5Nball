
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_BuffPool 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigBuffpoolEntity
	{
		
		public ConfigBuffpoolEntity()
		{
		}

		public ConfigBuffpoolEntity(
		System.Int32 id
,				System.String skillcode
,				System.Int32 skilllevel
,				System.Int32 buffsrctype
,				System.Int32 buffunittype
,				System.Int32 liveflag
,				System.Int32 buffno
,				System.Int32 dstdir
,				System.Int32 dstmode
,				System.String dstkey
,				System.String buffmap
,				System.Decimal buffval
,				System.Decimal buffper
,				System.String buffarg
,				System.Int32 expiryminutes
,				System.Int32 limittimes
,				System.Int32 totaltimes
,				System.Boolean repeatbuffflag
,				System.Boolean repeattimeflag
,				System.Boolean repeattimesflag
,				System.String coverskillcode
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.Id = id;
			this.SkillCode = skillcode;
			this.SkillLevel = skilllevel;
			this.BuffSrcType = buffsrctype;
			this.BuffUnitType = buffunittype;
			this.LiveFlag = liveflag;
			this.BuffNo = buffno;
			this.DstDir = dstdir;
			this.DstMode = dstmode;
			this.DstKey = dstkey;
			this.BuffMap = buffmap;
			this.BuffVal = buffval;
			this.BuffPer = buffper;
			this.BuffArg = buffarg;
			this.ExpiryMinutes = expiryminutes;
			this.LimitTimes = limittimes;
			this.TotalTimes = totaltimes;
			this.RepeatBuffFlag = repeatbuffflag;
			this.RepeatTimeFlag = repeattimeflag;
			this.RepeatTimesFlag = repeattimesflag;
			this.CoverSkillCode = coverskillcode;
			this.Memo = memo;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Id {get ; set ;}

		///<summary>
		///SkillCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///SkillLevel
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///来源类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 BuffSrcType {get ; set ;}

		///<summary>
		///效果类型
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 BuffUnitType {get ; set ;}

		///<summary>
		///LiveFlag
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 LiveFlag {get ; set ;}

		///<summary>
		///BuffNo
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 BuffNo {get ; set ;}

		///<summary>
		///目标方
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 DstDir {get ; set ;}

		///<summary>
		///目标模式
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 DstMode {get ; set ;}

		///<summary>
		///目标Key
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String DstKey {get ; set ;}

		///<summary>
		///BuffMap
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String BuffMap {get ; set ;}

		///<summary>
		///Buff值
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Decimal BuffVal {get ; set ;}

		///<summary>
		///Buff百分比
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Decimal BuffPer {get ; set ;}

		///<summary>
		///Buff系数
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String BuffArg {get ; set ;}

		///<summary>
		///有效时间
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 ExpiryMinutes {get ; set ;}

		///<summary>
		///限制次数
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 LimitTimes {get ; set ;}

		///<summary>
		///有效场次 -1位不计算场次 大于0为持续场次
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 TotalTimes {get ; set ;}

		///<summary>
		///累计Buff
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Boolean RepeatBuffFlag {get ; set ;}

		///<summary>
		///累计有效时间
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Boolean RepeatTimeFlag {get ; set ;}

		///<summary>
		///累计有效次数
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Boolean RepeatTimesFlag {get ; set ;}

		///<summary>
		///覆盖技能
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.String CoverSkillCode {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(23)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigBuffpoolEntity Clone()
        {
            ConfigBuffpoolEntity entity = new ConfigBuffpoolEntity();
			entity.Id = this.Id;
			entity.SkillCode = this.SkillCode;
			entity.SkillLevel = this.SkillLevel;
			entity.BuffSrcType = this.BuffSrcType;
			entity.BuffUnitType = this.BuffUnitType;
			entity.LiveFlag = this.LiveFlag;
			entity.BuffNo = this.BuffNo;
			entity.DstDir = this.DstDir;
			entity.DstMode = this.DstMode;
			entity.DstKey = this.DstKey;
			entity.BuffMap = this.BuffMap;
			entity.BuffVal = this.BuffVal;
			entity.BuffPer = this.BuffPer;
			entity.BuffArg = this.BuffArg;
			entity.ExpiryMinutes = this.ExpiryMinutes;
			entity.LimitTimes = this.LimitTimes;
			entity.TotalTimes = this.TotalTimes;
			entity.RepeatBuffFlag = this.RepeatBuffFlag;
			entity.RepeatTimeFlag = this.RepeatTimeFlag;
			entity.RepeatTimesFlag = this.RepeatTimesFlag;
			entity.CoverSkillCode = this.CoverSkillCode;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_BuffPool 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigBuffpoolResponse : BaseResponse<ConfigBuffpoolEntity>
    {

    }
}

