
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configarenadangrading_Delete    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenadangrading_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenadangrading_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenadangrading_GetById    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenadangrading_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenadangrading_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigArenadangrading_GetAll    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenadangrading_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenadangrading_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigArenadangrading_Insert    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenadangrading_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenadangrading_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigArenadangrading_Update    Script Date: 2016年8月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigArenadangrading_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigArenadangrading_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigArenadangrading_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_ArenaDanGrading]
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

CREATE PROCEDURE [dbo].P_ConfigArenadangrading_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaDanGrading] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigArenadangrading_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_ArenaDanGrading] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigArenadangrading_Insert
	@Idx int
	,@Integral int
	,@PrizeArenaCoin int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_ArenaDanGrading] (
	[Idx]
	,[Integral]
	,[PrizeArenaCoin]
) VALUES (
    @Idx
    ,@Integral
    ,@PrizeArenaCoin
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

CREATE PROCEDURE [dbo].P_ConfigArenadangrading_Update
	@Idx int, 
	@Integral int, -- 所需积分
	@PrizeArenaCoin int -- 奖励竞技币
AS



UPDATE [dbo].[Config_ArenaDanGrading] SET
	[Integral] = @Integral
	,[PrizeArenaCoin] = @PrizeArenaCoin
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


