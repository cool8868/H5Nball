
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configtask_Delete    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTask_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTask_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigTask_GetById    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTask_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTask_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigTask_GetAll    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTask_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTask_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigTask_Insert    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTask_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTask_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigTask_Update    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTask_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTask_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigTask_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Task]
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

CREATE PROCEDURE [dbo].P_ConfigTask_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Task] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigTask_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Task] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigTask_Insert
	@Idx int
	,@Name nvarchar(50)
	,@TaskType int
	,@ManagerLevel int
	,@ParentId int
	,@Times int
	,@PrizeExp int
	,@PrizeCoin int
	,@PrizeItemCode int
	,@OpenFunc int
	,@GuideBuff varchar(10)
	,@UniqueConstraint bit
	,@Description nvarchar(100)
	,@Tip nvarchar(100)
	,@NpcIdx int
	,@RecordPeriod int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Task] (
	[Idx]
	,[Name]
	,[TaskType]
	,[ManagerLevel]
	,[ParentId]
	,[Times]
	,[PrizeExp]
	,[PrizeCoin]
	,[PrizeItemCode]
	,[OpenFunc]
	,[GuideBuff]
	,[UniqueConstraint]
	,[Description]
	,[Tip]
	,[NpcIdx]
	,[RecordPeriod]
) VALUES (
    @Idx
    ,@Name
    ,@TaskType
    ,@ManagerLevel
    ,@ParentId
    ,@Times
    ,@PrizeExp
    ,@PrizeCoin
    ,@PrizeItemCode
    ,@OpenFunc
    ,@GuideBuff
    ,@UniqueConstraint
    ,@Description
    ,@Tip
    ,@NpcIdx
    ,@RecordPeriod
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

CREATE PROCEDURE [dbo].P_ConfigTask_Update
	@Idx int, 
	@Name nvarchar(50), -- 任务名称
	@TaskType int, -- 任务类型：1，主线任务；2，支线任务；3，每日任务
	@ManagerLevel int, -- 所需等级
	@ParentId int, -- 前置id
	@Times int, -- 需完成数量
	@PrizeExp int, -- 奖励经验
	@PrizeCoin int, -- 奖励游戏币
	@PrizeItemCode int, -- 奖励物品
	@OpenFunc int, -- 开放功能id
	@GuideBuff varchar(10), -- 新手buff
	@UniqueConstraint bit, -- 是否唯一，即同一任务下，执行操作的key需唯一
	@Description nvarchar(100), -- 描述
	@Tip nvarchar(100), 
	@NpcIdx int, -- NPC 序号 针对巡回赛任务有效
	@RecordPeriod int -- 记录周期  0永久 1单日 2天梯专属 单赛季 3天梯专属，记录天梯积分
AS



UPDATE [dbo].[Config_Task] SET
	[Name] = @Name
	,[TaskType] = @TaskType
	,[ManagerLevel] = @ManagerLevel
	,[ParentId] = @ParentId
	,[Times] = @Times
	,[PrizeExp] = @PrizeExp
	,[PrizeCoin] = @PrizeCoin
	,[PrizeItemCode] = @PrizeItemCode
	,[OpenFunc] = @OpenFunc
	,[GuideBuff] = @GuideBuff
	,[UniqueConstraint] = @UniqueConstraint
	,[Description] = @Description
	,[Tip] = @Tip
	,[NpcIdx] = @NpcIdx
	,[RecordPeriod] = @RecordPeriod
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



