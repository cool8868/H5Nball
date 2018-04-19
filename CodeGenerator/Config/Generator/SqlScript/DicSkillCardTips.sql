
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicskillcardtips_Delete    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcardtips_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcardtips_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicSkillcardtips_GetById    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcardtips_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcardtips_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicSkillcardtips_GetAll    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcardtips_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcardtips_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicSkillcardtips_Insert    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcardtips_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcardtips_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicSkillcardtips_Update    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillcardtips_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillcardtips_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicSkillcardtips_Delete
	@SkillCode varchar(20)
AS

DELETE FROM [dbo].[Dic_SkillCardTips]
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

CREATE PROCEDURE [dbo].P_DicSkillcardtips_GetById
	@SkillCode varchar(20)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillCardTips] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicSkillcardtips_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillCardTips] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicSkillcardtips_Insert
	@SkillId int
	,@SkillCode varchar(20)
	,@SkillName nvarchar(80)
	,@ActType int
	,@ActTypeMemo nvarchar(80)
	,@SkillClass int
	,@SkillClassMemo nvarchar(80)
	,@SkillRoot varchar(20)
	,@SkillLevel int
	,@GetLevel int
	,@MaxExp int
	,@MixDiscount decimal(6, 4)
	,@TriggerAction nvarchar(80)
	,@TriggerRate nvarchar(80)
	,@CD nvarchar(80)
	,@LastTime nvarchar(80)
	,@BuffMemo nvarchar(500)
	,@Icon varchar(20)
	,@Memo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_SkillCardTips] (
	[SkillId]
	,[SkillCode]
	,[SkillName]
	,[ActType]
	,[ActTypeMemo]
	,[SkillClass]
	,[SkillClassMemo]
	,[SkillRoot]
	,[SkillLevel]
	,[GetLevel]
	,[MaxExp]
	,[MixDiscount]
	,[TriggerAction]
	,[TriggerRate]
	,[CD]
	,[LastTime]
	,[BuffMemo]
	,[Icon]
	,[Memo]
	,[RowTime]
) VALUES (
    @SkillId
    ,@SkillCode
    ,@SkillName
    ,@ActType
    ,@ActTypeMemo
    ,@SkillClass
    ,@SkillClassMemo
    ,@SkillRoot
    ,@SkillLevel
    ,@GetLevel
    ,@MaxExp
    ,@MixDiscount
    ,@TriggerAction
    ,@TriggerRate
    ,@CD
    ,@LastTime
    ,@BuffMemo
    ,@Icon
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

CREATE PROCEDURE [dbo].P_DicSkillcardtips_Update
	@SkillId int, -- SkillId
	@SkillCode varchar(20), -- SkillCode
	@SkillName nvarchar(80), -- SkillName
	@ActType int, -- 动作类型
	@ActTypeMemo nvarchar(80), -- 动作类型描述
	@SkillClass int, -- 品质
	@SkillClassMemo nvarchar(80), -- 品质描述
	@SkillRoot varchar(20), -- 同名技能，用来判断合并时是否折扣经验
	@SkillLevel int, -- 等级
	@GetLevel int, -- 学习所需经理等级
	@MaxExp int, -- 经验上限
	@MixDiscount decimal(6, 4), -- 合并非同名的经验折扣
	@TriggerAction nvarchar(80), -- 触发动作
	@TriggerRate nvarchar(80), -- 触发率
	@CD nvarchar(80), -- CD
	@LastTime nvarchar(80), -- 持续时间
	@BuffMemo nvarchar(500), -- 效果描述
	@Icon varchar(20), -- Icon
	@Memo nvarchar(500), -- Memo
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_SkillCardTips] SET
	[SkillId] = @SkillId
	,[SkillName] = @SkillName
	,[ActType] = @ActType
	,[ActTypeMemo] = @ActTypeMemo
	,[SkillClass] = @SkillClass
	,[SkillClassMemo] = @SkillClassMemo
	,[SkillRoot] = @SkillRoot
	,[SkillLevel] = @SkillLevel
	,[GetLevel] = @GetLevel
	,[MaxExp] = @MaxExp
	,[MixDiscount] = @MixDiscount
	,[TriggerAction] = @TriggerAction
	,[TriggerRate] = @TriggerRate
	,[CD] = @CD
	,[LastTime] = @LastTime
	,[BuffMemo] = @BuffMemo
	,[Icon] = @Icon
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



