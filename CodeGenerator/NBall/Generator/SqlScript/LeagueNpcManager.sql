
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Leaguenpcmanager_Delete    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueNpcmanager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueNpcmanager_Delete]
GO

/****** Object:  Stored Procedure [dbo].LeagueNpcmanager_GetById    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueNpcmanager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueNpcmanager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LeagueNpcmanager_GetAll    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueNpcmanager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueNpcmanager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LeagueNpcmanager_Insert    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueNpcmanager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueNpcmanager_Insert]
GO

/****** Object:  Stored Procedure [dbo].LeagueNpcmanager_Update    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueNpcmanager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueNpcmanager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LeagueNpcmanager_Delete
	@Idx int
AS

DELETE FROM [dbo].[League_NpcManager]
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

CREATE PROCEDURE [dbo].P_LeagueNpcmanager_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_NpcManager] with(nolock)
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

CREATE PROCEDURE [dbo].P_LeagueNpcmanager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_NpcManager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LeagueNpcmanager_Insert
	@LaegueRecordId uniqueidentifier , 
	@NpcId uniqueidentifier , 
	@NpcName varchar(50) , 
	@Score int , 
	@MatchNumber int , 
	@WinNumber int , 
	@FlatNumber int , 
	@LoseNumber int , 
	@GoalsNumber int , 
	@FumbleNumber int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[League_NpcManager] (
	[LaegueRecordId]
	,[NpcId]
	,[NpcName]
	,[Score]
	,[MatchNumber]
	,[WinNumber]
	,[FlatNumber]
	,[LoseNumber]
	,[GoalsNumber]
	,[FumbleNumber]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @LaegueRecordId
    ,@NpcId
    ,@NpcName
    ,@Score
    ,@MatchNumber
    ,@WinNumber
    ,@FlatNumber
    ,@LoseNumber
    ,@GoalsNumber
    ,@FumbleNumber
    ,@UpdateTime
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_LeagueNpcmanager_Update
	@Idx int, 
	@LaegueRecordId uniqueidentifier, -- 联赛记录ID
	@NpcId uniqueidentifier, -- NPCId
	@NpcName varchar(50), -- NPC经理名
	@Score int, -- 获得的积分
	@MatchNumber int, -- 比赛次数
	@WinNumber int, -- 胜利次数
	@FlatNumber int, -- 打平次数
	@LoseNumber int, -- 打输次数
	@GoalsNumber int, -- 总进球数
	@FumbleNumber int, -- 总失球数
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[League_NpcManager] SET
	[LaegueRecordId] = @LaegueRecordId
	,[NpcId] = @NpcId
	,[NpcName] = @NpcName
	,[Score] = @Score
	,[MatchNumber] = @MatchNumber
	,[WinNumber] = @WinNumber
	,[FlatNumber] = @FlatNumber
	,[LoseNumber] = @LoseNumber
	,[GoalsNumber] = @GoalsNumber
	,[FumbleNumber] = @FumbleNumber
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



