
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crosscrowdmaxkillerrecord_Delete    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMaxkillerrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMaxkillerrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdMaxkillerrecord_GetById    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMaxkillerrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMaxkillerrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrosscrowdMaxkillerrecord_GetAll    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMaxkillerrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMaxkillerrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrosscrowdMaxkillerrecord_Insert    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMaxkillerrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMaxkillerrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdMaxkillerrecord_Update    Script Date: 2016年9月6日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdMaxkillerrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdMaxkillerrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrosscrowdMaxkillerrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossCrowd_MaxKillerRecord]
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

CREATE PROCEDURE [dbo].P_CrosscrowdMaxkillerrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_MaxKillerRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrosscrowdMaxkillerrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_MaxKillerRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrosscrowdMaxkillerrecord_Insert
	@CrossCrowdId int , 
	@SiteId varchar(20) , 
	@ManagerId uniqueidentifier , 
	@PrizeItems varchar(100) , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossCrowd_MaxKillerRecord] (
	[CrossCrowdId]
	,[SiteId]
	,[ManagerId]
	,[PrizeItems]
	,[Status]
	,[RowTime]
) VALUES (
    @CrossCrowdId
    ,@SiteId
    ,@ManagerId
    ,@PrizeItems
    ,@Status
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_CrosscrowdMaxkillerrecord_Update
	@Idx int, 
	@CrossCrowdId int, 
	@SiteId varchar(20), 
	@ManagerId uniqueidentifier, 
	@PrizeItems varchar(100), 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[CrossCrowd_MaxKillerRecord] SET
	[CrossCrowdId] = @CrossCrowdId
	,[SiteId] = @SiteId
	,[ManagerId] = @ManagerId
	,[PrizeItems] = @PrizeItems
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


