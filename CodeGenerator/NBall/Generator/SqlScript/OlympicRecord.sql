
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Olympicrecord_Delete    Script Date: 2016年8月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].OlympicRecord_GetById    Script Date: 2016年8月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].OlympicRecord_GetAll    Script Date: 2016年8月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].OlympicRecord_Insert    Script Date: 2016年8月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].OlympicRecord_Update    Script Date: 2016年8月2日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_OlympicRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_OlympicRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_OlympicRecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Olympic_Record]
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

CREATE PROCEDURE [dbo].P_OlympicRecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Olympic_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_OlympicRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Olympic_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_OlympicRecord_Insert
	@ManagerId uniqueidentifier , 
	@ExType int , 
	@ExItemCode int , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Olympic_Record] (
	[ManagerId]
	,[ExType]
	,[ExItemCode]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ExType
    ,@ExItemCode
    ,@RowTime
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

CREATE PROCEDURE [dbo].P_OlympicRecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ExType int, -- 兑换类型 1强化 2球星碎片3巨星碎片4钻石5元老碎片
	@ExItemCode int, -- 获得的物品
	@RowTime datetime 
AS



UPDATE [dbo].[Olympic_Record] SET
	[ManagerId] = @ManagerId
	,[ExType] = @ExType
	,[ExItemCode] = @ExItemCode
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


