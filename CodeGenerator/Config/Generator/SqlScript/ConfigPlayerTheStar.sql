
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configplayerthestar_Delete    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerthestar_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerthestar_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigPlayerthestar_GetById    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerthestar_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerthestar_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigPlayerthestar_GetAll    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerthestar_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerthestar_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigPlayerthestar_Insert    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerthestar_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerthestar_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigPlayerthestar_Update    Script Date: 2016年7月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerthestar_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerthestar_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigPlayerthestar_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_PlayerTheStar]
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

CREATE PROCEDURE [dbo].P_ConfigPlayerthestar_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PlayerTheStar] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigPlayerthestar_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PlayerTheStar] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigPlayerthestar_Insert
	@Idx int
	,@Exp int
	,@Coin int
	,@PlayerCard int
	,@PotentialCount int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_PlayerTheStar] (
	[Idx]
	,[Exp]
	,[Coin]
	,[PlayerCard]
	,[PotentialCount]
) VALUES (
    @Idx
    ,@Exp
    ,@Coin
    ,@PlayerCard
    ,@PotentialCount
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

CREATE PROCEDURE [dbo].P_ConfigPlayerthestar_Update
	@Idx int, 
	@Exp int, -- 需要星级
	@Coin int, -- 每次升星需要金币
	@PlayerCard int, -- 需要球员卡数量
	@PotentialCount int -- 可获得潜力数量
AS



UPDATE [dbo].[Config_PlayerTheStar] SET
	[Exp] = @Exp
	,[Coin] = @Coin
	,[PlayerCard] = @PlayerCard
	,[PotentialCount] = @PotentialCount
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


