
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Statisticonline_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticOnline_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticOnline_Delete]
GO

/****** Object:  Stored Procedure [dbo].StatisticOnline_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticOnline_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticOnline_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].StatisticOnline_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticOnline_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticOnline_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].StatisticOnline_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticOnline_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticOnline_Insert]
GO

/****** Object:  Stored Procedure [dbo].StatisticOnline_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticOnline_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticOnline_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_StatisticOnline_Delete
	@Idx bigint
AS

DELETE FROM [dbo].[Statistic_Online]
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

CREATE PROCEDURE [dbo].P_StatisticOnline_GetById
	@Idx bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Online] with(nolock)
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

CREATE PROCEDURE [dbo].P_StatisticOnline_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Online] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_StatisticOnline_Insert
	@ZoneId int , 
	@RecordDate datetime , 
	@TotalMinutes bigint , 
	@TotalValue int , 
	@RecordCount int , 
	@MinValue int , 
	@MinTime datetime , 
	@MaxValue int , 
	@MaxTime datetime , 
	@Hour0 int , 
	@Hour1 int , 
	@Hour2 int , 
	@Hour3 int , 
	@Hour4 int , 
	@Hour5 int , 
	@Hour6 int , 
	@Hour7 int , 
	@Hour8 int , 
	@Hour9 int , 
	@Hour10 int , 
	@Hour11 int , 
	@Hour12 int , 
	@Hour13 int , 
	@Hour14 int , 
	@Hour15 int , 
	@Hour16 int , 
	@Hour17 int , 
	@Hour18 int , 
	@Hour19 int , 
	@Hour20 int , 
	@Hour21 int , 
	@Hour22 int , 
	@Hour23 int , 
	@Count0 int , 
	@Count1 int , 
	@Count2 int , 
	@Count3 int , 
	@Count4 int , 
	@Count5 int , 
	@Count6 int , 
	@Count7 int , 
	@Count8 int , 
	@Count9 int , 
	@Count10 int , 
	@Count11 int , 
	@Count12 int , 
	@Count13 int , 
	@Count14 int , 
	@Count15 int , 
	@Count16 int , 
	@Count17 int , 
	@Count18 int , 
	@Count19 int , 
	@Count20 int , 
	@Count21 int , 
	@Count22 int , 
	@Count23 int , 
	@UpdateTime datetime , 
    @Idx bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Statistic_Online] (
	[ZoneId]
	,[RecordDate]
	,[TotalMinutes]
	,[TotalValue]
	,[RecordCount]
	,[MinValue]
	,[MinTime]
	,[MaxValue]
	,[MaxTime]
	,[Hour0]
	,[Hour1]
	,[Hour2]
	,[Hour3]
	,[Hour4]
	,[Hour5]
	,[Hour6]
	,[Hour7]
	,[Hour8]
	,[Hour9]
	,[Hour10]
	,[Hour11]
	,[Hour12]
	,[Hour13]
	,[Hour14]
	,[Hour15]
	,[Hour16]
	,[Hour17]
	,[Hour18]
	,[Hour19]
	,[Hour20]
	,[Hour21]
	,[Hour22]
	,[Hour23]
	,[Count0]
	,[Count1]
	,[Count2]
	,[Count3]
	,[Count4]
	,[Count5]
	,[Count6]
	,[Count7]
	,[Count8]
	,[Count9]
	,[Count10]
	,[Count11]
	,[Count12]
	,[Count13]
	,[Count14]
	,[Count15]
	,[Count16]
	,[Count17]
	,[Count18]
	,[Count19]
	,[Count20]
	,[Count21]
	,[Count22]
	,[Count23]
	,[UpdateTime]
) VALUES (
    @ZoneId
    ,@RecordDate
    ,@TotalMinutes
    ,@TotalValue
    ,@RecordCount
    ,@MinValue
    ,@MinTime
    ,@MaxValue
    ,@MaxTime
    ,@Hour0
    ,@Hour1
    ,@Hour2
    ,@Hour3
    ,@Hour4
    ,@Hour5
    ,@Hour6
    ,@Hour7
    ,@Hour8
    ,@Hour9
    ,@Hour10
    ,@Hour11
    ,@Hour12
    ,@Hour13
    ,@Hour14
    ,@Hour15
    ,@Hour16
    ,@Hour17
    ,@Hour18
    ,@Hour19
    ,@Hour20
    ,@Hour21
    ,@Hour22
    ,@Hour23
    ,@Count0
    ,@Count1
    ,@Count2
    ,@Count3
    ,@Count4
    ,@Count5
    ,@Count6
    ,@Count7
    ,@Count8
    ,@Count9
    ,@Count10
    ,@Count11
    ,@Count12
    ,@Count13
    ,@Count14
    ,@Count15
    ,@Count16
    ,@Count17
    ,@Count18
    ,@Count19
    ,@Count20
    ,@Count21
    ,@Count22
    ,@Count23
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

CREATE PROCEDURE [dbo].P_StatisticOnline_Update
	@Idx bigint, 
	@ZoneId int, 
	@RecordDate datetime, 
	@TotalMinutes bigint, 
	@TotalValue int, 
	@RecordCount int, 
	@MinValue int, 
	@MinTime datetime, 
	@MaxValue int, 
	@MaxTime datetime, 
	@Hour0 int, 
	@Hour1 int, 
	@Hour2 int, 
	@Hour3 int, 
	@Hour4 int, 
	@Hour5 int, 
	@Hour6 int, 
	@Hour7 int, 
	@Hour8 int, 
	@Hour9 int, 
	@Hour10 int, 
	@Hour11 int, 
	@Hour12 int, 
	@Hour13 int, 
	@Hour14 int, 
	@Hour15 int, 
	@Hour16 int, 
	@Hour17 int, 
	@Hour18 int, 
	@Hour19 int, 
	@Hour20 int, 
	@Hour21 int, 
	@Hour22 int, 
	@Hour23 int, 
	@Count0 int, 
	@Count1 int, 
	@Count2 int, 
	@Count3 int, 
	@Count4 int, 
	@Count5 int, 
	@Count6 int, 
	@Count7 int, 
	@Count8 int, 
	@Count9 int, 
	@Count10 int, 
	@Count11 int, 
	@Count12 int, 
	@Count13 int, 
	@Count14 int, 
	@Count15 int, 
	@Count16 int, 
	@Count17 int, 
	@Count18 int, 
	@Count19 int, 
	@Count20 int, 
	@Count21 int, 
	@Count22 int, 
	@Count23 int, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Statistic_Online] SET
	[ZoneId] = @ZoneId
	,[RecordDate] = @RecordDate
	,[TotalMinutes] = @TotalMinutes
	,[TotalValue] = @TotalValue
	,[RecordCount] = @RecordCount
	,[MinValue] = @MinValue
	,[MinTime] = @MinTime
	,[MaxValue] = @MaxValue
	,[MaxTime] = @MaxTime
	,[Hour0] = @Hour0
	,[Hour1] = @Hour1
	,[Hour2] = @Hour2
	,[Hour3] = @Hour3
	,[Hour4] = @Hour4
	,[Hour5] = @Hour5
	,[Hour6] = @Hour6
	,[Hour7] = @Hour7
	,[Hour8] = @Hour8
	,[Hour9] = @Hour9
	,[Hour10] = @Hour10
	,[Hour11] = @Hour11
	,[Hour12] = @Hour12
	,[Hour13] = @Hour13
	,[Hour14] = @Hour14
	,[Hour15] = @Hour15
	,[Hour16] = @Hour16
	,[Hour17] = @Hour17
	,[Hour18] = @Hour18
	,[Hour19] = @Hour19
	,[Hour20] = @Hour20
	,[Hour21] = @Hour21
	,[Hour22] = @Hour22
	,[Hour23] = @Hour23
	,[Count0] = @Count0
	,[Count1] = @Count1
	,[Count2] = @Count2
	,[Count3] = @Count3
	,[Count4] = @Count4
	,[Count5] = @Count5
	,[Count6] = @Count6
	,[Count7] = @Count7
	,[Count8] = @Count8
	,[Count9] = @Count9
	,[Count10] = @Count10
	,[Count11] = @Count11
	,[Count12] = @Count12
	,[Count13] = @Count13
	,[Count14] = @Count14
	,[Count15] = @Count15
	,[Count16] = @Count16
	,[Count17] = @Count17
	,[Count18] = @Count18
	,[Count19] = @Count19
	,[Count20] = @Count20
	,[Count21] = @Count21
	,[Count22] = @Count22
	,[Count23] = @Count23
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


