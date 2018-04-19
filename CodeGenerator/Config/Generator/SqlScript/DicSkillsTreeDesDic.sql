
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicskillstreedesdic_Delete    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstreedesdic_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstreedesdic_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicSkillstreedesdic_GetById    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstreedesdic_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstreedesdic_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicSkillstreedesdic_GetAll    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstreedesdic_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstreedesdic_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicSkillstreedesdic_Insert    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstreedesdic_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstreedesdic_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicSkillstreedesdic_Update    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSkillstreedesdic_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSkillstreedesdic_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicSkillstreedesdic_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_SkillsTreeDesDic]
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

CREATE PROCEDURE [dbo].P_DicSkillstreedesdic_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillsTreeDesDic] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicSkillstreedesdic_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SkillsTreeDesDic] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicSkillstreedesdic_Insert
	@Idx int
	,@SkillCode varchar(50)
	,@SkillLevel int
	,@Description varchar(2000)
	,@Duration int
	,@Status int
	,@RowTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_SkillsTreeDesDic] (
	[Idx]
	,[SkillCode]
	,[SkillLevel]
	,[Description]
	,[Duration]
	,[Status]
	,[RowTime]
) VALUES (
    @Idx
    ,@SkillCode
    ,@SkillLevel
    ,@Description
    ,@Duration
    ,@Status
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_DicSkillstreedesdic_Update
	@Idx int, 
	@SkillCode varchar(50), 
	@SkillLevel int, 
	@Description varchar(2000), 
	@Duration int, 
	@Status int, 
	@RowTime datetime 
AS



UPDATE [dbo].[Dic_SkillsTreeDesDic] SET
	[SkillCode] = @SkillCode
	,[SkillLevel] = @SkillLevel
	,[Description] = @Description
	,[Duration] = @Duration
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



