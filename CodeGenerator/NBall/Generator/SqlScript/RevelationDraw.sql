
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Revelationdraw_Delete    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationDraw_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationDraw_Delete]
GO

/****** Object:  Stored Procedure [dbo].RevelationDraw_GetById    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationDraw_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationDraw_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].RevelationDraw_GetAll    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationDraw_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationDraw_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].RevelationDraw_Insert    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationDraw_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationDraw_Insert]
GO

/****** Object:  Stored Procedure [dbo].RevelationDraw_Update    Script Date: 2017年1月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_RevelationDraw_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_RevelationDraw_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_RevelationDraw_Delete
	@DrawId uniqueidentifier
AS

DELETE FROM [dbo].[Revelation_Draw]
WHERE
	[DrawId] = @DrawId

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

CREATE PROCEDURE [dbo].P_RevelationDraw_GetById
	@DrawId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Draw] with(nolock)
WHERE
	[DrawId] = @DrawId
	
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

CREATE PROCEDURE [dbo].P_RevelationDraw_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Revelation_Draw] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_RevelationDraw_Insert
	@ManagerId uniqueidentifier , 
	@MarkId int , 
	@Schedule int , 
	@AllItemString varchar(200) , 
	@PrizeItemString varchar(200) , 
	@OpenNumber int , 
	@Status int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @DrawId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Revelation_Draw] (
	[DrawId],
	[ManagerId]
	,[MarkId]
	,[Schedule]
	,[AllItemString]
	,[PrizeItemString]
	,[OpenNumber]
	,[Status]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@DrawId,
    @ManagerId
    ,@MarkId
    ,@Schedule
    ,@AllItemString
    ,@PrizeItemString
    ,@OpenNumber
    ,@Status
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_RevelationDraw_Update
	@DrawId uniqueidentifier, 
	@ManagerId uniqueidentifier, 
	@MarkId int, 
	@Schedule int, 
	@AllItemString varchar(200), -- 翻牌的串
	@PrizeItemString varchar(200), -- 翻出的牌
	@OpenNumber int, -- 开启次数
	@Status int, 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[Revelation_Draw] SET
	[ManagerId] = @ManagerId
	,[MarkId] = @MarkId
	,[Schedule] = @Schedule
	,[AllItemString] = @AllItemString
	,[PrizeItemString] = @PrizeItemString
	,[OpenNumber] = @OpenNumber
	,[Status] = @Status
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[DrawId] = @DrawId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


