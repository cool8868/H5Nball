
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Europeseason_Delete    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeSeason_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeSeason_Delete]
GO

/****** Object:  Stored Procedure [dbo].EuropeSeason_GetById    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeSeason_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeSeason_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].EuropeSeason_GetAll    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeSeason_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeSeason_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].EuropeSeason_Insert    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeSeason_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeSeason_Insert]
GO

/****** Object:  Stored Procedure [dbo].EuropeSeason_Update    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeSeason_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeSeason_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_EuropeSeason_Delete
	@Idx int
AS

DELETE FROM [dbo].[Europe_Season]
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

CREATE PROCEDURE [dbo].P_EuropeSeason_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_Season] with(nolock)
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

CREATE PROCEDURE [dbo].P_EuropeSeason_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_Season] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_EuropeSeason_Insert
	@StartDate date , 
	@EndDate date , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Europe_Season] (
	[StartDate]
	,[EndDate]
	,[Status]
	,[RowTime]
) VALUES (
    @StartDate
    ,@EndDate
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

CREATE PROCEDURE [dbo].P_EuropeSeason_Update
	@Idx int, 
	@StartDate date, 
	@EndDate date, 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Europe_Season] SET
	[StartDate] = @StartDate
	,[EndDate] = @EndDate
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


