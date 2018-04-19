
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Itempackage_Delete    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ItemPackage_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ItemPackage_Delete]
GO

/****** Object:  Stored Procedure [dbo].ItemPackage_GetById    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ItemPackage_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ItemPackage_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ItemPackage_GetAll    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ItemPackage_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ItemPackage_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ItemPackage_Insert    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ItemPackage_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ItemPackage_Insert]
GO

/****** Object:  Stored Procedure [dbo].ItemPackage_Update    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ItemPackage_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ItemPackage_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ItemPackage_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Item_Package]
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_ItemPackage_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Item_Package] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
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

CREATE PROCEDURE [dbo].P_ItemPackage_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Item_Package] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ItemPackage_Insert
	@PackageSize int , 
	@ItemVersion tinyint , 
	@ItemString varbinary(max) , 
	@Status int , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Item_Package] (
	[ManagerId],
	[PackageSize]
	,[ItemVersion]
	,[ItemString]
	,[Status]
	,[RowTime]
) VALUES (
	@ManagerId,
    @PackageSize
    ,@ItemVersion
    ,@ItemString
    ,@Status
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_ItemPackage_Update
	@ManagerId uniqueidentifier, 
	@PackageSize int, -- 背包大小
	@ItemVersion tinyint, -- 物品序列化版本
	@ItemString varbinary(max), -- 物品串
	@Status int, 
	@RowTime datetime, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[Item_Package] SET
	[PackageSize] = @PackageSize
	,[ItemVersion] = @ItemVersion
	,[ItemString] = @ItemString
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



