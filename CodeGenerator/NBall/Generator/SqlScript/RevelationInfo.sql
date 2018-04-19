
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationinfo_Delete    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationInfo_GetById    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationInfo_GetAll    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationInfo_Insert    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationInfo_Update    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationInfo_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Revelation_Info]
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

CREATE PROCEDURE [dbo].P_RevelationInfo_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Info] with(nolock)
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

CREATE PROCEDURE [dbo].P_RevelationInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationInfo_Insert
	@Courage int , 
	@LockMark int , 
	@PresentMark int , 
	@Schedule int , 
	@IsGeneralMark bit , 
	@PassLog varchar(200) , 
	@FirstPassLog varchar(200) , 
	@DayMatchLog varchar(200) , 
	@Morale int , 
	@IsHaveDraw bit , 
	@DrawId uniqueidentifier , 
	@IsHook bit , 
	@HookId uniqueidentifier , 
	@RefreshData date , 
	@Status int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_Info] (
	[ManagerId],
	[Courage]
	,[LockMark]
	,[PresentMark]
	,[Schedule]
	,[IsGeneralMark]
	,[PassLog]
	,[FirstPassLog]
	,[DayMatchLog]
	,[Morale]
	,[IsHaveDraw]
	,[DrawId]
	,[IsHook]
	,[HookId]
	,[RefreshData]
	,[Status]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @Courage
    ,@LockMark
    ,@PresentMark
    ,@Schedule
    ,@IsGeneralMark
    ,@PassLog
    ,@FirstPassLog
    ,@DayMatchLog
    ,@Morale
    ,@IsHaveDraw
    ,@DrawId
    ,@IsHook
    ,@HookId
    ,@RefreshData
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

CREATE PROCEDURE [dbo].P_RevelationInfo_Update
	@ManagerId uniqueidentifier, 
	@Courage int, -- 勇气值
	@LockMark int, -- 解锁到了哪个关卡
	@PresentMark int, -- 当前比赛的关卡
	@Schedule int, -- 当前的小关卡
	@IsGeneralMark bit, -- 是否通过关卡
	@PassLog varchar(200), -- 通关记录
	@FirstPassLog varchar(200), -- 首次通关记录
	@DayMatchLog varchar(200), -- 当天的通关记录
	@Morale int, -- 士气
	@IsHaveDraw bit, -- 是否有翻牌
	@DrawId uniqueidentifier, -- 翻牌记录ID
	@IsHook bit, -- 是否有挂机
	@HookId uniqueidentifier, -- 挂机ID
	@RefreshData date, -- 刷新的日期
	@Status int, 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Revelation_Info] SET
	[Courage] = @Courage
	,[LockMark] = @LockMark
	,[PresentMark] = @PresentMark
	,[Schedule] = @Schedule
	,[IsGeneralMark] = @IsGeneralMark
	,[PassLog] = @PassLog
	,[FirstPassLog] = @FirstPassLog
	,[DayMatchLog] = @DayMatchLog
	,[Morale] = @Morale
	,[IsHaveDraw] = @IsHaveDraw
	,[DrawId] = @DrawId
	,[IsHook] = @IsHook
	,[HookId] = @HookId
	,[RefreshData] = @RefreshData
	,[Status] = @Status
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


