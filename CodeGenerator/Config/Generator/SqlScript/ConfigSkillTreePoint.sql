
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configskilltreepoint_Delete    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkilltreepoint_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkilltreepoint_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigSkilltreepoint_GetById    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkilltreepoint_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkilltreepoint_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigSkilltreepoint_GetAll    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkilltreepoint_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkilltreepoint_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigSkilltreepoint_Insert    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkilltreepoint_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkilltreepoint_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigSkilltreepoint_Update    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkilltreepoint_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkilltreepoint_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigSkilltreepoint_Delete
	@ManagerLevel int
AS

DELETE FROM [dbo].[Config_SkillTreePoint]
WHERE
	[ManagerLevel] = @ManagerLevel

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

CREATE PROCEDURE [dbo].P_ConfigSkilltreepoint_GetById
	@ManagerLevel int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SkillTreePoint] with(nolock)
WHERE
	[ManagerLevel] = @ManagerLevel
	
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

CREATE PROCEDURE [dbo].P_ConfigSkilltreepoint_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SkillTreePoint] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigSkilltreepoint_Insert
	@ManagerLevel int
	,@SumSkillPoint int
	,@AddSkillPoint int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_SkillTreePoint] (
	[ManagerLevel]
	,[SumSkillPoint]
	,[AddSkillPoint]
) VALUES (
    @ManagerLevel
    ,@SumSkillPoint
    ,@AddSkillPoint
)

select @ManagerLevel

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

CREATE PROCEDURE [dbo].P_ConfigSkilltreepoint_Update
	@ManagerLevel int, 
	@SumSkillPoint int, -- 总天赋点数
	@AddSkillPoint int -- 升级获得的点数
AS



UPDATE [dbo].[Config_SkillTreePoint] SET
	[SumSkillPoint] = @SumSkillPoint
	,[AddSkillPoint] = @AddSkillPoint
WHERE
	[ManagerLevel] = @ManagerLevel

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



