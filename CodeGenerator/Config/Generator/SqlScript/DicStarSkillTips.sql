
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicstarskilltips_Delete    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskilltips_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskilltips_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicStarskilltips_GetById    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskilltips_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskilltips_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicStarskilltips_GetAll    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskilltips_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskilltips_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicStarskilltips_Insert    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskilltips_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskilltips_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicStarskilltips_Update    Script Date: 2016年4月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicStarskilltips_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicStarskilltips_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicStarskilltips_Delete
	@SkillId int
AS

DELETE FROM [dbo].[Dic_StarSkillTips]
WHERE
	[SkillId] = @SkillId

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

CREATE PROCEDURE [dbo].P_DicStarskilltips_GetById
	@SkillId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_StarSkillTips] with(nolock)
WHERE
	[SkillId] = @SkillId
	
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

CREATE PROCEDURE [dbo].P_DicStarskilltips_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_StarSkillTips] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicStarskilltips_Insert
	@SkillId int
	,@StarSkillCode varchar(20)
	,@StarSkillName nvarchar(80)
	,@ActType int
	,@ActTypeMemo nvarchar(80)
	,@Pid int
	,@ReqStrength int
	,@TriggerAction nvarchar(80)
	,@TriggerRate nvarchar(80)
	,@CD nvarchar(80)
	,@LastTime nvarchar(80)
	,@BuffMemo nvarchar(500)
	,@Icon varchar(20)
	,@Memo nvarchar(500)
	,@PlusSkillCode varchar(20)
	,@PlusSkillName nvarchar(80)
	,@PlusSkillMemo nvarchar(500)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_StarSkillTips] (
	[SkillId]
	,[StarSkillCode]
	,[StarSkillName]
	,[ActType]
	,[ActTypeMemo]
	,[Pid]
	,[ReqStrength]
	,[TriggerAction]
	,[TriggerRate]
	,[CD]
	,[LastTime]
	,[BuffMemo]
	,[Icon]
	,[Memo]
	,[PlusSkillCode]
	,[PlusSkillName]
	,[PlusSkillMemo]
	,[RowTime]
) VALUES (
    @SkillId
    ,@StarSkillCode
    ,@StarSkillName
    ,@ActType
    ,@ActTypeMemo
    ,@Pid
    ,@ReqStrength
    ,@TriggerAction
    ,@TriggerRate
    ,@CD
    ,@LastTime
    ,@BuffMemo
    ,@Icon
    ,@Memo
    ,@PlusSkillCode
    ,@PlusSkillName
    ,@PlusSkillMemo
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

CREATE PROCEDURE [dbo].P_DicStarskilltips_Update
	@SkillId int, -- SkillId
	@StarSkillCode varchar(20), -- StarSkillCode
	@StarSkillName nvarchar(80), -- StarSkillName
	@ActType int, -- 动作类型
	@ActTypeMemo nvarchar(80), -- 动作类型描述
	@Pid int, -- 球员Id
	@ReqStrength int, -- 需要强化等级
	@TriggerAction nvarchar(80), -- 触发动作
	@TriggerRate nvarchar(80), -- 触发率
	@CD nvarchar(80), -- CD
	@LastTime nvarchar(80), -- 持续时间
	@BuffMemo nvarchar(500), -- 效果描述
	@Icon varchar(20), -- Icon
	@Memo nvarchar(500), -- Memo
	@PlusSkillCode varchar(20), -- 球星封印Code
	@PlusSkillName nvarchar(80), -- 封印名称
	@PlusSkillMemo nvarchar(500), -- 封印效果
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Dic_StarSkillTips] SET
	[StarSkillCode] = @StarSkillCode
	,[StarSkillName] = @StarSkillName
	,[ActType] = @ActType
	,[ActTypeMemo] = @ActTypeMemo
	,[Pid] = @Pid
	,[ReqStrength] = @ReqStrength
	,[TriggerAction] = @TriggerAction
	,[TriggerRate] = @TriggerRate
	,[CD] = @CD
	,[LastTime] = @LastTime
	,[BuffMemo] = @BuffMemo
	,[Icon] = @Icon
	,[Memo] = @Memo
	,[PlusSkillCode] = @PlusSkillCode
	,[PlusSkillName] = @PlusSkillName
	,[PlusSkillMemo] = @PlusSkillMemo
	,[RowTime] = @RowTime
WHERE
	[SkillId] = @SkillId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



