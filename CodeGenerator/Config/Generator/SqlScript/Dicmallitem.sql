
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicmallitem_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMallitem_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMallitem_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicMallitem_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMallitem_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMallitem_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicMallitem_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMallitem_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMallitem_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicMallitem_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMallitem_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMallitem_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicMallitem_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMallitem_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMallitem_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicMallitem_Delete
	@MallCode int
AS

DELETE FROM [dbo].[Dic_MallItem]
WHERE
	[MallCode] = @MallCode

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

CREATE PROCEDURE [dbo].P_DicMallitem_GetById
	@MallCode int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_MallItem] with(nolock)
WHERE
	[MallCode] = @MallCode
	
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

CREATE PROCEDURE [dbo].P_DicMallitem_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_MallItem] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicMallitem_Insert
	@MallCode int
	,@Name nvarchar(50)
	,@MallType int
	,@Quality int
	,@ShowOrder int
	,@ImageId int
	,@ItemIntro nvarchar(100)
	,@ItemTip nvarchar(100)
	,@UseNote nvarchar(100)
	,@UseMsg nvarchar(100)
	,@UseLevel int
	,@ShowUse bit
	,@CurrencyType int
	,@CurrencyCount int
	,@CurrencyDiscount varchar(100)
	,@EffectType int
	,@EffectValue int
	,@ShowFlag bit
	,@HotFlag bit
	,@PackageFlag bit
	,@ShowBatch bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_MallItem] (
	[MallCode]
	,[Name]
	,[MallType]
	,[Quality]
	,[ShowOrder]
	,[ImageId]
	,[ItemIntro]
	,[ItemTip]
	,[UseNote]
	,[UseMsg]
	,[UseLevel]
	,[ShowUse]
	,[CurrencyType]
	,[CurrencyCount]
	,[CurrencyDiscount]
	,[EffectType]
	,[EffectValue]
	,[ShowFlag]
	,[HotFlag]
	,[PackageFlag]
	,[ShowBatch]
) VALUES (
    @MallCode
    ,@Name
    ,@MallType
    ,@Quality
    ,@ShowOrder
    ,@ImageId
    ,@ItemIntro
    ,@ItemTip
    ,@UseNote
    ,@UseMsg
    ,@UseLevel
    ,@ShowUse
    ,@CurrencyType
    ,@CurrencyCount
    ,@CurrencyDiscount
    ,@EffectType
    ,@EffectValue
    ,@ShowFlag
    ,@HotFlag
    ,@PackageFlag
    ,@ShowBatch
)

select @MallCode

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

CREATE PROCEDURE [dbo].P_DicMallitem_Update
	@MallCode int, -- 商品Id
	@Name nvarchar(50), -- 商品名称
	@MallType int, -- 商品类别
	@Quality int, -- 道具品质:1,橙色；2，紫色；3，蓝色；4，绿色
	@ShowOrder int, 
	@ImageId int, -- 商品图标
	@ItemIntro nvarchar(100), -- 商品介绍
	@ItemTip nvarchar(100), -- 提示
	@UseNote nvarchar(100), -- 使用方法
	@UseMsg nvarchar(100), -- 使用结果提示
	@UseLevel int, -- 使用需等级
	@ShowUse bit, -- 是否显示使用按钮
	@CurrencyType int, -- 货币类型：1，点券；2，金币
	@CurrencyCount int, -- 货币数量
	@CurrencyDiscount varchar(100), -- 点券折扣，按时间配置，格式：0,0~100&77870,122510~60
	@EffectType int, -- 效果类型
	@EffectValue int, -- 效果值
	@ShowFlag bit, -- 是否在商城显示
	@HotFlag bit, -- 是否在热卖里显示
	@PackageFlag bit, -- 是否进背包
	@ShowBatch bit 
AS



UPDATE [dbo].[Dic_MallItem] SET
	[Name] = @Name
	,[MallType] = @MallType
	,[Quality] = @Quality
	,[ShowOrder] = @ShowOrder
	,[ImageId] = @ImageId
	,[ItemIntro] = @ItemIntro
	,[ItemTip] = @ItemTip
	,[UseNote] = @UseNote
	,[UseMsg] = @UseMsg
	,[UseLevel] = @UseLevel
	,[ShowUse] = @ShowUse
	,[CurrencyType] = @CurrencyType
	,[CurrencyCount] = @CurrencyCount
	,[CurrencyDiscount] = @CurrencyDiscount
	,[EffectType] = @EffectType
	,[EffectValue] = @EffectValue
	,[ShowFlag] = @ShowFlag
	,[HotFlag] = @HotFlag
	,[PackageFlag] = @PackageFlag
	,[ShowBatch] = @ShowBatch
WHERE
	[MallCode] = @MallCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



