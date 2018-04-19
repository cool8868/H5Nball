
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Arenashop_Delete    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaShop_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaShop_Delete]
GO

/****** Object:  Stored Procedure [dbo].ArenaShop_GetById    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaShop_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaShop_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ArenaShop_GetAll    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaShop_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaShop_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ArenaShop_Insert    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaShop_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaShop_Insert]
GO

/****** Object:  Stored Procedure [dbo].ArenaShop_Update    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaShop_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaShop_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ArenaShop_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Arena_Shop]
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

CREATE PROCEDURE [dbo].P_ArenaShop_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_Shop] with(nolock)
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

CREATE PROCEDURE [dbo].P_ArenaShop_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_Shop] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ArenaShop_Insert
	@ItemString varchar(500) , 
	@ExChangeRecord varchar(50) , 
	@RefreshTime datetime , 
	@RefreshNumber int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Arena_Shop] (
	[ManagerId],
	[ItemString]
	,[ExChangeRecord]
	,[RefreshTime]
	,[RefreshNumber]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @ItemString
    ,@ExChangeRecord
    ,@RefreshTime
    ,@RefreshNumber
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

CREATE PROCEDURE [dbo].P_ArenaShop_Update
	@ManagerId uniqueidentifier, 
	@ItemString varchar(500), 
	@ExChangeRecord varchar(50), -- 兑换记录   0,0,0,0,0,0 或 1,1,1,1,1,1
	@RefreshTime datetime, 
	@RefreshNumber int, -- 刷新次数
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Arena_Shop] SET
	[ItemString] = @ItemString
	,[ExChangeRecord] = @ExChangeRecord
	,[RefreshTime] = @RefreshTime
	,[RefreshNumber] = @RefreshNumber
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


