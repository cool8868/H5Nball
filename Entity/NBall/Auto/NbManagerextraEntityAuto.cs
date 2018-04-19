
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerExtra 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagerextraEntity
	{
		
		public NbManagerextraEntity()
		{
		}

		public NbManagerextraEntity(
		System.Guid managerid
,				System.Int32 stamina
,				System.Int32 staminamax
,				System.DateTime resumestaminatime
,				System.Int32 helptraincount
,				System.Int32 byhelptraincount
,				System.DateTime recorddate
,				System.Int32 kpi
,				System.String functionlist
,				System.String guidebuffrecord
,				System.Boolean hasguideprize
,				System.DateTime guideprizeexpired
,				System.Boolean payfirstflag
,				System.DateTime paycontinudate
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Int32 vigor
,				System.DateTime levelgiftexpired
,				System.Int32 guideprizecount
,				System.DateTime guideprizelastdate
,				System.Int32 scouting
,				System.DateTime scoutingupdate
,				System.Int32 levelgiftstep
,				System.DateTime levelgiftexpired2
,				System.DateTime levelgiftexpired3
,				System.Int32 active
,				System.Boolean scoutingpointfirst
,				System.Int32 friendinvitecount
,				System.Int32 veterannumber
,				System.Int32 staminagiftstatus
,				System.Int32 guideitemcode
,				System.Boolean isguidelottery
,				System.Int32 leaguescore
,				System.Int32 coinscouting
,				System.DateTime coinscoutingupdate
,				System.Int32 friendscouting
,				System.DateTime friendscoutingupdate
,				System.Int32 openboxcount
,				System.Int32 skillpoint
,				System.Int32 skilltype
,				System.Int32 resetpotentialnumber
		)
		{
			this.ManagerId = managerid;
			this.Stamina = stamina;
			this.StaminaMax = staminamax;
			this.ResumeStaminaTime = resumestaminatime;
			this.HelpTrainCount = helptraincount;
			this.ByHelpTrainCount = byhelptraincount;
			this.RecordDate = recorddate;
			this.Kpi = kpi;
			this.FunctionList = functionlist;
			this.GuideBuffRecord = guidebuffrecord;
			this.HasGuidePrize = hasguideprize;
			this.GuidePrizeExpired = guideprizeexpired;
			this.PayFirstFlag = payfirstflag;
			this.PayContinuDate = paycontinudate;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.Vigor = vigor;
			this.LevelGiftExpired = levelgiftexpired;
			this.GuidePrizeCount = guideprizecount;
			this.GuidePrizeLastDate = guideprizelastdate;
			this.Scouting = scouting;
			this.ScoutingUpdate = scoutingupdate;
			this.LevelGiftStep = levelgiftstep;
			this.LevelGiftExpired2 = levelgiftexpired2;
			this.LevelGiftExpired3 = levelgiftexpired3;
			this.Active = active;
			this.ScoutingPointFirst = scoutingpointfirst;
			this.FriendInviteCount = friendinvitecount;
			this.VeteranNumber = veterannumber;
			this.StaminaGiftStatus = staminagiftstatus;
			this.GuideItemCode = guideitemcode;
			this.IsGuideLottery = isguidelottery;
			this.LeagueScore = leaguescore;
			this.CoinScouting = coinscouting;
			this.CoinScoutingUpdate = coinscoutingupdate;
			this.FriendScouting = friendscouting;
			this.FriendScoutingUpdate = friendscoutingupdate;
			this.OpenBoxCount = openboxcount;
			this.SkillPoint = skillpoint;
			this.SkillType = skilltype;
			this.ResetPotentialNumber = resetpotentialnumber;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///体力
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Stamina {get ; set ;}

		///<summary>
		///最大体力
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 StaminaMax {get ; set ;}

		///<summary>
		///体力恢复时间
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime ResumeStaminaTime {get ; set ;}

		///<summary>
		///帮助训练次数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 HelpTrainCount {get ; set ;}

		///<summary>
		///被帮助次数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ByHelpTrainCount {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///综合实力
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Kpi {get ; set ;}

		///<summary>
		///激活功能列表
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String FunctionList {get ; set ;}

		///<summary>
		///GuideBuffRecord
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String GuideBuffRecord {get ; set ;}

		///<summary>
		///是否有新手奖励
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Boolean HasGuidePrize {get ; set ;}

		///<summary>
		///新手奖励过期时间
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime GuidePrizeExpired {get ; set ;}

		///<summary>
		///首充领取标记，没有则显示首充活动
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Boolean PayFirstFlag {get ; set ;}

		///<summary>
		///连续充值记录日期
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime PayContinuDate {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(17)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///精力值
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 Vigor {get ; set ;}

		///<summary>
		///LevelGiftExpired
		///</summary>
        [DataMember]
        [ProtoMember(19)]
        [JsonIgnore]
		public System.DateTime LevelGiftExpired {get ; set ;}

		///<summary>
		///GuidePrizeCount
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 GuidePrizeCount {get ; set ;}

		///<summary>
		///GuidePrizeLastDate
		///</summary>
        [DataMember]
        [ProtoMember(21)]
        [JsonIgnore]
		public System.DateTime GuidePrizeLastDate {get ; set ;}

		///<summary>
		///球探抽卡免费次数
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 Scouting {get ; set ;}

		///<summary>
		///球探抽卡免费次数刷新时间
		///</summary>
        [DataMember]
        [ProtoMember(23)]
        [JsonIgnore]
		public System.DateTime ScoutingUpdate {get ; set ;}

		///<summary>
		///LevelGiftStep
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 LevelGiftStep {get ; set ;}

		///<summary>
		///LevelGiftExpired2
		///</summary>
        [DataMember]
        [ProtoMember(25)]
        [JsonIgnore]
		public System.DateTime LevelGiftExpired2 {get ; set ;}

		///<summary>
		///LevelGiftExpired3
		///</summary>
        [DataMember]
        [ProtoMember(26)]
        [JsonIgnore]
		public System.DateTime LevelGiftExpired3 {get ; set ;}

		///<summary>
		///活跃度
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Int32 Active {get ; set ;}

		///<summary>
		///ScoutingPointFirst
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Boolean ScoutingPointFirst {get ; set ;}

		///<summary>
		///FriendInviteCount
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Int32 FriendInviteCount {get ; set ;}

		///<summary>
		///VeteranNumber
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.Int32 VeteranNumber {get ; set ;}

		///<summary>
		///StaminaGiftStatus
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.Int32 StaminaGiftStatus {get ; set ;}

		///<summary>
		///GuideItemCode
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.Int32 GuideItemCode {get ; set ;}

		///<summary>
		///IsGuideLottery
		///</summary>
        [DataMember]
        [ProtoMember(33)]
		public System.Boolean IsGuideLottery {get ; set ;}

		///<summary>
		///联赛积分
		///</summary>
        [DataMember]
        [ProtoMember(34)]
		public System.Int32 LeagueScore {get ; set ;}

		///<summary>
		///金币抽卡免费次数
		///</summary>
        [DataMember]
        [ProtoMember(35)]
		public System.Int32 CoinScouting {get ; set ;}

		///<summary>
		///金币抽卡免费次数刷新时间
		///</summary>
        [DataMember]
        [ProtoMember(36)]
        [JsonIgnore]
		public System.DateTime CoinScoutingUpdate {get ; set ;}

		///<summary>
		///友情点抽卡免费次数
		///</summary>
        [DataMember]
        [ProtoMember(37)]
		public System.Int32 FriendScouting {get ; set ;}

		///<summary>
		///友情点抽卡免费次数刷新时间
		///</summary>
        [DataMember]
        [ProtoMember(38)]
        [JsonIgnore]
		public System.DateTime FriendScoutingUpdate {get ; set ;}

		///<summary>
		///OpenBoxCount
		///</summary>
        [DataMember]
        [ProtoMember(39)]
		public System.Int32 OpenBoxCount {get ; set ;}

		///<summary>
		///技能点数
		///</summary>
        [DataMember]
        [ProtoMember(40)]
		public System.Int32 SkillPoint {get ; set ;}

		///<summary>
		///经理天赋类型
		///</summary>
        [DataMember]
        [ProtoMember(41)]
		public System.Int32 SkillType {get ; set ;}

		///<summary>
		///ResetPotentialNumber
		///</summary>
        [DataMember]
        [ProtoMember(42)]
		public System.Int32 ResetPotentialNumber {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagerextraEntity Clone()
        {
            NbManagerextraEntity entity = new NbManagerextraEntity();
			entity.ManagerId = this.ManagerId;
			entity.Stamina = this.Stamina;
			entity.StaminaMax = this.StaminaMax;
			entity.ResumeStaminaTime = this.ResumeStaminaTime;
			entity.HelpTrainCount = this.HelpTrainCount;
			entity.ByHelpTrainCount = this.ByHelpTrainCount;
			entity.RecordDate = this.RecordDate;
			entity.Kpi = this.Kpi;
			entity.FunctionList = this.FunctionList;
			entity.GuideBuffRecord = this.GuideBuffRecord;
			entity.HasGuidePrize = this.HasGuidePrize;
			entity.GuidePrizeExpired = this.GuidePrizeExpired;
			entity.PayFirstFlag = this.PayFirstFlag;
			entity.PayContinuDate = this.PayContinuDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.Vigor = this.Vigor;
			entity.LevelGiftExpired = this.LevelGiftExpired;
			entity.GuidePrizeCount = this.GuidePrizeCount;
			entity.GuidePrizeLastDate = this.GuidePrizeLastDate;
			entity.Scouting = this.Scouting;
			entity.ScoutingUpdate = this.ScoutingUpdate;
			entity.LevelGiftStep = this.LevelGiftStep;
			entity.LevelGiftExpired2 = this.LevelGiftExpired2;
			entity.LevelGiftExpired3 = this.LevelGiftExpired3;
			entity.Active = this.Active;
			entity.ScoutingPointFirst = this.ScoutingPointFirst;
			entity.FriendInviteCount = this.FriendInviteCount;
			entity.VeteranNumber = this.VeteranNumber;
			entity.StaminaGiftStatus = this.StaminaGiftStatus;
			entity.GuideItemCode = this.GuideItemCode;
			entity.IsGuideLottery = this.IsGuideLottery;
			entity.LeagueScore = this.LeagueScore;
			entity.CoinScouting = this.CoinScouting;
			entity.CoinScoutingUpdate = this.CoinScoutingUpdate;
			entity.FriendScouting = this.FriendScouting;
			entity.FriendScoutingUpdate = this.FriendScoutingUpdate;
			entity.OpenBoxCount = this.OpenBoxCount;
			entity.SkillPoint = this.SkillPoint;
			entity.SkillType = this.SkillType;
			entity.ResetPotentialNumber = this.ResetPotentialNumber;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerExtra 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagerextraResponse : BaseResponse<NbManagerextraEntity>
    {

    }
}
