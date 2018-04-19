
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configrevelationshop_Delete    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationshop_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationshop_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationshop_GetById    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationshop_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationshop_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigRevelationshop_GetAll    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationshop_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationshop_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigRevelationshop_Insert    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationshop_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationshop_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationshop_Update    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationshop_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationshop_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigRevelationshop_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_RevelationShop]
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

CREATE PROCEDURE [dbo].P_ConfigRevelationshop_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationShop] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigRevelationshop_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationShop] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigRevelationshop_Insert
	@Idx int
	,@ItemType int
	,@SubType int
	,@ItemCount int
	,@Price int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_RevelationShop] (
	[Idx]
	,[ItemType]
	,[SubType]
	,[ItemCount]
	,[Price]
) VALUES (
    @Idx
    ,@ItemType
    ,@SubType
    ,@ItemCount
    ,@Price
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

CREATE PROCEDURE [dbo].P_ConfigRevelationshop_Update
	@Idx int, -- 物品ID
	@ItemType int, -- 价格
	@SubType int, -- 经理达到等级
	@ItemCount int, -- 通关关卡要求
	@Price int -- 是否启用
AS



UPDATE [dbo].[Config_RevelationShop] SET
	[ItemType] = @ItemType
	,[SubType] = @SubType
	,[ItemCount] = @ItemCount
	,[Price] = @Price
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


