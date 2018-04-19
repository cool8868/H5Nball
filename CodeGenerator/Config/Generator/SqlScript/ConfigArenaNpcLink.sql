
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configarenanpclink_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpclink_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpclink_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenanpclink_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpclink_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpclink_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigArenanpclink_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpclink_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpclink_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigArenanpclink_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpclink_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpclink_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenanpclink_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpclink_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpclink_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigArenanpclink_Delete
	@NpcId uniqueidentifier
AS

DELETE FROM [dbo].[Config_ArenaNpcLink]
WHERE
	[NpcId] = @NpcId

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

CREATE PROCEDURE [dbo].P_ConfigArenanpclink_GetById
	@NpcId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaNpcLink] with(nolock)
WHERE
	[NpcId] = @NpcId
	
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

CREATE PROCEDURE [dbo].P_ConfigArenanpclink_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaNpcLink] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigArenanpclink_Insert
	@Idx int , 
	@GroupId int , 
	@Kpi int , 
	@DanGrading int , 
    @NpcId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_ArenaNpcLink] (
	[NpcId],
	[Idx]
	,[GroupId]
	,[Kpi]
	,[DanGrading]
) VALUES (
	@NpcId,
    @Idx
    ,@GroupId
    ,@Kpi
    ,@DanGrading
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

CREATE PROCEDURE [dbo].P_ConfigArenanpclink_Update
	@NpcId uniqueidentifier, 
	@Idx int, 
	@GroupId int, 
	@Kpi int, 
	@DanGrading int 
AS



UPDATE [dbo].[Config_ArenaNpcLink] SET
	[Idx] = @Idx
	,[GroupId] = @GroupId
	,[Kpi] = @Kpi
	,[DanGrading] = @DanGrading
WHERE
	[NpcId] = @NpcId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


