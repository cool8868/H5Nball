
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].A8csdk_Delete    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdk_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdk_Delete]
GO

/****** Object:  Stored Procedure [dbo].A8csdk_GetById    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdk_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdk_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].A8csdk_GetAll    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdk_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdk_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].A8csdk_Insert    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdk_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdk_Insert]
GO

/****** Object:  Stored Procedure [dbo].A8csdk_Update    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdk_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdk_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_A8csdk_Delete
	@Idx int
AS

DELETE FROM [dbo].[A8csdk]
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

CREATE PROCEDURE [dbo].P_A8csdk_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[A8csdk] with(nolock)
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

CREATE PROCEDURE [dbo].P_A8csdk_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[A8csdk] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_A8csdk_Insert
	@GameOrderId varchar(50) , 
	@Price int , 
	@GoodsName varchar(20) , 
	@GoodsId varchar(20) , 
	@Title varchar(20) , 
	@CsdkId varchar(20) , 
	@ChannelAlias varchar(20) , 
	@SubChannel varchar(20) , 
	@ServerId varchar(20) , 
	@ServerName varchar(20) , 
	@RoleId varchar(11) , 
	@RoleName varchar(20) , 
	@RoleLevel varchar(11) , 
	@SessionId varchar(50) , 
	@Model varchar(20) , 
	@Release varchar(20) , 
	@DeviceId varchar(64) , 
	@Ext varchar(1) , 
	@Uid varchar(50) , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[A8csdk] (
	[GameOrderId]
	,[Price]
	,[GoodsName]
	,[GoodsId]
	,[Title]
	,[CsdkId]
	,[ChannelAlias]
	,[SubChannel]
	,[ServerId]
	,[ServerName]
	,[RoleId]
	,[RoleName]
	,[RoleLevel]
	,[SessionId]
	,[Model]
	,[Release]
	,[DeviceId]
	,[Ext]
	,[Uid]
) VALUES (
    @GameOrderId
    ,@Price
    ,@GoodsName
    ,@GoodsId
    ,@Title
    ,@CsdkId
    ,@ChannelAlias
    ,@SubChannel
    ,@ServerId
    ,@ServerName
    ,@RoleId
    ,@RoleName
    ,@RoleLevel
    ,@SessionId
    ,@Model
    ,@Release
    ,@DeviceId
    ,@Ext
    ,@Uid
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

CREATE PROCEDURE [dbo].P_A8csdk_Update
	@Idx int, 
	@GameOrderId varchar(50), 
	@Price int, 
	@GoodsName varchar(20), 
	@GoodsId varchar(20), 
	@Title varchar(20), 
	@CsdkId varchar(20), 
	@ChannelAlias varchar(20), 
	@SubChannel varchar(20), 
	@ServerId varchar(20), 
	@ServerName varchar(20), 
	@RoleId varchar(11), 
	@RoleName varchar(20), 
	@RoleLevel varchar(11), 
	@SessionId varchar(50), 
	@Model varchar(20), 
	@Release varchar(20), 
	@DeviceId varchar(64), 
	@Ext varchar(1), 
	@Uid varchar(50) 
AS



UPDATE [dbo].[A8csdk] SET
	[GameOrderId] = @GameOrderId
	,[Price] = @Price
	,[GoodsName] = @GoodsName
	,[GoodsId] = @GoodsId
	,[Title] = @Title
	,[CsdkId] = @CsdkId
	,[ChannelAlias] = @ChannelAlias
	,[SubChannel] = @SubChannel
	,[ServerId] = @ServerId
	,[ServerName] = @ServerName
	,[RoleId] = @RoleId
	,[RoleName] = @RoleName
	,[RoleLevel] = @RoleLevel
	,[SessionId] = @SessionId
	,[Model] = @Model
	,[Release] = @Release
	,[DeviceId] = @DeviceId
	,[Ext] = @Ext
	,[Uid] = @Uid
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


