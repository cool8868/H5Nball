
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configformationlevel_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFormationlevel_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFormationlevel_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigFormationlevel_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFormationlevel_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFormationlevel_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigFormationlevel_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFormationlevel_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFormationlevel_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigFormationlevel_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFormationlevel_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFormationlevel_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigFormationlevel_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFormationlevel_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFormationlevel_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigFormationlevel_Delete
	@Level int
AS

DELETE FROM [dbo].[Config_FormationLevel]
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

CREATE PROCEDURE [dbo].P_ConfigFormationlevel_GetById
	@Level int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_FormationLevel] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigFormationlevel_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_FormationLevel] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigFormationlevel_Insert
	@Level int
	,@Sophisticate int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_FormationLevel] (
	[Level]
	,[Sophisticate]
) VALUES (
    @Level
    ,@Sophisticate
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

CREATE PROCEDURE [dbo].P_ConfigFormationlevel_Update
	@Level int, -- 等级
	@Sophisticate int -- 需要的阅历
AS



UPDATE [dbo].[Config_FormationLevel] SET
	[Sophisticate] = @Sophisticate
WHERE
	[Level] = @Level

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



