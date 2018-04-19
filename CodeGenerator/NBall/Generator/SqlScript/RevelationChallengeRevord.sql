
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationchallengerevord_Delete    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationChallengerevord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationChallengerevord_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationChallengerevord_GetById    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationChallengerevord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationChallengerevord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationChallengerevord_GetAll    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationChallengerevord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationChallengerevord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationChallengerevord_Insert    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationChallengerevord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationChallengerevord_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationChallengerevord_Update    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationChallengerevord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationChallengerevord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationChallengerevord_Delete
	@GameId uniqueidentifier
AS

DELETE FROM [dbo].[Revelation_ChallengeRevord]
WHERE
	[GameId] = @GameId

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

CREATE PROCEDURE [dbo].P_RevelationChallengerevord_GetById
	@GameId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_ChallengeRevord] with(nolock)
WHERE
	[GameId] = @GameId
	
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

CREATE PROCEDURE [dbo].P_RevelationChallengerevord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_ChallengeRevord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationChallengerevord_Insert
	@ManagerId uniqueidentifier , 
	@Mark int , 
	@Schedule int , 
	@Goals int , 
	@ToConcede int , 
	@GameDate datetime , 
    @GameId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_ChallengeRevord] (
	[GameId],
	[ManagerId]
	,[Mark]
	,[Schedule]
	,[Goals]
	,[ToConcede]
	,[GameDate]
) VALUES (
	@GameId,
    @ManagerId
    ,@Mark
    ,@Schedule
    ,@Goals
    ,@ToConcede
    ,@GameDate
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

CREATE PROCEDURE [dbo].P_RevelationChallengerevord_Update
	@GameId uniqueidentifier, -- 游戏ID
	@ManagerId uniqueidentifier, -- 经理ID
	@Mark int, -- 球星关卡ID
	@Schedule int, -- 小关卡ID
	@Goals int, -- 进球数
	@ToConcede int, -- 失球数
	@GameDate datetime -- 游戏时间
AS



UPDATE [dbo].[Revelation_ChallengeRevord] SET
	[ManagerId] = @ManagerId
	,[Mark] = @Mark
	,[Schedule] = @Schedule
	,[Goals] = @Goals
	,[ToConcede] = @ToConcede
	,[GameDate] = @GameDate
WHERE
	[GameId] = @GameId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



