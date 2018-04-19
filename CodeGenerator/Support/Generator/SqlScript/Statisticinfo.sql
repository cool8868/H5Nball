
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Statisticinfo_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticInfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticInfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].StatisticInfo_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticInfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticInfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].StatisticInfo_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticInfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticInfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].StatisticInfo_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticInfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticInfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].StatisticInfo_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticInfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticInfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_StatisticInfo_Delete
	@ZoneId int
AS

DELETE FROM [dbo].[Statistic_Info]
WHERE
	[ZoneId] = @ZoneId

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

CREATE PROCEDURE [dbo].P_StatisticInfo_GetById
	@ZoneId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Info] with(nolock)
WHERE
	[ZoneId] = @ZoneId
	
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

CREATE PROCEDURE [dbo].P_StatisticInfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Info] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_StatisticInfo_Insert
	@ZoneId int
	,@TotalUser int
	,@TotalManager int
	,@TotalPay bigint
	,@PointRemain bigint
	,@Pcu int
	,@Acu int
	,@OnlineMinutes bigint
	,@UpdateTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Statistic_Info] (
	[ZoneId]
	,[TotalUser]
	,[TotalManager]
	,[TotalPay]
	,[PointRemain]
	,[Pcu]
	,[Acu]
	,[OnlineMinutes]
	,[UpdateTime]
) VALUES (
    @ZoneId
    ,@TotalUser
    ,@TotalManager
    ,@TotalPay
    ,@PointRemain
    ,@Pcu
    ,@Acu
    ,@OnlineMinutes
    ,@UpdateTime
)

select @ZoneId

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

CREATE PROCEDURE [dbo].P_StatisticInfo_Update
	@ZoneId int, -- 区id
	@TotalUser int, -- 用户数
	@TotalManager int, -- 经理数
	@TotalPay bigint, -- 充值数
	@PointRemain bigint, -- 剩余点券
	@Pcu int, 
	@Acu int, 
	@OnlineMinutes bigint, -- 总在线时长
	@UpdateTime datetime 
AS



UPDATE [dbo].[Statistic_Info] SET
	[TotalUser] = @TotalUser
	,[TotalManager] = @TotalManager
	,[TotalPay] = @TotalPay
	,[PointRemain] = @PointRemain
	,[Pcu] = @Pcu
	,[Acu] = @Acu
	,[OnlineMinutes] = @OnlineMinutes
	,[UpdateTime] = @UpdateTime
WHERE
	[ZoneId] = @ZoneId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


