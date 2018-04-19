
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configolympicthegoldmedal_Delete    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicthegoldmedal_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicthegoldmedal_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigOlympicthegoldmedal_GetById    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicthegoldmedal_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicthegoldmedal_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigOlympicthegoldmedal_GetAll    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicthegoldmedal_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicthegoldmedal_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigOlympicthegoldmedal_Insert    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicthegoldmedal_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicthegoldmedal_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigOlympicthegoldmedal_Update    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigOlympicthegoldmedal_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigOlympicthegoldmedal_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigOlympicthegoldmedal_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_OlympicTheGoldMedal]
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

CREATE PROCEDURE [dbo].P_ConfigOlympicthegoldmedal_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_OlympicTheGoldMedal] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigOlympicthegoldmedal_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_OlympicTheGoldMedal] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigOlympicthegoldmedal_Insert
	@Idx int
	,@GetType int
	,@TheGoldMedalId int
	,@Rate int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_OlympicTheGoldMedal] (
	[Idx]
	,[GetType]
	,[TheGoldMedalId]
	,[Rate]
) VALUES (
    @Idx
    ,@GetType
    ,@TheGoldMedalId
    ,@Rate
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

CREATE PROCEDURE [dbo].P_ConfigOlympicthegoldmedal_Update
	@Idx int, 
	@GetType int, -- 掉落类型 1= 金币球探 2=钻石球探 3=友情的球探 4=友谊赛 5=紫卡分解 6=橙卡元老分解
	@TheGoldMedalId int, -- 金牌ID
	@Rate int -- 概率
AS



UPDATE [dbo].[Config_OlympicTheGoldMedal] SET
	[GetType] = @GetType
	,[TheGoldMedalId] = @TheGoldMedalId
	,[Rate] = @Rate
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


