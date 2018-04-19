
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Teammember_Delete    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Teammember_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Teammember_Delete]
GO

/****** Object:  Stored Procedure [dbo].Teammember_GetById    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Teammember_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Teammember_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].Teammember_GetAll    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Teammember_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Teammember_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].Teammember_Insert    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Teammember_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Teammember_Insert]
GO

/****** Object:  Stored Procedure [dbo].Teammember_Update    Script Date: 2016年7月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_Teammember_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_Teammember_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_Teammember_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Teammember]
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

CREATE PROCEDURE [dbo].P_Teammember_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Teammember] with(nolock)
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

CREATE PROCEDURE [dbo].P_Teammember_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Teammember] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_Teammember_Insert
	@ManagerId uniqueidentifier , 
	@PlayerId int , 
	@Level int , 
	@UsedProperty int , 
	@Speed float , 
	@Shoot float , 
	@FreeKick float , 
	@Balance float , 
	@Physique float , 
	@Bounce float , 
	@Aggression float , 
	@Disturb float , 
	@Interception float , 
	@Dribble float , 
	@Pass float , 
	@Mentality float , 
	@Response float , 
	@Positioning float , 
	@HandControl float , 
	@Acceleration float , 
	@UsedPlayerCard varbinary(300) , 
	@UsedEquipment varbinary(300) , 
	@Status int , 
	@RowTime datetime , 
	@IsCopyed bit , 
	@IsInherited bit , 
	@UsedBadge varbinary(300) , 
	@ArousalLv int , 
	@UsedClubClothes varbinary(300) , 
	@StrengthenLevel int , 
	@Power float , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Teammember] (
	[Idx],
	[ManagerId]
	,[PlayerId]
	,[Level]
	,[UsedProperty]
	,[Speed]
	,[Shoot]
	,[FreeKick]
	,[Balance]
	,[Physique]
	,[Bounce]
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
	,[UsedPlayerCard]
	,[UsedEquipment]
	,[Status]
	,[RowTime]
	,[IsCopyed]
	,[IsInherited]
	,[UsedBadge]
	,[ArousalLv]
	,[UsedClubClothes]
	,[StrengthenLevel]
	,[Power]
) VALUES (
	@Idx,
    @ManagerId
    ,@PlayerId
    ,@Level
    ,@UsedProperty
    ,@Speed
    ,@Shoot
    ,@FreeKick
    ,@Balance
    ,@Physique
    ,@Bounce
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
    ,@UsedPlayerCard
    ,@UsedEquipment
    ,@Status
    ,@RowTime
    ,@IsCopyed
    ,@IsInherited
    ,@UsedBadge
    ,@ArousalLv
    ,@UsedClubClothes
    ,@StrengthenLevel
    ,@Power
)




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

CREATE PROCEDURE [dbo].P_Teammember_Update
	@Idx uniqueidentifier, -- 球员唯一id
	@ManagerId uniqueidentifier, -- 经理id
	@PlayerId int, -- pid
	@Level int, -- 球员训练等级
	@UsedProperty int, -- 球员已分配属性点
	@Speed float, -- 速度
	@Shoot float, -- 射门
	@FreeKick float, -- 任意球
	@Balance float, -- 控制
	@Physique float, -- 体质
	@Bounce float, -- 弹跳
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
	@UsedPlayerCard varbinary(300), -- 使用的球员卡
	@UsedEquipment varbinary(300), -- 使用的装备
	@Status int, -- 状态：0,初始;1,主力;2,替补
	@RowTime datetime, 
	@IsCopyed bit, 
	@IsInherited bit, 
	@UsedBadge varbinary(300), 
	@ArousalLv int, 
	@UsedClubClothes varbinary(300), 
	@StrengthenLevel int, -- 强化等级
	@Power float 
AS



UPDATE [dbo].[Teammember] SET
	[ManagerId] = @ManagerId
	,[PlayerId] = @PlayerId
	,[Level] = @Level
	,[UsedProperty] = @UsedProperty
	,[Speed] = @Speed
	,[Shoot] = @Shoot
	,[FreeKick] = @FreeKick
	,[Balance] = @Balance
	,[Physique] = @Physique
	,[Bounce] = @Bounce
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
	,[UsedPlayerCard] = @UsedPlayerCard
	,[UsedEquipment] = @UsedEquipment
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[IsCopyed] = @IsCopyed
	,[IsInherited] = @IsInherited
	,[UsedBadge] = @UsedBadge
	,[ArousalLv] = @ArousalLv
	,[UsedClubClothes] = @UsedClubClothes
	,[StrengthenLevel] = @StrengthenLevel
	,[Power] = @Power
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


