
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicactivitystep_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivitystep_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivitystep_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicActivitystep_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivitystep_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivitystep_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicActivitystep_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivitystep_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivitystep_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicActivitystep_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivitystep_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivitystep_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicActivitystep_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivitystep_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivitystep_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicActivitystep_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_ActivityStep]
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

CREATE PROCEDURE [dbo].P_DicActivitystep_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ActivityStep] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicActivitystep_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ActivityStep] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicActivitystep_Insert
	@Idx int
	,@ActivityId int
	,@ActivityStep int
	,@Condition varchar(100)
	,@Description nvarchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_ActivityStep] (
	[Idx]
	,[ActivityId]
	,[ActivityStep]
	,[Condition]
	,[Description]
) VALUES (
    @Idx
    ,@ActivityId
    ,@ActivityStep
    ,@Condition
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

CREATE PROCEDURE [dbo].P_DicActivitystep_Update
	@Idx int, 
	@ActivityId int, -- 活动id
	@ActivityStep int, -- 活动步骤
	@Condition varchar(100), -- 条件
	@Description nvarchar(100) -- 活动步骤描述
AS



UPDATE [dbo].[Dic_ActivityStep] SET
	[ActivityId] = @ActivityId
	,[ActivityStep] = @ActivityStep
	,[Condition] = @Condition
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



