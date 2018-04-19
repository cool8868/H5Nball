
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Leaguerank_Delete    Script Date: 2016年1月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRank_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRank_Delete]
GO

/****** Object:  Stored Procedure [dbo].LeagueRank_GetById    Script Date: 2016年1月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRank_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRank_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LeagueRank_GetAll    Script Date: 2016年1月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRank_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRank_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LeagueRank_Insert    Script Date: 2016年1月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRank_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRank_Insert]
GO

/****** Object:  Stored Procedure [dbo].LeagueRank_Update    Script Date: 2016年1月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRank_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRank_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LeagueRank_Delete
	@Idx int
AS

DELETE FROM [dbo].[League_Rank]
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

CREATE PROCEDURE [dbo].P_LeagueRank_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_Rank] with(nolock)
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

CREATE PROCEDURE [dbo].P_LeagueRank_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_Rank] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LeagueRank_Insert
	@ManagerId uniqueidentifier , 
	@ManagerName nvarchar(50) , 
	@LeagueRecordId uniqueidentifier , 
	@LeagueRank int , 
	@Score int , 
	@Goal int , 
	@Lose int , 
	@WinCount int , 
	@DrawCount int , 
	@LostCount int , 
	@Status int , 
	@Rowtime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[League_Rank] (
	[ManagerId]
	,[ManagerName]
	,[LeagueRecordId]
	,[LeagueRank]
	,[Score]
	,[Goal]
	,[Lose]
	,[WinCount]
	,[DrawCount]
	,[LostCount]
	,[Status]
	,[Rowtime]
) VALUES (
    @ManagerId
    ,@ManagerName
    ,@LeagueRecordId
    ,@LeagueRank
    ,@Score
    ,@Goal
    ,@Lose
    ,@WinCount
    ,@DrawCount
    ,@LostCount
    ,@Status
    ,@Rowtime
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

CREATE PROCEDURE [dbo].P_LeagueRank_Update
	@Idx int, -- 标识号
	@ManagerId uniqueidentifier, -- 经理ID
	@ManagerName nvarchar(50), -- 经理名
	@LeagueRecordId uniqueidentifier, -- 联赛记录id
	@LeagueRank int, -- 联赛排名
	@Score int, -- 积分
	@Goal int, -- 进球数
	@Lose int, -- 失球数
	@WinCount int, -- 胜场
	@DrawCount int, -- 平场
	@LostCount int, -- 输场
	@Status int, -- 状态
	@Rowtime datetime -- 创建时间
AS



UPDATE [dbo].[League_Rank] SET
	[ManagerId] = @ManagerId
	,[ManagerName] = @ManagerName
	,[LeagueRecordId] = @LeagueRecordId
	,[LeagueRank] = @LeagueRank
	,[Score] = @Score
	,[Goal] = @Goal
	,[Lose] = @Lose
	,[WinCount] = @WinCount
	,[DrawCount] = @DrawCount
	,[LostCount] = @LostCount
	,[Status] = @Status
	,[Rowtime] = @Rowtime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



