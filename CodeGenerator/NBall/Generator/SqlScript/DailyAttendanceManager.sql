
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dailyattendancemanager_Delete    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailyattendanceManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailyattendanceManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].DailyattendanceManager_GetById    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailyattendanceManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailyattendanceManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DailyattendanceManager_GetAll    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailyattendanceManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailyattendanceManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DailyattendanceManager_Insert    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailyattendanceManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailyattendanceManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].DailyattendanceManager_Update    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DailyattendanceManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DailyattendanceManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DailyattendanceManager_Delete
	@Idx bigint
AS

DELETE FROM [dbo].[DailyAttendance_Manager]
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

CREATE PROCEDURE [dbo].P_DailyattendanceManager_GetById
	@Idx bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyAttendance_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_DailyattendanceManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[DailyAttendance_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DailyattendanceManager_Insert
	@ManagerId uniqueidentifier , 
	@Name nvarchar(50) , 
	@AttendTimes int , 
	@Month int , 
	@IsAttend bit , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[DailyAttendance_Manager] (
	[ManagerId]
	,[Name]
	,[AttendTimes]
	,[Month]
	,[IsAttend]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @ManagerId
    ,@Name
    ,@AttendTimes
    ,@Month
    ,@IsAttend
    ,@RowTime
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_DailyattendanceManager_Update
	@Idx bigint, 
	@ManagerId uniqueidentifier, 
	@Name nvarchar(50), 
	@AttendTimes int, 
	@Month int, 
	@IsAttend bit, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[DailyAttendance_Manager] SET
	[ManagerId] = @ManagerId
	,[Name] = @Name
	,[AttendTimes] = @AttendTimes
	,[Month] = @Month
	,[IsAttend] = @IsAttend
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



