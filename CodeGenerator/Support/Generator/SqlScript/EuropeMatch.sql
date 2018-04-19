
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Europematch_Delete    Script Date: 2016年8月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeMatch_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeMatch_Delete]
GO

/****** Object:  Stored Procedure [dbo].EuropeMatch_GetById    Script Date: 2016年8月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeMatch_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeMatch_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].EuropeMatch_GetAll    Script Date: 2016年8月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeMatch_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeMatch_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].EuropeMatch_Insert    Script Date: 2016年8月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeMatch_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeMatch_Insert]
GO

/****** Object:  Stored Procedure [dbo].EuropeMatch_Update    Script Date: 2016年8月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeMatch_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeMatch_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_EuropeMatch_Delete
	@MatchId int
AS

DELETE FROM [dbo].[Europe_Match]
WHERE
	[MatchId] = @MatchId

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

CREATE PROCEDURE [dbo].P_EuropeMatch_GetById
	@MatchId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_Match] with(nolock)
WHERE
	[MatchId] = @MatchId
	
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

CREATE PROCEDURE [dbo].P_EuropeMatch_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_Match] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_EuropeMatch_Insert
	@HomeName varchar(50) , 
	@AwayName varchar(50) , 
	@MatchDate date , 
	@MatchTime datetime , 
	@HomeGoals int , 
	@AwayGoals int , 
	@ResultType int , 
	@States int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @MatchId int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Europe_Match] (
	[HomeName]
	,[AwayName]
	,[MatchDate]
	,[MatchTime]
	,[HomeGoals]
	,[AwayGoals]
	,[ResultType]
	,[States]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @HomeName
    ,@AwayName
    ,@MatchDate
    ,@MatchTime
    ,@HomeGoals
    ,@AwayGoals
    ,@ResultType
    ,@States
    ,@UpdateTime
    ,@RowTime
)


SET @MatchId = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_EuropeMatch_Update
	@MatchId int, 
	@HomeName varchar(50), 
	@AwayName varchar(50), 
	@MatchDate date, -- 比赛日期
	@MatchTime datetime, -- 比赛时间
	@HomeGoals int, 
	@AwayGoals int, 
	@ResultType int, -- 比赛结果类型 1主胜 2平  3客胜
	@States int, -- 状态  0初始  1可竞猜 2比赛中 4发奖完成
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Europe_Match] SET
	[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[MatchDate] = @MatchDate
	,[MatchTime] = @MatchTime
	,[HomeGoals] = @HomeGoals
	,[AwayGoals] = @AwayGoals
	,[ResultType] = @ResultType
	,[States] = @States
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[MatchId] = @MatchId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


