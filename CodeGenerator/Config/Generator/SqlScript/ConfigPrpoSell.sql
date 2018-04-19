
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configprposell_Delete    Script Date: 2016年5月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPrposell_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPrposell_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigPrposell_GetById    Script Date: 2016年5月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPrposell_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPrposell_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigPrposell_GetAll    Script Date: 2016年5月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPrposell_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPrposell_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigPrposell_Insert    Script Date: 2016年5月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPrposell_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPrposell_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigPrposell_Update    Script Date: 2016年5月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPrposell_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPrposell_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigPrposell_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_PrpoSell]
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

CREATE PROCEDURE [dbo].P_ConfigPrposell_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PrpoSell] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigPrposell_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PrpoSell] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigPrposell_Insert
	@Idx int
	,@ItemType int
	,@Quality int
	,@Coin int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_PrpoSell] (
	[Idx]
	,[ItemType]
	,[Quality]
	,[Coin]
) VALUES (
    @Idx
    ,@ItemType
    ,@Quality
    ,@Coin
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

CREATE PROCEDURE [dbo].P_ConfigPrposell_Update
	@Idx int, 
	@ItemType int, -- 道具类型 1球员  2装备 3商城物品
	@Quality int, -- 品质
	@Coin int -- 获得的金币
AS



UPDATE [dbo].[Config_PrpoSell] SET
	[ItemType] = @ItemType
	,[Quality] = @Quality
	,[Coin] = @Coin
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



