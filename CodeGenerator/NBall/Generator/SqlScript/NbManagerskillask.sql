
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagerskillask_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillask_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillask_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagerskillask_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillask_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillask_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagerskillask_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillask_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillask_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagerskillask_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillask_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillask_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagerskillask_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillask_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillask_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerskillask_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[NB_ManagerSkillAsk]
WHERE
	[ManagerId] = @ManagerId
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

CREATE PROCEDURE [dbo].P_NbManagerskillask_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerSkillAsk] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagerskillask_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerSkillAsk] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagerskillask_Insert
	@Ask1 int , 
	@Ask2 int , 
	@Ask3 int , 
	@Ask4 int , 
	@Ask5 int , 
	@AskX varchar(255) , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerSkillAsk] (
	[ManagerId],
	[Ask1]
	,[Ask2]
	,[Ask3]
	,[Ask4]
	,[Ask5]
	,[AskX]
	,[RowTime]
) VALUES (
	@ManagerId,
    @Ask1
    ,@Ask2
    ,@Ask3
    ,@Ask4
    ,@Ask5
    ,@AskX
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

CREATE PROCEDURE [dbo].P_NbManagerskillask_Update
	@ManagerId uniqueidentifier, 
	@Ask1 int, 
	@Ask2 int, 
	@Ask3 int, 
	@Ask4 int, 
	@Ask5 int, 
	@AskX varchar(255), 
	@RowTime datetime, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[NB_ManagerSkillAsk] SET
	[Ask1] = @Ask1
	,[Ask2] = @Ask2
	,[Ask3] = @Ask3
	,[Ask4] = @Ask4
	,[Ask5] = @Ask5
	,[AskX] = @AskX
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



