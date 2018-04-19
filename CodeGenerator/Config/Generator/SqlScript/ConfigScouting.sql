
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configscouting_Delete    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigScouting_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigScouting_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigScouting_GetById    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigScouting_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigScouting_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigScouting_GetAll    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigScouting_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigScouting_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigScouting_Insert    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigScouting_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigScouting_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigScouting_Update    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigScouting_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigScouting_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigScouting_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Scouting]
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

CREATE PROCEDURE [dbo].P_ConfigScouting_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Scouting] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigScouting_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Scouting] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigScouting_Insert
	@Idx int
	,@Name nvarchar(50)
	,@Type int
	,@MallCode int
	,@HasTen bit
	,@OrangeLib int
	,@LowLib int
	,@Description nvarchar(100)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Scouting] (
	[Idx]
	,[Name]
	,[Type]
	,[MallCode]
	,[HasTen]
	,[OrangeLib]
	,[LowLib]
	,[Description]
) VALUES (
    @Idx
    ,@Name
    ,@Type
    ,@MallCode
    ,@HasTen
    ,@OrangeLib
    ,@LowLib
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

CREATE PROCEDURE [dbo].P_ConfigScouting_Update
	@Idx int, 
	@Name nvarchar(50), -- 球探名称
	@Type int, -- 类型
	@MallCode int, -- 商品编码
	@HasTen bit, 
	@OrangeLib int, -- 橙卡包卡库
	@LowLib int, 
	@Description nvarchar(100) -- 描述
AS



UPDATE [dbo].[Config_Scouting] SET
	[Name] = @Name
	,[Type] = @Type
	,[MallCode] = @MallCode
	,[HasTen] = @HasTen
	,[OrangeLib] = @OrangeLib
	,[LowLib] = @LowLib
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



