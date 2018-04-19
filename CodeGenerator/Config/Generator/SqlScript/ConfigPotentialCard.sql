
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configpotentialcard_Delete    Script Date: 2016年7月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotentialcard_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotentialcard_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigPotentialcard_GetById    Script Date: 2016年7月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotentialcard_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotentialcard_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigPotentialcard_GetAll    Script Date: 2016年7月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotentialcard_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotentialcard_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigPotentialcard_Insert    Script Date: 2016年7月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotentialcard_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotentialcard_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigPotentialcard_Update    Script Date: 2016年7月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotentialcard_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotentialcard_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigPotentialcard_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_PotentialCard]
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

CREATE PROCEDURE [dbo].P_ConfigPotentialcard_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PotentialCard] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigPotentialcard_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PotentialCard] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigPotentialcard_Insert
	@Idx int
	,@CardLevel varchar(10)
	,@PotentialLevel int
	,@Rate int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_PotentialCard] (
	[Idx]
	,[CardLevel]
	,[PotentialLevel]
	,[Rate]
) VALUES (
    @Idx
    ,@CardLevel
    ,@PotentialLevel
    ,@Rate
)

select @Idx

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

CREATE PROCEDURE [dbo].P_ConfigPotentialcard_Update
	@Idx int, 
	@CardLevel varchar(10), 
	@PotentialLevel int, 
	@Rate int 
AS



UPDATE [dbo].[Config_PotentialCard] SET
	[CardLevel] = @CardLevel
	,[PotentialLevel] = @PotentialLevel
	,[Rate] = @Rate
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


