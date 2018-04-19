
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configrevelationnpclink_Delete    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpclink_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpclink_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationnpclink_GetById    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpclink_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpclink_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigRevelationnpclink_GetAll    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpclink_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpclink_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigRevelationnpclink_Insert    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpclink_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpclink_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationnpclink_Update    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationnpclink_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationnpclink_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigRevelationnpclink_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_RevelationNpcLink]
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

CREATE PROCEDURE [dbo].P_ConfigRevelationnpclink_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationNpcLink] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigRevelationnpclink_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationNpcLink] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigRevelationnpclink_Insert
	@StageId int , 
	@SmallClearanceId int , 
	@NpcId uniqueidentifier , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_RevelationNpcLink] (
	[StageId]
	,[SmallClearanceId]
	,[NpcId]
) VALUES (
    @StageId
    ,@SmallClearanceId
    ,@NpcId
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

CREATE PROCEDURE [dbo].P_ConfigRevelationnpclink_Update
	@Idx int, 
	@StageId int, 
	@SmallClearanceId int, 
	@NpcId uniqueidentifier 
AS



UPDATE [dbo].[Config_RevelationNpcLink] SET
	[StageId] = @StageId
	,[SmallClearanceId] = @SmallClearanceId
	,[NpcId] = @NpcId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



