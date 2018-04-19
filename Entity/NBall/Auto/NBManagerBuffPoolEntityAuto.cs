
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerBuffPool 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagerbuffpoolEntity
	{
		
		public NbManagerbuffpoolEntity()
		{
		}

		public NbManagerbuffpoolEntity(
		System.Int64 id
,				System.Guid managerid
,				System.String skillcode
,				System.Int32 skilllevel
,				System.Int32 buffsrctype
,				System.String buffsrcid
,				System.Int32 buffunittype
,				System.Int32 liveflag
,				System.Int32 buffno
,				System.Int32 dstdir
,				System.Int32 dstmode
,				System.String dstkey
,				System.String buffmap
,				System.Decimal buffval
,				System.Decimal buffper
,				System.DateTime expirytime
,				System.Int32 limittimes
,				System.Int32 remaintimes
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.Id = id;
			this.ManagerId = managerid;
			this.SkillCode = skillcode;
			this.SkillLevel = skilllevel;
			this.BuffSrcType = buffsrctype;
			this.BuffSrcId = buffsrcid;
			this.BuffUnitType = buffunittype;
			this.LiveFlag = liveflag;
			this.BuffNo = buffno;
			this.DstDir = dstdir;
			this.DstMode = dstmode;
			this.DstKey = dstkey;
			this.BuffMap = buffmap;
			this.BuffVal = buffval;
			this.BuffPer = buffper;
			this.ExpiryTime = expirytime;
			this.LimitTimes = limittimes;
			this.RemainTimes = remaintimes;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///Id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int64 Id {get ; set ;}

		///<summary>
		///经理Id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///SkillCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///SkillLevel
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///BuffSrcType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 BuffSrcType {get ; set ;}

		///<summary>
		///BuffSrcId
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String BuffSrcId {get ; set ;}

		///<summary>
		///效果类型
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 BuffUnitType {get ; set ;}

		///<summary>
		///LiveFlag
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 LiveFlag {get ; set ;}

		///<summary>
		///BuffNo
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 BuffNo {get ; set ;}

		///<summary>
		///目标方
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 DstDir {get ; set ;}

		///<summary>
		///目标模式
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 DstMode {get ; set ;}

		///<summary>
		///目标Key
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String DstKey {get ; set ;}

		///<summary>
		///BuffMap
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String BuffMap {get ; set ;}

		///<summary>
		///Buff值
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Decimal BuffVal {get ; set ;}

		///<summary>
		///Buff百分比
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Decimal BuffPer {get ; set ;}

		///<summary>
		///失效时间
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.DateTime ExpiryTime {get ; set ;}

		///<summary>
		///限制次数
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 LimitTimes {get ; set ;}

		///<summary>
		///剩余场次 -1为不计算场次 大于等于0需计算场次
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 RemainTimes {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(19)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(20)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagerbuffpoolEntity Clone()
        {
            NbManagerbuffpoolEntity entity = new NbManagerbuffpoolEntity();
			entity.Id = this.Id;
			entity.ManagerId = this.ManagerId;
			entity.SkillCode = this.SkillCode;
			entity.SkillLevel = this.SkillLevel;
			entity.BuffSrcType = this.BuffSrcType;
			entity.BuffSrcId = this.BuffSrcId;
			entity.BuffUnitType = this.BuffUnitType;
			entity.LiveFlag = this.LiveFlag;
			entity.BuffNo = this.BuffNo;
			entity.DstDir = this.DstDir;
			entity.DstMode = this.DstMode;
			entity.DstKey = this.DstKey;
			entity.BuffMap = this.BuffMap;
			entity.BuffVal = this.BuffVal;
			entity.BuffPer = this.BuffPer;
			entity.ExpiryTime = this.ExpiryTime;
			entity.LimitTimes = this.LimitTimes;
			entity.RemainTimes = this.RemainTimes;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerBuffPool 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagerbuffpoolResponse : BaseResponse<NbManagerbuffpoolEntity>
    {

    }
}

