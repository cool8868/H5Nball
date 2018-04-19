
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Monitordailyevent_Delete    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorDailyevent_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorDailyevent_Delete]
GO

/****** Object:  Stored Procedure [dbo].MonitorDailyevent_GetById    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorDailyevent_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorDailyevent_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].MonitorDailyevent_GetAll    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorDailyevent_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorDailyevent_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].MonitorDailyevent_Insert    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorDailyevent_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorDailyevent_Insert]
GO

/****** Object:  Stored Procedure [dbo].MonitorDailyevent_Update    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorDailyevent_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorDailyevent_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_MonitorDailyevent_Delete
	@Idx int
AS

DELETE FROM [dbo].[Monitor_DailyEvent]
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

CREATE PROCEDURE [dbo].P_MonitorDailyevent_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Monitor_DailyEvent] with(nolock)
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

CREATE PROCEDURE [dbo].P_MonitorDailyevent_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Monitor_DailyEvent] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_MonitorDailyevent_Insert
	@ZoneId int , 
	@EventType int , 
	@OpenTime datetime , 
	@StartTime datetime , 
	@EndTime datetime , 
	@RecordDate datetime , 
	@Status int , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Monitor_DailyEvent] (
	[ZoneId]
	,[EventType]
	,[OpenTime]
	,[StartTime]
	,[EndTime]
	,[RecordDate]
	,[Status]
	,[UpdateTime]
) VALUES (
    @ZoneId
    ,@EventType
    ,@OpenTime
    ,@StartTime
    ,@EndTime
    ,@RecordDate
    ,@Status
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_MonitorDailyevent_Update
	@Idx int, 
	@ZoneId int, 
	@EventType int, 
	@OpenTime datetime, 
	@StartTime datetime, 
	@EndTime datetime, 
	@RecordDate datetime, 
	@Status int, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Monitor_DailyEvent] SET
	[ZoneId] = @ZoneId
	,[EventType] = @EventType
	,[OpenTime] = @OpenTime
	,[StartTime] = @StartTime
	,[EndTime] = @EndTime
	,[RecordDate] = @RecordDate
	,[Status] = @Status
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


