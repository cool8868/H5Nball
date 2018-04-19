
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicladderprize_Delete    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicLadderprize_GetById    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicLadderprize_GetAll    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicLadderprize_Insert    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicLadderprize_Update    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicLadderprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicLadderprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicLadderprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_LadderPrize]
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

CREATE PROCEDURE [dbo].P_DicLadderprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_LadderPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicLadderprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_LadderPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicLadderprize_Insert
	@Idx int
	,@MinRank int
	,@MaxRank int
	,@SubType int
	,@ItemCode int
	,@Title nvarchar(50)
	,@description nvarchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_LadderPrize] (
	[Idx]
	,[MinRank]
	,[MaxRank]
	,[SubType]
	,[ItemCode]
	,[Title]
	,[description]
) VALUES (
    @Idx
    ,@MinRank
    ,@MaxRank
    ,@SubType
    ,@ItemCode
    ,@Title
    ,@description
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

CREATE PROCEDURE [dbo].P_DicLadderprize_Update
	@Idx int, 
	@MinRank int, 
	@MaxRank int, 
	@SubType int, 
	@ItemCode int, 
	@Title nvarchar(50), 
	@description nvarchar(100) 
AS



UPDATE [dbo].[Dic_LadderPrize] SET
	[MinRank] = @MinRank
	,[MaxRank] = @MaxRank
	,[SubType] = @SubType
	,[ItemCode] = @ItemCode
	,[Title] = @Title
	,[description] = @description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



