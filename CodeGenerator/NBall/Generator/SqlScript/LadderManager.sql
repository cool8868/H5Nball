
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Laddermanager_Delete    Script Date: 2016年9月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].LadderManager_GetById    Script Date: 2016年9月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LadderManager_GetAll    Script Date: 2016年9月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LadderManager_Insert    Script Date: 2016年9月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].LadderManager_Update    Script Date: 2016年9月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LadderManager_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Ladder_Manager]
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_LadderManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_LadderManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LadderManager_Insert
	@Score int , 
	@NewlyScore int , 
	@NewlyHonor int , 
	@Honor int , 
	@MaxScore int , 
	@MatchTime int , 
	@LastExchageTime datetime , 
	@ExchangeIds varchar(200) , 
	@ExchangedIds varchar(200) , 
	@RefreshDate datetime , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@RefreshTimes int , 
	@EquipmentProperties varchar(max) , 
	@EquipmentItems varchar(100) , 
	@LadderCoin int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Ladder_Manager] (
	[ManagerId],
	[Score]
	,[NewlyScore]
	,[NewlyHonor]
	,[Honor]
	,[MaxScore]
	,[MatchTime]
	,[LastExchageTime]
	,[ExchangeIds]
	,[ExchangedIds]
	,[RefreshDate]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[RefreshTimes]
	,[EquipmentProperties]
	,[EquipmentItems]
	,[LadderCoin]
) VALUES (
	@ManagerId,
    @Score
    ,@NewlyScore
    ,@NewlyHonor
    ,@Honor
    ,@MaxScore
    ,@MatchTime
    ,@LastExchageTime
    ,@ExchangeIds
    ,@ExchangedIds
    ,@RefreshDate
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@RefreshTimes
    ,@EquipmentProperties
    ,@EquipmentItems
    ,@LadderCoin
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

CREATE PROCEDURE [dbo].P_LadderManager_Update
	@ManagerId uniqueidentifier, 
	@Score int, -- 天梯积分
	@NewlyScore int, -- 最近增加积分
	@NewlyHonor int, -- 最近兑换荣誉数量
	@Honor int, -- 荣誉数量
	@MaxScore int, -- 最大积分
	@MatchTime int, -- 今日比赛场次
	@LastExchageTime datetime, -- 最近兑换时间
	@ExchangeIds varchar(200), -- ExchangeId,ItemCode|ExchangeId,ItemCode
	@ExchangedIds varchar(200), -- 已兑换的物品 ExchangeId,ItemCode|ExchangeId,ItemCode
	@RefreshDate datetime, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@RowVersion timestamp, 
	@RefreshTimes int, 
	@EquipmentProperties varchar(max), 
	@EquipmentItems varchar(100), 
	@LadderCoin int 
AS



UPDATE [dbo].[Ladder_Manager] SET
	[Score] = @Score
	,[NewlyScore] = @NewlyScore
	,[NewlyHonor] = @NewlyHonor
	,[Honor] = @Honor
	,[MaxScore] = @MaxScore
	,[MatchTime] = @MatchTime
	,[LastExchageTime] = @LastExchageTime
	,[ExchangeIds] = @ExchangeIds
	,[ExchangedIds] = @ExchangedIds
	,[RefreshDate] = @RefreshDate
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[RefreshTimes] = @RefreshTimes
	,[EquipmentProperties] = @EquipmentProperties
	,[EquipmentItems] = @EquipmentItems
	,[LadderCoin] = @LadderCoin
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


