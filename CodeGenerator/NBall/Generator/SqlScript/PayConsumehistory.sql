
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Payconsumehistory_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayConsumehistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayConsumehistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].PayConsumehistory_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayConsumehistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayConsumehistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].PayConsumehistory_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayConsumehistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayConsumehistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].PayConsumehistory_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayConsumehistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayConsumehistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].PayConsumehistory_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_PayConsumehistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_PayConsumehistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_PayConsumehistory_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Pay_ConsumeHistory]
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

CREATE PROCEDURE [dbo].P_PayConsumehistory_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Pay_ConsumeHistory] with(nolock)
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

CREATE PROCEDURE [dbo].P_PayConsumehistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Pay_ConsumeHistory] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_PayConsumehistory_Insert
	@Account varchar(200) , 
	@ManagerId uniqueidentifier , 
	@Point int , 
	@Bonus int , 
	@SourceType int , 
	@SourceId varchar(50) , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Pay_ConsumeHistory] (
	[Idx],
	[Account]
	,[ManagerId]
	,[Point]
	,[Bonus]
	,[SourceType]
	,[SourceId]
	,[RowTime]
) VALUES (
	@Idx,
    @Account
    ,@ManagerId
    ,@Point
    ,@Bonus
    ,@SourceType
    ,@SourceId
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

CREATE PROCEDURE [dbo].P_PayConsumehistory_Update
	@Idx uniqueidentifier, -- Idx
	@Account varchar(200), -- Account
	@ManagerId uniqueidentifier, -- 经理id
	@Point int, -- 消耗点数
	@Bonus int, -- 消耗赠送点数
	@SourceType int, -- 消费来源类型,1:商城；2:联赛竞猜
	@SourceId varchar(50), -- 订单id
	@RowTime datetime -- RowTime
AS



UPDATE [dbo].[Pay_ConsumeHistory] SET
	[Account] = @Account
	,[ManagerId] = @ManagerId
	,[Point] = @Point
	,[Bonus] = @Bonus
	,[SourceType] = @SourceType
	,[SourceId] = @SourceId
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


