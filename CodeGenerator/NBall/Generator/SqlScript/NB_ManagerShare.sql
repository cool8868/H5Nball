
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagershare_Delete    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagershare_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagershare_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagershare_GetById    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagershare_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagershare_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagershare_GetAll    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagershare_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagershare_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagershare_Insert    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagershare_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagershare_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagershare_Update    Script Date: 2016年8月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagershare_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagershare_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagershare_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[NB_ManagerShare]
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

CREATE PROCEDURE [dbo].P_NbManagershare_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerShare] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagershare_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerShare] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagershare_Insert
	@InTime datetime , 
	@OutTime datetime , 
	@InPut int , 
	@OutPut int , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerShare] (
	[ManagerId],
	[InTime]
	,[OutTime]
	,[InPut]
	,[OutPut]
) VALUES (
	@ManagerId,
    @InTime
    ,@OutTime
    ,@InPut
    ,@OutPut
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

CREATE PROCEDURE [dbo].P_NbManagershare_Update
	@ManagerId uniqueidentifier, 
	@InTime datetime, 
	@OutTime datetime, 
	@InPut int, 
	@OutPut int 
AS



UPDATE [dbo].[NB_ManagerShare] SET
	[InTime] = @InTime
	,[OutTime] = @OutTime
	,[InPut] = @InPut
	,[OutPut] = @OutPut
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


