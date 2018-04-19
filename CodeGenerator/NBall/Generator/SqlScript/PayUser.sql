
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Payuser_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayUser_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayUser_Delete]
GO

/****** Object:  Stored Procedure [dbo].PayUser_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayUser_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayUser_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PayUser_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayUser_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayUser_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PayUser_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayUser_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayUser_Insert]
GO

/****** Object:  Stored Procedure [dbo].PayUser_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayUser_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayUser_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PayUser_Delete
	@Account varchar(200)
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Pay_User]
WHERE
	[Account] = @Account
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_PayUser_GetById
	@Account varchar(200)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Pay_User] with(nolock)
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

CREATE PROCEDURE [dbo].P_PayUser_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Pay_User] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PayUser_Insert
	@Account varchar(200)
	,@Point int
	,@Bonus int
	,@TotalCash decimal(18, 2)
	,@RowTime datetime
	,@RowVersion timestamp
	,@ChargePoint int
	,@BindPoint int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Pay_User] (
	[Account]
	,[Point]
	,[Bonus]
	,[TotalCash]
	,[RowTime]
	,[ChargePoint]
	,[BindPoint]
) VALUES (
    @Account
    ,@Point
    ,@Bonus
    ,@TotalCash
    ,@RowTime
    ,@ChargePoint
    ,@BindPoint
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

CREATE PROCEDURE [dbo].P_PayUser_Update
	@Account varchar(200), -- Account
	@Point int, -- 点券数
	@Bonus int, -- 免费点券数
	@TotalCash decimal(18, 2), -- 总充值金额
	@RowTime datetime, -- RowTime
	@RowVersion timestamp, 
	@ChargePoint int, 
	@BindPoint int 
AS



UPDATE [dbo].[Pay_User] SET
	[Point] = @Point
	,[Bonus] = @Bonus
	,[TotalCash] = @TotalCash
	,[RowTime] = @RowTime
	,[ChargePoint] = @ChargePoint
	,[BindPoint] = @BindPoint
WHERE
	[Account] = @Account
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


