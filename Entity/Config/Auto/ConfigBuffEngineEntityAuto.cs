
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_BuffEngine 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigBuffengineEntity
	{
		
		public ConfigBuffengineEntity()
		{
		}

		public ConfigBuffengineEntity(
		System.Int32 id
,				System.String skillcode
,				System.Int32 skilllevel
,				System.Int32 buffsrctype
,				System.Int32 buffunittype
,				System.Int32 liveflag
,				System.Int32 checkmode
,				System.String checkkey
,				System.Int32 calcmode
,				System.Int32 srcdir
,				System.Int32 srcmode
,				System.String srckey
,				System.Int32 dstdir
,				System.Int32 dstmode
,				System.String dstkey
,				System.String buffmap
,				System.Decimal buffval
,				System.Decimal buffper
,				System.String buffarg
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
			this.CheckMode = checkmode;
			this.CheckKey = checkkey;
			this.CalcMode = calcmode;
			this.SrcDir = srcdir;
			this.SrcMode = srcmode;
			this.SrcKey = srckey;
			this.DstDir = dstdir;
			this.DstMode = dstmode;
			this.DstKey = dstkey;
			this.BuffMap = buffmap;
			this.BuffVal = buffval;
			this.BuffPer = buffper;
			this.BuffArg = buffarg;
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
		///检查模式
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 CheckMode {get ; set ;}

		///<summary>
		///检查Key
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String CheckKey {get ; set ;}

		///<summary>
		///运算模式
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 CalcMode {get ; set ;}

		///<summary>
		///源方
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 SrcDir {get ; set ;}

		///<summary>
		///源模式
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 SrcMode {get ; set ;}

		///<summary>
		///源Key
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String SrcKey {get ; set ;}

		///<summary>
		///目标方
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 DstDir {get ; set ;}

		///<summary>
		///目标模式
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 DstMode {get ; set ;}

		///<summary>
		///目标Key
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String DstKey {get ; set ;}

		///<summary>
		///BuffMap
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.String BuffMap {get ; set ;}

		///<summary>
		///Buff值
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Decimal BuffVal {get ; set ;}

		///<summary>
		///Buff百分比
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Decimal BuffPer {get ; set ;}

		///<summary>
		///Buff系数
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.String BuffArg {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(21)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigBuffengineEntity Clone()
        {
            ConfigBuffengineEntity entity = new ConfigBuffengineEntity();
			entity.Id = this.Id;
			entity.SkillCode = this.SkillCode;
			entity.SkillLevel = this.SkillLevel;
			entity.BuffSrcType = this.BuffSrcType;
			entity.BuffUnitType = this.BuffUnitType;
			entity.LiveFlag = this.LiveFlag;
			entity.CheckMode = this.CheckMode;
			entity.CheckKey = this.CheckKey;
			entity.CalcMode = this.CalcMode;
			entity.SrcDir = this.SrcDir;
			entity.SrcMode = this.SrcMode;
			entity.SrcKey = this.SrcKey;
			entity.DstDir = this.DstDir;
			entity.DstMode = this.DstMode;
			entity.DstKey = this.DstKey;
			entity.BuffMap = this.BuffMap;
			entity.BuffVal = this.BuffVal;
			entity.BuffPer = this.BuffPer;
			entity.BuffArg = this.BuffArg;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_BuffEngine 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigBuffengineResponse : BaseResponse<ConfigBuffengineEntity>
    {

    }
}

