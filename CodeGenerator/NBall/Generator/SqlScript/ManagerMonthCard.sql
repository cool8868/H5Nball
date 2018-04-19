
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Managermonthcard_Delete    Script Date: 2016年5月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerMonthcard_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerMonthcard_Delete]
GO

/****** Object:  Stored Procedure [dbo].ManagerMonthcard_GetById    Script Date: 2016年5月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerMonthcard_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerMonthcard_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ManagerMonthcard_GetAll    Script Date: 2016年5月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerMonthcard_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerMonthcard_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ManagerMonthcard_Insert    Script Date: 2016年5月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerMonthcard_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerMonthcard_Insert]
GO

/****** Object:  Stored Procedure [dbo].ManagerMonthcard_Update    Script Date: 2016年5月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerMonthcard_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerMonthcard_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ManagerMonthcard_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Manager_MonthCard]
WHERE
	[ManagerId] = @ManagerId

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

CREATE PROCEDURE [dbo].P_ManagerMonthcard_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Manager_MonthCard] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
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

CREATE PROCEDURE [dbo].P_ManagerMonthcard_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Manager_MonthCard] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ManagerMonthcard_Insert
	@BuyNumber int , 
	@BuyTime datetime , 
	@DueToTime datetime , 
	@PrizeDate date , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Manager_MonthCard] (
	[ManagerId],
	[BuyNumber]
	,[BuyTime]
	,[DueToTime]
	,[PrizeDate]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @BuyNumber
    ,@BuyTime
    ,@DueToTime
    ,@PrizeDate
    ,@UpdateTime
    ,@RowTime
)




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

CREATE PROCEDURE [dbo].P_ManagerMonthcard_Update
	@ManagerId uniqueidentifier, 
	@BuyNumber int, 
	@BuyTime datetime, 
	@DueToTime datetime, 
	@PrizeDate date, 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Manager_MonthCard] SET
	[BuyNumber] = @BuyNumber
	,[BuyTime] = @BuyTime
	,[DueToTime] = @DueToTime
	,[PrizeDate] = @PrizeDate
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



