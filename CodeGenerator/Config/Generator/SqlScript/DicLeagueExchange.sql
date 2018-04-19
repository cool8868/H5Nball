
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicleagueexchange_Delete    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLeagueexchange_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLeagueexchange_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicLeagueexchange_GetById    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLeagueexchange_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLeagueexchange_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicLeagueexchange_GetAll    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLeagueexchange_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLeagueexchange_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicLeagueexchange_Insert    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLeagueexchange_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLeagueexchange_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicLeagueexchange_Update    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLeagueexchange_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLeagueexchange_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicLeagueexchange_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_LeagueExchange]
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

CREATE PROCEDURE [dbo].P_DicLeagueexchange_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_LeagueExchange] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicLeagueexchange_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_LeagueExchange] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicLeagueexchange_Insert
	@Idx int
	,@ItemType int
	,@ItemCode int
	,@CostScore int
	,@Type int
	,@Count int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_LeagueExchange] (
	[Idx]
	,[ItemType]
	,[ItemCode]
	,[CostScore]
	,[Type]
	,[Count]
) VALUES (
    @Idx
    ,@ItemType
    ,@ItemCode
    ,@CostScore
    ,@Type
    ,@Count
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

CREATE PROCEDURE [dbo].P_DicLeagueexchange_Update
	@Idx int, 
	@ItemType int, -- 物品类型 0指定物品 1 随机物品
	@ItemCode int, 
	@CostScore int, 
	@Type int, 
	@Count int 
AS



UPDATE [dbo].[Dic_LeagueExchange] SET
	[ItemType] = @ItemType
	,[ItemCode] = @ItemCode
	,[CostScore] = @CostScore
	,[Type] = @Type
	,[Count] = @Count
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



