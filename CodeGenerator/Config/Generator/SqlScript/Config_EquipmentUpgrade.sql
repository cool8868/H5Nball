
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configequipmentupgrade_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentupgrade_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentupgrade_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigEquipmentupgrade_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentupgrade_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentupgrade_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigEquipmentupgrade_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentupgrade_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentupgrade_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigEquipmentupgrade_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentupgrade_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentupgrade_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigEquipmentupgrade_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEquipmentupgrade_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEquipmentupgrade_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigEquipmentupgrade_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_EquipmentUpgrade]
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentupgrade_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EquipmentUpgrade] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentupgrade_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EquipmentUpgrade] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigEquipmentupgrade_Insert
	@Idx int
	,@EquipQuality int
	,@SourceLevel int
	,@TargetLevel int
	,@FailLevel int
	,@ProtectedLevel int
	,@ProtectedConsume int
	,@PropertyNum int
	,@Rate int
	,@Coin int
	,@ItemCode1 int
	,@ItemCount1 int
	,@ItemCode2 int
	,@ItemCount2 int
	,@ItemCode3 int
	,@ItemCount3 int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_EquipmentUpgrade] (
	[Idx]
	,[EquipQuality]
	,[SourceLevel]
	,[TargetLevel]
	,[FailLevel]
	,[ProtectedLevel]
	,[ProtectedConsume]
	,[PropertyNum]
	,[Rate]
	,[Coin]
	,[ItemCode1]
	,[ItemCount1]
	,[ItemCode2]
	,[ItemCount2]
	,[ItemCode3]
	,[ItemCount3]
) VALUES (
    @Idx
    ,@EquipQuality
    ,@SourceLevel
    ,@TargetLevel
    ,@FailLevel
    ,@ProtectedLevel
    ,@ProtectedConsume
    ,@PropertyNum
    ,@Rate
    ,@Coin
    ,@ItemCode1
    ,@ItemCount1
    ,@ItemCode2
    ,@ItemCount2
    ,@ItemCode3
    ,@ItemCount3
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

CREATE PROCEDURE [dbo].P_ConfigEquipmentupgrade_Update
	@Idx int, 
	@EquipQuality int, -- 装备类型
	@SourceLevel int, 
	@TargetLevel int, 
	@FailLevel int, 
	@ProtectedLevel int, 
	@ProtectedConsume int, 
	@PropertyNum int, 
	@Rate int, -- 进阶概率
	@Coin int, -- 进阶消耗金币
	@ItemCode1 int, 
	@ItemCount1 int, 
	@ItemCode2 int, 
	@ItemCount2 int, 
	@ItemCode3 int, 
	@ItemCount3 int 
AS



UPDATE [dbo].[Config_EquipmentUpgrade] SET
	[EquipQuality] = @EquipQuality
	,[SourceLevel] = @SourceLevel
	,[TargetLevel] = @TargetLevel
	,[FailLevel] = @FailLevel
	,[ProtectedLevel] = @ProtectedLevel
	,[ProtectedConsume] = @ProtectedConsume
	,[PropertyNum] = @PropertyNum
	,[Rate] = @Rate
	,[Coin] = @Coin
	,[ItemCode1] = @ItemCode1
	,[ItemCount1] = @ItemCount1
	,[ItemCode2] = @ItemCode2
	,[ItemCount2] = @ItemCount2
	,[ItemCode3] = @ItemCode3
	,[ItemCount3] = @ItemCount3
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



