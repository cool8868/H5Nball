
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityprizerecord_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityPrizerecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityPrizerecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityPrizerecord_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityPrizerecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityPrizerecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityPrizerecord_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityPrizerecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityPrizerecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityPrizerecord_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityPrizerecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityPrizerecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityPrizerecord_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityPrizerecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityPrizerecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityPrizerecord_Delete
	@Idx bigint
AS

DELETE FROM [dbo].[Activity_PrizeRecord]
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

CREATE PROCEDURE [dbo].P_ActivityPrizerecord_GetById
	@Idx bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Activity_PrizeRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityPrizerecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Activity_PrizeRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityPrizerecord_Insert
	@ManagerId uniqueidentifier , 
	@ActivityKey varchar(50) , 
	@RowTime datetime , 
    @Idx bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Activity_PrizeRecord] (
	[ManagerId]
	,[ActivityKey]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ActivityKey
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

CREATE PROCEDURE [dbo].P_ActivityPrizerecord_Update
	@Idx bigint, 
	@ManagerId uniqueidentifier, 
	@ActivityKey varchar(50), 
	@RowTime datetime 
AS



UPDATE [dbo].[Activity_PrizeRecord] SET
	[ManagerId] = @ManagerId
	,[ActivityKey] = @ActivityKey
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



