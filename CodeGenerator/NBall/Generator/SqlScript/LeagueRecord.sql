
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Leaguerecord_Delete    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].LeagueRecord_GetById    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LeagueRecord_GetAll    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LeagueRecord_Insert    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].LeagueRecord_Update    Script Date: 2016年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LeagueRecord_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[League_Record]
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

CREATE PROCEDURE [dbo].P_LeagueRecord_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_LeagueRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LeagueRecord_Insert
	@ManagerId uniqueidentifier , 
	@LaegueId int , 
	@Schedule int , 
	@Score int , 
	@Rank int , 
	@IsSend bit , 
	@UpdateTime datetime , 
	@RowTime datetime , 
	@PrizeTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[League_Record] (
	[Idx],
	[ManagerId]
	,[LaegueId]
	,[Schedule]
	,[Score]
	,[Rank]
	,[IsSend]
	,[UpdateTime]
	,[RowTime]
	,[PrizeTime]
) VALUES (
	@Idx,
    @ManagerId
    ,@LaegueId
    ,@Schedule
    ,@Score
    ,@Rank
    ,@IsSend
    ,@UpdateTime
    ,@RowTime
    ,@PrizeTime
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

CREATE PROCEDURE [dbo].P_LeagueRecord_Update
	@Idx uniqueidentifier, 
	@ManagerId uniqueidentifier, 
	@LaegueId int, -- 联赛ID
	@Schedule int, -- 进度
	@Score int, -- 获得积分，用于查看记录
	@Rank int, -- 排名，用户查看记录
	@IsSend bit, -- 是否发奖
	@UpdateTime datetime, 
	@RowTime datetime, 
	@PrizeTime datetime -- 通关奖励领取时间
AS



UPDATE [dbo].[League_Record] SET
	[ManagerId] = @ManagerId
	,[LaegueId] = @LaegueId
	,[Schedule] = @Schedule
	,[Score] = @Score
	,[Rank] = @Rank
	,[IsSend] = @IsSend
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
	,[PrizeTime] = @PrizeTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



