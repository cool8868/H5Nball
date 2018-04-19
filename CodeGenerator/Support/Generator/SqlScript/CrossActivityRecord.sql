
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossactivityrecord_Delete    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossactivityRecord_GetById    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossactivityRecord_GetAll    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossactivityRecord_Insert    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossactivityRecord_Update    Script Date: 2016年11月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossactivityRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossactivityRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossactivityRecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[CrossActivity_Record]
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

CREATE PROCEDURE [dbo].P_CrossactivityRecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossActivity_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossactivityRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossActivity_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossactivityRecord_Insert
	@ManagerId uniqueidentifier , 
	@SiteName varchar(50) , 
	@PrizeId int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[CrossActivity_Record] (
	[ManagerId]
	,[SiteName]
	,[PrizeId]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@SiteName
    ,@PrizeId
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

CREATE PROCEDURE [dbo].P_CrossactivityRecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@SiteName varchar(50), 
	@PrizeId int, 
	@RowTime datetime 
AS



UPDATE [dbo].[CrossActivity_Record] SET
	[ManagerId] = @ManagerId
	,[SiteName] = @SiteName
	,[PrizeId] = @PrizeId
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


