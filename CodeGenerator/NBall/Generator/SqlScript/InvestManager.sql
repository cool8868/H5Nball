
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Investmanager_Delete    Script Date: 2016年3月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InvestManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InvestManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].InvestManager_GetById    Script Date: 2016年3月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InvestManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InvestManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].InvestManager_GetAll    Script Date: 2016年3月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InvestManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InvestManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].InvestManager_Insert    Script Date: 2016年3月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InvestManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InvestManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].InvestManager_Update    Script Date: 2016年3月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_InvestManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_InvestManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_InvestManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Invest_Manager]
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

CREATE PROCEDURE [dbo].P_InvestManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Invest_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_InvestManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Invest_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_InvestManager_Insert
	@Deposit int , 
	@StepStatus varchar(100) , 
	@TheMonthly bit , 
	@MonthlyTime datetime , 
	@ExpirationTime datetime , 
	@Once bit , 
	@ReceivedCount int , 
	@RowTime datetime , 
	@DepositCount int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Invest_Manager] (
	[ManagerId],
	[Deposit]
	,[StepStatus]
	,[TheMonthly]
	,[MonthlyTime]
	,[ExpirationTime]
	,[Once]
	,[ReceivedCount]
	,[RowTime]
	,[DepositCount]
) VALUES (
	@ManagerId,
    @Deposit
    ,@StepStatus
    ,@TheMonthly
    ,@MonthlyTime
    ,@ExpirationTime
    ,@Once
    ,@ReceivedCount
    ,@RowTime
    ,@DepositCount
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

CREATE PROCEDURE [dbo].P_InvestManager_Update
	@ManagerId uniqueidentifier, 
	@Deposit int, -- 存的点券
	@StepStatus varchar(100), -- 存入每档领取状态：0-默认 1-可领  2-已领取
	@TheMonthly bit, 
	@MonthlyTime datetime, 
	@ExpirationTime datetime, 
	@Once bit, 
	@ReceivedCount int, 
	@RowTime datetime, 
	@DepositCount int 
AS



UPDATE [dbo].[Invest_Manager] SET
	[Deposit] = @Deposit
	,[StepStatus] = @StepStatus
	,[TheMonthly] = @TheMonthly
	,[MonthlyTime] = @MonthlyTime
	,[ExpirationTime] = @ExpirationTime
	,[Once] = @Once
	,[ReceivedCount] = @ReceivedCount
	,[RowTime] = @RowTime
	,[DepositCount] = @DepositCount
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



