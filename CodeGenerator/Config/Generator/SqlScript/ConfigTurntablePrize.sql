
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configturntableprize_Delete    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntableprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntableprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigTurntableprize_GetById    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntableprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntableprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigTurntableprize_GetAll    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntableprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntableprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigTurntableprize_Insert    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntableprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntableprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigTurntableprize_Update    Script Date: 2016年7月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntableprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntableprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigTurntableprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_TurntablePrize]
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

CREATE PROCEDURE [dbo].P_ConfigTurntableprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_TurntablePrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigTurntableprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_TurntablePrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigTurntableprize_Insert
	@Idx int
	,@TurntableId int
	,@TurntableType int
	,@PrizeType int
	,@SubType int
	,@ItemCount int
	,@SpecialString varchar(200)
	,@InitialRate int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_TurntablePrize] (
	[Idx]
	,[TurntableId]
	,[TurntableType]
	,[PrizeType]
	,[SubType]
	,[ItemCount]
	,[SpecialString]
	,[InitialRate]
) VALUES (
    @Idx
    ,@TurntableId
    ,@TurntableType
    ,@PrizeType
    ,@SubType
    ,@ItemCount
    ,@SpecialString
    ,@InitialRate
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

CREATE PROCEDURE [dbo].P_ConfigTurntableprize_Update
	@Idx int, 
	@TurntableId int, -- 转盘项ID
	@TurntableType int, -- 转盘类型 1青铜 2白银 3黄金
	@PrizeType int, -- 奖励类型  1钻石  2金币  3指定物品  4 卡库 5转盘 6特殊处理的物品
	@SubType int, -- 二级分类
	@ItemCount int, -- 物品数量
	@SpecialString varchar(200), -- 特殊物品串
	@InitialRate int -- 初始概率
AS



UPDATE [dbo].[Config_TurntablePrize] SET
	[TurntableId] = @TurntableId
	,[TurntableType] = @TurntableType
	,[PrizeType] = @PrizeType
	,[SubType] = @SubType
	,[ItemCount] = @ItemCount
	,[SpecialString] = @SpecialString
	,[InitialRate] = @InitialRate
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


