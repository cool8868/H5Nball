
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Templateregisterplayer_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegisterplayer_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegisterplayer_Delete]
GO

/****** Object:  Stored Procedure [dbo].TemplateRegisterplayer_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegisterplayer_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegisterplayer_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TemplateRegisterplayer_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegisterplayer_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegisterplayer_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TemplateRegisterplayer_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegisterplayer_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegisterplayer_Insert]
GO

/****** Object:  Stored Procedure [dbo].TemplateRegisterplayer_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateRegisterplayer_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateRegisterplayer_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TemplateRegisterplayer_Delete
	@Idx int
AS

DELETE FROM [dbo].[Template_RegisterPlayer]
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

CREATE PROCEDURE [dbo].P_TemplateRegisterplayer_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_RegisterPlayer] with(nolock)
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

CREATE PROCEDURE [dbo].P_TemplateRegisterplayer_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_RegisterPlayer] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TemplateRegisterplayer_Insert
	@Idx int
	,@TemplateId int
	,@PlayerId int
	,@Position int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Template_RegisterPlayer] (
	[Idx]
	,[TemplateId]
	,[PlayerId]
	,[Position]
) VALUES (
    @Idx
    ,@TemplateId
    ,@PlayerId
    ,@Position
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

CREATE PROCEDURE [dbo].P_TemplateRegisterplayer_Update
	@Idx int, 
	@TemplateId int, -- 模板id
	@PlayerId int, -- 球员pid
	@Position int -- 球员位置
AS



UPDATE [dbo].[Template_RegisterPlayer] SET
	[TemplateId] = @TemplateId
	,[PlayerId] = @PlayerId
	,[Position] = @Position
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



