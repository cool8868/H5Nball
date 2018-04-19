
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Zonedailyeventtime_Delete    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ZoneDailyeventtime_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ZoneDailyeventtime_Delete]
GO

/****** Object:  Stored Procedure [dbo].ZoneDailyeventtime_GetById    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ZoneDailyeventtime_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ZoneDailyeventtime_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ZoneDailyeventtime_GetAll    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ZoneDailyeventtime_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ZoneDailyeventtime_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ZoneDailyeventtime_Insert    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ZoneDailyeventtime_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ZoneDailyeventtime_Insert]
GO

/****** Object:  Stored Procedure [dbo].ZoneDailyeventtime_Update    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ZoneDailyeventtime_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ZoneDailyeventtime_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ZoneDailyeventtime_Delete
	@Idx int
AS

DELETE FROM [dbo].[Zone_DailyEventTime]
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

CREATE PROCEDURE [dbo].P_ZoneDailyeventtime_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Zone_DailyEventTime] with(nolock)
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

CREATE PROCEDURE [dbo].P_ZoneDailyeventtime_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Zone_DailyEventTime] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ZoneDailyeventtime_Insert
	@Idx int
	,@ZoneId int
	,@EventType int
	,@OpenHour int
	,@OpenMinute int
	,@StartHour int
	,@StartMinute int
	,@EndHour int
	,@EndMinute int
	,@StartDay int
	,@EndDay int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Zone_DailyEventTime] (
	[Idx]
	,[ZoneId]
	,[EventType]
	,[OpenHour]
	,[OpenMinute]
	,[StartHour]
	,[StartMinute]
	,[EndHour]
	,[EndMinute]
	,[StartDay]
	,[EndDay]
) VALUES (
    @Idx
    ,@ZoneId
    ,@EventType
    ,@OpenHour
    ,@OpenMinute
    ,@StartHour
    ,@StartMinute
    ,@EndHour
    ,@EndMinute
    ,@StartDay
    ,@EndDay
)

select @Idx

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

CREATE PROCEDURE [dbo].P_ZoneDailyeventtime_Update
	@Idx int, 
	@ZoneId int, 
	@EventType int, 
	@OpenHour int, 
	@OpenMinute int, 
	@StartHour int, 
	@StartMinute int, 
	@EndHour int, 
	@EndMinute int, 
	@StartDay int, 
	@EndDay int 
AS



UPDATE [dbo].[Zone_DailyEventTime] SET
	[ZoneId] = @ZoneId
	,[EventType] = @EventType
	,[OpenHour] = @OpenHour
	,[OpenMinute] = @OpenMinute
	,[StartHour] = @StartHour
	,[StartMinute] = @StartMinute
	,[EndHour] = @EndHour
	,[EndMinute] = @EndMinute
	,[StartDay] = @StartDay
	,[EndDay] = @EndDay
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


