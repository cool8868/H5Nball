
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagerhonor_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerhonor_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerhonor_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagerhonor_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerhonor_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerhonor_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagerhonor_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerhonor_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerhonor_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagerhonor_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerhonor_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerhonor_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagerhonor_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerhonor_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerhonor_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerhonor_Delete
	@Idx int
AS

DELETE FROM [dbo].[NB_ManagerHonor]
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

CREATE PROCEDURE [dbo].P_NbManagerhonor_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerHonor] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagerhonor_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerHonor] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagerhonor_Insert
	@ManagerId uniqueidentifier , 
	@MatchType int , 
	@SubType int , 
	@PeriodId int , 
	@Rank int , 
	@Rowtime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerHonor] (
	[ManagerId]
	,[MatchType]
	,[SubType]
	,[PeriodId]
	,[Rank]
	,[Rowtime]
) VALUES (
    @ManagerId
    ,@MatchType
    ,@SubType
    ,@PeriodId
    ,@Rank
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

CREATE PROCEDURE [dbo].P_NbManagerhonor_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@MatchType int, -- 比赛类型
	@SubType int, -- 二级类型
	@PeriodId int, -- 第几届
	@Rank int, -- 排名
	@Rowtime datetime 
AS



UPDATE [dbo].[NB_ManagerHonor] SET
	[ManagerId] = @ManagerId
	,[MatchType] = @MatchType
	,[SubType] = @SubType
	,[PeriodId] = @PeriodId
	,[Rank] = @Rank
	,[Rowtime] = @Rowtime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



