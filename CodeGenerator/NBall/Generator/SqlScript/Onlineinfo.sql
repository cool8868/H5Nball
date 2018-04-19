
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Onlineinfo_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].OnlineInfo_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].OnlineInfo_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].OnlineInfo_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].OnlineInfo_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_OnlineInfo_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Online_Info]
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

CREATE PROCEDURE [dbo].P_OnlineInfo_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Online_Info] with(nolock)
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

CREATE PROCEDURE [dbo].P_OnlineInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Online_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_OnlineInfo_Insert
	@ActiveFlag bit , 
	@ResetFlag bit , 
	@LoginTime datetime , 
	@GuildInTime datetime , 
	@ActiveTime datetime , 
	@TotalOnlineMinutes bigint , 
	@CntOnlineMinutes int , 
	@CurOnlineMinutes int , 
	@LoginIp varchar(50) , 
	@Status int , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Online_Info] (
	[ManagerId],
	[ActiveFlag]
	,[ResetFlag]
	,[LoginTime]
	,[GuildInTime]
	,[ActiveTime]
	,[TotalOnlineMinutes]
	,[CntOnlineMinutes]
	,[CurOnlineMinutes]
	,[LoginIp]
	,[Status]
	,[RowTime]
) VALUES (
	@ManagerId,
    @ActiveFlag
    ,@ResetFlag
    ,@LoginTime
    ,@GuildInTime
    ,@ActiveTime
    ,@TotalOnlineMinutes
    ,@CntOnlineMinutes
    ,@CurOnlineMinutes
    ,@LoginIp
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

CREATE PROCEDURE [dbo].P_OnlineInfo_Update
	@ManagerId uniqueidentifier, 
	@ActiveFlag bit, -- 在线标记:当前在线的为1
	@ResetFlag bit, -- 重置标记:当天上过线的为1
	@LoginTime datetime, -- 最近登录时间
	@GuildInTime datetime, 
	@ActiveTime datetime, -- 最后活跃时间
	@TotalOnlineMinutes bigint, -- 总计在线
	@CntOnlineMinutes int, -- 累计在线时间
	@CurOnlineMinutes int, -- 当前在线时间
	@LoginIp varchar(50), 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Online_Info] SET
	[ActiveFlag] = @ActiveFlag
	,[ResetFlag] = @ResetFlag
	,[LoginTime] = @LoginTime
	,[GuildInTime] = @GuildInTime
	,[ActiveTime] = @ActiveTime
	,[TotalOnlineMinutes] = @TotalOnlineMinutes
	,[CntOnlineMinutes] = @CntOnlineMinutes
	,[CurOnlineMinutes] = @CurOnlineMinutes
	,[LoginIp] = @LoginIp
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



