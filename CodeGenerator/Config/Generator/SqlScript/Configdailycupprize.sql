
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configdailycupprize_Delete    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailycupprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailycupprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigDailycupprize_GetById    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailycupprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailycupprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigDailycupprize_GetAll    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailycupprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailycupprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigDailycupprize_Insert    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailycupprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailycupprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigDailycupprize_Update    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDailycupprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDailycupprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigDailycupprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_DailycupPrize]
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

CREATE PROCEDURE [dbo].P_ConfigDailycupprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_DailycupPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigDailycupprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_DailycupPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigDailycupprize_Insert
	@Idx int
	,@Rank int
	,@WorldScore int
	,@Sophisticate int
	,@Coin int
	,@PrizeItems varchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_DailycupPrize] (
	[Idx]
	,[Rank]
	,[WorldScore]
	,[Sophisticate]
	,[Coin]
	,[PrizeItems]
) VALUES (
    @Idx
    ,@Rank
    ,@WorldScore
    ,@Sophisticate
    ,@Coin
    ,@PrizeItems
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

CREATE PROCEDURE [dbo].P_ConfigDailycupprize_Update
	@Idx int, 
	@Rank int, -- 杯赛排名，从冠军开始
	@WorldScore int, -- 冠军杯积分
	@Sophisticate int, -- 阅历
	@Coin int, -- 金币
	@PrizeItems varchar(50) 
AS



UPDATE [dbo].[Config_DailycupPrize] SET
	[Rank] = @Rank
	,[WorldScore] = @WorldScore
	,[Sophisticate] = @Sophisticate
	,[Coin] = @Coin
	,[PrizeItems] = @PrizeItems
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



