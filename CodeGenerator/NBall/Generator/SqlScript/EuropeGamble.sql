
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Europegamble_Delete    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamble_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamble_Delete]
GO

/****** Object:  Stored Procedure [dbo].EuropeGamble_GetById    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamble_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamble_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].EuropeGamble_GetAll    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamble_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamble_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].EuropeGamble_Insert    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamble_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamble_Insert]
GO

/****** Object:  Stored Procedure [dbo].EuropeGamble_Update    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeGamble_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeGamble_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_EuropeGamble_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Europe_Gamble]
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

CREATE PROCEDURE [dbo].P_EuropeGamble_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_Gamble] with(nolock)
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

CREATE PROCEDURE [dbo].P_EuropeGamble_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_Gamble] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_EuropeGamble_Insert
	@CorrectNumber int , 
	@PrizeRecord varchar(50) , 
	@UpdateTime datetime , 
	@RowTime datetime , 
	@SeasonId int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Europe_Gamble] (
	[ManagerId],
	[CorrectNumber]
	,[PrizeRecord]
	,[UpdateTime]
	,[RowTime]
	,[SeasonId]
) VALUES (
	@ManagerId,
    @CorrectNumber
    ,@PrizeRecord
    ,@UpdateTime
    ,@RowTime
    ,@SeasonId
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

CREATE PROCEDURE [dbo].P_EuropeGamble_Update
	@ManagerId uniqueidentifier, 
	@CorrectNumber int, -- 竞猜正确次数
	@PrizeRecord varchar(50), -- 奖励记录  1,1,1,1,1 
	@UpdateTime datetime, 
	@RowTime datetime, 
	@SeasonId int 
AS



UPDATE [dbo].[Europe_Gamble] SET
	[CorrectNumber] = @CorrectNumber
	,[PrizeRecord] = @PrizeRecord
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
	,[SeasonId] = @SeasonId
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


