
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicplayercarddec_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayercarddec_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayercarddec_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicPlayercarddec_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayercarddec_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayercarddec_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicPlayercarddec_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayercarddec_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayercarddec_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicPlayercarddec_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayercarddec_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayercarddec_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicPlayercarddec_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayercarddec_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayercarddec_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicPlayercarddec_Delete
	@ItemCode int
AS

DELETE FROM [dbo].[Dic_PlayerCardDec]
WHERE
	[ItemCode] = @ItemCode

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

CREATE PROCEDURE [dbo].P_DicPlayercarddec_GetById
	@ItemCode int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_PlayerCardDec] with(nolock)
WHERE
	[ItemCode] = @ItemCode
	
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

CREATE PROCEDURE [dbo].P_DicPlayercarddec_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_PlayerCardDec] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicPlayercarddec_Insert
	@ItemCode int
	,@Reiki int
	,@SoulRange int
	,@Soul int
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_PlayerCardDec] (
	[ItemCode]
	,[Reiki]
	,[SoulRange]
	,[Soul]
) VALUES (
    @ItemCode
    ,@Reiki
    ,@SoulRange
    ,@Soul
)

select @ItemCode

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

CREATE PROCEDURE [dbo].P_DicPlayercarddec_Update
	@ItemCode int, -- 球员卡ID
	@Reiki int, -- 灵气数量
	@SoulRange int, -- 获得球魂概率
	@Soul int -- 球魂数量
AS



UPDATE [dbo].[Dic_PlayerCardDec] SET
	[Reiki] = @Reiki
	,[SoulRange] = @SoulRange
	,[Soul] = @Soul
WHERE
	[ItemCode] = @ItemCode

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



