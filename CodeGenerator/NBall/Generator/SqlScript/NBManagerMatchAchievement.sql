
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagermatchachievement_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagermatchachievement_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagermatchachievement_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagermatchachievement_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagermatchachievement_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagermatchachievement_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagermatchachievement_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagermatchachievement_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagermatchachievement_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagermatchachievement_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagermatchachievement_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagermatchachievement_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagermatchachievement_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagermatchachievement_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagermatchachievement_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagermatchachievement_Delete
	@Idx int
AS

DELETE FROM [dbo].[NB_ManagerMatchAchievement]
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

CREATE PROCEDURE [dbo].P_NbManagermatchachievement_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerMatchAchievement] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagermatchachievement_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerMatchAchievement] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagermatchachievement_Insert
	@ManagerId uniqueidentifier , 
	@MatchType int , 
	@MatchTypeId int , 
	@RankIndex int , 
	@Status int , 
	@Rowtime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerMatchAchievement] (
	[ManagerId]
	,[MatchType]
	,[MatchTypeId]
	,[RankIndex]
	,[Status]
	,[Rowtime]
) VALUES (
    @ManagerId
    ,@MatchType
    ,@MatchTypeId
    ,@RankIndex
    ,@Status
    ,@Rowtime
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

CREATE PROCEDURE [dbo].P_NbManagermatchachievement_Update
	@Idx int, 
	@ManagerId uniqueidentifier, -- 经理ID
	@MatchType int, -- 比赛类型
	@MatchTypeId int, -- 比赛ID（比如是某个杯赛的ID）
	@RankIndex int, -- 排名
	@Status int, -- 状态
	@Rowtime datetime -- 创建时间
AS



UPDATE [dbo].[NB_ManagerMatchAchievement] SET
	[ManagerId] = @ManagerId
	,[MatchType] = @MatchType
	,[MatchTypeId] = @MatchTypeId
	,[RankIndex] = @RankIndex
	,[Status] = @Status
	,[Rowtime] = @Rowtime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



