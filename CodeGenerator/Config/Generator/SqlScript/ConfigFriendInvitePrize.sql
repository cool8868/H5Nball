
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configfriendinviteprize_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFriendinviteprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFriendinviteprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigFriendinviteprize_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFriendinviteprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFriendinviteprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigFriendinviteprize_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFriendinviteprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFriendinviteprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigFriendinviteprize_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFriendinviteprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFriendinviteprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigFriendinviteprize_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFriendinviteprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFriendinviteprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigFriendinviteprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_FriendInvitePrize]
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

CREATE PROCEDURE [dbo].P_ConfigFriendinviteprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_FriendInvitePrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigFriendinviteprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_FriendInvitePrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigFriendinviteprize_Insert
	@Idx int
	,@SucceedCount int
	,@PrizeType int
	,@ItemCode int
	,@Count int
	,@IsBinding bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_FriendInvitePrize] (
	[Idx]
	,[SucceedCount]
	,[PrizeType]
	,[ItemCode]
	,[Count]
	,[IsBinding]
) VALUES (
    @Idx
    ,@SucceedCount
    ,@PrizeType
    ,@ItemCode
    ,@Count
    ,@IsBinding
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

CREATE PROCEDURE [dbo].P_ConfigFriendinviteprize_Update
	@Idx int, 
	@SucceedCount int, -- 邀请成功人数
	@PrizeType int, -- 奖励类型 1=金币 2= 物品
	@ItemCode int, -- 物品ID
	@Count int, -- 数量
	@IsBinding bit -- 是否绑定
AS



UPDATE [dbo].[Config_FriendInvitePrize] SET
	[SucceedCount] = @SucceedCount
	,[PrizeType] = @PrizeType
	,[ItemCode] = @ItemCode
	,[Count] = @Count
	,[IsBinding] = @IsBinding
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



