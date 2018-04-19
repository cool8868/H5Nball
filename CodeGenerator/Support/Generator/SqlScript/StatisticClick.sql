
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Statisticclick_Delete    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticClick_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticClick_Delete]
GO

/****** Object:  Stored Procedure [dbo].StatisticClick_GetById    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticClick_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticClick_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].StatisticClick_GetAll    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticClick_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticClick_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].StatisticClick_Insert    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticClick_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticClick_Insert]
GO

/****** Object:  Stored Procedure [dbo].StatisticClick_Update    Script Date: 2016年1月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_StatisticClick_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_StatisticClick_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_StatisticClick_Delete
	@Idx bigint
AS

DELETE FROM [dbo].[Statistic_Click]
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

CREATE PROCEDURE [dbo].P_StatisticClick_GetById
	@Idx bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Click] with(nolock)
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

CREATE PROCEDURE [dbo].P_StatisticClick_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Statistic_Click] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_StatisticClick_Insert
	@Type int , 
	@RecordDate datetime , 
	@Count int , 
    @Idx bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Statistic_Click] (
	[Type]
	,[RecordDate]
	,[Count]
) VALUES (
    @Type
    ,@RecordDate
    ,@Count
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

CREATE PROCEDURE [dbo].P_StatisticClick_Update
	@Idx bigint, 
	@Type int, 
	@RecordDate datetime, 
	@Count int 
AS



UPDATE [dbo].[Statistic_Click] SET
	[Type] = @Type
	,[RecordDate] = @RecordDate
	,[Count] = @Count
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



