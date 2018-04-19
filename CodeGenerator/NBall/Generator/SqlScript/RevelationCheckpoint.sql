
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationcheckpoint_Delete    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationCheckpoint_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationCheckpoint_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationCheckpoint_GetById    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationCheckpoint_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationCheckpoint_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationCheckpoint_GetAll    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationCheckpoint_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationCheckpoint_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationCheckpoint_Insert    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationCheckpoint_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationCheckpoint_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationCheckpoint_Update    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationCheckpoint_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationCheckpoint_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationCheckpoint_Delete
	@Idx int
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Revelation_Checkpoint]
WHERE
	[Idx] = @Idx
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

CREATE PROCEDURE [dbo].P_RevelationCheckpoint_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Checkpoint] with(nolock)
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

CREATE PROCEDURE [dbo].P_RevelationCheckpoint_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Checkpoint] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationCheckpoint_Insert
	@ManagerId uniqueidentifier , 
	@ToDayGeneralNums int , 
	@CustomsPass int , 
	@Schedule int , 
	@IsGeneral bit , 
	@GeneralTime datetime , 
	@IsGeneralAwary bit , 
	@GeneralAwaryTime datetime , 
	@AwaryItem varchar(100) , 
	@States int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_Checkpoint] (
	[ManagerId]
	,[ToDayGeneralNums]
	,[CustomsPass]
	,[Schedule]
	,[IsGeneral]
	,[GeneralTime]
	,[IsGeneralAwary]
	,[GeneralAwaryTime]
	,[AwaryItem]
	,[States]
) VALUES (
    @ManagerId
    ,@ToDayGeneralNums
    ,@CustomsPass
    ,@Schedule
    ,@IsGeneral
    ,@GeneralTime
    ,@IsGeneralAwary
    ,@GeneralAwaryTime
    ,@AwaryItem
    ,@States
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

CREATE PROCEDURE [dbo].P_RevelationCheckpoint_Update
	@Idx int, 
	@ManagerId uniqueidentifier, -- 经理ID
	@ToDayGeneralNums int, -- 今天通关次数
	@CustomsPass int, -- 关卡
	@Schedule int, -- 进度
	@IsGeneral bit, -- 是否通关
	@GeneralTime datetime, -- 通关时间
	@IsGeneralAwary bit, -- 是否领取通关奖励
	@GeneralAwaryTime datetime, -- 通关奖励领取时间
	@AwaryItem varchar(100), -- 领取奖励物品
	@States int, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[Revelation_Checkpoint] SET
	[ManagerId] = @ManagerId
	,[ToDayGeneralNums] = @ToDayGeneralNums
	,[CustomsPass] = @CustomsPass
	,[Schedule] = @Schedule
	,[IsGeneral] = @IsGeneral
	,[GeneralTime] = @GeneralTime
	,[IsGeneralAwary] = @IsGeneralAwary
	,[GeneralAwaryTime] = @GeneralAwaryTime
	,[AwaryItem] = @AwaryItem
	,[States] = @States
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



