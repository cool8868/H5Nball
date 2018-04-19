
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configmanagerlevel_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigManagerlevel_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigManagerlevel_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigManagerlevel_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigManagerlevel_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigManagerlevel_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigManagerlevel_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigManagerlevel_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigManagerlevel_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigManagerlevel_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigManagerlevel_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigManagerlevel_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigManagerlevel_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigManagerlevel_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigManagerlevel_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigManagerlevel_Delete
	@Level int
AS

DELETE FROM [dbo].[Config_ManagerLevel]
WHERE
	[Level] = @Level

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

CREATE PROCEDURE [dbo].P_ConfigManagerlevel_GetById
	@Level int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ManagerLevel] with(nolock)
WHERE
	[Level] = @Level
	
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

CREATE PROCEDURE [dbo].P_ConfigManagerlevel_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ManagerLevel] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigManagerlevel_Insert
	@Level int
	,@Exp int
	,@SkillCount int
	,@MaxStamina int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_ManagerLevel] (
	[Level]
	,[Exp]
	,[SkillCount]
	,[MaxStamina]
) VALUES (
    @Level
    ,@Exp
    ,@SkillCount
    ,@MaxStamina
)

select @Level

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

CREATE PROCEDURE [dbo].P_ConfigManagerlevel_Update
	@Level int, 
	@Exp int, 
	@SkillCount int, 
	@MaxStamina int 
AS



UPDATE [dbo].[Config_ManagerLevel] SET
	[Exp] = @Exp
	,[SkillCount] = @SkillCount
	,[MaxStamina] = @MaxStamina
WHERE
	[Level] = @Level

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



