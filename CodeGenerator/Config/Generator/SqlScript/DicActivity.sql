
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicactivity_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivity_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivity_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicActivity_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivity_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivity_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicActivity_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivity_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivity_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicActivity_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivity_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivity_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicActivity_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicActivity_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicActivity_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicActivity_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_Activity]
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

CREATE PROCEDURE [dbo].P_DicActivity_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Activity] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicActivity_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Activity] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicActivity_Insert
	@Idx int
	,@ActivityType int
	,@Name nvarchar(50)
	,@Startdate datetime
	,@EndDate datetime
	,@IsValid bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Activity] (
	[Idx]
	,[ActivityType]
	,[Name]
	,[Startdate]
	,[EndDate]
	,[IsValid]
) VALUES (
    @Idx
    ,@ActivityType
    ,@Name
    ,@Startdate
    ,@EndDate
    ,@IsValid
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

CREATE PROCEDURE [dbo].P_DicActivity_Update
	@Idx int, 
	@ActivityType int, 
	@Name nvarchar(50), -- 活动名称
	@Startdate datetime, 
	@EndDate datetime, 
	@IsValid bit 
AS



UPDATE [dbo].[Dic_Activity] SET
	[ActivityType] = @ActivityType
	,[Name] = @Name
	,[Startdate] = @Startdate
	,[EndDate] = @EndDate
	,[IsValid] = @IsValid
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



