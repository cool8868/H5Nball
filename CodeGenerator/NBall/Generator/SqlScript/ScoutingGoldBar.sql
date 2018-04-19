
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Scoutinggoldbar_Delete    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingGoldbar_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingGoldbar_Delete]
GO

/****** Object:  Stored Procedure [dbo].ScoutingGoldbar_GetById    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingGoldbar_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingGoldbar_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ScoutingGoldbar_GetAll    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingGoldbar_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingGoldbar_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ScoutingGoldbar_Insert    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingGoldbar_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingGoldbar_Insert]
GO

/****** Object:  Stored Procedure [dbo].ScoutingGoldbar_Update    Script Date: 2016年10月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingGoldbar_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingGoldbar_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ScoutingGoldbar_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Scouting_GoldBar]
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

CREATE PROCEDURE [dbo].P_ScoutingGoldbar_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Scouting_GoldBar] with(nolock)
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

CREATE PROCEDURE [dbo].P_ScoutingGoldbar_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Scouting_GoldBar] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ScoutingGoldbar_Insert
	@GoldBarNumber int , 
	@ScoutingNumber int , 
	@TenNumber int , 
	@Status int , 
	@UpdateTiem datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Scouting_GoldBar] (
	[ManagerId],
	[GoldBarNumber]
	,[ScoutingNumber]
	,[TenNumber]
	,[Status]
	,[UpdateTiem]
	,[RowTime]
) VALUES (
	@ManagerId,
    @GoldBarNumber
    ,@ScoutingNumber
    ,@TenNumber
    ,@Status
    ,@UpdateTiem
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

CREATE PROCEDURE [dbo].P_ScoutingGoldbar_Update
	@ManagerId uniqueidentifier, 
	@GoldBarNumber int, -- 金条数量
	@ScoutingNumber int, -- 抽卡次数
	@TenNumber int, -- 十连抽次数
	@Status int, 
	@UpdateTiem datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Scouting_GoldBar] SET
	[GoldBarNumber] = @GoldBarNumber
	,[ScoutingNumber] = @ScoutingNumber
	,[TenNumber] = @TenNumber
	,[Status] = @Status
	,[UpdateTiem] = @UpdateTiem
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


