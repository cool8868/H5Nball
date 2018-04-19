
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Laddermanagerhistory_Delete    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManagerhistory_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManagerhistory_Delete]
GO

/****** Object:  Stored Procedure [dbo].LadderManagerhistory_GetById    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManagerhistory_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManagerhistory_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].LadderManagerhistory_GetAll    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManagerhistory_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManagerhistory_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].LadderManagerhistory_Insert    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManagerhistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManagerhistory_Insert]
GO

/****** Object:  Stored Procedure [dbo].LadderManagerhistory_Update    Script Date: 2016年1月11日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_LadderManagerhistory_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_LadderManagerhistory_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_LadderManagerhistory_Delete
	@Idx int
AS

DELETE FROM [dbo].[Ladder_ManagerHistory]
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

CREATE PROCEDURE [dbo].P_LadderManagerhistory_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_ManagerHistory] with(nolock)
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

CREATE PROCEDURE [dbo].P_LadderManagerhistory_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Ladder_ManagerHistory] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_LadderManagerhistory_Insert
	@Season int , 
	@ManagerId uniqueidentifier , 
	@Rank int , 
	@Score int , 
	@PrizeItems varchar(50) , 
	@Status int , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Ladder_ManagerHistory] (
	[Season]
	,[ManagerId]
	,[Rank]
	,[Score]
	,[PrizeItems]
	,[Status]
	,[RowTime]
	,[UpdateTime]
) VALUES (
    @Season
    ,@ManagerId
    ,@Rank
    ,@Score
    ,@PrizeItems
    ,@Status
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_LadderManagerhistory_Update
	@Idx int, 
	@Season int, -- 所属赛季
	@ManagerId uniqueidentifier, -- 经理id
	@Rank int, 
	@Score int, -- 积分
	@PrizeItems varchar(50), -- 格式：ItemCode,Strength,IsBinding|ItemCode,Strength,IsBinding
	@Status int, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Ladder_ManagerHistory] SET
	[Season] = @Season
	,[ManagerId] = @ManagerId
	,[Rank] = @Rank
	,[Score] = @Score
	,[PrizeItems] = @PrizeItems
	,[Status] = @Status
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



