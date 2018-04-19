
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configshare_Delete    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigShare_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigShare_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigShare_GetById    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigShare_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigShare_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigShare_GetAll    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigShare_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigShare_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigShare_Insert    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigShare_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigShare_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigShare_Update    Script Date: 2016年5月30日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigShare_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigShare_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigShare_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Share]
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

CREATE PROCEDURE [dbo].P_ConfigShare_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Share] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigShare_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Share] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigShare_Insert
	@ShareType int , 
	@IsRepetition bit , 
	@PrizeType int , 
	@PrizeItemType int , 
	@SubType int , 
	@Number int , 
	@MaxShareNumber int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_Share] (
	[ShareType]
	,[IsRepetition]
	,[PrizeType]
	,[PrizeItemType]
	,[SubType]
	,[Number]
	,[MaxShareNumber]
) VALUES (
    @ShareType
    ,@IsRepetition
    ,@PrizeType
    ,@PrizeItemType
    ,@SubType
    ,@Number
    ,@MaxShareNumber
)


SET @Idx = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_ConfigShare_Update
	@Idx int, 
	@ShareType int, -- 分享类型
	@IsRepetition bit, -- 是否可重复分享
	@PrizeType int, -- 奖励类型 0 = 首次分享 1=非首次分享
	@PrizeItemType int, -- 奖励物品类型 1=钻石 2=金币 3=指定物品
	@SubType int, -- 奖励物品code
	@Number int, -- 奖励物品数量
	@MaxShareNumber int -- 可重复分享，最多每天能分享多少次
AS



UPDATE [dbo].[Config_Share] SET
	[ShareType] = @ShareType
	,[IsRepetition] = @IsRepetition
	,[PrizeType] = @PrizeType
	,[PrizeItemType] = @PrizeItemType
	,[SubType] = @SubType
	,[Number] = @Number
	,[MaxShareNumber] = @MaxShareNumber
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



