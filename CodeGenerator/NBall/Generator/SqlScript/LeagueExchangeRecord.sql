
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Leagueexchangerecord_Delete    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueExchangerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueExchangerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].LeagueExchangerecord_GetById    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueExchangerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueExchangerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LeagueExchangerecord_GetAll    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueExchangerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueExchangerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LeagueExchangerecord_Insert    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueExchangerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueExchangerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].LeagueExchangerecord_Update    Script Date: 2016年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueExchangerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueExchangerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LeagueExchangerecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[League_ExchangeRecord]
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

CREATE PROCEDURE [dbo].P_LeagueExchangerecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_ExchangeRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_LeagueExchangerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_ExchangeRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LeagueExchangerecord_Insert
	@ManagerId uniqueidentifier , 
	@ItemCode int , 
	@CostScore int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[League_ExchangeRecord] (
	[ManagerId]
	,[ItemCode]
	,[CostScore]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ItemCode
    ,@CostScore
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_LeagueExchangerecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ItemCode int, 
	@CostScore int, 
	@RowTime datetime 
AS



UPDATE [dbo].[League_ExchangeRecord] SET
	[ManagerId] = @ManagerId
	,[ItemCode] = @ItemCode
	,[CostScore] = @CostScore
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



