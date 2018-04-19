
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configgambleicon_Delete    Script Date: 2016年6月8日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigGambleicon_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigGambleicon_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigGambleicon_GetById    Script Date: 2016年6月8日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigGambleicon_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigGambleicon_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigGambleicon_GetAll    Script Date: 2016年6月8日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigGambleicon_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigGambleicon_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigGambleicon_Insert    Script Date: 2016年6月8日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigGambleicon_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigGambleicon_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigGambleicon_Update    Script Date: 2016年6月8日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigGambleicon_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigGambleicon_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigGambleicon_Delete
	@Name varchar(50)
AS

DELETE FROM [dbo].[Config_GambleIcon]
WHERE
	[Name] = @Name

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

CREATE PROCEDURE [dbo].P_ConfigGambleicon_GetById
	@Name varchar(50)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_GambleIcon] with(nolock)
WHERE
	[Name] = @Name
	
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

CREATE PROCEDURE [dbo].P_ConfigGambleicon_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_GambleIcon] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigGambleicon_Insert
	@Name varchar(50)
	,@Idx int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_GambleIcon] (
	[Name]
	,[Idx]
) VALUES (
    @Name
    ,@Idx
)

select @Name

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

CREATE PROCEDURE [dbo].P_ConfigGambleicon_Update
	@Name varchar(50), 
	@Idx int 
AS



UPDATE [dbo].[Config_GambleIcon] SET
	[Idx] = @Idx
WHERE
	[Name] = @Name

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


