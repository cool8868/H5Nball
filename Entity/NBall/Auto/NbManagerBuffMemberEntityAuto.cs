
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerBuffMember 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagerbuffmemberEntity
	{
		
		public NbManagerbuffmemberEntity()
		{
		}

		public NbManagerbuffmemberEntity(
		System.Int64 id
,				System.Guid managerid
,				System.Guid tid
,				System.Int32 pid
,				System.Int32 ppos
,				System.Int32 pposon
,				System.Int32 kpi
,				System.Int32 level
,				System.Int32 strength
,				System.Int32 showorder
,				System.Boolean ismain
,				System.String readyskills
,				System.String liveskills
,				System.Double speedconst
,				System.Double speedcount
,				System.Double shootconst
,				System.Double shootcount
,				System.Double freekickconst
,				System.Double freekickcount
,				System.Double balanceconst
,				System.Double balancecount
,				System.Double physiqueconst
,				System.Double physiquecount
,				System.Double powerconst
,				System.Double powercount
,				System.Double aggressionconst
,				System.Double aggressioncount
,				System.Double disturbconst
,				System.Double disturbcount
,				System.Double interceptionconst
,				System.Double interceptioncount
,				System.Double dribbleconst
,				System.Double dribblecount
,				System.Double passconst
,				System.Double passcount
,				System.Double mentalityconst
,				System.Double mentalitycount
,				System.Double responseconst
,				System.Double responsecount
,				System.Double positioningconst
,				System.Double positioningcount
,				System.Double handcontrolconst
,				System.Double handcontrolcount
,				System.Double accelerationconst
,				System.Double accelerationcount
,				System.Double bounceconst
,				System.Double bouncecount
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.Id = id;
			this.ManagerId = managerid;
			this.Tid = tid;
			this.Pid = pid;
			this.PPos = ppos;
			this.PPosOn = pposon;
			this.Kpi = kpi;
			this.Level = level;
			this.Strength = strength;
			this.ShowOrder = showorder;
			this.IsMain = ismain;
			this.ReadySkills = readyskills;
			this.LiveSkills = liveskills;
			this.SpeedConst = speedconst;
			this.SpeedCount = speedcount;
			this.ShootConst = shootconst;
			this.ShootCount = shootcount;
			this.FreeKickConst = freekickconst;
			this.FreeKickCount = freekickcount;
			this.BalanceConst = balanceconst;
			this.BalanceCount = balancecount;
			this.PhysiqueConst = physiqueconst;
			this.PhysiqueCount = physiquecount;
			this.PowerConst = powerconst;
			this.PowerCount = powercount;
			this.AggressionConst = aggressionconst;
			this.AggressionCount = aggressioncount;
			this.DisturbConst = disturbconst;
			this.DisturbCount = disturbcount;
			this.InterceptionConst = interceptionconst;
			this.InterceptionCount = interceptioncount;
			this.DribbleConst = dribbleconst;
			this.DribbleCount = dribblecount;
			this.PassConst = passconst;
			this.PassCount = passcount;
			this.MentalityConst = mentalityconst;
			this.MentalityCount = mentalitycount;
			this.ResponseConst = responseconst;
			this.ResponseCount = responsecount;
			this.PositioningConst = positioningconst;
			this.PositioningCount = positioningcount;
			this.HandControlConst = handcontrolconst;
			this.HandControlCount = handcontrolcount;
			this.AccelerationConst = accelerationconst;
			this.AccelerationCount = accelerationcount;
			this.BounceConst = bounceconst;
			this.BounceCount = bouncecount;
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
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///Tid
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid Tid {get ; set ;}

		///<summary>
		///Pid
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Pid {get ; set ;}

		///<summary>
		///PPos
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PPos {get ; set ;}

		///<summary>
		///PPosOn
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PPosOn {get ; set ;}

		///<summary>
		///Kpi
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Kpi {get ; set ;}

		///<summary>
		///Level
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///Strength
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Strength {get ; set ;}

		///<summary>
		///ShowOrder
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 ShowOrder {get ; set ;}

		///<summary>
		///IsMain
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Boolean IsMain {get ; set ;}

		///<summary>
		///ReadySkills
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String ReadySkills {get ; set ;}

		///<summary>
		///LiveSkills
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String LiveSkills {get ; set ;}

		///<summary>
		///SpeedConst
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Double SpeedConst {get ; set ;}

		///<summary>
		///SpeedCount
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Double SpeedCount {get ; set ;}

		///<summary>
		///ShootConst
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Double ShootConst {get ; set ;}

		///<summary>
		///ShootCount
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Double ShootCount {get ; set ;}

		///<summary>
		///FreeKickConst
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Double FreeKickConst {get ; set ;}

		///<summary>
		///FreeKickCount
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Double FreeKickCount {get ; set ;}

		///<summary>
		///BalanceConst
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Double BalanceConst {get ; set ;}

		///<summary>
		///BalanceCount
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Double BalanceCount {get ; set ;}

		///<summary>
		///PhysiqueConst
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Double PhysiqueConst {get ; set ;}

		///<summary>
		///PhysiqueCount
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Double PhysiqueCount {get ; set ;}

		///<summary>
		///PowerConst
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Double PowerConst {get ; set ;}

		///<summary>
		///PowerCount
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Double PowerCount {get ; set ;}

		///<summary>
		///AggressionConst
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Double AggressionConst {get ; set ;}

		///<summary>
		///AggressionCount
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Double AggressionCount {get ; set ;}

		///<summary>
		///DisturbConst
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Double DisturbConst {get ; set ;}

		///<summary>
		///DisturbCount
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Double DisturbCount {get ; set ;}

		///<summary>
		///InterceptionConst
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.Double InterceptionConst {get ; set ;}

		///<summary>
		///InterceptionCount
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.Double InterceptionCount {get ; set ;}

		///<summary>
		///DribbleConst
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.Double DribbleConst {get ; set ;}

		///<summary>
		///DribbleCount
		///</summary>
        [DataMember]
        [ProtoMember(33)]
		public System.Double DribbleCount {get ; set ;}

		///<summary>
		///PassConst
		///</summary>
        [DataMember]
        [ProtoMember(34)]
		public System.Double PassConst {get ; set ;}

		///<summary>
		///PassCount
		///</summary>
        [DataMember]
        [ProtoMember(35)]
		public System.Double PassCount {get ; set ;}

		///<summary>
		///MentalityConst
		///</summary>
        [DataMember]
        [ProtoMember(36)]
		public System.Double MentalityConst {get ; set ;}

		///<summary>
		///MentalityCount
		///</summary>
        [DataMember]
        [ProtoMember(37)]
		public System.Double MentalityCount {get ; set ;}

		///<summary>
		///ResponseConst
		///</summary>
        [DataMember]
        [ProtoMember(38)]
		public System.Double ResponseConst {get ; set ;}

		///<summary>
		///ResponseCount
		///</summary>
        [DataMember]
        [ProtoMember(39)]
		public System.Double ResponseCount {get ; set ;}

		///<summary>
		///PositioningConst
		///</summary>
        [DataMember]
        [ProtoMember(40)]
		public System.Double PositioningConst {get ; set ;}

		///<summary>
		///PositioningCount
		///</summary>
        [DataMember]
        [ProtoMember(41)]
		public System.Double PositioningCount {get ; set ;}

		///<summary>
		///HandControlConst
		///</summary>
        [DataMember]
        [ProtoMember(42)]
		public System.Double HandControlConst {get ; set ;}

		///<summary>
		///HandControlCount
		///</summary>
        [DataMember]
        [ProtoMember(43)]
		public System.Double HandControlCount {get ; set ;}

		///<summary>
		///AccelerationConst
		///</summary>
        [DataMember]
        [ProtoMember(44)]
		public System.Double AccelerationConst {get ; set ;}

		///<summary>
		///AccelerationCount
		///</summary>
        [DataMember]
        [ProtoMember(45)]
		public System.Double AccelerationCount {get ; set ;}

		///<summary>
		///BounceConst
		///</summary>
        [DataMember]
        [ProtoMember(46)]
		public System.Double BounceConst {get ; set ;}

		///<summary>
		///BounceCount
		///</summary>
        [DataMember]
        [ProtoMember(47)]
		public System.Double BounceCount {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(48)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(49)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagerbuffmemberEntity Clone()
        {
            NbManagerbuffmemberEntity entity = new NbManagerbuffmemberEntity();
			entity.Id = this.Id;
			entity.ManagerId = this.ManagerId;
			entity.Tid = this.Tid;
			entity.Pid = this.Pid;
			entity.PPos = this.PPos;
			entity.PPosOn = this.PPosOn;
			entity.Kpi = this.Kpi;
			entity.Level = this.Level;
			entity.Strength = this.Strength;
			entity.ShowOrder = this.ShowOrder;
			entity.IsMain = this.IsMain;
			entity.ReadySkills = this.ReadySkills;
			entity.LiveSkills = this.LiveSkills;
			entity.SpeedConst = this.SpeedConst;
			entity.SpeedCount = this.SpeedCount;
			entity.ShootConst = this.ShootConst;
			entity.ShootCount = this.ShootCount;
			entity.FreeKickConst = this.FreeKickConst;
			entity.FreeKickCount = this.FreeKickCount;
			entity.BalanceConst = this.BalanceConst;
			entity.BalanceCount = this.BalanceCount;
			entity.PhysiqueConst = this.PhysiqueConst;
			entity.PhysiqueCount = this.PhysiqueCount;
			entity.PowerConst = this.PowerConst;
			entity.PowerCount = this.PowerCount;
			entity.AggressionConst = this.AggressionConst;
			entity.AggressionCount = this.AggressionCount;
			entity.DisturbConst = this.DisturbConst;
			entity.DisturbCount = this.DisturbCount;
			entity.InterceptionConst = this.InterceptionConst;
			entity.InterceptionCount = this.InterceptionCount;
			entity.DribbleConst = this.DribbleConst;
			entity.DribbleCount = this.DribbleCount;
			entity.PassConst = this.PassConst;
			entity.PassCount = this.PassCount;
			entity.MentalityConst = this.MentalityConst;
			entity.MentalityCount = this.MentalityCount;
			entity.ResponseConst = this.ResponseConst;
			entity.ResponseCount = this.ResponseCount;
			entity.PositioningConst = this.PositioningConst;
			entity.PositioningCount = this.PositioningCount;
			entity.HandControlConst = this.HandControlConst;
			entity.HandControlCount = this.HandControlCount;
			entity.AccelerationConst = this.AccelerationConst;
			entity.AccelerationCount = this.AccelerationCount;
			entity.BounceConst = this.BounceConst;
			entity.BounceCount = this.BounceCount;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerBuffMember 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagerbuffmemberResponse : BaseResponse<NbManagerbuffmemberEntity>
    {

    }
}

