
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Rankingladderyesterday_Delete    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingLadderyesterday_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingLadderyesterday_Delete]
GO

/****** Object:  Stored Procedure [dbo].RankingLadderyesterday_GetById    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingLadderyesterday_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingLadderyesterday_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RankingLadderyesterday_GetAll    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingLadderyesterday_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingLadderyesterday_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RankingLadderyesterday_Insert    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingLadderyesterday_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingLadderyesterday_Insert]
GO

/****** Object:  Stored Procedure [dbo].RankingLadderyesterday_Update    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RankingLadderyesterday_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RankingLadderyesterday_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RankingLadderyesterday_Delete
	@Idx int
AS

DELETE FROM [dbo].[Ranking_LadderYesterday]
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

CREATE PROCEDURE [dbo].P_RankingLadderyesterday_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ranking_LadderYesterday] with(nolock)
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

CREATE PROCEDURE [dbo].P_RankingLadderyesterday_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ranking_LadderYesterday] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RankingLadderyesterday_Insert
	@RankType int , 
	@ManagerId uniqueidentifier , 
	@Rank int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Ranking_LadderYesterday] (
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

CREATE PROCEDURE [dbo].P_RankingLadderyesterday_Update
	@Idx int, 
	@RankType int, 
	@ManagerId uniqueidentifier, 
	@Rank int 
AS



UPDATE [dbo].[Ranking_LadderYesterday] SET
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



