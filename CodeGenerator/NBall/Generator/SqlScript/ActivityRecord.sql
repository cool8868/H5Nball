
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityrecord_Delete    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityRecord_GetById    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityRecord_GetAll    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityRecord_Insert    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityRecord_Update    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityRecord_Delete
	@Idx int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Activity_Record]
WHERE
	[Idx] = @Idx
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

CREATE PROCEDURE [dbo].P_ActivityRecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Activity_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Activity_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityRecord_Insert
	@ManagerId uniqueidentifier , 
	@ActivityId int , 
	@ActivityStep int , 
	@StepRecord varchar(50) , 
	@RecordDate datetime , 
	@SettlementDate datetime , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Activity_Record] (
	[ManagerId]
	,[ActivityId]
	,[ActivityStep]
	,[StepRecord]
	,[RecordDate]
	,[SettlementDate]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @ManagerId
    ,@ActivityId
    ,@ActivityStep
    ,@StepRecord
    ,@RecordDate
    ,@SettlementDate
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

CREATE PROCEDURE [dbo].P_ActivityRecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ActivityId int, -- 活动id
	@ActivityStep int, -- 活动步骤
	@StepRecord varchar(50), -- 步骤参数记录
	@RecordDate datetime, 
	@SettlementDate datetime, 
	@Status int, -- 是否可领奖,0：否；1：是；2：今日已领完
	@RowTime datetime, 
	@UpdateTime datetime, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[Activity_Record] SET
	[ManagerId] = @ManagerId
	,[ActivityId] = @ActivityId
	,[ActivityStep] = @ActivityStep
	,[StepRecord] = @StepRecord
	,[RecordDate] = @RecordDate
	,[SettlementDate] = @SettlementDate
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



