
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationmyhistory_Delete    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationMyhistory_GetById    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationMyhistory_GetAll    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationMyhistory_Insert    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationMyhistory_Update    Script Date: 2017年1月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationMyhistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationMyhistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationMyhistory_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Revelation_MyHistory]
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

CREATE PROCEDURE [dbo].P_RevelationMyhistory_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_MyHistory] with(nolock)
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

CREATE PROCEDURE [dbo].P_RevelationMyhistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_MyHistory] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationMyhistory_Insert
	@GoalsString varbinary(max) , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_MyHistory] (
	[ManagerId],
	[GoalsString]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @GoalsString
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

CREATE PROCEDURE [dbo].P_RevelationMyhistory_Update
	@ManagerId uniqueidentifier, 
	@GoalsString varbinary(max), 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Revelation_MyHistory] SET
	[GoalsString] = @GoalsString
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


