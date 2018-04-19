
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Dicplayer_Delete    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayer_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayer_Delete]
GO

/****** Object:  Stored Procedure [dbo].DicPlayer_GetById    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayer_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayer_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].DicPlayer_GetAll    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayer_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayer_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].DicPlayer_Insert    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayer_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayer_Insert]
GO

/****** Object:  Stored Procedure [dbo].DicPlayer_Update    Script Date: 2016年5月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_DicPlayer_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_DicPlayer_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_DicPlayer_Delete
	@Idx int
AS

DELETE FROM [dbo].[Dic_Player]
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

CREATE PROCEDURE [dbo].P_DicPlayer_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Player] with(nolock)
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

CREATE PROCEDURE [dbo].P_DicPlayer_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Dic_Player] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_DicPlayer_Insert
	@Idx int
	,@Name nvarchar(50)
	,@Area int
	,@AllPosition nchar(50)
	,@Position int
	,@PositionDesc varchar(50)
	,@CardLevel int
	,@KpiLevel varchar(50)
	,@LeagueLevel int
	,@NameEn nvarchar(50)
	,@Specific float
	,@Kpi float
	,@Capacity int
	,@Speed float
	,@Shoot float
	,@FreeKick float
	,@Balance float
	,@Physique float
	,@Power float
	,@Aggression float
	,@Disturb float
	,@Interception float
	,@Dribble float
	,@Pass float
	,@Mentality float
	,@Response float
	,@Positioning float
	,@HandControl float
	,@Acceleration float
	,@Bounce float
	,@Club nvarchar(50)
	,@Birthday varchar(10)
	,@Stature float
	,@Weight float
	,@Nationality nvarchar(50)
	,@Description nvarchar(500)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[Dic_Player] (
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

CREATE PROCEDURE [dbo].P_DicPlayer_Update
	@Idx int, 
	@Name nvarchar(50), -- 名字
	@Area int, -- 所属赛区
	@AllPosition nchar(50), 
	@Position int, -- 场上位置
	@PositionDesc varchar(50), -- 球员实际位置
	@CardLevel int, -- 卡牌颜色
	@KpiLevel varchar(50), 
	@LeagueLevel int, -- 联赛级别
	@NameEn nvarchar(50), 
	@Specific float, -- 具体
	@Kpi float, -- 关键属性
	@Capacity int, -- 能力值
	@Speed float, -- 速度
	@Shoot float, -- 射门
	@FreeKick float, -- 任意球
	@Balance float, -- 控制
	@Physique float, -- 体质
	@Power float, -- 力量
	@Aggression float, -- 侵略性
	@Disturb float, -- 干扰
	@Interception float, -- 抢断
	@Dribble float, -- 控球
	@Pass float, -- 传球
	@Mentality float, -- 意识
	@Response float, -- 反应
	@Positioning float, -- 位置感
	@HandControl float, -- 手控球
	@Acceleration float, -- 加速度
	@Bounce float, -- 弹跳
	@Club nvarchar(50), -- 俱乐部
	@Birthday varchar(10), -- 生日
	@Stature float, -- 身高
	@Weight float, -- 体重
	@Nationality nvarchar(50), -- 国家
	@Description nvarchar(500) -- 描述
AS



UPDATE [dbo].[Dic_Player] SET
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



