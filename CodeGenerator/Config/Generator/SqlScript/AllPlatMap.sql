
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Allplatmap_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllPlatmap_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllPlatmap_Delete]
GO

/****** Object:  Stored Procedure [dbo].AllPlatmap_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllPlatmap_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllPlatmap_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AllPlatmap_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllPlatmap_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllPlatmap_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AllPlatmap_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllPlatmap_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllPlatmap_Insert]
GO

/****** Object:  Stored Procedure [dbo].AllPlatmap_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllPlatmap_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllPlatmap_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AllPlatmap_Delete
	@PlatCode varchar(20)
AS

DELETE FROM [dbo].[All_PlatMap]
WHERE
	[PlatCode] = @PlatCode

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

CREATE PROCEDURE [dbo].P_AllPlatmap_GetById
	@PlatCode varchar(20)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_PlatMap] with(nolock)
WHERE
	[PlatCode] = @PlatCode
	
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

CREATE PROCEDURE [dbo].P_AllPlatmap_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_PlatMap] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AllPlatmap_Insert
	@PlatCode varchar(20)
	,@PlatName varchar(50)
	,@ShareDomain varchar(50)
	,@SessionMode int
	,@AppKey varchar(100)
	,@AppSecret varchar(100)
	,@LoginKey varchar(100)
	,@PayKey varchar(100)
	,@PayPointRate int
	,@PlatMainUrl varchar(100)
	,@PlatApiUrl varchar(100)
	,@PayUrl varchar(100)
	,@BbsUrl varchar(100)
	,@NavUrl varchar(100)
	,@CdnUrl varchar(100)
	,@ChatUrl varchar(100)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[All_PlatMap] (
	[PlatCode]
	,[PlatName]
	,[ShareDomain]
	,[SessionMode]
	,[AppKey]
	,[AppSecret]
	,[LoginKey]
	,[PayKey]
	,[PayPointRate]
	,[PlatMainUrl]
	,[PlatApiUrl]
	,[PayUrl]
	,[BbsUrl]
	,[NavUrl]
	,[CdnUrl]
	,[ChatUrl]
	,[RowTime]
) VALUES (
    @PlatCode
    ,@PlatName
    ,@ShareDomain
    ,@SessionMode
    ,@AppKey
    ,@AppSecret
    ,@LoginKey
    ,@PayKey
    ,@PayPointRate
    ,@PlatMainUrl
    ,@PlatApiUrl
    ,@PayUrl
    ,@BbsUrl
    ,@NavUrl
    ,@CdnUrl
    ,@ChatUrl
    ,@RowTime
)

select @PlatCode

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

CREATE PROCEDURE [dbo].P_AllPlatmap_Update
	@PlatCode varchar(20), 
	@PlatName varchar(50), 
	@ShareDomain varchar(50), 
	@SessionMode int, 
	@AppKey varchar(100), 
	@AppSecret varchar(100), 
	@LoginKey varchar(100), 
	@PayKey varchar(100), 
	@PayPointRate int, 
	@PlatMainUrl varchar(100), 
	@PlatApiUrl varchar(100), 
	@PayUrl varchar(100), 
	@BbsUrl varchar(100), 
	@NavUrl varchar(100), 
	@CdnUrl varchar(100), 
	@ChatUrl varchar(100), 
	@RowTime datetime 
AS



UPDATE [dbo].[All_PlatMap] SET
	[PlatName] = @PlatName
	,[ShareDomain] = @ShareDomain
	,[SessionMode] = @SessionMode
	,[AppKey] = @AppKey
	,[AppSecret] = @AppSecret
	,[LoginKey] = @LoginKey
	,[PayKey] = @PayKey
	,[PayPointRate] = @PayPointRate
	,[PlatMainUrl] = @PlatMainUrl
	,[PlatApiUrl] = @PlatApiUrl
	,[PayUrl] = @PayUrl
	,[BbsUrl] = @BbsUrl
	,[NavUrl] = @NavUrl
	,[CdnUrl] = @CdnUrl
	,[ChatUrl] = @ChatUrl
	,[RowTime] = @RowTime
WHERE
	[PlatCode] = @PlatCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



