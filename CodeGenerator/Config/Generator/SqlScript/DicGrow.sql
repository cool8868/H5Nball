
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicgrow_Delete    Script Date: 2015年10月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGrow_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGrow_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicGrow_GetById    Script Date: 2015年10月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGrow_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGrow_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicGrow_GetAll    Script Date: 2015年10月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGrow_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGrow_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicGrow_Insert    Script Date: 2015年10月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGrow_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGrow_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicGrow_Update    Script Date: 2015年10月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGrow_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGrow_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicGrow_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_Grow]
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

CREATE PROCEDURE [dbo].P_DicGrow_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Grow] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicGrow_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Grow] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicGrow_Insert
	@Idx int
	,@Name nvarchar(50)
	,@Stage int
	,@Reiki int
	,@FastReiki int
	,@GrowNum int
	,@BreakRate int
	,@PlusPercent int
	,@PropertyMax int
	,@GrowTip nvarchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Grow] (
	[Idx]
	,[Name]
	,[Stage]
	,[Reiki]
	,[FastReiki]
	,[GrowNum]
	,[BreakRate]
	,[PlusPercent]
	,[PropertyMax]
	,[GrowTip]
) VALUES (
    @Idx
    ,@Name
    ,@Stage
    ,@Reiki
    ,@FastReiki
    ,@GrowNum
    ,@BreakRate
    ,@PlusPercent
    ,@PropertyMax
    ,@GrowTip
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

CREATE PROCEDURE [dbo].P_DicGrow_Update
	@Idx int, -- 阶段等级
	@Name nvarchar(50), -- 阶段名称
	@Stage int, 
	@Reiki int, -- 普通成长一次需要的灵气值
	@FastReiki int, -- 快速成长一次需要的灵气值
	@GrowNum int, -- 成长值(突破到下一阶段的值)
	@BreakRate int, -- 突破到下一阶段的概率(百分比)
	@PlusPercent int, -- 全属性加成(百分比)
	@PropertyMax int, -- 球员属性点分配上限提升值
	@GrowTip nvarchar(100) 
AS



UPDATE [dbo].[Dic_Grow] SET
	[Name] = @Name
	,[Stage] = @Stage
	,[Reiki] = @Reiki
	,[FastReiki] = @FastReiki
	,[GrowNum] = @GrowNum
	,[BreakRate] = @BreakRate
	,[PlusPercent] = @PlusPercent
	,[PropertyMax] = @PropertyMax
	,[GrowTip] = @GrowTip
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



