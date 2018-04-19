
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Friendmatch_Delete    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendMatch_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendMatch_Delete]
GO

/****** Object:  Stored Procedure [dbo].FriendMatch_GetById    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendMatch_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendMatch_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].FriendMatch_GetAll    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendMatch_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendMatch_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].FriendMatch_Insert    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendMatch_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendMatch_Insert]
GO

/****** Object:  Stored Procedure [dbo].FriendMatch_Update    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendMatch_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendMatch_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_FriendMatch_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Friend_Match]
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

CREATE PROCEDURE [dbo].P_FriendMatch_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Friend_Match] with(nolock)
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

CREATE PROCEDURE [dbo].P_FriendMatch_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Friend_Match] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_FriendMatch_Insert
	@HomeId uniqueidentifier , 
	@AwayId uniqueidentifier , 
	@HomeName nvarchar(50) , 
	@AwayName nvarchar(50) , 
	@HomeScore int , 
	@AwayScore int , 
	@Intimacy int , 
	@IsFriend bit , 
	@Status int , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Friend_Match] (
	[Idx],
	[HomeId]
	,[AwayId]
	,[HomeName]
	,[AwayName]
	,[HomeScore]
	,[AwayScore]
	,[Intimacy]
	,[IsFriend]
	,[Status]
	,[RowTime]
) VALUES (
	@Idx,
    @HomeId
    ,@AwayId
    ,@HomeName
    ,@AwayName
    ,@HomeScore
    ,@AwayScore
    ,@Intimacy
    ,@IsFriend
    ,@Status
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

CREATE PROCEDURE [dbo].P_FriendMatch_Update
	@Idx uniqueidentifier, -- 比赛id
	@HomeId uniqueidentifier, -- 主队经理id
	@AwayId uniqueidentifier, -- 客队 id
	@HomeName nvarchar(50), -- 主队名
	@AwayName nvarchar(50), -- 客队名
	@HomeScore int, -- 主队比分
	@AwayScore int, -- 客队比分
	@Intimacy int, 
	@IsFriend bit, 
	@Status int, 
	@RowTime datetime -- 记录时间
AS



UPDATE [dbo].[Friend_Match] SET
	[HomeId] = @HomeId
	,[AwayId] = @AwayId
	,[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[HomeScore] = @HomeScore
	,[AwayScore] = @AwayScore
	,[Intimacy] = @Intimacy
	,[IsFriend] = @IsFriend
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



