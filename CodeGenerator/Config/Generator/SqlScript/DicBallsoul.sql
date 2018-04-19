
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicballsoul_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBallsoul_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBallsoul_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicBallsoul_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBallsoul_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBallsoul_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicBallsoul_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBallsoul_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBallsoul_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicBallsoul_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBallsoul_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBallsoul_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicBallsoul_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicBallsoul_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicBallsoul_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicBallsoul_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_Ballsoul]
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

CREATE PROCEDURE [dbo].P_DicBallsoul_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Ballsoul] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicBallsoul_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Ballsoul] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicBallsoul_Insert
	@Idx int
	,@Name nvarchar(50)
	,@Color int
	,@Level int
	,@Type int
	,@Description nvarchar(200)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Ballsoul] (
	[Idx]
	,[Name]
	,[Color]
	,[Level]
	,[Type]
	,[Description]
) VALUES (
    @Idx
    ,@Name
    ,@Color
    ,@Level
    ,@Type
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

CREATE PROCEDURE [dbo].P_DicBallsoul_Update
	@Idx int, 
	@Name nvarchar(50), 
	@Color int, -- 球魂颜色
	@Level int, 
	@Type int, 
	@Description nvarchar(200) 
AS



UPDATE [dbo].[Dic_Ballsoul] SET
	[Name] = @Name
	,[Color] = @Color
	,[Level] = @Level
	,[Type] = @Type
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



