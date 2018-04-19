
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configsynthesis_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesis_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesis_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigSynthesis_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesis_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesis_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigSynthesis_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesis_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesis_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigSynthesis_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesis_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesis_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigSynthesis_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesis_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesis_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigSynthesis_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Synthesis]
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

CREATE PROCEDURE [dbo].P_ConfigSynthesis_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Synthesis] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigSynthesis_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Synthesis] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigSynthesis_Insert
	@Idx int
	,@CardLevel int
	,@Rate int
	,@Coin int
	,@CardLibrary int
	,@ProtectCode int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Synthesis] (
	[Idx]
	,[CardLevel]
	,[Rate]
	,[Coin]
	,[CardLibrary]
	,[ProtectCode]
) VALUES (
    @Idx
    ,@CardLevel
    ,@Rate
    ,@Coin
    ,@CardLibrary
    ,@ProtectCode
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

CREATE PROCEDURE [dbo].P_ConfigSynthesis_Update
	@Idx int, 
	@CardLevel int, -- 球员卡颜色
	@Rate int, -- 概率(0-10000)
	@Coin int, -- 消耗游戏币
	@CardLibrary int, -- 合成结果对应的卡库
	@ProtectCode int -- 合成保护商品编码
AS



UPDATE [dbo].[Config_Synthesis] SET
	[CardLevel] = @CardLevel
	,[Rate] = @Rate
	,[Coin] = @Coin
	,[CardLibrary] = @CardLibrary
	,[ProtectCode] = @ProtectCode
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



