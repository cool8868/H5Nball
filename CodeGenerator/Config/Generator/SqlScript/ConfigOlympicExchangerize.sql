
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configolympicexchangerize_Delete    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicexchangerize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicexchangerize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigOlympicexchangerize_GetById    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicexchangerize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicexchangerize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigOlympicexchangerize_GetAll    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicexchangerize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicexchangerize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigOlympicexchangerize_Insert    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicexchangerize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicexchangerize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigOlympicexchangerize_Update    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicexchangerize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicexchangerize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigOlympicexchangerize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_OlympicExchangerize]
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

CREATE PROCEDURE [dbo].P_ConfigOlympicexchangerize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_OlympicExchangerize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigOlympicexchangerize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_OlympicExchangerize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigOlympicexchangerize_Insert
	@Idx int
	,@ExchangeId int
	,@PrizeItemCode int
	,@ItemCount int
	,@TheGoldMedalId int
	,@TheGoldMedalCount int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_OlympicExchangerize] (
	[Idx]
	,[ExchangeId]
	,[PrizeItemCode]
	,[ItemCount]
	,[TheGoldMedalId]
	,[TheGoldMedalCount]
) VALUES (
    @Idx
    ,@ExchangeId
    ,@PrizeItemCode
    ,@ItemCount
    ,@TheGoldMedalId
    ,@TheGoldMedalCount
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

CREATE PROCEDURE [dbo].P_ConfigOlympicexchangerize_Update
	@Idx int, -- 0
	@ExchangeId int, -- 兑换ID
	@PrizeItemCode int, -- 要兑换的物品
	@ItemCount int, -- 物品数量
	@TheGoldMedalId int, -- 需要金牌ID
	@TheGoldMedalCount int -- 需要金牌数量
AS



UPDATE [dbo].[Config_OlympicExchangerize] SET
	[ExchangeId] = @ExchangeId
	,[PrizeItemCode] = @PrizeItemCode
	,[ItemCount] = @ItemCount
	,[TheGoldMedalId] = @TheGoldMedalId
	,[TheGoldMedalCount] = @TheGoldMedalCount
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


