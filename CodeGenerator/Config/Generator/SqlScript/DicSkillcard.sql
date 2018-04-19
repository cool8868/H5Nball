
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicskillcard_Delete    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcard_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcard_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicSkillcard_GetById    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcard_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcard_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicSkillcard_GetAll    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcard_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcard_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicSkillcard_Insert    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcard_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcard_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicSkillcard_Update    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcard_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcard_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicSkillcard_Delete
	@SkillCode varchar(20)
AS

DELETE FROM [dbo].[Dic_SkillCard]
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

CREATE PROCEDURE [dbo].P_DicSkillcard_GetById
	@SkillCode varchar(20)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillCard] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicSkillcard_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillCard] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicSkillcard_Insert
	@SkillCode varchar(20)
	,@ItemName nvarchar(80)
	,@ItemIcon varchar(20)
	,@SkillClass int
	,@SkillRoot varchar(20)
	,@SkillLevel int
	,@GetLevel int
	,@ActType int
	,@MixExp int
	,@MixDiscount decimal(6, 4)
	,@Memo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_SkillCard] (
	[SkillCode]
	,[ItemName]
	,[ItemIcon]
	,[SkillClass]
	,[SkillRoot]
	,[SkillLevel]
	,[GetLevel]
	,[ActType]
	,[MixExp]
	,[MixDiscount]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillCode
    ,@ItemName
    ,@ItemIcon
    ,@SkillClass
    ,@SkillRoot
    ,@SkillLevel
    ,@GetLevel
    ,@ActType
    ,@MixExp
    ,@MixDiscount
    ,@Memo
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

CREATE PROCEDURE [dbo].P_DicSkillcard_Update
	@SkillCode varchar(20), 
	@ItemName nvarchar(80), -- 名称
	@ItemIcon varchar(20), 
	@SkillClass int, -- 品质
	@SkillRoot varchar(20), 
	@SkillLevel int, -- 等级
	@GetLevel int, -- 学习所需经理等级
	@ActType int, -- 动作
	@MixExp int, -- 经验
	@MixDiscount decimal(6, 4), -- 合并非同名的经验折扣
	@Memo nvarchar(500), -- 描述
	@RowTime datetime 
AS



UPDATE [dbo].[Dic_SkillCard] SET
	[ItemName] = @ItemName
	,[ItemIcon] = @ItemIcon
	,[SkillClass] = @SkillClass
	,[SkillRoot] = @SkillRoot
	,[SkillLevel] = @SkillLevel
	,[GetLevel] = @GetLevel
	,[ActType] = @ActType
	,[MixExp] = @MixExp
	,[MixDiscount] = @MixDiscount
	,[Memo] = @Memo
	,[RowTime] = @RowTime
WHERE
	[SkillCode] = @SkillCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



