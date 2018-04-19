
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configequipmentprecisioncasting_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentprecisioncasting_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentprecisioncasting_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigEquipmentprecisioncasting_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentprecisioncasting_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentprecisioncasting_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigEquipmentprecisioncasting_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentprecisioncasting_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentprecisioncasting_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigEquipmentprecisioncasting_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentprecisioncasting_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentprecisioncasting_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigEquipmentprecisioncasting_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentprecisioncasting_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentprecisioncasting_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigEquipmentprecisioncasting_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_EquipmentPrecisionCasting]
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentprecisioncasting_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EquipmentPrecisionCasting] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentprecisioncasting_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EquipmentPrecisionCasting] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigEquipmentprecisioncasting_Insert
	@Idx int
	,@EquipmentQuality int
	,@PropertyQuality int
	,@PropertyType int
	,@RateMin int
	,@RateMax int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_EquipmentPrecisionCasting] (
	[Idx]
	,[EquipmentQuality]
	,[PropertyQuality]
	,[PropertyType]
	,[RateMin]
	,[RateMax]
) VALUES (
    @Idx
    ,@EquipmentQuality
    ,@PropertyQuality
    ,@PropertyType
    ,@RateMin
    ,@RateMax
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentprecisioncasting_Update
	@Idx int, 
	@EquipmentQuality int, 
	@PropertyQuality int, 
	@PropertyType int, 
	@RateMin int, 
	@RateMax int 
AS



UPDATE [dbo].[Config_EquipmentPrecisionCasting] SET
	[EquipmentQuality] = @EquipmentQuality
	,[PropertyQuality] = @PropertyQuality
	,[PropertyType] = @PropertyType
	,[RateMin] = @RateMin
	,[RateMax] = @RateMax
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



