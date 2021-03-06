﻿
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Arenaseason_Delete    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeason_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeason_Delete]
GO

/****** Object:  Stored Procedure [dbo].ArenaSeason_GetById    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeason_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeason_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ArenaSeason_GetAll    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeason_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeason_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ArenaSeason_Insert    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeason_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeason_Insert]
GO

/****** Object:  Stored Procedure [dbo].ArenaSeason_Update    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaSeason_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaSeason_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ArenaSeason_Delete
	@Idx int
AS

DELETE FROM [dbo].[Arena_Season]
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

CREATE PROCEDURE [dbo].P_ArenaSeason_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_Season] with(nolock)
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

CREATE PROCEDURE [dbo].P_ArenaSeason_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_Season] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ArenaSeason_Insert
	@SeasonId int , 
	@PrepareTime datetime , 
	@StartTime datetime , 
	@EndTime datetime , 
	@ArenaType int , 
	@Status int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Arena_Season] (
	[SeasonId]
	,[PrepareTime]
	,[StartTime]
	,[EndTime]
	,[ArenaType]
	,[Status]
	,[RowTime]
) VALUES (
    @SeasonId
    ,@PrepareTime
    ,@StartTime
    ,@EndTime
    ,@ArenaType
    ,@Status
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

CREATE PROCEDURE [dbo].P_ArenaSeason_Update
	@Idx int, 
	@SeasonId int, -- 赛季ID
	@PrepareTime datetime, -- 准备时间
	@StartTime datetime, -- 开始时间
	@EndTime datetime, -- 结束时间
	@ArenaType int, -- 开启的竞技场类型
	@Status int, -- 状态
	@RowTime datetime 
AS



UPDATE [dbo].[Arena_Season] SET
	[SeasonId] = @SeasonId
	,[PrepareTime] = @PrepareTime
	,[StartTime] = @StartTime
	,[EndTime] = @EndTime
	,[ArenaType] = @ArenaType
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


