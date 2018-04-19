
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Paycontinuing_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayContinuing_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayContinuing_Delete]
GO

/****** Object:  Stored Procedure [dbo].PayContinuing_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayContinuing_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayContinuing_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PayContinuing_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayContinuing_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayContinuing_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PayContinuing_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayContinuing_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayContinuing_Insert]
GO

/****** Object:  Stored Procedure [dbo].PayContinuing_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayContinuing_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayContinuing_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PayContinuing_Delete
	@Account varchar(200)
AS

DELETE FROM [dbo].[Pay_Continuing]
WHERE
	[Account] = @Account

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

CREATE PROCEDURE [dbo].P_PayContinuing_GetById
	@Account varchar(200)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Pay_Continuing] with(nolock)
WHERE
	[Account] = @Account
	
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

CREATE PROCEDURE [dbo].P_PayContinuing_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Pay_Continuing] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PayContinuing_Insert
	@Account varchar(200)
	,@LastPayDate datetime
	,@ContinuingDay int
	,@StartDate datetime
	,@EndDate datetime
	,@TotalPoint int
	,@Status int
	,@RowTime datetime
	,@UpdateTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Pay_Continuing] (
	[Account]
	,[LastPayDate]
	,[ContinuingDay]
	,[StartDate]
	,[EndDate]
	,[TotalPoint]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @Account
    ,@LastPayDate
    ,@ContinuingDay
    ,@StartDate
    ,@EndDate
    ,@TotalPoint
    ,@Status
    ,@RowTime
    ,@UpdateTime
)

select @Account

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

CREATE PROCEDURE [dbo].P_PayContinuing_Update
	@Account varchar(200), 
	@LastPayDate datetime, 
	@ContinuingDay int, 
	@StartDate datetime, 
	@EndDate datetime, 
	@TotalPoint int, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Pay_Continuing] SET
	[LastPayDate] = @LastPayDate
	,[ContinuingDay] = @ContinuingDay
	,[StartDate] = @StartDate
	,[EndDate] = @EndDate
	,[TotalPoint] = @TotalPoint
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Account] = @Account

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


