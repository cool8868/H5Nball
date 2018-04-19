
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configskillcardaskrank_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardaskrank_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardaskrank_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigSkillcardaskrank_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardaskrank_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardaskrank_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigSkillcardaskrank_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardaskrank_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardaskrank_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigSkillcardaskrank_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardaskrank_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardaskrank_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigSkillcardaskrank_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSkillcardaskrank_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSkillcardaskrank_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigSkillcardaskrank_Delete
	@NpcId int
AS

DELETE FROM [dbo].[Config_SkillCardAskRank]
WHERE
	[NpcId] = @NpcId

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

CREATE PROCEDURE [dbo].P_ConfigSkillcardaskrank_GetById
	@NpcId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SkillCardAskRank] with(nolock)
WHERE
	[NpcId] = @NpcId
	
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

CREATE PROCEDURE [dbo].P_ConfigSkillcardaskrank_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SkillCardAskRank] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigSkillcardaskrank_Insert
	@NpcId int
	,@NpcName nvarchar(80)
	,@AskRank int
	,@OpenCostGoldItemNo int
	,@OpenCostType int
	,@OpenCostValue int
	,@CostGoldItemNo int
	,@CostType int
	,@CostValue int
	,@SuccRate money
	,@SkillRateMap varchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_SkillCardAskRank] (
	[NpcId]
	,[NpcName]
	,[AskRank]
	,[OpenCostGoldItemNo]
	,[OpenCostType]
	,[OpenCostValue]
	,[CostGoldItemNo]
	,[CostType]
	,[CostValue]
	,[SuccRate]
	,[SkillRateMap]
	,[RowTime]
) VALUES (
    @NpcId
    ,@NpcName
    ,@AskRank
    ,@OpenCostGoldItemNo
    ,@OpenCostType
    ,@OpenCostValue
    ,@CostGoldItemNo
    ,@CostType
    ,@CostValue
    ,@SuccRate
    ,@SkillRateMap
    ,@RowTime
)

select @NpcId

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

CREATE PROCEDURE [dbo].P_ConfigSkillcardaskrank_Update
	@NpcId int, 
	@NpcName nvarchar(80), 
	@AskRank int, -- 等级
	@OpenCostGoldItemNo int, 
	@OpenCostType int, 
	@OpenCostValue int, 
	@CostGoldItemNo int, 
	@CostType int, 
	@CostValue int, 
	@SuccRate money, -- 晋级概率
	@SkillRateMap varchar(500), -- 技能卡概率
	@RowTime datetime 
AS



UPDATE [dbo].[Config_SkillCardAskRank] SET
	[NpcName] = @NpcName
	,[AskRank] = @AskRank
	,[OpenCostGoldItemNo] = @OpenCostGoldItemNo
	,[OpenCostType] = @OpenCostType
	,[OpenCostValue] = @OpenCostValue
	,[CostGoldItemNo] = @CostGoldItemNo
	,[CostType] = @CostType
	,[CostValue] = @CostValue
	,[SuccRate] = @SuccRate
	,[SkillRateMap] = @SkillRateMap
	,[RowTime] = @RowTime
WHERE
	[NpcId] = @NpcId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



