
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Alllogfunction_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllLogfunction_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllLogfunction_Delete]
GO

/****** Object:  Stored Procedure [dbo].AllLogfunction_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllLogfunction_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllLogfunction_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AllLogfunction_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllLogfunction_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllLogfunction_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AllLogfunction_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllLogfunction_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllLogfunction_Insert]
GO

/****** Object:  Stored Procedure [dbo].AllLogfunction_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllLogfunction_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllLogfunction_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AllLogfunction_Delete
	@Idx int
AS

DELETE FROM [dbo].[All_LogFunction]
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

CREATE PROCEDURE [dbo].P_AllLogfunction_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_LogFunction] with(nolock)
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

CREATE PROCEDURE [dbo].P_AllLogfunction_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_LogFunction] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AllLogfunction_Insert
	@Name varchar(50) , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[All_LogFunction] (
	[Name]
	,[RowTime]
) VALUES (
    @Name
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

CREATE PROCEDURE [dbo].P_AllLogfunction_Update
	@Idx int, 
	@Name varchar(50), 
	@RowTime datetime 
AS



UPDATE [dbo].[All_LogFunction] SET
	[Name] = @Name
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



