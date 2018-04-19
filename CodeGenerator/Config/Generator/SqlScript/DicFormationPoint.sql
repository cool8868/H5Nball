
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicformationpoint_Delete    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationpoint_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationpoint_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicFormationpoint_GetById    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationpoint_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationpoint_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicFormationpoint_GetAll    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationpoint_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationpoint_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicFormationpoint_Insert    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationpoint_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationpoint_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicFormationpoint_Update    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationpoint_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationpoint_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicFormationpoint_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_FormationPoint]
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

CREATE PROCEDURE [dbo].P_DicFormationpoint_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_FormationPoint] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicFormationpoint_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_FormationPoint] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicFormationpoint_Insert
	@Idx int
	,@PlayerPoint varchar(50)
	,@BallParkPoint varchar(50)
	,@Buff int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_FormationPoint] (
	[Idx]
	,[PlayerPoint]
	,[BallParkPoint]
	,[Buff]
) VALUES (
    @Idx
    ,@PlayerPoint
    ,@BallParkPoint
    ,@Buff
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

CREATE PROCEDURE [dbo].P_DicFormationpoint_Update
	@Idx int, 
	@PlayerPoint varchar(50), -- 球员位置
	@BallParkPoint varchar(50), -- 球场位置
	@Buff int -- buff百分比
AS



UPDATE [dbo].[Dic_FormationPoint] SET
	[PlayerPoint] = @PlayerPoint
	,[BallParkPoint] = @BallParkPoint
	,[Buff] = @Buff
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



