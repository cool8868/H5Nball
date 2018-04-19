
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagerextra_Delete    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerextra_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerextra_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagerextra_GetById    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerextra_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerextra_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagerextra_GetAll    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerextra_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerextra_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagerextra_Insert    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerextra_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerextra_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagerextra_Update    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerextra_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerextra_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerextra_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[NB_ManagerExtra]
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerextra_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerExtra] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerextra_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerExtra] with(nolock)
	
RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE [dbo].P_NbManagerextra_Insert
	@Stamina int , 
	@StaminaMax int , 
	@ResumeStaminaTime datetime , 
	@HelpTrainCount int , 
	@ByHelpTrainCount int , 
	@RecordDate datetime , 
	@Kpi int , 
	@FunctionList varchar(80) , 
	@GuideBuffRecord varchar(50) , 
	@HasGuidePrize bit , 
	@GuidePrizeExpired datetime , 
	@PayFirstFlag bit , 
	@PayContinuDate datetime , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@Vigor int , 
	@LevelGiftExpired datetime , 
	@GuidePrizeCount int , 
	@GuidePrizeLastDate datetime , 
	@Scouting int , 
	@ScoutingUpdate datetime , 
	@LevelGiftStep int , 
	@LevelGiftExpired2 datetime , 
	@LevelGiftExpired3 datetime , 
	@Active int , 
	@ScoutingPointFirst bit , 
	@FriendInviteCount int , 
	@VeteranNumber int , 
	@StaminaGiftStatus int , 
	@GuideItemCode int , 
	@IsGuideLottery bit , 
	@LeagueScore int , 
	@CoinScouting int , 
	@CoinScoutingUpdate datetime , 
	@FriendScouting int , 
	@FriendScoutingUpdate datetime , 
	@OpenBoxCount int , 
	@SkillPoint int , 
	@SkillType int , 
	@ResetPotentialNumber int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerExtra] (
	[ManagerId],
	[Stamina]
	,[StaminaMax]
	,[ResumeStaminaTime]
	,[HelpTrainCount]
	,[ByHelpTrainCount]
	,[RecordDate]
	,[Kpi]
	,[FunctionList]
	,[GuideBuffRecord]
	,[HasGuidePrize]
	,[GuidePrizeExpired]
	,[PayFirstFlag]
	,[PayContinuDate]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[Vigor]
	,[LevelGiftExpired]
	,[GuidePrizeCount]
	,[GuidePrizeLastDate]
	,[Scouting]
	,[ScoutingUpdate]
	,[LevelGiftStep]
	,[LevelGiftExpired2]
	,[LevelGiftExpired3]
	,[Active]
	,[ScoutingPointFirst]
	,[FriendInviteCount]
	,[VeteranNumber]
	,[StaminaGiftStatus]
	,[GuideItemCode]
	,[IsGuideLottery]
	,[LeagueScore]
	,[CoinScouting]
	,[CoinScoutingUpdate]
	,[FriendScouting]
	,[FriendScoutingUpdate]
	,[OpenBoxCount]
	,[SkillPoint]
	,[SkillType]
	,[ResetPotentialNumber]
) VALUES (
	@ManagerId,
    @Stamina
    ,@StaminaMax
    ,@ResumeStaminaTime
    ,@HelpTrainCount
    ,@ByHelpTrainCount
    ,@RecordDate
    ,@Kpi
    ,@FunctionList
    ,@GuideBuffRecord
    ,@HasGuidePrize
    ,@GuidePrizeExpired
    ,@PayFirstFlag
    ,@PayContinuDate
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@Vigor
    ,@LevelGiftExpired
    ,@GuidePrizeCount
    ,@GuidePrizeLastDate
    ,@Scouting
    ,@ScoutingUpdate
    ,@LevelGiftStep
    ,@LevelGiftExpired2
    ,@LevelGiftExpired3
    ,@Active
    ,@ScoutingPointFirst
    ,@FriendInviteCount
    ,@VeteranNumber
    ,@StaminaGiftStatus
    ,@GuideItemCode
    ,@IsGuideLottery
    ,@LeagueScore
    ,@CoinScouting
    ,@CoinScoutingUpdate
    ,@FriendScouting
    ,@FriendScoutingUpdate
    ,@OpenBoxCount
    ,@SkillPoint
    ,@SkillType
    ,@ResetPotentialNumber
)




