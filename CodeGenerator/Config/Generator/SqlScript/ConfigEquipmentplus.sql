
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configequipmentplus_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentplus_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentplus_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigEquipmentplus_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentplus_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentplus_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigEquipmentplus_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentplus_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentplus_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigEquipmentplus_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentplus_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentplus_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigEquipmentplus_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentplus_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentplus_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigEquipmentplus_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_EquipmentPlus]
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentplus_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EquipmentPlus] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentplus_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EquipmentPlus] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigEquipmentplus_Insert
	@Idx int
	,@Quality int
	,@PlusValueMin int
	,@PlusValueMax int
	,@PlusRateMin int
	,@PlusRateMax int
	,@SlotMin int
	,@SlotMax int
	,@WashMallCode int
	,@LockMallCode int
	,@StarSkillPlusRate int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_EquipmentPlus] (
	[Idx]
	,[Quality]
	,[PlusValueMin]
	,[PlusValueMax]
	,[PlusRateMin]
	,[PlusRateMax]
	,[SlotMin]
	,[SlotMax]
	,[WashMallCode]
	,[LockMallCode]
	,[StarSkillPlusRate]
) VALUES (
    @Idx
    ,@Quality
    ,@PlusValueMin
    ,@PlusValueMax
    ,@PlusRateMin
    ,@PlusRateMax
    ,@SlotMin
    ,@SlotMax
    ,@WashMallCode
    ,@LockMallCode
    ,@StarSkillPlusRate
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentplus_Update
	@Idx int, 
	@Quality int, -- 品质
	@PlusValueMin int, -- 加成值min
	@PlusValueMax int, -- 加成值max
	@PlusRateMin int, -- 加成百分比min
	@PlusRateMax int, -- 加成百分比max
	@SlotMin int, -- 插槽数量min
	@SlotMax int, -- 插槽数量max
	@WashMallCode int, 
	@LockMallCode int, 
	@StarSkillPlusRate int -- 附加球星技能概率0-10000
AS



UPDATE [dbo].[Config_EquipmentPlus] SET
	[Quality] = @Quality
	,[PlusValueMin] = @PlusValueMin
	,[PlusValueMax] = @PlusValueMax
	,[PlusRateMin] = @PlusRateMin
	,[PlusRateMax] = @PlusRateMax
	,[SlotMin] = @SlotMin
	,[SlotMax] = @SlotMax
	,[WashMallCode] = @WashMallCode
	,[LockMallCode] = @LockMallCode
	,[StarSkillPlusRate] = @StarSkillPlusRate
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



