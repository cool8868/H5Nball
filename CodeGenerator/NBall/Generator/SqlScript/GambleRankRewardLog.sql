
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gamblerankrewardlog_Delete    Script Date: 2014年9月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRankrewardlog_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRankrewardlog_Delete]
GO

/****** Object:  Stored Procedure [dbo].GambleRankrewardlog_GetById    Script Date: 2014年9月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRankrewardlog_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRankrewardlog_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GambleRankrewardlog_GetAll    Script Date: 2014年9月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRankrewardlog_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRankrewardlog_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GambleRankrewardlog_Insert    Script Date: 2014年9月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRankrewardlog_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRankrewardlog_Insert]
GO

/****** Object:  Stored Procedure [dbo].GambleRankrewardlog_Update    Script Date: 2014年9月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleRankrewardlog_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleRankrewardlog_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GambleRankrewardlog_Delete
	@Idx int
AS

DELETE FROM [dbo].[Gamble_RankRewardLog]
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

CREATE PROCEDURE [dbo].P_GambleRankrewardlog_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_RankRewardLog] with(nolock)
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

CREATE PROCEDURE [dbo].P_GambleRankrewardlog_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_RankRewardLog] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GambleRankrewardlog_Insert
	@Status int , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Gamble_RankRewardLog] (
	[Status]
	,[UpdateTime]
) VALUES (
    @Status
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

CREATE PROCEDURE [dbo].P_GambleRankrewardlog_Update
	@Idx int, 
	@Status int, -- 0没做任何处理，1发奖中，2发奖完成
	@UpdateTime datetime 
AS



UPDATE [dbo].[Gamble_RankRewardLog] SET
	[Status] = @Status
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



