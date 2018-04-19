
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configleaguefightmap_Delete    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguefightmap_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguefightmap_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguefightmap_GetById    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguefightmap_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguefightmap_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLeaguefightmap_GetAll    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguefightmap_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguefightmap_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLeaguefightmap_Insert    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguefightmap_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguefightmap_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguefightmap_Update    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguefightmap_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguefightmap_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLeaguefightmap_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_LeagueFightMap]
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

CREATE PROCEDURE [dbo].P_ConfigLeaguefightmap_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueFightMap] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigLeaguefightmap_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueFightMap] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLeaguefightmap_Insert
	@TeamCount int , 
	@TemplateId int , 
	@RoundIndex int , 
	@GroupIndex int , 
	@Team1 int , 
	@Team2 int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_LeagueFightMap] (
	[TeamCount]
	,[TemplateId]
	,[RoundIndex]
	,[GroupIndex]
	,[Team1]
	,[Team2]
) VALUES (
    @TeamCount
    ,@TemplateId
    ,@RoundIndex
    ,@GroupIndex
    ,@Team1
    ,@Team2
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

CREATE PROCEDURE [dbo].P_ConfigLeaguefightmap_Update
	@Idx int, 
	@TeamCount int, 
	@TemplateId int, 
	@RoundIndex int, 
	@GroupIndex int, 
	@Team1 int, 
	@Team2 int 
AS



UPDATE [dbo].[Config_LeagueFightMap] SET
	[TeamCount] = @TeamCount
	,[TemplateId] = @TemplateId
	,[RoundIndex] = @RoundIndex
	,[GroupIndex] = @GroupIndex
	,[Team1] = @Team1
	,[Team2] = @Team2
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



