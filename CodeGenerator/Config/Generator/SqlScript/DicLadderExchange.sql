﻿
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicladderexchange_Delete    Script Date: 2016年9月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderexchange_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderexchange_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicLadderexchange_GetById    Script Date: 2016年9月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderexchange_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderexchange_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicLadderexchange_GetAll    Script Date: 2016年9月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderexchange_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderexchange_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicLadderexchange_Insert    Script Date: 2016年9月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderexchange_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderexchange_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicLadderexchange_Update    Script Date: 2016年9月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderexchange_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderexchange_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicLadderexchange_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_LadderExchange]
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

CREATE PROCEDURE [dbo].P_DicLadderexchange_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_LadderExchange] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicLadderexchange_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_LadderExchange] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicLadderexchange_Insert
	@Idx int
	,@ItemType int
	,@ItemCode int
	,@CostHonor int
	,@Type int
	,@Count int
	,@LadderCoin int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_LadderExchange] (
	[Idx]
	,[ItemType]
	,[ItemCode]
	,[CostHonor]
	,[Type]
	,[Count]
	,[LadderCoin]
) VALUES (
    @Idx
    ,@ItemType
    ,@ItemCode
    ,@CostHonor
    ,@Type
    ,@Count
    ,@LadderCoin
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

CREATE PROCEDURE [dbo].P_DicLadderexchange_Update
	@Idx int, 
	@ItemType int, -- 物品类型 0指定物品 1 随机物品
	@ItemCode int, 
	@CostHonor int, 
	@Type int, 
	@Count int, 
	@LadderCoin int 
AS



UPDATE [dbo].[Dic_LadderExchange] SET
	[ItemType] = @ItemType
	,[ItemCode] = @ItemCode
	,[CostHonor] = @CostHonor
	,[Type] = @Type
	,[Count] = @Count
	,[LadderCoin] = @LadderCoin
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


