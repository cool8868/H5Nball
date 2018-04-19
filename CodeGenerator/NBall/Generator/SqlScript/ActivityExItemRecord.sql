
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Activityexitemrecord_Delete    Script Date: 2016年9月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexItemrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexItemrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ActivityexItemrecord_GetById    Script Date: 2016年9月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexItemrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexItemrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ActivityexItemrecord_GetAll    Script Date: 2016年9月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexItemrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexItemrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ActivityexItemrecord_Insert    Script Date: 2016年9月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexItemrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexItemrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ActivityexItemrecord_Update    Script Date: 2016年9月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ActivityexItemrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ActivityexItemrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ActivityexItemrecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[ActivityEx_ItemRecord]
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

CREATE PROCEDURE [dbo].P_ActivityexItemrecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_ItemRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_ActivityexItemrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ActivityEx_ItemRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ActivityexItemrecord_Insert
	@ManagerId uniqueidentifier , 
	@ZoneActivityId int , 
	@ExcitingId int , 
	@GroupId int , 
	@ItemString varchar(max) , 
	@RecordDate date , 
	@Status int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ActivityEx_ItemRecord] (
	[ManagerId]
	,[ZoneActivityId]
	,[ExcitingId]
	,[GroupId]
	,[ItemString]
	,[RecordDate]
	,[Status]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@ZoneActivityId
    ,@ExcitingId
    ,@GroupId
    ,@ItemString
    ,@RecordDate
    ,@Status
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_ActivityexItemrecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ZoneActivityId int, 
	@ExcitingId int, 
	@GroupId int, 
	@ItemString varchar(max), -- 可得到的物品（itemtype,itemcode,itemcount|）
	@RecordDate date, -- 刷新时间
	@Status int, -- 状态  0or1=未发奖 2=已发奖
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[ActivityEx_ItemRecord] SET
	[ManagerId] = @ManagerId
	,[ZoneActivityId] = @ZoneActivityId
	,[ExcitingId] = @ExcitingId
	,[GroupId] = @GroupId
	,[ItemString] = @ItemString
	,[RecordDate] = @RecordDate
	,[Status] = @Status
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


