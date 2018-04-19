
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configeuropeprize_Delete    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEuropeprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEuropeprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigEuropeprize_GetById    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEuropeprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEuropeprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigEuropeprize_GetAll    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEuropeprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEuropeprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigEuropeprize_Insert    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEuropeprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEuropeprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigEuropeprize_Update    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEuropeprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEuropeprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigEuropeprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_EuropePrize]
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

CREATE PROCEDURE [dbo].P_ConfigEuropeprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EuropePrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigEuropeprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EuropePrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigEuropeprize_Insert
	@Idx int
	,@Step int
	,@WinNumber int
	,@PrizeType int
	,@PrizeCode int
	,@PrizeCount int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_EuropePrize] (
	[Idx]
	,[Step]
	,[WinNumber]
	,[PrizeType]
	,[PrizeCode]
	,[PrizeCount]
) VALUES (
    @Idx
    ,@Step
    ,@WinNumber
    ,@PrizeType
    ,@PrizeCode
    ,@PrizeCount
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

CREATE PROCEDURE [dbo].P_ConfigEuropeprize_Update
	@Idx int, 
	@Step int, -- 活动步骤
	@WinNumber int, -- 需要获胜场次
	@PrizeType int, -- 奖励类型 1钻石 2金币 3物品
	@PrizeCode int, -- 奖励物品
	@PrizeCount int -- 物品数量
AS



UPDATE [dbo].[Config_EuropePrize] SET
	[Step] = @Step
	,[WinNumber] = @WinNumber
	,[PrizeType] = @PrizeType
	,[PrizeCode] = @PrizeCode
	,[PrizeCount] = @PrizeCount
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



