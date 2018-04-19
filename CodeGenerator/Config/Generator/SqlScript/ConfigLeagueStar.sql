
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configleaguestar_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguestar_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguestar_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguestar_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguestar_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguestar_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLeaguestar_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguestar_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguestar_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLeaguestar_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguestar_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguestar_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguestar_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguestar_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguestar_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLeaguestar_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_LeagueStar]
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

CREATE PROCEDURE [dbo].P_ConfigLeaguestar_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueStar] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigLeaguestar_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueStar] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLeaguestar_Insert
	@Idx int
	,@LeagueId int
	,@StarNumber int
	,@PrizeLevel int
	,@PrizeType int
	,@SubType int
	,@Count int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_LeagueStar] (
	[Idx]
	,[LeagueId]
	,[StarNumber]
	,[PrizeLevel]
	,[PrizeType]
	,[SubType]
	,[Count]
) VALUES (
    @Idx
    ,@LeagueId
    ,@StarNumber
    ,@PrizeLevel
    ,@PrizeType
    ,@SubType
    ,@Count
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

CREATE PROCEDURE [dbo].P_ConfigLeaguestar_Update
	@Idx int, 
	@LeagueId int, -- 联赛ID
	@StarNumber int, -- 需要星星数量
	@PrizeLevel int, -- 奖励等级
	@PrizeType int, -- 奖励物品类型 1钻石 2金币 3物品
	@SubType int, -- 物品code
	@Count int -- 数量
AS



UPDATE [dbo].[Config_LeagueStar] SET
	[LeagueId] = @LeagueId
	,[StarNumber] = @StarNumber
	,[PrizeLevel] = @PrizeLevel
	,[PrizeType] = @PrizeType
	,[SubType] = @SubType
	,[Count] = @Count
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



