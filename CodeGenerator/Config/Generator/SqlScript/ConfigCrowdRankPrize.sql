
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcrowdrankprize_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdrankprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdrankprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrowdrankprize_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdrankprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdrankprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCrowdrankprize_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdrankprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdrankprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCrowdrankprize_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdrankprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdrankprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrowdrankprize_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdrankprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdrankprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCrowdrankprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_CrowdRankPrize]
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

CREATE PROCEDURE [dbo].P_ConfigCrowdrankprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrowdRankPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigCrowdrankprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrowdRankPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCrowdrankprize_Insert
	@Idx int
	,@Category int
	,@CategorySub int
	,@Type int
	,@SubType int
	,@Rate int
	,@Min int
	,@Max int
	,@Strength int
	,@Count int
	,@IsBinding bit
	,@Description nvarchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_CrowdRankPrize] (
	[Idx]
	,[Category]
	,[CategorySub]
	,[Type]
	,[SubType]
	,[Rate]
	,[Min]
	,[Max]
	,[Strength]
	,[Count]
	,[IsBinding]
	,[Description]
) VALUES (
    @Idx
    ,@Category
    ,@CategorySub
    ,@Type
    ,@SubType
    ,@Rate
    ,@Min
    ,@Max
    ,@Strength
    ,@Count
    ,@IsBinding
    ,@Description
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

CREATE PROCEDURE [dbo].P_ConfigCrowdrankprize_Update
	@Idx int, 
	@Category int, 
	@CategorySub int, 
	@Type int, 
	@SubType int, 
	@Rate int, 
	@Min int, 
	@Max int, 
	@Strength int, 
	@Count int, 
	@IsBinding bit, 
	@Description nvarchar(100) 
AS



UPDATE [dbo].[Config_CrowdRankPrize] SET
	[Category] = @Category
	,[CategorySub] = @CategorySub
	,[Type] = @Type
	,[SubType] = @SubType
	,[Rate] = @Rate
	,[Min] = @Min
	,[Max] = @Max
	,[Strength] = @Strength
	,[Count] = @Count
	,[IsBinding] = @IsBinding
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


