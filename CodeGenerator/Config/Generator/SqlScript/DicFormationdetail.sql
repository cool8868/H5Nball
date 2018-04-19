
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicformationdetail_Delete    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationdetail_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationdetail_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicFormationdetail_GetById    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationdetail_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationdetail_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicFormationdetail_GetAll    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationdetail_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationdetail_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicFormationdetail_Insert    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationdetail_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationdetail_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicFormationdetail_Update    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicFormationdetail_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicFormationdetail_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicFormationdetail_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_FormationDetail]
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

CREATE PROCEDURE [dbo].P_DicFormationdetail_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_FormationDetail] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicFormationdetail_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_FormationDetail] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicFormationdetail_Insert
	@Idx int
	,@FormationId int
	,@Position int
	,@Coordinate varchar(10)
	,@SpecificPoint int
	,@SpecificPointDesc varchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_FormationDetail] (
	[Idx]
	,[FormationId]
	,[Position]
	,[Coordinate]
	,[SpecificPoint]
	,[SpecificPointDesc]
) VALUES (
    @Idx
    ,@FormationId
    ,@Position
    ,@Coordinate
    ,@SpecificPoint
    ,@SpecificPointDesc
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

CREATE PROCEDURE [dbo].P_DicFormationdetail_Update
	@Idx int, 
	@FormationId int, 
	@Position int, 
	@Coordinate varchar(10), 
	@SpecificPoint int, 
	@SpecificPointDesc varchar(50) 
AS



UPDATE [dbo].[Dic_FormationDetail] SET
	[FormationId] = @FormationId
	,[Position] = @Position
	,[Coordinate] = @Coordinate
	,[SpecificPoint] = @SpecificPoint
	,[SpecificPointDesc] = @SpecificPointDesc
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



