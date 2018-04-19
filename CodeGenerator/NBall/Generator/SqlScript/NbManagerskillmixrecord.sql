
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Nbmanagerskillmixrecord_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillmixrecord_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillmixrecord_Delete]
GO

/****** Object:  Stored Procedure [dbo].NbManagerskillmixrecord_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillmixrecord_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillmixrecord_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].NbManagerskillmixrecord_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillmixrecord_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillmixrecord_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].NbManagerskillmixrecord_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillmixrecord_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillmixrecord_Insert]
GO

/****** Object:  Stored Procedure [dbo].NbManagerskillmixrecord_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_NbManagerskillmixrecord_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_NbManagerskillmixrecord_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_NbManagerskillmixrecord_Delete
	@ManagerId uniqueidentifier
AS

DELETE FROM [dbo].[NB_ManagerSkillMixRecord]
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

CREATE PROCEDURE [dbo].P_NbManagerskillmixrecord_GetById
	@ManagerId uniqueidentifier
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerSkillMixRecord] with(nolock)
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

CREATE PROCEDURE [dbo].P_NbManagerskillmixrecord_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[NB_ManagerSkillMixRecord] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_NbManagerskillmixrecord_Insert
	@SkillMap varchar(max) , 
	@RowTime datetime , 
    @ManagerId uniqueidentifier OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[NB_ManagerSkillMixRecord] (
	[ManagerId],
	[SkillMap]
	,[RowTime]
) VALUES (
	@ManagerId,
    @SkillMap
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

CREATE PROCEDURE [dbo].P_NbManagerskillmixrecord_Update
	@ManagerId uniqueidentifier, 
	@SkillMap varchar(max), -- 技能背包
	@RowTime datetime 
AS



UPDATE [dbo].[NB_ManagerSkillMixRecord] SET
	[SkillMap] = @SkillMap
	,[RowTime] = @RowTime
WHERE
	[ManagerId] = @ManagerId

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



