
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Teammembertrain_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberTrain_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberTrain_Delete]
GO

/****** Object:  Stored Procedure [dbo].TeammemberTrain_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberTrain_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberTrain_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TeammemberTrain_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberTrain_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberTrain_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TeammemberTrain_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberTrain_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberTrain_Insert]
GO

/****** Object:  Stored Procedure [dbo].TeammemberTrain_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberTrain_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberTrain_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TeammemberTrain_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Teammember_Train]
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

CREATE PROCEDURE [dbo].P_TeammemberTrain_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Teammember_Train] with(nolock)
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

CREATE PROCEDURE [dbo].P_TeammemberTrain_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Teammember_Train] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TeammemberTrain_Insert
	@ManagerId uniqueidentifier , 
	@PlayerId int , 
	@Level int , 
	@EXP int , 
	@TrainStamina int , 
	@TrainState int , 
	@StartTime datetime , 
	@SettleTime datetime , 
	@Status int , 
	@RowTime datetime , 
	@TrainType int , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Teammember_Train] (
	[Idx],
	[ManagerId]
	,[PlayerId]
	,[Level]
	,[EXP]
	,[TrainStamina]
	,[TrainState]
	,[StartTime]
	,[SettleTime]
	,[Status]
	,[RowTime]
	,[TrainType]
) VALUES (
	@Idx,
    @ManagerId
    ,@PlayerId
    ,@Level
    ,@EXP
    ,@TrainStamina
    ,@TrainState
    ,@StartTime
    ,@SettleTime
    ,@Status
    ,@RowTime
    ,@TrainType
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

CREATE PROCEDURE [dbo].P_TeammemberTrain_Update
	@Idx uniqueidentifier, -- 球员唯一id
	@ManagerId uniqueidentifier, -- 经理id
	@PlayerId int, -- pid
	@Level int, -- 球员训练等级
	@EXP int, -- 球员训练经验
	@TrainStamina int, -- 球员训练体力
	@TrainState int, -- 训练状态：0,初始;1,正在训练;2,恢复体力
	@StartTime datetime, -- 开始训练时间/开始恢复体力时间
	@SettleTime datetime, -- 上一次结算时间
	@Status int, 
	@RowTime datetime, 
	@TrainType int 
AS



UPDATE [dbo].[Teammember_Train] SET
	[ManagerId] = @ManagerId
	,[PlayerId] = @PlayerId
	,[Level] = @Level
	,[EXP] = @EXP
	,[TrainStamina] = @TrainStamina
	,[TrainState] = @TrainState
	,[StartTime] = @StartTime
	,[SettleTime] = @SettleTime
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[TrainType] = @TrainType
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



