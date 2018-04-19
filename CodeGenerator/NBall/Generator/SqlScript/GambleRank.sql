
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gamblerank_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRank_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRank_Delete]
GO

/****** Object:  Stored Procedure [dbo].GambleRank_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRank_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRank_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GambleRank_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRank_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRank_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GambleRank_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRank_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRank_Insert]
GO

/****** Object:  Stored Procedure [dbo].GambleRank_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRank_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRank_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GambleRank_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Gamble_Rank]
WHERE
	[ManagerId] = @ManagerId

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

CREATE PROCEDURE [dbo].P_GambleRank_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Rank] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
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

CREATE PROCEDURE [dbo].P_GambleRank_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Rank] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GambleRank_Insert
	@ManagerName nvarchar(50) , 
	@RankIndex int , 
	@RankType int , 
	@WinTotalMoney int , 
	@Status int , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Gamble_Rank] (
	[ManagerId],
	[ManagerName]
	,[RankIndex]
	,[RankType]
	,[WinTotalMoney]
	,[Status]
	,[RowTime]
) VALUES (
	@ManagerId,
    @ManagerName
    ,@RankIndex
    ,@RankType
    ,@WinTotalMoney
    ,@Status
    ,@RowTime
)




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

CREATE PROCEDURE [dbo].P_GambleRank_Update
	@ManagerId uniqueidentifier, 
	@ManagerName nvarchar(50), -- 经理名
	@RankIndex int, 
	@RankType int, 
	@WinTotalMoney int, 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Gamble_Rank] SET
	[ManagerName] = @ManagerName
	,[RankIndex] = @RankIndex
	,[RankType] = @RankType
	,[WinTotalMoney] = @WinTotalMoney
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


