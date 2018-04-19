
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcoachupgrade_Delete    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachupgrade_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachupgrade_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCoachupgrade_GetById    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachupgrade_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachupgrade_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCoachupgrade_GetAll    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachupgrade_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachupgrade_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCoachupgrade_Insert    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachupgrade_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachupgrade_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCoachupgrade_Update    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachupgrade_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachupgrade_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCoachupgrade_Delete
	@Level int
AS

DELETE FROM [dbo].[Config_CoachUpgrade]
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

CREATE PROCEDURE [dbo].P_ConfigCoachupgrade_GetById
	@Level int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CoachUpgrade] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigCoachupgrade_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CoachUpgrade] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCoachupgrade_Insert
	@Level int
	,@UpgradeExp int
	,@UpgradeSumExp int
	,@UpgradeSkillCoin int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_CoachUpgrade] (
	[Level]
	,[UpgradeExp]
	,[UpgradeSumExp]
	,[UpgradeSkillCoin]
) VALUES (
    @Level
    ,@UpgradeExp
    ,@UpgradeSumExp
    ,@UpgradeSkillCoin
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

CREATE PROCEDURE [dbo].P_ConfigCoachupgrade_Update
	@Level int, 
	@UpgradeExp int, 
	@UpgradeSumExp int, 
	@UpgradeSkillCoin int 
AS



UPDATE [dbo].[Config_CoachUpgrade] SET
	[UpgradeExp] = @UpgradeExp
	,[UpgradeSumExp] = @UpgradeSumExp
	,[UpgradeSkillCoin] = @UpgradeSkillCoin
WHERE
	[Level] = @Level

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


