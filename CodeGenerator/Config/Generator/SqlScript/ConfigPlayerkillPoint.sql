
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configplayerkillpoint_Delete    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerkillpoint_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerkillpoint_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigPlayerkillpoint_GetById    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerkillpoint_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerkillpoint_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigPlayerkillpoint_GetAll    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerkillpoint_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerkillpoint_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigPlayerkillpoint_Insert    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerkillpoint_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerkillpoint_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigPlayerkillpoint_Update    Script Date: 2016年5月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigPlayerkillpoint_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigPlayerkillpoint_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigPlayerkillpoint_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_PlayerkillPoint]
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

CREATE PROCEDURE [dbo].P_ConfigPlayerkillpoint_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PlayerkillPoint] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigPlayerkillpoint_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_PlayerkillPoint] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigPlayerkillpoint_Insert
	@Idx int
	,@VipLevel int
	,@PrizePoint int
	,@TotalPoint int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_PlayerkillPoint] (
	[Idx]
	,[VipLevel]
	,[PrizePoint]
	,[TotalPoint]
) VALUES (
    @Idx
    ,@VipLevel
    ,@PrizePoint
    ,@TotalPoint
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

CREATE PROCEDURE [dbo].P_ConfigPlayerkillpoint_Update
	@Idx int, 
	@VipLevel int, 
	@PrizePoint int, 
	@TotalPoint int 
AS



UPDATE [dbo].[Config_PlayerkillPoint] SET
	[VipLevel] = @VipLevel
	,[PrizePoint] = @PrizePoint
	,[TotalPoint] = @TotalPoint
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


