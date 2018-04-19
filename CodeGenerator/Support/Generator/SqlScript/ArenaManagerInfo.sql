
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Arenamanagerinfo_Delete    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerinfo_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerinfo_Delete]
GO

/****** Object:  Stored Procedure [dbo].ArenaManagerinfo_GetById    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerinfo_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerinfo_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ArenaManagerinfo_GetAll    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerinfo_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerinfo_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ArenaManagerinfo_Insert    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerinfo_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerinfo_Insert]
GO

/****** Object:  Stored Procedure [dbo].ArenaManagerinfo_Update    Script Date: 2016年9月1日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ArenaManagerinfo_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ArenaManagerinfo_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ArenaManagerinfo_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Arena_ManagerInfo]
WHERE
	[ManagerId] = @ManagerId

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

CREATE PROCEDURE [dbo].P_ArenaManagerinfo_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_ManagerInfo] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
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

CREATE PROCEDURE [dbo].P_ArenaManagerinfo_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Arena_ManagerInfo] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ArenaManagerinfo_Insert
	@ManagerName varchar(50) , 
	@SiteId varchar(50) , 
	@ZoneName varchar(50) , 
	@Logo varchar(500) , 
	@ChampionNumber int , 
	@Integral int , 
	@DanGrading int , 
	@ArenaCoin int , 
	@ArenaType int , 
	@Stamina int , 
	@MaxStamina int , 
	@BuyStaminaNumber int , 
	@StaminaRestoreTime datetime , 
	@Rank int , 
	@Status int , 
	@Teammember1Status bit , 
	@Teammember2Status bit , 
	@Teammember3Status bit , 
	@Teammember4Status bit , 
	@Teammember5Status bit , 
	@UpdateTime datetime , 
	@RowTime datetime , 
	@Opponent varbinary(max) , 
	@DomainId int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Arena_ManagerInfo] (
	[ManagerId],
	[ManagerName]
	,[SiteId]
	,[ZoneName]
	,[Logo]
	,[ChampionNumber]
	,[Integral]
	,[DanGrading]
	,[ArenaCoin]
	,[ArenaType]
	,[Stamina]
	,[MaxStamina]
	,[BuyStaminaNumber]
	,[StaminaRestoreTime]
	,[Rank]
	,[Status]
	,[Teammember1Status]
	,[Teammember2Status]
	,[Teammember3Status]
	,[Teammember4Status]
	,[Teammember5Status]
	,[UpdateTime]
	,[RowTime]
	,[Opponent]
	,[DomainId]
) VALUES (
	@ManagerId,
    @ManagerName
    ,@SiteId
    ,@ZoneName
    ,@Logo
    ,@ChampionNumber
    ,@Integral
    ,@DanGrading
    ,@ArenaCoin
    ,@ArenaType
    ,@Stamina
    ,@MaxStamina
    ,@BuyStaminaNumber
    ,@StaminaRestoreTime
    ,@Rank
    ,@Status
    ,@Teammember1Status
    ,@Teammember2Status
    ,@Teammember3Status
    ,@Teammember4Status
    ,@Teammember5Status
    ,@UpdateTime
    ,@RowTime
    ,@Opponent
    ,@DomainId
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

CREATE PROCEDURE [dbo].P_ArenaManagerinfo_Update
	@ManagerId uniqueidentifier, -- 经理ID
	@ManagerName varchar(50), -- 经理名
	@SiteId varchar(50), 
	@ZoneName varchar(50), -- 区id
	@Logo varchar(500), 
	@ChampionNumber int, -- 获得冠军次数
	@Integral int, -- 积分
	@DanGrading int, -- 段位
	@ArenaCoin int, -- 竞技币数量
	@ArenaType int, -- 竞技场类型
	@Stamina int, -- 体力
	@MaxStamina int, -- 最大体力
	@BuyStaminaNumber int, -- 购买体力次数
	@StaminaRestoreTime datetime, -- 体力恢复记录时间
	@Rank int, -- 排名
	@Status int, -- 状态
	@Teammember1Status bit, -- 球队是否组建完成
	@Teammember2Status bit, 
	@Teammember3Status bit, 
	@Teammember4Status bit, 
	@Teammember5Status bit, 
	@UpdateTime datetime, 
	@RowTime datetime, 
	@Opponent varbinary(max), 
	@DomainId int 
AS



UPDATE [dbo].[Arena_ManagerInfo] SET
	[ManagerName] = @ManagerName
	,[SiteId] = @SiteId
	,[ZoneName] = @ZoneName
	,[Logo] = @Logo
	,[ChampionNumber] = @ChampionNumber
	,[Integral] = @Integral
	,[DanGrading] = @DanGrading
	,[ArenaCoin] = @ArenaCoin
	,[ArenaType] = @ArenaType
	,[Stamina] = @Stamina
	,[MaxStamina] = @MaxStamina
	,[BuyStaminaNumber] = @BuyStaminaNumber
	,[StaminaRestoreTime] = @StaminaRestoreTime
	,[Rank] = @Rank
	,[Status] = @Status
	,[Teammember1Status] = @Teammember1Status
	,[Teammember2Status] = @Teammember2Status
	,[Teammember3Status] = @Teammember3Status
	,[Teammember4Status] = @Teammember4Status
	,[Teammember5Status] = @Teammember5Status
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
	,[Opponent] = @Opponent
	,[DomainId] = @DomainId
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


