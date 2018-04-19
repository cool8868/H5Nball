
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicstararousalskills_Delete    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStararousalskills_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStararousalskills_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicStararousalskills_GetById    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStararousalskills_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStararousalskills_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicStararousalskills_GetAll    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStararousalskills_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStararousalskills_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicStararousalskills_Insert    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStararousalskills_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStararousalskills_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicStararousalskills_Update    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStararousalskills_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStararousalskills_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicStararousalskills_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_StarArousalSkills]
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

CREATE PROCEDURE [dbo].P_DicStararousalskills_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_StarArousalSkills] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicStararousalskills_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_StarArousalSkills] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicStararousalskills_Insert
	@Idx int
	,@PlayerId int
	,@ArousalLv int
	,@ArousalSkillId int
	,@ArousalSkillCode varchar(20)
	,@RawSkillCode varchar(20)
	,@SkillName nvarchar(50)
	,@Description nvarchar(200)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_StarArousalSkills] (
	[Idx]
	,[PlayerId]
	,[ArousalLv]
	,[ArousalSkillId]
	,[ArousalSkillCode]
	,[RawSkillCode]
	,[SkillName]
	,[Description]
) VALUES (
    @Idx
    ,@PlayerId
    ,@ArousalLv
    ,@ArousalSkillId
    ,@ArousalSkillCode
    ,@RawSkillCode
    ,@SkillName
    ,@Description
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

CREATE PROCEDURE [dbo].P_DicStararousalskills_Update
	@Idx int, 
	@PlayerId int, 
	@ArousalLv int, 
	@ArousalSkillId int, 
	@ArousalSkillCode varchar(20), 
	@RawSkillCode varchar(20), 
	@SkillName nvarchar(50), 
	@Description nvarchar(200) 
AS



UPDATE [dbo].[Dic_StarArousalSkills] SET
	[PlayerId] = @PlayerId
	,[ArousalLv] = @ArousalLv
	,[ArousalSkillId] = @ArousalSkillId
	,[ArousalSkillCode] = @ArousalSkillCode
	,[RawSkillCode] = @RawSkillCode
	,[SkillName] = @SkillName
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



