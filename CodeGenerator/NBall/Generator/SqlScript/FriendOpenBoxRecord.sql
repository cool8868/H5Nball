
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Friendopenboxrecord_Delete    Script Date: 2016年3月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendOpenboxrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendOpenboxrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].FriendOpenboxrecord_GetById    Script Date: 2016年3月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendOpenboxrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendOpenboxrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].FriendOpenboxrecord_GetAll    Script Date: 2016年3月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendOpenboxrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendOpenboxrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].FriendOpenboxrecord_Insert    Script Date: 2016年3月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendOpenboxrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendOpenboxrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].FriendOpenboxrecord_Update    Script Date: 2016年3月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendOpenboxrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendOpenboxrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_FriendOpenboxrecord_Delete
	@idx int
AS

DELETE FROM [dbo].[Friend_OpenBoxRecord]
WHERE
	[idx] = @idx

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

CREATE PROCEDURE [dbo].P_FriendOpenboxrecord_GetById
	@idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Friend_OpenBoxRecord] with(nolock)
WHERE
	[idx] = @idx
	
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

CREATE PROCEDURE [dbo].P_FriendOpenboxrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Friend_OpenBoxRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_FriendOpenboxrecord_Insert
	@ManagerId uniqueidentifier , 
	@FriendId uniqueidentifier , 
	@PrizeType int , 
	@PrizeItem int , 
	@PrizeCount int , 
	@RowTime datetime , 
    @idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Friend_OpenBoxRecord] (
	[ManagerId]
	,[FriendId]
	,[PrizeType]
	,[PrizeItem]
	,[PrizeCount]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@FriendId
    ,@PrizeType
    ,@PrizeItem
    ,@PrizeCount
    ,@RowTime
)


SET @idx = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_FriendOpenboxrecord_Update
	@idx int, 
	@ManagerId uniqueidentifier, 
	@FriendId uniqueidentifier, 
	@PrizeType int, 
	@PrizeItem int, 
	@PrizeCount int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Friend_OpenBoxRecord] SET
	[ManagerId] = @ManagerId
	,[FriendId] = @FriendId
	,[PrizeType] = @PrizeType
	,[PrizeItem] = @PrizeItem
	,[PrizeCount] = @PrizeCount
	,[RowTime] = @RowTime
WHERE
	[idx] = @idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



