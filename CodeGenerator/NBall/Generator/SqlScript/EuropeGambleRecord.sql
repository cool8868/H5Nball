
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Europegamblerecord_Delete    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamblerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamblerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].EuropeGamblerecord_GetById    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamblerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamblerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].EuropeGamblerecord_GetAll    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamblerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamblerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].EuropeGamblerecord_Insert    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamblerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamblerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].EuropeGamblerecord_Update    Script Date: 2016年6月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamblerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamblerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_EuropeGamblerecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Europe_GambleRecord]
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

CREATE PROCEDURE [dbo].P_EuropeGamblerecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_GambleRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_EuropeGamblerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_GambleRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_EuropeGamblerecord_Insert
	@ManagerId uniqueidentifier , 
	@MatchId int , 
	@GambleType int , 
	@Point int , 
	@ReturnPoint int , 
	@IsSendPrize bit , 
	@IsGambleCorrect bit , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Europe_GambleRecord] (
	[ManagerId]
	,[MatchId]
	,[GambleType]
	,[Point]
	,[ReturnPoint]
	,[IsSendPrize]
	,[IsGambleCorrect]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@MatchId
    ,@GambleType
    ,@Point
    ,@ReturnPoint
    ,@IsSendPrize
    ,@IsGambleCorrect
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_EuropeGamblerecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@MatchId int, 
	@GambleType int, -- 竞猜类型 1主队胜 2平 3客队胜
	@Point int, -- 押注点卷
	@ReturnPoint int, 
	@IsSendPrize bit, -- 是否发奖
	@IsGambleCorrect bit, -- 是否竞猜正确
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Europe_GambleRecord] SET
	[ManagerId] = @ManagerId
	,[MatchId] = @MatchId
	,[GambleType] = @GambleType
	,[Point] = @Point
	,[ReturnPoint] = @ReturnPoint
	,[IsSendPrize] = @IsSendPrize
	,[IsGambleCorrect] = @IsGambleCorrect
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



