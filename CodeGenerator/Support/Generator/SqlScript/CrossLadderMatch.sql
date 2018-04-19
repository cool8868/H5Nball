
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossladdermatch_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderMatch_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderMatch_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossladderMatch_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderMatch_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderMatch_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossladderMatch_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderMatch_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderMatch_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossladderMatch_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderMatch_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderMatch_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossladderMatch_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderMatch_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderMatch_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossladderMatch_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[CrossLadder_Match]
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

CREATE PROCEDURE [dbo].P_CrossladderMatch_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_Match] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossladderMatch_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_Match] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossladderMatch_Insert
	@DomainId int , 
	@LadderId uniqueidentifier , 
	@HomeId uniqueidentifier , 
	@AwayId uniqueidentifier , 
	@HomeName nvarchar(50) , 
	@AwayName nvarchar(50) , 
	@HomeLogo varchar(50) , 
	@AwayLogo varchar(50) , 
	@HomeSiteId varchar(20) , 
	@AwaySiteId varchar(20) , 
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


INSERT INTO [dbo].[CrossLadder_Match] (
	[Idx],
	[DomainId]
	,[LadderId]
	,[HomeId]
	,[AwayId]
	,[HomeName]
	,[AwayName]
	,[HomeLogo]
	,[AwayLogo]
	,[HomeSiteId]
	,[AwaySiteId]
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
    @DomainId
    ,@LadderId
    ,@HomeId
    ,@AwayId
    ,@HomeName
    ,@AwayName
    ,@HomeLogo
    ,@AwayLogo
    ,@HomeSiteId
    ,@AwaySiteId
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

CREATE PROCEDURE [dbo].P_CrossladderMatch_Update
	@Idx uniqueidentifier, 
	@DomainId int, 
	@LadderId uniqueidentifier, -- 天梯id
	@HomeId uniqueidentifier, -- 主队经理
	@AwayId uniqueidentifier, -- 客队经理
	@HomeName nvarchar(50), -- 主队名
	@AwayName nvarchar(50), -- 客队名
	@HomeLogo varchar(50), 
	@AwayLogo varchar(50), 
	@HomeSiteId varchar(20), 
	@AwaySiteId varchar(20), 
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



UPDATE [dbo].[CrossLadder_Match] SET
	[DomainId] = @DomainId
	,[LadderId] = @LadderId
	,[HomeId] = @HomeId
	,[AwayId] = @AwayId
	,[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[HomeLogo] = @HomeLogo
	,[AwayLogo] = @AwayLogo
	,[HomeSiteId] = @HomeSiteId
	,[AwaySiteId] = @AwaySiteId
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


