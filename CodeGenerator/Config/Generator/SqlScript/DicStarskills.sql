
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicstarskills_Delete    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskills_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskills_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicStarskills_GetById    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskills_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskills_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicStarskills_GetAll    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskills_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskills_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicStarskills_Insert    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskills_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskills_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicStarskills_Update    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskills_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskills_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicStarskills_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_StarSkills]
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

CREATE PROCEDURE [dbo].P_DicStarskills_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_StarSkills] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicStarskills_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_StarSkills] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicStarskills_Insert
	@Idx int
	,@PlayerId int
	,@SkillCode varchar(10)
	,@ActType int
	,@Name nvarchar(50)
	,@Strength int
	,@PlusCode varchar(10)
	,@IsValid bit
	,@Description nvarchar(200)
	,@PlusDescription nvarchar(200)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_StarSkills] (
	[Idx]
	,[PlayerId]
	,[SkillCode]
	,[ActType]
	,[Name]
	,[Strength]
	,[PlusCode]
	,[IsValid]
	,[Description]
	,[PlusDescription]
) VALUES (
    @Idx
    ,@PlayerId
    ,@SkillCode
    ,@ActType
    ,@Name
    ,@Strength
    ,@PlusCode
    ,@IsValid
    ,@Description
    ,@PlusDescription
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

CREATE PROCEDURE [dbo].P_DicStarskills_Update
	@Idx int, 
	@PlayerId int, -- 球员id
	@SkillCode varchar(10), -- 技能编号
	@ActType int, -- 动作类型
	@Name nvarchar(50), -- 名称
	@Strength int, -- 需要强化等级
	@PlusCode varchar(10), -- 附加技能编号
	@IsValid bit, -- 是否有效
	@Description nvarchar(200), -- 描述
	@PlusDescription nvarchar(200) -- 附加技能描述
AS



UPDATE [dbo].[Dic_StarSkills] SET
	[PlayerId] = @PlayerId
	,[SkillCode] = @SkillCode
	,[ActType] = @ActType
	,[Name] = @Name
	,[Strength] = @Strength
	,[PlusCode] = @PlusCode
	,[IsValid] = @IsValid
	,[Description] = @Description
	,[PlusDescription] = @PlusDescription
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



