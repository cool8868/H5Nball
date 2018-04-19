
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossladdermanager_Delete    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossladderManager_GetById    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossladderManager_GetAll    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossladderManager_Insert    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossladderManager_Update    Script Date: 2016年9月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossladderManager_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[CrossLadder_Manager]
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_CrossladderManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossladderManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossladderManager_Insert
	@DomainId int , 
	@Name nvarchar(50) , 
	@Logo varchar(50) , 
	@Kpi int , 
	@SiteId varchar(20) , 
	@SiteName nvarchar(50) , 
	@Score int , 
	@NewlyScore int , 
	@NewlyHonor int , 
	@Honor int , 
	@NewlyLadderCoin int , 
	@LadderCoin int , 
	@MaxScore int , 
	@MatchTime int , 
	@LastExchageTime datetime , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@DailyMaxScore int , 
	@DailyMaxAddScore int , 
	@Stamina int , 
	@StaminaBuy int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossLadder_Manager] (
	[ManagerId],
	[DomainId]
	,[Name]
	,[Logo]
	,[Kpi]
	,[SiteId]
	,[SiteName]
	,[Score]
	,[NewlyScore]
	,[NewlyHonor]
	,[Honor]
	,[NewlyLadderCoin]
	,[LadderCoin]
	,[MaxScore]
	,[MatchTime]
	,[LastExchageTime]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[DailyMaxScore]
	,[DailyMaxAddScore]
	,[Stamina]
	,[StaminaBuy]
) VALUES (
	@ManagerId,
    @DomainId
    ,@Name
    ,@Logo
    ,@Kpi
    ,@SiteId
    ,@SiteName
    ,@Score
    ,@NewlyScore
    ,@NewlyHonor
    ,@Honor
    ,@NewlyLadderCoin
    ,@LadderCoin
    ,@MaxScore
    ,@MatchTime
    ,@LastExchageTime
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@DailyMaxScore
    ,@DailyMaxAddScore
    ,@Stamina
    ,@StaminaBuy
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

CREATE PROCEDURE [dbo].P_CrossladderManager_Update
	@ManagerId uniqueidentifier, 
	@DomainId int, 
	@Name nvarchar(50), 
	@Logo varchar(50), 
	@Kpi int, 
	@SiteId varchar(20), 
	@SiteName nvarchar(50), 
	@Score int, -- 天梯积分
	@NewlyScore int, -- 最近增加积分
	@NewlyHonor int, -- 最近兑换荣誉数量
	@Honor int, -- 荣誉数量
	@NewlyLadderCoin int, 
	@LadderCoin int, 
	@MaxScore int, -- 最大积分
	@MatchTime int, -- 今日比赛场次
	@LastExchageTime datetime, -- 最近兑换时间
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@RowVersion timestamp, 
	@DailyMaxScore int, 
	@DailyMaxAddScore int, 
	@Stamina int, 
	@StaminaBuy int 
AS



UPDATE [dbo].[CrossLadder_Manager] SET
	[DomainId] = @DomainId
	,[Name] = @Name
	,[Logo] = @Logo
	,[Kpi] = @Kpi
	,[SiteId] = @SiteId
	,[SiteName] = @SiteName
	,[Score] = @Score
	,[NewlyScore] = @NewlyScore
	,[NewlyHonor] = @NewlyHonor
	,[Honor] = @Honor
	,[NewlyLadderCoin] = @NewlyLadderCoin
	,[LadderCoin] = @LadderCoin
	,[MaxScore] = @MaxScore
	,[MatchTime] = @MatchTime
	,[LastExchageTime] = @LastExchageTime
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[DailyMaxScore] = @DailyMaxScore
	,[DailyMaxAddScore] = @DailyMaxAddScore
	,[Stamina] = @Stamina
	,[StaminaBuy] = @StaminaBuy
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


