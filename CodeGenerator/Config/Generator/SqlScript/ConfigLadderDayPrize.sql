
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configladderdayprize_Delete    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLadderdayprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLadderdayprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLadderdayprize_GetById    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLadderdayprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLadderdayprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLadderdayprize_GetAll    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLadderdayprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLadderdayprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLadderdayprize_Insert    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLadderdayprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLadderdayprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLadderdayprize_Update    Script Date: 2016年5月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLadderdayprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLadderdayprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLadderdayprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_LadderDayPrize]
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

CREATE PROCEDURE [dbo].P_ConfigLadderdayprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LadderDayPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigLadderdayprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_LadderDayPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLadderdayprize_Insert
	@WinNumber int , 
	@PrizeType int , 
	@ItemCode int , 
	@Number int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_LadderDayPrize] (
	[WinNumber]
	,[PrizeType]
	,[ItemCode]
	,[Number]
) VALUES (
    @WinNumber
    ,@PrizeType
    ,@ItemCode
    ,@Number
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

CREATE PROCEDURE [dbo].P_ConfigLadderdayprize_Update
	@Idx int, 
	@WinNumber int, -- 获胜场次
	@PrizeType int, -- 奖励类型  1钻石  2金币 3 ItemCode 4荣誉
	@ItemCode int, -- 奖励具体CODE PrizeType=3时有效
	@Number int -- 数量
AS



UPDATE [dbo].[Config_LadderDayPrize] SET
	[WinNumber] = @WinNumber
	,[PrizeType] = @PrizeType
	,[ItemCode] = @ItemCode
	,[Number] = @Number
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



