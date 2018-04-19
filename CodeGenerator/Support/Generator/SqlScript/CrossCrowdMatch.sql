
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crosscrowdmatch_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMatch_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMatch_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdMatch_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMatch_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMatch_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrosscrowdMatch_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMatch_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMatch_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrosscrowdMatch_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMatch_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMatch_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdMatch_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMatch_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMatch_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrosscrowdMatch_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[CrossCrowd_Match]
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

CREATE PROCEDURE [dbo].P_CrosscrowdMatch_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_Match] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrosscrowdMatch_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_Match] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrosscrowdMatch_Insert
	@CrossCrowdId int , 
	@PairIndex int , 
	@HomeSiteId varchar(20) , 
	@AwaySiteId varchar(20) , 
	@HomeId uniqueidentifier , 
	@AwayId uniqueidentifier , 
	@HomeName nvarchar(50) , 
	@AwayName nvarchar(50) , 
	@HomeScore int , 
	@AwayScore int , 
	@HomePrizeCoin int , 
	@HomePrizeHonor int , 
	@HomeMorale int , 
	@HomeCostMorale int , 
	@HomePrizeScore int , 
	@AwayPrizeCoin int , 
	@AwayPrizeHonor int , 
	@AwayMorale int , 
	@AwayCostMorale int , 
	@AwayPrizeScore int , 
	@IsKill bit , 
	@Status int , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossCrowd_Match] (
	[Idx],
	[CrossCrowdId]
	,[PairIndex]
	,[HomeSiteId]
	,[AwaySiteId]
	,[HomeId]
	,[AwayId]
	,[HomeName]
	,[AwayName]
	,[HomeScore]
	,[AwayScore]
	,[HomePrizeCoin]
	,[HomePrizeHonor]
	,[HomeMorale]
	,[HomeCostMorale]
	,[HomePrizeScore]
	,[AwayPrizeCoin]
	,[AwayPrizeHonor]
	,[AwayMorale]
	,[AwayCostMorale]
	,[AwayPrizeScore]
	,[IsKill]
	,[Status]
	,[RowTime]
) VALUES (
	@Idx,
    @CrossCrowdId
    ,@PairIndex
    ,@HomeSiteId
    ,@AwaySiteId
    ,@HomeId
    ,@AwayId
    ,@HomeName
    ,@AwayName
    ,@HomeScore
    ,@AwayScore
    ,@HomePrizeCoin
    ,@HomePrizeHonor
    ,@HomeMorale
    ,@HomeCostMorale
    ,@HomePrizeScore
    ,@AwayPrizeCoin
    ,@AwayPrizeHonor
    ,@AwayMorale
    ,@AwayCostMorale
    ,@AwayPrizeScore
    ,@IsKill
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

CREATE PROCEDURE [dbo].P_CrosscrowdMatch_Update
	@Idx uniqueidentifier, 
	@CrossCrowdId int, 
	@PairIndex int, 
	@HomeSiteId varchar(20), 
	@AwaySiteId varchar(20), 
	@HomeId uniqueidentifier, 
	@AwayId uniqueidentifier, 
	@HomeName nvarchar(50), 
	@AwayName nvarchar(50), 
	@HomeScore int, 
	@AwayScore int, 
	@HomePrizeCoin int, 
	@HomePrizeHonor int, 
	@HomeMorale int, 
	@HomeCostMorale int, 
	@HomePrizeScore int, 
	@AwayPrizeCoin int, 
	@AwayPrizeHonor int, 
	@AwayMorale int, 
	@AwayCostMorale int, 
	@AwayPrizeScore int, 
	@IsKill bit, 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[CrossCrowd_Match] SET
	[CrossCrowdId] = @CrossCrowdId
	,[PairIndex] = @PairIndex
	,[HomeSiteId] = @HomeSiteId
	,[AwaySiteId] = @AwaySiteId
	,[HomeId] = @HomeId
	,[AwayId] = @AwayId
	,[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[HomeScore] = @HomeScore
	,[AwayScore] = @AwayScore
	,[HomePrizeCoin] = @HomePrizeCoin
	,[HomePrizeHonor] = @HomePrizeHonor
	,[HomeMorale] = @HomeMorale
	,[HomeCostMorale] = @HomeCostMorale
	,[HomePrizeScore] = @HomePrizeScore
	,[AwayPrizeCoin] = @AwayPrizeCoin
	,[AwayPrizeHonor] = @AwayPrizeHonor
	,[AwayMorale] = @AwayMorale
	,[AwayCostMorale] = @AwayCostMorale
	,[AwayPrizeScore] = @AwayPrizeScore
	,[IsKill] = @IsKill
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


