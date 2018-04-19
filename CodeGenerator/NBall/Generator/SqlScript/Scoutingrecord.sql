
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Scoutingrecord_Delete    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].ScoutingRecord_GetById    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ScoutingRecord_GetAll    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ScoutingRecord_Insert    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].ScoutingRecord_Update    Script Date: 2015年12月7日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ScoutingRecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ScoutingRecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ScoutingRecord_Delete
	@Idx int
AS

DELETE FROM [dbo].[Scouting_Record]
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

CREATE PROCEDURE [dbo].P_ScoutingRecord_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Scouting_Record] with(nolock)
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

CREATE PROCEDURE [dbo].P_ScoutingRecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Scouting_Record] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ScoutingRecord_Insert
	@ManagerId uniqueidentifier , 
	@ScoutingType int , 
	@ItemCode int , 
	@ItemString varchar(100) , 
	@Strength int , 
	@IsBinding bit , 
	@RowTime datetime , 
	@Status int , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Scouting_Record] (
	[ManagerId]
	,[ScoutingType]
	,[ItemCode]
	,[ItemString]
	,[Strength]
	,[IsBinding]
	,[RowTime]
	,[Status]
) VALUES (
    @ManagerId
    ,@ScoutingType
    ,@ItemCode
    ,@ItemString
    ,@Strength
    ,@IsBinding
    ,@RowTime
    ,@Status
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

CREATE PROCEDURE [dbo].P_ScoutingRecord_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@ScoutingType int, -- 球探类型
	@ItemCode int, -- 获得的物品code
	@ItemString varchar(100), -- 物品串
	@Strength int, -- 强化等级
	@IsBinding bit, -- 是否绑定
	@RowTime datetime, 
	@Status int -- 状态:0,初始;1,已领
AS



UPDATE [dbo].[Scouting_Record] SET
	[ManagerId] = @ManagerId
	,[ScoutingType] = @ScoutingType
	,[ItemCode] = @ItemCode
	,[ItemString] = @ItemString
	,[Strength] = @Strength
	,[IsBinding] = @IsBinding
	,[RowTime] = @RowTime
	,[Status] = @Status
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



