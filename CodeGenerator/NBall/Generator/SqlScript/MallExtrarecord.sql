
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Mallextrarecord_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallExtrarecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallExtrarecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].MallExtrarecord_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallExtrarecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallExtrarecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].MallExtrarecord_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallExtrarecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallExtrarecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].MallExtrarecord_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallExtrarecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallExtrarecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].MallExtrarecord_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_MallExtrarecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_MallExtrarecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_MallExtrarecord_Delete
	@Idx int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Mall_ExtraRecord]
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

CREATE PROCEDURE [dbo].P_MallExtrarecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Mall_ExtraRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_MallExtrarecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Mall_ExtraRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_MallExtrarecord_Insert
	@ManagerId uniqueidentifier , 
	@ExtraType int , 
	@UsedCount int , 
	@RecordDate datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Mall_ExtraRecord] (
	[ManagerId]
	,[ExtraType]
	,[UsedCount]
	,[RecordDate]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ExtraType
    ,@UsedCount
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

CREATE PROCEDURE [dbo].P_MallExtrarecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ExtraType int, -- extra类型
	@UsedCount int, -- 使用次数/当天使用次数
	@RecordDate datetime, -- 记录日期
	@RowTime datetime, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[Mall_ExtraRecord] SET
	[ManagerId] = @ManagerId
	,[ExtraType] = @ExtraType
	,[UsedCount] = @UsedCount
	,[RecordDate] = @RecordDate
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



