
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Taskdailyhistory_Delete    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyhistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyhistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].TaskDailyhistory_GetById    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyhistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyhistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TaskDailyhistory_GetAll    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyhistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyhistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TaskDailyhistory_Insert    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyhistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyhistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].TaskDailyhistory_Update    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskDailyhistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskDailyhistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TaskDailyhistory_Delete
	@Idx int
AS

DELETE FROM [dbo].[Task_DailyHistory]
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

CREATE PROCEDURE [dbo].P_TaskDailyhistory_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_DailyHistory] with(nolock)
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

CREATE PROCEDURE [dbo].P_TaskDailyhistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_DailyHistory] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TaskDailyhistory_Insert
	@ManagerId uniqueidentifier , 
	@DailyCount int , 
	@RecordDate datetime , 
	@TaskId int , 
	@CurTimes int , 
	@StepRecord varchar(20) , 
	@DoneParam varbinary(200) , 
	@PrizeExp int , 
	@PrizeCoin int , 
	@PrizeItemCode int , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Task_DailyHistory] (
	[ManagerId]
	,[DailyCount]
	,[RecordDate]
	,[TaskId]
	,[CurTimes]
	,[StepRecord]
	,[DoneParam]
	,[PrizeExp]
	,[PrizeCoin]
	,[PrizeItemCode]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @ManagerId
    ,@DailyCount
    ,@RecordDate
    ,@TaskId
    ,@CurTimes
    ,@StepRecord
    ,@DoneParam
    ,@PrizeExp
    ,@PrizeCoin
    ,@PrizeItemCode
    ,@Status
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_TaskDailyhistory_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@DailyCount int, 
	@RecordDate datetime, 
	@TaskId int, -- 任务id
	@CurTimes int, 
	@StepRecord varchar(20), -- 任务执行情况
	@DoneParam varbinary(200), -- 任务执行参数记录，要求唯一id的任务需要用到
	@PrizeExp int, 
	@PrizeCoin int, 
	@PrizeItemCode int, 
	@Status int, -- 任务状态：0，初始；1，已完成；2，放弃
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Task_DailyHistory] SET
	[ManagerId] = @ManagerId
	,[DailyCount] = @DailyCount
	,[RecordDate] = @RecordDate
	,[TaskId] = @TaskId
	,[CurTimes] = @CurTimes
	,[StepRecord] = @StepRecord
	,[DoneParam] = @DoneParam
	,[PrizeExp] = @PrizeExp
	,[PrizeCoin] = @PrizeCoin
	,[PrizeItemCode] = @PrizeItemCode
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



