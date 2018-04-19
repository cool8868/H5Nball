
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanager_Delete    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManager_GetById    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManager_GetAll    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManager_Insert    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManager_Update    Script Date: 2016年6月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManager_Delete
	@Idx uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[NB_Manager]
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_NbManager_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManager_Insert
	@Account varchar(200) , 
	@Name nvarchar(50) , 
	@Logo varchar(200) , 
	@Type int , 
	@Level int , 
	@EXP int , 
	@Sophisticate int , 
	@Score int , 
	@Coin int , 
	@Reiki int , 
	@TeammemberMax int , 
	@TrainSeatMax int , 
	@VipLevel int , 
	@Mod int , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
	@FriendShipPoint int , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_Manager] (
	[Idx],
	[Account]
	,[Name]
	,[Logo]
	,[Type]
	,[Level]
	,[EXP]
	,[Sophisticate]
	,[Score]
	,[Coin]
	,[Reiki]
	,[TeammemberMax]
	,[TrainSeatMax]
	,[VipLevel]
	,[Mod]
	,[Status]
	,[RowTime]
	,[UpdateTime]
	,[FriendShipPoint]
) VALUES (
	@Idx,
    @Account
    ,@Name
    ,@Logo
    ,@Type
    ,@Level
    ,@EXP
    ,@Sophisticate
    ,@Score
    ,@Coin
    ,@Reiki
    ,@TeammemberMax
    ,@TrainSeatMax
    ,@VipLevel
    ,@Mod
    ,@Status
    ,@RowTime
    ,@UpdateTime
    ,@FriendShipPoint
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

CREATE PROCEDURE [dbo].P_NbManager_Update
	@Idx uniqueidentifier, 
	@Account varchar(200), 
	@Name nvarchar(50), 
	@Logo varchar(200), 
	@Type int, -- 经理类型
	@Level int, -- 等级
	@EXP int, -- 经验
	@Sophisticate int, -- 灵气
	@Score int, -- 世界杯积分
	@Coin int, -- 金币
	@Reiki int, -- 灵气
	@TeammemberMax int, -- 球员数上限
	@TrainSeatMax int, -- 训练位上限
	@VipLevel int, -- Vip等级
	@Mod int, 
	@Status int, -- 经理状态：-1,未注册经理;0,正常经理;1,沙包
	@RowTime datetime, 
	@UpdateTime datetime, 
	@RowVersion timestamp, 
	@FriendShipPoint int -- 友情点
AS



UPDATE [dbo].[NB_Manager] SET
	[Account] = @Account
	,[Name] = @Name
	,[Logo] = @Logo
	,[Type] = @Type
	,[Level] = @Level
	,[EXP] = @EXP
	,[Sophisticate] = @Sophisticate
	,[Score] = @Score
	,[Coin] = @Coin
	,[Reiki] = @Reiki
	,[TeammemberMax] = @TeammemberMax
	,[TrainSeatMax] = @TrainSeatMax
	,[VipLevel] = @VipLevel
	,[Mod] = @Mod
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
	,[FriendShipPoint] = @FriendShipPoint
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


