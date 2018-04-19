
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Playerkillinfo_Delete    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].PlayerkillInfo_GetById    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PlayerkillInfo_GetAll    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PlayerkillInfo_Insert    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].PlayerkillInfo_Update    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PlayerkillInfo_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[PlayerKill_Info]
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

CREATE PROCEDURE [dbo].P_PlayerkillInfo_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PlayerKill_Info] with(nolock)
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

CREATE PROCEDURE [dbo].P_PlayerkillInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PlayerKill_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PlayerkillInfo_Insert
	@RemainTimes int , 
	@RemainByTimes int , 
	@BuyTimes int , 
	@DayWinTimes int , 
	@RecordDate datetime , 
	@Win int , 
	@Lose int , 
	@Draw int , 
	@LotteryMatchId uniqueidentifier , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@OpponentInfo varbinary(2000) , 
	@OpponentRefreshTime datetime , 
	@DayPoint int , 
	@SpecialItemNumber int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[PlayerKill_Info] (
	[ManagerId],
	[RemainTimes]
	,[RemainByTimes]
	,[BuyTimes]
	,[DayWinTimes]
	,[RecordDate]
	,[Win]
	,[Lose]
	,[Draw]
	,[LotteryMatchId]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[OpponentInfo]
	,[OpponentRefreshTime]
	,[DayPoint]
	,[SpecialItemNumber]
) VALUES (
	@ManagerId,
    @RemainTimes
    ,@RemainByTimes
    ,@BuyTimes
    ,@DayWinTimes
    ,@RecordDate
    ,@Win
    ,@Lose
    ,@Draw
    ,@LotteryMatchId
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@OpponentInfo
    ,@OpponentRefreshTime
    ,@DayPoint
    ,@SpecialItemNumber
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

CREATE PROCEDURE [dbo].P_PlayerkillInfo_Update
	@ManagerId uniqueidentifier, 
	@RemainTimes int, 
	@RemainByTimes int, 
	@BuyTimes int, 
	@DayWinTimes int, 
	@RecordDate datetime, 
	@Win int, 
	@Lose int, 
	@Draw int, 
	@LotteryMatchId uniqueidentifier, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@OpponentInfo varbinary(2000), 
	@OpponentRefreshTime datetime, 
	@DayPoint int, 
	@SpecialItemNumber int 
AS



UPDATE [dbo].[PlayerKill_Info] SET
	[RemainTimes] = @RemainTimes
	,[RemainByTimes] = @RemainByTimes
	,[BuyTimes] = @BuyTimes
	,[DayWinTimes] = @DayWinTimes
	,[RecordDate] = @RecordDate
	,[Win] = @Win
	,[Lose] = @Lose
	,[Draw] = @Draw
	,[LotteryMatchId] = @LotteryMatchId
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[OpponentInfo] = @OpponentInfo
	,[OpponentRefreshTime] = @OpponentRefreshTime
	,[DayPoint] = @DayPoint
	,[SpecialItemNumber] = @SpecialItemNumber
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


