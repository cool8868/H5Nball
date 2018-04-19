
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcrossactivityprize_Delete    Script Date: 2016年11月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrossactivityprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrossactivityprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrossactivityprize_GetById    Script Date: 2016年11月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrossactivityprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrossactivityprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCrossactivityprize_GetAll    Script Date: 2016年11月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrossactivityprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrossactivityprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCrossactivityprize_Insert    Script Date: 2016年11月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrossactivityprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrossactivityprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrossactivityprize_Update    Script Date: 2016年11月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrossactivityprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrossactivityprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCrossactivityprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_CrossActivityPrize]
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

CREATE PROCEDURE [dbo].P_ConfigCrossactivityprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrossActivityPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigCrossactivityprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrossActivityPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCrossactivityprize_Insert
	@Idx int
	,@ActivityId int
	,@PrizeId int
	,@PrizeType int
	,@PrizeCode int
	,@PrizeCount int
	,@PrizeCount2 int
	,@Rate int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_CrossActivityPrize] (
	[Idx]
	,[ActivityId]
	,[PrizeId]
	,[PrizeType]
	,[PrizeCode]
	,[PrizeCount]
	,[PrizeCount2]
	,[Rate]
) VALUES (
    @Idx
    ,@ActivityId
    ,@PrizeId
    ,@PrizeType
    ,@PrizeCode
    ,@PrizeCount
    ,@PrizeCount2
    ,@Rate
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

CREATE PROCEDURE [dbo].P_ConfigCrossactivityprize_Update
	@Idx int, 
	@ActivityId int, -- 活动ID
	@PrizeId int, -- 奖励ID
	@PrizeType int, -- 奖励物品类型
	@PrizeCode int, -- 奖励物品code
	@PrizeCount int, -- 奖励数量
	@PrizeCount2 int, 
	@Rate int -- 概率
AS



UPDATE [dbo].[Config_CrossActivityPrize] SET
	[ActivityId] = @ActivityId
	,[PrizeId] = @PrizeId
	,[PrizeType] = @PrizeType
	,[PrizeCode] = @PrizeCode
	,[PrizeCount] = @PrizeCount
	,[PrizeCount2] = @PrizeCount2
	,[Rate] = @Rate
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


