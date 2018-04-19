
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configrevelationvip_Delete    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationvip_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationvip_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationvip_GetById    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationvip_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationvip_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigRevelationvip_GetAll    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationvip_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationvip_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigRevelationvip_Insert    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationvip_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationvip_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigRevelationvip_Update    Script Date: 2016年11月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigRevelationvip_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigRevelationvip_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigRevelationvip_Delete
	@VipLevel int
AS

DELETE FROM [dbo].[Config_RevelationVIP]
WHERE
	[VipLevel] = @VipLevel

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

CREATE PROCEDURE [dbo].P_ConfigRevelationvip_GetById
	@VipLevel int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationVIP] with(nolock)
WHERE
	[VipLevel] = @VipLevel
	
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

CREATE PROCEDURE [dbo].P_ConfigRevelationvip_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_RevelationVIP] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigRevelationvip_Insert
	@VipLevel int
	,@Challenges int
	,@CDTime int
	,@ItemIsBind bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_RevelationVIP] (
	[VipLevel]
	,[Challenges]
	,[CDTime]
	,[ItemIsBind]
) VALUES (
    @VipLevel
    ,@Challenges
    ,@CDTime
    ,@ItemIsBind
)

select @VipLevel

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

CREATE PROCEDURE [dbo].P_ConfigRevelationvip_Update
	@VipLevel int, 
	@Challenges int, -- 可通关次数
	@CDTime int, -- 挑战失败CD时间
	@ItemIsBind bit -- 勇气商城物品是否绑定
AS



UPDATE [dbo].[Config_RevelationVIP] SET
	[Challenges] = @Challenges
	,[CDTime] = @CDTime
	,[ItemIsBind] = @ItemIsBind
WHERE
	[VipLevel] = @VipLevel

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



