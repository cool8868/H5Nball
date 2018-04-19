
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Everydayactivityprize_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Everydayactivityprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Everydayactivityprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].Everydayactivityprize_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Everydayactivityprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Everydayactivityprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].Everydayactivityprize_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Everydayactivityprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Everydayactivityprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].Everydayactivityprize_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Everydayactivityprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Everydayactivityprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].Everydayactivityprize_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Everydayactivityprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Everydayactivityprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_Everydayactivityprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[EveryDayActivityPrize]
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

CREATE PROCEDURE [dbo].P_Everydayactivityprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[EveryDayActivityPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_Everydayactivityprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[EveryDayActivityPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_Everydayactivityprize_Insert
	@ManagerId uniqueidentifier , 
	@ActivityId int , 
	@ActivityStep int , 
	@SubType int , 
	@ItemCode int , 
	@RowDate datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[EveryDayActivityPrize] (
	[ManagerId]
	,[ActivityId]
	,[ActivityStep]
	,[SubType]
	,[ItemCode]
	,[RowDate]
) VALUES (
    @ManagerId
    ,@ActivityId
    ,@ActivityStep
    ,@SubType
    ,@ItemCode
    ,@RowDate
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

CREATE PROCEDURE [dbo].P_Everydayactivityprize_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ActivityId int, 
	@ActivityStep int, 
	@SubType int, 
	@ItemCode int, 
	@RowDate datetime 
AS



UPDATE [dbo].[EveryDayActivityPrize] SET
	[ManagerId] = @ManagerId
	,[ActivityId] = @ActivityId
	,[ActivityStep] = @ActivityStep
	,[SubType] = @SubType
	,[ItemCode] = @ItemCode
	,[RowDate] = @RowDate
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



