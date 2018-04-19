
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Penaltykickmanager_Delete    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].PenaltykickManager_GetById    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PenaltykickManager_GetAll    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PenaltykickManager_Insert    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].PenaltykickManager_Update    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PenaltykickManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[PenaltyKick_Manager]
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

CREATE PROCEDURE [dbo].P_PenaltykickManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PenaltyKick_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_PenaltykickManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PenaltyKick_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PenaltykickManager_Insert
	@ShootNumber int , 
	@FreeNumber int , 
	@GameCurrency int , 
	@DayProduceLuckyCoin int , 
	@TotalScore int , 
	@AvailableScore int , 
	@TotalGoals int , 
	@ShooterAttribute int , 
	@ShootLog varchar(50) , 
	@CombGoals int , 
	@MaxCombGoals int , 
	@ExChangeString varchar(200) , 
	@Status int , 
	@Rank int , 
	@IsPrize bit , 
	@UpdateTime datetime , 
	@RowTime datetime , 
	@RefreshDate date , 
	@ScoreChangeTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[PenaltyKick_Manager] (
	[ManagerId],
	[ShootNumber]
	,[FreeNumber]
	,[GameCurrency]
	,[DayProduceLuckyCoin]
	,[TotalScore]
	,[AvailableScore]
	,[TotalGoals]
	,[ShooterAttribute]
	,[ShootLog]
	,[CombGoals]
	,[MaxCombGoals]
	,[ExChangeString]
	,[Status]
	,[Rank]
	,[IsPrize]
	,[UpdateTime]
	,[RowTime]
	,[RefreshDate]
	,[ScoreChangeTime]
) VALUES (
	@ManagerId,
    @ShootNumber
    ,@FreeNumber
    ,@GameCurrency
    ,@DayProduceLuckyCoin
    ,@TotalScore
    ,@AvailableScore
    ,@TotalGoals
    ,@ShooterAttribute
    ,@ShootLog
    ,@CombGoals
    ,@MaxCombGoals
    ,@ExChangeString
    ,@Status
    ,@Rank
    ,@IsPrize
    ,@UpdateTime
    ,@RowTime
    ,@RefreshDate
    ,@ScoreChangeTime
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

CREATE PROCEDURE [dbo].P_PenaltykickManager_Update
	@ManagerId uniqueidentifier, 
	@ShootNumber int, -- 游戏次数
	@FreeNumber int, -- 免费次数
	@GameCurrency int, -- 游戏币
	@DayProduceLuckyCoin int, -- 每天可产出的游戏币数量
	@TotalScore int, -- 总积分
	@AvailableScore int, -- 可用于兑换的积分
	@TotalGoals int, -- 总进球数
	@ShooterAttribute int, -- 射门属性
	@ShootLog varchar(50), -- 踢球记录
	@CombGoals int, -- 连续进球数
	@MaxCombGoals int, -- 当前游戏最大连续进球数
	@ExChangeString varchar(200), -- 可兑换的物品
	@Status int, -- 游戏状态  0=初始 1=踢球中 2=踢球结束可以领奖
	@Rank int, 
	@IsPrize bit, 
	@UpdateTime datetime, 
	@RowTime datetime, 
	@RefreshDate date, -- 刷新时间
	@ScoreChangeTime datetime -- 积分变化时间 用于排名
AS



UPDATE [dbo].[PenaltyKick_Manager] SET
	[ShootNumber] = @ShootNumber
	,[FreeNumber] = @FreeNumber
	,[GameCurrency] = @GameCurrency
	,[DayProduceLuckyCoin] = @DayProduceLuckyCoin
	,[TotalScore] = @TotalScore
	,[AvailableScore] = @AvailableScore
	,[TotalGoals] = @TotalGoals
	,[ShooterAttribute] = @ShooterAttribute
	,[ShootLog] = @ShootLog
	,[CombGoals] = @CombGoals
	,[MaxCombGoals] = @MaxCombGoals
	,[ExChangeString] = @ExChangeString
	,[Status] = @Status
	,[Rank] = @Rank
	,[IsPrize] = @IsPrize
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
	,[RefreshDate] = @RefreshDate
	,[ScoreChangeTime] = @ScoreChangeTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


