
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configeverydayactivitylegend_Delete    Script Date: 2016年11月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEverydayactivitylegend_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEverydayactivitylegend_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigEverydayactivitylegend_GetById    Script Date: 2016年11月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEverydayactivitylegend_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEverydayactivitylegend_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigEverydayactivitylegend_GetAll    Script Date: 2016年11月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEverydayactivitylegend_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEverydayactivitylegend_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigEverydayactivitylegend_Insert    Script Date: 2016年11月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEverydayactivitylegend_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEverydayactivitylegend_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigEverydayactivitylegend_Update    Script Date: 2016年11月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigEverydayactivitylegend_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigEverydayactivitylegend_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigEverydayactivitylegend_Delete
	@RefreshDate date
AS

DELETE FROM [dbo].[Config_EverydayActivityLegend]
WHERE
	[RefreshDate] = @RefreshDate

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

CREATE PROCEDURE [dbo].P_ConfigEverydayactivitylegend_GetById
	@RefreshDate date
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EverydayActivityLegend] with(nolock)
WHERE
	[RefreshDate] = @RefreshDate
	
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

CREATE PROCEDURE [dbo].P_ConfigEverydayactivitylegend_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_EverydayActivityLegend] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigEverydayactivitylegend_Insert
	@RefreshDate date
	,@LegendCode int
	,@LegendDebrisCode int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_EverydayActivityLegend] (
	[RefreshDate]
	,[LegendCode]
	,[LegendDebrisCode]
) VALUES (
    @RefreshDate
    ,@LegendCode
    ,@LegendDebrisCode
)

select @RefreshDate

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

CREATE PROCEDURE [dbo].P_ConfigEverydayactivitylegend_Update
	@RefreshDate date, 
	@LegendCode int, 
	@LegendDebrisCode int 
AS



UPDATE [dbo].[Config_EverydayActivityLegend] SET
	[LegendCode] = @LegendCode
	,[LegendDebrisCode] = @LegendDebrisCode
WHERE
	[RefreshDate] = @RefreshDate

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


