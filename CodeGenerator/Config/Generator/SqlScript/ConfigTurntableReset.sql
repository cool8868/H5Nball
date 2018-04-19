
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configturntablereset_Delete    Script Date: 2016年7月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntablereset_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntablereset_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigTurntablereset_GetById    Script Date: 2016年7月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntablereset_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntablereset_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigTurntablereset_GetAll    Script Date: 2016年7月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntablereset_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntablereset_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigTurntablereset_Insert    Script Date: 2016年7月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntablereset_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntablereset_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigTurntablereset_Update    Script Date: 2016年7月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigTurntablereset_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigTurntablereset_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigTurntablereset_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_TurntableReset]
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

CREATE PROCEDURE [dbo].P_ConfigTurntablereset_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_TurntableReset] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigTurntablereset_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_TurntableReset] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigTurntablereset_Insert
	@Idx int
	,@TurntableType int
	,@ResetNumber int
	,@Point int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_TurntableReset] (
	[Idx]
	,[TurntableType]
	,[ResetNumber]
	,[Point]
) VALUES (
    @Idx
    ,@TurntableType
    ,@ResetNumber
    ,@Point
)

select @Idx

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

CREATE PROCEDURE [dbo].P_ConfigTurntablereset_Update
	@Idx int, 
	@TurntableType int, 
	@ResetNumber int, 
	@Point int 
AS



UPDATE [dbo].[Config_TurntableReset] SET
	[TurntableType] = @TurntableType
	,[ResetNumber] = @ResetNumber
	,[Point] = @Point
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


