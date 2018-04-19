
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configdecompose_Delete    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDecompose_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDecompose_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigDecompose_GetById    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDecompose_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDecompose_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigDecompose_GetAll    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDecompose_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDecompose_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigDecompose_Insert    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDecompose_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDecompose_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigDecompose_Update    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDecompose_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDecompose_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigDecompose_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Decompose]
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

CREATE PROCEDURE [dbo].P_ConfigDecompose_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Decompose] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigDecompose_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Decompose] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigDecompose_Insert
	@Idx int
	,@CardLevel int
	,@Coin int
	,@CritiRate int
	,@EquipmentRate int
	,@EquipmentLotteryId int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Decompose] (
	[Idx]
	,[CardLevel]
	,[Coin]
	,[CritiRate]
	,[EquipmentRate]
	,[EquipmentLotteryId]
) VALUES (
    @Idx
    ,@CardLevel
    ,@Coin
    ,@CritiRate
    ,@EquipmentRate
    ,@EquipmentLotteryId
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

CREATE PROCEDURE [dbo].P_ConfigDecompose_Update
	@Idx int, 
	@CardLevel int, -- 卡牌颜色
	@Coin int, -- 金币
	@CritiRate int, -- 暴击率
	@EquipmentRate int, -- 分解获得装备概率
	@EquipmentLotteryId int -- 装备抽取id
AS



UPDATE [dbo].[Config_Decompose] SET
	[CardLevel] = @CardLevel
	,[Coin] = @Coin
	,[CritiRate] = @CritiRate
	,[EquipmentRate] = @EquipmentRate
	,[EquipmentLotteryId] = @EquipmentLotteryId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



