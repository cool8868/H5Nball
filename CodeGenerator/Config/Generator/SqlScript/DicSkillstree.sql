
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicskillstree_Delete    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstree_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstree_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicSkillstree_GetById    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstree_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstree_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicSkillstree_GetAll    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstree_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstree_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicSkillstree_Insert    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstree_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstree_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicSkillstree_Update    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstree_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstree_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicSkillstree_Delete
	@SkillCode varchar(10)
AS

DELETE FROM [dbo].[Dic_SkillsTree]
WHERE
	[SkillCode] = @SkillCode

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

CREATE PROCEDURE [dbo].P_DicSkillstree_GetById
	@SkillCode varchar(10)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillsTree] with(nolock)
WHERE
	[SkillCode] = @SkillCode
	
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

CREATE PROCEDURE [dbo].P_DicSkillstree_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillsTree] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicSkillstree_Insert
	@SkillCode varchar(10)
	,@SkillName varchar(50)
	,@ManagerType int
	,@Series int
	,@RequireManagerLevel int
	,@Description varchar(500)
	,@Condition varchar(100)
	,@ConditionPoint int
	,@MaxPoint int
	,@Opener int
	,@Status int
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_SkillsTree] (
	[SkillCode]
	,[SkillName]
	,[ManagerType]
	,[Series]
	,[RequireManagerLevel]
	,[Description]
	,[Condition]
	,[ConditionPoint]
	,[MaxPoint]
	,[Opener]
	,[Status]
	,[RowTime]
) VALUES (
    @SkillCode
    ,@SkillName
    ,@ManagerType
    ,@Series
    ,@RequireManagerLevel
    ,@Description
    ,@Condition
    ,@ConditionPoint
    ,@MaxPoint
    ,@Opener
    ,@Status
    ,@RowTime
)

select @SkillCode

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

CREATE PROCEDURE [dbo].P_DicSkillstree_Update
	@SkillCode varchar(10), 
	@SkillName varchar(50), 
	@ManagerType int, 
	@Series int, 
	@RequireManagerLevel int, 
	@Description varchar(500), 
	@Condition varchar(100), 
	@ConditionPoint int, 
	@MaxPoint int, 
	@Opener int, 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Dic_SkillsTree] SET
	[SkillName] = @SkillName
	,[ManagerType] = @ManagerType
	,[Series] = @Series
	,[RequireManagerLevel] = @RequireManagerLevel
	,[Description] = @Description
	,[Condition] = @Condition
	,[ConditionPoint] = @ConditionPoint
	,[MaxPoint] = @MaxPoint
	,[Opener] = @Opener
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[SkillCode] = @SkillCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



