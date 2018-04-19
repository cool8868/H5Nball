
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Turntableluckyrecord_Delete    Script Date: 2016年7月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableLuckyrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableLuckyrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].TurntableLuckyrecord_GetById    Script Date: 2016年7月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableLuckyrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableLuckyrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TurntableLuckyrecord_GetAll    Script Date: 2016年7月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableLuckyrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableLuckyrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TurntableLuckyrecord_Insert    Script Date: 2016年7月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableLuckyrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableLuckyrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].TurntableLuckyrecord_Update    Script Date: 2016年7月14日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TurntableLuckyrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TurntableLuckyrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TurntableLuckyrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Turntable_LuckyRecord]
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

CREATE PROCEDURE [dbo].P_TurntableLuckyrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Turntable_LuckyRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_TurntableLuckyrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Turntable_LuckyRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TurntableLuckyrecord_Insert
	@ManagerId uniqueidentifier , 
	@IsAdd bit , 
	@OperationNumber int , 
	@RowTime datetime , 
	@LuckDrawString varchar(50) , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Turntable_LuckyRecord] (
	[ManagerId]
	,[IsAdd]
	,[OperationNumber]
	,[RowTime]
	,[LuckDrawString]
) VALUES (
    @ManagerId
    ,@IsAdd
    ,@OperationNumber
    ,@RowTime
    ,@LuckDrawString
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

CREATE PROCEDURE [dbo].P_TurntableLuckyrecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@IsAdd bit, 
	@OperationNumber int, 
	@RowTime datetime, 
	@LuckDrawString varchar(50) 
AS



UPDATE [dbo].[Turntable_LuckyRecord] SET
	[ManagerId] = @ManagerId
	,[IsAdd] = @IsAdd
	,[OperationNumber] = @OperationNumber
	,[RowTime] = @RowTime
	,[LuckDrawString] = @LuckDrawString
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


