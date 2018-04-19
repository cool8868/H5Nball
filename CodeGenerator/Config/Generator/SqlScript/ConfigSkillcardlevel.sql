
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configskillcardlevel_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardlevel_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardlevel_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigSkillcardlevel_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardlevel_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardlevel_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigSkillcardlevel_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardlevel_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardlevel_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigSkillcardlevel_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardlevel_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardlevel_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigSkillcardlevel_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardlevel_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardlevel_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigSkillcardlevel_Delete
	@RowId int
AS

DELETE FROM [dbo].[Config_SkillCardLevel]
WHERE
	[RowId] = @RowId

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

CREATE PROCEDURE [dbo].P_ConfigSkillcardlevel_GetById
	@RowId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SkillCardLevel] with(nolock)
WHERE
	[RowId] = @RowId
	
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

CREATE PROCEDURE [dbo].P_ConfigSkillcardlevel_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SkillCardLevel] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigSkillcardlevel_Insert
	@SkillClass int , 
	@SkillLevel int , 
	@MinExp int , 
	@MaxExp int , 
	@RowTime date , 
    @RowId int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_SkillCardLevel] (
	[SkillClass]
	,[SkillLevel]
	,[MinExp]
	,[MaxExp]
	,[RowTime]
) VALUES (
    @SkillClass
    ,@SkillLevel
    ,@MinExp
    ,@MaxExp
    ,@RowTime
)


SET @RowId = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_ConfigSkillcardlevel_Update
	@RowId int, 
	@SkillClass int, 
	@SkillLevel int, 
	@MinExp int, 
	@MaxExp int, -- 经验值
	@RowTime date 
AS



UPDATE [dbo].[Config_SkillCardLevel] SET
	[SkillClass] = @SkillClass
	,[SkillLevel] = @SkillLevel
	,[MinExp] = @MinExp
	,[MaxExp] = @MaxExp
	,[RowTime] = @RowTime
WHERE
	[RowId] = @RowId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



