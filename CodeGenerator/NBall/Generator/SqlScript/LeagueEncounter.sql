
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Leagueencounter_Delete    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueEncounter_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueEncounter_Delete]
GO

/****** Object:  Stored Procedure [dbo].LeagueEncounter_GetById    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueEncounter_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueEncounter_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LeagueEncounter_GetAll    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueEncounter_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueEncounter_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LeagueEncounter_Insert    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueEncounter_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueEncounter_Insert]
GO

/****** Object:  Stored Procedure [dbo].LeagueEncounter_Update    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueEncounter_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueEncounter_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LeagueEncounter_Delete
	@MatchId uniqueidentifier
AS

DELETE FROM [dbo].[League_Encounter]
WHERE
	[MatchId] = @MatchId

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

CREATE PROCEDURE [dbo].P_LeagueEncounter_GetById
	@MatchId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_Encounter] with(nolock)
WHERE
	[MatchId] = @MatchId
	
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

CREATE PROCEDURE [dbo].P_LeagueEncounter_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_Encounter] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LeagueEncounter_Insert
	@LeagueRecordId uniqueidentifier , 
	@HomeName varchar(50) , 
	@AwayName varchar(50) , 
	@WheelNumber int , 
	@HomeId uniqueidentifier , 
	@AwayId uniqueidentifier , 
	@HomeIsNpc bit , 
	@AwayIsNpc bit , 
	@IsMatch bit , 
	@ReMatched bit , 
	@Confirmed bit , 
	@HomeGoals int , 
	@AwayGoals int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @MatchId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[League_Encounter] (
	[MatchId],
	[LeagueRecordId]
	,[HomeName]
	,[AwayName]
	,[WheelNumber]
	,[HomeId]
	,[AwayId]
	,[HomeIsNpc]
	,[AwayIsNpc]
	,[IsMatch]
	,[ReMatched]
	,[Confirmed]
	,[HomeGoals]
	,[AwayGoals]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@MatchId,
    @LeagueRecordId
    ,@HomeName
    ,@AwayName
    ,@WheelNumber
    ,@HomeId
    ,@AwayId
    ,@HomeIsNpc
    ,@AwayIsNpc
    ,@IsMatch
    ,@ReMatched
    ,@Confirmed
    ,@HomeGoals
    ,@AwayGoals
    ,@UpdateTime
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_LeagueEncounter_Update
	@MatchId uniqueidentifier, -- 比赛ID
	@LeagueRecordId uniqueidentifier, -- 联赛记录ID
	@HomeName varchar(50), -- 主队经理名
	@AwayName varchar(50), -- 客队经理名
	@WheelNumber int, -- 轮数
	@HomeId uniqueidentifier, -- 主队经理ID
	@AwayId uniqueidentifier, -- 客队经理ID
	@HomeIsNpc bit, -- 主队是否是NPC
	@AwayIsNpc bit, -- 客队是否是NPC
	@IsMatch bit, -- 是否比赛了
	@ReMatched bit, -- 是否已重赛过
	@Confirmed bit, -- 是否已确认过
	@HomeGoals int, -- 主队进球数
	@AwayGoals int, -- 客队进球数
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[League_Encounter] SET
	[LeagueRecordId] = @LeagueRecordId
	,[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[WheelNumber] = @WheelNumber
	,[HomeId] = @HomeId
	,[AwayId] = @AwayId
	,[HomeIsNpc] = @HomeIsNpc
	,[AwayIsNpc] = @AwayIsNpc
	,[IsMatch] = @IsMatch
	,[ReMatched] = @ReMatched
	,[Confirmed] = @Confirmed
	,[HomeGoals] = @HomeGoals
	,[AwayGoals] = @AwayGoals
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[MatchId] = @MatchId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


