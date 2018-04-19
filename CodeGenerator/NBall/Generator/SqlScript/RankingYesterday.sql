
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Rankingyesterday_Delete    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingYesterday_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingYesterday_Delete]
GO

/****** Object:  Stored Procedure [dbo].RankingYesterday_GetById    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingYesterday_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingYesterday_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RankingYesterday_GetAll    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingYesterday_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingYesterday_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RankingYesterday_Insert    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingYesterday_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingYesterday_Insert]
GO

/****** Object:  Stored Procedure [dbo].RankingYesterday_Update    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingYesterday_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingYesterday_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RankingYesterday_Delete
	@Idx int
AS

DELETE FROM [dbo].[Ranking_Yesterday]
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

CREATE PROCEDURE [dbo].P_RankingYesterday_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ranking_Yesterday] with(nolock)
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

CREATE PROCEDURE [dbo].P_RankingYesterday_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ranking_Yesterday] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RankingYesterday_Insert
	@RankType int , 
	@ManagerId uniqueidentifier , 
	@Rank int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Ranking_Yesterday] (
	[RankType]
	,[ManagerId]
	,[Rank]
) VALUES (
    @RankType
    ,@ManagerId
    ,@Rank
)


SET @Idx = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_RankingYesterday_Update
	@Idx int, 
	@RankType int, -- 排名类型
	@ManagerId uniqueidentifier, 
	@Rank int 
AS



UPDATE [dbo].[Ranking_Yesterday] SET
	[RankType] = @RankType
	,[ManagerId] = @ManagerId
	,[Rank] = @Rank
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



