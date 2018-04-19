
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Templateactivityexgroup_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexgroup_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexgroup_Delete]
GO

/****** Object:  Stored Procedure [dbo].TemplateActivityexgroup_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexgroup_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexgroup_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TemplateActivityexgroup_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexgroup_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexgroup_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TemplateActivityexgroup_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexgroup_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexgroup_Insert]
GO

/****** Object:  Stored Procedure [dbo].TemplateActivityexgroup_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityexgroup_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityexgroup_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TemplateActivityexgroup_Delete
	@Idx int
AS

DELETE FROM [dbo].[Template_ActivityExGroup]
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

CREATE PROCEDURE [dbo].P_TemplateActivityexgroup_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_ActivityExGroup] with(nolock)
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

CREATE PROCEDURE [dbo].P_TemplateActivityexgroup_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_ActivityExGroup] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TemplateActivityexgroup_Insert
	@Idx int
	,@ExcitingId int
	,@GroupId int
	,@ActivityExType int
	,@ExRequireId int
	,@StatisticCycle int
	,@IsRank bit
	,@RankCondition int
	,@RankCount int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Template_ActivityExGroup] (
	[Idx]
	,[ExcitingId]
	,[GroupId]
	,[ActivityExType]
	,[ExRequireId]
	,[StatisticCycle]
	,[IsRank]
	,[RankCondition]
	,[RankCount]
) VALUES (
    @Idx
    ,@ExcitingId
    ,@GroupId
    ,@ActivityExType
    ,@ExRequireId
    ,@StatisticCycle
    ,@IsRank
    ,@RankCondition
    ,@RankCount
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

CREATE PROCEDURE [dbo].P_TemplateActivityexgroup_Update
	@Idx int, 
	@ExcitingId int, 
	@GroupId int, 
	@ActivityExType int, 
	@ExRequireId int, 
	@StatisticCycle int, 
	@IsRank bit, 
	@RankCondition int, 
	@RankCount int 
AS



UPDATE [dbo].[Template_ActivityExGroup] SET
	[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[ActivityExType] = @ActivityExType
	,[ExRequireId] = @ExRequireId
	,[StatisticCycle] = @StatisticCycle
	,[IsRank] = @IsRank
	,[RankCondition] = @RankCondition
	,[RankCount] = @RankCount
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



