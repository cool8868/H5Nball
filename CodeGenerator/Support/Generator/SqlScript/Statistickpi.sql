
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Statistickpi_Delete    Script Date: 2016年7月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticKpi_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticKpi_Delete]
GO

/****** Object:  Stored Procedure [dbo].StatisticKpi_GetById    Script Date: 2016年7月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticKpi_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticKpi_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].StatisticKpi_GetAll    Script Date: 2016年7月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticKpi_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticKpi_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].StatisticKpi_Insert    Script Date: 2016年7月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticKpi_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticKpi_Insert]
GO

/****** Object:  Stored Procedure [dbo].StatisticKpi_Update    Script Date: 2016年7月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticKpi_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticKpi_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_StatisticKpi_Delete
	@Idx bigint
AS

DELETE FROM [dbo].[Statistic_Kpi]
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

CREATE PROCEDURE [dbo].P_StatisticKpi_GetById
	@Idx bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Kpi] with(nolock)
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

CREATE PROCEDURE [dbo].P_StatisticKpi_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Kpi] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_StatisticKpi_Insert
	@ZoneId int , 
	@RecordMonth char(6) , 
	@RecordDate datetime , 
	@TotalUser int , 
	@TotalManager int , 
	@Dau int , 
	@DUniqueIp int , 
	@DNewUser int , 
	@DNewManager int , 
	@DLostUser7 int , 
	@DLostUser15 int , 
	@DLostUser30 int , 
	@Retention2 int , 
	@Retention3 int , 
	@Retention4 int , 
	@Retention5 int , 
	@Retention6 int , 
	@Retention7 int , 
	@Retention15 int , 
	@Retention30 int , 
	@Acu int , 
	@Pcu int , 
	@Lcu int , 
	@TotalOnline bigint , 
	@Wau int , 
	@WLost int , 
	@WHonor int , 
	@WHonorLost int , 
	@Mau int , 
	@PayUserCount int , 
	@PayCount int , 
	@PayTotal int , 
	@PaySum bigint , 
	@PayFirst int , 
	@PointRemain bigint , 
	@PointConsume bigint , 
	@PointCirculate bigint , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@GetPoint int , 
	@GetCoin bigint , 
	@CoinConsume bigint , 
	@EnergyConsume int , 
    @Idx bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Statistic_Kpi] (
	[ZoneId]
	,[RecordMonth]
	,[RecordDate]
	,[TotalUser]
	,[TotalManager]
	,[Dau]
	,[DUniqueIp]
	,[DNewUser]
	,[DNewManager]
	,[DLostUser7]
	,[DLostUser15]
	,[DLostUser30]
	,[Retention2]
	,[Retention3]
	,[Retention4]
	,[Retention5]
	,[Retention6]
	,[Retention7]
	,[Retention15]
	,[Retention30]
	,[Acu]
	,[Pcu]
	,[Lcu]
	,[TotalOnline]
	,[Wau]
	,[WLost]
	,[WHonor]
	,[WHonorLost]
	,[Mau]
	,[PayUserCount]
	,[PayCount]
	,[PayTotal]
	,[PaySum]
	,[PayFirst]
	,[PointRemain]
	,[PointConsume]
	,[PointCirculate]
	,[RowTime]
	,[UpdateTime]
	,[GetPoint]
	,[GetCoin]
	,[CoinConsume]
	,[EnergyConsume]
) VALUES (
    @ZoneId
    ,@RecordMonth
    ,@RecordDate
    ,@TotalUser
    ,@TotalManager
    ,@Dau
    ,@DUniqueIp
    ,@DNewUser
    ,@DNewManager
    ,@DLostUser7
    ,@DLostUser15
    ,@DLostUser30
    ,@Retention2
    ,@Retention3
    ,@Retention4
    ,@Retention5
    ,@Retention6
    ,@Retention7
    ,@Retention15
    ,@Retention30
    ,@Acu
    ,@Pcu
    ,@Lcu
    ,@TotalOnline
    ,@Wau
    ,@WLost
    ,@WHonor
    ,@WHonorLost
    ,@Mau
    ,@PayUserCount
    ,@PayCount
    ,@PayTotal
    ,@PaySum
    ,@PayFirst
    ,@PointRemain
    ,@PointConsume
    ,@PointCirculate
    ,@RowTime
    ,@UpdateTime
    ,@GetPoint
    ,@GetCoin
    ,@CoinConsume
    ,@EnergyConsume
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

CREATE PROCEDURE [dbo].P_StatisticKpi_Update
	@Idx bigint, 
	@ZoneId int, 
	@RecordMonth char(6), -- 所属月份
	@RecordDate datetime, -- 统计日期
	@TotalUser int, 
	@TotalManager int, 
	@Dau int, -- 日活跃用户
	@DUniqueIp int, -- 唯一Ip数
	@DNewUser int, -- 新用户
	@DNewManager int, -- 新经理
	@DLostUser7 int, -- 流失用户7天
	@DLostUser15 int, -- 流失用户15
	@DLostUser30 int, -- 流失用户30
	@Retention2 int, -- 次日留存
	@Retention3 int, -- 3日留存
	@Retention4 int, 
	@Retention5 int, 
	@Retention6 int, 
	@Retention7 int, 
	@Retention15 int, 
	@Retention30 int, 
	@Acu int, -- 平均同时在线
	@Pcu int, -- 最高同时在线
	@Lcu int, 
	@TotalOnline bigint, -- 总在线时长
	@Wau int, -- 周活跃用户
	@WLost int, -- 周流失用户
	@WHonor int, -- 周忠诚用户数
	@WHonorLost int, 
	@Mau int, -- 月活跃用户
	@PayUserCount int, -- 充值人数
	@PayCount int, 
	@PayTotal int, -- 充值金额
	@PaySum bigint, -- 该服务器总充值
	@PayFirst int, -- 首充人数
	@PointRemain bigint, -- 剩余点券
	@PointConsume bigint, -- 消耗点券
	@PointCirculate bigint, -- 流通点券
	@RowTime datetime, 
	@UpdateTime datetime, 
	@GetPoint int, 
	@GetCoin bigint, 
	@CoinConsume bigint, 
	@EnergyConsume int 
AS



UPDATE [dbo].[Statistic_Kpi] SET
	[ZoneId] = @ZoneId
	,[RecordMonth] = @RecordMonth
	,[RecordDate] = @RecordDate
	,[TotalUser] = @TotalUser
	,[TotalManager] = @TotalManager
	,[Dau] = @Dau
	,[DUniqueIp] = @DUniqueIp
	,[DNewUser] = @DNewUser
	,[DNewManager] = @DNewManager
	,[DLostUser7] = @DLostUser7
	,[DLostUser15] = @DLostUser15
	,[DLostUser30] = @DLostUser30
	,[Retention2] = @Retention2
	,[Retention3] = @Retention3
	,[Retention4] = @Retention4
	,[Retention5] = @Retention5
	,[Retention6] = @Retention6
	,[Retention7] = @Retention7
	,[Retention15] = @Retention15
	,[Retention30] = @Retention30
	,[Acu] = @Acu
	,[Pcu] = @Pcu
	,[Lcu] = @Lcu
	,[TotalOnline] = @TotalOnline
	,[Wau] = @Wau
	,[WLost] = @WLost
	,[WHonor] = @WHonor
	,[WHonorLost] = @WHonorLost
	,[Mau] = @Mau
	,[PayUserCount] = @PayUserCount
	,[PayCount] = @PayCount
	,[PayTotal] = @PayTotal
	,[PaySum] = @PaySum
	,[PayFirst] = @PayFirst
	,[PointRemain] = @PointRemain
	,[PointConsume] = @PointConsume
	,[PointCirculate] = @PointCirculate
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[GetPoint] = @GetPoint
	,[GetCoin] = @GetCoin
	,[CoinConsume] = @CoinConsume
	,[EnergyConsume] = @EnergyConsume
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


