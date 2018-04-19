
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configfunctionopen_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFunctionopen_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFunctionopen_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigFunctionopen_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFunctionopen_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFunctionopen_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigFunctionopen_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFunctionopen_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFunctionopen_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigFunctionopen_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFunctionopen_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFunctionopen_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigFunctionopen_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigFunctionopen_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigFunctionopen_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigFunctionopen_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_FunctionOpen]
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

CREATE PROCEDURE [dbo].P_ConfigFunctionopen_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_FunctionOpen] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigFunctionopen_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_FunctionOpen] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigFunctionopen_Insert
	@Idx int
	,@ManagerLevel int
	,@FunctionList varchar(80)
	,@FunctionId int
	,@Name nvarchar(50)
	,@LockMemo nvarchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_FunctionOpen] (
	[Idx]
	,[ManagerLevel]
	,[FunctionList]
	,[FunctionId]
	,[Name]
	,[LockMemo]
) VALUES (
    @Idx
    ,@ManagerLevel
    ,@FunctionList
    ,@FunctionId
    ,@Name
    ,@LockMemo
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

CREATE PROCEDURE [dbo].P_ConfigFunctionopen_Update
	@Idx int, 
	@ManagerLevel int, 
	@FunctionList varchar(80), 
	@FunctionId int, 
	@Name nvarchar(50), 
	@LockMemo nvarchar(50) 
AS



UPDATE [dbo].[Config_FunctionOpen] SET
	[ManagerLevel] = @ManagerLevel
	,[FunctionList] = @FunctionList
	,[FunctionId] = @FunctionId
	,[Name] = @Name
	,[LockMemo] = @LockMemo
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



