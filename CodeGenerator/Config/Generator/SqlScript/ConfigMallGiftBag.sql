
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configmallgiftbag_Delete    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigMallgiftbag_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigMallgiftbag_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigMallgiftbag_GetById    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigMallgiftbag_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigMallgiftbag_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigMallgiftbag_GetAll    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigMallgiftbag_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigMallgiftbag_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigMallgiftbag_Insert    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigMallgiftbag_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigMallgiftbag_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigMallgiftbag_Update    Script Date: 2016年6月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigMallgiftbag_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigMallgiftbag_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigMallgiftbag_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_MallGiftBag]
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

CREATE PROCEDURE [dbo].P_ConfigMallgiftbag_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_MallGiftBag] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigMallgiftbag_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_MallGiftBag] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigMallgiftbag_Insert
	@Idx int
	,@MallCode int
	,@PrizeType int
	,@SubType int
	,@ItemCount int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_MallGiftBag] (
	[Idx]
	,[MallCode]
	,[PrizeType]
	,[SubType]
	,[ItemCount]
) VALUES (
    @Idx
    ,@MallCode
    ,@PrizeType
    ,@SubType
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

CREATE PROCEDURE [dbo].P_ConfigMallgiftbag_Update
	@Idx int, 
	@MallCode int, 
	@PrizeType int, -- 奖励类型  1钻石 2金币  3指定物品
	@SubType int, -- 指定物品的code
	@ItemCount int 
AS



UPDATE [dbo].[Config_MallGiftBag] SET
	[MallCode] = @MallCode
	,[PrizeType] = @PrizeType
	,[SubType] = @SubType
	,[ItemCount] = @ItemCount
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



