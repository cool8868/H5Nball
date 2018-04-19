
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configsynthesiscard_Delete    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesiscard_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesiscard_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigSynthesiscard_GetById    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesiscard_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesiscard_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigSynthesiscard_GetAll    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesiscard_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesiscard_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigSynthesiscard_Insert    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesiscard_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesiscard_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigSynthesiscard_Update    Script Date: 2016年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSynthesiscard_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSynthesiscard_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigSynthesiscard_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_SynthesisCard]
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

CREATE PROCEDURE [dbo].P_ConfigSynthesiscard_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SynthesisCard] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigSynthesiscard_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_SynthesisCard] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigSynthesiscard_Insert
	@CardLevel int , 
	@Coin int , 
	@ProtectPoint int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_SynthesisCard] (
	[CardLevel]
	,[Coin]
	,[ProtectPoint]
) VALUES (
    @CardLevel
    ,@Coin
    ,@ProtectPoint
)


SET @Idx = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_ConfigSynthesiscard_Update
	@Idx int, 
	@CardLevel int, -- 球员卡级别
	@Coin int, -- 消耗金币
	@ProtectPoint int -- 合成保护消耗点券
AS



UPDATE [dbo].[Config_SynthesisCard] SET
	[CardLevel] = @CardLevel
	,[Coin] = @Coin
	,[ProtectPoint] = @ProtectPoint
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



