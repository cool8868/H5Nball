
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicnewplayerpack_Delete    Script Date: 2016年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNewplayerpack_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNewplayerpack_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicNewplayerpack_GetById    Script Date: 2016年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNewplayerpack_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNewplayerpack_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicNewplayerpack_GetAll    Script Date: 2016年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNewplayerpack_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNewplayerpack_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicNewplayerpack_Insert    Script Date: 2016年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNewplayerpack_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNewplayerpack_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicNewplayerpack_Update    Script Date: 2016年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicNewplayerpack_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicNewplayerpack_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicNewplayerpack_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_NewPlayerPack]
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

CREATE PROCEDURE [dbo].P_DicNewplayerpack_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_NewPlayerPack] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicNewplayerpack_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_NewPlayerPack] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicNewplayerpack_Insert
	@Idx int
	,@PackId int
	,@Type int
	,@SubType int
	,@Strength int
	,@Count int
	,@IsBinding bit
	,@Description nvarchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_NewPlayerPack] (
	[Idx]
	,[PackId]
	,[Type]
	,[SubType]
	,[Strength]
	,[Count]
	,[IsBinding]
	,[Description]
) VALUES (
    @Idx
    ,@PackId
    ,@Type
    ,@SubType
    ,@Strength
    ,@Count
    ,@IsBinding
    ,@Description
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

CREATE PROCEDURE [dbo].P_DicNewplayerpack_Update
	@Idx int, 
	@PackId int, 
	@Type int, 
	@SubType int, 
	@Strength int, 
	@Count int, 
	@IsBinding bit, 
	@Description nvarchar(50) 
AS



UPDATE [dbo].[Dic_NewPlayerPack] SET
	[PackId] = @PackId
	,[Type] = @Type
	,[SubType] = @SubType
	,[Strength] = @Strength
	,[Count] = @Count
	,[IsBinding] = @IsBinding
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



