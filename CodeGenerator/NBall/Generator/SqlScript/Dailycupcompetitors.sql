
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dailycupcompetitors_Delete    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupCompetitors_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupCompetitors_Delete]
GO

/****** Object:  Stored Procedure [dbo].DailycupCompetitors_GetById    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupCompetitors_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupCompetitors_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DailycupCompetitors_GetAll    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupCompetitors_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupCompetitors_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DailycupCompetitors_Insert    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupCompetitors_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupCompetitors_Insert]
GO

/****** Object:  Stored Procedure [dbo].DailycupCompetitors_Update    Script Date: 2016年1月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupCompetitors_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupCompetitors_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DailycupCompetitors_Delete
	@Idx int
AS

DELETE FROM [dbo].[DailyCup_Competitors]
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

CREATE PROCEDURE [dbo].P_DailycupCompetitors_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyCup_Competitors] with(nolock)
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

CREATE PROCEDURE [dbo].P_DailycupCompetitors_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyCup_Competitors] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DailycupCompetitors_Insert
	@DailyCupId int , 
	@ManagerId uniqueidentifier , 
	@ManagerName nvarchar(50) , 
	@Logo varchar(200) , 
	@MaxRound int , 
	@WinCount int , 
	@Rank int , 
	@PrizeScore int , 
	@PrizeSophisticate int , 
	@PrizeCoin int , 
	@PrizeItems varchar(200) , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[DailyCup_Competitors] (
	[DailyCupId]
	,[ManagerId]
	,[ManagerName]
	,[Logo]
	,[MaxRound]
	,[WinCount]
	,[Rank]
	,[PrizeScore]
	,[PrizeSophisticate]
	,[PrizeCoin]
	,[PrizeItems]
	,[Status]
	,[RowTime]
) VALUES (
    @DailyCupId
    ,@ManagerId
    ,@ManagerName
    ,@Logo
    ,@MaxRound
    ,@WinCount
    ,@Rank
    ,@PrizeScore
    ,@PrizeSophisticate
    ,@PrizeCoin
    ,@PrizeItems
    ,@Status
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_DailycupCompetitors_Update
	@Idx int, 
	@DailyCupId int, -- 每日杯赛id
	@ManagerId uniqueidentifier, -- 报名经理id
	@ManagerName nvarchar(50), -- 经理名 
	@Logo varchar(200), 
	@MaxRound int, 
	@WinCount int, 
	@Rank int, -- 排名
	@PrizeScore int, -- 获得的积分
	@PrizeSophisticate int, 
	@PrizeCoin int, 
	@PrizeItems varchar(200), -- 奖励物品
	@Status int, -- 状态：0，初始；1，已发奖；
	@RowTime datetime 
AS



UPDATE [dbo].[DailyCup_Competitors] SET
	[DailyCupId] = @DailyCupId
	,[ManagerId] = @ManagerId
	,[ManagerName] = @ManagerName
	,[Logo] = @Logo
	,[MaxRound] = @MaxRound
	,[WinCount] = @WinCount
	,[Rank] = @Rank
	,[PrizeScore] = @PrizeScore
	,[PrizeSophisticate] = @PrizeSophisticate
	,[PrizeCoin] = @PrizeCoin
	,[PrizeItems] = @PrizeItems
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



