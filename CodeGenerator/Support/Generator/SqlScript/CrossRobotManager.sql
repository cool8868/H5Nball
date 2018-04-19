
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossrobotmanager_Delete    Script Date: 2016年8月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossrobotManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossrobotManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossrobotManager_GetById    Script Date: 2016年8月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossrobotManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossrobotManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossrobotManager_GetAll    Script Date: 2016年8月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossrobotManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossrobotManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossrobotManager_Insert    Script Date: 2016年8月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossrobotManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossrobotManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossrobotManager_Update    Script Date: 2016年8月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossrobotManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossrobotManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossrobotManager_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[CrossRobot_Manager]
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

CREATE PROCEDURE [dbo].P_CrossrobotManager_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossRobot_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossrobotManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossRobot_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossrobotManager_Insert
	@SiteId varchar(50) , 
	@CrossCrowd bit , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossRobot_Manager] (
	[Idx],
	[SiteId]
	,[CrossCrowd]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
	@Idx,
    @SiteId
    ,@CrossCrowd
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

CREATE PROCEDURE [dbo].P_CrossrobotManager_Update
	@Idx uniqueidentifier, 
	@SiteId varchar(50), 
	@CrossCrowd bit, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[CrossRobot_Manager] SET
	[SiteId] = @SiteId
	,[CrossCrowd] = @CrossCrowd
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


