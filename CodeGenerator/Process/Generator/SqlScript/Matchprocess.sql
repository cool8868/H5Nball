
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Matchprocess_Delete    Script Date: 2015年1月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Matchprocess_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Matchprocess_Delete]
GO

/****** Object:  Stored Procedure [dbo].Matchprocess_GetById    Script Date: 2015年1月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Matchprocess_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Matchprocess_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].Matchprocess_GetAll    Script Date: 2015年1月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Matchprocess_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Matchprocess_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].Matchprocess_Insert    Script Date: 2015年1月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Matchprocess_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Matchprocess_Insert]
GO

/****** Object:  Stored Procedure [dbo].Matchprocess_Update    Script Date: 2015年1月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Matchprocess_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Matchprocess_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_Matchprocess_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[MatchProcess]
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

CREATE PROCEDURE [dbo].P_Matchprocess_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[MatchProcess] with(nolock)
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

CREATE PROCEDURE [dbo].P_Matchprocess_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[MatchProcess] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_Matchprocess_Insert
	@Process varbinary(max) , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[MatchProcess] (
	[Idx],
	[Process]
	,[RowTime]
) VALUES (
	@Idx,
    @Process
    ,@RowTime
)




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

CREATE PROCEDURE [dbo].P_Matchprocess_Update
	@Idx uniqueidentifier, 
	@Process varbinary(max), 
	@RowTime datetime 
AS



UPDATE [dbo].[MatchProcess] SET
	[Process] = @Process
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



