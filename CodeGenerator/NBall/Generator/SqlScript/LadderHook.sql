
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Ladderhook_Delete    Script Date: 2016年6月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderHook_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderHook_Delete]
GO

/****** Object:  Stored Procedure [dbo].LadderHook_GetById    Script Date: 2016年6月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderHook_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderHook_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LadderHook_GetAll    Script Date: 2016年6月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderHook_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderHook_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LadderHook_Insert    Script Date: 2016年6月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderHook_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderHook_Insert]
GO

/****** Object:  Stored Procedure [dbo].LadderHook_Update    Script Date: 2016年6月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderHook_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderHook_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LadderHook_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Ladder_Hook]
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

CREATE PROCEDURE [dbo].P_LadderHook_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_Hook] with(nolock)
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

CREATE PROCEDURE [dbo].P_LadderHook_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_Hook] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LadderHook_Insert
	@CurTimes int , 
	@CurWiningTimes int , 
	@MaxTimes int , 
	@MinScore int , 
	@MaxScore int , 
	@MaxWiningTimes int , 
	@NextMatchTime datetime , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@Expired datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Ladder_Hook] (
	[ManagerId],
	[CurTimes]
	,[CurWiningTimes]
	,[MaxTimes]
	,[MinScore]
	,[MaxScore]
	,[MaxWiningTimes]
	,[NextMatchTime]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[Expired]
) VALUES (
	@ManagerId,
    @CurTimes
    ,@CurWiningTimes
    ,@MaxTimes
    ,@MinScore
    ,@MaxScore
    ,@MaxWiningTimes
    ,@NextMatchTime
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@Expired
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

CREATE PROCEDURE [dbo].P_LadderHook_Update
	@ManagerId uniqueidentifier, 
	@CurTimes int, 
	@CurWiningTimes int, 
	@MaxTimes int, 
	@MinScore int, 
	@MaxScore int, 
	@MaxWiningTimes int, 
	@NextMatchTime datetime, 
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@Expired datetime 
AS



UPDATE [dbo].[Ladder_Hook] SET
	[CurTimes] = @CurTimes
	,[CurWiningTimes] = @CurWiningTimes
	,[MaxTimes] = @MaxTimes
	,[MinScore] = @MinScore
	,[MaxScore] = @MaxScore
	,[MaxWiningTimes] = @MaxWiningTimes
	,[NextMatchTime] = @NextMatchTime
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[Expired] = @Expired
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


