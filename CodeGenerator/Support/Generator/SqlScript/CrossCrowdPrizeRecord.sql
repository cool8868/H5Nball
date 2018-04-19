
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crosscrowdprizerecord_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPrizerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPrizerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdPrizerecord_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPrizerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPrizerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrosscrowdPrizerecord_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPrizerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPrizerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrosscrowdPrizerecord_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPrizerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPrizerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrosscrowdPrizerecord_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrosscrowdPrizerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrosscrowdPrizerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrosscrowdPrizerecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossCrowd_PrizeRecord]
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

CREATE PROCEDURE [dbo].P_CrosscrowdPrizerecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_PrizeRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrosscrowdPrizerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossCrowd_PrizeRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrosscrowdPrizerecord_Insert
	@Category int , 
	@SiteId varchar(20) , 
	@ManagerId uniqueidentifier , 
	@Source varchar(50) , 
	@PrizeItems varchar(100) , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossCrowd_PrizeRecord] (
	[Category]
	,[SiteId]
	,[ManagerId]
	,[Source]
	,[PrizeItems]
	,[RowTime]
) VALUES (
    @Category
    ,@SiteId
    ,@ManagerId
    ,@Source
    ,@PrizeItems
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

CREATE PROCEDURE [dbo].P_CrosscrowdPrizerecord_Update
	@Idx int, 
	@Category int, 
	@SiteId varchar(20), 
	@ManagerId uniqueidentifier, 
	@Source varchar(50), 
	@PrizeItems varchar(100), 
	@RowTime datetime 
AS



UPDATE [dbo].[CrossCrowd_PrizeRecord] SET
	[Category] = @Category
	,[SiteId] = @SiteId
	,[ManagerId] = @ManagerId
	,[Source] = @Source
	,[PrizeItems] = @PrizeItems
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


