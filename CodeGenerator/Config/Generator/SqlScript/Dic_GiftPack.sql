
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicgiftpack_Delete    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpack_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpack_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicGiftpack_GetById    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpack_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpack_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicGiftpack_GetAll    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpack_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpack_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicGiftpack_Insert    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpack_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpack_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicGiftpack_Update    Script Date: 2016年2月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicGiftpack_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicGiftpack_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicGiftpack_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_GiftPack]
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

CREATE PROCEDURE [dbo].P_DicGiftpack_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_GiftPack] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicGiftpack_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_GiftPack] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicGiftpack_Insert
	@Idx int
	,@Name nvarchar(50)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_GiftPack] (
	[Idx]
	,[Name]
) VALUES (
    @Idx
    ,@Name
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

CREATE PROCEDURE [dbo].P_DicGiftpack_Update
	@Idx int, 
	@Name nvarchar(50) 
AS



UPDATE [dbo].[Dic_GiftPack] SET
	[Name] = @Name
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



