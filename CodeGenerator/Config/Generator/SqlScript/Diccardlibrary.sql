
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Diccardlibrary_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCardlibrary_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCardlibrary_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicCardlibrary_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCardlibrary_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCardlibrary_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicCardlibrary_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCardlibrary_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCardlibrary_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicCardlibrary_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCardlibrary_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCardlibrary_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicCardlibrary_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicCardlibrary_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicCardlibrary_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicCardlibrary_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_CardLibrary]
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

CREATE PROCEDURE [dbo].P_DicCardlibrary_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_CardLibrary] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicCardlibrary_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_CardLibrary] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicCardlibrary_Insert
	@Idx int
	,@Name nvarchar(50)
	,@Type int
	,@ItemType int
	,@SubType int
	,@ThirdType int
	,@MinPower int
	,@MaxPower int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_CardLibrary] (
	[Idx]
	,[Name]
	,[Type]
	,[ItemType]
	,[SubType]
	,[ThirdType]
	,[MinPower]
	,[MaxPower]
) VALUES (
    @Idx
    ,@Name
    ,@Type
    ,@ItemType
    ,@SubType
    ,@ThirdType
    ,@MinPower
    ,@MaxPower
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

CREATE PROCEDURE [dbo].P_DicCardlibrary_Update
	@Idx int, -- 卡库id
	@Name nvarchar(50), -- 卡库名字
	@Type int, -- 卡库类型
	@ItemType int, -- 物品类型
	@SubType int, -- 二级分类
	@ThirdType int, -- 三级分类
	@MinPower int, -- 能力值min
	@MaxPower int -- 能力值max
AS



UPDATE [dbo].[Dic_CardLibrary] SET
	[Name] = @Name
	,[Type] = @Type
	,[ItemType] = @ItemType
	,[SubType] = @SubType
	,[ThirdType] = @ThirdType
	,[MinPower] = @MinPower
	,[MaxPower] = @MaxPower
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



