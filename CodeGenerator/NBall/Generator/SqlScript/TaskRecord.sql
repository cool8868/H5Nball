
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Taskrecord_Delete    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].TaskRecord_GetById    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TaskRecord_GetAll    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TaskRecord_Insert    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].TaskRecord_Update    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TaskRecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Task_Record]
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

CREATE PROCEDURE [dbo].P_TaskRecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_TaskRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TaskRecord_Insert
	@ManagerId uniqueidentifier , 
	@TaskId int , 
	@CurTimes int , 
	@StepRecord varchar(20) , 
	@DoneParam varbinary(200) , 
	@ManagerLevel int , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Task_Record] (
	[ManagerId]
	,[TaskId]
	,[CurTimes]
	,[StepRecord]
	,[DoneParam]
	,[ManagerLevel]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @ManagerId
    ,@TaskId
    ,@CurTimes
    ,@StepRecord
    ,@DoneParam
    ,@ManagerLevel
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

CREATE PROCEDURE [dbo].P_TaskRecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@TaskId int, -- 任务id
	@CurTimes int, 
	@StepRecord varchar(20), -- 任务执行情况
	@DoneParam varbinary(200), -- 任务执行参数记录，要求唯一id的任务需要用到
	@ManagerLevel int, -- 需要等级，挂起时有效
	@Status int, -- 任务状态：0，初始；1，已完成；2，放弃；-1，挂起
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Task_Record] SET
	[ManagerId] = @ManagerId
	,[TaskId] = @TaskId
	,[CurTimes] = @CurTimes
	,[StepRecord] = @StepRecord
	,[DoneParam] = @DoneParam
	,[ManagerLevel] = @ManagerLevel
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



