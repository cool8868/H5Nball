
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Olympicmanager_Delete    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].OlympicManager_GetById    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].OlympicManager_GetAll    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].OlympicManager_Insert    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].OlympicManager_Update    Script Date: 2016年7月29日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_OlympicManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Olympic_Manager]
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

CREATE PROCEDURE [dbo].P_OlympicManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Olympic_Manager] with(nolock)
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

CREATE PROCEDURE [dbo].P_OlympicManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Olympic_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_OlympicManager_Insert
	@Football int , 
	@Basketball int , 
	@Volleyball int , 
	@Swimming int , 
	@Gymnastics int , 
	@Shooting int , 
	@TrackAndField int , 
	@WeightLifting int , 
	@TableTennis int , 
	@Badminton int , 
	@Rowing int , 
	@Judo int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Olympic_Manager] (
	[ManagerId],
	[Football]
	,[Basketball]
	,[Volleyball]
	,[Swimming]
	,[Gymnastics]
	,[Shooting]
	,[TrackAndField]
	,[WeightLifting]
	,[TableTennis]
	,[Badminton]
	,[Rowing]
	,[Judo]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @Football
    ,@Basketball
    ,@Volleyball
    ,@Swimming
    ,@Gymnastics
    ,@Shooting
    ,@TrackAndField
    ,@WeightLifting
    ,@TableTennis
    ,@Badminton
    ,@Rowing
    ,@Judo
    ,@UpdateTime
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_OlympicManager_Update
	@ManagerId uniqueidentifier, 
	@Football int, -- 足球金牌数量
	@Basketball int, -- 篮球金牌数量
	@Volleyball int, -- 排球金牌数量
	@Swimming int, -- 游泳金牌数量
	@Gymnastics int, -- 体操金牌数量
	@Shooting int, -- 射击金牌数量
	@TrackAndField int, -- 田径金牌数量
	@WeightLifting int, -- 举重金牌数量
	@TableTennis int, -- 乒乓球金牌数量
	@Badminton int, -- 羽毛球金牌数量
	@Rowing int, -- 赛艇金牌数量
	@Judo int, -- 柔道金牌数量
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Olympic_Manager] SET
	[Football] = @Football
	,[Basketball] = @Basketball
	,[Volleyball] = @Volleyball
	,[Swimming] = @Swimming
	,[Gymnastics] = @Gymnastics
	,[Shooting] = @Shooting
	,[TrackAndField] = @TrackAndField
	,[WeightLifting] = @WeightLifting
	,[TableTennis] = @TableTennis
	,[Badminton] = @Badminton
	,[Rowing] = @Rowing
	,[Judo] = @Judo
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


