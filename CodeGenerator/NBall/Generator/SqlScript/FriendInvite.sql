
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Friendinvite_Delete    Script Date: 2016年6月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Friendinvite_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Friendinvite_Delete]
GO

/****** Object:  Stored Procedure [dbo].Friendinvite_GetById    Script Date: 2016年6月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Friendinvite_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Friendinvite_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].Friendinvite_GetAll    Script Date: 2016年6月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Friendinvite_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Friendinvite_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].Friendinvite_Insert    Script Date: 2016年6月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Friendinvite_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Friendinvite_Insert]
GO

/****** Object:  Stored Procedure [dbo].Friendinvite_Update    Script Date: 2016年6月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Friendinvite_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Friendinvite_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_Friendinvite_Delete
	@ByAccount varchar(100)
AS

DELETE FROM [dbo].[FriendInvite]
WHERE
	[ByAccount] = @ByAccount

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

CREATE PROCEDURE [dbo].P_Friendinvite_GetById
	@ByAccount varchar(100)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[FriendInvite] with(nolock)
WHERE
	[ByAccount] = @ByAccount
	
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

CREATE PROCEDURE [dbo].P_Friendinvite_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[FriendInvite] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_Friendinvite_Insert
	@ByAccount varchar(100)
	,@Account varchar(100)
	,@Level int
	,@IsPrize bit
	,@MayPrize int
	,@AlreadyPrize int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[FriendInvite] (
	[ByAccount]
	,[Account]
	,[Level]
	,[IsPrize]
	,[MayPrize]
	,[AlreadyPrize]
) VALUES (
    @ByAccount
    ,@Account
    ,@Level
    ,@IsPrize
    ,@MayPrize
    ,@AlreadyPrize
)

select @ByAccount

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

CREATE PROCEDURE [dbo].P_Friendinvite_Update
	@ByAccount varchar(100), -- 被邀请的经理ID
	@Account varchar(100), -- 邀请的经理ID
	@Level int, -- 等级
	@IsPrize bit, -- 是否可以领取
	@MayPrize int, -- 可以领取多少点
	@AlreadyPrize int -- 已经领取了多少点
AS



UPDATE [dbo].[FriendInvite] SET
	[Account] = @Account
	,[Level] = @Level
	,[IsPrize] = @IsPrize
	,[MayPrize] = @MayPrize
	,[AlreadyPrize] = @AlreadyPrize
WHERE
	[ByAccount] = @ByAccount

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


