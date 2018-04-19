
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Penaltykickseason_Delete    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickSeason_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickSeason_Delete]
GO

/****** Object:  Stored Procedure [dbo].PenaltykickSeason_GetById    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickSeason_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickSeason_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PenaltykickSeason_GetAll    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickSeason_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickSeason_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PenaltykickSeason_Insert    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickSeason_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickSeason_Insert]
GO

/****** Object:  Stored Procedure [dbo].PenaltykickSeason_Update    Script Date: 2016年9月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PenaltykickSeason_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PenaltykickSeason_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PenaltykickSeason_Delete
	@Idx int
AS

DELETE FROM [dbo].[PenaltyKick_Season]
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

CREATE PROCEDURE [dbo].P_PenaltykickSeason_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PenaltyKick_Season] with(nolock)
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

CREATE PROCEDURE [dbo].P_PenaltykickSeason_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PenaltyKick_Season] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PenaltykickSeason_Insert
	@Idx int
	,@StartTime datetime
	,@EndTime datetime
	,@IsPrize bit
	,@PrizeTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[PenaltyKick_Season] (
	[Idx]
	,[StartTime]
	,[EndTime]
	,[IsPrize]
	,[PrizeTime]
) VALUES (
    @Idx
    ,@StartTime
    ,@EndTime
    ,@IsPrize
    ,@PrizeTime
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

CREATE PROCEDURE [dbo].P_PenaltykickSeason_Update
	@Idx int, -- 赛季ID
	@StartTime datetime, -- 开始时间
	@EndTime datetime, -- 结束时间
	@IsPrize bit, -- 是否发奖
	@PrizeTime datetime -- 发奖时间
AS



UPDATE [dbo].[PenaltyKick_Season] SET
	[StartTime] = @StartTime
	,[EndTime] = @EndTime
	,[IsPrize] = @IsPrize
	,[PrizeTime] = @PrizeTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


