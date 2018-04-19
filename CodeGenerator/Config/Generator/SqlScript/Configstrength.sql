
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configstrength_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigStrength_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigStrength_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigStrength_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigStrength_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigStrength_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigStrength_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigStrength_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigStrength_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigStrength_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigStrength_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigStrength_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigStrength_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigStrength_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigStrength_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigStrength_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Strength]
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

CREATE PROCEDURE [dbo].P_ConfigStrength_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Strength] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigStrength_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Strength] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigStrength_Insert
	@Idx int
	,@CardLevel int
	,@Source int
	,@Target int
	,@Result int
	,@Rate int
	,@Coin int
	,@ProtectPoint int
	,@ShowRate int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Strength] (
	[Idx]
	,[CardLevel]
	,[Source]
	,[Target]
	,[Result]
	,[Rate]
	,[Coin]
	,[ProtectPoint]
	,[ShowRate]
) VALUES (
    @Idx
    ,@CardLevel
    ,@Source
    ,@Target
    ,@Result
    ,@Rate
    ,@Coin
    ,@ProtectPoint
    ,@ShowRate
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

CREATE PROCEDURE [dbo].P_ConfigStrength_Update
	@Idx int, 
	@CardLevel int, -- 球员卡颜色
	@Source int, -- 源卡强化级别
	@Target int, -- 目标卡强化级别
	@Result int, -- 结果强化级别
	@Rate int, -- 成功率(0-10000)
	@Coin int, -- 消耗游戏币
	@ProtectPoint int, 
	@ShowRate int 
AS



UPDATE [dbo].[Config_Strength] SET
	[CardLevel] = @CardLevel
	,[Source] = @Source
	,[Target] = @Target
	,[Result] = @Result
	,[Rate] = @Rate
	,[Coin] = @Coin
	,[ProtectPoint] = @ProtectPoint
	,[ShowRate] = @ShowRate
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



