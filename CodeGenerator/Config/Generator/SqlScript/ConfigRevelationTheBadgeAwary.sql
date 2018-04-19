
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configrevelationthebadgeawary_Delete    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationthebadgeawary_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationthebadgeawary_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationthebadgeawary_GetById    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationthebadgeawary_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationthebadgeawary_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigRevelationthebadgeawary_GetAll    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationthebadgeawary_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationthebadgeawary_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigRevelationthebadgeawary_Insert    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationthebadgeawary_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationthebadgeawary_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationthebadgeawary_Update    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationthebadgeawary_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationthebadgeawary_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigRevelationthebadgeawary_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_RevelationTheBadgeAwary]
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

CREATE PROCEDURE [dbo].P_ConfigRevelationthebadgeawary_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationTheBadgeAwary] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigRevelationthebadgeawary_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationTheBadgeAwary] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigRevelationthebadgeawary_Insert
	@Idx int
	,@PlayersId int
	,@PersonalResume nvarchar(200)
	,@TheBadgeID int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_RevelationTheBadgeAwary] (
	[Idx]
	,[PlayersId]
	,[PersonalResume]
	,[TheBadgeID]
) VALUES (
    @Idx
    ,@PlayersId
    ,@PersonalResume
    ,@TheBadgeID
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

CREATE PROCEDURE [dbo].P_ConfigRevelationthebadgeawary_Update
	@Idx int, -- 关卡
	@PlayersId int, -- 球员ID
	@PersonalResume nvarchar(200), -- 个人履历
	@TheBadgeID int -- 徽章ID
AS



UPDATE [dbo].[Config_RevelationTheBadgeAwary] SET
	[PlayersId] = @PlayersId
	,[PersonalResume] = @PersonalResume
	,[TheBadgeID] = @TheBadgeID
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



