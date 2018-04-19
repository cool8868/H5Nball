
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Monitorschedule_Delete    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorSchedule_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorSchedule_Delete]
GO

/****** Object:  Stored Procedure [dbo].MonitorSchedule_GetById    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorSchedule_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorSchedule_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].MonitorSchedule_GetAll    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorSchedule_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorSchedule_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].MonitorSchedule_Insert    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorSchedule_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorSchedule_Insert]
GO

/****** Object:  Stored Procedure [dbo].MonitorSchedule_Update    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MonitorSchedule_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MonitorSchedule_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_MonitorSchedule_Delete
	@Idx int
AS

DELETE FROM [dbo].[Monitor_Schedule]
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

CREATE PROCEDURE [dbo].P_MonitorSchedule_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Monitor_Schedule] with(nolock)
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

CREATE PROCEDURE [dbo].P_MonitorSchedule_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Monitor_Schedule] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_MonitorSchedule_Insert
	@ZoneId int , 
	@ScheduleId int , 
	@AppId int , 
	@TerminalIp varchar(20) , 
	@StartTime datetime , 
	@NextInvokeTime datetime , 
	@EndTime datetime , 
	@LastFailTime datetime , 
	@Status int , 
	@SuccessTimes bigint , 
	@FailTimes bigint , 
	@RetryTimes int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Monitor_Schedule] (
	[ZoneId]
	,[ScheduleId]
	,[AppId]
	,[TerminalIp]
	,[StartTime]
	,[NextInvokeTime]
	,[EndTime]
	,[LastFailTime]
	,[Status]
	,[SuccessTimes]
	,[FailTimes]
	,[RetryTimes]
) VALUES (
    @ZoneId
    ,@ScheduleId
    ,@AppId
    ,@TerminalIp
    ,@StartTime
    ,@NextInvokeTime
    ,@EndTime
    ,@LastFailTime
    ,@Status
    ,@SuccessTimes
    ,@FailTimes
    ,@RetryTimes
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

CREATE PROCEDURE [dbo].P_MonitorSchedule_Update
	@Idx int, 
	@ZoneId int, 
	@ScheduleId int, -- 计划任务id
	@AppId int, 
	@TerminalIp varchar(20), -- ip地址
	@StartTime datetime, -- 开始时间
	@NextInvokeTime datetime, -- 下一次执行时间
	@EndTime datetime, -- 结束时间
	@LastFailTime datetime, 
	@Status int, -- 状态:1，开始；2，手工退出；3，成功；4，执行失败
	@SuccessTimes bigint, 
	@FailTimes bigint, 
	@RetryTimes int 
AS



UPDATE [dbo].[Monitor_Schedule] SET
	[ZoneId] = @ZoneId
	,[ScheduleId] = @ScheduleId
	,[AppId] = @AppId
	,[TerminalIp] = @TerminalIp
	,[StartTime] = @StartTime
	,[NextInvokeTime] = @NextInvokeTime
	,[EndTime] = @EndTime
	,[LastFailTime] = @LastFailTime
	,[Status] = @Status
	,[SuccessTimes] = @SuccessTimes
	,[FailTimes] = @FailTimes
	,[RetryTimes] = @RetryTimes
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



