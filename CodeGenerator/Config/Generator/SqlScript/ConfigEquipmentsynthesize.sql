
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configequipmentsynthesize_Delete    Script Date: 2016年2月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentsynthesize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentsynthesize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigEquipmentsynthesize_GetById    Script Date: 2016年2月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentsynthesize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentsynthesize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigEquipmentsynthesize_GetAll    Script Date: 2016年2月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentsynthesize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentsynthesize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigEquipmentsynthesize_Insert    Script Date: 2016年2月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentsynthesize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentsynthesize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigEquipmentsynthesize_Update    Script Date: 2016年2月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentsynthesize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentsynthesize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigEquipmentsynthesize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_EquipmentSynthesize]
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentsynthesize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EquipmentSynthesize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentsynthesize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EquipmentSynthesize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigEquipmentsynthesize_Insert
	@Idx int
	,@Quality int
	,@RateQuality1 int
	,@RateQuality2 int
	,@RateQuality3 int
	,@RateQuality4 int
	,@Coin int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_EquipmentSynthesize] (
	[Idx]
	,[Quality]
	,[RateQuality1]
	,[RateQuality2]
	,[RateQuality3]
	,[RateQuality4]
	,[Coin]
) VALUES (
    @Idx
    ,@Quality
    ,@RateQuality1
    ,@RateQuality2
    ,@RateQuality3
    ,@RateQuality4
    ,@Coin
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentsynthesize_Update
	@Idx int, 
	@Quality int, 
	@RateQuality1 int, -- 概率(0-10000)
	@RateQuality2 int, 
	@RateQuality3 int, 
	@RateQuality4 int, 
	@Coin int -- 消耗游戏币
AS



UPDATE [dbo].[Config_EquipmentSynthesize] SET
	[Quality] = @Quality
	,[RateQuality1] = @RateQuality1
	,[RateQuality2] = @RateQuality2
	,[RateQuality3] = @RateQuality3
	,[RateQuality4] = @RateQuality4
	,[Coin] = @Coin
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



