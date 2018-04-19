
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configexchangerize_Delete    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigExchangerize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigExchangerize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigExchangerize_GetById    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigExchangerize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigExchangerize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigExchangerize_GetAll    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigExchangerize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigExchangerize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigExchangerize_Insert    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigExchangerize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigExchangerize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigExchangerize_Update    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigExchangerize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigExchangerize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigExchangerize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Exchangerize]
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

CREATE PROCEDURE [dbo].P_ConfigExchangerize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Exchangerize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigExchangerize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Exchangerize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigExchangerize_Insert
	@Idx int
	,@ExchangeId int
	,@PrizeItemCode int
	,@ItemCount int
	,@TheGoldMedalId int
	,@TheGoldMedalCount int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Exchangerize] (
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

CREATE PROCEDURE [dbo].P_ConfigExchangerize_Update
	@Idx int, -- 0
	@ExchangeId int, -- 兑换ID
	@PrizeItemCode int, -- 要兑换的物品
	@ItemCount int, -- 物品数量
	@TheGoldMedalId int, -- 需要金牌ID
	@TheGoldMedalCount int -- 需要金牌数量
AS



UPDATE [dbo].[Config_Exchangerize] SET
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


