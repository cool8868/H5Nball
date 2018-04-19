
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityexprizerecord_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexPrizerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexPrizerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityexPrizerecord_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexPrizerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexPrizerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityexPrizerecord_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexPrizerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexPrizerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityexPrizerecord_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexPrizerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexPrizerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityexPrizerecord_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexPrizerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexPrizerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityexPrizerecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[ActivityEx_PrizeRecord]
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

CREATE PROCEDURE [dbo].P_ActivityexPrizerecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_PrizeRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityexPrizerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_PrizeRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityexPrizerecord_Insert
	@ExKey varchar(50) , 
	@ExRecordId int , 
	@ManagerId uniqueidentifier , 
	@ZoneActivityId int , 
	@ExcitingId int , 
	@GroupId int , 
	@CurData int , 
	@ExData int , 
	@ExStep int , 
	@ReceiveTimes int , 
	@RecordDate datetime , 
	@SendTimes int , 
	@ReturnCode int , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ActivityEx_PrizeRecord] (
	[ExKey]
	,[ExRecordId]
	,[ManagerId]
	,[ZoneActivityId]
	,[ExcitingId]
	,[GroupId]
	,[CurData]
	,[ExData]
	,[ExStep]
	,[ReceiveTimes]
	,[RecordDate]
	,[SendTimes]
	,[ReturnCode]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @ExKey
    ,@ExRecordId
    ,@ManagerId
    ,@ZoneActivityId
    ,@ExcitingId
    ,@GroupId
    ,@CurData
    ,@ExData
    ,@ExStep
    ,@ReceiveTimes
    ,@RecordDate
    ,@SendTimes
    ,@ReturnCode
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

CREATE PROCEDURE [dbo].P_ActivityexPrizerecord_Update
	@Idx int, 
	@ExKey varchar(50), 
	@ExRecordId int, 
	@ManagerId uniqueidentifier, 
	@ZoneActivityId int, 
	@ExcitingId int, 
	@GroupId int, 
	@CurData int, 
	@ExData int, 
	@ExStep int, 
	@ReceiveTimes int, 
	@RecordDate datetime, 
	@SendTimes int, 
	@ReturnCode int, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[ActivityEx_PrizeRecord] SET
	[ExKey] = @ExKey
	,[ExRecordId] = @ExRecordId
	,[ManagerId] = @ManagerId
	,[ZoneActivityId] = @ZoneActivityId
	,[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[CurData] = @CurData
	,[ExData] = @ExData
	,[ExStep] = @ExStep
	,[ReceiveTimes] = @ReceiveTimes
	,[RecordDate] = @RecordDate
	,[SendTimes] = @SendTimes
	,[ReturnCode] = @ReturnCode
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



