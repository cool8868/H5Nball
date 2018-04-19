
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityexcrossrank_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCrossrank_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCrossrank_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityexCrossrank_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCrossrank_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCrossrank_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityexCrossrank_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCrossrank_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCrossrank_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityexCrossrank_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCrossrank_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCrossrank_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityexCrossrank_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexCrossrank_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexCrossrank_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityexCrossrank_Delete
	@Idx int
AS

DELETE FROM [dbo].[ActivityEx_CrossRank]
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

CREATE PROCEDURE [dbo].P_ActivityexCrossrank_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_CrossRank] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityexCrossrank_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_CrossRank] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityexCrossrank_Insert
	@DomainId int , 
	@SiteId varchar(20) , 
	@ManagerId uniqueidentifier , 
	@Name nvarchar(50) , 
	@RankKey varchar(50) , 
	@ExData int , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ActivityEx_CrossRank] (
	[DomainId]
	,[SiteId]
	,[ManagerId]
	,[Name]
	,[RankKey]
	,[ExData]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @DomainId
    ,@SiteId
    ,@ManagerId
    ,@Name
    ,@RankKey
    ,@ExData
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

CREATE PROCEDURE [dbo].P_ActivityexCrossrank_Update
	@Idx int, 
	@DomainId int, -- 域ID
	@SiteId varchar(20), -- 区编号
	@ManagerId uniqueidentifier, 
	@Name nvarchar(50), 
	@RankKey varchar(50), 
	@ExData int, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[ActivityEx_CrossRank] SET
	[DomainId] = @DomainId
	,[SiteId] = @SiteId
	,[ManagerId] = @ManagerId
	,[Name] = @Name
	,[RankKey] = @RankKey
	,[ExData] = @ExData
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



