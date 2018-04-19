
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crosscrowdmanager_Delete    Script Date: 2016年8月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdManager_GetById    Script Date: 2016年8月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrosscrowdManager_GetAll    Script Date: 2016年8月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrosscrowdManager_Insert    Script Date: 2016年8月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdManager_Update    Script Date: 2016年8月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrosscrowdManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[CrossCrowd_Manager]
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

CREATE PROCEDURE [dbo].P_CrosscrowdManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrosscrowdManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrosscrowdManager_Insert
	@DomainId int , 
	@SiteId varchar(20) , 
	@SiteName nvarchar(20) , 
	@Name nvarchar(50) , 
	@Logo varchar(50) , 
	@CrossCrowdId int , 
	@Morale int , 
	@Score int , 
	@ScoreUpdateTime datetime , 
	@KillNumber int , 
	@ByKillNumber int , 
	@NextMatchTime datetime , 
	@ClearCdCount int , 
	@ResurrectionTime datetime , 
	@ResurrectionCount int , 
	@ResurrectionAuto int , 
	@WinningCount int , 
	@Kpi int , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossCrowd_Manager] (
	[ManagerId],
	[DomainId]
	,[SiteId]
	,[SiteName]
	,[Name]
	,[Logo]
	,[CrossCrowdId]
	,[Morale]
	,[Score]
	,[ScoreUpdateTime]
	,[KillNumber]
	,[ByKillNumber]
	,[NextMatchTime]
	,[ClearCdCount]
	,[ResurrectionTime]
	,[ResurrectionCount]
	,[ResurrectionAuto]
	,[WinningCount]
	,[Kpi]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
	@ManagerId,
    @DomainId
    ,@SiteId
    ,@SiteName
    ,@Name
    ,@Logo
    ,@CrossCrowdId
    ,@Morale
    ,@Score
    ,@ScoreUpdateTime
    ,@KillNumber
    ,@ByKillNumber
    ,@NextMatchTime
    ,@ClearCdCount
    ,@ResurrectionTime
    ,@ResurrectionCount
    ,@ResurrectionAuto
    ,@WinningCount
    ,@Kpi
    ,@Status
    ,@RowTime
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_CrosscrowdManager_Update
	@ManagerId uniqueidentifier, 
	@DomainId int, 
	@SiteId varchar(20), 
	@SiteName nvarchar(20), 
	@Name nvarchar(50), 
	@Logo varchar(50), 
	@CrossCrowdId int, 
	@Morale int, 
	@Score int, 
	@ScoreUpdateTime datetime, 
	@KillNumber int, 
	@ByKillNumber int, 
	@NextMatchTime datetime, 
	@ClearCdCount int, 
	@ResurrectionTime datetime, 
	@ResurrectionCount int, -- 点券复活次数
	@ResurrectionAuto int, -- 到时间自动复活次数
	@WinningCount int, 
	@Kpi int, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[CrossCrowd_Manager] SET
	[DomainId] = @DomainId
	,[SiteId] = @SiteId
	,[SiteName] = @SiteName
	,[Name] = @Name
	,[Logo] = @Logo
	,[CrossCrowdId] = @CrossCrowdId
	,[Morale] = @Morale
	,[Score] = @Score
	,[ScoreUpdateTime] = @ScoreUpdateTime
	,[KillNumber] = @KillNumber
	,[ByKillNumber] = @ByKillNumber
	,[NextMatchTime] = @NextMatchTime
	,[ClearCdCount] = @ClearCdCount
	,[ResurrectionTime] = @ResurrectionTime
	,[ResurrectionCount] = @ResurrectionCount
	,[ResurrectionAuto] = @ResurrectionAuto
	,[WinningCount] = @WinningCount
	,[Kpi] = @Kpi
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


