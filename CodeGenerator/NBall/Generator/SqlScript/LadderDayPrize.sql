
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Ladderdayprize_Delete    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderDayprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderDayprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].LadderDayprize_GetById    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderDayprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderDayprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LadderDayprize_GetAll    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderDayprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderDayprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LadderDayprize_Insert    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderDayprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderDayprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].LadderDayprize_Update    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderDayprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderDayprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LadderDayprize_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Ladder_DayPrize]
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

CREATE PROCEDURE [dbo].P_LadderDayprize_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_DayPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_LadderDayprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_DayPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LadderDayprize_Insert
	@WinNumber int , 
	@PrizeRecord varchar(50) , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Ladder_DayPrize] (
	[ManagerId],
	[WinNumber]
	,[PrizeRecord]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @WinNumber
    ,@PrizeRecord
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_LadderDayprize_Update
	@ManagerId uniqueidentifier, 
	@WinNumber int, -- 获胜场次
	@PrizeRecord varchar(50), -- 奖励记录（1,1,1,1）
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Ladder_DayPrize] SET
	[WinNumber] = @WinNumber
	,[PrizeRecord] = @PrizeRecord
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



