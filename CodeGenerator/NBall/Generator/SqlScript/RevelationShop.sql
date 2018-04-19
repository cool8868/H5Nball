
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationshop_Delete    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationShop_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationShop_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationShop_GetById    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationShop_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationShop_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationShop_GetAll    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationShop_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationShop_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationShop_Insert    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationShop_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationShop_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationShop_Update    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationShop_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationShop_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationShop_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Revelation_Shop]
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

CREATE PROCEDURE [dbo].P_RevelationShop_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Shop] with(nolock)
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

CREATE PROCEDURE [dbo].P_RevelationShop_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Shop] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationShop_Insert
	@ItemString varchar(500) , 
	@ExChangeString varchar(500) , 
	@Status int , 
	@RefreshData datetime , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_Shop] (
	[ManagerId],
	[ItemString]
	,[ExChangeString]
	,[Status]
	,[RefreshData]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @ItemString
    ,@ExChangeString
    ,@Status
    ,@RefreshData
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

CREATE PROCEDURE [dbo].P_RevelationShop_Update
	@ManagerId uniqueidentifier, 
	@ItemString varchar(500), 
	@ExChangeString varchar(500), 
	@Status int, 
	@RefreshData datetime, 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Revelation_Shop] SET
	[ItemString] = @ItemString
	,[ExChangeString] = @ExChangeString
	,[Status] = @Status
	,[RefreshData] = @RefreshData
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


