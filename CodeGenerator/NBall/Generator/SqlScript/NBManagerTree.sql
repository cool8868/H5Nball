
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagertree_Delete    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagertree_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagertree_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagertree_GetById    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagertree_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagertree_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagertree_GetAll    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagertree_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagertree_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagertree_Insert    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagertree_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagertree_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagertree_Update    Script Date: 2016年5月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagertree_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagertree_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagertree_Delete
	@Idx int
AS

DELETE FROM [dbo].[NB_ManagerTree]
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

CREATE PROCEDURE [dbo].P_NbManagertree_GetById
	@Idx int
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerTree] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagertree_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerTree] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagertree_Insert
	@ManagerId uniqueidentifier , 
	@SkillCode varchar(10) , 
	@Points int , 
	@Status int , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @Idx int OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerTree] (
	[ManagerId]
	,[SkillCode]
	,[Points]
	,[Status]
	,[UpdateTime]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@SkillCode
    ,@Points
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

CREATE PROCEDURE [dbo].P_NbManagertree_Update
	@Idx int, 
	@ManagerId uniqueidentifier, 
	@SkillCode varchar(10), 
	@Points int, 
	@Status int, 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[NB_ManagerTree] SET
	[ManagerId] = @ManagerId
	,[SkillCode] = @SkillCode
	,[Points] = @Points
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



