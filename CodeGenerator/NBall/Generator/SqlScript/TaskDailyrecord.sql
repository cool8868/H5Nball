
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Taskdailyrecord_Delete    Script Date: 2016年5月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].TaskDailyrecord_GetById    Script Date: 2016年5月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TaskDailyrecord_GetAll    Script Date: 2016年5月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TaskDailyrecord_Insert    Script Date: 2016年5月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].TaskDailyrecord_Update    Script Date: 2016年5月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TaskDailyrecord_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Task_DailyRecord]
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_TaskDailyrecord_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_DailyRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_TaskDailyrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_DailyRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TaskDailyrecord_Insert
	@DailyCount int , 
	@RecordDate datetime , 
	@TaskIds varchar(200) , 
	@CurTimes varchar(200) , 
	@StepRecords varchar(200) , 
	@DoneParam varbinary(200) , 
	@Status varchar(200) , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@IsReceive bit , 
	@FinishCount int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Task_DailyRecord] (
	[ManagerId],
	[DailyCount]
	,[RecordDate]
	,[TaskIds]
	,[CurTimes]
	,[StepRecords]
	,[DoneParam]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[IsReceive]
	,[FinishCount]
) VALUES (
	@ManagerId,
    @DailyCount
    ,@RecordDate
    ,@TaskIds
    ,@CurTimes
    ,@StepRecords
    ,@DoneParam
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@IsReceive
    ,@FinishCount
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

CREATE PROCEDURE [dbo].P_TaskDailyrecord_Update
	@ManagerId uniqueidentifier, 
	@DailyCount int, 
	@RecordDate datetime, 
	@TaskIds varchar(200), -- 任务id 以逗号分割
	@CurTimes varchar(200), 
	@StepRecords varchar(200), -- 任务执行情况 以|分割
	@DoneParam varbinary(200), -- 任务执行参数记录，要求唯一id的任务需要用到
	@Status varchar(200), -- 任务状态：0，初始；1，已完成；多任务逗号分割
	@RowTime datetime, 
	@UpdateTime datetime, 
	@RowVersion timestamp, 
	@IsReceive bit, 
	@FinishCount int 
AS



UPDATE [dbo].[Task_DailyRecord] SET
	[DailyCount] = @DailyCount
	,[RecordDate] = @RecordDate
	,[TaskIds] = @TaskIds
	,[CurTimes] = @CurTimes
	,[StepRecords] = @StepRecords
	,[DoneParam] = @DoneParam
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[IsReceive] = @IsReceive
	,[FinishCount] = @FinishCount
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



