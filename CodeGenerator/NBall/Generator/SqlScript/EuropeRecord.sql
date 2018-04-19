
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Europerecord_Delete    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].EuropeRecord_GetById    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].EuropeRecord_GetAll    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].EuropeRecord_Insert    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].EuropeRecord_Update    Script Date: 2016年8月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_EuropeRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_EuropeRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_EuropeRecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Europe_Record]
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

CREATE PROCEDURE [dbo].P_EuropeRecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_EuropeRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Europe_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_EuropeRecord_Insert
	@ManagerId uniqueidentifier , 
	@Season int , 
	@CorrectNumber int , 
	@PrizeRecord varchar(50) , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Europe_Record] (
	[ManagerId]
	,[Season]
	,[CorrectNumber]
	,[PrizeRecord]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@Season
    ,@CorrectNumber
    ,@PrizeRecord
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

CREATE PROCEDURE [dbo].P_EuropeRecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@Season int, 
	@CorrectNumber int, 
	@PrizeRecord varchar(50), 
	@RowTime datetime 
AS



UPDATE [dbo].[Europe_Record] SET
	[ManagerId] = @ManagerId
	,[Season] = @Season
	,[CorrectNumber] = @CorrectNumber
	,[PrizeRecord] = @PrizeRecord
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


