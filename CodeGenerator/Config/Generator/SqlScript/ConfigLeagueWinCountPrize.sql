
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configleaguewincountprize_Delete    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguewincountprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguewincountprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguewincountprize_GetById    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguewincountprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguewincountprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLeaguewincountprize_GetAll    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguewincountprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguewincountprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLeaguewincountprize_Insert    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguewincountprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguewincountprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeaguewincountprize_Update    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeaguewincountprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeaguewincountprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLeaguewincountprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_LeagueWinCountPrize]
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

CREATE PROCEDURE [dbo].P_ConfigLeaguewincountprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueWinCountPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigLeaguewincountprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LeagueWinCountPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLeaguewincountprize_Insert
	@LeagueId int , 
	@WinCount int , 
	@PrizePoint int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_LeagueWinCountPrize] (
	[LeagueId]
	,[WinCount]
	,[PrizePoint]
) VALUES (
    @LeagueId
    ,@WinCount
    ,@PrizePoint
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

CREATE PROCEDURE [dbo].P_ConfigLeaguewincountprize_Update
	@Idx int, 
	@LeagueId int, -- 联赛类型
	@WinCount int, -- 获胜场次
	@PrizePoint int -- 奖励钻石数量
AS



UPDATE [dbo].[Config_LeagueWinCountPrize] SET
	[LeagueId] = @LeagueId
	,[WinCount] = @WinCount
	,[PrizePoint] = @PrizePoint
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



