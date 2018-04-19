
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configcrowdprize_Delete    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrowdprize_GetById    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigCrowdprize_GetAll    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigCrowdprize_Insert    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigCrowdprize_Update    Script Date: 2016年8月15日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigCrowdprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigCrowdprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigCrowdprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_CrowdPrize]
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

CREATE PROCEDURE [dbo].P_ConfigCrowdprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrowdPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigCrowdprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_CrowdPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigCrowdprize_Insert
	@Idx int
	,@WinType int
	,@Coin int
	,@Honor int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_CrowdPrize] (
	[Idx]
	,[WinType]
	,[Coin]
	,[Honor]
) VALUES (
    @Idx
    ,@WinType
    ,@Coin
    ,@Honor
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

CREATE PROCEDURE [dbo].P_ConfigCrowdprize_Update
	@Idx int, 
	@WinType int, 
	@Coin int, 
	@Honor int 
AS



UPDATE [dbo].[Config_CrowdPrize] SET
	[WinType] = @WinType
	,[Coin] = @Coin
	,[Honor] = @Honor
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


