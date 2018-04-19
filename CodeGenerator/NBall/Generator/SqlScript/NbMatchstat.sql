
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmatchstat_Delete    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbMatchstat_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbMatchstat_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbMatchstat_GetById    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbMatchstat_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbMatchstat_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbMatchstat_GetAll    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbMatchstat_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbMatchstat_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbMatchstat_Insert    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbMatchstat_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbMatchstat_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbMatchstat_Update    Script Date: 2016年3月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbMatchstat_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbMatchstat_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbMatchstat_Delete
	@Idx int
AS

DELETE FROM [dbo].[NB_MatchStat]
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

CREATE PROCEDURE [dbo].P_NbMatchstat_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_MatchStat] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbMatchstat_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_MatchStat] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbMatchstat_Insert
	@ManagerId uniqueidentifier , 
	@MatchType int , 
	@Win int , 
	@Lose int , 
	@Draw int , 
	@Goals int , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_MatchStat] (
	[ManagerId]
	,[MatchType]
	,[Win]
	,[Lose]
	,[Draw]
	,[Goals]
	,[UpdateTime]
) VALUES (
    @ManagerId
    ,@MatchType
    ,@Win
    ,@Lose
    ,@Draw
    ,@Goals
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_NbMatchstat_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@MatchType int, 
	@Win int, 
	@Lose int, 
	@Draw int, 
	@Goals int, -- 进球数
	@UpdateTime datetime 
AS



UPDATE [dbo].[NB_MatchStat] SET
	[ManagerId] = @ManagerId
	,[MatchType] = @MatchType
	,[Win] = @Win
	,[Lose] = @Lose
	,[Draw] = @Draw
	,[Goals] = @Goals
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



