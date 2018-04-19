
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicplayerlink_Delete    Script Date: 2016年7月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayerlink_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayerlink_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicPlayerlink_GetById    Script Date: 2016年7月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayerlink_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayerlink_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicPlayerlink_GetAll    Script Date: 2016年7月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayerlink_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayerlink_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicPlayerlink_Insert    Script Date: 2016年7月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayerlink_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayerlink_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicPlayerlink_Update    Script Date: 2016年7月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayerlink_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayerlink_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicPlayerlink_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_PlayerLink]
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

CREATE PROCEDURE [dbo].P_DicPlayerlink_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_PlayerLink] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicPlayerlink_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_PlayerLink] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicPlayerlink_Insert
	@Idx int
	,@LinkId int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_PlayerLink] (
	[Idx]
	,[LinkId]
) VALUES (
    @Idx
    ,@LinkId
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

CREATE PROCEDURE [dbo].P_DicPlayerlink_Update
	@Idx int, 
	@LinkId int 
AS



UPDATE [dbo].[Dic_PlayerLink] SET
	[LinkId] = @LinkId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


