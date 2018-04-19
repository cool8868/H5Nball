
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Templateactivityexdetail_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexdetail_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexdetail_Delete]
GO

/****** Object:  Stored Procedure [dbo].TemplateActivityexdetail_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexdetail_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexdetail_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TemplateActivityexdetail_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexdetail_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexdetail_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TemplateActivityexdetail_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexdetail_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexdetail_Insert]
GO

/****** Object:  Stored Procedure [dbo].TemplateActivityexdetail_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexdetail_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexdetail_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TemplateActivityexdetail_Delete
	@Idx int
AS

DELETE FROM [dbo].[Template_ActivityExDetail]
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

CREATE PROCEDURE [dbo].P_TemplateActivityexdetail_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_ActivityExDetail] with(nolock)
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

CREATE PROCEDURE [dbo].P_TemplateActivityexdetail_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_ActivityExDetail] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TemplateActivityexdetail_Insert
	@Idx int
	,@ExcitingId int
	,@GroupId int
	,@ExStep int
	,@Count int
	,@Condition int
	,@ConditionSub int
	,@EffectType int
	,@EffectRate int
	,@EffectValue int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Template_ActivityExDetail] (
	[Idx]
	,[ExcitingId]
	,[GroupId]
	,[ExStep]
	,[Count]
	,[Condition]
	,[ConditionSub]
	,[EffectType]
	,[EffectRate]
	,[EffectValue]
) VALUES (
    @Idx
    ,@ExcitingId
    ,@GroupId
    ,@ExStep
    ,@Count
    ,@Condition
    ,@ConditionSub
    ,@EffectType
    ,@EffectRate
    ,@EffectValue
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

CREATE PROCEDURE [dbo].P_TemplateActivityexdetail_Update
	@Idx int, 
	@ExcitingId int, 
	@GroupId int, 
	@ExStep int, 
	@Count int, 
	@Condition int, 
	@ConditionSub int, 
	@EffectType int, 
	@EffectRate int, 
	@EffectValue int 
AS



UPDATE [dbo].[Template_ActivityExDetail] SET
	[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[ExStep] = @ExStep
	,[Count] = @Count
	,[Condition] = @Condition
	,[ConditionSub] = @ConditionSub
	,[EffectType] = @EffectType
	,[EffectRate] = @EffectRate
	,[EffectValue] = @EffectValue
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



