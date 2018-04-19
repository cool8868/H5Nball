
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gambleseason_Delete    Script Date: 2014年7月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleSeason_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleSeason_Delete]
GO

/****** Object:  Stored Procedure [dbo].GambleSeason_GetById    Script Date: 2014年7月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleSeason_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleSeason_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GambleSeason_GetAll    Script Date: 2014年7月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleSeason_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleSeason_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GambleSeason_Insert    Script Date: 2014年7月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleSeason_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleSeason_Insert]
GO

/****** Object:  Stored Procedure [dbo].GambleSeason_Update    Script Date: 2014年7月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleSeason_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleSeason_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GambleSeason_Delete
	@Idx int
AS

DELETE FROM [dbo].[Gamble_Season]
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

CREATE PROCEDURE [dbo].P_GambleSeason_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Season] with(nolock)
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

CREATE PROCEDURE [dbo].P_GambleSeason_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Season] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GambleSeason_Insert
	@StartTime datetime , 
	@EndTime datetime , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Gamble_Season] (
	[StartTime]
	,[EndTime]
	,[Status]
	,[RowTime]
) VALUES (
    @StartTime
    ,@EndTime
    ,@Status
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

CREATE PROCEDURE [dbo].P_GambleSeason_Update
	@Idx int, 
	@StartTime datetime, 
	@EndTime datetime, 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Gamble_Season] SET
	[StartTime] = @StartTime
	,[EndTime] = @EndTime
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



