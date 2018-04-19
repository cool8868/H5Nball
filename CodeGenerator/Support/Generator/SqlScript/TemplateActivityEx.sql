
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Templateactivityex_Delete    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityex_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityex_Delete]
GO

/****** Object:  Stored Procedure [dbo].TemplateActivityex_GetById    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityex_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityex_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TemplateActivityex_GetAll    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityex_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityex_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TemplateActivityex_Insert    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityex_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityex_Insert]
GO

/****** Object:  Stored Procedure [dbo].TemplateActivityex_Update    Script Date: 2016年3月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TemplateActivityex_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TemplateActivityex_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TemplateActivityex_Delete
	@Idx int
AS

DELETE FROM [dbo].[Template_ActivityEx]
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

CREATE PROCEDURE [dbo].P_TemplateActivityex_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_ActivityEx] with(nolock)
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

CREATE PROCEDURE [dbo].P_TemplateActivityex_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Template_ActivityEx] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TemplateActivityex_Insert
	@Idx int
	,@Name nvarchar(100)
	,@ImageId int
	,@Status int
	,@RowTime datetime
	,@UpdateTime datetime
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Template_ActivityEx] (
	[Idx]
	,[Name]
	,[ImageId]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @Idx
    ,@Name
    ,@ImageId
    ,@Status
    ,@RowTime
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_TemplateActivityex_Update
	@Idx int, 
	@Name nvarchar(100), 
	@ImageId int, 
	@Status int, --  活动状态：0：正常，1：已结束
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Template_ActivityEx] SET
	[Name] = @Name
	,[ImageId] = @ImageId
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



