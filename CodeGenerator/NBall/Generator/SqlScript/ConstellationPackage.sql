
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Constellationpackage_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConstellationPackage_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConstellationPackage_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConstellationPackage_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConstellationPackage_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConstellationPackage_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConstellationPackage_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConstellationPackage_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConstellationPackage_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConstellationPackage_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConstellationPackage_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConstellationPackage_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConstellationPackage_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConstellationPackage_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConstellationPackage_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConstellationPackage_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Constellation_Package]
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

CREATE PROCEDURE [dbo].P_ConstellationPackage_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Constellation_Package] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConstellationPackage_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Constellation_Package] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConstellationPackage_Insert
	@PackageSize int , 
	@ItemString varbinary(max) , 
	@Status int , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Constellation_Package] (
	[ManagerId],
	[PackageSize]
	,[ItemString]
	,[Status]
	,[RowTime]
) VALUES (
	@ManagerId,
    @PackageSize
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

CREATE PROCEDURE [dbo].P_ConstellationPackage_Update
	@ManagerId uniqueidentifier, 
	@PackageSize int, 
	@ItemString varbinary(max), -- 物品串
	@Status int, 
	@RowTime datetime, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[Constellation_Package] SET
	[PackageSize] = @PackageSize
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



