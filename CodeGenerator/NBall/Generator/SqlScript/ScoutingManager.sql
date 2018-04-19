
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Scoutingmanager_Delete    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].ScoutingManager_GetById    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ScoutingManager_GetAll    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ScoutingManager_Insert    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].ScoutingManager_Update    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ScoutingManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Scouting_Manager]
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

CREATE PROCEDURE [dbo].P_ScoutingManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Scouting_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_ScoutingManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Scouting_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ScoutingManager_Insert
	@CoinLotteryCount int , 
	@CoinTenLotteryCount int , 
	@PointLotteryCount int , 
	@PointTenLotteryCount int , 
	@FriendLotteryCount int , 
	@FriendTenLotteryCount int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@SpecialItemCoin int , 
	@SpecialItemPoint int , 
	@SpecialItemFriend int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Scouting_Manager] (
	[ManagerId],
	[CoinLotteryCount]
	,[CoinTenLotteryCount]
	,[PointLotteryCount]
	,[PointTenLotteryCount]
	,[FriendLotteryCount]
	,[FriendTenLotteryCount]
	,[RowTime]
	,[UpdateTime]
	,[SpecialItemCoin]
	,[SpecialItemPoint]
	,[SpecialItemFriend]
) VALUES (
	@ManagerId,
    @CoinLotteryCount
    ,@CoinTenLotteryCount
    ,@PointLotteryCount
    ,@PointTenLotteryCount
    ,@FriendLotteryCount
    ,@FriendTenLotteryCount
    ,@RowTime
    ,@UpdateTime
    ,@SpecialItemCoin
    ,@SpecialItemPoint
    ,@SpecialItemFriend
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

CREATE PROCEDURE [dbo].P_ScoutingManager_Update
	@ManagerId uniqueidentifier, 
	@CoinLotteryCount int, -- 金币单抽次数
	@CoinTenLotteryCount int, -- 金币十连抽次数
	@PointLotteryCount int, -- 点券单抽次数
	@PointTenLotteryCount int, -- 点券十连抽次数
	@FriendLotteryCount int, -- 友情点单抽次数
	@FriendTenLotteryCount int, -- 友情点十连抽次数
	@RowTime datetime, 
	@UpdateTime datetime, 
	@SpecialItemCoin int, 
	@SpecialItemPoint int, 
	@SpecialItemFriend int 
AS



UPDATE [dbo].[Scouting_Manager] SET
	[CoinLotteryCount] = @CoinLotteryCount
	,[CoinTenLotteryCount] = @CoinTenLotteryCount
	,[PointLotteryCount] = @PointLotteryCount
	,[PointTenLotteryCount] = @PointTenLotteryCount
	,[FriendLotteryCount] = @FriendLotteryCount
	,[FriendTenLotteryCount] = @FriendTenLotteryCount
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[SpecialItemCoin] = @SpecialItemCoin
	,[SpecialItemPoint] = @SpecialItemPoint
	,[SpecialItemFriend] = @SpecialItemFriend
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


