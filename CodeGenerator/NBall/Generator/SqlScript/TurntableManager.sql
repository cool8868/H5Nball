
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Turntablemanager_Delete    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].TurntableManager_GetById    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TurntableManager_GetAll    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TurntableManager_Insert    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].TurntableManager_Update    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TurntableManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Turntable_Manager]
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

CREATE PROCEDURE [dbo].P_TurntableManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Turntable_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_TurntableManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Turntable_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TurntableManager_Insert
	@LuckyCoin int , 
	@GiveLuckyCoin int , 
	@DayProduceLuckyCoin int , 
	@TurntableType int , 
	@DetailsString varbinary(max) , 
	@ResetNumber varchar(50) , 
	@RefreshDate date , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Turntable_Manager] (
	[ManagerId],
	[LuckyCoin]
	,[GiveLuckyCoin]
	,[DayProduceLuckyCoin]
	,[TurntableType]
	,[DetailsString]
	,[ResetNumber]
	,[RefreshDate]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @LuckyCoin
    ,@GiveLuckyCoin
    ,@DayProduceLuckyCoin
    ,@TurntableType
    ,@DetailsString
    ,@ResetNumber
    ,@RefreshDate
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_TurntableManager_Update
	@ManagerId uniqueidentifier, 
	@LuckyCoin int, -- 幸运币数量
	@GiveLuckyCoin int, -- 赠送的幸运币
	@DayProduceLuckyCoin int, -- 系统每天产出的幸运币数量
	@TurntableType int, -- 转盘类型 1=青铜 2=白银  3=黄金
	@DetailsString varbinary(max), -- 转盘详情串
	@ResetNumber varchar(50), -- 重置次数 1,1|2,1|3,1|（转盘ID,次数）
	@RefreshDate date, 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Turntable_Manager] SET
	[LuckyCoin] = @LuckyCoin
	,[GiveLuckyCoin] = @GiveLuckyCoin
	,[DayProduceLuckyCoin] = @DayProduceLuckyCoin
	,[TurntableType] = @TurntableType
	,[DetailsString] = @DetailsString
	,[ResetNumber] = @ResetNumber
	,[RefreshDate] = @RefreshDate
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


