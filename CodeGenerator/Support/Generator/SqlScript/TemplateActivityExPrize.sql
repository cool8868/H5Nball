
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Templateactivityexprize_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].TemplateActivityexprize_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TemplateActivityexprize_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TemplateActivityexprize_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].TemplateActivityexprize_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TemplateActivityexprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Template_ActivityExPrize]
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

CREATE PROCEDURE [dbo].P_TemplateActivityexprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_ActivityExPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_TemplateActivityexprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_ActivityExPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TemplateActivityexprize_Insert
	@Idx int
	,@ExcitingId int
	,@GroupId int
	,@ExStep int
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


INSERT INTO [dbo].[Template_ActivityExPrize] (
	[Idx]
	,[ExcitingId]
	,[GroupId]
	,[ExStep]
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
    ,@ExcitingId
    ,@GroupId
    ,@ExStep
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

CREATE PROCEDURE [dbo].P_TemplateActivityexprize_Update
	@Idx int, 
	@ExcitingId int, 
	@GroupId int, 
	@ExStep int, 
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



UPDATE [dbo].[Template_ActivityExPrize] SET
	[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[ExStep] = @ExStep
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



