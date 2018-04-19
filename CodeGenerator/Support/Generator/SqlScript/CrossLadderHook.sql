
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Crossladderhook_Delete    Script Date: 2016年9月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHook_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHook_Delete]
GO

/****** Object:  Stored Procedure [dbo].CrossladderHook_GetById    Script Date: 2016年9月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHook_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHook_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].CrossladderHook_GetAll    Script Date: 2016年9月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHook_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHook_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].CrossladderHook_Insert    Script Date: 2016年9月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHook_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHook_Insert]
GO

/****** Object:  Stored Procedure [dbo].CrossladderHook_Update    Script Date: 2016年9月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_CrossladderHook_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_CrossladderHook_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_CrossladderHook_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[CrossLadder_Hook]
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

CREATE PROCEDURE [dbo].P_CrossladderHook_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_Hook] with(nolock)
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

CREATE PROCEDURE [dbo].P_CrossladderHook_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[CrossLadder_Hook] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_CrossladderHook_Insert
	@SiteId varchar(20) , 
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


INSERT INTO [dbo].[CrossLadder_Hook] (
	[ManagerId],
	[SiteId]
	,[CurTimes]
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
    @SiteId
    ,@CurTimes
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

CREATE PROCEDURE [dbo].P_CrossladderHook_Update
	@ManagerId uniqueidentifier, 
	@SiteId varchar(20), 
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



UPDATE [dbo].[CrossLadder_Hook] SET
	[SiteId] = @SiteId
	,[CurTimes] = @CurTimes
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


