
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configarenaprize_Delete    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenaprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenaprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenaprize_GetById    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenaprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenaprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigArenaprize_GetAll    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenaprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenaprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigArenaprize_Insert    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenaprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenaprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenaprize_Update    Script Date: 2016年8月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenaprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenaprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigArenaprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_ArenaPrize]
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

CREATE PROCEDURE [dbo].P_ConfigArenaprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigArenaprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigArenaprize_Insert
	@Idx int
	,@StartRank int
	,@EndRank int
	,@PrizeType int
	,@PrizeCode int
	,@PrizeNumber int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_ArenaPrize] (
	[Idx]
	,[StartRank]
	,[EndRank]
	,[PrizeType]
	,[PrizeCode]
	,[PrizeNumber]
) VALUES (
    @Idx
    ,@StartRank
    ,@EndRank
    ,@PrizeType
    ,@PrizeCode
    ,@PrizeNumber
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

CREATE PROCEDURE [dbo].P_ConfigArenaprize_Update
	@Idx int, 
	@StartRank int, -- 起始排名
	@EndRank int, -- 截止排名
	@PrizeType int, -- 奖励类型
	@PrizeCode int, -- 奖励code
	@PrizeNumber int -- 奖励数量
AS



UPDATE [dbo].[Config_ArenaPrize] SET
	[StartRank] = @StartRank
	,[EndRank] = @EndRank
	,[PrizeType] = @PrizeType
	,[PrizeCode] = @PrizeCode
	,[PrizeNumber] = @PrizeNumber
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


