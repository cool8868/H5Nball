
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicarenabagconfig_Delete    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicArenabagconfig_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicArenabagconfig_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicArenabagconfig_GetById    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicArenabagconfig_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicArenabagconfig_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicArenabagconfig_GetAll    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicArenabagconfig_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicArenabagconfig_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicArenabagconfig_Insert    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicArenabagconfig_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicArenabagconfig_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicArenabagconfig_Update    Script Date: 2016年8月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicArenabagconfig_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicArenabagconfig_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicArenabagconfig_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_ArenaBagConfig]
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

CREATE PROCEDURE [dbo].P_DicArenabagconfig_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ArenaBagConfig] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicArenabagconfig_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_ArenaBagConfig] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicArenabagconfig_Insert
	@Idx int
	,@Name nvarchar(50)
	,@Area varchar(50)
	,@AllPosition varchar(50)
	,@Position varchar(50)
	,@PositionDesc varchar(50)
	,@CardLevel varchar(50)
	,@KpiLevel varchar(50)
	,@LeagueLevel varchar(50)
	,@NameEn varchar(50)
	,@Specific varchar(50)
	,@Kpi varchar(50)
	,@Capacity varchar(50)
	,@Speed varchar(50)
	,@Shoot varchar(50)
	,@FreeKick varchar(50)
	,@Balance varchar(50)
	,@Physique varchar(50)
	,@Power varchar(50)
	,@Aggression varchar(50)
	,@Disturb varchar(50)
	,@Interception varchar(50)
	,@Dribble varchar(50)
	,@Pass varchar(50)
	,@Mentality varchar(50)
	,@Response varchar(50)
	,@Positioning varchar(50)
	,@HandControl varchar(50)
	,@Acceleration varchar(50)
	,@Bounce varchar(50)
	,@Club nvarchar(50)
	,@Birthday varchar(10)
	,@Stature varchar(50)
	,@Weight varchar(50)
	,@Nationality nvarchar(50)
	,@Description nvarchar(500)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_ArenaBagConfig] (
	[Idx]
	,[Name]
	,[Area]
	,[AllPosition]
	,[Position]
	,[PositionDesc]
	,[CardLevel]
	,[KpiLevel]
	,[LeagueLevel]
	,[NameEn]
	,[Specific]
	,[Kpi]
	,[Capacity]
	,[Speed]
	,[Shoot]
	,[FreeKick]
	,[Balance]
	,[Physique]
	,[Power]
	,[Aggression]
	,[Disturb]
	,[Interception]
	,[Dribble]
	,[Pass]
	,[Mentality]
	,[Response]
	,[Positioning]
	,[HandControl]
	,[Acceleration]
	,[Bounce]
	,[Club]
	,[Birthday]
	,[Stature]
	,[Weight]
	,[Nationality]
	,[Description]
) VALUES (
    @Idx
    ,@Name
    ,@Area
    ,@AllPosition
    ,@Position
    ,@PositionDesc
    ,@CardLevel
    ,@KpiLevel
    ,@LeagueLevel
    ,@NameEn
    ,@Specific
    ,@Kpi
    ,@Capacity
    ,@Speed
    ,@Shoot
    ,@FreeKick
    ,@Balance
    ,@Physique
    ,@Power
    ,@Aggression
    ,@Disturb
    ,@Interception
    ,@Dribble
    ,@Pass
    ,@Mentality
    ,@Response
    ,@Positioning
    ,@HandControl
    ,@Acceleration
    ,@Bounce
    ,@Club
    ,@Birthday
    ,@Stature
    ,@Weight
    ,@Nationality
    ,@Description
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

CREATE PROCEDURE [dbo].P_DicArenabagconfig_Update
	@Idx int, 
	@Name nvarchar(50), -- 名字
	@Area varchar(50), -- 所属赛区
	@AllPosition varchar(50), 
	@Position varchar(50), -- 场上位置
	@PositionDesc varchar(50), -- 球员实际位置
	@CardLevel varchar(50), -- 卡牌颜色
	@KpiLevel varchar(50), 
	@LeagueLevel varchar(50), -- 联赛级别
	@NameEn varchar(50), 
	@Specific varchar(50), -- 具体
	@Kpi varchar(50), -- 关键属性
	@Capacity varchar(50), -- 能力值
	@Speed varchar(50), -- 速度
	@Shoot varchar(50), -- 射门
	@FreeKick varchar(50), -- 任意球
	@Balance varchar(50), -- 控制
	@Physique varchar(50), -- 体质
	@Power varchar(50), -- 力量
	@Aggression varchar(50), -- 侵略性
	@Disturb varchar(50), -- 干扰
	@Interception varchar(50), -- 抢断
	@Dribble varchar(50), -- 控球
	@Pass varchar(50), -- 传球
	@Mentality varchar(50), -- 意识
	@Response varchar(50), -- 反应
	@Positioning varchar(50), -- 位置感
	@HandControl varchar(50), -- 手控球
	@Acceleration varchar(50), -- 加速度
	@Bounce varchar(50), -- 弹跳
	@Club nvarchar(50), -- 俱乐部
	@Birthday varchar(10), -- 生日
	@Stature varchar(50), -- 身高
	@Weight varchar(50), -- 体重
	@Nationality nvarchar(50), -- 国家
	@Description nvarchar(500) -- 描述
AS



UPDATE [dbo].[Dic_ArenaBagConfig] SET
	[Name] = @Name
	,[Area] = @Area
	,[AllPosition] = @AllPosition
	,[Position] = @Position
	,[PositionDesc] = @PositionDesc
	,[CardLevel] = @CardLevel
	,[KpiLevel] = @KpiLevel
	,[LeagueLevel] = @LeagueLevel
	,[NameEn] = @NameEn
	,[Specific] = @Specific
	,[Kpi] = @Kpi
	,[Capacity] = @Capacity
	,[Speed] = @Speed
	,[Shoot] = @Shoot
	,[FreeKick] = @FreeKick
	,[Balance] = @Balance
	,[Physique] = @Physique
	,[Power] = @Power
	,[Aggression] = @Aggression
	,[Disturb] = @Disturb
	,[Interception] = @Interception
	,[Dribble] = @Dribble
	,[Pass] = @Pass
	,[Mentality] = @Mentality
	,[Response] = @Response
	,[Positioning] = @Positioning
	,[HandControl] = @HandControl
	,[Acceleration] = @Acceleration
	,[Bounce] = @Bounce
	,[Club] = @Club
	,[Birthday] = @Birthday
	,[Stature] = @Stature
	,[Weight] = @Weight
	,[Nationality] = @Nationality
	,[Description] = @Description
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


