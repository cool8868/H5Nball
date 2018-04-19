
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Announcement_Delete    Script Date: 2016年8月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Announcement_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Announcement_Delete]
GO

/****** Object:  Stored Procedure [dbo].Announcement_GetById    Script Date: 2016年8月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Announcement_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Announcement_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].Announcement_GetAll    Script Date: 2016年8月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Announcement_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Announcement_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].Announcement_Insert    Script Date: 2016年8月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Announcement_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Announcement_Insert]
GO

/****** Object:  Stored Procedure [dbo].Announcement_Update    Script Date: 2016年8月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Announcement_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Announcement_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_Announcement_Delete
	@Idx int
AS

DELETE FROM [dbo].[Announcement]
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

CREATE PROCEDURE [dbo].P_Announcement_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Announcement] with(nolock)
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

CREATE PROCEDURE [dbo].P_Announcement_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Announcement] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_Announcement_Insert
	@Platform varchar(50) , 
	@IsTop bit , 
	@IsRnable bit , 
	@Title varchar(200) , 
	@ContentString varchar(4000) , 
	@StartTime datetime , 
	@EndTime datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Announcement] (
	[Platform]
	,[IsTop]
	,[IsRnable]
	,[Title]
	,[ContentString]
	,[StartTime]
	,[EndTime]
	,[RowTime]
) VALUES (
    @Platform
    ,@IsTop
    ,@IsRnable
    ,@Title
    ,@ContentString
    ,@StartTime
    ,@EndTime
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

CREATE PROCEDURE [dbo].P_Announcement_Update
	@Idx int, 
	@Platform varchar(50), 
	@IsTop bit, 
	@IsRnable bit, 
	@Title varchar(200), 
	@ContentString varchar(4000), 
	@StartTime datetime, 
	@EndTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Announcement] SET
	[Platform] = @Platform
	,[IsTop] = @IsTop
	,[IsRnable] = @IsRnable
	,[Title] = @Title
	,[ContentString] = @ContentString
	,[StartTime] = @StartTime
	,[EndTime] = @EndTime
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


