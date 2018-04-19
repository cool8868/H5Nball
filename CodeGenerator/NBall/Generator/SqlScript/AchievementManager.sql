
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Achievementmanager_Delete    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AchievementManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AchievementManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].AchievementManager_GetById    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AchievementManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AchievementManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AchievementManager_GetAll    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AchievementManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AchievementManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AchievementManager_Insert    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AchievementManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AchievementManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].AchievementManager_Update    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AchievementManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AchievementManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AchievementManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Achievement_Manager]
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

CREATE PROCEDURE [dbo].P_AchievementManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Achievement_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_AchievementManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Achievement_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AchievementManager_Insert
	@PurpleCardCount int , 
	@OrangeCardCount int , 
	@SilverCardCount int , 
	@GoldCardCount int , 
	@MaxLadderGoals int , 
	@MaxPkMatchGoals int , 
	@DayPkMatchGoals int , 
	@DayPkMatchDate datetime , 
	@MaxDayPkMatchGoals int , 
	@MaxLadderWin int , 
	@LadderWin int , 
	@LadderSeason int , 
	@FriendWinComb int , 
	@MaxFriendWinComb int , 
	@MaxDailyCupRank int , 
	@Level5CardCount int , 
	@Level10CardCount int , 
	@Level20CardCount int , 
	@Level30CardCount int , 
	@LeagueScore1 int , 
	@LeagueScore2 int , 
	@LeagueScore3 int , 
	@LeagueScore4 int , 
	@LeagueScore5 int , 
	@LeagueScore6 int , 
	@LeagueScore7 int , 
	@LeagueScore8 int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Achievement_Manager] (
	[ManagerId],
	[PurpleCardCount]
	,[OrangeCardCount]
	,[SilverCardCount]
	,[GoldCardCount]
	,[MaxLadderGoals]
	,[MaxPkMatchGoals]
	,[DayPkMatchGoals]
	,[DayPkMatchDate]
	,[MaxDayPkMatchGoals]
	,[MaxLadderWin]
	,[LadderWin]
	,[LadderSeason]
	,[FriendWinComb]
	,[MaxFriendWinComb]
	,[MaxDailyCupRank]
	,[Level5CardCount]
	,[Level10CardCount]
	,[Level20CardCount]
	,[Level30CardCount]
	,[LeagueScore1]
	,[LeagueScore2]
	,[LeagueScore3]
	,[LeagueScore4]
	,[LeagueScore5]
	,[LeagueScore6]
	,[LeagueScore7]
	,[LeagueScore8]
) VALUES (
	@ManagerId,
    @PurpleCardCount
    ,@OrangeCardCount
    ,@SilverCardCount
    ,@GoldCardCount
    ,@MaxLadderGoals
    ,@MaxPkMatchGoals
    ,@DayPkMatchGoals
    ,@DayPkMatchDate
    ,@MaxDayPkMatchGoals
    ,@MaxLadderWin
    ,@LadderWin
    ,@LadderSeason
    ,@FriendWinComb
    ,@MaxFriendWinComb
    ,@MaxDailyCupRank
    ,@Level5CardCount
    ,@Level10CardCount
    ,@Level20CardCount
    ,@Level30CardCount
    ,@LeagueScore1
    ,@LeagueScore2
    ,@LeagueScore3
    ,@LeagueScore4
    ,@LeagueScore5
    ,@LeagueScore6
    ,@LeagueScore7
    ,@LeagueScore8
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

CREATE PROCEDURE [dbo].P_AchievementManager_Update
	@ManagerId uniqueidentifier, 
	@PurpleCardCount int, -- 紫卡数量
	@OrangeCardCount int, -- 橙卡数量
	@SilverCardCount int, -- 银卡数量
	@GoldCardCount int, -- 金卡数量
	@MaxLadderGoals int, -- 单场天梯赛最多进球
	@MaxPkMatchGoals int, -- 单场PK赛最多进球
	@DayPkMatchGoals int, -- 单日Pk赛进球数
	@DayPkMatchDate datetime, -- PK赛统计日期
	@MaxDayPkMatchGoals int, -- 单日PK赛最高进球总数
	@MaxLadderWin int, -- 单赛季天梯赛最高胜场
	@LadderWin int, -- 单赛季天梯赛胜场
	@LadderSeason int, -- 天梯赛赛季
	@FriendWinComb int, -- 好友赛当前连胜场次
	@MaxFriendWinComb int, -- 好友赛最多连胜
	@MaxDailyCupRank int, --  
	@Level5CardCount int, 
	@Level10CardCount int, 
	@Level20CardCount int, 
	@Level30CardCount int, 
	@LeagueScore1 int, -- 联赛1冠军积分 
	@LeagueScore2 int, -- 联赛2冠军积分 
	@LeagueScore3 int, -- 联赛3冠军积分 
	@LeagueScore4 int, -- 联赛4冠军积分 
	@LeagueScore5 int, -- 联赛5冠军积分 
	@LeagueScore6 int, -- 联赛6冠军积分 
	@LeagueScore7 int, -- 联赛7冠军积分 
	@LeagueScore8 int -- 联赛8冠军积分 
AS



UPDATE [dbo].[Achievement_Manager] SET
	[PurpleCardCount] = @PurpleCardCount
	,[OrangeCardCount] = @OrangeCardCount
	,[SilverCardCount] = @SilverCardCount
	,[GoldCardCount] = @GoldCardCount
	,[MaxLadderGoals] = @MaxLadderGoals
	,[MaxPkMatchGoals] = @MaxPkMatchGoals
	,[DayPkMatchGoals] = @DayPkMatchGoals
	,[DayPkMatchDate] = @DayPkMatchDate
	,[MaxDayPkMatchGoals] = @MaxDayPkMatchGoals
	,[MaxLadderWin] = @MaxLadderWin
	,[LadderWin] = @LadderWin
	,[LadderSeason] = @LadderSeason
	,[FriendWinComb] = @FriendWinComb
	,[MaxFriendWinComb] = @MaxFriendWinComb
	,[MaxDailyCupRank] = @MaxDailyCupRank
	,[Level5CardCount] = @Level5CardCount
	,[Level10CardCount] = @Level10CardCount
	,[Level20CardCount] = @Level20CardCount
	,[Level30CardCount] = @Level30CardCount
	,[LeagueScore1] = @LeagueScore1
	,[LeagueScore2] = @LeagueScore2
	,[LeagueScore3] = @LeagueScore3
	,[LeagueScore4] = @LeagueScore4
	,[LeagueScore5] = @LeagueScore5
	,[LeagueScore6] = @LeagueScore6
	,[LeagueScore7] = @LeagueScore7
	,[LeagueScore8] = @LeagueScore8
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



