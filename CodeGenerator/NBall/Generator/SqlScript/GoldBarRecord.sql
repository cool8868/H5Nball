
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Goldbarrecord_Delete    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GoldbarRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GoldbarRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].GoldbarRecord_GetById    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GoldbarRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GoldbarRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GoldbarRecord_GetAll    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GoldbarRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GoldbarRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GoldbarRecord_Insert    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GoldbarRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GoldbarRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].GoldbarRecord_Update    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GoldbarRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GoldbarRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GoldbarRecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[GoldBar_Record]
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

CREATE PROCEDURE [dbo].P_GoldbarRecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[GoldBar_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_GoldbarRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[GoldBar_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GoldbarRecord_Insert
	@ManagerId uniqueidentifier , 
	@IsAdd bit , 
	@Number int , 
	@OperationType int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[GoldBar_Record] (
	[ManagerId]
	,[IsAdd]
	,[Number]
	,[OperationType]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@IsAdd
    ,@Number
    ,@OperationType
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

CREATE PROCEDURE [dbo].P_GoldbarRecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@IsAdd bit, -- 是否增加
	@Number int, -- 操作数量
	@OperationType int, -- 操作类型
	@RowTime datetime 
AS



UPDATE [dbo].[GoldBar_Record] SET
	[ManagerId] = @ManagerId
	,[IsAdd] = @IsAdd
	,[Number] = @Number
	,[OperationType] = @OperationType
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


