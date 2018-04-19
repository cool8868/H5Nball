
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Allzoneinfo_Delete    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllZoneinfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllZoneinfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].AllZoneinfo_GetById    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllZoneinfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllZoneinfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AllZoneinfo_GetAll    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllZoneinfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllZoneinfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AllZoneinfo_Insert    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllZoneinfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllZoneinfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].AllZoneinfo_Update    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AllZoneinfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AllZoneinfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AllZoneinfo_Delete
	@Idx int
AS

DELETE FROM [dbo].[All_Zoneinfo]
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

CREATE PROCEDURE [dbo].P_AllZoneinfo_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_Zoneinfo] with(nolock)
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

CREATE PROCEDURE [dbo].P_AllZoneinfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[All_Zoneinfo] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AllZoneinfo_Insert
	@Idx int
	,@PlatformCode varchar(50)
	,@ZoneName varchar(50)
	,@PlatformZoneName varchar(50)
	,@Name nvarchar(50)
	,@ApiUrl varchar(100)
	,@ChargeUrl varchar(100)
	,@WebServerUrl varchar(100)
	,@IsDebug int
	,@IsOpenCharge int
	,@ChatIp varchar(100)
	,@ChatPort int
	,@ClientVersion varchar(50)
	,@Cdn varchar(100)
	,@CrossName nvarchar(50)
	,@OpenIndulge bit
	,@TxFooter nvarchar(500)
	,@GameName nvarchar(50)
	,@States int
	,@Maintain varchar(500)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[All_Zoneinfo] (
	[Idx]
	,[PlatformCode]
	,[ZoneName]
	,[PlatformZoneName]
	,[Name]
	,[ApiUrl]
	,[ChargeUrl]
	,[WebServerUrl]
	,[IsDebug]
	,[IsOpenCharge]
	,[ChatIp]
	,[ChatPort]
	,[ClientVersion]
	,[Cdn]
	,[CrossName]
	,[OpenIndulge]
	,[TxFooter]
	,[GameName]
	,[States]
	,[Maintain]
) VALUES (
    @Idx
    ,@PlatformCode
    ,@ZoneName
    ,@PlatformZoneName
    ,@Name
    ,@ApiUrl
    ,@ChargeUrl
    ,@WebServerUrl
    ,@IsDebug
    ,@IsOpenCharge
    ,@ChatIp
    ,@ChatPort
    ,@ClientVersion
    ,@Cdn
    ,@CrossName
    ,@OpenIndulge
    ,@TxFooter
    ,@GameName
    ,@States
    ,@Maintain
)

select @Idx

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

CREATE PROCEDURE [dbo].P_AllZoneinfo_Update
	@Idx int, 
	@PlatformCode varchar(50), 
	@ZoneName varchar(50), 
	@PlatformZoneName varchar(50), 
	@Name nvarchar(50), 
	@ApiUrl varchar(100), 
	@ChargeUrl varchar(100), 
	@WebServerUrl varchar(100), 
	@IsDebug int, 
	@IsOpenCharge int, 
	@ChatIp varchar(100), 
	@ChatPort int, 
	@ClientVersion varchar(50), 
	@Cdn varchar(100), 
	@CrossName nvarchar(50), 
	@OpenIndulge bit, 
	@TxFooter nvarchar(500), 
	@GameName nvarchar(50), 
	@States int, -- 区状态
	@Maintain varchar(500) -- 维护说明
AS



UPDATE [dbo].[All_Zoneinfo] SET
	[PlatformCode] = @PlatformCode
	,[ZoneName] = @ZoneName
	,[PlatformZoneName] = @PlatformZoneName
	,[Name] = @Name
	,[ApiUrl] = @ApiUrl
	,[ChargeUrl] = @ChargeUrl
	,[WebServerUrl] = @WebServerUrl
	,[IsDebug] = @IsDebug
	,[IsOpenCharge] = @IsOpenCharge
	,[ChatIp] = @ChatIp
	,[ChatPort] = @ChatPort
	,[ClientVersion] = @ClientVersion
	,[Cdn] = @Cdn
	,[CrossName] = @CrossName
	,[OpenIndulge] = @OpenIndulge
	,[TxFooter] = @TxFooter
	,[GameName] = @GameName
	,[States] = @States
	,[Maintain] = @Maintain
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



