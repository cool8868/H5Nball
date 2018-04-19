
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Friendinviteprizerecord_Delete    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendinvitePrizerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendinvitePrizerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].FriendinvitePrizerecord_GetById    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendinvitePrizerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendinvitePrizerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].FriendinvitePrizerecord_GetAll    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendinvitePrizerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendinvitePrizerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].FriendinvitePrizerecord_Insert    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendinvitePrizerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendinvitePrizerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].FriendinvitePrizerecord_Update    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendinvitePrizerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendinvitePrizerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_FriendinvitePrizerecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[FriendInvite_PrizeRecord]
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

CREATE PROCEDURE [dbo].P_FriendinvitePrizerecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[FriendInvite_PrizeRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_FriendinvitePrizerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[FriendInvite_PrizeRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_FriendinvitePrizerecord_Insert
	@Account varchar(100) , 
	@PrizeType int , 
	@PrizeInfo varchar(5000) , 
	@PrizeString varchar(1000) , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[FriendInvite_PrizeRecord] (
	[Account]
	,[PrizeType]
	,[PrizeInfo]
	,[PrizeString]
	,[UpdateTime]
) VALUES (
    @Account
    ,@PrizeType
    ,@PrizeInfo
    ,@PrizeString
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_FriendinvitePrizerecord_Update
	@Idx int, 
	@Account varchar(100), -- 领取人
	@PrizeType int, -- 领取奖励类型 0= 邀请奖励 1= 成长奖励
	@PrizeInfo varchar(5000), -- 奖励详情 prizeType =1时存的是领取的被邀请人ID,分割
	@PrizeString varchar(1000), -- 领取奖励物品串 prizeType = 0时 存的是 ItemCode,ItemNumber|ItemCode,ItemNumber = 1时存的是点卷数量
	@UpdateTime datetime -- 领取时间
AS



UPDATE [dbo].[FriendInvite_PrizeRecord] SET
	[Account] = @Account
	,[PrizeType] = @PrizeType
	,[PrizeInfo] = @PrizeInfo
	,[PrizeString] = @PrizeString
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



