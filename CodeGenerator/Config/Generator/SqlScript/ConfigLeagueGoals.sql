
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configleaguegoals_Delete    Script Date: 2016年6月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguegoals_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguegoals_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguegoals_GetById    Script Date: 2016年6月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguegoals_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguegoals_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLeaguegoals_GetAll    Script Date: 2016年6月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguegoals_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguegoals_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLeaguegoals_Insert    Script Date: 2016年6月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguegoals_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguegoals_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguegoals_Update    Script Date: 2016年6月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguegoals_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguegoals_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLeaguegoals_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_LeagueGoals]
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

CREATE PROCEDURE [dbo].P_ConfigLeaguegoals_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueGoals] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigLeaguegoals_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueGoals] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLeaguegoals_Insert
	@Idx int
	,@TemplateId int
	,@NpcId int
	,@MinGoals int
	,@MaxGoals int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_LeagueGoals] (
	[Idx]
	,[TemplateId]
	,[NpcId]
	,[MinGoals]
	,[MaxGoals]
) VALUES (
    @Idx
    ,@TemplateId
    ,@NpcId
    ,@MinGoals
    ,@MaxGoals
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

CREATE PROCEDURE [dbo].P_ConfigLeaguegoals_Update
	@Idx int, 
	@TemplateId int, 
	@NpcId int, 
	@MinGoals int, 
	@MaxGoals int 
AS



UPDATE [dbo].[Config_LeagueGoals] SET
	[TemplateId] = @TemplateId
	,[NpcId] = @NpcId
	,[MinGoals] = @MinGoals
	,[MaxGoals] = @MaxGoals
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



