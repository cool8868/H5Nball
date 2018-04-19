
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicskill_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkill_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkill_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicSkill_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkill_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkill_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicSkill_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkill_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkill_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicSkill_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkill_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkill_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicSkill_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkill_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkill_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicSkill_Delete
	@SkillCode varchar(20),
	@SkillLevel int
AS

DELETE FROM [dbo].[Dic_Skill]
WHERE
	[SkillCode] = @SkillCode
	AND [SkillLevel] = @SkillLevel

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

CREATE PROCEDURE [dbo].P_DicSkill_GetById
	@SkillCode varchar(20),
	@SkillLevel int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Skill] with(nolock)
WHERE
	[SkillCode] = @SkillCode
	AND [SkillLevel] = @SkillLevel
	
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

CREATE PROCEDURE [dbo].P_DicSkill_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Skill] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicSkill_Insert
	@SkillId int
	,@SkillCode varchar(20)
	,@SkillLevel int
	,@SkillName nvarchar(80)
	,@BuffSrcType int
	,@RefType varchar(10)
	,@RefKey varchar(80)
	,@RefFlag varchar(80)
	,@SkillType int
	,@PoolFlag int
	,@LiveFlag int
	,@Memo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Skill] (
	[SkillId]
	,[SkillCode]
	,[SkillLevel]
	,[SkillName]
	,[BuffSrcType]
	,[RefType]
	,[RefKey]
	,[RefFlag]
	,[SkillType]
	,[PoolFlag]
	,[LiveFlag]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@SkillLevel
    ,@SkillName
    ,@BuffSrcType
    ,@RefType
    ,@RefKey
    ,@RefFlag
    ,@SkillType
    ,@PoolFlag
    ,@LiveFlag
    ,@Memo
    ,@RowTime
)

select @SkillId

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

CREATE PROCEDURE [dbo].P_DicSkill_Update
	@SkillId int, -- SkillId
	@SkillCode varchar(20), -- SkillCode
	@SkillLevel int, -- SkillLevel
	@SkillName nvarchar(80), -- SkillName
	@BuffSrcType int, -- BuffSrcType
	@RefType varchar(10), -- RefType
	@RefKey varchar(80), -- RefKey
	@RefFlag varchar(80), -- RefFlag
	@SkillType int, -- SkillType
	@PoolFlag int, -- PoolFlag
	@LiveFlag int, -- LiveFlag
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_Skill] SET
	[SkillId] = @SkillId
	,[SkillName] = @SkillName
	,[BuffSrcType] = @BuffSrcType
	,[RefType] = @RefType
	,[RefKey] = @RefKey
	,[RefFlag] = @RefFlag
	,[SkillType] = @SkillType
	,[PoolFlag] = @PoolFlag
	,[LiveFlag] = @LiveFlag
	,[Memo] = @Memo
	,[RowTime] = @RowTime
WHERE
	[SkillCode] = @SkillCode
	AND [SkillLevel] = @SkillLevel

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



