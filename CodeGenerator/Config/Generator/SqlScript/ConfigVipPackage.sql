
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configvippackage_Delete    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigVippackage_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigVippackage_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigVippackage_GetById    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigVippackage_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigVippackage_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigVippackage_GetAll    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigVippackage_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigVippackage_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigVippackage_Insert    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigVippackage_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigVippackage_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigVippackage_Update    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigVippackage_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigVippackage_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigVippackage_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_VipPackage]
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

CREATE PROCEDURE [dbo].P_ConfigVippackage_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_VipPackage] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigVippackage_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_VipPackage] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigVippackage_Insert
	@Idx int
	,@VipLevel int
	,@Price int
	,@PrizeType int
	,@PrizeItemCode int
	,@Counts int
	,@IsBindlny int
	,@PackageId int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_VipPackage] (
	[Idx]
	,[VipLevel]
	,[Price]
	,[PrizeType]
	,[PrizeItemCode]
	,[Counts]
	,[IsBindlny]
	,[PackageId]
) VALUES (
    @Idx
    ,@VipLevel
    ,@Price
    ,@PrizeType
    ,@PrizeItemCode
    ,@Counts
    ,@IsBindlny
    ,@PackageId
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

CREATE PROCEDURE [dbo].P_ConfigVippackage_Update
	@Idx int, 
	@VipLevel int, 
	@Price int, 
	@PrizeType int, 
	@PrizeItemCode int, 
	@Counts int, 
	@IsBindlny int, 
	@PackageId int 
AS



UPDATE [dbo].[Config_VipPackage] SET
	[VipLevel] = @VipLevel
	,[Price] = @Price
	,[PrizeType] = @PrizeType
	,[PrizeItemCode] = @PrizeItemCode
	,[Counts] = @Counts
	,[IsBindlny] = @IsBindlny
	,[PackageId] = @PackageId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



