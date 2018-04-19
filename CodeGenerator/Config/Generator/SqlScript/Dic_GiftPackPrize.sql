
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicgiftpackprize_Delete    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpackprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpackprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicGiftpackprize_GetById    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpackprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpackprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicGiftpackprize_GetAll    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpackprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpackprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicGiftpackprize_Insert    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpackprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpackprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicGiftpackprize_Update    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpackprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpackprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicGiftpackprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_GiftPackPrize]
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

CREATE PROCEDURE [dbo].P_DicGiftpackprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_GiftPackPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicGiftpackprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_GiftPackPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicGiftpackprize_Insert
	@Idx int
	,@PackId int
	,@PrizeType int
	,@SubType int
	,@ThirdType int
	,@MinPower int
	,@MaxPower int
	,@Count int
	,@Strength1 int
	,@Strength2 int
	,@IsBinding bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_GiftPackPrize] (
	[Idx]
	,[PackId]
	,[PrizeType]
	,[SubType]
	,[ThirdType]
	,[MinPower]
	,[MaxPower]
	,[Count]
	,[Strength1]
	,[Strength2]
	,[IsBinding]
) VALUES (
    @Idx
    ,@PackId
    ,@PrizeType
    ,@SubType
    ,@ThirdType
    ,@MinPower
    ,@MaxPower
    ,@Count
    ,@Strength1
    ,@Strength2
    ,@IsBinding
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

CREATE PROCEDURE [dbo].P_DicGiftpackprize_Update
	@Idx int, 
	@PackId int, 
	@PrizeType int, 
	@SubType int, 
	@ThirdType int, 
	@MinPower int, 
	@MaxPower int, 
	@Count int, 
	@Strength1 int, 
	@Strength2 int, 
	@IsBinding bit 
AS



UPDATE [dbo].[Dic_GiftPackPrize] SET
	[PackId] = @PackId
	,[PrizeType] = @PrizeType
	,[SubType] = @SubType
	,[ThirdType] = @ThirdType
	,[MinPower] = @MinPower
	,[MaxPower] = @MaxPower
	,[Count] = @Count
	,[Strength1] = @Strength1
	,[Strength2] = @Strength2
	,[IsBinding] = @IsBinding
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



