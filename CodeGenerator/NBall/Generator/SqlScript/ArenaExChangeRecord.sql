
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Arenaexchangerecord_Delete    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaExchangerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaExchangerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ArenaExchangerecord_GetById    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaExchangerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaExchangerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ArenaExchangerecord_GetAll    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaExchangerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaExchangerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ArenaExchangerecord_Insert    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaExchangerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaExchangerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ArenaExchangerecord_Update    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaExchangerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaExchangerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ArenaExchangerecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Arena_ExChangeRecord]
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

CREATE PROCEDURE [dbo].P_ArenaExchangerecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_ExChangeRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_ArenaExchangerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_ExChangeRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ArenaExchangerecord_Insert
	@ManagerId uniqueidentifier , 
	@ExItemCode int , 
	@ArenaCoin int , 
	@ExItemCount int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Arena_ExChangeRecord] (
	[ManagerId]
	,[ExItemCode]
	,[ArenaCoin]
	,[ExItemCount]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ExItemCode
    ,@ArenaCoin
    ,@ExItemCount
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_ArenaExchangerecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ExItemCode int, -- 兑换物品code
	@ArenaCoin int, -- 消耗竞技币
	@ExItemCount int, -- 兑换物品数量
	@RowTime datetime 
AS



UPDATE [dbo].[Arena_ExChangeRecord] SET
	[ManagerId] = @ManagerId
	,[ExItemCode] = @ExItemCode
	,[ArenaCoin] = @ArenaCoin
	,[ExItemCount] = @ExItemCount
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


