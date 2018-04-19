
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationmain_Delete    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMain_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMain_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationMain_GetById    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMain_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMain_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationMain_GetAll    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMain_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMain_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationMain_Insert    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMain_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMain_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationMain_Update    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMain_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMain_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationMain_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Revelation_Main]
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

CREATE PROCEDURE [dbo].P_RevelationMain_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Main] with(nolock)
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

CREATE PROCEDURE [dbo].P_RevelationMain_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Main] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationMain_Insert
	@ChallengesNums int , 
	@BuyTheNumber int , 
	@Courage int , 
	@OnHookCD datetime , 
	@FailCD datetime , 
	@RefreshTime date , 
	@States int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_Main] (
	[ManagerId],
	[ChallengesNums]
	,[BuyTheNumber]
	,[Courage]
	,[OnHookCD]
	,[FailCD]
	,[RefreshTime]
	,[States]
) VALUES (
	@ManagerId,
    @ChallengesNums
    ,@BuyTheNumber
    ,@Courage
    ,@OnHookCD
    ,@FailCD
    ,@RefreshTime
    ,@States
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

CREATE PROCEDURE [dbo].P_RevelationMain_Update
	@ManagerId uniqueidentifier, -- 经理ID
	@ChallengesNums int, -- 今天挑战次数
	@BuyTheNumber int, -- 购买次数
	@Courage int, -- 勇气值
	@OnHookCD datetime, -- 挂机CD时间
	@FailCD datetime, -- 挑战失败CD时间
	@RefreshTime date, -- 刷新时间
	@States int, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[Revelation_Main] SET
	[ChallengesNums] = @ChallengesNums
	,[BuyTheNumber] = @BuyTheNumber
	,[Courage] = @Courage
	,[OnHookCD] = @OnHookCD
	,[FailCD] = @FailCD
	,[RefreshTime] = @RefreshTime
	,[States] = @States
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



