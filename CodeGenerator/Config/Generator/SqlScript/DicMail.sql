
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicmail_Delete    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMail_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMail_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicMail_GetById    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMail_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMail_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicMail_GetAll    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMail_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMail_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicMail_Insert    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMail_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMail_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicMail_Update    Script Date: 2016年2月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicMail_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicMail_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicMail_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_Mail]
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

CREATE PROCEDURE [dbo].P_DicMail_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Mail] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicMail_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Mail] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicMail_Insert
	@Idx int
	,@Category int
	,@SourceType int
	,@Title nvarchar(80)
	,@ContentTemplate nvarchar(150)
	,@ContentParam varchar(150)
	,@HasAttach bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Mail] (
	[Idx]
	,[Category]
	,[SourceType]
	,[Title]
	,[ContentTemplate]
	,[ContentParam]
	,[HasAttach]
) VALUES (
    @Idx
    ,@Category
    ,@SourceType
    ,@Title
    ,@ContentTemplate
    ,@ContentParam
    ,@HasAttach
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

CREATE PROCEDURE [dbo].P_DicMail_Update
	@Idx int, 
	@Category int, -- 邮件类别
	@SourceType int, -- 来源类型
	@Title nvarchar(80), -- 标题
	@ContentTemplate nvarchar(150), -- 内容模板
	@ContentParam varchar(150), -- 内容参数
	@HasAttach bit -- 是否有附件
AS



UPDATE [dbo].[Dic_Mail] SET
	[Category] = @Category
	,[SourceType] = @SourceType
	,[Title] = @Title
	,[ContentTemplate] = @ContentTemplate
	,[ContentParam] = @ContentParam
	,[HasAttach] = @HasAttach
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



