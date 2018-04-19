
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Statisticdetail_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticDetail_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticDetail_Delete]
GO

/****** Object:  Stored Procedure [dbo].StatisticDetail_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticDetail_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticDetail_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].StatisticDetail_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticDetail_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticDetail_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].StatisticDetail_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticDetail_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticDetail_Insert]
GO

/****** Object:  Stored Procedure [dbo].StatisticDetail_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticDetail_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticDetail_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_StatisticDetail_Delete
	@Idx bigint
AS

DELETE FROM [dbo].[Statistic_Detail]
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

CREATE PROCEDURE [dbo].P_StatisticDetail_GetById
	@Idx bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Detail] with(nolock)
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

CREATE PROCEDURE [dbo].P_StatisticDetail_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Detail] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_StatisticDetail_Insert
	@ZoneId int , 
	@AnalyseType int , 
	@RecordDate datetime , 
	@TotalValue int , 
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
	@UpdateTime datetime , 
    @Idx bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Statistic_Detail] (
	[ZoneId]
	,[AnalyseType]
	,[RecordDate]
	,[TotalValue]
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
	,[UpdateTime]
) VALUES (
    @ZoneId
    ,@AnalyseType
    ,@RecordDate
    ,@TotalValue
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

CREATE PROCEDURE [dbo].P_StatisticDetail_Update
	@Idx bigint, 
	@ZoneId int, 
	@AnalyseType int, 
	@RecordDate datetime, 
	@TotalValue int, 
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
	@UpdateTime datetime 
AS



UPDATE [dbo].[Statistic_Detail] SET
	[ZoneId] = @ZoneId
	,[AnalyseType] = @AnalyseType
	,[RecordDate] = @RecordDate
	,[TotalValue] = @TotalValue
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
	,[UpdateTime] = @UpdateTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


