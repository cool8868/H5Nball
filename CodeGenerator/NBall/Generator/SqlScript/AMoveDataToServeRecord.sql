
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Amovedatatoserverecord_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AMovedatatoserverecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AMovedatatoserverecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].AMovedatatoserverecord_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AMovedatatoserverecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AMovedatatoserverecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].AMovedatatoserverecord_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AMovedatatoserverecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AMovedatatoserverecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].AMovedatatoserverecord_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AMovedatatoserverecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AMovedatatoserverecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].AMovedatatoserverecord_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_AMovedatatoserverecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_AMovedatatoserverecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_AMovedatatoserverecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[A_MoveDataToServeRecord]
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

CREATE PROCEDURE [dbo].P_AMovedatatoserverecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[A_MoveDataToServeRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_AMovedatatoserverecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[A_MoveDataToServeRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_AMovedatatoserverecord_Insert
	@SourceDbFullName varchar(50) , 
	@OldZoneName nvarchar(50) , 
	@Account varchar(50) , 
	@ManagerId uniqueidentifier , 
	@Name nvarchar(50) , 
	@Level int , 
	@Mod int , 
	@TargetAccount varchar(50) , 
	@NewName nvarchar(50) , 
	@Status int , 
	@ReturnValue int , 
	@ReturnMessage varchar(300) , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@BindCode uniqueidentifier , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[A_MoveDataToServeRecord] (
	[SourceDbFullName]
	,[OldZoneName]
	,[Account]
	,[ManagerId]
	,[Name]
	,[Level]
	,[Mod]
	,[TargetAccount]
	,[NewName]
	,[Status]
	,[ReturnValue]
	,[ReturnMessage]
	,[RowTime]
	,[UpdateTime]
	,[BindCode]
) VALUES (
    @SourceDbFullName
    ,@OldZoneName
    ,@Account
    ,@ManagerId
    ,@Name
    ,@Level
    ,@Mod
    ,@TargetAccount
    ,@NewName
    ,@Status
    ,@ReturnValue
    ,@ReturnMessage
    ,@RowTime
    ,@UpdateTime
    ,@BindCode
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

CREATE PROCEDURE [dbo].P_AMovedatatoserverecord_Update
	@Idx int, 
	@SourceDbFullName varchar(50), 
	@OldZoneName nvarchar(50), 
	@Account varchar(50), 
	@ManagerId uniqueidentifier, 
	@Name nvarchar(50), 
	@Level int, 
	@Mod int, 
	@TargetAccount varchar(50), 
	@NewName nvarchar(50), 
	@Status int, -- 状态：0，初始；1，成功；-1，失败；
	@ReturnValue int, 
	@ReturnMessage varchar(300), 
	@RowTime datetime, 
	@UpdateTime datetime, 
	@BindCode uniqueidentifier 
AS



UPDATE [dbo].[A_MoveDataToServeRecord] SET
	[SourceDbFullName] = @SourceDbFullName
	,[OldZoneName] = @OldZoneName
	,[Account] = @Account
	,[ManagerId] = @ManagerId
	,[Name] = @Name
	,[Level] = @Level
	,[Mod] = @Mod
	,[TargetAccount] = @TargetAccount
	,[NewName] = @NewName
	,[Status] = @Status
	,[ReturnValue] = @ReturnValue
	,[ReturnMessage] = @ReturnMessage
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[BindCode] = @BindCode
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



