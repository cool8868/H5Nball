
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gmlog_Delete    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GmLog_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GmLog_Delete]
GO

/****** Object:  Stored Procedure [dbo].GmLog_GetById    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GmLog_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GmLog_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GmLog_GetAll    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GmLog_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GmLog_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GmLog_Insert    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GmLog_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GmLog_Insert]
GO

/****** Object:  Stored Procedure [dbo].GmLog_Update    Script Date: 2016年6月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GmLog_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GmLog_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GmLog_Delete
	@Idx int
AS

DELETE FROM [dbo].[Gm_Log]
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

CREATE PROCEDURE [dbo].P_GmLog_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gm_Log] with(nolock)
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

CREATE PROCEDURE [dbo].P_GmLog_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gm_Log] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GmLog_Insert
	@Idx int
	,@AdminName varchar(50)
	,@Ip varchar(50)
	,@OperationType int
	,@TargetZoneId varchar(50)
	,@TargetUser varchar(50)
	,@TargetUserList varchar(5000)
	,@ManagerName nvarchar(50)
	,@ManagerId uniqueidentifier
	,@Memo varchar(5000)
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Gm_Log] (
	[Idx]
	,[AdminName]
	,[Ip]
	,[OperationType]
	,[TargetZoneId]
	,[TargetUser]
	,[TargetUserList]
	,[ManagerName]
	,[ManagerId]
	,[Memo]
	,[RowTime]
) VALUES (
    @Idx
    ,@AdminName
    ,@Ip
    ,@OperationType
    ,@TargetZoneId
    ,@TargetUser
    ,@TargetUserList
    ,@ManagerName
    ,@ManagerId
    ,@Memo
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_GmLog_Update
	@Idx int, 
	@AdminName varchar(50), 
	@Ip varchar(50), 
	@OperationType int, 
	@TargetZoneId varchar(50), 
	@TargetUser varchar(50), 
	@TargetUserList varchar(5000), 
	@ManagerName nvarchar(50), 
	@ManagerId uniqueidentifier, 
	@Memo varchar(5000), 
	@RowTime datetime 
AS



UPDATE [dbo].[Gm_Log] SET
	[AdminName] = @AdminName
	,[Ip] = @Ip
	,[OperationType] = @OperationType
	,[TargetZoneId] = @TargetZoneId
	,[TargetUser] = @TargetUser
	,[TargetUserList] = @TargetUserList
	,[ManagerName] = @ManagerName
	,[ManagerId] = @ManagerId
	,[Memo] = @Memo
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


