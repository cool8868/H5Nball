
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Diccrossladderprize_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicCrossladderprize_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicCrossladderprize_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicCrossladderprize_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicCrossladderprize_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCrossladderprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCrossladderprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicCrossladderprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_CrossLadderPrize]
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

CREATE PROCEDURE [dbo].P_DicCrossladderprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_CrossLadderPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicCrossladderprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_CrossLadderPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicCrossladderprize_Insert
	@Idx int
	,@MinRank int
	,@MaxRank int
	,@SubType int
	,@PrizeType int
	,@Title nvarchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_CrossLadderPrize] (
	[Idx]
	,[MinRank]
	,[MaxRank]
	,[SubType]
	,[PrizeType]
	,[Title]
) VALUES (
    @Idx
    ,@MinRank
    ,@MaxRank
    ,@SubType
    ,@PrizeType
    ,@Title
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

CREATE PROCEDURE [dbo].P_DicCrossladderprize_Update
	@Idx int, 
	@MinRank int, 
	@MaxRank int, 
	@SubType int, 
	@PrizeType int, 
	@Title nvarchar(50) 
AS



UPDATE [dbo].[Dic_CrossLadderPrize] SET
	[MinRank] = @MinRank
	,[MaxRank] = @MaxRank
	,[SubType] = @SubType
	,[PrizeType] = @PrizeType
	,[Title] = @Title
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


