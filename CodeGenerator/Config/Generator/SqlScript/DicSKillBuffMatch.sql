
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicskillbuffmatch_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillbuffmatch_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillbuffmatch_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicSkillbuffmatch_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillbuffmatch_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillbuffmatch_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicSkillbuffmatch_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillbuffmatch_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillbuffmatch_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicSkillbuffmatch_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillbuffmatch_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillbuffmatch_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicSkillbuffmatch_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillbuffmatch_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillbuffmatch_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicSkillbuffmatch_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_SkillBuffMatch]
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

CREATE PROCEDURE [dbo].P_DicSkillbuffmatch_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillBuffMatch] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicSkillbuffmatch_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillBuffMatch] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicSkillbuffmatch_Insert
	@Idx int
	,@Type int
	,@LinkId varchar(30)
	,@LinkType varchar(30)
	,@BuffEngineId varchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_SkillBuffMatch] (
	[Idx]
	,[Type]
	,[LinkId]
	,[LinkType]
	,[BuffEngineId]
) VALUES (
    @Idx
    ,@Type
    ,@LinkId
    ,@LinkType
    ,@BuffEngineId
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

CREATE PROCEDURE [dbo].P_DicSkillbuffmatch_Update
	@Idx int, 
	@Type int, 
	@LinkId varchar(30), 
	@LinkType varchar(30), 
	@BuffEngineId varchar(50) 
AS



UPDATE [dbo].[Dic_SkillBuffMatch] SET
	[Type] = @Type
	,[LinkId] = @LinkId
	,[LinkType] = @LinkType
	,[BuffEngineId] = @BuffEngineId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



