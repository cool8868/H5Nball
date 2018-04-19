
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].A8csdkstate_Delete    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkState_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkState_Delete]
GO

/****** Object:  Stored Procedure [dbo].A8csdkState_GetById    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkState_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkState_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].A8csdkState_GetAll    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkState_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkState_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].A8csdkState_Insert    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkState_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkState_Insert]
GO

/****** Object:  Stored Procedure [dbo].A8csdkState_Update    Script Date: 2016年5月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_A8csdkState_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_A8csdkState_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_A8csdkState_Delete
	@Idx int
AS

DELETE FROM [dbo].[A8csdk_State]
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

CREATE PROCEDURE [dbo].P_A8csdkState_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[A8csdk_State] with(nolock)
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

CREATE PROCEDURE [dbo].P_A8csdkState_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[A8csdk_State] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_A8csdkState_Insert
	@Idx int
	,@GameOrderId varchar(50)
	,@OrderState varchar(20)
AS

-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY


INSERT INTO [dbo].[A8csdk_State] (
	[Idx]
	,[GameOrderId]
	,[OrderState]
) VALUES (
    @Idx
    ,@GameOrderId
    ,@OrderState
)

select @Idx

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

CREATE PROCEDURE [dbo].P_A8csdkState_Update
	@Idx int, 
	@GameOrderId varchar(50), 
	@OrderState varchar(20) 
AS



UPDATE [dbo].[A8csdk_State] SET
	[GameOrderId] = @GameOrderId
	,[OrderState] = @OrderState
WHERE
	[Idx] = @Idx

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


