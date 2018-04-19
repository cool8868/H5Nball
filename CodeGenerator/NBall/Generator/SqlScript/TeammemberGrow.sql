
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Teammembergrow_Delete    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberGrow_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberGrow_Delete]
GO

/****** Object:  Stored Procedure [dbo].TeammemberGrow_GetById    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberGrow_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberGrow_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].TeammemberGrow_GetAll    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberGrow_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberGrow_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].TeammemberGrow_Insert    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberGrow_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberGrow_Insert]
GO

/****** Object:  Stored Procedure [dbo].TeammemberGrow_Update    Script Date: 2015年10月18日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_TeammemberGrow_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_TeammemberGrow_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_TeammemberGrow_Delete
	@Idx uniqueidentifier
AS

DELETE FROM [dbo].[Teammember_Grow]
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

CREATE PROCEDURE [dbo].P_TeammemberGrow_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Teammember_Grow] with(nolock)
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

CREATE PROCEDURE [dbo].P_TeammemberGrow_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Teammember_Grow] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_TeammemberGrow_Insert
	@ManagerId uniqueidentifier , 
	@GrowLevel int , 
	@GrowNum int , 
	@DayGrowCount int , 
	@DayFastGrowCount int , 
	@DayFreeFastGrowCount int , 
	@RecordDate datetime , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Teammember_Grow] (
	[Idx],
	[ManagerId]
	,[GrowLevel]
	,[GrowNum]
	,[DayGrowCount]
	,[DayFastGrowCount]
	,[DayFreeFastGrowCount]
	,[RecordDate]
	,[RowTime]
) VALUES (
	@Idx,
    @ManagerId
    ,@GrowLevel
    ,@GrowNum
    ,@DayGrowCount
    ,@DayFastGrowCount
    ,@DayFreeFastGrowCount
    ,@RecordDate
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

CREATE PROCEDURE [dbo].P_TeammemberGrow_Update
	@Idx uniqueidentifier, -- 球员ID
	@ManagerId uniqueidentifier, -- 经理ID
	@GrowLevel int, -- 球员成长阶段
	@GrowNum int, -- 累计成长值
	@DayGrowCount int, -- 当天使用普通成长次数
	@DayFastGrowCount int, -- 当天使用快速成长次数
	@DayFreeFastGrowCount int, -- 当天使用免费快速成长次数
	@RecordDate datetime, -- 记录日期
	@RowTime datetime 
AS



UPDATE [dbo].[Teammember_Grow] SET
	[ManagerId] = @ManagerId
	,[GrowLevel] = @GrowLevel
	,[GrowNum] = @GrowNum
	,[DayGrowCount] = @DayGrowCount
	,[DayFastGrowCount] = @DayFastGrowCount
	,[DayFreeFastGrowCount] = @DayFreeFastGrowCount
	,[RecordDate] = @RecordDate
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



