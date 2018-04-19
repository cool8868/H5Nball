
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configarenanpcopponent_Delete    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpcopponent_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpcopponent_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenanpcopponent_GetById    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpcopponent_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpcopponent_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigArenanpcopponent_GetAll    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpcopponent_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpcopponent_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigArenanpcopponent_Insert    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpcopponent_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpcopponent_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenanpcopponent_Update    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenanpcopponent_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenanpcopponent_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigArenanpcopponent_Delete
	@Idx int,
	@Opponent int
AS

DELETE FROM [dbo].[Config_ArenaNpcOpponent]
WHERE
	[Idx] = @Idx
	AND [Opponent] = @Opponent

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

CREATE PROCEDURE [dbo].P_ConfigArenanpcopponent_GetById
	@Idx int,
	@Opponent int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaNpcOpponent] with(nolock)
WHERE
	[Idx] = @Idx
	AND [Opponent] = @Opponent
	
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

CREATE PROCEDURE [dbo].P_ConfigArenanpcopponent_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaNpcOpponent] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigArenanpcopponent_Insert
	@Idx int
	,@Opponent int
	,@GroupId int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_ArenaNpcOpponent] (
	[Idx]
	,[Opponent]
	,[GroupId]
) VALUES (
    @Idx
    ,@Opponent
    ,@GroupId
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

CREATE PROCEDURE [dbo].P_ConfigArenanpcopponent_Update
	@Idx int, -- 段位
	@Opponent int, -- 对手序号
	@GroupId int -- 分组ID
AS



UPDATE [dbo].[Config_ArenaNpcOpponent] SET
	[GroupId] = @GroupId
WHERE
	[Idx] = @Idx
	AND [Opponent] = @Opponent

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


