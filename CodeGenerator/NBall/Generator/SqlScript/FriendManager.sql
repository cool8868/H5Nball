
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Friendmanager_Delete    Script Date: 2016年6月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].FriendManager_GetById    Script Date: 2016年6月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].FriendManager_GetAll    Script Date: 2016年6月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].FriendManager_Insert    Script Date: 2016年6月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].FriendManager_Update    Script Date: 2016年6月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_FriendManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_FriendManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_FriendManager_Delete
	@Idx int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Friend_Manager]
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_FriendManager_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Friend_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_FriendManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Friend_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_FriendManager_Insert
	@ManagerId uniqueidentifier , 
	@FriendId uniqueidentifier , 
	@Intimacy int , 
	@DayIntimacy int , 
	@HelpTrainCount int , 
	@DayHelpTrainCount int , 
	@DayOpenBoxCount int , 
	@MatchCount int , 
	@DayMatchCount int , 
	@RecordDate datetime , 
	@Status int , 
	@RowTime datetime , 
	@OpenBoxTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Friend_Manager] (
	[ManagerId]
	,[FriendId]
	,[Intimacy]
	,[DayIntimacy]
	,[HelpTrainCount]
	,[DayHelpTrainCount]
	,[DayOpenBoxCount]
	,[MatchCount]
	,[DayMatchCount]
	,[RecordDate]
	,[Status]
	,[RowTime]
	,[OpenBoxTime]
) VALUES (
    @ManagerId
    ,@FriendId
    ,@Intimacy
    ,@DayIntimacy
    ,@HelpTrainCount
    ,@DayHelpTrainCount
    ,@DayOpenBoxCount
    ,@MatchCount
    ,@DayMatchCount
    ,@RecordDate
    ,@Status
    ,@RowTime
    ,@OpenBoxTime
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

CREATE PROCEDURE [dbo].P_FriendManager_Update
	@Idx int, 
	@ManagerId uniqueidentifier, -- 经理id
	@FriendId uniqueidentifier, -- 好友id
	@Intimacy int, -- 亲密度
	@DayIntimacy int, -- 今日增加亲密度
	@HelpTrainCount int, -- 帮助好友训练次数
	@DayHelpTrainCount int, -- 今天帮助好友训练次数
	@DayOpenBoxCount int, 
	@MatchCount int, -- 好友对战次数
	@DayMatchCount int, -- 今天好友对战次数
	@RecordDate datetime, 
	@Status int, -- 状态,0:表示好友，1：表示黑名单，2：表示被对方加入黑名单
	@RowTime datetime, -- 时间
	@RowVersion timestamp, -- 时间戳
	@OpenBoxTime datetime 
AS



UPDATE [dbo].[Friend_Manager] SET
	[ManagerId] = @ManagerId
	,[FriendId] = @FriendId
	,[Intimacy] = @Intimacy
	,[DayIntimacy] = @DayIntimacy
	,[HelpTrainCount] = @HelpTrainCount
	,[DayHelpTrainCount] = @DayHelpTrainCount
	,[DayOpenBoxCount] = @DayOpenBoxCount
	,[MatchCount] = @MatchCount
	,[DayMatchCount] = @DayMatchCount
	,[RecordDate] = @RecordDate
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[OpenBoxTime] = @OpenBoxTime
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



