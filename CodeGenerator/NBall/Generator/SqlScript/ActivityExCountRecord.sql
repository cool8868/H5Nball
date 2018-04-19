
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityexcountrecord_Delete    Script Date: 2016年6月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCountrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCountrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityexCountrecord_GetById    Script Date: 2016年6月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCountrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCountrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityexCountrecord_GetAll    Script Date: 2016年6月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCountrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCountrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityexCountrecord_Insert    Script Date: 2016年6月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCountrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCountrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityexCountrecord_Update    Script Date: 2016年6月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCountrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCountrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityexCountrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[ActivityEx_CountRecord]
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

CREATE PROCEDURE [dbo].P_ActivityexCountrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_CountRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityexCountrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_CountRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityexCountrecord_Insert
	@ZoneActivityId int , 
	@ManagerId uniqueidentifier , 
	@ExcitingId int , 
	@GroupId int , 
	@ExData int , 
	@CurData int , 
	@ExStep int , 
	@AlreadySendCount int , 
	@Status int , 
	@RecordDate date , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ActivityEx_CountRecord] (
	[ZoneActivityId]
	,[ManagerId]
	,[ExcitingId]
	,[GroupId]
	,[ExData]
	,[CurData]
	,[ExStep]
	,[AlreadySendCount]
	,[Status]
	,[RecordDate]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @ZoneActivityId
    ,@ManagerId
    ,@ExcitingId
    ,@GroupId
    ,@ExData
    ,@CurData
    ,@ExStep
    ,@AlreadySendCount
    ,@Status
    ,@RecordDate
    ,@UpdateTime
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_ActivityexCountrecord_Update
	@Idx int, 
	@ZoneActivityId int, 
	@ManagerId uniqueidentifier, 
	@ExcitingId int, 
	@GroupId int, 
	@ExData int, 
	@CurData int, 
	@ExStep int, 
	@AlreadySendCount int, -- 已经领取过的数量
	@Status int, 
	@RecordDate date, 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[ActivityEx_CountRecord] SET
	[ZoneActivityId] = @ZoneActivityId
	,[ManagerId] = @ManagerId
	,[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[ExData] = @ExData
	,[CurData] = @CurData
	,[ExStep] = @ExStep
	,[AlreadySendCount] = @AlreadySendCount
	,[Status] = @Status
	,[RecordDate] = @RecordDate
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



