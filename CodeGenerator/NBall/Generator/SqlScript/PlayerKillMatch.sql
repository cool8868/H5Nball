
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Playerkillmatch_Delete    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillMatch_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillMatch_Delete]
GO

/****** Object:  Stored Procedure [dbo].PlayerkillMatch_GetById    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillMatch_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillMatch_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PlayerkillMatch_GetAll    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillMatch_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillMatch_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PlayerkillMatch_Insert    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillMatch_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillMatch_Insert]
GO

/****** Object:  Stored Procedure [dbo].PlayerkillMatch_Update    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PlayerkillMatch_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PlayerkillMatch_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PlayerkillMatch_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[PlayerKill_Match]
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

CREATE PROCEDURE [dbo].P_PlayerkillMatch_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PlayerKill_Match] with(nolock)
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

CREATE PROCEDURE [dbo].P_PlayerkillMatch_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[PlayerKill_Match] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PlayerkillMatch_Insert
	@HomeId uniqueidentifier , 
	@AwayId uniqueidentifier , 
	@HomeName nvarchar(50) , 
	@AwayName nvarchar(50) , 
	@HomeScore int , 
	@AwayScore int , 
	@PrizeExp int , 
	@PrizeCoin int , 
	@PrizeItemCode int , 
	@PrizeItemString varchar(100) , 
	@PrizeItemCount int , 
	@IsRevenge bit , 
	@Status int , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[PlayerKill_Match] (
	[Idx],
	[HomeId]
	,[AwayId]
	,[HomeName]
	,[AwayName]
	,[HomeScore]
	,[AwayScore]
	,[PrizeExp]
	,[PrizeCoin]
	,[PrizeItemCode]
	,[PrizeItemString]
	,[PrizeItemCount]
	,[IsRevenge]
	,[Status]
	,[RowTime]
) VALUES (
	@Idx,
    @HomeId
    ,@AwayId
    ,@HomeName
    ,@AwayName
    ,@HomeScore
    ,@AwayScore
    ,@PrizeExp
    ,@PrizeCoin
    ,@PrizeItemCode
    ,@PrizeItemString
    ,@PrizeItemCount
    ,@IsRevenge
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

CREATE PROCEDURE [dbo].P_PlayerkillMatch_Update
	@Idx uniqueidentifier, -- 比赛id
	@HomeId uniqueidentifier, -- 主队经理id
	@AwayId uniqueidentifier, -- 客队 id
	@HomeName nvarchar(50), -- 主队名
	@AwayName nvarchar(50), -- 客队名
	@HomeScore int, -- 主队比分
	@AwayScore int, -- 客队比分
	@PrizeExp int, 
	@PrizeCoin int, 
	@PrizeItemCode int, 
	@PrizeItemString varchar(100), 
	@PrizeItemCount int, 
	@IsRevenge bit, 
	@Status int, 
	@RowTime datetime -- 记录时间
AS



UPDATE [dbo].[PlayerKill_Match] SET
	[HomeId] = @HomeId
	,[AwayId] = @AwayId
	,[HomeName] = @HomeName
	,[AwayName] = @AwayName
	,[HomeScore] = @HomeScore
	,[AwayScore] = @AwayScore
	,[PrizeExp] = @PrizeExp
	,[PrizeCoin] = @PrizeCoin
	,[PrizeItemCode] = @PrizeItemCode
	,[PrizeItemString] = @PrizeItemString
	,[PrizeItemCount] = @PrizeItemCount
	,[IsRevenge] = @IsRevenge
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


