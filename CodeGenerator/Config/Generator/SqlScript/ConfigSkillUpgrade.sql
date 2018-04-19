
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configskillupgrade_Delete    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillupgrade_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillupgrade_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigSkillupgrade_GetById    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillupgrade_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillupgrade_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigSkillupgrade_GetAll    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillupgrade_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillupgrade_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigSkillupgrade_Insert    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillupgrade_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillupgrade_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigSkillupgrade_Update    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillupgrade_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillupgrade_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigSkillupgrade_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_SkillUpgrade]
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

CREATE PROCEDURE [dbo].P_ConfigSkillupgrade_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SkillUpgrade] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigSkillupgrade_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SkillUpgrade] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigSkillupgrade_Insert
	@Idx int
	,@SkillLevel int
	,@Quality int
	,@Coin int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_SkillUpgrade] (
	[Idx]
	,[SkillLevel]
	,[Quality]
	,[Coin]
) VALUES (
    @Idx
    ,@SkillLevel
    ,@Quality
    ,@Coin
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

CREATE PROCEDURE [dbo].P_ConfigSkillupgrade_Update
	@Idx int, 
	@SkillLevel int, -- 技能等级
	@Quality int, -- 品质
	@Coin int -- 消耗金币
AS



UPDATE [dbo].[Config_SkillUpgrade] SET
	[SkillLevel] = @SkillLevel
	,[Quality] = @Quality
	,[Coin] = @Coin
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



