
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Laddermatch_Delete    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderMatch_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderMatch_Delete]
GO

/****** Object:  Stored Procedure [dbo].LadderMatch_GetById    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderMatch_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderMatch_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LadderMatch_GetAll    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderMatch_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderMatch_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LadderMatch_Insert    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderMatch_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderMatch_Insert]
GO

/****** Object:  Stored Procedure [dbo].LadderMatch_Update    Script Date: 2016年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderMatch_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderMatch_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LadderMatch_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Ladder_Match]
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

CREATE PROCEDURE [dbo].P_LadderMatch_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_Match] with(nolock)
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

CREATE PROCEDURE [dbo].P_LadderMatch_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_Match] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LadderMatch_Insert
	@LadderId uniqueidentifier , 
	@HomeId uniqueidentifier , 
	@AwayId uniqueidentifier , 
	@HomeName varchar(50) , 
	@AwayName varchar(50) , 
	@HomeLadderScore int , 
	@AwayLadderScore int , 
	@HomeScore int , 
	@AwayScore int , 
	@HomeIsBot bit , 
	@AwayIsBot bit , 
	@GroupIndex int , 
	@PrizeHomeScore int , 
	@PrizeAwayScore int , 
	@Status int , 
	@RowTime datetime , 
	@HomeCoin int , 
	@HomeExp int , 
	@AwayCoin int , 
	@AwayExp int , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Ladder_Match] (
	[Idx],
	[LadderId]
	,[HomeId]
	,[AwayId]
	,[HomeName]
	,[AwayName]
	,[HomeLadderScore]
	,[AwayLadderScore]
	,[HomeScore]
	,[AwayScore]
	,[HomeIsBot]
	,[AwayIsBot]
	,[GroupIndex]
	,[PrizeHomeScore]
	,[PrizeAwayScore]
	,[Status]
	,[RowTime]
	,[HomeCoin]
	,[HomeExp]
	,[AwayCoin]
	,[AwayExp]
) VALUES (
	@Idx,
    @LadderId
    ,@HomeId
    ,@AwayId
    ,@HomeName
    ,@AwayName
    ,@HomeLadderScore
    ,@AwayLadderScore
    ,@HomeScore
    ,@AwayScore
    ,@HomeIsBot
    ,@AwayIsBot
    ,@GroupIndex
    ,@PrizeHomeScore
    ,@PrizeAwayScore
    ,@Status
    ,@RowTime
    ,@HomeCoin
    ,@HomeExp
    ,@AwayCoin
    ,@AwayExp
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

CREATE PROCEDURE [dbo].P_LadderMatch_Update
	@Idx uniqueidentifier, 
	@LadderId uniqueidentifier, -- 天梯id
	@HomeId uniqueidentifier, -- 主队经理
	@AwayId uniqueidentifier, -- 客队经理
	@HomeName varchar(50), -- 主队名
	@AwayName varchar(50), -- 客队名
	@HomeLadderScore int, -- 主队天梯积分
	@AwayLadderScore int, -- 客队天梯积分
	@HomeScore int, -- 主队比分
	@AwayScore int, -- 客队比分
	@HomeIsBot bit, 
	@AwayIsBot bit, 
	@GroupIndex int, -- 所属分组
	@PrizeHomeScore int, -- 奖励主队积分
	@PrizeAwayScore int, -- 奖励客队积分
	@Status int, 
	@RowTime datetime, 
	@HomeCoin int, 
	@HomeExp int, 
	@AwayCoin int, 
	@AwayExp int 
AS



UPDATE [dbo].[Ladder_Match] SET
	[LadderId] = @LadderId
	,[HomeId] = @HomeId
	,[AwayId] = @AwayId
	,[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[HomeLadderScore] = @HomeLadderScore
	,[AwayLadderScore] = @AwayLadderScore
	,[HomeScore] = @HomeScore
	,[AwayScore] = @AwayScore
	,[HomeIsBot] = @HomeIsBot
	,[AwayIsBot] = @AwayIsBot
	,[GroupIndex] = @GroupIndex
	,[PrizeHomeScore] = @PrizeHomeScore
	,[PrizeAwayScore] = @PrizeAwayScore
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[HomeCoin] = @HomeCoin
	,[HomeExp] = @HomeExp
	,[AwayCoin] = @AwayCoin
	,[AwayExp] = @AwayExp
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



