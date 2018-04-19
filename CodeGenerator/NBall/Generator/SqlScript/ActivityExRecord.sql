
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityexrecord_Delete    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityexRecord_GetById    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityexRecord_GetAll    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityexRecord_Insert    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityexRecord_Update    Script Date: 2016年5月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityexRecord_Delete
	@Idx int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[ActivityEx_Record]
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

CREATE PROCEDURE [dbo].P_ActivityexRecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityexRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityexRecord_Insert
	@ManagerId uniqueidentifier , 
	@ZoneActivityId int , 
	@ExcitingId int , 
	@GroupId int , 
	@CurData int , 
	@ExData int , 
	@ExStep int , 
	@ReceiveTimes int , 
	@RecordDate datetime , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ActivityEx_Record] (
	[ManagerId]
	,[ZoneActivityId]
	,[ExcitingId]
	,[GroupId]
	,[CurData]
	,[ExData]
	,[ExStep]
	,[ReceiveTimes]
	,[RecordDate]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @ManagerId
    ,@ZoneActivityId
    ,@ExcitingId
    ,@GroupId
    ,@CurData
    ,@ExData
    ,@ExStep
    ,@ReceiveTimes
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

CREATE PROCEDURE [dbo].P_ActivityexRecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ZoneActivityId int, 
	@ExcitingId int, 
	@GroupId int, 
	@CurData int, 
	@ExData int, 
	@ExStep int, 
	@ReceiveTimes int, 
	@RecordDate datetime, 
	@Status int, -- 是否可领奖,0：否；1：是；2：今日已领完
	@RowTime datetime, 
	@UpdateTime datetime, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[ActivityEx_Record] SET
	[ManagerId] = @ManagerId
	,[ZoneActivityId] = @ZoneActivityId
	,[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[CurData] = @CurData
	,[ExData] = @ExData
	,[ExStep] = @ExStep
	,[ReceiveTimes] = @ReceiveTimes
	,[RecordDate] = @RecordDate
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



