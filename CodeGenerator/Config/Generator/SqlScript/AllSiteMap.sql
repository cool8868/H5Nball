
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Allsitemap_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllSitemap_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllSitemap_Delete]
GO

/****** Object:  Stored Procedure [dbo].AllSitemap_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllSitemap_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllSitemap_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AllSitemap_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllSitemap_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllSitemap_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AllSitemap_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllSitemap_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllSitemap_Insert]
GO

/****** Object:  Stored Procedure [dbo].AllSitemap_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllSitemap_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllSitemap_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AllSitemap_Delete
	@Id int
AS

DELETE FROM [dbo].[All_SiteMap]
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

CREATE PROCEDURE [dbo].P_AllSitemap_GetById
	@Id int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_SiteMap] with(nolock)
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

CREATE PROCEDURE [dbo].P_AllSitemap_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_SiteMap] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AllSitemap_Insert
	@PlatCode varchar(20) , 
	@PlatSiteId varchar(20) , 
	@SiteId varchar(20) , 
	@SiteName varchar(20) , 
	@PendStartTime datetime , 
	@PendEndTime datetime , 
	@SiteState varchar(20) , 
	@PlatMainUrl varchar(100) , 
	@PlatApiUrl varchar(100) , 
	@PayUrl varchar(100) , 
	@BbsUrl varchar(100) , 
	@NavUrl varchar(100) , 
	@CdnUrl varchar(100) , 
	@ChatUrl varchar(100) , 
	@SiteMainUrl varchar(100) , 
	@SiteLoginUrl varchar(100) , 
	@SiteApiUrl varchar(100) , 
	@SiteSvcUrl varchar(100) , 
	@RowTime datetime , 
    @Id int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[All_SiteMap] (
	[PlatCode]
	,[PlatSiteId]
	,[SiteId]
	,[SiteName]
	,[PendStartTime]
	,[PendEndTime]
	,[SiteState]
	,[PlatMainUrl]
	,[PlatApiUrl]
	,[PayUrl]
	,[BbsUrl]
	,[NavUrl]
	,[CdnUrl]
	,[ChatUrl]
	,[SiteMainUrl]
	,[SiteLoginUrl]
	,[SiteApiUrl]
	,[SiteSvcUrl]
	,[RowTime]
) VALUES (
    @PlatCode
    ,@PlatSiteId
    ,@SiteId
    ,@SiteName
    ,@PendStartTime
    ,@PendEndTime
    ,@SiteState
    ,@PlatMainUrl
    ,@PlatApiUrl
    ,@PayUrl
    ,@BbsUrl
    ,@NavUrl
    ,@CdnUrl
    ,@ChatUrl
    ,@SiteMainUrl
    ,@SiteLoginUrl
    ,@SiteApiUrl
    ,@SiteSvcUrl
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

CREATE PROCEDURE [dbo].P_AllSitemap_Update
	@Id int, 
	@PlatCode varchar(20), 
	@PlatSiteId varchar(20), 
	@SiteId varchar(20), 
	@SiteName varchar(20), 
	@PendStartTime datetime, 
	@PendEndTime datetime, 
	@SiteState varchar(20), 
	@PlatMainUrl varchar(100), 
	@PlatApiUrl varchar(100), 
	@PayUrl varchar(100), 
	@BbsUrl varchar(100), 
	@NavUrl varchar(100), 
	@CdnUrl varchar(100), 
	@ChatUrl varchar(100), 
	@SiteMainUrl varchar(100), 
	@SiteLoginUrl varchar(100), 
	@SiteApiUrl varchar(100), 
	@SiteSvcUrl varchar(100), 
	@RowTime datetime 
AS



UPDATE [dbo].[All_SiteMap] SET
	[PlatCode] = @PlatCode
	,[PlatSiteId] = @PlatSiteId
	,[SiteId] = @SiteId
	,[SiteName] = @SiteName
	,[PendStartTime] = @PendStartTime
	,[PendEndTime] = @PendEndTime
	,[SiteState] = @SiteState
	,[PlatMainUrl] = @PlatMainUrl
	,[PlatApiUrl] = @PlatApiUrl
	,[PayUrl] = @PayUrl
	,[BbsUrl] = @BbsUrl
	,[NavUrl] = @NavUrl
	,[CdnUrl] = @CdnUrl
	,[ChatUrl] = @ChatUrl
	,[SiteMainUrl] = @SiteMainUrl
	,[SiteLoginUrl] = @SiteLoginUrl
	,[SiteApiUrl] = @SiteApiUrl
	,[SiteSvcUrl] = @SiteSvcUrl
	,[RowTime] = @RowTime
WHERE
	[Id] = @Id

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



