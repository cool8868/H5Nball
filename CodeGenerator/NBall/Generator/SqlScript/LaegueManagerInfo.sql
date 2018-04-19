
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Laeguemanagerinfo_Delete    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LaegueManagerinfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LaegueManagerinfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].LaegueManagerinfo_GetById    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LaegueManagerinfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LaegueManagerinfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LaegueManagerinfo_GetAll    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LaegueManagerinfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LaegueManagerinfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LaegueManagerinfo_Insert    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LaegueManagerinfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LaegueManagerinfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].LaegueManagerinfo_Update    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LaegueManagerinfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LaegueManagerinfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LaegueManagerinfo_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Laegue_ManagerInfo]
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

CREATE PROCEDURE [dbo].P_LaegueManagerinfo_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Laegue_ManagerInfo] with(nolock)
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

CREATE PROCEDURE [dbo].P_LaegueManagerinfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Laegue_ManagerInfo] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LaegueManagerinfo_Insert
	@SumScore int , 
	@ExchangeIds varchar(200) , 
	@ExchangedIds varchar(200) , 
	@EquipmentItems varchar(100) , 
	@EquipmentProperties varchar(max) , 
	@RefreshDate datetime , 
	@RefreshTimes int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
	@DailyWinCount int , 
	@DailyWinUpdateTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Laegue_ManagerInfo] (
	[ManagerId],
	[SumScore]
	,[ExchangeIds]
	,[ExchangedIds]
	,[EquipmentItems]
	,[EquipmentProperties]
	,[RefreshDate]
	,[RefreshTimes]
	,[UpdateTime]
	,[RowTime]
	,[DailyWinCount]
	,[DailyWinUpdateTime]
) VALUES (
	@ManagerId,
    @SumScore
    ,@ExchangeIds
    ,@ExchangedIds
    ,@EquipmentItems
    ,@EquipmentProperties
    ,@RefreshDate
    ,@RefreshTimes
    ,@UpdateTime
    ,@RowTime
    ,@DailyWinCount
    ,@DailyWinUpdateTime
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

CREATE PROCEDURE [dbo].P_LaegueManagerinfo_Update
	@ManagerId uniqueidentifier, 
	@SumScore int, -- 联赛积分
	@ExchangeIds varchar(200), -- 可兑换物品 ExchangeId,ItemCode|ExchangeId,ItemCode
	@ExchangedIds varchar(200), -- 已兑换的物品 ExchangeId,ItemCode|ExchangeId,ItemCode
	@EquipmentItems varchar(100), 
	@EquipmentProperties varchar(max), 
	@RefreshDate datetime, -- 刷新时间
	@RefreshTimes int, -- 刷新次数
	@UpdateTime datetime, 
	@RowTime datetime, 
	@DailyWinCount int, 
	@DailyWinUpdateTime datetime 
AS



UPDATE [dbo].[Laegue_ManagerInfo] SET
	[SumScore] = @SumScore
	,[ExchangeIds] = @ExchangeIds
	,[ExchangedIds] = @ExchangedIds
	,[EquipmentItems] = @EquipmentItems
	,[EquipmentProperties] = @EquipmentProperties
	,[RefreshDate] = @RefreshDate
	,[RefreshTimes] = @RefreshTimes
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
	,[DailyWinCount] = @DailyWinCount
	,[DailyWinUpdateTime] = @DailyWinUpdateTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



