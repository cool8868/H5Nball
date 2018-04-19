
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Arenaseasoninfo_Delete    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeasoninfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeasoninfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].ArenaSeasoninfo_GetById    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeasoninfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeasoninfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ArenaSeasoninfo_GetAll    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeasoninfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeasoninfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ArenaSeasoninfo_Insert    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeasoninfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeasoninfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].ArenaSeasoninfo_Update    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeasoninfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeasoninfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ArenaSeasoninfo_Delete
	@Idx int
AS

DELETE FROM [dbo].[Arena_SeasonInfo]
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

CREATE PROCEDURE [dbo].P_ArenaSeasoninfo_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_SeasonInfo] with(nolock)
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

CREATE PROCEDURE [dbo].P_ArenaSeasoninfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_SeasonInfo] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ArenaSeasoninfo_Insert
	@PrepareTime datetime , 
	@StartTime datetime , 
	@EndTime datetime , 
	@ArenaType int , 
	@Status int , 
	@IsPrize bit , 
	@PrizeTime datetime , 
	@OnChampionId uniqueidentifier , 
	@OnChampionName varchar(50) , 
	@OnChampionZoneName varchar(50) , 
	@TheKingId uniqueidentifier , 
	@TheKingName varchar(50) , 
	@TheKingZoneName varchar(50) , 
	@TheKingChampionNumber int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
	@DomainId int , 
	@SeasonId int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Arena_SeasonInfo] (
	[PrepareTime]
	,[StartTime]
	,[EndTime]
	,[ArenaType]
	,[Status]
	,[IsPrize]
	,[PrizeTime]
	,[OnChampionId]
	,[OnChampionName]
	,[OnChampionZoneName]
	,[TheKingId]
	,[TheKingName]
	,[TheKingZoneName]
	,[TheKingChampionNumber]
	,[UpdateTime]
	,[RowTime]
	,[DomainId]
	,[SeasonId]
) VALUES (
    @PrepareTime
    ,@StartTime
    ,@EndTime
    ,@ArenaType
    ,@Status
    ,@IsPrize
    ,@PrizeTime
    ,@OnChampionId
    ,@OnChampionName
    ,@OnChampionZoneName
    ,@TheKingId
    ,@TheKingName
    ,@TheKingZoneName
    ,@TheKingChampionNumber
    ,@UpdateTime
    ,@RowTime
    ,@DomainId
    ,@SeasonId
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

CREATE PROCEDURE [dbo].P_ArenaSeasoninfo_Update
	@Idx int, -- 赛季ID
	@PrepareTime datetime, 
	@StartTime datetime, 
	@EndTime datetime, 
	@ArenaType int, 
	@Status int, -- 竞技场状态  0=准备中 1=比赛中 2=结束
	@IsPrize bit, 
	@PrizeTime datetime, 
	@OnChampionId uniqueidentifier, -- 上届冠军ID
	@OnChampionName varchar(50), -- 上届冠军名字
	@OnChampionZoneName varchar(50), -- 上届冠军所在区
	@TheKingId uniqueidentifier, -- 王者之师经理ID
	@TheKingName varchar(50), -- 王者之师名字
	@TheKingZoneName varchar(50), -- 王者之师所在区
	@TheKingChampionNumber int, -- 王者之师冠军次数
	@UpdateTime datetime, 
	@RowTime datetime, 
	@DomainId int, 
	@SeasonId int 
AS



UPDATE [dbo].[Arena_SeasonInfo] SET
	[PrepareTime] = @PrepareTime
	,[StartTime] = @StartTime
	,[EndTime] = @EndTime
	,[ArenaType] = @ArenaType
	,[Status] = @Status
	,[IsPrize] = @IsPrize
	,[PrizeTime] = @PrizeTime
	,[OnChampionId] = @OnChampionId
	,[OnChampionName] = @OnChampionName
	,[OnChampionZoneName] = @OnChampionZoneName
	,[TheKingId] = @TheKingId
	,[TheKingName] = @TheKingName
	,[TheKingZoneName] = @TheKingZoneName
	,[TheKingChampionNumber] = @TheKingChampionNumber
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
	,[DomainId] = @DomainId
	,[SeasonId] = @SeasonId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


