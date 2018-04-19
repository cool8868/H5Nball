
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Leaguewincountrecord_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueWincountrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueWincountrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].LeagueWincountrecord_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueWincountrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueWincountrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LeagueWincountrecord_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueWincountrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueWincountrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LeagueWincountrecord_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueWincountrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueWincountrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].LeagueWincountrecord_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueWincountrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueWincountrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LeagueWincountrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[League_WincountRecord]
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

CREATE PROCEDURE [dbo].P_LeagueWincountrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_WincountRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_LeagueWincountrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_WincountRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LeagueWincountrecord_Insert
	@ManagerId uniqueidentifier , 
	@LeagueId int , 
	@WinCount1 int , 
	@WinCount1Status int , 
	@WinCount2 int , 
	@WinCount2Status int , 
	@WinCount3 int , 
	@WinCount3Status int , 
	@MaxWinCount int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
	@PrizeDate date , 
	@PrizeStep varchar(50) , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[League_WincountRecord] (
	[ManagerId]
	,[LeagueId]
	,[WinCount1]
	,[WinCount1Status]
	,[WinCount2]
	,[WinCount2Status]
	,[WinCount3]
	,[WinCount3Status]
	,[MaxWinCount]
	,[UpdateTime]
	,[RowTime]
	,[PrizeDate]
	,[PrizeStep]
) VALUES (
    @ManagerId
    ,@LeagueId
    ,@WinCount1
    ,@WinCount1Status
    ,@WinCount2
    ,@WinCount2Status
    ,@WinCount3
    ,@WinCount3Status
    ,@MaxWinCount
    ,@UpdateTime
    ,@RowTime
    ,@PrizeDate
    ,@PrizeStep
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

CREATE PROCEDURE [dbo].P_LeagueWincountrecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@LeagueId int, 
	@WinCount1 int, 
	@WinCount1Status int, -- 0初始化 1可领取 2已领取
	@WinCount2 int, 
	@WinCount2Status int, -- 0初始化 1可领取 2已领取
	@WinCount3 int, 
	@WinCount3Status int, -- 0初始化 1可领取 2已领取
	@MaxWinCount int, 
	@UpdateTime datetime, 
	@RowTime datetime, 
	@PrizeDate date, 
	@PrizeStep varchar(50) 
AS



UPDATE [dbo].[League_WincountRecord] SET
	[ManagerId] = @ManagerId
	,[LeagueId] = @LeagueId
	,[WinCount1] = @WinCount1
	,[WinCount1Status] = @WinCount1Status
	,[WinCount2] = @WinCount2
	,[WinCount2Status] = @WinCount2Status
	,[WinCount3] = @WinCount3
	,[WinCount3Status] = @WinCount3Status
	,[MaxWinCount] = @MaxWinCount
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
	,[PrizeDate] = @PrizeDate
	,[PrizeStep] = @PrizeStep
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



