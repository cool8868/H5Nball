
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Leaguemanagerrecord_Delete    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueManagerrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueManagerrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].LeagueManagerrecord_GetById    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueManagerrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueManagerrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LeagueManagerrecord_GetAll    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueManagerrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueManagerrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LeagueManagerrecord_Insert    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueManagerrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueManagerrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].LeagueManagerrecord_Update    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueManagerrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueManagerrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LeagueManagerrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[League_ManagerRecord]
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

CREATE PROCEDURE [dbo].P_LeagueManagerrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_ManagerRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_LeagueManagerrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_ManagerRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LeagueManagerrecord_Insert
	@ManagerId uniqueidentifier , 
	@LaegueId int , 
	@LeagueRecordId uniqueidentifier , 
	@LastPrizeLeagueRecordId uniqueidentifier , 
	@SendFirstPassPrize bit , 
	@MatchId uniqueidentifier , 
	@MaxWheelNumber int , 
	@Score int , 
	@IsLock bit , 
	@IsStart bit , 
	@IsPass bit , 
	@PassNumber int , 
	@MatchNumber int , 
	@WinNumber int , 
	@FlatNumber int , 
	@LoseNumber int , 
	@GoalsNumber int , 
	@FumbleNumber int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
	@FightDicId int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[League_ManagerRecord] (
	[ManagerId]
	,[LaegueId]
	,[LeagueRecordId]
	,[LastPrizeLeagueRecordId]
	,[SendFirstPassPrize]
	,[MatchId]
	,[MaxWheelNumber]
	,[Score]
	,[IsLock]
	,[IsStart]
	,[IsPass]
	,[PassNumber]
	,[MatchNumber]
	,[WinNumber]
	,[FlatNumber]
	,[LoseNumber]
	,[GoalsNumber]
	,[FumbleNumber]
	,[UpdateTime]
	,[RowTime]
	,[FightDicId]
) VALUES (
    @ManagerId
    ,@LaegueId
    ,@LeagueRecordId
    ,@LastPrizeLeagueRecordId
    ,@SendFirstPassPrize
    ,@MatchId
    ,@MaxWheelNumber
    ,@Score
    ,@IsLock
    ,@IsStart
    ,@IsPass
    ,@PassNumber
    ,@MatchNumber
    ,@WinNumber
    ,@FlatNumber
    ,@LoseNumber
    ,@GoalsNumber
    ,@FumbleNumber
    ,@UpdateTime
    ,@RowTime
    ,@FightDicId
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

CREATE PROCEDURE [dbo].P_LeagueManagerrecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@LaegueId int, -- 联赛ID
	@LeagueRecordId uniqueidentifier, -- 联赛记录ID
	@LastPrizeLeagueRecordId uniqueidentifier, -- 最后一次通关并且未领取奖励记录id
	@SendFirstPassPrize bit, -- 是否已领取首次通关奖励
	@MatchId uniqueidentifier, -- 未确认的比赛
	@MaxWheelNumber int, -- 总比赛轮数
	@Score int, -- 本次联赛获得积分
	@IsLock bit, -- 是否解锁了联赛
	@IsStart bit, -- 是否开始了本场联赛
	@IsPass bit, -- 本次联赛是否通关了
	@PassNumber int, -- 通关联赛次数
	@MatchNumber int, -- 本届联赛比赛次数
	@WinNumber int, -- 本届联赛胜场
	@FlatNumber int, -- 本届联赛平场
	@LoseNumber int, -- 本届联赛负场
	@GoalsNumber int, -- 本届总进球数
	@FumbleNumber int, -- 本届总失球数
	@UpdateTime datetime, 
	@RowTime datetime, 
	@FightDicId int 
AS



UPDATE [dbo].[League_ManagerRecord] SET
	[ManagerId] = @ManagerId
	,[LaegueId] = @LaegueId
	,[LeagueRecordId] = @LeagueRecordId
	,[LastPrizeLeagueRecordId] = @LastPrizeLeagueRecordId
	,[SendFirstPassPrize] = @SendFirstPassPrize
	,[MatchId] = @MatchId
	,[MaxWheelNumber] = @MaxWheelNumber
	,[Score] = @Score
	,[IsLock] = @IsLock
	,[IsStart] = @IsStart
	,[IsPass] = @IsPass
	,[PassNumber] = @PassNumber
	,[MatchNumber] = @MatchNumber
	,[WinNumber] = @WinNumber
	,[FlatNumber] = @FlatNumber
	,[LoseNumber] = @LoseNumber
	,[GoalsNumber] = @GoalsNumber
	,[FumbleNumber] = @FumbleNumber
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
	,[FightDicId] = @FightDicId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



