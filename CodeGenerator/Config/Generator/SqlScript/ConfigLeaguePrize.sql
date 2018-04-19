
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configleagueprize_Delete    Script Date: 2015年10月31日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeagueprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeagueprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeagueprize_GetById    Script Date: 2015年10月31日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeagueprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeagueprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLeagueprize_GetAll    Script Date: 2015年10月31日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeagueprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeagueprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLeagueprize_Insert    Script Date: 2015年10月31日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeagueprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeagueprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeagueprize_Update    Script Date: 2015年10月31日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeagueprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeagueprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLeagueprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_LeaguePrize]
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

CREATE PROCEDURE [dbo].P_ConfigLeagueprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeaguePrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigLeagueprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeaguePrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLeagueprize_Insert
	@Idx int
	,@LeagueID int
	,@ResultType int
	,@PrizeType int
	,@ItemCode int
	,@Count int
	,@IsBindIng bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_LeaguePrize] (
	[Idx]
	,[LeagueID]
	,[ResultType]
	,[PrizeType]
	,[ItemCode]
	,[Count]
	,[IsBindIng]
) VALUES (
    @Idx
    ,@LeagueID
    ,@ResultType
    ,@PrizeType
    ,@ItemCode
    ,@Count
    ,@IsBindIng
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

CREATE PROCEDURE [dbo].P_ConfigLeagueprize_Update
	@Idx int, 
	@LeagueID int, 
	@ResultType int, -- 1:胜，2：平，3：负，4：第一次获得冠军，5：第一次之后获得冠军
	@PrizeType int, -- 1：经验，2：金币，3：联赛积分,4:点卷，5：物品，6：卡库
	@ItemCode int, 
	@Count int, 
	@IsBindIng bit 
AS



UPDATE [dbo].[Config_LeaguePrize] SET
	[LeagueID] = @LeagueID
	,[ResultType] = @ResultType
	,[PrizeType] = @PrizeType
	,[ItemCode] = @ItemCode
	,[Count] = @Count
	,[IsBindIng] = @IsBindIng
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



