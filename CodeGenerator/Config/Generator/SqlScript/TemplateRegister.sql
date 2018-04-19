
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Templateregister_Delete    Script Date: 2015年10月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegister_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegister_Delete]
GO

/****** Object:  Stored Procedure [dbo].TemplateRegister_GetById    Script Date: 2015年10月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegister_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegister_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TemplateRegister_GetAll    Script Date: 2015年10月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegister_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegister_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TemplateRegister_Insert    Script Date: 2015年10月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegister_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegister_Insert]
GO

/****** Object:  Stored Procedure [dbo].TemplateRegister_Update    Script Date: 2015年10月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegister_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegister_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TemplateRegister_Delete
	@Idx int
AS

DELETE FROM [dbo].[Template_Register]
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

CREATE PROCEDURE [dbo].P_TemplateRegister_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_Register] with(nolock)
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

CREATE PROCEDURE [dbo].P_TemplateRegister_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_Register] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TemplateRegister_Insert
	@Idx int
	,@SolutionString char(65)
	,@Kpi int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Template_Register] (
	[Idx]
	,[SolutionString]
	,[Kpi]
) VALUES (
    @Idx
    ,@SolutionString
    ,@Kpi
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

CREATE PROCEDURE [dbo].P_TemplateRegister_Update
	@Idx int, -- 模板id
	@SolutionString char(65), -- 阵型字符串
	@Kpi int 
AS



UPDATE [dbo].[Template_Register] SET
	[SolutionString] = @SolutionString
	,[Kpi] = @Kpi
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



