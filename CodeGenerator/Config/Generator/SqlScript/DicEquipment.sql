
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicequipment_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipment_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipment_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicEquipment_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipment_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipment_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicEquipment_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipment_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipment_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicEquipment_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipment_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipment_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicEquipment_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicEquipment_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicEquipment_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicEquipment_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_Equipment]
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

CREATE PROCEDURE [dbo].P_DicEquipment_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Equipment] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicEquipment_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Equipment] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicEquipment_Insert
	@Idx int
	,@Name nvarchar(50)
	,@SuitType int
	,@SuitId int
	,@Quality int
	,@PropertyType1 int
	,@PropertyType2 int
	,@Description nvarchar(500)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Equipment] (
	[Idx]
	,[Name]
	,[SuitType]
	,[SuitId]
	,[Quality]
	,[PropertyType1]
	,[PropertyType2]
	,[Description]
) VALUES (
    @Idx
    ,@Name
    ,@SuitType
    ,@SuitId
    ,@Quality
    ,@PropertyType1
    ,@PropertyType2
    ,@Description
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

CREATE PROCEDURE [dbo].P_DicEquipment_Update
	@Idx int, 
	@Name nvarchar(50), 
	@SuitType int, -- 套装类型：1,7件套套装;2,5件套套装;3,3件套套装;4,散装;
	@SuitId int, 
	@Quality int, -- 装备品质：1,史诗;2,精良;3,优质;4,普通;5,劣质;
	@PropertyType1 int, -- 属性加成类型1，绝对值
	@PropertyType2 int, -- 属性加成类型2，百分比
	@Description nvarchar(500) -- 装备描述
AS



UPDATE [dbo].[Dic_Equipment] SET
	[Name] = @Name
	,[SuitType] = @SuitType
	,[SuitId] = @SuitId
	,[Quality] = @Quality
	,[PropertyType1] = @PropertyType1
	,[PropertyType2] = @PropertyType2
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



