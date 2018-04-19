
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Configlottery_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLottery_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLottery_Delete]
GO

/****** Object:  Stored Procedure [dbo].ConfigLottery_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLottery_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLottery_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ConfigLottery_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLottery_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLottery_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ConfigLottery_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLottery_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLottery_Insert]
GO

/****** Object:  Stored Procedure [dbo].ConfigLottery_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ConfigLottery_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ConfigLottery_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ConfigLottery_Delete
	@Idx int
AS

DELETE FROM [dbo].[Config_Lottery]
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

CREATE PROCEDURE [dbo].P_ConfigLottery_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Lottery] with(nolock)
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

CREATE PROCEDURE [dbo].P_ConfigLottery_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Config_Lottery] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ConfigLottery_Insert
	@Idx int
	,@Name nvarchar(50)
	,@Type int
	,@SubType int
	,@MinLevel int
	,@MaxLevel int
	,@MinVip int
	,@MaxVip int
	,@MinTime datetime
	,@MaxTime datetime
	,@Strength int
	,@IsBinding bit
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Config_Lottery] (
	[Idx]
	,[Name]
	,[Type]
	,[SubType]
	,[MinLevel]
	,[MaxLevel]
	,[MinVip]
	,[MaxVip]
	,[MinTime]
	,[MaxTime]
	,[Strength]
	,[IsBinding]
) VALUES (
    @Idx
    ,@Name
    ,@Type
    ,@SubType
    ,@MinLevel
    ,@MaxLevel
    ,@MinVip
    ,@MaxVip
    ,@MinTime
    ,@MaxTime
    ,@Strength
    ,@IsBinding
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

CREATE PROCEDURE [dbo].P_ConfigLottery_Update
	@Idx int, 
	@Name nvarchar(50), -- 抽奖库名称
	@Type int, -- 抽奖类型
	@SubType int, -- 抽奖二级分类
	@MinLevel int, -- 经理等级min
	@MaxLevel int, -- 经理等级max
	@MinVip int, -- Vip等级min
	@MaxVip int, -- Vip等级max
	@MinTime datetime, 
	@MaxTime datetime, 
	@Strength int, -- 强化等级
	@IsBinding bit -- 是否绑定
AS



UPDATE [dbo].[Config_Lottery] SET
	[Name] = @Name
	,[Type] = @Type
	,[SubType] = @SubType
	,[MinLevel] = @MinLevel
	,[MaxLevel] = @MaxLevel
	,[MinVip] = @MinVip
	,[MaxVip] = @MaxVip
	,[MinTime] = @MinTime
	,[MaxTime] = @MaxTime
	,[Strength] = @Strength
	,[IsBinding] = @IsBinding
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



