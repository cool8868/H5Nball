
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].A8csdkstartgame_Delete    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkStartgame_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkStartgame_Delete]
GO

/****** Object:  Stored Procedure [dbo].A8csdkStartgame_GetById    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkStartgame_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkStartgame_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].A8csdkStartgame_GetAll    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkStartgame_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkStartgame_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].A8csdkStartgame_Insert    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkStartgame_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkStartgame_Insert]
GO

/****** Object:  Stored Procedure [dbo].A8csdkStartgame_Update    Script Date: 2016年6月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkStartgame_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkStartgame_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_A8csdkStartgame_Delete
	@OpenId varchar(200)
AS

DELETE FROM [dbo].[A8csdk_StartGame]
WHERE
	[OpenId] = @OpenId

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

CREATE PROCEDURE [dbo].P_A8csdkStartgame_GetById
	@OpenId varchar(200)
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[A8csdk_StartGame] with(nolock)
WHERE
	[OpenId] = @OpenId
	
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

CREATE PROCEDURE [dbo].P_A8csdkStartgame_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[A8csdk_StartGame] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_A8csdkStartgame_Insert
	@OpenId varchar(200)
	,@State varchar(300)
	,@ServerId varchar(50)
	,@Pf varchar(50)
	,@SessionId varchar(50)
	,@JsNeed varchar(50)
	,@NickName varchar(50)
	,@Common varchar(2000)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[A8csdk_StartGame] (
	[OpenId]
	,[State]
	,[ServerId]
	,[Pf]
	,[SessionId]
	,[JsNeed]
	,[NickName]
	,[Common]
) VALUES (
    @OpenId
    ,@State
    ,@ServerId
    ,@Pf
    ,@SessionId
    ,@JsNeed
    ,@NickName
    ,@Common
)

select @OpenId

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

CREATE PROCEDURE [dbo].P_A8csdkStartgame_Update
	@OpenId varchar(200), 
	@State varchar(300), 
	@ServerId varchar(50), 
	@Pf varchar(50), 
	@SessionId varchar(50), 
	@JsNeed varchar(50), 
	@NickName varchar(50), 
	@Common varchar(2000) 
AS



UPDATE [dbo].[A8csdk_StartGame] SET
	[State] = @State
	,[ServerId] = @ServerId
	,[Pf] = @Pf
	,[SessionId] = @SessionId
	,[JsNeed] = @JsNeed
	,[NickName] = @NickName
	,[Common] = @Common
WHERE
	[OpenId] = @OpenId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


