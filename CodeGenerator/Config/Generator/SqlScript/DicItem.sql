
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicitem_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicItem_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicItem_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicItem_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicItem_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicItem_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicItem_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicItem_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicItem_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicItem_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicItem_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicItem_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicItem_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicItem_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicItem_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicItem_Delete
	@ItemCode int
AS

DELETE FROM [dbo].[Dic_Item]
WHERE
	[ItemCode] = @ItemCode

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

CREATE PROCEDURE [dbo].P_DicItem_GetById
	@ItemCode int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Item] with(nolock)
WHERE
	[ItemCode] = @ItemCode
	
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

CREATE PROCEDURE [dbo].P_DicItem_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Item] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicItem_Insert
	@ItemCode int
	,@ItemName nvarchar(50)
	,@ItemType int
	,@SubType int
	,@ThirdType int
	,@FourthType int
	,@ImageId int
	,@LinkId int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Item] (
	[ItemCode]
	,[ItemName]
	,[ItemType]
	,[SubType]
	,[ThirdType]
	,[FourthType]
	,[ImageId]
	,[LinkId]
) VALUES (
    @ItemCode
    ,@ItemName
    ,@ItemType
    ,@SubType
    ,@ThirdType
    ,@FourthType
    ,@ImageId
    ,@LinkId
)

select @ItemCode

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

CREATE PROCEDURE [dbo].P_DicItem_Update
	@ItemCode int, 
	@ItemName nvarchar(50), -- 物品名称
	@ItemType int, -- 物品类型
	@SubType int, -- 二级分类(球员卡>颜色；装备>套装or散装；)
	@ThirdType int, -- 三级分类（球员卡>所属联赛；装备>品质）
	@FourthType int, -- 四级分类(球员卡>综合能力)
	@ImageId int, -- 图片id
	@LinkId int -- 关联id
AS



UPDATE [dbo].[Dic_Item] SET
	[ItemName] = @ItemName
	,[ItemType] = @ItemType
	,[SubType] = @SubType
	,[ThirdType] = @ThirdType
	,[FourthType] = @FourthType
	,[ImageId] = @ImageId
	,[LinkId] = @LinkId
WHERE
	[ItemCode] = @ItemCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



