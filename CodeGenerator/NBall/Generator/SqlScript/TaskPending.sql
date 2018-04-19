
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Taskpending_Delete    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskPending_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskPending_Delete]
GO

/****** Object:  Stored Procedure [dbo].TaskPending_GetById    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskPending_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskPending_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TaskPending_GetAll    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskPending_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskPending_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TaskPending_Insert    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskPending_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskPending_Insert]
GO

/****** Object:  Stored Procedure [dbo].TaskPending_Update    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TaskPending_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TaskPending_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TaskPending_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Task_Pending]
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

CREATE PROCEDURE [dbo].P_TaskPending_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_Pending] with(nolock)
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

CREATE PROCEDURE [dbo].P_TaskPending_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Task_Pending] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TaskPending_Insert
	@TaskString varchar(100) , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Task_Pending] (
	[ManagerId],
	[TaskString]
) VALUES (
	@ManagerId,
    @TaskString
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

CREATE PROCEDURE [dbo].P_TaskPending_Update
	@ManagerId uniqueidentifier, 
	@TaskString varchar(100) 
AS



UPDATE [dbo].[Task_Pending] SET
	[TaskString] = @TaskString
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



