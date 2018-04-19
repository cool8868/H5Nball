
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Allcsdk_Delete    Script Date: 2016年5月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllCsdk_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllCsdk_Delete]
GO

/****** Object:  Stored Procedure [dbo].AllCsdk_GetById    Script Date: 2016年5月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllCsdk_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllCsdk_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AllCsdk_GetAll    Script Date: 2016年5月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllCsdk_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllCsdk_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AllCsdk_Insert    Script Date: 2016年5月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllCsdk_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllCsdk_Insert]
GO

/****** Object:  Stored Procedure [dbo].AllCsdk_Update    Script Date: 2016年5月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllCsdk_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllCsdk_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AllCsdk_Delete
	@Idx int
AS

DELETE FROM [dbo].[All_CSDK]
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

CREATE PROCEDURE [dbo].P_AllCsdk_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_CSDK] with(nolock)
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

CREATE PROCEDURE [dbo].P_AllCsdk_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_CSDK] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AllCsdk_Insert
	@_sign text , 
	@orderId int , 
	@gameOrderId text , 
	@price int , 
	@channelAlias text , 
	@playerId text , 
	@serverId text , 
	@goodsId int , 
	@payResult int , 
	@_state text , 
	@payTime date , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[All_CSDK] (
	[_sign]
	,[orderId]
	,[gameOrderId]
	,[price]
	,[channelAlias]
	,[playerId]
	,[serverId]
	,[goodsId]
	,[payResult]
	,[_state]
	,[payTime]
) VALUES (
    @_sign
    ,@orderId
    ,@gameOrderId
    ,@price
    ,@channelAlias
    ,@playerId
    ,@serverId
    ,@goodsId
    ,@payResult
    ,@_state
    ,@payTime
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

CREATE PROCEDURE [dbo].P_AllCsdk_Update
	@Idx int, 
	@_sign text, 
	@orderId int, 
	@gameOrderId text, 
	@price int, 
	@channelAlias text, 
	@playerId text, 
	@serverId text, 
	@goodsId int, 
	@payResult int, 
	@_state text, 
	@payTime date 
AS



UPDATE [dbo].[All_CSDK] SET
	[_sign] = @_sign
	,[orderId] = @orderId
	,[gameOrderId] = @gameOrderId
	,[price] = @price
	,[channelAlias] = @channelAlias
	,[playerId] = @playerId
	,[serverId] = @serverId
	,[goodsId] = @goodsId
	,[payResult] = @payResult
	,[_state] = @_state
	,[payTime] = @payTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


