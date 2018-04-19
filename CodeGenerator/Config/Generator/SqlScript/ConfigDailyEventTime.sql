
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configdailyeventtime_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailyeventtime_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailyeventtime_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigDailyeventtime_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailyeventtime_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailyeventtime_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigDailyeventtime_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailyeventtime_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailyeventtime_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigDailyeventtime_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailyeventtime_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailyeventtime_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigDailyeventtime_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailyeventtime_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailyeventtime_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigDailyeventtime_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_DailyEventTime]
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

CREATE PROCEDURE [dbo].P_ConfigDailyeventtime_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_DailyEventTime] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigDailyeventtime_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_DailyEventTime] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigDailyeventtime_Insert
	@Idx int
	,@EventType int
	,@OpenHour int
	,@OpenMinute int
	,@StartHour int
	,@StartMinute int
	,@EndHour int
	,@EndMinute int
	,@Version int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_DailyEventTime] (
	[Idx]
	,[EventType]
	,[OpenHour]
	,[OpenMinute]
	,[StartHour]
	,[StartMinute]
	,[EndHour]
	,[EndMinute]
	,[Version]
) VALUES (
    @Idx
    ,@EventType
    ,@OpenHour
    ,@OpenMinute
    ,@StartHour
    ,@StartMinute
    ,@EndHour
    ,@EndMinute
    ,@Version
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

CREATE PROCEDURE [dbo].P_ConfigDailyeventtime_Update
	@Idx int, 
	@EventType int, 
	@OpenHour int, 
	@OpenMinute int, 
	@StartHour int, 
	@StartMinute int, 
	@EndHour int, 
	@EndMinute int, 
	@Version int 
AS



UPDATE [dbo].[Config_DailyEventTime] SET
	[EventType] = @EventType
	,[OpenHour] = @OpenHour
	,[OpenMinute] = @OpenMinute
	,[StartHour] = @StartHour
	,[StartMinute] = @StartMinute
	,[EndHour] = @EndHour
	,[EndMinute] = @EndMinute
	,[Version] = @Version
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



