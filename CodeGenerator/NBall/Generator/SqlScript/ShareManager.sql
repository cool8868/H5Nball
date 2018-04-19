
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Sharemanager_Delete    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ShareManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ShareManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].ShareManager_GetById    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ShareManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ShareManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ShareManager_GetAll    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ShareManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ShareManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ShareManager_Insert    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ShareManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ShareManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].ShareManager_Update    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ShareManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ShareManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ShareManager_Delete
	@Idx int
AS

DELETE FROM [dbo].[Share_Manager]
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

CREATE PROCEDURE [dbo].P_ShareManager_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Share_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_ShareManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Share_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ShareManager_Insert
	@ManagerId uniqueidentifier , 
	@ShareType int , 
	@ShareNumber int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Share_Manager] (
	[ManagerId]
	,[ShareType]
	,[ShareNumber]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ShareType
    ,@ShareNumber
    ,@UpdateTime
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_ShareManager_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ShareType int, -- 分享类型
	@ShareNumber int, -- 分享次数
	@UpdateTime datetime, -- 更新时间
	@RowTime datetime 
AS



UPDATE [dbo].[Share_Manager] SET
	[ManagerId] = @ManagerId
	,[ShareType] = @ShareType
	,[ShareNumber] = @ShareNumber
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


