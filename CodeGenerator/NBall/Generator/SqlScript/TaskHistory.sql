
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Taskhistory_Delete    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskHistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskHistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].TaskHistory_GetById    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskHistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskHistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TaskHistory_GetAll    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskHistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskHistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TaskHistory_Insert    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskHistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskHistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].TaskHistory_Update    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskHistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskHistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TaskHistory_Delete
	@Idx int
AS

DELETE FROM [dbo].[Task_History]
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

CREATE PROCEDURE [dbo].P_TaskHistory_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_History] with(nolock)
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

CREATE PROCEDURE [dbo].P_TaskHistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_History] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TaskHistory_Insert
	@ManagerId uniqueidentifier , 
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


INSERT INTO [dbo].[Task_History] (
	[ManagerId]
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

CREATE PROCEDURE [dbo].P_TaskHistory_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
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



UPDATE [dbo].[Task_History] SET
	[ManagerId] = @ManagerId
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



