
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configschedule_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSchedule_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSchedule_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigSchedule_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSchedule_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSchedule_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigSchedule_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSchedule_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSchedule_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigSchedule_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSchedule_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSchedule_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigSchedule_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigSchedule_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigSchedule_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigSchedule_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Schedule]
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

CREATE PROCEDURE [dbo].P_ConfigSchedule_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Schedule] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigSchedule_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Schedule] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigSchedule_Insert
	@Idx int
	,@Name varchar(50)
	,@Category int
	,@ActionType int
	,@TimeType int
	,@Setting varchar(80)
	,@Times int
	,@RetryTimes int
	,@Parameters varchar(80)
	,@RunDelay int
	,@Description nvarchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Schedule] (
	[Idx]
	,[Name]
	,[Category]
	,[ActionType]
	,[TimeType]
	,[Setting]
	,[Times]
	,[RetryTimes]
	,[Parameters]
	,[RunDelay]
	,[Description]
) VALUES (
    @Idx
    ,@Name
    ,@Category
    ,@ActionType
    ,@TimeType
    ,@Setting
    ,@Times
    ,@RetryTimes
    ,@Parameters
    ,@RunDelay
    ,@Description
)

select @Idx

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

CREATE PROCEDURE [dbo].P_ConfigSchedule_Update
	@Idx int, 
	@Name varchar(50), -- 计划任务名
	@Category int, 
	@ActionType int, 
	@TimeType int, -- 计划类型：1，按时间表；2，按间隔
	@Setting varchar(80), -- 执行时间配置
	@Times int, -- 执行次数：-1，循环执行；0，暂停执行；1，只执行一次
	@RetryTimes int, 
	@Parameters varchar(80), -- 参数，逗号分隔
	@RunDelay int, 
	@Description nvarchar(100) -- 描述
AS



UPDATE [dbo].[Config_Schedule] SET
	[Name] = @Name
	,[Category] = @Category
	,[ActionType] = @ActionType
	,[TimeType] = @TimeType
	,[Setting] = @Setting
	,[Times] = @Times
	,[RetryTimes] = @RetryTimes
	,[Parameters] = @Parameters
	,[RunDelay] = @RunDelay
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



