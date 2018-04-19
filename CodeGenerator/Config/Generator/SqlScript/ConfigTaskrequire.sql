
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configtaskrequire_Delete    Script Date: 2016年2月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTaskrequire_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTaskrequire_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigTaskrequire_GetById    Script Date: 2016年2月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTaskrequire_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTaskrequire_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigTaskrequire_GetAll    Script Date: 2016年2月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTaskrequire_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTaskrequire_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigTaskrequire_Insert    Script Date: 2016年2月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTaskrequire_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTaskrequire_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigTaskrequire_Update    Script Date: 2016年2月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTaskrequire_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTaskrequire_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigTaskrequire_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_TaskRequire]
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

CREATE PROCEDURE [dbo].P_ConfigTaskrequire_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_TaskRequire] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigTaskrequire_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_TaskRequire] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigTaskrequire_Insert
	@Idx int
	,@TaskId int
	,@RequireType int
	,@RequireSub int
	,@RequireThird int
	,@OverState int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_TaskRequire] (
	[Idx]
	,[TaskId]
	,[RequireType]
	,[RequireSub]
	,[RequireThird]
	,[OverState]
) VALUES (
    @Idx
    ,@TaskId
    ,@RequireType
    ,@RequireSub
    ,@RequireThird
    ,@OverState
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

CREATE PROCEDURE [dbo].P_ConfigTaskrequire_Update
	@Idx int, 
	@TaskId int, -- 任务id
	@RequireType int, -- 需求类型
	@RequireSub int, -- 需求二级
	@RequireThird int, -- 需求三级
	@OverState int 
AS



UPDATE [dbo].[Config_TaskRequire] SET
	[TaskId] = @TaskId
	,[RequireType] = @RequireType
	,[RequireSub] = @RequireSub
	,[RequireThird] = @RequireThird
	,[OverState] = @OverState
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



