
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcrosssite_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrosssite_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrosssite_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrosssite_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrosssite_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrosssite_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCrosssite_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrosssite_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrosssite_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCrosssite_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrosssite_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrosssite_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrosssite_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrosssite_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrosssite_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCrosssite_Delete
	@Id int
AS

DELETE FROM [dbo].[Config_CrossSite]
WHERE
	[Id] = @Id

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

CREATE PROCEDURE [dbo].P_ConfigCrosssite_GetById
	@Id int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrossSite] with(nolock)
WHERE
	[Id] = @Id
	
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

CREATE PROCEDURE [dbo].P_ConfigCrosssite_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrossSite] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCrosssite_Insert
	@DomainId int , 
	@DomainName nvarchar(50) , 
	@SiteId varchar(50) , 
	@SiteName nvarchar(100) , 
	@ShowSiteId nvarchar(100) , 
	@ShowSiteName nvarchar(100) , 
	@InvalidFlag int , 
	@RowTime datetime , 
    @Id int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_CrossSite] (
	[DomainId]
	,[DomainName]
	,[SiteId]
	,[SiteName]
	,[ShowSiteId]
	,[ShowSiteName]
	,[InvalidFlag]
	,[RowTime]
) VALUES (
    @DomainId
    ,@DomainName
    ,@SiteId
    ,@SiteName
    ,@ShowSiteId
    ,@ShowSiteName
    ,@InvalidFlag
    ,@RowTime
)


SET @Id = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_ConfigCrosssite_Update
	@Id int, 
	@DomainId int, 
	@DomainName nvarchar(50), 
	@SiteId varchar(50), 
	@SiteName nvarchar(100), 
	@ShowSiteId nvarchar(100), 
	@ShowSiteName nvarchar(100), 
	@InvalidFlag int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Config_CrossSite] SET
	[DomainId] = @DomainId
	,[DomainName] = @DomainName
	,[SiteId] = @SiteId
	,[SiteName] = @SiteName
	,[ShowSiteId] = @ShowSiteId
	,[ShowSiteName] = @ShowSiteName
	,[InvalidFlag] = @InvalidFlag
	,[RowTime] = @RowTime
WHERE
	[Id] = @Id

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



