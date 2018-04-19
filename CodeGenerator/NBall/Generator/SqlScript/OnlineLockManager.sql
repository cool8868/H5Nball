
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Onlinelockmanager_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineLockmanager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineLockmanager_Delete]
GO

/****** Object:  Stored Procedure [dbo].OnlineLockmanager_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineLockmanager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineLockmanager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].OnlineLockmanager_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineLockmanager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineLockmanager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].OnlineLockmanager_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineLockmanager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineLockmanager_Insert]
GO

/****** Object:  Stored Procedure [dbo].OnlineLockmanager_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OnlineLockmanager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OnlineLockmanager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_OnlineLockmanager_Delete
	@Id bigint
AS

DELETE FROM [dbo].[Online_LockManager]
WHERE
	[Id] = @Id

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

CREATE PROCEDURE [dbo].P_OnlineLockmanager_GetById
	@Id bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Online_LockManager] with(nolock)
WHERE
	[Id] = @Id
	
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

CREATE PROCEDURE [dbo].P_OnlineLockmanager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Online_LockManager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_OnlineLockmanager_Insert
	@ManagerId uniqueidentifier , 
	@ManagerName varchar(50) , 
	@LockType int , 
	@LockOperator varchar(50) , 
	@LockDate datetime , 
	@LockMemo varchar(255) , 
	@BreakFlag bit , 
	@PreBreakDate datetime , 
	@BreakOperator varchar(50) , 
	@BreakDate datetime , 
	@BreakMemo varchar(255) , 
	@Status int , 
	@RowTime datetime , 
    @Id bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Online_LockManager] (
	[ManagerId]
	,[ManagerName]
	,[LockType]
	,[LockOperator]
	,[LockDate]
	,[LockMemo]
	,[BreakFlag]
	,[PreBreakDate]
	,[BreakOperator]
	,[BreakDate]
	,[BreakMemo]
	,[Status]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ManagerName
    ,@LockType
    ,@LockOperator
    ,@LockDate
    ,@LockMemo
    ,@BreakFlag
    ,@PreBreakDate
    ,@BreakOperator
    ,@BreakDate
    ,@BreakMemo
    ,@Status
    ,@RowTime
)


SET @Id = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_OnlineLockmanager_Update
	@Id bigint, 
	@ManagerId uniqueidentifier, -- 经理Id
	@ManagerName varchar(50), -- 经理名称
	@LockType int, -- 封停类型
	@LockOperator varchar(50), -- 封停GM
	@LockDate datetime, -- 封停日期
	@LockMemo varchar(255), -- 封停原因
	@BreakFlag bit, -- 解除标记
	@PreBreakDate datetime, -- 预计解除日期
	@BreakOperator varchar(50), -- 解封GM
	@BreakDate datetime, -- 解封日期
	@BreakMemo varchar(255), -- 解封原因
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Online_LockManager] SET
	[ManagerId] = @ManagerId
	,[ManagerName] = @ManagerName
	,[LockType] = @LockType
	,[LockOperator] = @LockOperator
	,[LockDate] = @LockDate
	,[LockMemo] = @LockMemo
	,[BreakFlag] = @BreakFlag
	,[PreBreakDate] = @PreBreakDate
	,[BreakOperator] = @BreakOperator
	,[BreakDate] = @BreakDate
	,[BreakMemo] = @BreakMemo
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Id] = @Id

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



