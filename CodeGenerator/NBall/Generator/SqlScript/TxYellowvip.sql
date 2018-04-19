
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Txyellowvip_Delete    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxYellowvip_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxYellowvip_Delete]
GO

/****** Object:  Stored Procedure [dbo].TxYellowvip_GetById    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxYellowvip_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxYellowvip_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TxYellowvip_GetAll    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxYellowvip_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxYellowvip_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TxYellowvip_Insert    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxYellowvip_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxYellowvip_Insert]
GO

/****** Object:  Stored Procedure [dbo].TxYellowvip_Update    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TxYellowvip_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TxYellowvip_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TxYellowvip_Delete
	@Account varchar(50)
AS

DELETE FROM [dbo].[Tx_YellowVip]
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

CREATE PROCEDURE [dbo].P_TxYellowvip_GetById
	@Account varchar(50)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Tx_YellowVip] with(nolock)
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

CREATE PROCEDURE [dbo].P_TxYellowvip_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Tx_YellowVip] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TxYellowvip_Insert
	@Account varchar(50)
	,@IsYellowVip bit
	,@IsYellowYearVip bit
	,@IsYellowHighVip bit
	,@YellowVipLevel int
	,@OpeningTimes int
	,@Status int
	,@RowTime datetime
	,@UpdateTime datetime
	,@ExtraData varchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Tx_YellowVip] (
	[Account]
	,[IsYellowVip]
	,[IsYellowYearVip]
	,[IsYellowHighVip]
	,[YellowVipLevel]
	,[OpeningTimes]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[ExtraData]
) VALUES (
    @Account
    ,@IsYellowVip
    ,@IsYellowYearVip
    ,@IsYellowHighVip
    ,@YellowVipLevel
    ,@OpeningTimes
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@ExtraData
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

CREATE PROCEDURE [dbo].P_TxYellowvip_Update
	@Account varchar(50), 
	@IsYellowVip bit, 
	@IsYellowYearVip bit, 
	@IsYellowHighVip bit, 
	@YellowVipLevel int, 
	@OpeningTimes int, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@ExtraData varchar(100) 
AS



UPDATE [dbo].[Tx_YellowVip] SET
	[IsYellowVip] = @IsYellowVip
	,[IsYellowYearVip] = @IsYellowYearVip
	,[IsYellowHighVip] = @IsYellowHighVip
	,[YellowVipLevel] = @YellowVipLevel
	,[OpeningTimes] = @OpeningTimes
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[ExtraData] = @ExtraData
WHERE
	[Account] = @Account

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


