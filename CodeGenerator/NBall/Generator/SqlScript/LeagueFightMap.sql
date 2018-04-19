
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Leaguefightmap_Delete    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueFightmap_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueFightmap_Delete]
GO

/****** Object:  Stored Procedure [dbo].LeagueFightmap_GetById    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueFightmap_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueFightmap_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LeagueFightmap_GetAll    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueFightmap_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueFightmap_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LeagueFightmap_Insert    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueFightmap_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueFightmap_Insert]
GO

/****** Object:  Stored Procedure [dbo].LeagueFightmap_Update    Script Date: 2016年6月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LeagueFightmap_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LeagueFightmap_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LeagueFightmap_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[League_FightMap]
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

CREATE PROCEDURE [dbo].P_LeagueFightmap_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_FightMap] with(nolock)
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

CREATE PROCEDURE [dbo].P_LeagueFightmap_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[League_FightMap] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LeagueFightmap_Insert
	@LeaguRecordId uniqueidentifier , 
	@FightMapString varbinary(max) , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[League_FightMap] (
	[ManagerId],
	[LeaguRecordId]
	,[FightMapString]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @LeaguRecordId
    ,@FightMapString
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

CREATE PROCEDURE [dbo].P_LeagueFightmap_Update
	@ManagerId uniqueidentifier, 
	@LeaguRecordId uniqueidentifier, 
	@FightMapString varbinary(max), 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[League_FightMap] SET
	[LeaguRecordId] = @LeaguRecordId
	,[FightMapString] = @FightMapString
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



