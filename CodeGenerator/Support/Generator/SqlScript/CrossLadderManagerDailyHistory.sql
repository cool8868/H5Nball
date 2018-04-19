
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossladdermanagerdailyhistory_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerdailyhistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerdailyhistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossladderManagerdailyhistory_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerdailyhistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerdailyhistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossladderManagerdailyhistory_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerdailyhistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerdailyhistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossladderManagerdailyhistory_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerdailyhistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerdailyhistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossladderManagerdailyhistory_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerdailyhistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerdailyhistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossladderManagerdailyhistory_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossLadder_ManagerDailyHistory]
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

CREATE PROCEDURE [dbo].P_CrossladderManagerdailyhistory_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_ManagerDailyHistory] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossladderManagerdailyhistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_ManagerDailyHistory] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossladderManagerdailyhistory_Insert
	@DomainId int , 
	@RecordDate datetime , 
	@Season int , 
	@ManagerId uniqueidentifier , 
	@SiteId varchar(50) , 
	@Rank int , 
	@Score int , 
	@PrizeItems varchar(50) , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@DailyMaxScore int , 
	@DailyMaxAddScore int , 
	@MaxScore int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossLadder_ManagerDailyHistory] (
	[DomainId]
	,[RecordDate]
	,[Season]
	,[ManagerId]
	,[SiteId]
	,[Rank]
	,[Score]
	,[PrizeItems]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[DailyMaxScore]
	,[DailyMaxAddScore]
	,[MaxScore]
) VALUES (
    @DomainId
    ,@RecordDate
    ,@Season
    ,@ManagerId
    ,@SiteId
    ,@Rank
    ,@Score
    ,@PrizeItems
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@DailyMaxScore
    ,@DailyMaxAddScore
    ,@MaxScore
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

CREATE PROCEDURE [dbo].P_CrossladderManagerdailyhistory_Update
	@Idx int, 
	@DomainId int, 
	@RecordDate datetime, 
	@Season int, 
	@ManagerId uniqueidentifier, 
	@SiteId varchar(50), 
	@Rank int, 
	@Score int, 
	@PrizeItems varchar(50), 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@DailyMaxScore int, 
	@DailyMaxAddScore int, 
	@MaxScore int 
AS



UPDATE [dbo].[CrossLadder_ManagerDailyHistory] SET
	[DomainId] = @DomainId
	,[RecordDate] = @RecordDate
	,[Season] = @Season
	,[ManagerId] = @ManagerId
	,[SiteId] = @SiteId
	,[Rank] = @Rank
	,[Score] = @Score
	,[PrizeItems] = @PrizeItems
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[DailyMaxScore] = @DailyMaxScore
	,[DailyMaxAddScore] = @DailyMaxAddScore
	,[MaxScore] = @MaxScore
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


