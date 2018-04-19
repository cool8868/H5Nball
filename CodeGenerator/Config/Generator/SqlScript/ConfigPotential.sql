
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configpotential_Delete    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotential_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotential_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigPotential_GetById    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotential_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotential_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigPotential_GetAll    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotential_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotential_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigPotential_Insert    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotential_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotential_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigPotential_Update    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPotential_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPotential_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigPotential_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Potential]
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

CREATE PROCEDURE [dbo].P_ConfigPotential_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Potential] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigPotential_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Potential] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigPotential_Insert
	@Idx int
	,@PotentialId int
	,@Name nvarchar(50)
	,@Level int
	,@MinBuff decimal(4, 2)
	,@MaxBuff decimal(4, 2)
	,@BuffType int
	,@GKGetType int
	,@BuffId int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Potential] (
	[Idx]
	,[PotentialId]
	,[Name]
	,[Level]
	,[MinBuff]
	,[MaxBuff]
	,[BuffType]
	,[GKGetType]
	,[BuffId]
) VALUES (
    @Idx
    ,@PotentialId
    ,@Name
    ,@Level
    ,@MinBuff
    ,@MaxBuff
    ,@BuffType
    ,@GKGetType
    ,@BuffId
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

CREATE PROCEDURE [dbo].P_ConfigPotential_Update
	@Idx int, 
	@PotentialId int, -- 潜力ID
	@Name nvarchar(50), -- 潜力名称
	@Level int, -- 潜力等级 1低级 2中级  3高级 
	@MinBuff decimal(4, 2), -- Buff范围值
	@MaxBuff decimal(4, 2), -- buff值
	@BuffType int, -- BUff类型  1=值 2=百分比
	@GKGetType int, -- 守门员获得此属性类型  1所有可活动  2守门员不可得  3只限守门员获得
	@BuffId int 
AS



UPDATE [dbo].[Config_Potential] SET
	[PotentialId] = @PotentialId
	,[Name] = @Name
	,[Level] = @Level
	,[MinBuff] = @MinBuff
	,[MaxBuff] = @MaxBuff
	,[BuffType] = @BuffType
	,[GKGetType] = @GKGetType
	,[BuffId] = @BuffId
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


