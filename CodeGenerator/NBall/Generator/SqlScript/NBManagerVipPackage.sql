
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagervippackage_Delete    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagervippackage_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagervippackage_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagervippackage_GetById    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagervippackage_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagervippackage_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagervippackage_GetAll    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagervippackage_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagervippackage_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagervippackage_Insert    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagervippackage_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagervippackage_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagervippackage_Update    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagervippackage_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagervippackage_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagervippackage_Delete
	@Idx int
AS

DELETE FROM [dbo].[NB_ManagerVipPackage]
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

CREATE PROCEDURE [dbo].P_NbManagervippackage_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerVipPackage] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagervippackage_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerVipPackage] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagervippackage_Insert
	@ManagerId uniqueidentifier , 
	@PackageLevel int , 
	@IsGet int , 
	@RowTime date , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerVipPackage] (
	[ManagerId]
	,[PackageLevel]
	,[IsGet]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@PackageLevel
    ,@IsGet
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

CREATE PROCEDURE [dbo].P_NbManagervippackage_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@PackageLevel int, 
	@IsGet int, 
	@RowTime date 
AS



UPDATE [dbo].[NB_ManagerVipPackage] SET
	[ManagerId] = @ManagerId
	,[PackageLevel] = @PackageLevel
	,[IsGet] = @IsGet
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


