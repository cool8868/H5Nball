
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Paychargehistory_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayChargehistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayChargehistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].PayChargehistory_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayChargehistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayChargehistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PayChargehistory_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayChargehistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayChargehistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PayChargehistory_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayChargehistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayChargehistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].PayChargehistory_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayChargehistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayChargehistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PayChargehistory_Delete
	@Idx varchar(50)
AS

DELETE FROM [dbo].[Pay_ChargeHistory]
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

CREATE PROCEDURE [dbo].P_PayChargehistory_GetById
	@Idx varchar(50)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Pay_ChargeHistory] with(nolock)
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

CREATE PROCEDURE [dbo].P_PayChargehistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Pay_ChargeHistory] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PayChargehistory_Insert
	@Idx varchar(50)
	,@Account varchar(200)
	,@SourceType int
	,@BillingId varchar(50)
	,@Point int
	,@Bonus int
	,@Cash decimal(18, 2)
	,@IsFirst bit
	,@RowTime datetime
	,@UpdateTime datetime
	,@MallCode int
	,@States int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Pay_ChargeHistory] (
	[Idx]
	,[Account]
	,[SourceType]
	,[BillingId]
	,[Point]
	,[Bonus]
	,[Cash]
	,[IsFirst]
	,[RowTime]
	,[UpdateTime]
	,[MallCode]
	,[States]
) VALUES (
    @Idx
    ,@Account
    ,@SourceType
    ,@BillingId
    ,@Point
    ,@Bonus
    ,@Cash
    ,@IsFirst
    ,@RowTime
    ,@UpdateTime
    ,@MallCode
    ,@States
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

CREATE PROCEDURE [dbo].P_PayChargehistory_Update
	@Idx varchar(50), 
	@Account varchar(200), -- Account
	@SourceType int, -- 来源类型：0，充值；1，联赛竞猜；2，邮件收取
	@BillingId varchar(50), -- 订单id
	@Point int, -- 点券数
	@Bonus int, -- 赠送点数
	@Cash decimal(18, 2), -- 支付现金数
	@IsFirst bit, -- 是否首充
	@RowTime datetime, -- RowTime
	@UpdateTime datetime, 
	@MallCode int, 
	@States int 
AS



UPDATE [dbo].[Pay_ChargeHistory] SET
	[Account] = @Account
	,[SourceType] = @SourceType
	,[BillingId] = @BillingId
	,[Point] = @Point
	,[Bonus] = @Bonus
	,[Cash] = @Cash
	,[IsFirst] = @IsFirst
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[MallCode] = @MallCode
	,[States] = @States
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


