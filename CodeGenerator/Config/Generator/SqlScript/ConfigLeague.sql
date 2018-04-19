
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configleague_Delete    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeague_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeague_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeague_GetById    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeague_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeague_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLeague_GetAll    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeague_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeague_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLeague_Insert    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeague_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeague_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLeague_Update    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLeague_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLeague_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLeague_Delete
	@LeagueID int
AS

DELETE FROM [dbo].[Config_League]
WHERE
	[LeagueID] = @LeagueID

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

CREATE PROCEDURE [dbo].P_ConfigLeague_GetById
	@LeagueID int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_League] with(nolock)
WHERE
	[LeagueID] = @LeagueID
	
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

CREATE PROCEDURE [dbo].P_ConfigLeague_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_League] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLeague_Insert
	@LeagueID int
	,@LeagueName nvarchar(50)
	,@Level int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_League] (
	[LeagueID]
	,[LeagueName]
	,[Level]
) VALUES (
    @LeagueID
    ,@LeagueName
    ,@Level
)

select @LeagueID

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

CREATE PROCEDURE [dbo].P_ConfigLeague_Update
	@LeagueID int, 
	@LeagueName nvarchar(50), 
	@Level int -- 开启等级
AS



UPDATE [dbo].[Config_League] SET
	[LeagueName] = @LeagueName
	,[Level] = @Level
WHERE
	[LeagueID] = @LeagueID

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



