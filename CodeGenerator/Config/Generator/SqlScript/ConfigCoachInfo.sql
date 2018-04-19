
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcoachinfo_Delete    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachinfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachinfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCoachinfo_GetById    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachinfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachinfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCoachinfo_GetAll    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachinfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachinfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCoachinfo_Insert    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachinfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachinfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCoachinfo_Update    Script Date: 2017年2月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCoachinfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCoachinfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCoachinfo_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_CoachInfo]
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

CREATE PROCEDURE [dbo].P_ConfigCoachinfo_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CoachInfo] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigCoachinfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CoachInfo] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCoachinfo_Insert
	@Idx int
	,@Name nvarchar(20)
	,@Offensive int
	,@Organizational int
	,@Defense int
	,@BodyAttr int
	,@Goalkeeping int
	,@IsSkill bit
	,@SkillId int
	,@DebrisCode int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_CoachInfo] (
	[Idx]
	,[Name]
	,[Offensive]
	,[Organizational]
	,[Defense]
	,[BodyAttr]
	,[Goalkeeping]
	,[IsSkill]
	,[SkillId]
	,[DebrisCode]
) VALUES (
    @Idx
    ,@Name
    ,@Offensive
    ,@Organizational
    ,@Defense
    ,@BodyAttr
    ,@Goalkeeping
    ,@IsSkill
    ,@SkillId
    ,@DebrisCode
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

CREATE PROCEDURE [dbo].P_ConfigCoachinfo_Update
	@Idx int, 
	@Name nvarchar(20), 
	@Offensive int, 
	@Organizational int, 
	@Defense int, 
	@BodyAttr int, 
	@Goalkeeping int, 
	@IsSkill bit, 
	@SkillId int, 
	@DebrisCode int 
AS



UPDATE [dbo].[Config_CoachInfo] SET
	[Name] = @Name
	,[Offensive] = @Offensive
	,[Organizational] = @Organizational
	,[Defense] = @Defense
	,[BodyAttr] = @BodyAttr
	,[Goalkeeping] = @Goalkeeping
	,[IsSkill] = @IsSkill
	,[SkillId] = @SkillId
	,[DebrisCode] = @DebrisCode
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


