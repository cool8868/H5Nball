
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Scoutingrecordfordays_Delete    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecordfordays_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecordfordays_Delete]
GO

/****** Object:  Stored Procedure [dbo].ScoutingRecordfordays_GetById    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecordfordays_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecordfordays_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ScoutingRecordfordays_GetAll    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecordfordays_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecordfordays_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ScoutingRecordfordays_Insert    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecordfordays_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecordfordays_Insert]
GO

/****** Object:  Stored Procedure [dbo].ScoutingRecordfordays_Update    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecordfordays_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecordfordays_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ScoutingRecordfordays_Delete
	@Idx int
AS

DELETE FROM [dbo].[Scouting_RecordForDays]
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

CREATE PROCEDURE [dbo].P_ScoutingRecordfordays_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Scouting_RecordForDays] with(nolock)
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

CREATE PROCEDURE [dbo].P_ScoutingRecordfordays_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Scouting_RecordForDays] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ScoutingRecordfordays_Insert
	@ManagerId uniqueidentifier , 
	@CardItemCodeThen89 int , 
	@RowTime datetime , 
	@ScoutingType int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Scouting_RecordForDays] (
	[ManagerId]
	,[CardItemCodeThen89]
	,[RowTime]
	,[ScoutingType]
) VALUES (
    @ManagerId
    ,@CardItemCodeThen89
    ,@RowTime
    ,@ScoutingType
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

CREATE PROCEDURE [dbo].P_ScoutingRecordfordays_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@CardItemCodeThen89 int, -- 大于89的球员卡itemCode
	@RowTime datetime, 
	@ScoutingType int 
AS



UPDATE [dbo].[Scouting_RecordForDays] SET
	[ManagerId] = @ManagerId
	,[CardItemCodeThen89] = @CardItemCodeThen89
	,[RowTime] = @RowTime
	,[ScoutingType] = @ScoutingType
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


