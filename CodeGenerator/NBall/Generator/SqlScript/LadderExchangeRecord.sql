
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Ladderexchangerecord_Delete    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderExchangerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderExchangerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].LadderExchangerecord_GetById    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderExchangerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderExchangerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LadderExchangerecord_GetAll    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderExchangerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderExchangerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LadderExchangerecord_Insert    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderExchangerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderExchangerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].LadderExchangerecord_Update    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderExchangerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderExchangerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LadderExchangerecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Ladder_ExchangeRecord]
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

CREATE PROCEDURE [dbo].P_LadderExchangerecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_ExchangeRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_LadderExchangerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_ExchangeRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LadderExchangerecord_Insert
	@ManagerId uniqueidentifier , 
	@ItemCode int , 
	@CostHonor int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Ladder_ExchangeRecord] (
	[ManagerId]
	,[ItemCode]
	,[CostHonor]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ItemCode
    ,@CostHonor
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

CREATE PROCEDURE [dbo].P_LadderExchangerecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ItemCode int, 
	@CostHonor int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Ladder_ExchangeRecord] SET
	[ManagerId] = @ManagerId
	,[ItemCode] = @ItemCode
	,[CostHonor] = @CostHonor
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