RETURN 0


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerextra_Update
	@ManagerId uniqueidentifier, 
	@Stamina int, -- 体力
	@StaminaMax int, -- 最大体力
	@ResumeStaminaTime datetime, -- 体力恢复时间
	@HelpTrainCount int, -- 帮助训练次数
	@ByHelpTrainCount int, -- 被帮助次数
	@RecordDate datetime, 
	@Kpi int, -- 综合实力
	@FunctionList varchar(80), -- 激活功能列表
	@GuideBuffRecord varchar(50), 
	@HasGuidePrize bit, -- 是否有新手奖励
	@GuidePrizeExpired datetime, -- 新手奖励过期时间
	@PayFirstFlag bit, -- 首充领取标记，没有则显示首充活动
	@PayContinuDate datetime, -- 连续充值记录日期
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@Vigor int, -- 精力值
	@LevelGiftExpired datetime, 
	@GuidePrizeCount int, 
	@GuidePrizeLastDate datetime, 
	@Scouting int, -- 球探抽卡免费次数
	@ScoutingUpdate datetime, -- 球探抽卡免费次数刷新时间
	@LevelGiftStep int, 
	@LevelGiftExpired2 datetime, 
	@LevelGiftExpired3 datetime, 
	@Active int, -- 活跃度
	@ScoutingPointFirst bit, 
	@FriendInviteCount int, 
	@VeteranNumber int, 
	@StaminaGiftStatus int, 
	@GuideItemCode int, 
	@IsGuideLottery bit, 
	@LeagueScore int, -- 联赛积分
	@CoinScouting int, -- 金币抽卡免费次数
	@CoinScoutingUpdate datetime, -- 金币抽卡免费次数刷新时间
	@FriendScouting int, -- 友情点抽卡免费次数
	@FriendScoutingUpdate datetime, -- 友情点抽卡免费次数刷新时间
	@OpenBoxCount int, 
	@SkillPoint int, -- 技能点数
	@SkillType int, -- 经理天赋类型
	@ResetPotentialNumber int 
AS



UPDATE [dbo].[NB_ManagerExtra] SET
	[Stamina] = @Stamina
	,[StaminaMax] = @StaminaMax
	,[ResumeStaminaTime] = @ResumeStaminaTime
	,[HelpTrainCount] = @HelpTrainCount
	,[ByHelpTrainCount] = @ByHelpTrainCount
	,[RecordDate] = @RecordDate
	,[Kpi] = @Kpi
	,[FunctionList] = @FunctionList
	,[GuideBuffRecord] = @GuideBuffRecord
	,[HasGuidePrize] = @HasGuidePrize
	,[GuidePrizeExpired] = @GuidePrizeExpired
	,[PayFirstFlag] = @PayFirstFlag
	,[PayContinuDate] = @PayContinuDate
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[Vigor] = @Vigor
	,[LevelGiftExpired] = @LevelGiftExpired
	,[GuidePrizeCount] = @GuidePrizeCount
	,[GuidePrizeLastDate] = @GuidePrizeLastDate
	,[Scouting] = @Scouting
	,[ScoutingUpdate] = @ScoutingUpdate
	,[LevelGiftStep] = @LevelGiftStep
	,[LevelGiftExpired2] = @LevelGiftExpired2
	,[LevelGiftExpired3] = @LevelGiftExpired3
	,[Active] = @Active
	,[ScoutingPointFirst] = @ScoutingPointFirst
	,[FriendInviteCount] = @FriendInviteCount
	,[VeteranNumber] = @VeteranNumber
	,[StaminaGiftStatus] = @StaminaGiftStatus
	,[GuideItemCode] = @GuideItemCode
	,[IsGuideLottery] = @IsGuideLottery
	,[LeagueScore] = @LeagueScore
	,[CoinScouting] = @CoinScouting
	,[CoinScoutingUpdate] = @CoinScoutingUpdate
	,[FriendScouting] = @FriendScouting
	,[FriendScoutingUpdate] = @FriendScoutingUpdate
	,[OpenBoxCount] = @OpenBoxCount
	,[SkillPoint] = @SkillPoint
	,[SkillType] = @SkillType
	,[ResetPotentialNumber] = @ResetPotentialNumber
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


