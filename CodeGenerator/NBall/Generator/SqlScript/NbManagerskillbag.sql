
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagerskillbag_Delete    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillbag_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillbag_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagerskillbag_GetById    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillbag_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillbag_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagerskillbag_GetAll    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillbag_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillbag_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagerskillbag_Insert    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillbag_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillbag_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagerskillbag_Update    Script Date: 2016年1月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillbag_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillbag_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerskillbag_Delete
	@ManagerId uniqueidentifier
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[NB_ManagerSkillBag]
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

CREATE PROCEDURE [dbo].P_NbManagerskillbag_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerSkillBag] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagerskillbag_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerSkillBag] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagerskillbag_Insert
	@SetSkills varchar(200) , 
	@SetMap varchar(3000) , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerSkillBag] (
	[ManagerId],
	[SetSkills]
	,[SetMap]
	,[RowTime]
) VALUES (
	@ManagerId,
    @SetSkills
    ,@SetMap
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

CREATE PROCEDURE [dbo].P_NbManagerskillbag_Update
	@ManagerId uniqueidentifier, 
	@SetSkills varchar(200), -- 已设置的技能
	@SetMap varchar(3000), -- 已学习的技能
	@RowTime datetime, 
	@RowVersion timestamp 
AS



UPDATE [dbo].[NB_ManagerSkillBag] SET
	[SetSkills] = @SetSkills
	,[SetMap] = @SetMap
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



