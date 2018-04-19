
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configdaysattendprize_Delete    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDaysattendprize_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDaysattendprize_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigDaysattendprize_GetById    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDaysattendprize_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDaysattendprize_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigDaysattendprize_GetAll    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDaysattendprize_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDaysattendprize_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigDaysattendprize_Insert    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDaysattendprize_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDaysattendprize_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigDaysattendprize_Update    Script Date: 2016年2月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigDaysattendprize_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigDaysattendprize_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigDaysattendprize_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_DaysAttendPrize]
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

CREATE PROCEDURE [dbo].P_ConfigDaysattendprize_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_DaysAttendPrize] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigDaysattendprize_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_DaysAttendPrize] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigDaysattendprize_Insert
	@PrizeType int , 
	@PrizeItemCode int , 
	@Count int , 
	@HasDouble bit , 
	@DoubleVipLevel int , 
	@IsBinding bit , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Config_DaysAttendPrize] (
	[PrizeType]
	,[PrizeItemCode]
	,[Count]
	,[HasDouble]
	,[DoubleVipLevel]
	,[IsBinding]
) VALUES (
    @PrizeType
    ,@PrizeItemCode
    ,@Count
    ,@HasDouble
    ,@DoubleVipLevel
    ,@IsBinding
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

CREATE PROCEDURE [dbo].P_ConfigDaysattendprize_Update
	@Idx int, 
	@PrizeType int, -- 奖励类型 1点券 2物品
	@PrizeItemCode int, -- 奖励物品
	@Count int, -- 数量
	@HasDouble bit, -- 是否有双倍奖励
	@DoubleVipLevel int, -- 双倍奖励Vip等级
	@IsBinding bit -- 是否为绑定物品
AS



UPDATE [dbo].[Config_DaysAttendPrize] SET
	[PrizeType] = @PrizeType
	,[PrizeItemCode] = @PrizeItemCode
	,[Count] = @Count
	,[HasDouble] = @HasDouble
	,[DoubleVipLevel] = @DoubleVipLevel
	,[IsBinding] = @IsBinding
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



