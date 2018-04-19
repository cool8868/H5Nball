
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcoachstar_Delete    Script Date: 2017年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachstar_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachstar_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCoachstar_GetById    Script Date: 2017年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachstar_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachstar_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCoachstar_GetAll    Script Date: 2017年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachstar_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachstar_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCoachstar_Insert    Script Date: 2017年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachstar_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachstar_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCoachstar_Update    Script Date: 2017年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachstar_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachstar_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCoachstar_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_CoachStar]
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

CREATE PROCEDURE [dbo].P_ConfigCoachstar_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CoachStar] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigCoachstar_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CoachStar] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCoachstar_Insert
	@Idx int
	,@StarLevel int
	,@CoachId int
	,@ConsumeDebris int
	,@MaxLevel int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_CoachStar] (
	[Idx]
	,[StarLevel]
	,[CoachId]
	,[ConsumeDebris]
	,[MaxLevel]
) VALUES (
    @Idx
    ,@StarLevel
    ,@CoachId
    ,@ConsumeDebris
    ,@MaxLevel
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

CREATE PROCEDURE [dbo].P_ConfigCoachstar_Update
	@Idx int, 
	@StarLevel int, 
	@CoachId int, 
	@ConsumeDebris int, 
	@MaxLevel int 
AS



UPDATE [dbo].[Config_CoachStar] SET
	[StarLevel] = @StarLevel
	,[CoachId] = @CoachId
	,[ConsumeDebris] = @ConsumeDebris
	,[MaxLevel] = @MaxLevel
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


