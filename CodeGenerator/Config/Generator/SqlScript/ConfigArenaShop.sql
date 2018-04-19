
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configarenashop_Delete    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenashop_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenashop_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenashop_GetById    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenashop_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenashop_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigArenashop_GetAll    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenashop_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenashop_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigArenashop_Insert    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenashop_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenashop_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenashop_Update    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenashop_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenashop_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigArenashop_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_ArenaShop]
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

CREATE PROCEDURE [dbo].P_ConfigArenashop_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaShop] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigArenashop_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaShop] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigArenashop_Insert
	@Idx int
	,@ItemType int
	,@ItemCode int
	,@ItemCount int
	,@Price int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_ArenaShop] (
	[Idx]
	,[ItemType]
	,[ItemCode]
	,[ItemCount]
	,[Price]
) VALUES (
    @Idx
    ,@ItemType
    ,@ItemCode
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

CREATE PROCEDURE [dbo].P_ConfigArenashop_Update
	@Idx int, 
	@ItemType int, -- 物品类型
	@ItemCode int, 
	@ItemCount int, 
	@Price int -- 价格
AS



UPDATE [dbo].[Config_ArenaShop] SET
	[ItemType] = @ItemType
	,[ItemCode] = @ItemCode
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


