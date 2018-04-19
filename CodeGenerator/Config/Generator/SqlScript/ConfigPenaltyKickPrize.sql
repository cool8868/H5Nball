
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configpenaltykickprize_Delete    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPenaltykickprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPenaltykickprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigPenaltykickprize_GetById    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPenaltykickprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPenaltykickprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigPenaltykickprize_GetAll    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPenaltykickprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPenaltykickprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigPenaltykickprize_Insert    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPenaltykickprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPenaltykickprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigPenaltykickprize_Update    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPenaltykickprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPenaltykickprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigPenaltykickprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_PenaltyKickPrize]
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

CREATE PROCEDURE [dbo].P_ConfigPenaltykickprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PenaltyKickPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigPenaltykickprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PenaltyKickPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigPenaltykickprize_Insert
	@Idx int
	,@PrizeType int
	,@PrizeSub int
	,@ItemType int
	,@ItemCode int
	,@ItemCount int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_PenaltyKickPrize] (
	[Idx]
	,[PrizeType]
	,[PrizeSub]
	,[ItemType]
	,[ItemCode]
	,[ItemCount]
) VALUES (
    @Idx
    ,@PrizeType
    ,@PrizeSub
    ,@ItemType
    ,@ItemCode
    ,@ItemCount
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

CREATE PROCEDURE [dbo].P_ConfigPenaltykickprize_Update
	@Idx int, 
	@PrizeType int, -- 奖励类型 1进球奖励 2连续进球奖励 3排名奖励 4兑换的物品
	@PrizeSub int, -- 奖励二级类型 
	@ItemType int, -- 奖励物品类型  1=金币 2=点卷 3=物品 4=卡库
	@ItemCode int, -- 奖励物品code
	@ItemCount int -- 奖励物品数量
AS



UPDATE [dbo].[Config_PenaltyKickPrize] SET
	[PrizeType] = @PrizeType
	,[PrizeSub] = @PrizeSub
	,[ItemType] = @ItemType
	,[ItemCode] = @ItemCode
	,[ItemCount] = @ItemCount
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


