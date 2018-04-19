
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configviplevel_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigViplevel_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigViplevel_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigViplevel_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigViplevel_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigViplevel_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigViplevel_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigViplevel_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigViplevel_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigViplevel_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigViplevel_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigViplevel_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigViplevel_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigViplevel_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigViplevel_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigViplevel_Delete
	@EffectId int
AS

DELETE FROM [dbo].[Config_VipLevel]
WHERE
	[EffectId] = @EffectId

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

CREATE PROCEDURE [dbo].P_ConfigViplevel_GetById
	@EffectId int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_VipLevel] with(nolock)
WHERE
	[EffectId] = @EffectId
	
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

CREATE PROCEDURE [dbo].P_ConfigViplevel_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_VipLevel] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigViplevel_Insert
	@EffectId int
	,@Name nvarchar(100)
	,@Vip0 int
	,@Vip1 int
	,@Vip2 int
	,@Vip3 int
	,@Vip4 int
	,@Vip5 int
	,@Vip6 int
	,@Vip7 int
	,@Vip8 int
	,@Vip9 int
	,@Vip10 int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_VipLevel] (
	[EffectId]
	,[Name]
	,[Vip0]
	,[Vip1]
	,[Vip2]
	,[Vip3]
	,[Vip4]
	,[Vip5]
	,[Vip6]
	,[Vip7]
	,[Vip8]
	,[Vip9]
	,[Vip10]
) VALUES (
    @EffectId
    ,@Name
    ,@Vip0
    ,@Vip1
    ,@Vip2
    ,@Vip3
    ,@Vip4
    ,@Vip5
    ,@Vip6
    ,@Vip7
    ,@Vip8
    ,@Vip9
    ,@Vip10
)

select @EffectId

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

CREATE PROCEDURE [dbo].P_ConfigViplevel_Update
	@EffectId int, 
	@Name nvarchar(100), 
	@Vip0 int, 
	@Vip1 int, 
	@Vip2 int, 
	@Vip3 int, 
	@Vip4 int, 
	@Vip5 int, 
	@Vip6 int, 
	@Vip7 int, 
	@Vip8 int, 
	@Vip9 int, 
	@Vip10 int 
AS



UPDATE [dbo].[Config_VipLevel] SET
	[Name] = @Name
	,[Vip0] = @Vip0
	,[Vip1] = @Vip1
	,[Vip2] = @Vip2
	,[Vip3] = @Vip3
	,[Vip4] = @Vip4
	,[Vip5] = @Vip5
	,[Vip6] = @Vip6
	,[Vip7] = @Vip7
	,[Vip8] = @Vip8
	,[Vip9] = @Vip9
	,[Vip10] = @Vip10
WHERE
	[EffectId] = @EffectId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



