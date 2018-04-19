
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicactivityprize_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivityprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivityprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicActivityprize_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivityprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivityprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicActivityprize_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivityprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivityprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicActivityprize_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivityprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivityprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicActivityprize_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivityprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivityprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicActivityprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_ActivityPrize]
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

CREATE PROCEDURE [dbo].P_DicActivityprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ActivityPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicActivityprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ActivityPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicActivityprize_Insert
	@Idx int
	,@ActivityId int
	,@ActivityStep int
	,@PrizeType int
	,@SubType int
	,@Count int
	,@Strength int
	,@IsBinding bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_ActivityPrize] (
	[Idx]
	,[ActivityId]
	,[ActivityStep]
	,[PrizeType]
	,[SubType]
	,[Count]
	,[Strength]
	,[IsBinding]
) VALUES (
    @Idx
    ,@ActivityId
    ,@ActivityStep
    ,@PrizeType
    ,@SubType
    ,@Count
    ,@Strength
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

CREATE PROCEDURE [dbo].P_DicActivityprize_Update
	@Idx int, 
	@ActivityId int, -- 活动id
	@ActivityStep int, -- 活动步骤
	@PrizeType int, -- 奖励类型
	@SubType int, -- 二级类型
	@Count int, -- 数量
	@Strength int, -- 强化级别
	@IsBinding bit -- 是否绑定
AS



UPDATE [dbo].[Dic_ActivityPrize] SET
	[ActivityId] = @ActivityId
	,[ActivityStep] = @ActivityStep
	,[PrizeType] = @PrizeType
	,[SubType] = @SubType
	,[Count] = @Count
	,[Strength] = @Strength
	,[IsBinding] = @IsBinding
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



