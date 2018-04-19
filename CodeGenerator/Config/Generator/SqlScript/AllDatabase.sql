
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Alldatabase_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllDatabase_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllDatabase_Delete]
GO

/****** Object:  Stored Procedure [dbo].AllDatabase_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllDatabase_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllDatabase_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AllDatabase_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllDatabase_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllDatabase_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AllDatabase_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllDatabase_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllDatabase_Insert]
GO

/****** Object:  Stored Procedure [dbo].AllDatabase_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllDatabase_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllDatabase_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AllDatabase_Delete
	@Idx int
AS

DELETE FROM [dbo].[All_Database]
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

CREATE PROCEDURE [dbo].P_AllDatabase_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_Database] with(nolock)
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

CREATE PROCEDURE [dbo].P_AllDatabase_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_Database] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AllDatabase_Insert
	@ZoneName varchar(50) , 
	@DBType varchar(50) , 
	@DBServerName varchar(50) , 
	@DBName varchar(50) , 
	@UserId varchar(50) , 
	@Password varchar(50) , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[All_Database] (
	[ZoneName]
	,[DBType]
	,[DBServerName]
	,[DBName]
	,[UserId]
	,[Password]
) VALUES (
    @ZoneName
    ,@DBType
    ,@DBServerName
    ,@DBName
    ,@UserId
    ,@Password
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

CREATE PROCEDURE [dbo].P_AllDatabase_Update
	@Idx int, 
	@ZoneName varchar(50), 
	@DBType varchar(50), 
	@DBServerName varchar(50), 
	@DBName varchar(50), 
	@UserId varchar(50), 
	@Password varchar(50) 
AS



UPDATE [dbo].[All_Database] SET
	[ZoneName] = @ZoneName
	,[DBType] = @DBType
	,[DBServerName] = @DBServerName
	,[DBName] = @DBName
	,[UserId] = @UserId
	,[Password] = @Password
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



