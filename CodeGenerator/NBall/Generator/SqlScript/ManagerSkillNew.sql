﻿
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Managerskillnew_Delete    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillNew_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillNew_Delete]
GO

/****** Object:  Stored Procedure [dbo].ManagerskillNew_GetById    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillNew_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillNew_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ManagerskillNew_GetAll    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillNew_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillNew_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ManagerskillNew_Insert    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillNew_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillNew_Insert]
GO

/****** Object:  Stored Procedure [dbo].ManagerskillNew_Update    Script Date: 2016年4月5日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillNew_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillNew_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ManagerskillNew_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[ManagerSkill_New]
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

CREATE PROCEDURE [dbo].P_ManagerskillNew_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ManagerSkill_New] with(nolock)
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

CREATE PROCEDURE [dbo].P_ManagerskillNew_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ManagerSkill_New] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ManagerskillNew_Insert
	@NewSkills varchar(max) , 
	@UpdateTime datetime , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ManagerSkill_New] (
	[ManagerId],
	[NewSkills]
	,[UpdateTime]
	,[RowTime]
) VALUES (
	@ManagerId,
    @NewSkills
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

CREATE PROCEDURE [dbo].P_ManagerskillNew_Update
	@ManagerId uniqueidentifier, 
	@NewSkills varchar(max), 
	@UpdateTime datetime, 
	@RowTime datetime 
AS



UPDATE [dbo].[ManagerSkill_New] SET
	[NewSkills] = @NewSkills
	,[UpdateTime] = @UpdateTime
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



