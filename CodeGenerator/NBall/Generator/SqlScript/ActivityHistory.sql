
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityhistory_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityHistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityHistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityHistory_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityHistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityHistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityHistory_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityHistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityHistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityHistory_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityHistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityHistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityHistory_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityHistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityHistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityHistory_Delete
	@Idx int
AS

DELETE FROM [dbo].[Activity_History]
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

CREATE PROCEDURE [dbo].P_ActivityHistory_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Activity_History] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityHistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Activity_History] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityHistory_Insert
	@ManagerId uniqueidentifier , 
	@ActivityId int , 
	@ActivityStep int , 
	@StepRecord varchar(50) , 
	@RecordDate datetime , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Activity_History] (
	[ManagerId]
	,[ActivityId]
	,[ActivityStep]
	,[StepRecord]
	,[RecordDate]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @ManagerId
    ,@ActivityId
    ,@ActivityStep
    ,@StepRecord
    ,@RecordDate
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

CREATE PROCEDURE [dbo].P_ActivityHistory_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ActivityId int, -- 活动id
	@ActivityStep int, -- 活动步骤
	@StepRecord varchar(50), -- 步骤参数记录
	@RecordDate datetime, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Activity_History] SET
	[ManagerId] = @ManagerId
	,[ActivityId] = @ActivityId
	,[ActivityStep] = @ActivityStep
	,[StepRecord] = @StepRecord
	,[RecordDate] = @RecordDate
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



