
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Gambletitle_Delete    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleTitle_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleTitle_Delete]
GO

/****** Object:  Stored Procedure [dbo].GambleTitle_GetById    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleTitle_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleTitle_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].GambleTitle_GetAll    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleTitle_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleTitle_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].GambleTitle_Insert    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleTitle_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleTitle_Insert]
GO

/****** Object:  Stored Procedure [dbo].GambleTitle_Update    Script Date: 2016年6月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_GambleTitle_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_GambleTitle_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_GambleTitle_Delete
	@Idx uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[Gamble_Title]
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

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

CREATE PROCEDURE [dbo].P_GambleTitle_GetById
	@Idx uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Title] with(nolock)
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

CREATE PROCEDURE [dbo].P_GambleTitle_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Gamble_Title] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_GambleTitle_Insert
	@Title nvarchar(500) , 
	@IsOfficial int , 
	@ResultFlagId uniqueidentifier , 
	@StartTime datetime , 
	@StopTime datetime , 
	@OpenTime datetime , 
	@CurrentCount int , 
	@Status int , 
	@RowTime datetime , 
    @Idx uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Gamble_Title] (
	[Idx],
	[Title]
	,[IsOfficial]
	,[ResultFlagId]
	,[StartTime]
	,[StopTime]
	,[OpenTime]
	,[CurrentCount]
	,[Status]
	,[RowTime]
) VALUES (
	@Idx,
    @Title
    ,@IsOfficial
    ,@ResultFlagId
    ,@StartTime
    ,@StopTime
    ,@OpenTime
    ,@CurrentCount
    ,@Status
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

CREATE PROCEDURE [dbo].P_GambleTitle_Update
	@Idx uniqueidentifier, 
	@Title nvarchar(500), -- 竞猜场次主题
	@IsOfficial int, -- 是否只能官方参与
	@ResultFlagId uniqueidentifier, -- 最终胜负选项
	@StartTime datetime, -- 开始竞猜时间
	@StopTime datetime, -- 竞猜截至时间
	@OpenTime datetime, -- 开奖时间
	@CurrentCount int, -- 当前参与人数（最多3人）
	@Status int, -- 状态，0为初始，1为开奖中,2为已开奖
	@RowTime datetime, -- 创建时间
	@RowVersion timestamp -- Version
AS



UPDATE [dbo].[Gamble_Title] SET
	[Title] = @Title
	,[IsOfficial] = @IsOfficial
	,[ResultFlagId] = @ResultFlagId
	,[StartTime] = @StartTime
	,[StopTime] = @StopTime
	,[OpenTime] = @OpenTime
	,[CurrentCount] = @CurrentCount
	,[Status] = @Status
	,[RowTime] = @RowTime
WHERE
	[Idx] = @Idx
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


