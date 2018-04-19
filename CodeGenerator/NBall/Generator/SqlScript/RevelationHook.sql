
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationhook_Delete    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHook_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHook_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationHook_GetById    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHook_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHook_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationHook_GetAll    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHook_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHook_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationHook_Insert    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHook_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHook_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationHook_Update    Script Date: 2017年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationHook_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationHook_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationHook_Delete
	@HookId uniqueidentifier
AS

DELETE FROM [dbo].[Revelation_Hook]
WHERE
	[HookId] = @HookId

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

CREATE PROCEDURE [dbo].P_RevelationHook_GetById
	@HookId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Hook] with(nolock)
WHERE
	[HookId] = @HookId
	
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

CREATE PROCEDURE [dbo].P_RevelationHook_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Hook] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationHook_Insert
	@ManagerId uniqueidentifier , 
	@MarkId int , 
	@Schedule int , 
	@ScoreLog varchar(500) , 
	@ItemString varchar(500) , 
	@Status int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @HookId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_Hook] (
	[HookId],
	[ManagerId]
	,[MarkId]
	,[Schedule]
	,[ScoreLog]
	,[ItemString]
	,[Status]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@HookId,
    @ManagerId
    ,@MarkId
    ,@Schedule
    ,@ScoreLog
    ,@ItemString
    ,@Status
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

CREATE PROCEDURE [dbo].P_RevelationHook_Update
	@HookId uniqueidentifier, 
	@ManagerId uniqueidentifier, 
	@MarkId int, -- 关卡ID
	@Schedule int, -- 当前进度
	@ScoreLog varchar(500), -- 比分记录
	@ItemString varchar(500), -- 得到的物品
	@Status int, -- 状态
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Revelation_Hook] SET
	[ManagerId] = @ManagerId
	,[MarkId] = @MarkId
	,[Schedule] = @Schedule
	,[ScoreLog] = @ScoreLog
	,[ItemString] = @ItemString
	,[Status] = @Status
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[HookId] = @HookId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


