
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossladdermanagerhistory_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerhistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerhistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossladderManagerhistory_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerhistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerhistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossladderManagerhistory_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerhistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerhistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossladderManagerhistory_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerhistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerhistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossladderManagerhistory_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderManagerhistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderManagerhistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossladderManagerhistory_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossLadder_ManagerHistory]
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

CREATE PROCEDURE [dbo].P_CrossladderManagerhistory_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_ManagerHistory] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossladderManagerhistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_ManagerHistory] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossladderManagerhistory_Insert
	@DomainId int , 
	@Season int , 
	@ManagerId uniqueidentifier , 
	@SiteId varchar(50) , 
	@Rank int , 
	@Score int , 
	@PrizeItems varchar(50) , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossLadder_ManagerHistory] (
	[DomainId]
	,[Season]
	,[ManagerId]
	,[SiteId]
	,[Rank]
	,[Score]
	,[PrizeItems]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @DomainId
    ,@Season
    ,@ManagerId
    ,@SiteId
    ,@Rank
    ,@Score
    ,@PrizeItems
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

CREATE PROCEDURE [dbo].P_CrossladderManagerhistory_Update
	@Idx int, 
	@DomainId int, 
	@Season int, -- 所属赛季
	@ManagerId uniqueidentifier, -- 经理id
	@SiteId varchar(50), 
	@Rank int, 
	@Score int, -- 积分
	@PrizeItems varchar(50), -- 格式：ItemCode,Strength,IsBinding|ItemCode,Strength,IsBinding
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[CrossLadder_ManagerHistory] SET
	[DomainId] = @DomainId
	,[Season] = @Season
	,[ManagerId] = @ManagerId
	,[SiteId] = @SiteId
	,[Rank] = @Rank
	,[Score] = @Score
	,[PrizeItems] = @PrizeItems
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


