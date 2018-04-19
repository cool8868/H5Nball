
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Arenamanagerrecord_Delete    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ArenaManagerrecord_GetById    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ArenaManagerrecord_GetAll    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ArenaManagerrecord_Insert    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ArenaManagerrecord_Update    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ArenaManagerrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Arena_ManagerRecord]
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

CREATE PROCEDURE [dbo].P_ArenaManagerrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_ManagerRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_ArenaManagerrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_ManagerRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ArenaManagerrecord_Insert
	@ManagerId uniqueidentifier , 
	@ManagerName varchar(50) , 
	@SiteId varchar(50) , 
	@ZoneName varchar(50) , 
	@Integral int , 
	@DanGrading int , 
	@ArenaType int , 
	@SeasonId int , 
	@Rank int , 
	@IsPrize bit , 
	@PrizeId int , 
	@PrizeTime datetime , 
	@RowTime datetime , 
	@DomainId int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Arena_ManagerRecord] (
	[ManagerId]
	,[ManagerName]
	,[SiteId]
	,[ZoneName]
	,[Integral]
	,[DanGrading]
	,[ArenaType]
	,[SeasonId]
	,[Rank]
	,[IsPrize]
	,[PrizeId]
	,[PrizeTime]
	,[RowTime]
	,[DomainId]
) VALUES (
    @ManagerId
    ,@ManagerName
    ,@SiteId
    ,@ZoneName
    ,@Integral
    ,@DanGrading
    ,@ArenaType
    ,@SeasonId
    ,@Rank
    ,@IsPrize
    ,@PrizeId
    ,@PrizeTime
    ,@RowTime
    ,@DomainId
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

CREATE PROCEDURE [dbo].P_ArenaManagerrecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ManagerName varchar(50), 
	@SiteId varchar(50), 
	@ZoneName varchar(50), 
	@Integral int, 
	@DanGrading int, 
	@ArenaType int, 
	@SeasonId int, 
	@Rank int, 
	@IsPrize bit, 
	@PrizeId int, 
	@PrizeTime datetime, 
	@RowTime datetime, 
	@DomainId int 
AS



UPDATE [dbo].[Arena_ManagerRecord] SET
	[ManagerId] = @ManagerId
	,[ManagerName] = @ManagerName
	,[SiteId] = @SiteId
	,[ZoneName] = @ZoneName
	,[Integral] = @Integral
	,[DanGrading] = @DanGrading
	,[ArenaType] = @ArenaType
	,[SeasonId] = @SeasonId
	,[Rank] = @Rank
	,[IsPrize] = @IsPrize
	,[PrizeId] = @PrizeId
	,[PrizeTime] = @PrizeTime
	,[RowTime] = @RowTime
	,[DomainId] = @DomainId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


