
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicsyntheticitem_Delete    Script Date: 2015年10月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSyntheticitem_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSyntheticitem_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicSyntheticitem_GetById    Script Date: 2015年10月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSyntheticitem_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSyntheticitem_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicSyntheticitem_GetAll    Script Date: 2015年10月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSyntheticitem_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSyntheticitem_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicSyntheticitem_Insert    Script Date: 2015年10月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSyntheticitem_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSyntheticitem_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicSyntheticitem_Update    Script Date: 2015年10月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicSyntheticitem_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicSyntheticitem_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicSyntheticitem_Delete
	@ItemCode int
AS

DELETE FROM [dbo].[Dic_SyntheticItem]
WHERE
	[ItemCode] = @ItemCode

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

CREATE PROCEDURE [dbo].P_DicSyntheticitem_GetById
	@ItemCode int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SyntheticItem] with(nolock)
WHERE
	[ItemCode] = @ItemCode
	
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

CREATE PROCEDURE [dbo].P_DicSyntheticitem_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_SyntheticItem] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicSyntheticitem_Insert
	@ItemCode int
	,@TarItemCode int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_SyntheticItem] (
	[ItemCode]
	,[TarItemCode]
) VALUES (
    @ItemCode
    ,@TarItemCode
)

select @ItemCode

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

CREATE PROCEDURE [dbo].P_DicSyntheticitem_Update
	@ItemCode int, 
	@TarItemCode int 
AS



UPDATE [dbo].[Dic_SyntheticItem] SET
	[TarItemCode] = @TarItemCode
WHERE
	[ItemCode] = @ItemCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



