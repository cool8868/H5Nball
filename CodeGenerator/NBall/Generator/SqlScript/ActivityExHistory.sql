
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityexhistory_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexHistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexHistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityexHistory_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexHistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexHistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityexHistory_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexHistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexHistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityexHistory_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexHistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexHistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityexHistory_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexHistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexHistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityexHistory_Delete
	@Idx int
AS

DELETE FROM [dbo].[ActivityEx_History]
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

CREATE PROCEDURE [dbo].P_ActivityexHistory_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_History] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityexHistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_History] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityexHistory_Insert
	@ManagerId uniqueidentifier , 
	@ZoneActivityId int , 
	@ExcitingId int , 
	@GroupId int , 
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


INSERT INTO [dbo].[ActivityEx_History] (
	[ManagerId]
	,[ZoneActivityId]
	,[ExcitingId]
	,[GroupId]
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

CREATE PROCEDURE [dbo].P_ActivityexHistory_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ZoneActivityId int, 
	@ExcitingId int, 
	@GroupId int, 
	@ExData int, 
	@ExStep int, 
	@ReceiveTimes int, 
	@RecordDate datetime, 
	@Status int, -- 是否可领奖,0：否；1：是；2：今日已领完
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[ActivityEx_History] SET
	[ManagerId] = @ManagerId
	,[ZoneActivityId] = @ZoneActivityId
	,[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[ExData] = @ExData
	,[ExStep] = @ExStep
	,[ReceiveTimes] = @ReceiveTimes
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



