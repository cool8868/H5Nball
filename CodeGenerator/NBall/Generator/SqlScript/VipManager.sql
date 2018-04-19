
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Vipmanager_Delete    Script Date: 2016年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_VipManager_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_VipManager_Delete]
GO

/****** Object:  Stored Procedure [dbo].VipManager_GetById    Script Date: 2016年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_VipManager_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_VipManager_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].VipManager_GetAll    Script Date: 2016年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_VipManager_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_VipManager_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].VipManager_Insert    Script Date: 2016年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_VipManager_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_VipManager_Insert]
GO

/****** Object:  Stored Procedure [dbo].VipManager_Update    Script Date: 2016年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_VipManager_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_VipManager_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_VipManager_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[Vip_Manager]
WHERE
	[ManagerId] = @ManagerId

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

CREATE PROCEDURE [dbo].P_VipManager_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Vip_Manager] with(nolock)
WHERE
	[ManagerId] = @ManagerId
	
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

CREATE PROCEDURE [dbo].P_VipManager_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[Vip_Manager] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_VipManager_Insert
	@VipExp int , 
	@ReceiveDate datetime , 
	@RowTime datetime , 
	@UpdateTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[Vip_Manager] (
	[ManagerId],
	[VipExp]
	,[ReceiveDate]
	,[RowTime]
	,[UpdateTime]
) VALUES (
	@ManagerId,
    @VipExp
    ,@ReceiveDate
    ,@RowTime
    ,@UpdateTime
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

CREATE PROCEDURE [dbo].P_VipManager_Update
	@ManagerId uniqueidentifier, 
	@VipExp int, -- 签到获得的VIP经验
	@ReceiveDate datetime, 
	@RowTime datetime, 
	@UpdateTime datetime 
AS



UPDATE [dbo].[Vip_Manager] SET
	[VipExp] = @VipExp
	,[ReceiveDate] = @ReceiveDate
	,[RowTime] = @RowTime
	,[UpdateTime] = @UpdateTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



