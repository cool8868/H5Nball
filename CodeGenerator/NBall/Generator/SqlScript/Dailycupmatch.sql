
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dailycupmatch_Delete    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupMatch_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupMatch_Delete]
GO

/****** Object:  Stored Procedure [dbo].DailycupMatch_GetById    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupMatch_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupMatch_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DailycupMatch_GetAll    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupMatch_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupMatch_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DailycupMatch_Insert    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupMatch_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupMatch_Insert]
GO

/****** Object:  Stored Procedure [dbo].DailycupMatch_Update    Script Date: 2016年1月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailycupMatch_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailycupMatch_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DailycupMatch_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[DailyCup_Match]
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

CREATE PROCEDURE [dbo].P_DailycupMatch_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyCup_Match] with(nolock)
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

CREATE PROCEDURE [dbo].P_DailycupMatch_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyCup_Match] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DailycupMatch_Insert
	@DailyCupId int , 
	@HomeManager uniqueidentifier , 
	@AwayManager uniqueidentifier , 
	@HomeName nvarchar(50) , 
	@AwayName nvarchar(50) , 
	@HomeLogo varchar(200) , 
	@AwayLogo varchar(200) , 
	@HomeLevel int , 
	@AwayLevel int , 
	@HomePower int , 
	@AwayPower int , 
	@HomeWorldScore int , 
	@AwayWorldScore int , 
	@HomeScore int , 
	@AwayScore int , 
	@Round int , 
	@ChipInCount int , 
	@Status int , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[DailyCup_Match] (
	[Idx],
	[DailyCupId]
	,[HomeManager]
	,[AwayManager]
	,[HomeName]
	,[AwayName]
	,[HomeLogo]
	,[AwayLogo]
	,[HomeLevel]
	,[AwayLevel]
	,[HomePower]
	,[AwayPower]
	,[HomeWorldScore]
	,[AwayWorldScore]
	,[HomeScore]
	,[AwayScore]
	,[Round]
	,[ChipInCount]
	,[Status]
	,[RowTime]
) VALUES (
	@Idx,
    @DailyCupId
    ,@HomeManager
    ,@AwayManager
    ,@HomeName
    ,@AwayName
    ,@HomeLogo
    ,@AwayLogo
    ,@HomeLevel
    ,@AwayLevel
    ,@HomePower
    ,@AwayPower
    ,@HomeWorldScore
    ,@AwayWorldScore
    ,@HomeScore
    ,@AwayScore
    ,@Round
    ,@ChipInCount
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

CREATE PROCEDURE [dbo].P_DailycupMatch_Update
	@Idx uniqueidentifier, 
	@DailyCupId int, -- 杯赛ID
	@HomeManager uniqueidentifier, -- 主队经理的GUID
	@AwayManager uniqueidentifier, -- 客队经理的GUID
	@HomeName nvarchar(50), -- 主队经理名
	@AwayName nvarchar(50), -- 客队经理名
	@HomeLogo varchar(200), 
	@AwayLogo varchar(200), 
	@HomeLevel int, -- 主队等级
	@AwayLevel int, -- 客队等级
	@HomePower int, -- 主队综合实力
	@AwayPower int, -- 客队综合实力
	@HomeWorldScore int, -- 主队世界杯积分
	@AwayWorldScore int, -- 客队世界杯积分
	@HomeScore int, -- 主队比分
	@AwayScore int, -- 客队比分
	@Round int, -- 轮次
	@ChipInCount int, -- 投注数量
	@Status int, -- 状态:0,正常；
	@RowTime datetime -- 创建时间
AS



UPDATE [dbo].[DailyCup_Match] SET
	[DailyCupId] = @DailyCupId
	,[HomeManager] = @HomeManager
	,[AwayManager] = @AwayManager
	,[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[HomeLogo] = @HomeLogo
	,[AwayLogo] = @AwayLogo
	,[HomeLevel] = @HomeLevel
	,[AwayLevel] = @AwayLevel
	,[HomePower] = @HomePower
	,[AwayPower] = @AwayPower
	,[HomeWorldScore] = @HomeWorldScore
	,[AwayWorldScore] = @AwayWorldScore
	,[HomeScore] = @HomeScore
	,[AwayScore] = @AwayScore
	,[Round] = @Round
	,[ChipInCount] = @ChipInCount
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



