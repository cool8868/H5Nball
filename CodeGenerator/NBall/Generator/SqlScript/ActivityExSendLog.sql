
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityexsendlog_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexSendlog_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexSendlog_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityexSendlog_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexSendlog_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexSendlog_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityexSendlog_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexSendlog_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexSendlog_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityexSendlog_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexSendlog_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexSendlog_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityexSendlog_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexSendlog_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexSendlog_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityexSendlog_Delete
	@Idx int
AS

DELETE FROM [dbo].[ActivityEx_SendLog]
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

CREATE PROCEDURE [dbo].P_ActivityexSendlog_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_SendLog] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityexSendlog_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_SendLog] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityexSendlog_Insert
	@ExcitingId int , 
	@GroupId int , 
	@RecordDate datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ActivityEx_SendLog] (
	[ExcitingId]
	,[GroupId]
	,[RecordDate]
	,[RowTime]
) VALUES (
    @ExcitingId
    ,@GroupId
    ,@RecordDate
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

CREATE PROCEDURE [dbo].P_ActivityexSendlog_Update
	@Idx int, 
	@ExcitingId int, 
	@GroupId int, 
	@RecordDate datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[ActivityEx_SendLog] SET
	[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[RecordDate] = @RecordDate
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



