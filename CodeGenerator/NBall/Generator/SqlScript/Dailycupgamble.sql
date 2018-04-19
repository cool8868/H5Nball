
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dailycupgamble_Delete    Script Date: 2016年5月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupGamble_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupGamble_Delete]
GO

/****** Object:  Stored Procedure [dbo].DailycupGamble_GetById    Script Date: 2016年5月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupGamble_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupGamble_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DailycupGamble_GetAll    Script Date: 2016年5月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupGamble_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupGamble_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DailycupGamble_Insert    Script Date: 2016年5月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupGamble_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupGamble_Insert]
GO

/****** Object:  Stored Procedure [dbo].DailycupGamble_Update    Script Date: 2016年5月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupGamble_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupGamble_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DailycupGamble_Delete
	@Idx int
AS

DELETE FROM [dbo].[DailyCup_Gamble]
WHERE
	[Idx] = @Idx

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

CREATE PROCEDURE [dbo].P_DailycupGamble_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyCup_Gamble] with(nolock)
WHERE
	[Idx] = @Idx
	
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

CREATE PROCEDURE [dbo].P_DailycupGamble_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyCup_Gamble] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DailycupGamble_Insert
	@ManagerId uniqueidentifier , 
	@GamblePoint int , 
	@MatchId uniqueidentifier , 
	@HomeName nvarchar(50) , 
	@AwayName nvarchar(50) , 
	@DailyCupId int , 
	@RoundLevel int , 
	@GambleResult int , 
	@GambleManagerId uniqueidentifier , 
	@GambleManagerName nvarchar(50) , 
	@ResultPoint int , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[DailyCup_Gamble] (
	[ManagerId]
	,[GamblePoint]
	,[MatchId]
	,[HomeName]
	,[AwayName]
	,[DailyCupId]
	,[RoundLevel]
	,[GambleResult]
	,[GambleManagerId]
	,[GambleManagerName]
	,[ResultPoint]
	,[Status]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@GamblePoint
    ,@MatchId
    ,@HomeName
    ,@AwayName
    ,@DailyCupId
    ,@RoundLevel
    ,@GambleResult
    ,@GambleManagerId
    ,@GambleManagerName
    ,@ResultPoint
    ,@Status
    ,@RowTime
)


SET @Idx = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_DailycupGamble_Update
	@Idx int, 
	@ManagerId uniqueidentifier, -- 经理id
	@GamblePoint int, -- 押注砖石数量
	@MatchId uniqueidentifier, -- 下注的比赛
	@HomeName nvarchar(50), 
	@AwayName nvarchar(50), 
	@DailyCupId int, -- 杯赛id
	@RoundLevel int, 
	@GambleResult int, -- 押注的值--主队比赛结果（0：平，1：赢，2：败）
	@GambleManagerId uniqueidentifier, 
	@GambleManagerName nvarchar(50), -- 被押注经理名称
	@ResultPoint int, -- 返回砖石数量
	@Status int, -- 0为未处理，1为已开奖且竞猜成功，2为已开奖且竞猜失败
	@RowTime datetime 
AS



UPDATE [dbo].[DailyCup_Gamble] SET
	[ManagerId] = @ManagerId
	,[GamblePoint] = @GamblePoint
	,[MatchId] = @MatchId
	,[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[DailyCupId] = @DailyCupId
	,[RoundLevel] = @RoundLevel
	,[GambleResult] = @GambleResult
	,[GambleManagerId] = @GambleManagerId
	,[GambleManagerName] = @GambleManagerName
	,[ResultPoint] = @ResultPoint
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



