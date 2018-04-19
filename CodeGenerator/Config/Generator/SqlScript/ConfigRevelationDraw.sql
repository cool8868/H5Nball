
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configrevelationdraw_Delete    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationdraw_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationdraw_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationdraw_GetById    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationdraw_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationdraw_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigRevelationdraw_GetAll    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationdraw_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationdraw_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigRevelationdraw_Insert    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationdraw_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationdraw_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationdraw_Update    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationdraw_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationdraw_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigRevelationdraw_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_RevelationDraw]
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

CREATE PROCEDURE [dbo].P_ConfigRevelationdraw_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationDraw] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigRevelationdraw_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationDraw] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigRevelationdraw_Insert
	@Idx int
	,@PrizeType int
	,@SubType int
	,@ItemCount int
	,@StartRate int
	,@EndRate int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_RevelationDraw] (
	[Idx]
	,[PrizeType]
	,[SubType]
	,[ItemCount]
	,[StartRate]
	,[EndRate]
) VALUES (
    @Idx
    ,@PrizeType
    ,@SubType
    ,@ItemCount
    ,@StartRate
    ,@EndRate
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

CREATE PROCEDURE [dbo].P_ConfigRevelationdraw_Update
	@Idx int, 
	@PrizeType int, 
	@SubType int, 
	@ItemCount int, 
	@StartRate int, 
	@EndRate int 
AS



UPDATE [dbo].[Config_RevelationDraw] SET
	[PrizeType] = @PrizeType
	,[SubType] = @SubType
	,[ItemCount] = @ItemCount
	,[StartRate] = @StartRate
	,[EndRate] = @EndRate
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


