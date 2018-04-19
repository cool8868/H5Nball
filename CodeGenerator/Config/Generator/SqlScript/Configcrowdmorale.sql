
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcrowdmorale_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdmorale_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdmorale_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrowdmorale_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdmorale_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdmorale_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCrowdmorale_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdmorale_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdmorale_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCrowdmorale_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdmorale_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdmorale_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrowdmorale_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdmorale_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdmorale_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCrowdmorale_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_CrowdMorale]
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

CREATE PROCEDURE [dbo].P_ConfigCrowdmorale_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrowdMorale] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigCrowdmorale_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrowdMorale] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCrowdmorale_Insert
	@Idx int
	,@CostMorela int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_CrowdMorale] (
	[Idx]
	,[CostMorela]
) VALUES (
    @Idx
    ,@CostMorela
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

CREATE PROCEDURE [dbo].P_ConfigCrowdmorale_Update
	@Idx int, 
	@CostMorela int 
AS



UPDATE [dbo].[Config_CrowdMorale] SET
	[CostMorela] = @CostMorela
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


