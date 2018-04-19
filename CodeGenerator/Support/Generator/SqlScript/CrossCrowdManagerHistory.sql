
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crosscrowdmanagerhistory_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManagerhistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManagerhistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdManagerhistory_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManagerhistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManagerhistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrosscrowdManagerhistory_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManagerhistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManagerhistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrosscrowdManagerhistory_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManagerhistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManagerhistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdManagerhistory_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdManagerhistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdManagerhistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrosscrowdManagerhistory_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossCrowd_ManagerHistory]
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

CREATE PROCEDURE [dbo].P_CrosscrowdManagerhistory_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_ManagerHistory] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrosscrowdManagerhistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_ManagerHistory] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrosscrowdManagerhistory_Insert
	@SiteId varchar(20) , 
	@ManagerId uniqueidentifier , 
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
	@Rank int , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossCrowd_ManagerHistory] (
	[SiteId]
	,[ManagerId]
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
	,[Rank]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @SiteId
    ,@ManagerId
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
    ,@Rank
    ,@Status
    ,@RowTime
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_CrosscrowdManagerhistory_Update
	@Idx int, 
	@SiteId varchar(20), 
	@ManagerId uniqueidentifier, 
	@CrossCrowdId int, 
	@Morale int, 
	@Score int, 
	@ScoreUpdateTime datetime, 
	@KillNumber int, 
	@ByKillNumber int, 
	@NextMatchTime datetime, 
	@ClearCdCount int, 
	@ResurrectionTime datetime, 
	@ResurrectionCount int, 
	@ResurrectionAuto int, 
	@WinningCount int, 
	@Rank int, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[CrossCrowd_ManagerHistory] SET
	[SiteId] = @SiteId
	,[ManagerId] = @ManagerId
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
	,[Rank] = @Rank
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


