
-----------------------------
--drop the old procedures

/****** Object:  Stored Procedure [dbo].Managerskillwillsrc_Delete    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillWillsrc_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillWillsrc_Delete]
GO

/****** Object:  Stored Procedure [dbo].ManagerskillWillsrc_GetById    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillWillsrc_GetById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillWillsrc_GetById]
GO

	
	
/****** Object:  Stored Procedure [dbo].ManagerskillWillsrc_GetAll    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillWillsrc_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillWillsrc_GetAll]
GO

	
	

/****** Object:  Stored Procedure [dbo].ManagerskillWillsrc_Insert    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillWillsrc_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillWillsrc_Insert]
GO

/****** Object:  Stored Procedure [dbo].ManagerskillWillsrc_Update    Script Date: 2015年10月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[P_ManagerskillWillsrc_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[P_ManagerskillWillsrc_Update]
GO

	

-------------------------------------------------

    
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].P_ManagerskillWillsrc_Delete
	@Id bigint
	,@RowVersion timestamp
AS

DELETE FROM [dbo].[ManagerSkill_WillSrc]
WHERE
	[Id] = @Id
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

CREATE PROCEDURE [dbo].P_ManagerskillWillsrc_GetById
	@Id bigint
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ManagerSkill_WillSrc] with(nolock)
WHERE
	[Id] = @Id
	
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

CREATE PROCEDURE [dbo].P_ManagerskillWillsrc_GetAll
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT ON

SELECT
	*
FROM
	[dbo].[ManagerSkill_WillSrc] with(nolock)
	
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


CREATE PROCEDURE [dbo].P_ManagerskillWillsrc_Insert
	@ManagerId uniqueidentifier , 
	@SkillCode varchar(20) , 
	@PartMap varchar(2000) , 
	@EnableFlag int , 
	@RowTime datetime , 
    @Id bigint OUTPUT 
AS

SET LOCK_TIMEOUT 2000
SET XACT_ABORT ON
SET NOCOUNT OFF


INSERT INTO [dbo].[ManagerSkill_WillSrc] (
	[ManagerId]
	,[SkillCode]
	,[PartMap]
	,[EnableFlag]
	,[RowTime]
) VALUES (
    @ManagerId
    ,@SkillCode
    ,@PartMap
    ,@EnableFlag
    ,@RowTime
)


SET @Id = @@IDENTITY




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

CREATE PROCEDURE [dbo].P_ManagerskillWillsrc_Update
	@Id bigint, -- Id
	@ManagerId uniqueidentifier, -- 经理Id
	@SkillCode varchar(20), -- SkillCode
	@PartMap varchar(2000), -- 球员组成
	@EnableFlag int, -- 完成标记
	@RowTime datetime, -- RowTime
	@RowVersion timestamp -- RowVersion
AS



UPDATE [dbo].[ManagerSkill_WillSrc] SET
	[ManagerId] = @ManagerId
	,[SkillCode] = @SkillCode
	,[PartMap] = @PartMap
	,[EnableFlag] = @EnableFlag
	,[RowTime] = @RowTime
WHERE
	[Id] = @Id
	AND [RowVersion] = @RowVersion

RETURN 0

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



