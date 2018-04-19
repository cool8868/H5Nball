
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Templategiants_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateGiants_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateGiants_Delete]
GO

/****** Object:  Stored Procedure [dbo].TemplateGiants_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateGiants_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateGiants_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TemplateGiants_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateGiants_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateGiants_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TemplateGiants_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateGiants_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateGiants_Insert]
GO

/****** Object:  Stored Procedure [dbo].TemplateGiants_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateGiants_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateGiants_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TemplateGiants_Delete
	@MarkId int,
	@Round int
AS

DELETE FROM [dbo].[Template_Giants]
WHERE
	[MarkId] = @MarkId
	AND [Round] = @Round

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

CREATE PROCEDURE [dbo].P_TemplateGiants_GetById
	@MarkId int,
	@Round int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_Giants] with(nolock)
WHERE
	[MarkId] = @MarkId
	AND [Round] = @Round
	
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

CREATE PROCEDURE [dbo].P_TemplateGiants_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_Giants] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TemplateGiants_Insert
	@MarkId int
	,@Round int
	,@SPlay int
	,@Eplay int
	,@Strength int
	,@playLevel int
	,@FormationLevel int
	,@SkillCount int
	,@MinSkillClass int
	,@MaxSkillClass int
	,@SkillLevel int
	,@IsWill bit
	,@EquipCount int
	,@EquipQuality int
	,@SuitType int
	,@TalentLevel int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Template_Giants] (
	[MarkId]
	,[Round]
	,[SPlay]
	,[Eplay]
	,[Strength]
	,[playLevel]
	,[FormationLevel]
	,[SkillCount]
	,[MinSkillClass]
	,[MaxSkillClass]
	,[SkillLevel]
	,[IsWill]
	,[EquipCount]
	,[EquipQuality]
	,[SuitType]
	,[TalentLevel]
) VALUES (
    @MarkId
    ,@Round
    ,@SPlay
    ,@Eplay
    ,@Strength
    ,@playLevel
    ,@FormationLevel
    ,@SkillCount
    ,@MinSkillClass
    ,@MaxSkillClass
    ,@SkillLevel
    ,@IsWill
    ,@EquipCount
    ,@EquipQuality
    ,@SuitType
    ,@TalentLevel
)

select @MarkId

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

CREATE PROCEDURE [dbo].P_TemplateGiants_Update
	@MarkId int, -- 关卡ID
	@Round int, -- 回合数
	@SPlay int, -- 球员能力
	@Eplay int, -- 球员能力
	@Strength int, -- 强化等级
	@playLevel int, -- 球员等级
	@FormationLevel int, -- 阵形等级
	@SkillCount int, -- 技能数量
	@MinSkillClass int, -- 技能等级
	@MaxSkillClass int, -- 技能等级
	@SkillLevel int, -- 技能等级
	@IsWill bit, -- 是否有意志
	@EquipCount int, -- 装备数量
	@EquipQuality int, -- 装备品质
	@SuitType int, -- 套装类型
	@TalentLevel int 
AS



UPDATE [dbo].[Template_Giants] SET
	[SPlay] = @SPlay
	,[Eplay] = @Eplay
	,[Strength] = @Strength
	,[playLevel] = @playLevel
	,[FormationLevel] = @FormationLevel
	,[SkillCount] = @SkillCount
	,[MinSkillClass] = @MinSkillClass
	,[MaxSkillClass] = @MaxSkillClass
	,[SkillLevel] = @SkillLevel
	,[IsWill] = @IsWill
	,[EquipCount] = @EquipCount
	,[EquipQuality] = @EquipQuality
	,[SuitType] = @SuitType
	,[TalentLevel] = @TalentLevel
WHERE
	[MarkId] = @MarkId
	AND [Round] = @Round

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



