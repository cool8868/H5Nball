
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Exchangeinfo_Delete    Script Date: 2016年6月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ExchangeInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ExchangeInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].ExchangeInfo_GetById    Script Date: 2016年6月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ExchangeInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ExchangeInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ExchangeInfo_GetAll    Script Date: 2016年6月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ExchangeInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ExchangeInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ExchangeInfo_Insert    Script Date: 2016年6月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ExchangeInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ExchangeInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].ExchangeInfo_Update    Script Date: 2016年6月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ExchangeInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ExchangeInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ExchangeInfo_Delete
	@Idx varchar(50)
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Exchange_Info]
WHERE
	[Idx] = @Idx
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

CREATE PROCEDURE [dbo].P_ExchangeInfo_GetById
	@Idx varchar(50)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Exchange_Info] with(nolock)
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

CREATE PROCEDURE [dbo].P_ExchangeInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Exchange_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ExchangeInfo_Insert
	@Idx varchar(50)
	,@ExchangeType int
	,@ZoneName int
	,@Account varchar(50)
	,@ManagerId uniqueidentifier
	,@AtZoneId int
	,@PackId int
	,@BatchId int
	,@Status int
	,@RowTime datetime
	,@UpdateTime datetime
	,@RowVersion timestamp
	,@PlatformCode varchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Exchange_Info] (
	[Idx]
	,[ExchangeType]
	,[ZoneName]
	,[Account]
	,[ManagerId]
	,[AtZoneId]
	,[PackId]
	,[BatchId]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[PlatformCode]
) VALUES (
    @Idx
    ,@ExchangeType
    ,@ZoneName
    ,@Account
    ,@ManagerId
    ,@AtZoneId
    ,@PackId
    ,@BatchId
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@PlatformCode
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

CREATE PROCEDURE [dbo].P_ExchangeInfo_Update
	@Idx varchar(50), 
	@ExchangeType int, -- 礼包类型
	@ZoneName int, -- 针对区id
	@Account varchar(50), 
	@ManagerId uniqueidentifier, 
	@AtZoneId int, -- 兑换用户所在区
	@PackId int, -- 对应礼包id
	@BatchId int, -- 兑换批次
	@Status int, -- 状态：0，初始；1，已使用 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@RowVersion timestamp, 
	@PlatformCode varchar(50) 
AS



UPDATE [dbo].[Exchange_Info] SET
	[ExchangeType] = @ExchangeType
	,[ZoneName] = @ZoneName
	,[Account] = @Account
	,[ManagerId] = @ManagerId
	,[AtZoneId] = @AtZoneId
	,[PackId] = @PackId
	,[BatchId] = @BatchId
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[PlatformCode] = @PlatformCode
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


